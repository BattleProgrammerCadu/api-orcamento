namespace api.Models;

public record PessoaFisica
{
    public int Id { get;set; } = default!;
    public string Nome { get;set; } = default!;
    public string CPF { get;set; } = default!;
    public string? Telefone { get;set; }
    public DateTime DataCriacao { get;set; } = default!;
}