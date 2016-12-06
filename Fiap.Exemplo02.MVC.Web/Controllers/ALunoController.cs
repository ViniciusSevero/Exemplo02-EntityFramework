using Fiap.Exemplo02.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

                Mensagem = msg,

                DataNascimento = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Listar(String msg)
        {

            AlunoViewModel viewModel = new AlunoViewModel()
            {
                Alunos = _unit.AlunoRepository.Listar(),
                Grupos = carregarGrupos(),
                Mensagem = msg
            };


            //include -> busca o relacionamento (preenche o grupo que o aluno possui), faz o join
            //var lista = ctx.Aluno.Include("Grupo").ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Alterar(int id,string msg)
        {

            Aluno a = _unit.AlunoRepository.BuscarPorId(id);

            int[] ProfessoresId = new int[a.Professor.Count];
            for(int i = 0; i< a.Professor.Count; i++)
            {
                ProfessoresId[i] = a.Professor.ElementAt(i).Id;
            }

            AlunoViewModel viewModel = new AlunoViewModel()
            {
                Id = a.Id,
                Bolsa = a.Bolsa,
                DataNascimento = a.DataNascimento,
                Desconto = a.Desconto,
                GrupoId = a.GrupoId,
                Nome = a.Nome,
                ProfessoresId = ProfessoresId,
                Cep = a.Cep,

                Mensagem = msg,

                Grupos = carregarGrupos(),
                Professores = carregarProfessores()

            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Buscar(string nomeBusca, int? idBusca) //permite ser Null
        {
            List<Aluno> resultado = new List<Aluno>();


            resultado = _unit.AlunoRepository.BuscarPor(a => (a.Nome.Contains(nomeBusca) && a.GrupoId == idBusca)
                                                        || (idBusca == null && a.Nome.Contains(nomeBusca))).ToList();


         //   AlunoViewModel alunoViewModel = new AlunoViewModel()
         //   {
         //       Alunos = resultado,
         //       Grupos = carregarGrupos()
         //   };

            return PartialView("_tabela", resultado);
        }

        #endregion

        #region POST
        [HttpPost]
        public ActionResult Cadastrar(AlunoViewModel alunoViewModel)
        {
            if (ModelState.IsValid)
            {
                var aluno = new Aluno()
                {
                    Nome = alunoViewModel.Nome,
                    DataNascimento = alunoViewModel.DataNascimento,
                    Bolsa = alunoViewModel.Bolsa,
                    GrupoId = alunoViewModel.GrupoId,
                    Desconto = alunoViewModel.Desconto,
                    Cep = alunoViewModel.Cep
                };

                foreach (var id in alunoViewModel.ProfessoresId)
                {
                    Professor prof = _unit.ProfessorRepository.BuscarPorId(id);
                    aluno.Professor.Add(prof);
                }

                _unit.AlunoRepository.Cadastrar(aluno);
                try
                {
                    _unit.Save();
                }catch(Exception e)
                {
                    alunoViewModel.Mensagem = "Erro - " + e.Message;
                    alunoViewModel.Grupos = carregarGrupos();
                    alunoViewModel.Professores = carregarProfessores();
                    return View(alunoViewModel);
                }
                

                return RedirectToAction("Cadastrar", new { msg = "Aluno cadastrado" });
            }
            else
            {
                alunoViewModel.Grupos = carregarGrupos();
                alunoViewModel.Professores = carregarProfessores();
                return View(alunoViewModel);
            }
        }



        [HttpPost]
        public ActionResult Excluir(AlunoViewModel viewModel)
        {
            _unit.AlunoRepository.Remover(viewModel.Id);
            _unit.Save();
            return RedirectToAction("Listar", new { msg = "Aluno excluído com sucesso" });
        }



        [HttpPost]
        public ActionResult Alterar(AlunoViewModel alunoViewModel)
        {
            Aluno a = new Aluno()
            {
                Bolsa = alunoViewModel.Bolsa,
                DataNascimento = alunoViewModel.DataNascimento,
                Desconto = alunoViewModel.Desconto,
                GrupoId = alunoViewModel.GrupoId,
                Id = alunoViewModel.Id,
                Nome = alunoViewModel.Nome,
                Cep = alunoViewModel.Cep
            };

            //só para garantir
            a.Professor.Clear();

            //adiciona professores
            foreach (var id in alunoViewModel.ProfessoresId)
            {
                Professor prof = _unit.ProfessorRepository.BuscarPorId(id);
                a.Professor.Add(prof);
            }

            _unit.AlunoRepository.Atualizar(a);
            _unit.Save();

            return RedirectToAction("Alterar", new { id = a.Id, msg = "Cadastro alterado com suceso"});
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

        #region AJAX

        public ActionResult VerificarNome(string nome)
        {
            bool jaExiste = _unit.AlunoRepository.BuscarPor(c => c.Nome == nome).Any();
            return Json(new { existe = jaExiste }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}