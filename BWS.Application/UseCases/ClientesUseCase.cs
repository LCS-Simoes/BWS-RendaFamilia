using BWS.Application.DTOs;
using BWS.Application.Services;
using BWS.Domain.Entidades;
using BWS.Domain.Interfaces;
using BWS.Domain.Validations;


namespace BWS.Application.UseCases
{
    public class ClientesUseCase
    {
        private readonly IClientes _clientesRepositiro;

        public ClientesUseCase(IClientes clientesRepositorio)
        {
            _clientesRepositiro = clientesRepositorio;
        }

        // VERIFICAR PROBLEMAS HOJE MESMO 

        public async Task<List<ClienteResponse>> Handle()
        {
            var clientes = await _clientesRepositiro.TodosUsuarios();

            return clientes.Select(s => new ClienteResponse
            {
                Id = s.Id,
                Nome = s.Nome,
                Cpf = s.Cpf,
                DataNascimento = s.DataNascimento,
                DataCadastro = s.DataCadastro,
                RendaFamilia = s.RendaFamilia,
                Classe = CalcularClasse(s.RendaFamilia)
            }).ToList();
        }

        public async Task<ClienteResponse> BuscarID(int id)
        {
            var cliente = await _clientesRepositiro.BuscarId(id);
            if (cliente == null) { throw new Exception($"Usuário {id} não foi encontrado no banco de dados"); }

            return new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                DataNascimento = cliente.DataNascimento,
                DataCadastro = cliente.DataCadastro,
                RendaFamilia = cliente.RendaFamilia
            };
        }

        public async Task<ClienteResponse> ExecuteAsync(CadastrarRequest request)
        {
            if (!ValidacaoCpf.ValidarCpf(request.Cpf))
            {
                throw new ArgumentException("O CPF fornecido não é válido.");
            }

            var cliente = new Clientes
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                DataNascimento = request.DataNascimento,
                RendaFamilia = request.RendaFamilia
            };

            var clienteCriado = await _clientesRepositiro.Cadastrar(cliente);

            return new ClienteResponse
            {
                Id = clienteCriado.Id,
                Nome = clienteCriado.Nome,
                Cpf = clienteCriado.Cpf,
                DataNascimento = clienteCriado.DataNascimento,
                DataCadastro = clienteCriado.DataCadastro,
                RendaFamilia = clienteCriado.RendaFamilia,
                Classe = CalcularClasse(clienteCriado.RendaFamilia)
            };
        }

        public async Task<ClienteResponse> AtualizarCliente(int id, AtualizarRequest request)
        {
            var cliente = await _clientesRepositiro.BuscarId(id);
            if (cliente == null) { throw new Exception($"Usuário {id} não foi encontrado no banco de dados"); }

            cliente.Nome = request.Nome;
            cliente.DataNascimento = request.DataNascimento;
            cliente.RendaFamilia = request.RendaFamilia;


            var atualizado = await _clientesRepositiro.Atualizar(cliente, id);

            return new ClienteResponse
            {
                Id = atualizado.Id,
                Nome = atualizado.Nome,
                DataCadastro = atualizado.DataCadastro,
                RendaFamilia = atualizado.RendaFamilia
            };
        }

        public async Task<bool> Deletar(int id)
        {
            return await _clientesRepositiro.Deletar(id);
        }

        private string CalcularClasse(decimal? renda)
        {
            if (renda is null)
                return "Não possui renda";

            if (renda <= 980)
                return "A";

            if (renda <= 2500)
                return "B";

            return "C";
        }
    }
}
