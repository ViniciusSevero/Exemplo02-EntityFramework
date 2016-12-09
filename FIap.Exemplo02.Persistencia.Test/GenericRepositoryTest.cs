using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fiap.Exemplo02.MVC.Web.Repositories;
using Fiap.Exemplo02.Dominio.Models;
using System.Data.Entity.Infrastructure;

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
            int r = _ctx.SaveChanges();

            Assert.AreEqual(1, r);//qtde de linhas afetadas
            Assert.AreNotEqual(professor.Id, 0); // id gerado pelo banco
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void Cadastrar_Prof_Sem_Nome()
        {

            //instancaiar um Professor
            var professor = new Professor()
            {
                Salario = 5000

            };
            //Cadastrar o Professor
            _rep.Cadastrar(professor);
            _ctx.SaveChanges();
        }
    }
}
