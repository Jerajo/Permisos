using Permisos.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Permisos.Común.Dominio.Models
{
	public abstract class PermisoParaManipulaciónDto
	{
		public int Id { get; set; }

		[Required(ErrorMessageResourceName =
			nameof(Resources.NombreEmpleadoErrorNulo),
			ErrorMessageResourceType = typeof(Resources))]
		[Display(Name = nameof(Resources.NombreEmpleado),
			ResourceType = typeof(Resources))]
		[StringLength(100, ErrorMessageResourceName =
			nameof(Resources.NombreEmpleadoErrorMuyLargo),
			ErrorMessageResourceType = typeof(Resources))]
		public string NombreEmpleado { get; set; }

		[Required(ErrorMessageResourceName =
			nameof(Resources.ApellidosEmpleadoErrorNulo),
			ErrorMessageResourceType = typeof(Resources))]
		[Display(Name = nameof(Resources.ApellidosEmpleado),
			ResourceType = typeof(Resources))]
		[StringLength(100, ErrorMessageResourceName =
			nameof(Resources.ApellidosEmpleadoErrorMuyLargo),
			ErrorMessageResourceType = typeof(Resources))]
		public string ApellidosEmpleado { get; set; }

		[Required(ErrorMessageResourceName =
			nameof(Resources.TipoPermisoErrorNulo),
			ErrorMessageResourceType = typeof(Resources))]
		[Display(Name = nameof(Resources.TipoPermiso),
			ResourceType = typeof(Resources))]
		public int TipoPermisoId { get; set; }
		public List<TipoPermisoDto> TiposPermisos { get; set; }

		[Required(ErrorMessageResourceName =
			nameof(Resources.FechaPermisoErrorNulo),
			ErrorMessageResourceType = typeof(Resources))]
		[Display(Name = nameof(Resources.FechaPermiso),
			ResourceType = typeof(Resources))]
		[DataType(DataType.Date, ErrorMessageResourceName = 
			nameof(Resources.FechaPermisoErrorDatosInvalidos),
			ErrorMessageResourceType = typeof(Resources))]
		[DateTimeValidador(ErrorMessageResourceName = 
			nameof(Resources.FechaPermisoErrorFechaMuyAntigua),
			ErrorMessageResourceType = typeof(Resources))]
		public DateTime FechaPermiso { get; set; }
	}
}
