using System;
using Xamarin.Forms;

namespace Pokedex.ViewModels
{
    [QueryProperty("PokemonName", "name")]
    public class DetailViewModel : BaseViewModel
    {
        private string name;

        public string PokemonName
        {
            set { SetProperty(ref name, Uri.UnescapeDataString(value));}
            get { return name; }
        }
    }
}