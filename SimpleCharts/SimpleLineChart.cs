using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;

namespace SimpleCharts
{
    public class SimpleLineChart : SimpleChart
    {
        public SimpleLineChart AddPoint(string line, SimpleLineChartPoint point)
        {
            if (!_points.ContainsKey(line))
            {
                _points.Add(line, new List<SimpleLineChartPoint>());
            }

            _points[line].Add(point);

            return this;
        }

        public SimpleLineChart AddPoint(string line, DateTime x, decimal y)
        {
            return AddPoint(line, new SimpleLineChartPoint() { XValue = x, YValue = y });
        }

        private Dictionary<string, List<SimpleLineChartPoint>> _points = new Dictionary<string, List<SimpleLineChartPoint>>(StringComparer.OrdinalIgnoreCase);

        protected override Chart GetChart()
        {
            var chart = new Chart();

            chart.Size = new Size(2000, 2000);

            if (!string.IsNullOrWhiteSpace(Title))
            {
                chart.Titles.Add(Title);
                chart.Titles[0].Font = TitleFont;
            }

            var chartArea = new ChartArea();
            chartArea.AxisX.LabelStyle.Format = "dd/MMM\nhh:mm";
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            chart.ChartAreas.Add(chartArea);

            foreach (var line in _points.Keys)
            {
                var series = chart.Series.Add("Series_" + line);
                series.Color = Color.Red;
                series.ChartType = SeriesChartType.FastLine;
                series.XValueType = ChartValueType.Date;

                var entries = _points[line];
                var xValues = entries.Select(e => e.XValue).ToArray();
                var yValues = entries.Select(e => e.YValue).ToArray();

                series.Points.DataBindXY(xValues, yValues);
            }

            //chart.DataManipulator.CopySeriesValues("Series1", "Series2");
            //chart.DataManipulator.FinancialFormula(FinancialFormula.WeightedMovingAverage, "Series2");
            //chart.Series["Series2"].ChartType = SeriesChartType.FastLine;

            chart.Invalidate();

            return chart;
        }
    }

    public class SimpleLineChartPoint
    {
        public DateTime XValue { get; set; }
        public decimal YValue { get; set; }
    }
}
