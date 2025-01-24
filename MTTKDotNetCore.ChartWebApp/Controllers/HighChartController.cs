using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.ChartWebApp.Models;

namespace MTTKDotNetCore.ChartWebApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult LineChart()
        {
            HighChartLineChartModel model = new HighChartLineChartModel();
            model.Title = "U.S Solar Employment Growth";
            model.SubTitle = "By Job Category. Source: <a href='https://irecusa.org/programs/solar-jobs-census/' target='_blank'>IREC</a>.";
            model.XAxis = "Number of Employees";
            model.YAxis = "Range: 2010 to 2022";
            model.Series = new List<SerieData?>
            {
                new SerieData
                {
                    Name = "Installation & Developers",
                    Data = new List<int?> {43934, 48656, 65165, 81827, 112143, 142383,171533, 165174, 155157, 161454, 154610, 168960, 171558}
                },
                new SerieData
                {
                    Name = "Manufacturing",
                    Data = new List<int?> {24916, 37941, 29742, 29851, 32490, 30282,38121, 36885, 33726, 34243, 31050, 33099, 33473}
                },
                new SerieData
                {
                    Name = "Sales & Distribution",
                    Data = new List<int?> {11744, 30000, 16005, 19771, 20185, 24377, 32147, 30912, 29243, 29213, 25663, 28978, 30618}
                },
                new SerieData
                {
                    Name = "Operations & Maintenance",
                    Data = new List<int?> {null, null, null, null, null, null, null, null, 11164, 11218, 10077, 12530, 16585}
                },
                new SerieData
                {
                    Name = "Other",
                    Data = new List<int?> {21908, 5548, 8105, 11248, 8989, 11816, 18274, 17300, 13053, 11906, 10073, 11471, 11648}
                },
            };
            return View(model);
        }

        public IActionResult Column3DChart()
        {
            HighChart3DColumnModel model = new HighChart3DColumnModel();
            model.ColorByPrint = true;
            model.Data = new List<SeriesDetail?>
            {
                new SeriesDetail
                {
                    Brand = "Toyota",
                    Sales = 1795
                },
                new SeriesDetail
                {
                    Brand = "Volkswagen",
                    Sales = 1242
                },
                new SeriesDetail
                {
                    Brand = "Volvo",
                    Sales = 1074
                },
                new SeriesDetail
                {
                    Brand = "Tesla",
                    Sales = 832
                },new SeriesDetail
                {
                    Brand = "Hyundai",
                    Sales = 593
                },new SeriesDetail
                {
                    Brand = "MG",
                    Sales = 509
                },new SeriesDetail
                {
                    Brand = "Skoda",
                    Sales = 471
                },new SeriesDetail
                {
                    Brand = "BMW",
                    Sales = 442
                },new SeriesDetail
                {
                    Brand = "Ford",
                    Sales = 385
                },new SeriesDetail
                {
                    Brand = "Nissan",
                    Sales = 371
                },
            };
            return View(model);
        }
    }
}
