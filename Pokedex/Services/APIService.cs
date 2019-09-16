namespace Pokedex
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class APIService
    {
        public static void ConectivityStatus()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                //Internet Connection is Ok
            }
            else
            {
                Application.Current.MainPage.DisplayAlert(
                    "No Internet Connection",
                    "Please check your internet connection and try again",
                    "Ok");
            }
        }
        public class RestService
        {
            HttpClient client = new HttpClient();
            public async Task<PokedexData> GetPokedexDataAsync(string uri)
            {
                PokedexData pokedexData = null;
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pokedexData = PokedexData.FromJson(content);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\tERROR {0}", ex.Message);
                }
                return pokedexData;
            }
        }
    }
}

