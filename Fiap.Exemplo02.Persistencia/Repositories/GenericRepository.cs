using Fiap.Exemplo02.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Exemplo02.MVC.Web.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class //T = os 2 vão trabalhar com a mesma classe
    {
        protected PortalContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(PortalContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); //pega tipo do DbSet
        }

        public virtual void Atualizar(T t)
        {
            _context.Entry(t).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual ICollection<T> BuscarPor(System.Linq.Expressions.Expression<Func<T, bool>> filtro)
        {
            return _dbSet.Where(filtro).ToList();
        }

        public virtual T BuscarPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Cadastrar(T t)
        {
            //_context.Entry(t).State = System.Data.Entity.EntityState.Added; OU
            _dbSet.Add(t);
        }

        public virtual ICollection<T> Listar()
        {
            return _dbSet.ToList<T>();
        }

        public virtual void Remover(int id)
        {
            T t = _dbSet.Find(id);
            _dbSet.Remove(t);
        }
    }
}
