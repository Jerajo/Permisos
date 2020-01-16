using Permisos.Común.Persistencia.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Permisos.SqlServer.Entidades
{
	[Table("TipoPermisos")]
	public class TipoPermiso : IEntidad
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Descripción { get; set; }
	}
}
