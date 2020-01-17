using Permisos.Común.Persistencia.Entidades;
using System;
using System.Collections.Generic;

namespace Permisos.Común.Persistencia.Servicios
{
	public interface IRepositorio<T> where T : class, IEntidad
	{
		IEnumerable<T> ObtenerColecciónCompleta();
		IEnumerable<T> Dónde(Func<T, bool> predicate);
		T Buscar(int id);
		void AñadirLista(IEnumerable<T> models);
		void Añadir(T model);
		void Eliminar(T model);
	}
}