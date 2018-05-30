using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Freefy
{
    interface ImageLabeler
    {
        Task<Dictionary<string, double>> GetLabelsAsync(string url);
        Task<Dictionary<string, double>> GetLabelsAsync(Image img);
        Task<int> GetRecommendedMatch(Image img, Image[] matches);

        bool CanRecommend { get; }
    }
}
