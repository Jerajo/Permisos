using Permisos.Común.Persistencia.Entidades;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Común.Persistencia
{
	public interface IPermisosContexto
	{
		IQueryable<T> Datos<T>() where T : class, IEntidad;
		void Añadir<T>(T entity) where T : class, IEntidad;
		void Eliminar<T>(T entity) where T : class, IEntidad;
		void GuardarCambios();
		Task GuardarCambiosAsíncrono(CancellationToken
			cancellationToken = default);
	}
}
