import random, string

active_tokens = []
cache = {}
TOKEN_LENGTH = 32


def generate_token(n=TOKEN_LENGTH):
    t = ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(n))
    while t in active_tokens:
        t = ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(n))
    active_tokens.append(t)
    cache[t] = []
    return t


def add_obj(token, obj):
    cache[token].append(obj)
    return len(cache[token]) - 1


def release_token(t):
    active_tokens.remove(t)
    cache[t] = None


def is_active(t):
    return t in active_tokens


def get_cache(t):
    return cache[t]