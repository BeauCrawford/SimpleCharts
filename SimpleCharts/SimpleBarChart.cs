using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimpleCharts
{
    public class SimpleBarChart : SimpleChart
    {
        public SimpleBarChart()
        {
            Points = new List<SimpleBarChartPoint>();

            XAxisFont = new Font("Tahoma", 30, FontStyle.Regular, GraphicsUnit.Pixel);
            YAxisFont = new Font("Tahoma", 30, FontStyle.Regular, GraphicsUnit.Pixel);
            BarTextFont = new Font("Tahoma", 25, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public List<SimpleBarChartPoint> Points { get; private set; }

        public Font XAxisFont { get; private set; }
        public Font YAxisFont { get; private set; }
        public Font BarTextFont { get; private set; }

        public bool DisplayBarText { get; set; }

        protected override Chart GetChart()
        {
            var chart = new Chart();
            chart.Series.Clear();
            //chart.BackColor = Color.LightYellow;
            chart.Palette = ChartColorPalette.Fire;
            chart.Size = new Size(4000, 2000);

            if (!string.IsNullOrWhiteSpace(Title))
            {
                chart.Titles.Add(Title);
                chart.Titles[0].Font = TitleFont;
            }

            var chartArea = chart.ChartAreas.Add("ChartArea");
            chartArea.BackColor = Color.Transparent;

            chartArea.AxisX.LabelStyle.Font = XAxisFont;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.MinorGrid.Enabled = false;
            chartArea.AxisX.MinorGrid.LineColor = Color.LightGray;

            chartArea.AxisY.LabelStyle.Font = YAxisFont;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MinorGrid.Enabled = false;
            chartArea.AxisY.MinorGrid.LineColor = Color.LightGray;

            chartArea.IsSameFontSizeForAllAxes = false;

            var series = new Series
            {
                Name = "ChartSeries",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };

            chart.Series.Add(series);

            int pointCounter = 1;

            foreach (var point in Points)
            {
                var chartPoint = series.Points.Add(Convert.ToDouble(point.Value));
                chartPoint.Color = Color.LightBlue;
                chartPoint.AxisLabel = point.AxisText;
                chartPoint.LegendText = point.LegendText;

                if (DisplayBarText)
                {
                    chartPoint.Label = point.Value.ToString();
                    chartPoint.Font = BarTextFont;
                }

                pointCounter++;
            }

            chart.Invalidate();

            return chart;
        }
    }

    public class SimpleBarChartPoint
    {
        public string AxisText { get; set; }
        public string LegendText { get; set; }
        public decimal Value { get; set; }
    }
}
