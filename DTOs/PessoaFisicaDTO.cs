
using System.Text.Json.Serialization;

namespace api.DTOs;

public record PessoaFisicaDTO
{
    [JsonPropertyName("nome")]
    public string Nome {get; set;} = default!;
    [JsonPropertyName("cpf")]
    public string CPF { get;set; } = default!;
    [JsonPropertyName("fone")]
    public string? Telefone { get;set; }
   

}
