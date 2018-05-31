from flask import Flask
from flask import jsonify
from flask import request
import numpy as np
from PIL import Image
import io
import token_mgr
import model_ensemble
import recommender

app = Flask(__name__)

@app.route('/ping')
def hello_world():
    return jsonify(['Hello', 'World'])


@app.route('/getPred', methods=['POST'])
def get_pred():
    image_data = np.fromstring(request.data, np.uint8)
    img = Image.open(io.BytesIO(image_data))

    preds = {}
    i = 0
    for p in model_ensemble.predict(img=img):
        if i == 0 or p[1] > 0.3:
            for l in p[0].split('_'):
                preds[l] = float(p[1])
                i += 1

    return jsonify(preds)


@app.route('/getRecToken', methods=['GET'])
def get_rec_toekn():
    return jsonify({'token':token_mgr.generate_token()})


@app.route('/postImage', methods=['POST'])
def post_image():
    data = np.fromstring(request.data, np.uint8)
    token = data[:token_mgr.TOKEN_LENGTH].tostring().decode('ascii')
    image_data = data[token_mgr.TOKEN_LENGTH:]

    if not token_mgr.is_active(token):
        return jsonify("Unidentified Token")

    img = Image.open(io.BytesIO(image_data))
    id = token_mgr.add_obj(token, img)

    return jsonify({'id':id})


@app.route('/getRecByToken', methods=['POST'])
def get_rec_by_token():
    req = request.get_json()
    token = req['token']

    images = token_mgr.get_cache(token)

    idx = recommender.get_recommended(images[0], images[1:])

    return jsonify({'id':idx + 1})


if __name__ == '__main__':
    app.run()
