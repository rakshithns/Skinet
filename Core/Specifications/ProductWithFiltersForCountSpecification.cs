using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductParamSpec productParamSpec):base(x=> (string.IsNullOrEmpty(productParamSpec.Search)||x.Name.Contains(productParamSpec.Search))&&(!productParamSpec.BrandId.HasValue || x.ProductBrandId == productParamSpec.BrandId) && (!productParamSpec.TypeId.HasValue || x.ProductTypeId==productParamSpec.TypeId))
        {

        }
    }
}