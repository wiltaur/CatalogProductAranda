using Aranda.CatalogProductCore.Business.Interface;
using Aranda.CatalogProductCore.Repository.Context;
using Aranda.CatalogProductCore.Repository.Dto;
using Aranda.CatalogProductCore.Repository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.CatalogProductCore.Business.Implement
{
    public class CatalogProductService : ICatalogProductService
    {
        private readonly CatalogProductDbContext _context;
        private readonly IConfiguration _config;

        public CatalogProductService(CatalogProductDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> AddProduct(ProductBaseRequest product)
        {
            _context.Products.Add(MapInfoProduct(product));
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateProduct(ProductModifyRequest product)
        {
            var existingProduct = (from prod in _context.Products
                                   where prod.Id == product.Id
                                   select prod).FirstOrDefault();

            if (existingProduct != null)
            {
                byte[] image = Convert.FromBase64String(product.Image);

                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Image = image;

                _context.Products.Update(existingProduct);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existingProduct = (from prod in _context.Products
                                   where prod.Id == id
                                   select prod).FirstOrDefault();

            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductListResponse> GetAllProducts(TableViewRequest tableView)
        {
            ProductListResponse productListResponse = new ProductListResponse();
            productListResponse.CurrentSort = tableView.SortOrder;
            productListResponse.SortName = String.IsNullOrEmpty(tableView.SortOrder) ? "name_desc" : "";
            productListResponse.SortCategory = tableView.SortOrder == "category_name" ? "category_name_desc" : "category_name";

            if (tableView.SearchString != null)
            {
                tableView.PageNumber = Convert.ToInt32(_config.GetSection("DefaultParams").GetSection("PageNumber").Value);
            }
            else
            {
                tableView.SearchString = tableView.CurrentFilter;
            }
            productListResponse.CurrentFilter = tableView.SearchString;

            int pageNumber = tableView.PageNumber ?? Convert.ToInt32(_config.GetSection("DefaultParams").GetSection("PageNumber").Value);

            int pageZise = tableView.PageSize ?? Convert.ToInt32(_config.GetSection("DefaultParams").GetSection("PageSize").Value);

            IQueryable<ProductBaseResponse> products;

            if (!string.IsNullOrEmpty(tableView.SearchString))
            {
                switch (tableView.SortOrder)
                {
                    case "name_desc":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                            .Where(p => p.Name.Contains(tableView.SearchString)
                                       || p.Description.Contains(tableView.SearchString)
                                       || p.CategoryName.Contains(tableView.SearchString))
                            .OrderByDescending(p => p.Name)
                            .Skip((pageNumber - 1) * pageZise)
                            .Take(pageZise);
                        break;
                    case "category_name_desc":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                            .Where(p => p.Name.Contains(tableView.SearchString)
                                       || p.Description.Contains(tableView.SearchString)
                                       || p.CategoryName.Contains(tableView.SearchString))
                            .OrderByDescending(p => p.CategoryName)
                            .Skip((pageNumber - 1) * pageZise)
                            .Take(pageZise);
                        break;
                    case "category_name":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                            .Where(p => p.Name.Contains(tableView.SearchString)
                                       || p.Description.Contains(tableView.SearchString)
                                       || p.CategoryName.Contains(tableView.SearchString))
                            .OrderBy(p => p.CategoryName)
                            .Skip((pageNumber - 1) * pageZise)
                            .Take(pageZise);
                        break;
                    default:
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                            .Where(p => p.Name.Contains(tableView.SearchString)
                                       || p.Description.Contains(tableView.SearchString)
                                       || p.CategoryName.Contains(tableView.SearchString))
                            .OrderBy(p => p.Name)
                            .Skip((pageNumber - 1) * pageZise)
                            .Take(pageZise);
                        break;
                }

                productListResponse.TotalRecords = await _context.Products.Include(c => c.Category).Where(p => p.Name.Contains(tableView.SearchString)
                                       || p.Description.Contains(tableView.SearchString)
                                       || p.Category.Name.Contains(tableView.SearchString)).CountAsync();
            }
            else
            {
                switch (tableView.SortOrder)
                {
                    case "name_desc":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                                    .OrderByDescending(p => p.Name)
                                    .Skip((pageNumber - 1) * pageZise)
                                    .Take(pageZise);
                        break;
                    case "category_name_desc":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                                    .OrderByDescending(p => p.CategoryName)
                                    .Skip((pageNumber - 1) * pageZise)
                                    .Take(pageZise);
                        break;
                    case "category_name":
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                                    .OrderBy(p => p.CategoryName)
                                    .Skip((pageNumber - 1) * pageZise)
                                    .Take(pageZise);
                        break;
                    default:
                        products = (from prod in _context.Products.Include(c => c.Category)
                                    select new ProductBaseResponse
                                    {
                                        Id = prod.Id,
                                        Name = prod.Name,
                                        Description = prod.Description,
                                        CategoryId = prod.CategoryId,
                                        CategoryName = prod.Category.Name,
                                        Image = Convert.ToBase64String(prod.Image)
                                    })
                                    .OrderBy(p => p.Name)
                                    .Skip((pageNumber - 1) * pageZise)
                                    .Take(pageZise);
                        break;
                }

                productListResponse.TotalRecords = await _context.Products.CountAsync();
            }

            var totalPages = (double)productListResponse.TotalRecords / pageZise;
            productListResponse.TotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            productListResponse.PageNumber = pageNumber;
            productListResponse.PageSize = pageZise;

            productListResponse.Products = await products.AsNoTracking().ToListAsync();
            return productListResponse;
        }

        private Product MapInfoProduct(ProductBaseRequest product)
        {
            byte[] image = Convert.FromBase64String(product.Image);

            Product productInfo = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Image = image
            };

            return productInfo;
        }
    }
}