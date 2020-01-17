using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Permisos.Común.Persistencia;
using Permisos.SqlServer.Entidades;
using Permisos.SqlServer.Perfiles;
using Permisos.SqlServer.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Permisos.Pruebas
{
	[TestClass]
	public class Ensamblado
	{
		#region Atributos
		private static readonly object _loocker = new { };
		private static ICollection<Permiso> _permisosFalsos;
		private static ICollection<TipoPermiso> _tipoPermisosFalsos;
		#endregion

		#region Propiedades
		public static IMapper Mapeador { get; private set; }
		public static UnidadDeTrabajo UnidadDeTrabajo { get; private set; }
		public static Mock<IPermisosContexto> DbContextoFalso
		{
			get;
			private set;
		}
		private static ICollection<Permiso> PermisosFalsos
		{
			get
			{
				lock (_loocker)
				{
					return _permisosFalsos ??
								 (_permisosFalsos = GetPermisosFalsos());
				}
			}
		}
		private static ICollection<TipoPermiso> TipoPermisosFalsos
		{
			get
			{
				lock (_loocker)
				{
					return _tipoPermisosFalsos ??
							 (_tipoPermisosFalsos = GetTipoPermisosFalsos());
				}
			}
		}
		#endregion

		#region "Inicializa el ensamblado"
		[AssemblyInitialize]
		public static void
			IntilializeAssemblyDependencies(TestContext testContext)
		{
			Mapeador = new Mapper(
				configurationProvider: new MapperConfiguration(mce =>
				{
					mce.AddProfile<PermisoPerfil>();
					mce.AddProfile<TipoPermisoPerfil>();
				}));

			DbContextoFalso = new Mock<IPermisosContexto>(MockBehavior.Loose);

			DbContextoFalso.Setup(config => config.Datos<Permiso>())
				.Returns(() => PermisosFalsos.AsQueryable());

			DbContextoFalso.Setup(config => config.Datos<TipoPermiso>())
				.Returns(() => TipoPermisosFalsos.AsQueryable());

			DbContextoFalso.Setup(config =>
					config.Añadir(It.IsAny<Permiso>()))
				.Callback<Permiso>(model =>
					PermisosFalsos.Add(model));

			DbContextoFalso.Setup(config =>
					config.Eliminar(It.IsAny<Permiso>()))
				.Callback<Permiso>(model =>
					PermisosFalsos.Remove(model));

			UnidadDeTrabajo = new UnidadDeTrabajo(DbContextoFalso.Object);
		}
		#endregion

		#region "Metodos auxiliares"
		private static ICollection<Permiso> GetPermisosFalsos() =>
			new List<Permiso>
			{
				new Permiso
				{
					Id = 1,
					NombreEmpleado = "Jesse",
					ApellidosEmpleado = "Jose",
					TipoPermisoId = (int)TipoPermisoEnumerador.Aniversario,
					TipoPermiso = TipoPermisosFalsos.First(m => m.Id ==
						(int)TipoPermisoEnumerador.Aniversario),
					FechaPermiso = DateTime.Now
				},
				new Permiso
				{
					Id = 2,
					NombreEmpleado = "Juan",
					ApellidosEmpleado = "Jose",
					TipoPermisoId = (int)TipoPermisoEnumerador.Enfermedad,
					TipoPermiso = TipoPermisosFalsos.First(m => m.Id ==
						(int)TipoPermisoEnumerador.Enfermedad),
					FechaPermiso = DateTime.Now
				},
				new Permiso
				{
					Id = 3,
					NombreEmpleado = "Jeffrey",
					ApellidosEmpleado = "Jose",
					TipoPermisoId = (int)TipoPermisoEnumerador.Diligencias,
					TipoPermiso = TipoPermisosFalsos.First(m => m.Id ==
						(int)TipoPermisoEnumerador.Diligencias),
					FechaPermiso = DateTime.Now
				}
			};
		private static ICollection<TipoPermiso> GetTipoPermisosFalsos() =>
			new List<TipoPermiso>
			{
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Aniversario,
					Descripción = nameof(TipoPermisoEnumerador.Aniversario)
				},
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Diligencias,
					Descripción = nameof(TipoPermisoEnumerador.Diligencias)
				},
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Enfermedad,
					Descripción = nameof(TipoPermisoEnumerador.Enfermedad)
				},
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Funeral,
					Descripción = nameof(TipoPermisoEnumerador.Funeral)
				},
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Parto,
					Descripción = nameof(TipoPermisoEnumerador.Parto)
				},
				new TipoPermiso
				{
					Id = (int)TipoPermisoEnumerador.Vacasiones,
					Descripción = nameof(TipoPermisoEnumerador.Vacasiones)
				}
			};
		#endregion
	}
}
