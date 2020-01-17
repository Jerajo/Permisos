using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permisos.Común.Dominio.Models;
using Permisos.Común.Persistencia.Servicios;
using System.Diagnostics;

namespace Permisos.Controllers
{
	public abstract class BaseController : Controller
	{
		public BaseController(ILogger<PermisosController> logger,
			IUnidadDeTrabajo unidadDeTrabajo,
			IMapper mapeador)
		{
			Guard.Against.Null(logger, nameof(logger));
			Guard.Against.Null(unidadDeTrabajo, nameof(unidadDeTrabajo));
			Guard.Against.Null(mapeador, nameof(mapeador));

			Logger = logger;
			UnidadDeTrabajo = unidadDeTrabajo;
			Mapeador = mapeador;
		}

		#region Propiedades
		protected ILogger<PermisosController> Logger { get; }
		protected IUnidadDeTrabajo UnidadDeTrabajo { get; }
		protected IMapper Mapeador { get; }
		#endregion

		#region "Métodos Compartidos"
		public virtual IActionResult PolíticasDePrivacidad()
		{
			return View();
		}

		public virtual IActionResult PáginaNoEncontrada()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public virtual IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		#endregion
	}
}
