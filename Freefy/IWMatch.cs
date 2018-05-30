namespace Freefy
{
    internal class IWMatch
    {
        public string OriginalURL { get { return Original.URL; } }
        public string MatchURL { get { return Match.URL; } }

        public ImageWrapper Original;
        public ImageWrapper Match;
        public string FilePrefix { get; private set; }

        public IWMatch(ImageWrapper iw, ImageWrapper m, string filename)
        {
            Original = iw;
            Match = m;
            FilePrefix = filename;
        }
    }
}