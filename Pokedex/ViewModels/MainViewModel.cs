using Pokedex.Models;
using Pokedex.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pokedex.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Services
        private APIService apiService;
        private APIService.RestService restService;
        #endregion

        #region Constructor
        public MainViewModel()
        {
            this.apiService = new APIService();
        }
        #endregion

        #region Commands
        public ICommand LoadPokemonsCommand => new Command(LoadPokemons);
        private async void LoadPokemons()
        {
            restService = new APIService.RestService();
            APIService.ConectivityStatus();

            PokedexData pokedexData = await restService.GetPokedexDataAsync(PokemonSourceData);

            PokemonName = pokedexData.Pokemon[0].Name;
            PokemonImage = pokedexData.Pokemon[0].Img;
            PokemonType = string.Join(" ", pokedexData.Pokemon[0].Type);

            var list = new ObservableCollection<Pokemons>(pokedexData.Pokemon);
            this.Pokemon = list;
        }
        
        public ICommand ItemCommand => new Command(PokemonSelected);
        private async void PokemonSelected()
        {
            if (Item != null)
            {
                Routing.RegisterRoute("Main/Detail", typeof(DetailPage));
                await Shell.Current.GoToAsync($"//Main/Detail?name={Item.Name}");
                Item = null;
            }
        }
        #endregion

        #region Attributes
        private string _pokemonName;
        private Uri _pokemonImage;
        private string _pokemontype;
        private ObservableCollection<Pokemons> pokemons;
        private Pokemons _item;
        public const string PokemonSourceData = "http://geojorgx.com/api/v2/pokedex.json";
        #endregion

        #region Properties
        public string PokemonName
        {
            get { return _pokemonName; }
            set { SetProperty(ref _pokemonName, value); }
        }
        public Uri PokemonImage
        {
            get { return _pokemonImage; }
            set { SetProperty(ref _pokemonImage, value); }
        }
        public string PokemonType
        {
            get { return _pokemontype; }
            set { SetProperty(ref _pokemontype, value); }
        }
        public ObservableCollection<Pokemons> Pokemon
        {
            get { return this.pokemons; }
            set { SetProperty(ref this.pokemons, value); }
        }
        public Pokemons Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
        #endregion
    }
}