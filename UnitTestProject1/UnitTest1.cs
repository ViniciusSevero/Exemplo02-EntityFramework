using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fiap.Exemplo02.MVC.Web.Controllers;
using Fiap.Exemplo02.MVC.Web.ViewModels;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Cadastrar_Post()
        {
            AlunoController controller = new AlunoController();
            var aluno = new AlunoViewModel()
            {
                Nome = "Vinicius",
                Bolsa = true,
                Cep = "08420680",
                DataNascimento = DateTime.Now,
                Desconto = 100,
                GrupoId = 1,
                ProfessoresId = new int[] { 1 }
                
            };

           
            ActionResult result = controller.Cadastrar(aluno);
            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        public void Cadastrar_Get()
        {
            AlunoController controller = new AlunoController();
            var result = (ViewResult)controller.Cadastrar("");
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}
