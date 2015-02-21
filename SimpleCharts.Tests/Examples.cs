using NUnit.Framework;
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
            var pie = new SimplePieChart();
            pie.Title = "Customer Sales";
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer A", Value = 70 });
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer B", Value = 20 });
            pie.Points.Add(new SimplePieChartPoint() { AxisText = "Customer C", Value = 10 });
            pie.Generate(Path.Combine(ExamplesDirectory, "SimplePieChart.png"));
        }
    }
}