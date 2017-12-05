from pymongo import MongoClient

######## DATABASE ############
MONGO_CONNECTION_STRING = "libras_connect"

def get(collection):
    return [d for d in MongoClient()[MONGO_CONNECTION_STRING][collection].find()]