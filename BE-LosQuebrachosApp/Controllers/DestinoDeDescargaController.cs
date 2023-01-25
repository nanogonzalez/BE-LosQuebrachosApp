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
    public class DestinoDeDescargaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDestinoDeDescargaRepository _destinoDeDescargaRepository;
        public DestinoDeDescargaController(IMapper mapper, IDestinoDeDescargaRepository destinoDeDescargaRepository)
        {
            _mapper = mapper;
            _destinoDeDescargaRepository = destinoDeDescargaRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(DestinoDeDescargaDto destinoDeDescargaDto)
        {
            try
            {
                var destinoDeDescarga = _mapper.Map<DestinoDeDescarga>(destinoDeDescargaDto);

                destinoDeDescarga = await _destinoDeDescargaRepository.AddDestinoDeDescarga(destinoDeDescarga);

                var destinoDeDescargaItemDto = _mapper.Map<DestinoDeDescargaDto>(destinoDeDescarga);

                return CreatedAtAction("Get", new { id = destinoDeDescargaItemDto.Id }, destinoDeDescargaItemDto);
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
                var pagedResponse = await _destinoDeDescargaRepository.GetListDestinoDeDescarga(filter, route);

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
                var destinoDeDescarga = await _destinoDeDescargaRepository.GetDestinoDeDescarga(id);

                if (destinoDeDescarga == null)
                {
                    return NotFound();
                }
                var destinoDeDescargaDto = _mapper.Map<DestinoDeDescargaDto>(destinoDeDescarga);

                return Ok(destinoDeDescargaDto);
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
                var destinoDeDescarga = await _destinoDeDescargaRepository.GetDestinoDeDescarga(id);

                if (destinoDeDescarga == null)
                {
                    return NotFound();
                }
                await _destinoDeDescargaRepository.DeleteDestinoDeDescarga(destinoDeDescarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DestinoDeDescargaDto destinoDeDescargaDto)
        {
            try
            {
                var destinoDeDescarga = _mapper.Map<DestinoDeDescarga>(destinoDeDescargaDto);

                if (id != destinoDeDescarga.Id)
                {
                    return BadRequest();
                }

                var destinoDeDescargaItem = await _destinoDeDescargaRepository.GetDestinoDeDescarga(id);

                if (destinoDeDescargaItem == null)
                {
                    return NotFound();
                }

                await _destinoDeDescargaRepository.UpdateDestinoDeDescarga(destinoDeDescarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
