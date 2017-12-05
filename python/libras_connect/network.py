import mongo_base_repository

from io import BytesIO
from PIL import Image

import math
from cntk.layers import *
import numpy as np
import image_util
import os
import config

os.chdir(config.FILEPATH)

def create_containers():
    hand_input = C.sequence.input_variable((1, config.DATA_SIZE))
    network_output = C.sequence.input_variable((1, config.NUM_LABELS))
    return hand_input, network_output

def create_model(hidden_dim=300, input=None):
    h_fwd = Recurrence(LSTM(hidden_dim))(input)
    h_fwd_s = Stabilizer()(h_fwd)
    h_bwd = Recurrence(LSTM(hidden_dim), go_backwards=True)(input)
    h_bwd_s = Stabilizer()(h_bwd)
    h = splice(h_fwd_s, h_bwd_s)
    return Dense(config.NUM_LABELS, name='classify',activation=C.softmax)(h)

def create_reader(path="data/hand_data.txt", is_training=True):
    return C.io.MinibatchSource(C.io.CTFDeserializer(path, C.io.StreamDefs(
        data=C.io.StreamDef(field='data', shape=config.DATA_SIZE),
        labels=C.io.StreamDef(field='label', shape=config.NUM_LABELS)
    )), randomize=is_training, max_sweeps=C.io.INFINITELY_REPEAT if is_training else 1)

def create_criterion_function(model, labels):
    ce = C.cross_entropy_with_softmax(model, labels)
    errs = C.classification_error(model, labels)
    return ce, errs

def train(save=False, hidden_dim=521):
    reader=create_reader(path="data/train.txt")
    hand_input, labels = create_containers()
    model = create_model(hidden_dim=hidden_dim,input=hand_input)
    loss, label_error = create_criterion_function(model, labels)

    epoch_size = 100
    lr_per_sample = [3e-4] * 4 + [1.5e-4]
    lr_per_minibatch = [lr * config.MINIBATCH_SIZE for lr in lr_per_sample]
    lr_schedule = C.learning_rate_schedule(lr_per_minibatch, C.UnitType.minibatch, epoch_size)
    momentum_as_time_constant = C.momentum_as_time_constant_schedule(700)

    learner = C.adam(parameters=model.parameters,
					 lr=lr_schedule,
					 momentum=momentum_as_time_constant,
					 gradient_clipping_threshold_per_sample=15,
					 gradient_clipping_with_truncation=True)

    progress_printer = C.logging.ProgressPrinter(tag='Training', num_epochs=config.MAX_EPOCHS)
    trainer = C.Trainer(model, (loss, label_error), learner, progress_printer)
    C.logging.log_number_of_parameters(model)

    t = 0
    for epoch in range(config.MAX_EPOCHS):
        epoch_end = (epoch + 1) * epoch_size

        while t < epoch_end:
            input_data = reader.next_minibatch(config.MINIBATCH_SIZE, input_map={
                hand_input: reader.streams.data,
                labels: reader.streams.labels
            })

            trainer.train_minibatch(input_data)

            t += input_data[labels].num_samples

        trainer.summarize_training_progress()

    print(trainer.previous_minibatch_evaluation_average)

    reader_test = create_reader(path="data/test.txt", is_training=False)
    test_input_map = {
        hand_input  : reader_test.streams.data,
        labels  : reader_test.streams.labels,
    }

    test_minibatch_size = 30
    test_result = 0.0
    num_minibatches_tested = 0

    while True:
        data = reader_test.next_minibatch(test_minibatch_size, input_map=test_input_map)
        if not data:
            break

        num_minibatches_tested += 1
        eval_error = trainer.test_minibatch(data)
        test_result = test_result + eval_error

    error_tax = test_result * 100 / num_minibatches_tested
    print("Average test error: {0:.2f}%".format(error_tax))

    if save :
        model.save("libras_connect.dnn")

    return error_tax, model