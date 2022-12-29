using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_LosQuebrachosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDeGasoilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrdenDeGasoilRepository _ordenDeGasoilRepository;

        public OrdenDeGasoilController(IMapper mapper, IOrdenDeGasoilRepository ordenDeGasoilRepository)
        {
            _mapper = mapper;
            _ordenDeGasoilRepository = ordenDeGasoilRepository;
        }

        [HttpPost]

        public async Task<IActionResult> Post(OrdenDeGasoilDto ordenDeGasoilDto)
        {
            try
            {
                var ordenDeGasoil = _mapper.Map<OrdenDeGasoil>(ordenDeGasoilDto);

                ordenDeGasoil = await _ordenDeGasoilRepository.AddOrdenDeGasoil(ordenDeGasoil);

                var ordenDeGasoilItemDto = _mapper.Map<OrdenDeGasoilDto>(ordenDeGasoil);

                return CreatedAtAction("Get", new { id = ordenDeGasoilItemDto.Id }, ordenDeGasoilItemDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                var filter = new PaginationFilter(pageNumber, pageSize, search, sortOrder);

                if (filter?.PageSize > 20)
                {
                    filter.PageSize = 20;
                }

                var route = Request.Path.Value;
                var pagedResponse = await _ordenDeGasoilRepository.GetListOrdenDeGasoil(filter, route);

                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ordenDeGasoil = await _ordenDeGasoilRepository.GetOrdenDeGasoil(id);

                if (ordenDeGasoil == null)
                {
                    return NotFound();
                }

                var ordenDeGasoilDto = _mapper.Map<OrdenDeGasoilDto>(ordenDeGasoil);

                return Ok(ordenDeGasoilDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ordenDeGasoil = await _ordenDeGasoilRepository.GetOrdenDeGasoil(id);

                if (ordenDeGasoil == null)
                {
                    return NotFound();
                }

                await _ordenDeGasoilRepository.DeleteOrdenDeGasoil(ordenDeGasoil);

                return NoContent();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrdenDeGasoilDto ordenDeGasoilDto) 
        {
            try
            {
                var ordenDeGasoil = _mapper.Map<OrdenDeGasoil>(ordenDeGasoilDto);

                if (id != ordenDeGasoil.Id)
                {
                    return BadRequest();
                }

                var ordenDeGasoilItem = await _ordenDeGasoilRepository.GetOrdenDeGasoil(id);

                if (ordenDeGasoilItem == null)
                {
                    return NotFound();
                }

                await _ordenDeGasoilRepository.UpdateOrdenDeGasoil(ordenDeGasoil);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
