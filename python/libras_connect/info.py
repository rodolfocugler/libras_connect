class infoobject(object):
    dim = 0,
    error = 0

    def __init__(self, dim, error):
        self.dim = dim
        self.error = error

    def print_info(self):
        print("error {0} | dim {1}".format(self.error, self.dim))
