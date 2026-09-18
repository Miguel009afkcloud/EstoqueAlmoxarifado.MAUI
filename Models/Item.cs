public class Item
{
    public int Id { get; set; }
    public string Nome { get; set;}
    public string? Descricao { get; set; }
    public string? CodigoBarras { get; set; }
    public string UnidadeMedida { get; set; }
    public int QuantidadeEstoque { get; set;}
    public int QuantidadeMinima { get; set; }
    public decimal PrecoUnitario { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataValidade { get; set;}
    public string CategoriaNome { get; set; } = string.Empty;
    public string? FornecedorNome { get; set; }

    public bool EstoqueBaixo => QuantidadeEstoque <= QuantidadeMinima;
    public Color CorEstoque => EstoqueBaixo ? Colors.Red : Colors.Green;
}