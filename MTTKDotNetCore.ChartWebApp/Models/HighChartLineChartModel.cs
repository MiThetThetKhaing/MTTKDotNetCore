namespace MTTKDotNetCore.ChartWebApp.Models
{
    public class HighChartLineChartModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string YAxis { get; set; }
        public string XAxis { get; set; }
        public List<SerieData?> Series { get; set; }
    }

    public class SerieData
    {
        public string Name { get; set; }

        public List<int?> Data { get; set; }
    }
}
