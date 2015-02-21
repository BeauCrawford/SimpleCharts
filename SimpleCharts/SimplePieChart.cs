using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimpleCharts
{
    public class SimplePieChart : SimpleChart
    {
        public SimplePieChart()
        {
            Points = new List<SimplePieChartPoint>();
        }

        public List<SimplePieChartPoint> Points { get; private set; }

        protected override Chart GetChart()
        {
            var font = new Font("Tahoma", 50, FontStyle.Regular, GraphicsUnit.Pixel);

            var chart = new Chart();

            if (!string.IsNullOrWhiteSpace(Title))
            {
                chart.Titles.Add(Title);
                chart.Titles[0].Font = font;
            }

            chart.ChartAreas.Add("PieChartArea");
            chart.Size = new Size(2000, 2000);
            chart.Series.Clear();
            chart.Palette = ChartColorPalette.Fire;
            chart.BackColor = Color.White;
            
            chart.ChartAreas[0].BackColor = Color.White;

            chart.ChartAreas[0].Position = new ElementPosition(0, 0, 100, 100);
            chart.ChartAreas[0].Area3DStyle.Enable3D = true;

            var series = new Series
            {
                Name = "series1",
                IsVisibleInLegend = true,
                Color = System.Drawing.Color.Green,
                ChartType = SeriesChartType.Pie
            };

            series["PieLabelStyle"] = "Outside";
            series["PieDrawingStyle"] = "SoftEdge";
            series["PieLineColor"] = "Black";

            chart.Series.Add(series);

            foreach (var point in Points)
            {
                var chartPoint = series.Points.Add(Convert.ToDouble(point.Value));
                chartPoint.AxisLabel = point.AxisText;
                chartPoint.LegendText = point.LegendText;
                chartPoint.Font = font;
                chartPoint["Exploded"] = point.Exploded.ToString().ToLower();
            }

            //chart.Legends.Add("Legend1");
            //chart.Legends[0].Font = font;
            //chart.Legends[0].Position.Auto = true;

            chart.Invalidate();

            return chart;
        }
    }

    public class SimplePieChartPoint
    {
        public string AxisText { get; set; }
        public string LegendText { get; set; }
        public bool Exploded { get; set; }
        public decimal Value { get; set; }
    }
}