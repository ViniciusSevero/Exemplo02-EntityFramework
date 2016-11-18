using Fiap.Exemplo02.MVC.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Exemplo02.MVC.Web.ViewModels
{
    public class AlunoViewModel
    {
        //Opções do select
        public virtual ICollection<Professor> Professores { get; set; }
        public virtual SelectList Grupos { get; set; }

        //Listagem de Alunos
        public ICollection<Aluno> Alunos { get; set; }

        //MSG
        public string Mensagem{get;set;}

        //Setters
        public int Id { get; set; }
        public string Nome { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public bool Bolsa { get; set; }
        public Nullable<double> Desconto { get; set; }
        [Display(Name = "Grupo")]
        public int GrupoId { get; set; }
        //não preciso da propriedade de navegação só o ID

        //array de IDs dos profs selecionados
        public int[]  ProfessoresId { get; set; }


    }
}