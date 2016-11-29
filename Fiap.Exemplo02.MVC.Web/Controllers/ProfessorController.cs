using Fiap.Exemplo02.Dominio.Models;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class ProfessorController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();

        // GET: Professor
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Professor p)
        {
            _unit.ProfessorRepository.Cadastrar(p);
            _unit.Save();
            TempData["msg"] = "Cadastro efetuado com sucesso";
            return View();
        }

        public ActionResult Listar()
        {
            List<Professor> professores = (List<Professor>) _unit.ProfessorRepository.Listar();
            return View(professores);
        }
    }
}