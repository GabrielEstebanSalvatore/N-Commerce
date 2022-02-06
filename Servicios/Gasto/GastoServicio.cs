using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.Gasto;
using IServicios.Gasto.DTOs;
using Servicios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Servicios.Gasto
{
    public class GastoServicio : IGastoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public GastoServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
       
        public void Eliminar(long id)
        {
            _unidadDeTrabajo.GastoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (GastoDto)dtoEntidad;
            
            var entidad = new Dominio.Entidades.Gasto
            {
                Fecha = dto.Fecha,
                ConceptoGastoId = dto.ConceptoGastoId,
                Monto = dto.Importe,
                Descripcion = dto.Descripcion,
                EstaEliminado = false
            };
            _unidadDeTrabajo.GastoRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (GastoDto)dtoEntidad;
            var entidad = _unidadDeTrabajo.GastoRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener el Archivo");

            entidad.Fecha = dto.Fecha;
            entidad.ConceptoGastoId = dto.ConceptoGastoId;
            entidad.Monto = dto.Importe;
            entidad.Descripcion = dto.Descripcion;
            entidad.EstaEliminado = false;

            _unidadDeTrabajo.GastoRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.GastoRepositorio.Obtener(id, "");

            return new GastoDto
            {
                Id = entidad.Id,
                ConceptoGastoId = entidad.ConceptoGastoId,
                Fecha=entidad.Fecha,
                Importe = entidad.Monto,
                Descripcion = entidad.Descripcion,
                Eliminado = entidad.EstaEliminado
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Dominio.Entidades.Gasto, bool>> filtro = x =>
            x.Descripcion.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            return _unidadDeTrabajo.GastoRepositorio.Obtener(filtro, "ConceptoGasto")
                .Select(x => new GastoDto
                {
                    Id = x.Id,
                    ConceptoGastoId = x.ConceptoGastoId,
                    ConceptoGastoStr = x.ConceptoGasto.Descripcion,
                    Fecha = x.Fecha,
                    Importe = x.Monto,
                    Descripcion = x.Descripcion,
                    Eliminado = x.EstaEliminado
                })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
               ? _unidadDeTrabajo.GastoRepositorio.Obtener(x => !x.EstaEliminado
                                                                       && x.Id != entidadId.Value
                                                                       && x.Descripcion.Equals(datoVerificar,
                                                                           StringComparison.CurrentCultureIgnoreCase)).Any()
               : _unidadDeTrabajo.GastoRepositorio.Obtener(x => !x.EstaEliminado
                                                                       && x.Descripcion.Equals(datoVerificar,
                                                                           StringComparison.CurrentCultureIgnoreCase)).Any();
        }
    }
}
