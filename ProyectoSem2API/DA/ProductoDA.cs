using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ProductoDA : IProductoDA

    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }



        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var resultQuery = await
                                _sqlConnection.ExecuteScalarAsync<Guid>(query, new
                                {
                                    Id = Guid.NewGuid(),
                                    IdSubCategoria = producto.IdSubCategoria,
                                    Nombre = producto.Nombre,
                                    Descripcion = producto.Descripcion,
                                    Precio = producto.Precio,
                                    Stock = producto.Stock,
                                    CodigoBarras = producto.CodigoBarras
                                });

            return resultQuery;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {

            await verificarProductoExiste(Id);
            string query = @"EditarProducto";
            var resultQuery = await
                                _sqlConnection.ExecuteScalarAsync<Guid>(query, new
                                {
                                    Id = Id,
                                    IdSubCategoria = producto.IdSubCategoria,
                                    Nombre = producto.Nombre,
                                    Descripcion = producto.Descripcion,
                                    Precio = producto.Precio,
                                    Stock = producto.Stock,
                                    CodigoBarras = producto.CodigoBarras
                                });

            return resultQuery;

        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProductoExiste(Id);
            string query = @"EliminarProducto";
            var resultQuery = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new { Id = Id });

            return resultQuery;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultQuery = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultQuery;
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerVehiculo";
            var resultQuery = await _sqlConnection.QueryAsync<ProductoDetalle>(query, new { Id });
            return resultQuery.FirstOrDefault();
        }

        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("No se encontro el Producto");

        }
    }
}
