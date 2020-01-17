using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permisos.Común.Dominio.Models;
using Permisos.Común.Persistencia.Servicios;
using Permisos.SqlServer.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Permisos.Controllers
{
	public class PermisosController : BaseController
	{
		public PermisosController(ILogger<PermisosController> logger,
			IUnidadDeTrabajo unidadDeTrabajo,
			IMapper mapeador) :
			base(logger, unidadDeTrabajo, mapeador) {}

		#region GET
		[HttpGet]
		public IActionResult Index()
		{
			var permisos = UnidadDeTrabajo.Repositorio<Permiso>()
				.ObtenerColecciónCompleta()
				.ToList();

			var permisoModelos = Mapeador
				.Map<List<PermisoDto>>(permisos);

			return View(permisoModelos);
		}

		[HttpGet("{permisoId}")]
		public IActionResult VerPermiso(int permisoId)
		{
			var permisoEntidad = UnidadDeTrabajo.Repositorio<Permiso>()
				.Buscar(permisoId);

			if (permisoEntidad is null)
				return RedirectToAction(nameof(PáginaNoEncontrada));

			var permisoModelo = Mapeador.Map<PermisoDto>(permisoEntidad);

			return View(permisoModelo);
		}
		#endregion

		#region POST
		[HttpGet]
		public IActionResult SolicitarPermiso()
		{
			var tipoPermisosEntidad = UnidadDeTrabajo.Repositorio<TipoPermiso>()
				.ObtenerColecciónCompleta()
				.ToList();

			var tipoPermisosModelo = Mapeador
				.Map<List<TipoPermisoDto>>(tipoPermisosEntidad);

			var permisoModelo = new PermisoParaCreaciónDto
			{
				TiposPermisos = tipoPermisosModelo
			};

			return View(permisoModelo);
		}

		[HttpPost]
		public IActionResult SolicitarPermiso(
			[Bind("NombreEmpleado,ApellidosEmpleado,TipoPermisoId,FechaPermiso")]
			PermisoParaCreaciónDto permisoModelo)
		{
			if (!ModelState.IsValid)
			{
				var tipoPermisosEntidad = UnidadDeTrabajo.Repositorio<TipoPermiso>()
					.ObtenerColecciónCompleta()
					.ToList();

				var tipoPermisosModelo = Mapeador
					.Map<List<TipoPermisoDto>>(tipoPermisosEntidad);

				permisoModelo.TiposPermisos = tipoPermisosModelo;
				return View(permisoModelo);
			}

			var permisoEntidad = Mapeador.Map<Permiso>(permisoModelo);

			UnidadDeTrabajo.Repositorio<Permiso>().Añadir(permisoEntidad);
			UnidadDeTrabajo.Guardar();

			return RedirectToAction(nameof(VerPermiso),
				new { permisoId = permisoEntidad.Id });
		}
		#endregion

		#region DELETE
		public IActionResult EliminarPermiso(int permisoId)
		{
			var permisoEntidad = UnidadDeTrabajo.Repositorio<Permiso>()
				.Buscar(permisoId);

			if (permisoEntidad is null)
				return RedirectToAction(nameof(PáginaNoEncontrada));

			UnidadDeTrabajo.Repositorio<Permiso>().Eliminar(permisoEntidad);
			UnidadDeTrabajo.Guardar();

			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
