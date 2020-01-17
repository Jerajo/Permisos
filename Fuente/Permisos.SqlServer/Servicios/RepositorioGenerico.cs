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
		public T Buscar(int id) =>
			_context.Datos<T>()
				.FirstOrDefault(m => m.Id == id);

		public IEnumerable<T> ObtenerColecciónCompleta() =>
			_context.Datos<T>();

		public IEnumerable<T> Dónde(Func<T, bool> predicate) =>
			_context.Datos<T>().Where(predicate);
		#endregion

		#region POST
		public void AñadirLista(IEnumerable<T> models)
		{
			foreach (var model in models)
				Añadir(model);
		}

		public void Añadir(T model) =>
			_context.Añadir(model);
		#endregion

		#region DELETE
		public void Eliminar(T model) =>
			_context.Eliminar(model);
		#endregion
	}
}
