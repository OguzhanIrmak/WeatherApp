using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetWeather("Istanbul", "74b39257cd8e293eff746127357f8280");
        }

        void GetWeather(string city,string appId)
        {
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&mode=xml&units=metric",city,appId);

            var xmlDocument = XDocument.Load(url).Element("current");


            Climate Weather = new Climate
            {
                City = xmlDocument.Element("city").Attribute("name").Value,
                Date = DateTime.Parse(xmlDocument.Element("lastupdate").Attribute("value").Value).ToString("dd.MM.yyyy dddd"),
                MaxTemp = double.Parse(xmlDocument.Element("temperature").Attribute("max").Value, CultureInfo.InvariantCulture),
                MinTemp = double.Parse(xmlDocument.Element("temperature").Attribute("min").Value, CultureInfo.InvariantCulture),
                Temp = double.Parse(xmlDocument.Element("temperature").Attribute("value").Value, CultureInfo.InvariantCulture),
                FeelsLike = double.Parse(xmlDocument.Element("feels_like").Attribute("value").Value, CultureInfo.InvariantCulture),
                WindSpeed = double.Parse(xmlDocument.Element("wind").Element("speed").Attribute("value").Value, CultureInfo.InvariantCulture),
                Direction = double.Parse(xmlDocument.Element("wind").Element("direction").Attribute("value").Value, CultureInfo.InvariantCulture),
                Image = new BitmapImage(new Uri("https://openweathermap.org/img/wn/" + xmlDocument.Element("weather").Attribute("icon").Value + "@2x.png"))
            };
            MainGrid.DataContext = Weather;
        }
    }
    public class Climate
    {
        public string City { get; set; }
        public string Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double WindSpeed { get; set; }
        public double Direction { get; set; }
        public ImageSource Image { get; set; }
       
    }
    public class TemperatureConvertToHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double temperature=double.Parse(value.ToString());
            return temperature + 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
