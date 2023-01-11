
using api.DTOs;
using api.Models;
using api.Repositorios.Interfaces;
using api.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("pessoas-fisicas")]
public class PessoaFisicaController: ControllerBase
{
    private IServico<PessoaFisica> _servico;
    public PessoaFisicaController(IServico<PessoaFisica> servico) 
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
    public async Task<IActionResult> Novo([FromBody] PessoaFisicaDTO pessoaFisicaDTO)
    {
        var pessoa = BuilderServico<PessoaFisica>.Builder(pessoaFisicaDTO);
        await _servico.IncluirAsync(pessoa);
        return StatusCode(201, pessoa);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Apagar([FromRoute] int id)
    {
        var pessoa = (await _servico.TodosAsync()).Find(pf => pf.Id == id);
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
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] PessoaFisica pessoaFisica)
    {
        
        if(pessoaFisica.Id != id){
             return StatusCode(404, new{
                Mensagem = "Essa pessoa não existe"
            });
        }
        
        var pessoaDb = await _servico.AtualizarAsync(pessoaFisica);
        return StatusCode(200, pessoaDb);
    }
}