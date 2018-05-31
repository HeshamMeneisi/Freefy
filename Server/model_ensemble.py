from keras.preprocessing import image
from keras.applications.xception import Xception, preprocess_input
from keras.applications import ResNet50
from keras.applications import InceptionV3
from keras.applications import VGG16
from keras.applications import VGG19
from keras.applications import imagenet_utils
from PIL import Image
import requests
import numpy as np
from io import BytesIO

model_classes = {
'vgg16': VGG16,
'vgg19': VGG19,
'inception': InceptionV3,
'xception': Xception,
'resnet50': ResNet50
}

model_weights = {
"vgg16": 0.8,
"vgg19": 1,
"inception": 1.2,
"xception": 1.5,
"resnet50": 1.2
}

models = {}

used_models = ['inception', 'resnet50', 'xception']
for model in used_models:
    type = model_classes[model]
    models[model] = type(weights='imagenet')


def get_bottleneck(model_name, img):
    assert model_name in models.keys()
    model = models[model_name]

    target_size = (224, 224)

    if model_name in ['xception', 'inception']:
        preprocess = preprocess_input
    else:
        preprocess = imagenet_utils.preprocess_input

    if img.size != target_size:
        img = img.resize(target_size)

    x = image.img_to_array(img)

    if x.shape[2] == 4:
        x = x[:,:,1:]
    elif x.shape[2] == 1:
        rgb = np.broadcast_to(x, x.shape[:2]+(3,)).copy()
        x = rgb

    x = np.expand_dims(x, axis=0)
    x = preprocess(x)

    return model.predict(x)


def predict(img, top_n=None):
    if not top_n:
        top_n = len(used_models)
    preds = {}
    for model in used_models:
        print('\n', model, ' predicts\n')
        b = get_bottleneck(model, img)
        p = imagenet_utils.decode_predictions(b, top=top_n)
        for _, label, prob in p[0]:
            print('%.2f%% %s' % (prob * 100, label))
            if label not in preds.keys():
                preds[label] = prob
            else:
                preds[label] += prob * model_weights[model]
    s = np.sum([v for v in preds.values()])
    for k in preds.keys():
        preds[k] = preds[k] / s
    return sorted(preds.items(), key=lambda x: x[1], reverse=True)


def predict_from_url(url, top_n=3):
    response = requests.get(url)
    img = Image.open(BytesIO(response.content))
    preds = predict(img, top_n=3)
    return preds
