public class ApiService : IApiService
{   
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ApiService()
    {
        _httpClient = new HttpClient();

        //URL base da API -- ajuste conforme o ambiente
        //Android emulator: http://10.0.2.2.5000
        //iOS Simulator / Windows: http://localhost:5000
        //Dispositivo fisico: http://SEU_IP:5000
        _httpClient.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlataform.Android
        ? "http://10.0.2.2.5000/"
        : "http://localhost:5000"); 

        _httpClient.Timeout = TimeSpan.FromSeconds(30);

        _options = new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<List<Item>> GetItensAsync(string? busca = null, bool? estoqueBaixo = null)
    {
        var url = "api/itens";
        var parametros = new List<string>();

        if (!string.IsNullOrWhiteSpace(busca))
            parametros.Add($"busca={Uri.EscapeDataString(busca)}");
        if (estoqueBaixo == true)
            parametros.Add("estoqueBaixo=true");

        if (parametros.Count > 0)
        url += "?" + string.Join("&", parametros);

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Item>>(json, _options) ?? new List<Item>();

    }
    public async Task<Item?> GetItemAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/itens/{id}");
        if (!response.Is.SuccessStatusCode) return null 

        var json = await response.ContentReadAsStringAsync();
        return JsonSerializer.Deserialize<Item>(json, _options);
    }
    public async Task<Item?> CriarItemAsync(CriarItemDto dto)
    {
        var content = newStringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");
            
            var response = await _httpClient.PostAsync("api/itens", content);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Item>(json, _options);
    }

    public async Task<bool> AtualizarItemAsync(int id, CriarItemDto dto)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PutAsync($"api/itens{id}, content");
            return response.IsSuccessStatusCode;
    }

    public async Task<bool> ExcluirItemAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/itens{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MovimentarEstoqueAsync(int itemId, MovimentacaoDto dto)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PostAsync($"api/itens/{itemId}/movimentar", content);
            return response.IsSuccessStatusCode;
    }

    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        var response = await _httpClient.GetAsync("api/categorias");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Categoria>>(json, _options) ?? new List<Categoria>();
    }

    public async Task<List<Fornecedor>> GetFornecedoresAsync()
    {
        var response = await _httpClient.GetAsync("api/fornecedores");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Fornecedor>>(json, _options) ?? new List <Fornecedor>();
    }
}