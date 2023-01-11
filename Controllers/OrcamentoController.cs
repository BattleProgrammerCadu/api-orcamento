
using api.DTOs;
using api.Models;
using api.Repositorios.Interfaces;
using api.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[Route("orcamentos")]
public class OrcamentoController: ControllerBase
{
    private IServico<Orcamento> _servico;
    public OrcamentoController(IServico<Orcamento> servico) 
    {
        _servico = servico;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var orcamentos = await _servico.TodosAsync();
        return StatusCode(200, orcamentos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
       var orcamento = (await _servico.TodosAsync()).Find(o => o.Id == id);
       if(orcamento is null)
       {
         return StatusCode(404, new{
                Mensagem = "Essa orcamento não existe"
            });
       }
       return StatusCode(200, orcamento);
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] Orcamento orcamento)
    {
        await _servico.IncluirAsync(orcamento);
        return StatusCode(201, orcamento);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Apagar([FromRoute] int id)
    {
        var orcamento = (await _servico.TodosAsync()).Find(o => o.Id == id);
        if( orcamento is null)
        {
            return StatusCode(404, new{
                Mensagem = "Essa orcamento não existe"
            });
        }

        await _servico.ApagarAsync(orcamento);
        return StatusCode(204);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Orcamento orcamento)
    {
        if(orcamento.Id != id){
             return StatusCode(404, new{
                Mensagem = "Esse orcamento não existe"
            });
        }
        var orcamentoDb = await _servico.AtualizarAsync(orcamento);
        return StatusCode(200, orcamentoDb);
    }
}