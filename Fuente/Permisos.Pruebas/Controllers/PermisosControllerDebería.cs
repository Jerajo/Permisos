using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Permisos.Controllers;
using System;
using Permisos.SqlServer.Servicios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using Permisos.SqlServer.Entidades;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Servicios;
using Microsoft.Extensions.Logging;
using Permisos.Común.Dominio.Models;

namespace TestAPI.Test
{
	[TestClass]
	public class PermisosControllerDebería
	{
		#region Attributes
		private PermisosController _sut;
		private IRepositorio<Permiso> _permisosRepositorio;

		private static IMapper _mapeador;
		private static UnidadDeTrabajo _unidadDeTrabajo;
		private static Mock<IPermisosContexto> _DbContextoFalso;
		private static Mock<ILogger<PermisosController>> _loggerFalso;
		#endregion

		#region Setup Tests
		[ClassInitialize]
		public static void InicializaDependenciasCostosas(
			TestContext testContext)
		{
			_DbContextoFalso = Ensamblado.DbContextoFalso;
			_mapeador = Ensamblado.Mapeador;
			_unidadDeTrabajo = Ensamblado.UnidadDeTrabajo;
			_loggerFalso = new
				Mock<ILogger<PermisosController>>(MockBehavior.Loose);
		}

		[TestInitialize]
		public void InitializeSut()
		{
			_permisosRepositorio = _unidadDeTrabajo.Repositorio<Permiso>();
			
			var objectValidator = new Mock<IObjectModelValidator>();

			objectValidator.Setup(o => o.Validate(
				It.IsAny<ActionContext>(),
				It.IsAny<ValidationStateDictionary>(),
				It.IsAny<string>(),
				It.IsAny<object>()));

			_sut = new PermisosController(_loggerFalso.Object,
				_unidadDeTrabajo,
				_mapeador);

			_sut.ObjectValidator = objectValidator.Object;
		}
		#endregion

		#region Constructor
		[TestMethod]
		public void Constructor_ProtegerContraLoggerNulo()
		{
			Assert.ThrowsException<ArgumentNullException>(() =>
				new PermisosController(null, _unidadDeTrabajo, _mapeador));
		}

		[TestMethod]
		public void Constructor_ProtegerContraUnidadDeTrabajoNulo()
		{
			Assert.ThrowsException<ArgumentNullException>(() =>
				new PermisosController(_loggerFalso.Object,
					null,
					_mapeador));
		}

		[TestMethod]
		public void Constructor_ProtegerContraMapeadorNulo()
		{
			Assert.ThrowsException<ArgumentNullException>(() =>
				new PermisosController(_loggerFalso.Object,
					_unidadDeTrabajo,
					null));
		}
		#endregion

		#region GET
		[TestMethod]
		public void MostrarLaPantallaPrincipal()
		{
			var resultado = _sut.Index() as ViewResult;

			Assert.IsNotNull(resultado);
		}

		[TestMethod]
		public void MostrarDetallesDelPermiso()
		{
			var permisoId = 1;

			var resultado = _sut.VerPermiso(permisoId) as ViewResult;

			Assert.IsNotNull(resultado);
		}

		[TestMethod]
		public void NoMostrarDetallesDelPermiso()
		{
			var permisoId = 0;

			var resultado = _sut.VerPermiso(permisoId) as ViewResult;

			Assert.IsNull(resultado);
		}
		#endregion

		#region POST
		[TestMethod]
		public void MostrarFormularioDePermisos()
		{
			var resultado = _sut.SolicitarPermiso() as ViewResult;

			Assert.IsNotNull(resultado);
		}

		[TestMethod]
		public void SolicitarUnPermiso()
		{
			var cuentaInicial = _permisosRepositorio.GetAll().Count();
			var permiso = _permisosRepositorio.GetAll().First();
			var permisoParaCreación = _mapeador
				.Map<PermisoParaCreaciónDto>(permiso);

			_sut.SolicitarPermiso(permisoParaCreación);
			var cuentaActual = _permisosRepositorio.GetAll().Count();

			Assert.AreNotEqual(cuentaInicial, cuentaActual);
		}

		[TestMethod]
		public void NoSolicitarUnPermiso_ModeloNoValido()
		{
			var listValidationResults = new List<ValidationResult>();
			var permisoParaCreación = new PermisoParaCreaciónDto();

			_sut.SolicitarPermiso(permisoParaCreación);

			var isValid = Validator.TryValidateObject(
				permisoParaCreación,
				new ValidationContext(permisoParaCreación),
				listValidationResults);

			Assert.IsFalse(isValid);
		}
		#endregion

		#region DELETE
		[TestMethod]
		public void EliminarPermiso()
		{
			var cuentaInicial = _permisosRepositorio.GetAll().Count();
			var permisoId = 3;

			_sut.EliminarPermiso(permisoId);
			var cuentaActual = _permisosRepositorio.GetAll().Count();

			Assert.AreNotEqual(cuentaInicial, cuentaActual);
		}

		[TestMethod]
		public void NoEliminarPermiso_IdNoEncontrado()
		{
			var cuentaInicial = _permisosRepositorio.GetAll().Count();
			var permisoId = 0;

			_sut.EliminarPermiso(permisoId);
			var cuentaActual = _permisosRepositorio.GetAll().Count();

			Assert.AreEqual(cuentaInicial, cuentaActual);
		}
		#endregion
	}
}
