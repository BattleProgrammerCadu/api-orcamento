using api.Models;

namespace api.Repositorios.Interfaces;

public interface IServico<T>
{
    Task<List<T>> TodosAsync();
    Task IncluirAsync(T t);
    Task<T> AtualizarAsync(T t);
    Task ApagarAsync(T t);
}