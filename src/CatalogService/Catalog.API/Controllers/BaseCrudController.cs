using AutoMapper;
using Catalog.API.Contract;
using Catalog.Business.Models;
using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseCrudController<TEntity, TDto, TCreateDto, TUpdateDto, TDeleteDto> : ControllerBase
    where TEntity : BaseEntity
{
    protected ICrudService<TEntity> CrudService { get; }
    protected IMapper Mapper { get; }

    public BaseCrudController(ICrudService<TEntity> crudService, IMapper mapper)
    {
        CrudService = crudService;
        Mapper = mapper;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll([FromQuery]PaginationQuery paginationQuery)
    {
        var entities = await CrudService.GetAllAsync(paginationQuery ?? new PaginationQuery());
        return Ok(Mapper.Map<List<TDto>>(entities));
    }

    [HttpGet("{id:int}")]
    public virtual async Task<IActionResult> GetById(int id)
    {
        var entity = await CrudService.GetByIdAsync(id);

        return entity != default ? Ok(Mapper.Map<TDto>(entity)) : Ok();
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(TCreateDto createDto)
    {
        var entity = Mapper.Map<TEntity>(createDto);
        var result = await CrudService.CreateAsync(entity);

        return result != null ? Ok(Mapper.Map<TDto>(result)) : StatusCode(500);
    }

    [HttpPut("{id:int}")]
    public virtual async Task<IActionResult> Update(int id, TUpdateDto updateDto)
    {
        var entity = Mapper.Map<TEntity>(updateDto);
        var result = await CrudService.UpdateAsync(id, entity);

        return result != null ? Ok(Mapper.Map<TDto>(result)) : StatusCode(500);
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id, TDeleteDto deleteDto)
    {
        var entity = Mapper.Map<TEntity>(deleteDto);
        var result = await CrudService.DeleteAsync(id, entity);

        return result > 0 ? NoContent() : StatusCode(500);
    }
}
