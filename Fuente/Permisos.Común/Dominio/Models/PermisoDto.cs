using System;
using System.ComponentModel.DataAnnotations;

namespace Permisos.Común.Dominio.Models
{
	public class PermisoDto
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = nameof(Resources.NombreEmpleado),
			ResourceType = typeof(Resources))]
		[StringLength(100)]
		public string NombreEmpleado { get; set; }

		[Required]
		[Display(Name = nameof(Resources.ApellidosEmpleado),
			ResourceType = typeof(Resources))]
		[StringLength(100)]
		public string ApellidosEmpleado { get; set; }

		[Required]
		[Display(Name = nameof(Resources.TipoPermiso),
			ResourceType = typeof(Resources))]
		public string TipoPermiso { get; set; }

		[Required]
		[Display(Name = nameof(Resources.FechaPermiso),
			ResourceType = typeof(Resources))]
		public DateTime FechaPermiso { get; set; }
	}
}
