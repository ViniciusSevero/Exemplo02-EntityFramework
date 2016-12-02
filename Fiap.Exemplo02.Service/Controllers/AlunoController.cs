using Fiap.Exemplo02.Dominio.Models;
using Fiap.Exemplo02.MVC.Web.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Fiap.Exemplo02.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AlunoController : ApiController
    {
        private UnitOfWork _unit = new UnitOfWork();

        //GET api/aluno
        public ICollection<Aluno> Get()
        {
            return _unit.AlunoRepository.Listar();
        }

        //GET api/aluno/{id}
        public Aluno Get(int id)
        {
            return _unit.AlunoRepository.BuscarPorId(id);
        }

        public IHttpActionResult Post(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _unit.AlunoRepository.Cadastrar(aluno);
                _unit.Save();
                var uri = Url.Link("DefaultApi", new { id = aluno.Id });
                return Created<Aluno>(new Uri(uri), aluno);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        public IHttpActionResult Put(int id,Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Id = id;
                _unit.AlunoRepository.Atualizar(aluno);
                _unit.Save();
                return Ok(aluno);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        public void Delete(int id)
        {
            _unit.AlunoRepository.Remover(id);
            _unit.Save();

        }


    }
}
