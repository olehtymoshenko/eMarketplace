using AutoMapper;
using Catalog.API.Mapping;
using Catalog.Business.Services.Abstractions;
using Catalog.Common.Models;
using Catalog.Persistence.Entities.Abstractions;
using FluentValidation;
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
    public virtual async Task<IResult> GetAll([FromQuery] PaginationQuery paginationQuery)
    {
        var entities = await CrudService.GetAllAsync(paginationQuery ?? new PaginationQuery());

        return entities.Match(
            (res) => Results.Ok(Mapper.Map<List<TDto>>(res ?? Array.Empty<TEntity>())),
            (err) => err.MapToResponse());
    }

    [HttpGet("{id:int}")]
    public virtual async Task<IResult> GetById(int id)
    {
        if(id <= 0)
        {
            return Results.ValidationProblem(errors: new Dictionary<string, string[]>()
            {
                { nameof(id), ["Value must be greater than 0"] }
            });
        }

        var entity = await CrudService.GetByIdAsync(id);

        return entity.Match(
            (res) => Results.Ok(Mapper.Map<TDto>(res)),
            (err) => err.MapToResponse());
    }

    [HttpPost]
    public virtual async Task<IResult> Create(TCreateDto createDto, [FromServices]IValidator<TCreateDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(createDto);
        if(!validatorResult.IsValid)
        {
            return Results.ValidationProblem(validatorResult.ToDictionary());
        }

        var entity = Mapper.Map<TEntity>(createDto);
        var result = await CrudService.CreateAsync(entity);

        return result.Match(
            (res) => Results.Ok(Mapper.Map<TDto>(res)),
            (err) => err.MapToResponse());
    }

    [HttpPut("{id:int}")]
    public virtual async Task<IResult> Update(int id, TUpdateDto updateDto, [FromServices] IValidator<TUpdateDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(updateDto);
        if (!validatorResult.IsValid)
        {
            return Results.ValidationProblem(validatorResult.ToDictionary());
        }

        var entity = Mapper.Map<TEntity>(updateDto);
        var result = await CrudService.UpdateAsync(id, entity);

        return result.Match(
            (res) => Results.Ok(Mapper.Map<TDto>(res)),
            (err) => err.MapToResponse());
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IResult> Delete(int id, TDeleteDto deleteDto, [FromServices] IValidator<TDeleteDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(deleteDto);
        if (!validatorResult.IsValid)
        {
            return Results.ValidationProblem(validatorResult.ToDictionary());
        }

        var entity = Mapper.Map<TEntity>(deleteDto);
        var result = await CrudService.DeleteAsync(id, entity);

        return result.Match(
            (res) => Results.NoContent(),
            (err) => err.MapToResponse());
    }
}
