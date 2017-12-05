from io import BytesIO
from PIL import Image

import math
import numpy as np

def image_from_bytes(imgdata):
    imgbytes = BytesIO(imgdata)
    img = Image.open(imgbytes)
    return img

def get_grayscale(img, size):
    return img.resize((size), Image.ANTIALIAS).convert('LA')

def get_array_from_image(img):
    im_width = img.size[0]
    im_height = img.size[1]

    array = []

    for j in range(im_height):
        array.append([img.getpixel((i, j))[0] for i in range(im_width)])

    return array

def array_from_bytes(imgdata, size):
    return get_array_from_image(get_grayscale(image_from_bytes(imgdata), size))