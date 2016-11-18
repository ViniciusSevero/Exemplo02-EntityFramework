using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fiap.Exemplo02.MVC.Web.Models;
using System.Data.Entity;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;
using Fiap.Exemplo02.MVC.Web.ViewModels;

namespace Fiap.Exemplo02.MVC.Web.Controllers
{
    public class AlunoController : Controller
    {
        //private PortalContext ctx = new PortalContext();
        private UnitOfWork _unit = new UnitOfWork();

        #region GET

        [HttpGet]
        public ActionResult Cadastrar(string msg)
        {
            AlunoViewModel viewModel = new AlunoViewModel()
            {
                Grupos = carregarGrupos(),

                Professores = carregarProfessores(),

                Mensagem = msg
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Listar()
        {

            AlunoViewModel viewModel = new AlunoViewModel()
            {
                Alunos = _unit.AlunoRepository.Listar(),
                Grupos = carregarGrupos()
            };


            //include -> busca o relacionamento (preenche o grupo que o aluno possui), faz o join
            //var lista = ctx.Aluno.Include("Grupo").ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Alterar(int id)
        {
            //SelectList
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");

            Aluno a = _unit.AlunoRepository.BuscarPorId(id);
            return View(a);
        }

        [HttpGet]
        public ActionResult Buscar(AlunoViewModel viewModel) //permite ser Null
        {
            List<Aluno> resultado = new List<Aluno>();

            if (viewModel.idBusca == null)
            {
                //busca o aluno no banco por parte do nome
                resultado = _unit.AlunoRepository.BuscarPor(a => a.Nome.Contains(viewModel.NomeBusca)).ToList();
            }
            else
            {
                //busca o aluno no banco por nome do grupo
                resultado = _unit.AlunoRepository.BuscarPor(a => a.Grupo.Id == viewModel.idBusca && a.Nome.Contains(viewModel.NomeBusca)).ToList();
            }

            AlunoViewModel alunoViewModel2 = new AlunoViewModel()
            {
                Alunos = resultado,
                Grupos = carregarGrupos()
            };

            //passo direto para a view de listar e não para a action
            return View(alunoViewModel2);
        }

        #endregion

        #region POST
        [HttpPost]
        public ActionResult Cadastrar(AlunoViewModel alunoViewModel)
        {
            var aluno = new Aluno()
            {
                Nome = alunoViewModel.Nome,
                DataNascimento = alunoViewModel.DataNascimento,
                Bolsa = alunoViewModel.Bolsa,
                GrupoId = alunoViewModel.GrupoId,
                Desconto = alunoViewModel.Desconto
            };

            foreach (var id in alunoViewModel.ProfessoresId)
            {
                Professor prof = _unit.ProfessorRepository.BuscarPorId(id);
                aluno.Professor.Add(prof);
            }

            _unit.AlunoRepository.Cadastrar(aluno);
            _unit.Save();

            return RedirectToAction("Cadastrar", new { msg = "Aluno cadastrado" });
        }



        [HttpPost]
        public ActionResult Excluir(int id)
        {
            _unit.AlunoRepository.Remover(id);
            _unit.Save();
            TempData["msg"] = "Aluno excluído com sucesso";
            return RedirectToAction("Listar");
        }



        [HttpPost]
        public ActionResult Alterar(Aluno a)
        {
            //SelectList
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            ViewBag.grupos = new SelectList(grupos, "Id", "Nome");

            _unit.AlunoRepository.Atualizar(a);
            _unit.Save();
            ViewBag.msg = "Alterado com sucesso";
            return View(a);
        }

        #endregion

        #region PRIVATE


        #endregion

        #region DISPOSE
        //sobrescrever o método do Dispose do COntroller
        protected override void Dispose(bool disposing)
        {
            _unit.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region UTILS

        private SelectList carregarGrupos()
        {
            List<Grupo> grupos = (List<Grupo>)_unit.GrupoRepository.Listar();
            return new SelectList(grupos, "Id", "Nome");
        }

        private List<Professor> carregarProfessores()
        {
            List<Professor> lista = (List<Professor>)_unit.ProfessorRepository.Listar();
            return lista;
        }

        #endregion

    }
}