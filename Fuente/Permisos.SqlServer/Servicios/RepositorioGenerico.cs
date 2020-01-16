using Ardalis.GuardClauses;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Entidades;
using Permisos.Común.Persistencia.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Permisos.SqlServer.Servicios
{
	public class RepositorioGenerico<T> : IRepositorio<T>
		where T : class, IEntidad
	{
		private readonly IPermisosContexto _context;
		public RepositorioGenerico(IPermisosContexto context)
		{
			Guard.Against.Null(context, nameof(context));
			_context = context;
		}

		#region GET
		public T Find(int id) =>
			_context.Data<T>()
				.FirstOrDefault(m => m.Id == id);

		public IEnumerable<T> GetAll() =>
			_context.Data<T>();

		public IEnumerable<T> Where(Func<T, bool> predicate) =>
			_context.Data<T>().Where(predicate);
		#endregion

		#region POST
		public void AddRange(IEnumerable<T> models)
		{
			foreach (var model in models)
				Add(model);
		}

		public void Add(T model) =>
			_context.AddEntity(model);
		#endregion

		#region DELETE
		public void Delete(T model) =>
			_context.RemoveEntity(model);
		#endregion
	}
}
