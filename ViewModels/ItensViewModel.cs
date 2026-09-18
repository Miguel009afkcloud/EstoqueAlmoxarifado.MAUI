public class ItensViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private OservableCollection<Item> _itens = new();

    public ObservableCollection<Item> Itens
    {
        get => _itens;
        set { _itens = value; OnPropertyChanged(); }
    }

    private string _termoBusca = string.Empty;
    public string TermoBusca
    {
        get => _termoBusca;
        set { _termoBusca = value; OnPropertyChanged(); }
    }
    public ICommand CarregarItensCommand { get; }
    public ICommand BuscarCommand { get; }
    public ICommand ExcluirItemCommand { get; }
    public ICommand IrParaDetalheCommand { get; } 

    public ItensViewModel(IApiService apiservice)
    {
        _apiService = apiService;
        CarregarItensCommand = new Command(async() => await CarregarItensAsync());
        BuscarCommand = new Command (async () => await CarregarItensAsync(TermoBusca));
        ExcluirItemCommand = new Command<Item>(async (item) => await ExcluirItemAsync(item));
        IrParaDetalheCommand = new Command<Item>(async (item) => await IrParaDetalheAsync(item));
    }

    public async Task CarregarItensAsync(string? busca = null)
    {
        if(isBusy) return;

        try
        {
            IsBusy = true;
            var itens = await _apiService.GetItensAsync(busca);

            Itens.Clear();
            foreach (var item in itens)
                Itens.Add(item);
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Erro", $"Falha ao carregar itens: {ex.Message}", "OK");
        }
        finally 
        {
            IsBusy = false;
        }
    }
    private async Task ExcluirItemAsync(Item item)
    {
        if(item == null) return;

        bool confirmar = await Application.Current!.MainPage!.DisplayAlert(
            "Confirmar", $"Excluir {item.Nome}?", "Sim", "Não");

        if (!confirmar) return;

        var sucesso = await _apiService.ExcluirItemAsync(item.Id);
        if(sucesso)
            Itens.Remove(item);
        else
            await Application.Current!.MainPage!.DisplayAlert(
                "Erro", "Não foi possível excluir o item.", "OK");
    }
    private async Task IrParaDetalheAsync(Item item)
    {
        if (item == null) return;

        await Shell.Current.GoToAsync(nameof(ItemDetalhePage),
            new Dictionary<string, object> { { "Item", item } });
    }
}