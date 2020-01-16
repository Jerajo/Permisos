using System.ComponentModel.DataAnnotations;

namespace Permisos.Común.Dominio.Models
{
	public abstract class TipoPermisoParaManipulaciónDto
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Descripción { get; set; }
	}
}
