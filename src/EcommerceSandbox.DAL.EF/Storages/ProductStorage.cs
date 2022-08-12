using AutoMapper;
using EcommerceSandbox.App.Storage.Interfaces;
using EcommerceSandbox.App.Storage.PersistentDtos;
using EcommerceSandbox.DAL.EF.Entities;
using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;

namespace EcommerceSandbox.DAL.EF.Storages;

public class ProductStorage : IProductStorage
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Product> _repository;

    public ProductStorage(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;

        _repository = _unitOfWork.GenericRepository<Product>();
    }

    #region IProductStorage
    
    public async Task<ProductPDto> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return _mapper.Map<ProductPDto>(entity);
    }
    
    public async Task<IEnumerable<ProductPDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductPDto>>(entities);
    }
    
    public async Task<ProductPDto> CreateAsync(ProductPDto dto)
    {
        try
        {
            _unitOfWork.CreateTransaction();

            var entity = await _repository.InsertAsync(_mapper.Map<Product>(dto));

            _unitOfWork.Save();
            _unitOfWork.Commit();

            return _mapper.Map<ProductPDto>(entity);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();

            throw;
        }
    }
    
    public async Task<ProductPDto> UpdateAsync(ProductPDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);

        _mapper.Map(dto, entity);

        await _repository.UpdateAsync(entity);

        return _mapper.Map<ProductPDto>(entity);
    }
    
    public async Task<ProductPDto> DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            return null;
        }

        var dto = _mapper.Map<ProductPDto>(entity);

        await _repository.DeleteAsync(entity);

        _unitOfWork.Save();

        return dto;
    }

    #endregion

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductPDto>().ReverseMap();
        }
    }
}