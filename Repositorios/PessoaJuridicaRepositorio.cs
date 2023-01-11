using api.Models;
using api.Repositorios.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositorios;

public class PessoaJuridicaRepositorio : IServico<PessoaJuridica>
{
    public PessoaJuridicaRepositorio()
    {
        conexao = Environment.GetEnvironmentVariable("DATABASE_URL_API");
        if(conexao is null) conexao = "Server=localhost;Database=Api;Uid=root;Pwd=root;";
    }

    private string? conexao = null;
    async Task IServico<PessoaJuridica>.IncluirAsync(PessoaJuridica pessoaJuridica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into pessoa_juridica(nome, cnpj, telefone, data_criacao) values(@nome, @cnpj, @telefone, @data_criacao)";
            var command = new MySqlCommand(query, conn);
            DateTime dataCriacao = DateTime.Now;
            command.Parameters.Add(new MySqlParameter("@nome", pessoaJuridica.Nome));
            command.Parameters.Add(new MySqlParameter("@CNPJ", pessoaJuridica.CNPJ));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaJuridica.Telefone));
            command.Parameters.Add(new MySqlParameter("@data_criacao",  dataCriacao));

            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
    async Task<List<PessoaJuridica>> IServico<PessoaJuridica>.TodosAsync()
    {

        var listaPessoas = new List<PessoaJuridica>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = "select * from pessoa_juridica";
            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                listaPessoas.Add(new PessoaJuridica{
                    Id = Convert.ToInt32(dr["id"]),
                    Nome = dr["nome"].ToString() ?? "",
                    Telefone = dr["telefone"].ToString() ?? "",
                    CNPJ = dr["cnpj"].ToString() ?? "",
                    DataCriacao = Convert.ToDateTime(dr["data_criacao"])
                });
            }
            conn.Close();
        };
        return listaPessoas;
    }
    
    async Task IServico<PessoaJuridica>.ApagarAsync(PessoaJuridica pessoaJuridica)
    {
       
       using(var conn = new MySqlConnection(conexao))
       {
            conn.Open();
            var query = $"delete from pessoa_juridica where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaJuridica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
       }
    }

   async Task<PessoaJuridica> IServico<PessoaJuridica>.AtualizarAsync(PessoaJuridica pessoaJuridica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update pessoa_juridica set nome=@nome,telefone=@telefone,cnpj=@cnpj where id=@id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@nome", pessoaJuridica.Nome));
            command.Parameters.Add(new MySqlParameter("@cnpj", pessoaJuridica.CNPJ));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaJuridica.Telefone));
            command.Parameters.Add(new MySqlParameter("@id", pessoaJuridica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }

        return pessoaJuridica;
    }
}

 
           

