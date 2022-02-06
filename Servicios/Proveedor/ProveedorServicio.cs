using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicio.Persona.DTOs;
using IServicios.Proveedor;
using IServicios.Proveedor.DTOs;
using Servicios.Base;

namespace Servicios.Proveedor
{
    public class ProveedorServicio : IProveedorServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ProveedorServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        public void Eliminar(long id)
        {
            _unidadDeTrabajo.ProveedorRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (ProveedorDto)dtoEntidad;

            var entidad = new Dominio.Entidades.Proveedor
            {
                RazonSocial = dto.RazonSocial,
                CUIT = dto.CUIT,
                
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Mail = dto.Mail,
                LocalidadId = dto.LocalidadId,
                CondicionIvaId = dto.CondicionIvaId,
                EstaEliminado = false
            };

            _unidadDeTrabajo.ProveedorRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (ProveedorDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.ProveedorRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener el Proveedor");

            entidad.RazonSocial = dto.RazonSocial;
            entidad.CUIT = dto.CUIT;
            entidad.Direccion = dto.Direccion;
            entidad.Telefono = dto.Telefono;
            entidad.Mail = dto.Mail;
            entidad.LocalidadId = dto.LocalidadId;
            entidad.CondicionIvaId = dto.CondicionIvaId;

            _unidadDeTrabajo.ProveedorRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.ProveedorRepositorio.Obtener(id, "CondicionIva, Localidad, Localidad.Departamento, Localidad.Departamento.Provincia");

            return new ProveedorDto
            {
                Id = entidad.Id,
                RazonSocial = entidad.RazonSocial,
                CUIT = entidad.CUIT,
                Direccion = entidad.Direccion,
                Telefono = entidad.Telefono,
                Mail = entidad.Mail,
                ProvinciaId = entidad.Localidad.Departamento.ProvinciaId,
                Provincia = entidad.Localidad.Departamento.Provincia.Descripcion,
                DepartamentoId = entidad.Localidad.DepartamentoId,
                Departamento = entidad.Localidad.Departamento.Descripcion,
                LocalidadId = entidad.LocalidadId,
                Localidad = entidad.Localidad.Descripcion,
                CondicionIvaId = entidad.CondicionIvaId,
                CondicionIva = entidad.CondicionIva.Descripcion,
                Eliminado = entidad.EstaEliminado
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Dominio.Entidades.Proveedor, bool>> filtro = x =>
               x.Mail.Contains(cadenaBuscar);

            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            return _unidadDeTrabajo.ProveedorRepositorio.Obtener(filtro, "CondicionIva,Localidad, Localidad.Departamento, Localidad.Departamento.Provincia")
               .Select(x => new ProveedorDto
               {
                   Id = x.Id,
                   RazonSocial = x.RazonSocial,
                   CUIT = x.CUIT,
                   Direccion = x.Direccion,
                   Telefono = x.Telefono,
                   Mail = x.Mail,
                   ProvinciaId = x.Localidad.Departamento.ProvinciaId,
                   Provincia = x.Localidad.Departamento.Provincia.Descripcion,
                   DepartamentoId = x.Localidad.DepartamentoId,
                   Departamento = x.Localidad.Departamento.Descripcion,
                   LocalidadId = x.LocalidadId,
                   Localidad = x.Localidad.Descripcion,
                   CondicionIvaId = x.CondicionIvaId,
                   CondicionIva = x.CondicionIva.Descripcion,
                   Eliminado = x.EstaEliminado
               })
               .OrderBy(x => x.CUIT)
               .ToList();
        }
    }
}
