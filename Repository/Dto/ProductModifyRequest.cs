using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Aranda.CatalogProductCore.Repository.Dto
{
    public partial class ProductModifyRequest : ProductBaseRequest
    {
        public int Id { get; set; }
    }
}