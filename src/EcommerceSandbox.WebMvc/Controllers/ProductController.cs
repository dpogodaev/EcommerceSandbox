using AutoMapper;
using EcommerceSandbox.App.Services.Interfaces.Services;
using EcommerceSandbox.WebMvc.Dtos;
using EcommerceSandbox.WebMvc.Models.Product;
using Microsoft.AspNetCore.Mvc;
using AppModels = EcommerceSandbox.App.Services.Models.Product;
using AppDto = EcommerceSandbox.App.Services.Dtos;

namespace EcommerceSandbox.WebMvc.Controllers;

public class ProductController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProductController> _logger;
    private readonly IProductAppService _productAppService;

    public ProductController(
        IMapper mapper,
        ILogger<ProductController> logger,
        IProductAppService productAppService)
    {
        _mapper = mapper;
        _logger = logger;
        _productAppService = productAppService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productAppService.GetAllAsync();

        return View(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductCreationModel creationModel)
    {
        await _productAppService.CreateAsync(_mapper.Map<AppModels.ProductCreationModel>(creationModel));

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Mapping rules for models and DTOs of App and Web MVC components.
    /// </summary>
    public class Mapping : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public Mapping()
        {
            CreateMap<AppDto.ProductDto, ProductDto>();
            CreateMap<ProductCreationModel, AppModels.ProductCreationModel>();
        }
    }
}