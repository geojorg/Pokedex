namespace Pokedex.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

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
        public ICommand LoadPokemonsCommand
        {
            get
            {
                return new RelayCommand(LoadPokemons);
            }
        }

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
            
        #endregion

        #region Attributes
        private string pokemonName;
        private Uri pokemonImage;
        private string pokemontype;
        private ObservableCollection<Pokemons> pokemons;
        public const string PokemonSourceData = "http://geojorgx.com/api/v2/pokedex.json";
        #endregion

        #region Properties
        public string PokemonName
        {
            get { return this.pokemonName; }
            set { SetValue(ref this.pokemonName, value); }
        }
        public Uri PokemonImage
        {
            get { return this.pokemonImage; }
            set { SetValue(ref this.pokemonImage, value); }
        }
        public string PokemonType
        {
            get { return this.pokemontype; }
            set { SetValue(ref this.pokemontype, value); }
        }

        public ObservableCollection<Pokemons> Pokemon
        {
            get { return this.pokemons; }
            set { SetValue(ref this.pokemons, value); }
        }
        #endregion
    }
}