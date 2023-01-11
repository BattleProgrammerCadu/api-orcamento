using api.Models;
using api.Repositorios.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositorios;

public class PessoaFisicaRepositorio : IServico<PessoaFisica>
{
    public PessoaFisicaRepositorio()
    {
        conexao = Environment.GetEnvironmentVariable("DATABASE_URL_API");
        if(conexao is null) conexao = "Server=localhost;Database=Api;Uid=root;Pwd=root;";
    }

    private string? conexao = null;
    async Task IServico<PessoaFisica>.IncluirAsync(PessoaFisica pessoaFisica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"insert into pessoa_fisica(nome, cpf, telefone, data_criacao) values(@nome, @cpf, @telefone, @data_criacao)";
            var command = new MySqlCommand(query, conn);
            DateTime dataCriacao = DateTime.Now;
            command.Parameters.Add(new MySqlParameter("@nome", pessoaFisica.Nome));
            command.Parameters.Add(new MySqlParameter("@cpf", pessoaFisica.CPF));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaFisica.Telefone));
            command.Parameters.Add(new MySqlParameter("@data_criacao",  dataCriacao));

            await command.ExecuteNonQueryAsync();
            conn.Close();
        }
    }
    async Task<List<PessoaFisica>> IServico<PessoaFisica>.TodosAsync()
    {

        var listaPessoas = new List<PessoaFisica>();
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = "select * from pessoa_fisica";
            var command = new MySqlCommand(query, conn);
            var dr = await command.ExecuteReaderAsync();
            while(dr.Read())
            {
                listaPessoas.Add(new PessoaFisica{
                    Id = Convert.ToInt32(dr["id"]),
                    Nome = dr["nome"].ToString() ?? "",
                    Telefone = dr["telefone"].ToString() ?? "",
                    CPF = dr["cpf"].ToString() ?? "",
                    DataCriacao = Convert.ToDateTime(dr["data_criacao"])
                });
            }
            conn.Close();
        };
        return listaPessoas;
    }
    
    async Task IServico<PessoaFisica>.ApagarAsync(PessoaFisica pessoaFisica)
    {
       
       using(var conn = new MySqlConnection(conexao))
       {
            conn.Open();
            var query = $"delete from pessoa_fisica where id = @id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@id", pessoaFisica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
       }
    }

   async Task<PessoaFisica> IServico<PessoaFisica>.AtualizarAsync(PessoaFisica pessoaFisica)
    {
        using(var conn = new MySqlConnection(conexao))
        {
            conn.Open();
            var query = $"update pessoa_fisica set nome=@nome,telefone=@telefone,cpf=@cpf where id=@id;";
            var command = new MySqlCommand(query, conn);
            command.Parameters.Add(new MySqlParameter("@nome", pessoaFisica.Nome));
            command.Parameters.Add(new MySqlParameter("@cpf", pessoaFisica.CPF));
            command.Parameters.Add(new MySqlParameter("@telefone", pessoaFisica.Telefone));
            command.Parameters.Add(new MySqlParameter("@id", pessoaFisica.Id));
            await command.ExecuteNonQueryAsync();
            conn.Close();
        }

        return pessoaFisica;
    }
}

 
           

