import random
import info
import network
import config

def find_solution_seq() :
	best_network = info.infoobject(0, 100)

	dim_array = [0 for i in range(config.POPULATION_SIZE)]

	for i in range(config.POPULATION_SIZE):
		dim = random.randint(200, 1300)

		while dim in dim_array or dim % 2 > 0:
			dim = random.randint(100, 650)

		dim_array[i] = dim

		error, model = network.train(save=False, hidden_dim=dim)
		if error < best_network.error:
			best_network = info.infoobject(dim, error)
			model.save("libras_connect.dnn")

		best_network.print_info()

find_solution_seq()