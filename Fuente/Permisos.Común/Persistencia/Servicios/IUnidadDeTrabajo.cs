using Permisos.Común.Persistencia.Entidades;
using System.Threading.Tasks;

namespace Permisos.Común.Persistencia.Servicios
{
	public interface IUnidadDeTrabajo
	{
		IRepositorio<T> Repositorio<T>() where T : class, IEntidad;
		void Guardar();
		Task GuardarAsíncrono();
	}
}