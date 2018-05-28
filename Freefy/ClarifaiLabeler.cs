using Clarifai.API;
using Clarifai.DTOs.Inputs;
using Clarifai.DTOs.Predictions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Freefy
{
    class ClarifaiLabeler : ImageLabeler
    {
        public ClarifaiLabeler()
        {
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            ClarifaiClient client = new ClarifaiClient("a9242b9fba1c48e99d68200dfa4f34a4");
            var res = await client.PublicModels.GeneralModel
               .Predict(new ClarifaiURLImage(url))
               .ExecuteAsync();

            Dictionary<string, double> results = new Dictionary<string, double>();

            foreach (Concept c in res.Get().Data)
                results[c.Name] = (double)c.Value;

            return results;
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            ClarifaiClient client = new ClarifaiClient("a9242b9fba1c48e99d68200dfa4f34a4");
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                bytes = ms.ToArray();
            }
            var res = await client.PublicModels.GeneralModel
               .Predict(new ClarifaiFileImage(bytes))
               .ExecuteAsync();

            Dictionary<string, double> results = new Dictionary<string, double>();

            foreach (Concept c in res.Get().Data)
                results[c.Name] = (double)c.Value;

            return results;
        }
    }

    class DummyLabeler : ImageLabeler
    {
        public async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            return new Dictionary<string, double>() {
                {"is", 0.8 },
                {"this", 0.9 },
                {"dummy", 0.70 },
                {"a", 0.75 }
            };
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            return new Dictionary<string, double>() {
                {"is", 0.8 },
                {"this", 0.9 },
                {"dummy", 0.70 },
                {"a", 0.75 }
            };
        }
    }

    class FlaskLabeler : ImageLabeler
    {
        public async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            return await ServerProxy.GetPredictions(url);
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            return new Dictionary<string, double>() {
                {"is", 0.8 },
                {"this", 0.9 },
                {"dummy", 0.70 },
                {"a", 0.75 }
            };
        }
    }
}
