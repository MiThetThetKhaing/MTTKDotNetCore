namespace MTTKDotNetCore.ChartWebApp.Models
{
    public class HighChart3DColumnModel
    {
        public List<SeriesDetail?> Data { get; set; }
        public bool ColorByPrint { get; set; }
    }

    public class SeriesDetail
    {
        public string Brand { get; set; }

        public int Sales { get; set; }
    }
}
