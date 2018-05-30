using Clarifai.API;
using Clarifai.DTOs.Inputs;
using Clarifai.DTOs.Predictions;
using Freefy.Properties;
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
    class Labeler
    {
        public enum Method { ByImage = 0, ByUrl = 1 }

        public enum LabelerType { DummyLabeler = 0, FlaskLabeler = 1, ClarifaiLabeler = 2 }

        public static Method PreferredMethod = Method.ByImage;

        public static ImageLabeler CurrentLabeler = new DummyLabeler();

        public static async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            return await CurrentLabeler.GetLabelsAsync(url);
        }

        public static async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            return await CurrentLabeler.GetLabelsAsync(img);
        }

        internal static void Reset()
        {
            PreferredMethod = (Method)Settings.Default.APIMethod;
            switch ((LabelerType)Settings.Default.APIType)
            {
                case LabelerType.FlaskLabeler:
                    CurrentLabeler = new FlaskLabeler();
                    break;
                case LabelerType.ClarifaiLabeler:
                    CurrentLabeler = new ClarifaiLabeler();
                    break;
                default:
                    CurrentLabeler = new DummyLabeler();
                    break;
            }
        }

        internal static async Task<int> GetRecommended(Image img, Image[] matches)
        {
            return await CurrentLabeler.GetRecommendedMatch(img, matches);
        }
    }

    class ClarifaiLabeler : ImageLabeler
    {
        public bool CanRecommend => false;

        ClarifaiClient client;
        public ClarifaiLabeler()
        {
            client = new ClarifaiClient(Settings.Default.ClarifaiAPIKey);
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
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
            byte[] bytes = Helper.GetImageBytes(img);

            var res = await client.PublicModels.GeneralModel
               .Predict(new ClarifaiFileImage(bytes))
               .ExecuteAsync();

            Dictionary<string, double> results = new Dictionary<string, double>();

            foreach (Concept c in res.Get().Data)
                results[c.Name] = (double)c.Value;

            return results;
        }

        public Task<int> GetRecommendedMatch(Image img, Image[] matches)
        {
            throw new NotImplementedException();
        }
    }

    class DummyLabeler : ImageLabeler
    {
        public bool CanRecommend => false;

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

        public Task<int> GetRecommendedMatch(Image img, Image[] matches)
        {
            throw new NotImplementedException();
        }
    }

    class FlaskLabeler : ImageLabeler
    {
        public bool CanRecommend => true;

        public async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            return await ServerProxy.GetPredictions(url);
        }

        public async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            return await ServerProxy.GetPredictions(img);
        }

        public async Task<int> GetRecommendedMatch(Image img, Image[] matches)
        {
            return await ServerProxy.GetRecommended(img, matches);
        }
    }
}
