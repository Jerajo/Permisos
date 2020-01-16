using AutoMapper;
using Permisos.Común.Dominio.Models;

namespace Permisos.SqlServer.Perfiles
{
	public class PermisoPerfil : Profile
	{
		public PermisoPerfil()
		{
			CreateMap<Entidades.Permiso, PermisoDto>()
				.ForMember(mce => mce.TipoPermiso,
				mce => mce.MapFrom(p => p.TipoPermiso.Descripción));
			CreateMap<PermisoDto, Entidades.Permiso>();
			CreateMap<PermisoParaCreaciónDto, Entidades.Permiso>();
			CreateMap<Entidades.Permiso, PermisoParaCreaciónDto>();
			CreateMap<PermisoParaActualizaciónDto, Entidades.Permiso>();
			CreateMap<PermisoParaActualizaciónDto, PermisoDto>();
			CreateMap<Entidades.Permiso, PermisoParaActualizaciónDto>();
			CreateMap<PermisoDto, PermisoParaActualizaciónDto>();
		}
	}
}
