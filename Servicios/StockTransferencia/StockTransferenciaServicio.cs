using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.StockTransferencia;
using IServicios.StockTransferencia.DTOs;
using Servicios.Base;

namespace Servicios.StockTransferencia
{
    public class StockTransferenciaServicio : IStockTransferenciaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public StockTransferenciaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        public void Eliminar(long id)
        {
            _unidadDeTrabajo.StockTrasnferenciaRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (StockTransferenciaServicioDto)dtoEntidad;

            var entidad = new Dominio.Entidades.StockTrasnferencia
            {
                ArticuloId = dto.ArticuloId,
                DepositoDonanteId = dto.DepositoDonanteId,
                DepositoReceptorId = dto.DepositoReceptorId,
                Cantidad = dto.Cantidad,
                UsuarioId = dto.UsuarioId,
                Fecha = dto.Fecha,
                EstaEliminado = false,

            };

            _unidadDeTrabajo.StockTrasnferenciaRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (StockTransferenciaServicioDto)dtoEntidad;
            var entidad = _unidadDeTrabajo.StockTrasnferenciaRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener la transferencia");

          
            entidad.ArticuloId = dto.ArticuloId;
            entidad.DepositoDonanteId = dto.DepositoDonanteId;
            entidad.DepositoReceptorId = dto.DepositoReceptorId;
            entidad.Cantidad = dto.Cantidad;
            entidad.Fecha = dto.Fecha;
            entidad.UsuarioId = dto.UsuarioId;
         
            _unidadDeTrabajo.StockTrasnferenciaRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.StockTrasnferenciaRepositorio.Obtener(id, "Articulo, Stock, Usuario");

            return new StockTransferenciaServicioDto
            {
                Id = entidad.Id,
                ArticuloId = entidad.ArticuloId,
                Articulo = entidad.Articulo.Descripcion,
                DepositoDonanteId = entidad.DepositoDonanteId,
                DepositoDonante = entidad.DepositoDonante.Deposito.Descripcion,
                DepositoReceptorId = entidad.DepositoReceptorId,
                DepositoReceptor = entidad.DepositoReceptor.Deposito.Descripcion,
                Cantidad = entidad.Cantidad,
                UsuarioId = entidad.UsuarioId,
                UsuarioStr = entidad.Usuario.Nombre,
                Fecha = entidad.Fecha,
                Eliminado = entidad.EstaEliminado,
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            Expression<Func<Dominio.Entidades.StockTrasnferencia, bool>> filtro = x =>
             x.Usuario.Nombre.Contains(cadenaBuscar);


            if (!mostrarTodos)
            {
                filtro = filtro.And(x => !x.EstaEliminado);
            }

            return _unidadDeTrabajo.StockTrasnferenciaRepositorio.Obtener(filtro, "Articulo, Usuario, DepositoDonante, DepositoReceptor")
                .Select(x => new StockTransferenciaServicioDto
                {
                    Id = x.Id,
                    ArticuloId = x.ArticuloId,
                    Articulo = x.Articulo.Descripcion,
                    DepositoDonanteId = x.DepositoDonanteId,
                    DepositoDonante = x.DepositoDonante.Deposito.Descripcion,
                    DepositoReceptorId = x.DepositoReceptorId,
                    DepositoReceptor = x.DepositoReceptor.Deposito.Descripcion,
                    Cantidad = x.Cantidad,
                    UsuarioId = x.UsuarioId,
                    UsuarioStr = x.Usuario.Nombre,
                })
                .OrderBy(x => x.Cantidad)
                .ToList();
        }
    }
}
