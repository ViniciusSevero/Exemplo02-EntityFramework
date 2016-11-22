using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Exemplo02.MVC.Web.Repositories
{
    public class AlunoRepository : GenericRepository<Aluno>
    {
        public AlunoRepository(PortalContext context):base(context)
        {
        }


        public override void Atualizar(Aluno aluno)
        {
            Aluno a = BuscarPorId(aluno.Id);
            a.Bolsa = aluno.Bolsa;
            a.DataNascimento = aluno.DataNascimento;
            a.Desconto = aluno.Desconto;
            a.GrupoId = aluno.GrupoId;
            a.Id = aluno.Id;
            a.Nome = aluno.Nome;

            //limpa profesores
            a.Professor.Clear();

            a.Professor = aluno.Professor;

        }

        public override void Remover(int id)
        {
            Aluno aluno = _dbSet.Find(id);
            aluno.Professor.Clear();
            _dbSet.Remove(aluno);
        }
    }
}