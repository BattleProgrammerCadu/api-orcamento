namespace api.Models;

public record Orcamento
{
    public int Id { get;set; } = default!;
    public int Cliente { get;set; } = default!;
    public int Fornecedor { get;set; } = default!;
    public string DescricaoDoServico { get;set; } = default!;
    public double ValorTotal { get;set; } = default!;
    public DateTime DataCriacao { get;set; } = default!;
}