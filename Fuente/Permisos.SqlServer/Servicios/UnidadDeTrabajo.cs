using Ardalis.GuardClauses;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Entidades;
using Permisos.Común.Persistencia.Servicios;
using Permisos.SqlServer.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Permisos.SqlServer.Servicios
{
	public class UnidadDeTrabajo : IUnidadDeTrabajo
	{
		#region Atributos
		private readonly IPermisosContexto _context;
		private RepositorioGenerico<Permiso> _permisosRepositorio;
		private RepositorioGenerico<TipoPermiso> _tipoPermisosRepositorio;
		#endregion

		public UnidadDeTrabajo(IPermisosContexto context)
		{
			Guard.Against.Null(context, nameof(context));
			_context = context;
		}

		#region Propiedades
		public RepositorioGenerico<Permiso> PermisosRepositorio =>
			_permisosRepositorio ?? (_permisosRepositorio = 
			new RepositorioGenerico<Permiso>(_context));

		public RepositorioGenerico<TipoPermiso> TipoPermisosRepositorio =>
			_tipoPermisosRepositorio ?? (_tipoPermisosRepositorio = 
			new RepositorioGenerico<TipoPermiso>(_context));
		#endregion

		public IRepositorio<T> Repositorio<T>() where T : class, IEntidad
		{
			if (typeof(T) == typeof(Permiso))
				return PermisosRepositorio as RepositorioGenerico<T>;
			else if (typeof(T) == typeof(TipoPermiso))
				return TipoPermisosRepositorio as RepositorioGenerico<T>;

			throw new InvalidOperationException(
				$"Repositorio: {typeof(T).Name} no registrado.");
		}

		public void Commit()
		{
			AuditChanges();
			_context.SaveChanges();
		}

		public Task CommitAsync()
		{
			AuditChanges();
			return _context.SaveChangesAsync();
		}

		public void AuditChanges()
		{
			//TODO: implement audit changes
		}
	}
}
