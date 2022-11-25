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
    public class OrdenDeCargaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrdenDeCargaRepository _ordenDeCargaRepository;

        public OrdenDeCargaController(IMapper mapper, IOrdenDeCargaRepository ordenDeCargaRepository)
        {
            _mapper = mapper;
            _ordenDeCargaRepository = ordenDeCargaRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrdenDeCargaDto ordenDeCargaDto)
        {
            try
            {
                var ordenDeCarga = _mapper.Map<OrdenDeCarga>(ordenDeCargaDto);

                ordenDeCarga = await _ordenDeCargaRepository.AddOrdenDeCarga(ordenDeCarga);

                var ordenDeCargaItemDto = _mapper.Map<OrdenDeCargaDto>(ordenDeCarga);

                return CreatedAtAction("Get", new { id = ordenDeCargaItemDto.Id }, ordenDeCargaItemDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            try
            {
                if (filter?.PageSize > 20)
                {
                    filter.PageSize = 20;
                }

                var route = Request.Path.Value;
                var pagedResponse = await _ordenDeCargaRepository.GetListOrdenDeCarga(filter, route);

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
                var ordenDeCarga = await _ordenDeCargaRepository.GetOrdenDeCarga(id);

                if (ordenDeCarga == null)
                {
                    return NotFound();
                }
                var ordenDeCargaDto = _mapper.Map<OrdenDeCargaDto>(ordenDeCarga);

                return Ok(new Response<OrdenDeCargaDto>(ordenDeCargaDto));
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
                var ordenDeCarga = await _ordenDeCargaRepository.GetOrdenDeCarga(id);

                if (ordenDeCarga == null)
                {
                    return NotFound();
                }
                await _ordenDeCargaRepository.DeleteOrdenDeCarga(ordenDeCarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, OrdenDeCargaDto ordenDeCargaDto)
        {
            try
            {
                var ordenDeCarga = _mapper.Map<OrdenDeCarga>(ordenDeCargaDto);

                if (id != ordenDeCarga.Id)
                {
                    return BadRequest();
                }

                var ordenDeCargaItem = await _ordenDeCargaRepository.GetOrdenDeCarga(id);

                if (ordenDeCargaItem == null)
                {
                    return NotFound();
                }

                await _ordenDeCargaRepository.UpdateOrdenDeCarga(ordenDeCarga);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

