using Ardalis.GuardClauses;
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
	[Route("[controller]/[action]")]
	public class PermisosController : BaseController
	{
		public PermisosController(ILogger<PermisosController> logger,
			IUnidadDeTrabajo unitOfWork,
			IMapper mapper) :
			base(logger, unitOfWork, mapper) {}

		#region GET
		[HttpGet]
		public IActionResult Index()
		{
			var permisos = UnitOfWork.Repositorio<Permiso>()
				.GetAll()
				.ToList();
			
			var permisoModelos = Mapper
				.Map<List<PermisoDto>>(permisos);

			return View(permisoModelos);
		}

		[HttpGet("{permisoId}")]
		public IActionResult VerPermiso(int permisoId)
		{
			var permisoEntidad = UnitOfWork.Repositorio<Permiso>()
				.Find(permisoId);

			if (permisoEntidad is null)
				return RedirectToAction(nameof(ResourceNotFound));

			var permisoModelo = Mapper.Map<PermisoDto>(permisoEntidad);

			return View(permisoModelo);
		}
		#endregion

		#region POST
		[HttpGet]
		public IActionResult SolicitarPermiso()
		{
			var tipoPermisosEntidad = UnitOfWork.Repositorio<TipoPermiso>()
				.GetAll()
				.ToList();

			var tipoPermisosModelo = Mapper
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
				var tipoPermisosEntidad = UnitOfWork.Repositorio<TipoPermiso>()
					.GetAll()
					.ToList();

				var tipoPermisosModelo = Mapper
					.Map<List<TipoPermisoDto>>(tipoPermisosEntidad);

				permisoModelo.TiposPermisos = tipoPermisosModelo;
				return View(permisoModelo);
			}

			var permisoEntidad = Mapper.Map<Permiso>(permisoModelo);

			UnitOfWork.Repositorio<Permiso>().Add(permisoEntidad);
			UnitOfWork.Commit();

			return RedirectToAction(nameof(VerPermiso),
				new { permisoId = permisoEntidad.Id });
		}
		#endregion

		#region DELETE
		public IActionResult EliminarPermiso(int permisoId)
		{
			var permisoEntidad = UnitOfWork.Repositorio<Permiso>()
				.Find(permisoId);

			if (permisoEntidad is null)
				return RedirectToAction(nameof(ResourceNotFound));

			UnitOfWork.Repositorio<Permiso>().Delete(permisoEntidad);
			UnitOfWork.Commit();

			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
