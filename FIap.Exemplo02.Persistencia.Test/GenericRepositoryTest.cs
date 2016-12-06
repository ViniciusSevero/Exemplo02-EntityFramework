using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fiap.Exemplo02.MVC.Web.Repositories;
using Fiap.Exemplo02.Dominio.Models;

namespace FIap.Exemplo02.Persistencia.Test
{

    [TestClass]
    public class GenericRepositoryTest
    {
        private GenericRepository<Professor> _rep;
        private PortalContext _ctx;

        [TestInitialize]
        public void init()
        {
            _ctx = new PortalContext();
            _rep = new GenericRepository<Professor>(_ctx);
        }

        [TestMethod]
        public void Cadastrar_OK()
        {
            //instancaiar um Professor
            var professor = new Professor()
            {
                Nome = "Humberto",
                Salario = 5000

            };
            //Cadastrar o Professor
            _rep.Cadastrar(professor);
            _ctx.SaveChanges();
            //??? fazer em casa, verificar se foi cadastrado
        }
    }
}
