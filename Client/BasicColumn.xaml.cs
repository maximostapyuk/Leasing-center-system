using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для BasicColumn.xaml
    /// </summary>
    public partial class BasicColumn : Window
    {
        public BasicColumn()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Skoda",
                    Values = new ChartValues<double> { MethodWindow.target1 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Wolksvagen",
                Values = new ChartValues<double> { MethodWindow.target2 }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Reno",
                Values = new ChartValues<double> { MethodWindow.target3 }
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Audi",
                Values = new ChartValues<double> { MethodWindow.target4 }
            });


            Labels = new[] { "Общий весовой коэффицент" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            BasicColumn basicColumn = new BasicColumn();
            basicColumn.Show();
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(DiagramBorder, "report");
            }
        }
    }
    
}
