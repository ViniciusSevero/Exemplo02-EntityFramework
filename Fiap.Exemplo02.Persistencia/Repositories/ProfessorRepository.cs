using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fiap.Exemplo02.Dominio.Models;

namespace Fiap.Exemplo02.MVC.Web.Repositories
{
    public class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository // ele pega a implementação da classe automaticamente
    {
        public ProfessorRepository(PortalContext context):base(context)
        {
        }

        public void Promocao(int id, float valor)
        {
            var profesor = BuscarPorId(id);
            profesor.Salario += profesor.Salario * valor;
        }
    }
}
