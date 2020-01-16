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
			IUnidadDeTrabajo unitOfWork,
			IMapper mapper)
		{
			Guard.Against.Null(logger, nameof(logger));
			Guard.Against.Null(unitOfWork, nameof(unitOfWork));
			Guard.Against.Null(mapper, nameof(mapper));

			Logger = logger;
			UnitOfWork = unitOfWork;
			Mapper = mapper;
		}

		#region Propiedades
		protected ILogger<PermisosController> Logger { get; }
		protected IUnidadDeTrabajo UnitOfWork { get; }
		protected IMapper Mapper { get; }
		#endregion

		#region "Métodos Auxiliares"
		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult ResourceNotFound()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		#endregion
	}
}
