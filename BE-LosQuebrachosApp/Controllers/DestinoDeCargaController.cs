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
    public class DestinoDeCargaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDestinoDeCargaRepository _destinoDeCargaRepository;

        public DestinoDeCargaController(IMapper mapper, IDestinoDeCargaRepository destinoDeCargaRepository)
        {
            _mapper = mapper;
            _destinoDeCargaRepository = destinoDeCargaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DestinoDeCargaDto destinoDeCargaDto)
        {
            try
            {
                var destinoDeCarga = _mapper.Map<DestinoDeCarga>(destinoDeCargaDto);

                destinoDeCarga = await _destinoDeCargaRepository.AddDestinoDeCarga(destinoDeCarga);

                var destinoDeCargaItemDto = _mapper.Map<DestinoDeCargaDto>(destinoDeCarga);

                return CreatedAtAction("Get", new { id = destinoDeCargaItemDto.Id }, destinoDeCargaItemDto);
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
                var pagedResponse = await _destinoDeCargaRepository.GetListDestinoDeCarga(filter, route);

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
                var destinoDeCarga = await _destinoDeCargaRepository.GetDestinoDeCarga(id);

                if (destinoDeCarga == null)
                {
                    return NotFound();
                }
                var destinoDeCargaDto = _mapper.Map<DestinoDeCargaDto>(destinoDeCarga);

                return Ok(destinoDeCargaDto);
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
                var destinoDeCarga = await _destinoDeCargaRepository.GetDestinoDeCarga(id);

                if (destinoDeCarga == null)
                {
                    return NotFound();
                }
                await _destinoDeCargaRepository.DeleteDestinoDeCarga(destinoDeCarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DestinoDeCargaDto destinoDeCargaDto)
        {
            try
            {
                var destinoDeCarga = _mapper.Map<DestinoDeCarga>(destinoDeCargaDto);

                if (id != destinoDeCarga.Id)
                {
                    return BadRequest();
                }

                var destinoDeCargaItem = await _destinoDeCargaRepository.GetDestinoDeCarga(id);

                if (destinoDeCargaItem == null)
                {
                    return NotFound();
                }

                await _destinoDeCargaRepository.UpdateDestinoDeCarga(destinoDeCarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
