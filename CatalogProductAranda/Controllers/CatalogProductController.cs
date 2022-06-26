using Aranda.CatalogProductCore.Business.Interface;
using Aranda.CatalogProductCore.Repository.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CatalogProductAranda.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogProductController : ControllerBase
    {
        private const string ERROR_PROCESAR_INFORMACION = "Error al procesar la información.";
        private readonly ICatalogProductService _catalogProductService;

        public CatalogProductController(ICatalogProductService catalogProductService)
        {
            _catalogProductService = catalogProductService;
        }

        #region PrincipalMethods

        /// <summary>
        /// Método para crear un producto en el catálogo.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddProduct([FromBody] ProductBaseRequest product)
        {
            try
            {
                var result = await _catalogProductService.AddProduct(product);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = true,
                    ReturnMessage = $"La información ha sido guardada exitosamente"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para modificar un producto en el catálogo.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModifyRequest product)
        {
            try
            {
                var result = await _catalogProductService.UpdateProduct(product);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = result,
                    ReturnMessage = result ? $"La información ha sido modificada exitosamente" : "No se encontró el producto en la Base de Datos."
                };
                return result ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para eliminar un producto en el catálogo.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _catalogProductService.DeleteProduct(id);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = result,
                    ReturnMessage = result ? $"Se eliminó el producto exitosamente" : "No se encontró el producto en la Base de Datos."
                };
                return result ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para listar todo el catálogo de productos con filtros de una DataTable.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ProductListResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<ProductListResponse>))]
        public async Task<IActionResult> GetAllProducts([FromQuery] TableViewRequest tableView)
        {
            try
            {
                var result = await _catalogProductService.GetAllProducts(tableView);

                var response = new ApiResponse<ProductListResponse>(result)
                {
                    IsSuccess = true,
                    ReturnMessage = $"La información ha sido consultada exitosamente"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        #endregion PrincipalMethods

        #region PrivateMethods

        /// <summary>
        /// Método privado para el manejo del mensaje a imprimir cuando se presenta excepciones.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [NonAction]
        private ApiResponse<string> BadRequestResult(Exception ex) {
            var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            return new ApiResponse<string>(message)
            {
                IsSuccess = false,
                ReturnMessage = ERROR_PROCESAR_INFORMACION
            };
        }

        #endregion PrivateMethods
    }
}