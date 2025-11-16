using BWS.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BWS.FrontEnd.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteServiceApi _service;

        public ClientesController(ClienteServiceApi service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string nome)
        {
            try
            {
                var clientes = await _service.ListarAsync();

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    clientes = clientes
                        .Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Exibe mensagens de TempData, se houver
                if (TempData["Sucesso"] != null)
                    ViewBag.Sucesso = TempData["Sucesso"];
                if (TempData["Erro"] != null)
                    ViewBag.Erro = TempData["Erro"];

                return View(clientes);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro ao listar clientes: " + ex.Message;
                return View(new List<ClienteViewModel>());
            }
        }

        public async Task<IActionResult> FormCadastro(CadastroViewModel cadastro)
        {
            if (!ModelState.IsValid)
                return View(cadastro);

            try
            {
                var sucesso = await _service.CriarAsync(cadastro);
                if (!sucesso)
                {
                    TempData["Erro"] = "Erro ao criar cliente.";
                    return RedirectToAction("Index");
                }

                TempData["Sucesso"] = "Cliente criado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao criar cliente: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> EditarCliente(int id)
        {
            try
            {
                var cliente = await _service.BuscarPorIdAsync(id);
                if (cliente == null)
                {
                    TempData["Erro"] = "Cliente não encontrado.";
                    return RedirectToAction("Index");
                }

                return View("EditarCliente", cliente);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao buscar cliente: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditarCliente(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var sucesso = await _service.AtualizarAsync(model.Id, new ClienteViewModel
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataNascimento = model.DataNascimento,
                    RendaFamilia = model.RendaFamilia
                });

                if (!sucesso)
                {
                    TempData["Erro"] = "Erro ao atualizar cliente.";
                    return View(model);
                }

                TempData["Sucesso"] = "Cliente atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar cliente: " + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExcluirCliente(int id)
        {
            var cliente = await _service.BuscarPorIdAsync(id);
            if (cliente == null)
            {
                TempData["Erro"] = "Cliente não encontrado.";
                return RedirectToAction("Index");
            }

            return View(cliente); 
        }

        [HttpPost, ActionName("ConfirmarExcluir")]
        public async Task<IActionResult> ExcluirClientePost(int id)
        {
            try
            {
                bool apagado = await _service.ExcluirAsync(id);
                if (apagado)
                    TempData["Sucesso"] = "Cliente excluído com sucesso!";
                else
                    TempData["Erro"] = "Não foi possível excluir o cliente.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao excluir cliente: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


        public async Task <IActionResult> RelatoriosCliente(string periodo = "Hoje")
        {
            var clientes = await _service.ListarAsync();

            DateTime hoje = DateTime.Today;
            DateTime inicioPeriodo;

            switch (periodo)
            {
                case "Semana":
                    inicioPeriodo = hoje.AddDays(-(int)hoje.DayOfWeek);
                    break;
                case "Mes":
                    inicioPeriodo = new DateTime(hoje.Year, hoje.Month, 1);
                    break;
                default:
                    inicioPeriodo = hoje;
                    break;
            }

            var clientesFiltrados = clientes
                .Where(c => c.DataCadastro >= inicioPeriodo)
                .ToList();

            decimal mediaRenda = clientesFiltrados.Any() ? clientesFiltrados.Average(c => c.RendaFamilia ?? 0) : 0;

            var relatorios = new RelatorioViewModel
            {
                FiltroPeriodo = periodo,
                ClientesAcimaMedia = clientesFiltrados.Count(c => c.Idade >= 18 && (c.RendaFamilia ?? 0 ) > mediaRenda),
                ClientesA = clientesFiltrados.Count(c => c.Classe == "A"),
                ClientesB = clientesFiltrados.Count(c => c.Classe == "B"),
                ClientesC = clientesFiltrados.Count(c => c.Classe == "C")
            };

            return View(relatorios);
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
