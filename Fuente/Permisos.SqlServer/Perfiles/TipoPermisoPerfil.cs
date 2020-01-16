using AutoMapper;
using Permisos.Común.Dominio.Models;

namespace Permisos.SqlServer.Perfiles
{
	public class TipoPermisoPerfil : Profile
	{
		public TipoPermisoPerfil()
		{
			CreateMap<Entidades.TipoPermiso, TipoPermisoDto>();
			CreateMap<TipoPermisoDto, Entidades.TipoPermiso>();
			CreateMap<TipoPermisoParaCreaciónDto,
				Entidades.TipoPermiso>();
			CreateMap<TipoPermisoParaCreaciónDto, TipoPermisoDto>();
			CreateMap<TipoPermisoParaActualizaciónDto,
				Entidades.TipoPermiso>();
			CreateMap<TipoPermisoParaActualizaciónDto, TipoPermisoDto>();
			CreateMap<Entidades.TipoPermiso,
				TipoPermisoParaActualizaciónDto>();
			CreateMap<TipoPermisoDto, TipoPermisoParaActualizaciónDto>();
		}
	}
}
