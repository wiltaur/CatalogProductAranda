using Aranda.CatalogProductCore.Repository.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.CatalogProductCore.Business.Interface
{
    public interface ICatalogProductService
    {
        Task<bool> AddProduct(ProductBaseRequest product);
        Task<bool> UpdateProduct(ProductModifyRequest product);
        Task<bool> DeleteProduct(int id);
        Task<ProductListResponse> GetAllProducts(TableViewRequest tableView);
    }
}