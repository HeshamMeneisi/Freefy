# Server Requirments

```Python 3.6```
```Keras```
```Tensorflow```
```Numpy```
```Scipy```
```Flask```

# GUI Requirments

```.NET 4.6```

# Running the Server

```sh
python server.py
```

When the server is ready, the following message will be displayed:

```Running on http://127.0.0.1:5000/ (Press CTRL+C to quit)```

# Using the Server

```POST /getPred``` Use this function to post an image and get the predicted labels.
Returns a JSON object containing each label and the corresponding probability.

```GET /getRecToken``` Use this function to get a token to use with ```/postImage```
Returns a token string.

```POST /postImage``` Upload an image perceded by the token number. (e.g. TOKEN{IMAGEDATA})
Returns the id associated with that image.

```POST  /getRecByToken``` Calculate the recommendation for a token. The token should be passed in a JSON object (```{"token":"TOKEN"}```)
Returns the id associated with the recommended match for the first posted image, out of the next n images.

# Using the GUI

Run ```Freefy.exe``` to start up the GUI.

To use the GUI with the Flask server. You must make sure that the ```Labeler``` is set to ```FlaskLabeler``` and the ```Method``` is set to ```ByImage``` in the settings panel. Additionally, set ```File name labels``` to ```0``` in order to exclude labels inferred from file names.

![Settings Panel - Flask](/settings_flask.png "Settings Panel")