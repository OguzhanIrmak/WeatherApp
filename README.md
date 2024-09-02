**Project Name: WeatherApp**

WeatherApp is a desktop application built using WPF (Windows Presentation Foundation) that retrieves weather data from the OpenWeatherMap API and displays it visually. 
The application shows current weather data for a specified city, including temperature, wind speed, feels-like temperature, and wind direction.

**Technologies Used:**
- WPF (Windows Presentation Foundation): A UI framework for building modern desktop applications.
 
- OpenWeatherMap API: An API for fetching weather data.
  
- XAML (Extensible Application Markup Language): An XML-based language for defining the user interface in WPF.
  
- C#: The programming language used for the backend logic.
  
**Methods and Features:**

1- API Integration:

The application uses the OpenWeatherMap API to fetch weather data in XML format.
The XDocument.Load() method is used to retrieve and parse the XML data.
The data is then bound to the UI and presented to the user.

2-Data Binding:

The DataContext feature in WPF is used to bind the fetched weather data to the UI.
In XAML, {Binding} expressions are used to dynamically display the data in the user interface.

3-Value Converter Usage:

The IValueConverter implementation TemperatureConvertToHeight is used to convert temperature values into visual elements.
This allows the temperature value to be visually represented as the height of a thermometer.

4-Dynamic UI Update:

The UI automatically updates based on the data received from the API. For example, wind direction information is visualized with an arrow icon that rotates based on the wind direction value.
Temperature values are displayed using a visual thermometer.

5-Styling:

Styles for different colored TextBlocks are defined in XAML (e.g., Blue and Black styles).
These styles are applied to various text elements to ensure consistent UI design.

6-Image Usage and Binding:

Weather icons from the OpenWeatherMap API are dynamically displayed using the Image control.
Other images, such as thermometers and arrows, are also included in the UI via XAML.

7-Changes Made:

- Use of BeginInvoke: The Dispatcher.BeginInvoke method has been added to the asynchronous GetWeather method to ensure that user interface updates are performed on the UI thread. This change helps to make UI updates smoother and more error-free.
  
8-New Features:

- City List Loading: The LoadCitiesFromTxt method reads cities from a file and adds them to the CityList collection.
- Weather Retrieval: The GetWeather method fetches and processes weather data for the selected city from the API and displays it on the screen.
- Weather Visualization: Weather data, including direction, temperature, and wind speed, is visualized and displayed.




![Ekran görüntüsü 2024-09-02 172319](https://github.com/user-attachments/assets/d317ef1b-80ec-4fd6-b270-36cd1c02c8f2)



![Ekran görüntüsü 2024-08-31 004841](https://github.com/user-attachments/assets/1d8e6464-71f2-4b09-99a9-113bd63eb39a)
