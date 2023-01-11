using api.Models;
using api.Repositorios.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositorios;

public class OrcamentoRepositorio : IServico<Orcamento>
{
    public OrcamentoRepositorio()
    {
        conexao = Environment.GetEnvironmentVariable("DATABASE_URL_API");
        if(conexao is null) conexao = "Server=localhost;Database=Api;Uid=root;Pwd=root;";
    }

    private string? conexao = null;
    async Task IServico<Orcamento>.IncluirAsync(Orcamento orcamento)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into orcamento(ClienteId, FornecedorId, DescricaoDoServico, ValorTotal, DataCriacao) values(@ClienteId, @FornecedorId, @DescricaoDoServico, @ValorTotal, @DataCriacao)";
            var command = new MySqlCommand(query, conn);
            DateTime dataCriacao = DateTime.Now;
            command.Parameters.Add(new MySqlParameter("@ClienteId", orcamento.Cliente));
            command.Parameters.Add(new MySqlParameter("@FornecedorId", orcamento.Fornecedor));
            command.Parameters.Add(new MySqlParameter("@DescricaoDoServico", orcamento.DescricaoDoServico));
            command.Parameters.Add(new MySqlParameter("@ValorTotal",  orcamento.ValorTotal));
            command.Parameters.Add(new MySqlParameter("@DataCriacao",  dataCriacao));

            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
    async Task<List<Orcamento>> IServico<Orcamento>.TodosAsync()
    {

        var listaOrcamentos = new List<Orcamento>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = "select * from orcamento";
            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                listaOrcamentos.Add(new Orcamento{
                    Id = Convert.ToInt32(dr["id"]),
                    Cliente = Convert.ToInt32(dr["ClienteId"]),
                    Fornecedor = Convert.ToInt32(dr["FornecedorId"]),
                    DescricaoDoServico = dr["DescricaoDoServico"].ToString() ?? "",
                    ValorTotal = Convert.ToDouble(dr["ValorTotal"]),
                    DataCriacao = Convert.ToDateTime(dr["DataCriacao"])
                });
            }
            conn.Close();
        };
        return listaOrcamentos;
    }
    
    async Task IServico<Orcamento>.ApagarAsync(Orcamento orcamento)
    {
       
       using(var conn = new MySqlConnection(conexao))
       {
            conn.Open();
            var query = $"delete from orcamento where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", orcamento.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
       }
    }

   async Task<Orcamento> IServico<Orcamento>.AtualizarAsync(Orcamento orcamento)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update orcamento set ClienteId=@ClienteId, FornecedorId=@FornecedorId, DescricaoDoServico=@DescricaoDoServico, ValorTotal=@ValorTotal, DataCriacao=@DataCriacao where id=@id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@ClienteId", orcamento.Cliente));
            command.Parameters.Add(new MySqlParameter("@id", orcamento.Id));
            command.Parameters.Add(new MySqlParameter("@FornecedorId", orcamento.Fornecedor));
            command.Parameters.Add(new MySqlParameter("@DescricaoDoServico", orcamento.DescricaoDoServico));
            command.Parameters.Add(new MySqlParameter("@ValorTotal", orcamento.ValorTotal));
            command.Parameters.Add(new MySqlParameter("@DataCriacao", orcamento.DataCriacao));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }

        return orcamento;
    }
}

 
           

