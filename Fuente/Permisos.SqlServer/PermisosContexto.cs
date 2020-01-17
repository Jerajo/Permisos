using Microsoft.EntityFrameworkCore;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Entidades;
using Permisos.SqlServer.Entidades;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Permisos.SqlServer
{
	public class PermisosContexto : DbContext, IPermisosContexto
	{
		public PermisosContexto(DbContextOptions<PermisosContexto> options)
			: base(options) {}

		#region Configuración
		public DbSet<Permiso> Permisos { get; }

		public DbSet<TipoPermiso> TipoPermisos { get; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Permiso>()
				.HasOne(p => p.TipoPermiso)
				.WithMany();

			base.OnModelCreating(modelBuilder);
		}
		#endregion

		#region "Métodos de interfaz"
		public IQueryable<T> Datos<T>() where T : class, IEntidad
			=> Set<T>();

		public void Añadir<T>(T entity) where T : class, IEntidad
			=> Set<T>().Add(entity);

		public void Eliminar<T>(T entity) where T : class, IEntidad
			=> Set<T>().Remove(entity);

		public void GuardarCambios() => SaveChanges();

		public Task GuardarCambiosAsíncrono(CancellationToken
			cancellationToken = default) =>
			SaveChangesAsync(cancellationToken);
		#endregion
	}
}
