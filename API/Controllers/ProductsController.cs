using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;
using API.Errors;
using System.Net;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{    
    public class ProductsController : BaseApiController
    {
        //private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepository
        , IGenericRepository<ProductBrand> productBrandRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            this._productTypeRepository = productTypeRepository;
            this._productBrandRepository = productBrandRepository;
            this._productRepository = productRepository;
            this._mapper = mapper;
            
        }
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductParamSpec productParamSpec)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParamSpec);
        var countSpec = new ProductWithFiltersForCountSpecification(productParamSpec);
        var totalCount = await _productRepository.CountAsync(countSpec);
        var products = await _productRepository.ListAsync(spec);
        var productsResponse = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(productParamSpec.PageIndex,productParamSpec.PageSize,totalCount,productsResponse));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct(long id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productRepository.GetEntityWithSpec(spec);
        if(product==null)
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
        }
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<Product>> GetProductBrands()
    {
        var productBrands = await _productBrandRepository.ListAllAsync();
        return Ok(productBrands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<Product>> GetProductTypes()
    {
        var productTypes = await _productTypeRepository.ListAllAsync();
        return Ok(productTypes);
    }
}
}