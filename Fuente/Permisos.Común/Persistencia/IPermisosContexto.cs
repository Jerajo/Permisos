using Permisos.Común.Persistencia.Entidades;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.Común.Persistencia
{
	public interface IPermisosContexto
	{
		IQueryable<T> Data<T>() where T : class, IEntidad;
		void AddEntity<T>(T entity) where T : class, IEntidad;
		void RemoveEntity<T>(T entity) where T : class, IEntidad;
		int SaveChanges(bool acceptAllChangesOnSuccess);
		int SaveChanges();
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
