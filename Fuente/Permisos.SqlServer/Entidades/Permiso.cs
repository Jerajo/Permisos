using Permisos.Común.Persistencia.Entidades;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Permisos.SqlServer.Entidades
{
	[Table("Permisos")]
	public class Permiso : IEntidad
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string NombreEmpleado { get; set; }

		[Required]
		[StringLength(100)]
		public string ApellidosEmpleado { get; set; }

		[Required]
		[Column("TipoPermiso")]
		[ForeignKey(nameof(TipoPermiso))]
		public int TipoPermisoId { get; set; }

		public virtual TipoPermiso TipoPermiso { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime FechaPermiso { get; set; }
	}
}
