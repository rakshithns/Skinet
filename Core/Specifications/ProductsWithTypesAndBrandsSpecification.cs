using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductParamSpec productParamSpec):base(x=> (string.IsNullOrEmpty(productParamSpec.Search)||x.Name.Contains(productParamSpec.Search))&&(!productParamSpec.BrandId.HasValue || x.ProductBrandId == productParamSpec.BrandId) && (!productParamSpec.TypeId.HasValue || x.ProductTypeId==productParamSpec.TypeId))
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
            AddOrderBy(x=>x.Name);
            ApplyPaging(productParamSpec.PageSize, (productParamSpec.PageSize*(productParamSpec.PageIndex-1)));
            if(!string.IsNullOrEmpty(productParamSpec.Sort))
            {
                switch(productParamSpec.Sort)
                {
                    case "priceAsc":AddOrderBy(x=>x.Price);
                                    break;
                    case "priceDesc":AddOrderByDescending(x=>x.Price);
                                    break; 
                    default:AddOrderBy(x=>x.Name);
                            break;        
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(long Id) : base(x=>x.Id==Id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}