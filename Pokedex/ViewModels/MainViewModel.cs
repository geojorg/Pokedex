namespace Pokedex.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
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

            PokedexData pokedexData = await restService.GetPokedexDataAsync(GenerateRequestUri(PokemonSourceData));

            PokemonName = pokedexData.Pokemon[0].Name;
            PokemonImage = string.Join("", "https://www.serebii.net/", pokedexData.Pokemon[0].Img.LocalPath);
            PokemonType = string.Join(" ", pokedexData.Pokemon[0].Type);

            
            this.Pokemon = new ObservableCollection<Pokemons>(pokedexData.Pokemon);


        //If the method needs some query parameters or has a KEY. (This is OPTIONAL)
        string GenerateRequestUri(string endpoint)
            {
                string requestUri = endpoint;
                requestUri += "https://raw.githubusercontent.com/biuni/pokemonGo-pokedex/";
                requestUri += "master/pokedex.json";
                return requestUri;
            }
        }

        #endregion

        #region Attributes
        private string pokemonName;
        private string pokemonImage;
        private string pokemontype;
        private ObservableCollection<Pokemons> pokemons;
        public const string PokemonSourceData = "https://raw.githubusercontent.com/biuni/pokemonGo-pokedex/master/pokedex.json";
        #endregion

        #region Properties
        public string PokemonName
        {
            get { return this.pokemonName; }
            set { SetValue(ref this.pokemonName, value); }
        }
        public string PokemonImage
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