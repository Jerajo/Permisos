using Microsoft.EntityFrameworkCore;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Entidades;
using Permisos.SqlServer.Entidades;
using System.Linq;

namespace Permisos.SqlServer
{
	public class PermisosContexto : DbContext, IPermisosContexto
	{
		public PermisosContexto(DbContextOptions<PermisosContexto> options)
			: base(options) {}

		public DbSet<Permiso> Permisos { get; }
		public DbSet<TipoPermiso> TipoPermisos { get; }

		public IQueryable<T> Data<T>() where T : class, IEntidad
			=> Set<T>();
		public void AddEntity<T>(T entity) where T : class, IEntidad
			=> Set<T>().Add(entity);
		public void RemoveEntity<T>(T entity) where T : class, IEntidad
			=> Set<T>().Remove(entity);

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Permiso>()
				.HasOne(p => p.TipoPermiso)
				.WithMany();

			base.OnModelCreating(modelBuilder);
		}
	}
}
