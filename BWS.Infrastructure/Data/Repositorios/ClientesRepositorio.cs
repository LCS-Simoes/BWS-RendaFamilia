using BWS.Domain.Entidades;
using BWS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BWS.Infrastructure.Data.Repositorios
{

    public class ClientesRepositorio : IClientes
    {
        private readonly bwsDbContext _dbContext;
        
        public ClientesRepositorio(bwsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Clientes>> TodosUsuarios()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<Clientes> BuscarId(int id)
        {
            return await _dbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Clientes> Cadastrar(Clientes clientes)
        {
            clientes.DataCadastro = DateTime.Now;
            await _dbContext.Clientes.AddAsync(clientes);
            await _dbContext.SaveChangesAsync();
            return clientes;
        }

        public async Task<Clientes> Atualizar(Clientes clientes, int id)
        {
            Clientes clienteID = await BuscarId(id);
            if (clienteID == null) { throw new Exception($"Usuário {id} não foi encontrado no banco de dados"); }

            clientes.Id = clienteID.Id;
            clientes.Nome = clienteID.Nome;
            clientes.Cpf = clienteID.Cpf;
            clientes.DataNascimento = clienteID.DataNascimento;
            clientes.RendaFamilia = clienteID.RendaFamilia;

            _dbContext.Clientes.Update(clienteID);
            await _dbContext.SaveChangesAsync();

            return clienteID;
            //Deixei a Data de Cadastro fora já que é FIXA
        }

        public async Task<bool> Deletar(int id)
        {
            Clientes clienteID = await BuscarId(id);
            if (clienteID == null) { throw new Exception($"Usuário {id} não foi encontrado no banco de dados"); }

            _dbContext.Clientes.Remove(clienteID);
            await _dbContext.SaveChangesAsync();

            return true;
        } 
    }
}
