using System.Windows.Forms.DataVisualization.Charting;

namespace SimpleCharts
{
    public abstract class SimpleChart
    {
        protected SimpleChart()
        {
        }

        public string Title { get; set; }

        protected abstract Chart GetChart();

        public void Generate(string filePath)
        {
            var chart = GetChart();
            chart.SaveImage(filePath, ChartImageFormat.Png);
        }
    }
}
