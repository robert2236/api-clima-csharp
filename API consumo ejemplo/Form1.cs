using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API_consumo_ejemplo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var city = textBox1.Text;
            var weather = await GetWeatherAsync(city);
            textBox2.Text = weather;
        }

        private async Task<string> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=b52c3983097db331b846b5b31be60ffc&units=metric";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                string description = data.weather[0].description;
                double temperature = data.main.temp;
                return $"El clima actual en {city} es {description} y la temperatura es {temperature} grados Celsius.";
            }
            else
            {
                return "No se pudo obtener el clima.";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
