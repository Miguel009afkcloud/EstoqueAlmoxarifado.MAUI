public interface IApiService
{
    Task<List<Item>> GetItensAsync(string? busca = null, bool? estoqueBaixo = null);
    Task<Item?> GetItemAsync(int id);
    Task<Item?> CriarItemAsync(CriarItemDto dto);
    Task<bool> AtualizarItemAsync(int id, CriarItemDto dto);
    Task<bool> ExcluirItemAsync(int id);
    Task<bool> MovimentarEstoqueAsync(int itemId, MovimentacaoDto dto);
    Task<List<Categoria>> GetCategoriasAsync();
    Task<List<Fornecedor>> GetFornecedoresAsync();
}