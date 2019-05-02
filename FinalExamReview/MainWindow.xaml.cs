using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json; //Add this first

namespace FinalExamReview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient()) //add using HTTPClient
            {
                //add resposne from webpage MUST ADD .RESULT AT END!!!!!
                var response = client.GetAsync($@"http://pcbstuou.w27.wh-2.com/webservices/3033/api/Movies?number=100").Result;
                //see if webpage is successful
                if (response.IsSuccessStatusCode)
                {
                    //read all content from HTTP link as a string and get the result.
                    var content = response.Content.ReadAsStringAsync().Result;
                    //Call the class to create a deserialized object.
                    List<Movies> m = JsonConvert.DeserializeObject<List<Movies>>(content);

                    var count = 0;
                    var total = 0;
                    var countD = 0;
                    var totalD = 0;
                    var score = 0.0;
                    var name = "";
                    foreach (var movie in m)
                    {
                        if(movie.actor_1_name == "Robert Downey Jr.")
                        {   
                            count++;
                            total += count;
                            
                        }
                        if(movie.director_name == "Anthony Russo")
                        {
                            countD++;
                            totalD += count;
                        }
                        if(movie.imdb_score > score)
                        {
                            score = movie.imdb_score;
                            name = movie.movie_title;
                        }
                     }
            
                  lstRobertDowney.Items.Add(total);
                    lstAnthonyRusso.Items.Add(totalD);
                    lblHighestIMDB.Content = name + "with " + score;
                   

                }
            }
        }
    }
}
