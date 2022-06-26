using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Aranda.CatalogProductCore.Repository.Dto
{
    public partial class ProductListResponse
    {
        public string CurrentSort { get; set; }
        public string SortName { get; set; }
        public string SortCategory { get; set; }
        public string CurrentFilter { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalRecords { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public virtual ICollection<ProductBaseResponse> Products { get; set; }
    }
}