using Permisos.Común.Persistencia.Entidades;
using System;
using System.Collections.Generic;

namespace Permisos.Común.Persistencia.Servicios
{
	public interface IRepositorio<T> where T : class, IEntidad
	{
		IEnumerable<T> GetAll();
		IEnumerable<T> Where(Func<T, bool> predicate);
		T Find(int id);
		void AddRange(IEnumerable<T> models);
		void Add(T model);
		void Delete(T model);
	}
}