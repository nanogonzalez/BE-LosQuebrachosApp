using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Filter;
using BE_LosQuebrachosApp.Repositories;
using BE_LosQuebrachosApp.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_LosQuebrachosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransporteRepository _transporteRepository;

        public TransporteController(IMapper mapper, ITransporteRepository transporteRepository)
        {
            _mapper = mapper;
            _transporteRepository = transporteRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(TransporteDto transporteDto)
        {
            try
            {
                var transporte = _mapper.Map<Transporte>(transporteDto);

                transporte = await _transporteRepository.AddTransporte(transporte);

                var transporteItemDto = _mapper.Map<TransporteDto>(transporte);

                return CreatedAtAction("Get", new { id = transporteItemDto.Id }, transporteItemDto);
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

                if(filter?.PageSize > 20)
                {
                    filter.PageSize = 20;
                }
                
                var route = Request.Path.Value;
                var pagedResponse = await _transporteRepository.GetListTransportes(filter, route);

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
                var transporte = await _transporteRepository.GetTransporte(id);

                if (transporte == null)
                {
                    return NotFound();
                }
                var transporteDto = _mapper.Map<TransporteDto>(transporte);

                return Ok(new Response<TransporteDto>(transporteDto));
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
                var transporte = await _transporteRepository.GetTransporte(id);

                if (transporte == null)
                {
                    return NotFound();
                }
                await _transporteRepository.DeleteTransporte(transporte);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TransporteDto transporteDto)
        {
            try
            {
                var transporte = _mapper.Map<Transporte>(transporteDto);

                if (id != transporte.Id)
                {
                    return BadRequest();
                }

                var transporteItem = await _transporteRepository.GetTransporte(id);

                if (transporteItem == null)
                {
                    return NotFound();
                }

                await _transporteRepository.UpdateTransporte(transporte);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

