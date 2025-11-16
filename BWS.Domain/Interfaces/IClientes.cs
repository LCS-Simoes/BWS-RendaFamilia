using BWS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWS.Domain.Interfaces
{
    public interface IClientes
    {
        Task<List<Clientes>> TodosUsuarios();
        Task<Clientes> BuscarId(int id);
        Task<Clientes> Cadastrar(Clientes clientes);
        Task<Clientes> Atualizar(Clientes clientes, int id);
        Task<bool> Deletar(int id);
    }
}
