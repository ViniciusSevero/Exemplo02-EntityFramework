using Fiap.Exemplo02.Dominio.Models;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class GrupoController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Grupo grupo)
        {
            _unit.GrupoRepository.Cadastrar(grupo);
            _unit.Save();
            TempData["msg"] = "Cadastro efetuado com sucesso";
            return View();
        }

        [HttpGet]
        public ActionResult Listar()
        {
            List<Grupo> grupos = (List<Grupo>) _unit.GrupoRepository.Listar();
            return View(grupos);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Grupo grupo = _unit.GrupoRepository.BuscarPorId(id);
            return View(grupo);
        }

        [HttpPost]
        public ActionResult Editar(Grupo g)
        {
            _unit.GrupoRepository.Atualizar(g);
            _unit.Save();
            TempData["msg"] = "Alterado com sucesso";
            return View(g);
        }

        public ActionResult Excluir(int idGrupo)
        {
            _unit.GrupoRepository.Remover(idGrupo);
            _unit.Save();
            return RedirectToAction("Listar");
        }

        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }
    }

    
   
}