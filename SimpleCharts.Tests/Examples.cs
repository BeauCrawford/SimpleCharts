using NUnit.Framework;
using System;
using System.IO;

namespace SimpleCharts.Tests
{
    [TestFixture]
    public class Examples
    {
        private const string ExamplesDirectory = "Examples";

        [TestFixtureSetUp]
        public void Setup()
        {
            if (!Directory.Exists(ExamplesDirectory))
                Directory.CreateDirectory(ExamplesDirectory);
        }

        [Test]
        [Category("Integration")]
        public void Generate_Examples()
        {
            var random = new Random();

            var line = new SimpleLineChart();
            line.Title = "Customer Sales";
            
            var someSeriesValue = 1;

            var anotherSeriesValue = 100;

            for (int i = 100; i >= 1; i--)
            {
                line.AddPoint("SomeSeries", DateTime.Now.AddDays(i * -1), someSeriesValue);
                someSeriesValue = random.Next(someSeriesValue, someSeriesValue + random.Next(1,100));

                line.AddPoint("AnotherSeries", DateTime.Now.AddDays(i * -1), anotherSeriesValue);
                anotherSeriesValue = random.Next(anotherSeriesValue, anotherSeriesValue + random.Next(1,100));
            }

            line.Generate(Path.Combine(ExamplesDirectory, "SimpleLineChart.png"));

            var pie = new SimplePieChart();
            pie.Title = "Customer Sales";
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer A", Value = 70 });
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer B", Value = 20 });
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer C", Value = 10 });
            pie.Generate(Path.Combine(ExamplesDirectory, "SimplePieChart.png"));

            var bar = new SimpleBarChart();
            bar.DisplayBarText = true;           
            bar.Title = "Customer Sales";

            for (int i = 0; i < 50; i++)
            {
                bar.Points.Add(new SimpleBarChartPoint() { AxisText = "Customer " + i, Value = random.Next(0, 1000) });
            }

            bar.Generate(Path.Combine(ExamplesDirectory, "SimpleBarChart.png"));
        }
    }
}