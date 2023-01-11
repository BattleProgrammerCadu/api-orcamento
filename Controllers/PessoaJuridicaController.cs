
using api.DTOs;
using api.Models;
using api.Repositorios.Interfaces;
using api.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("pessoas-juridicas")]
public class PessoaJuridicaController: ControllerBase
{
    private IServico<PessoaJuridica> _servico;
    public PessoaJuridicaController(IServico<PessoaJuridica> servico) 
    {
        _servico = servico;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var pessoas = await _servico.TodosAsync();
        return StatusCode(200, pessoas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
       var pessoa = (await _servico.TodosAsync()).Find(pf => pf.Id == id);
       if(pessoa is null)
       {
         return StatusCode(404, new{
                Mensagem = "Essa pessoa não existe"
            });
       }
       return StatusCode(200, pessoa);
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] PessoaJuridica pessoaJuridica)
    {
        
        await _servico.IncluirAsync(pessoaJuridica);
        return StatusCode(201, pessoaJuridica);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Apagar([FromRoute] int id)
    {
        var pessoa = (await _servico.TodosAsync()).Find(pj => pj.Id == id);
        if( pessoa is null)
        {
            return StatusCode(404, new{
                Mensagem = "Essa pessoa não existe"
            });
        }

        await _servico.ApagarAsync(pessoa);
        return StatusCode(204);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] PessoaJuridica pessoaJuridica)
    {
        
        if(pessoaJuridica.Id != id){
             return StatusCode(404, new{
                Mensagem = "Essa pessoa não existe"
            });
        }
        
        var pessoaDb = await _servico.AtualizarAsync(pessoaJuridica);
        return StatusCode(200, pessoaDb);
    }
}