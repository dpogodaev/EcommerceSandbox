using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSandbox.DomainEntities.Entities;
using EcommerceSandbox.WebMvc.Dtos;
using EcommerceSandbox.WebMvc.Interfaces;
using EcommerceSandbox.WebMvc.Models.Product;
using AppLayerServices = EcommerceSandbox.AppServices.Interfaces.Services;
using AppLayerModels = EcommerceSandbox.AppServices.Models.Product;
using AppLayerDtos = EcommerceSandbox.AppServices.Dtos;

namespace EcommerceSandbox.WebMvc.Adapters;

/// <summary>
/// Adapter for <see cref="IProductService"/>.
/// </summary>
public class ProductServiceAdapter : IProductService
{
    private readonly IMapper _mapper;
    private readonly AppLayerServices.IProductAppService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductServiceAdapter"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="service">Used for performing operations with <see cref="Product"/>s.</param>
    public ProductServiceAdapter(
        IMapper mapper,
        AppLayerServices.IProductAppService service)
    {
        _mapper = mapper;
        _service = service;
    }

    #region IProductService

    /// <inheritdoc cref="IProductService.GetByIdAsync"/>
    public async Task<ProductDto> GetByIdAsync(long id)
    {
        return _mapper.Map<ProductDto>(
            await _service.GetByIdAsync(id));
    }

    /// <inheritdoc cref="IProductService.GetAll"/>
    public IEnumerable<ProductDto> GetAll()
    {
        return _mapper.Map<IEnumerable<ProductDto>>(
            _service.GetAll());
    }

    /// <inheritdoc cref="IProductService.AddProduct"/>
    public async Task<ProductDto> AddProduct(ProductCreationModel modelToCreate)
    {
        return _mapper.Map<ProductDto>(
            await _service.CreateAsync(
                _mapper.Map<AppLayerModels.ProductCreationModel>(modelToCreate)));
    }

    /// <inheritdoc cref="IProductService.UpdateAsync"/>
    public async Task<ProductDto> UpdateAsync(ProductUpdateModel modelToUpdate)
    {
        return _mapper.Map<ProductDto>(
            await _service.UpdateAsync(
                _mapper.Map<AppLayerModels.ProductUpdateModel>(modelToUpdate)));
    }

    /// <inheritdoc cref="IProductService.BulkUpdateAsync"/>
    public async Task BulkUpdateAsync(IEnumerable<ProductUpdateModel> modelsToUpdate)
    {
        await _service.BulkUpdateAsync(
            _mapper.Map<IEnumerable<AppLayerModels.ProductUpdateModel>>(modelsToUpdate));
    }

    /// <inheritdoc cref="IProductService.DeleteAsync"/>
    public async Task<ProductDto> DeleteAsync(long id)
    {
        return _mapper.Map<ProductDto>(
            await _service.DeleteAsync(id));
    }

    #endregion

    /// <summary>
    /// Mapping rules for <see cref="EcommerceSandbox.WebMvc"/> and <see cref="EcommerceSandbox.AppServices"/>.
    /// </summary>
    public class Mapping : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public Mapping()
        {
            CreateMap<ProductCreationModel, AppLayerModels.ProductCreationModel>();
            CreateMap<ProductUpdateModel, AppLayerModels.ProductUpdateModel>();
            CreateMap<ProductDto, AppLayerDtos.ProductDto>().ReverseMap();
        }
    }
}