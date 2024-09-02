using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Collections.ObjectModel;
using System.IO;


namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> CityList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
           
           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCitiesFromTxt();
            CityListBox.ItemsSource = CityList;
            //Thread weatherThread = new Thread(GetWeather);
            //weatherThread.Start(new string[] { "Doha", "74b39257cd8e293eff746127357f8280" });
        }

        private void LoadCitiesFromTxt()
        {
           
            CityList = new ObservableCollection<string>();

           
            string filePath = "C:\\Users\\oguzh\\source\\repos\\WeatherApp\\Resources\\Cities.txt";

            
            if (File.Exists(filePath))
            {
                foreach (var city in File.ReadAllLines(filePath))
                {
                    CityList.Add(city);
                }
            }
            else
            {
                MessageBox.Show("Şehir listesini içeren dosya bulunamadı!", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        async void GetWeather(object obj)
        {
            // Unboxing
            string[] parameters=obj as string[];
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&mode=xml&units=metric", parameters[0], parameters[1]);

            var xmlDocument = XDocument.Load(url).Element("current");


            var Weather = new 
            {
                DirectionName =xmlDocument.Element("wind").Element("direction").Attribute("name").Value,
                City = xmlDocument.Element("city").Attribute("name").Value,
                Date = DateTime.Parse(xmlDocument.Element("lastupdate").Attribute("value").Value).ToString("dd.MM.yyyy dddd"),
                MaxTemp = double.Parse(xmlDocument.Element("temperature").Attribute("max").Value, CultureInfo.InvariantCulture),
                MinTemp = double.Parse(xmlDocument.Element("temperature").Attribute("min").Value, CultureInfo.InvariantCulture),
                Temp = double.Parse(xmlDocument.Element("temperature").Attribute("value").Value, CultureInfo.InvariantCulture),
                FeelsLike = double.Parse(xmlDocument.Element("feels_like").Attribute("value").Value, CultureInfo.InvariantCulture),
                WindSpeed = double.Parse(xmlDocument.Element("wind").Element("speed").Attribute("value").Value, CultureInfo.InvariantCulture),
                Direction = double.Parse(xmlDocument.Element("wind").Element("direction").Attribute("value").Value, CultureInfo.InvariantCulture),
               Image = (new Uri("https://openweathermap.org/img/wn/" + xmlDocument.Element("weather").Attribute("icon").Value + "@2x.png"))
                
            };
            //Thread.Sleep(5000);
#pragma warning disable CS4014
            MainGrid.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainGrid.DataContext = Weather;
                
            }));
#pragma warning restore CS4014
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string selectedCity = CityListBox.SelectedItem as string;

           
            if (selectedCity == null)
            {
                MessageBox.Show("Lütfen bir şehir seçin", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                
                Thread weatherThread = new Thread(GetWeather);
                weatherThread.Start(new string[] { selectedCity, "74b39257cd8e293eff746127357f8280" });
            }
        }

    }
    //public class Climate
    //{
    //    public string City { get; set; }
    //    public string Date { get; set; }
    //    public double MaxTemp { get; set; }
    //    public double MinTemp { get; set; }
    //    public double Temp { get; set; }
    //    public double FeelsLike { get; set; }
    //    public double WindSpeed { get; set; }
    //    public double Direction { get; set; }
    //    public ImageSource Image { get; set; }
    //    public string DirectionName { get; set; }
    //}
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
    public class UriToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(value as Uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
