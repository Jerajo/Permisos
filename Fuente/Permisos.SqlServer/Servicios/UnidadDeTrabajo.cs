using Ardalis.GuardClauses;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Entidades;
using Permisos.Común.Persistencia.Servicios;
using Permisos.SqlServer.Entidades;
using System;
using System.Threading.Tasks;

namespace Permisos.SqlServer.Servicios
{
	public class UnidadDeTrabajo : IUnidadDeTrabajo
	{
		#region Atributos
		private readonly object _loocker = new { };
		private readonly IPermisosContexto _contexto;
		private RepositorioGenerico<Permiso> _permisosRepositorio;
		private RepositorioGenerico<TipoPermiso> _tipoPermisosRepositorio;
		#endregion

		public UnidadDeTrabajo(IPermisosContexto contexto)
		{
			Guard.Against.Null(contexto, nameof(contexto));
			_contexto = contexto;
		}

		#region Propiedades
		public RepositorioGenerico<Permiso> PermisosRepositorio
		{
			get
			{
				lock (_loocker)
				{
					return _permisosRepositorio ?? (_permisosRepositorio =
						new RepositorioGenerico<Permiso>(_contexto));
				}
			}
		}

		public RepositorioGenerico<TipoPermiso> TipoPermisosRepositorio
		{
			get
			{
				lock (_loocker)
				{
					return _tipoPermisosRepositorio ?? (_tipoPermisosRepositorio
						= new RepositorioGenerico<TipoPermiso>(_contexto));
				}
			}
		}
		#endregion

		#region "Métodos de interfaz"
		public IRepositorio<T> Repositorio<T>() where T : class, IEntidad
		{
			if (typeof(T) == typeof(Permiso))
				return PermisosRepositorio as RepositorioGenerico<T>;

			if (typeof(T) == typeof(TipoPermiso))
				return TipoPermisosRepositorio as RepositorioGenerico<T>;

			throw new InvalidOperationException(
				$"Repositorio: {typeof(T).Name} no registrado.");
		}

		public void Guardar()
		{
			AuditarCambios();
			_contexto.GuardarCambios();
		}

		public Task GuardarAsíncrono()
		{
			AuditarCambios();
			return _contexto.GuardarCambiosAsíncrono();
		}
		#endregion

		#region "Métodos de auxiliares"
		protected virtual void AuditarCambios()
		{
			//TODO: implement audit changes
		}
		#endregion
	}
}
