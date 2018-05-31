import model_ensemble
from scipy.spatial.distance import euclidean


def get_recommended(img, candidates):
    votes = {}
    for model in model_ensemble.used_models:
        b = model_ensemble.get_bottleneck(model, img)
        best = None
        best_s = None
        id = 0
        for c in candidates:
            cb = model_ensemble.get_bottleneck(model, c)
            d = euclidean(b, cb)
            s = 1/(d+1)
            if best is None or s > best_s:
                best = id
                best_s = s
            id += 1
        v = model_ensemble.model_weights[model]
        print(model, 'voted', best)
        if best in votes.keys():
            votes[best] += v
        else:
            votes[best] = v

    return next(iter(sorted(votes.items(), key=lambda x:x[1], reverse=True)))[0]
