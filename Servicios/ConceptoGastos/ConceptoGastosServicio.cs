using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.ConceptoGastos;
using IServicios.ConceptoGastos.DTOs;
using Servicios.Base;

namespace Servicios.ConceptoGastos
{
    public class ConceptoGastosServicio : IConceptoGastosServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ConceptoGastosServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public void Eliminar(long id)
        {
            _unidadDeTrabajo.ConceptoGastoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (ConceptoGastosDto)dtoEntidad;

            var entidad = new Dominio.Entidades.ConceptoGasto
            {
                Descripcion = dto.Descripcion,
                EstaEliminado = false
            };

            _unidadDeTrabajo.ConceptoGastoRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (ConceptoGastosDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.ConceptoGastoRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener el Archivo");


            entidad.Descripcion = dto.Descripcion;
        
            _unidadDeTrabajo.ConceptoGastoRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.ConceptoGastoRepositorio.Obtener(id);

            return new ConceptoGastosDto
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                Eliminado = entidad.EstaEliminado
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Dominio.Entidades.ConceptoGasto, bool>> filtro = x =>
             x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }
            return _unidadDeTrabajo.ConceptoGastoRepositorio.Obtener(filtro)
                .Select(x => new ConceptoGastosDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Eliminado = x.EstaEliminado
                })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
             ? _unidadDeTrabajo.MotivoBajaRepositorio.Obtener(x => !x.EstaEliminado
                                                                     && x.Id != entidadId.Value
                                                                     && x.Descripcion.Equals(datoVerificar,
                                                                         StringComparison.CurrentCultureIgnoreCase)).Any()
             : _unidadDeTrabajo.MotivoBajaRepositorio.Obtener(x => !x.EstaEliminado
                                                                     && x.Descripcion.Equals(datoVerificar,
                                                                         StringComparison.CurrentCultureIgnoreCase)).Any();
        }
    }
}
