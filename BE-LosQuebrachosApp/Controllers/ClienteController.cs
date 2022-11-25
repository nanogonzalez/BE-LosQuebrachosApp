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
    public class ClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);

                cliente = await _clienteRepository.AddCliente(cliente);

                var clienteItemDto = _mapper.Map<ClienteDto>(clienteDto);

                return CreatedAtAction("Get", new { id = clienteItemDto.Id }, clienteItemDto);
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
                var pagedResponse = await _clienteRepository.GetListCliente(filter, route);

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
                var cliente = await _clienteRepository.GetCliente(id);

                if (cliente == null)
                {
                    return NotFound();
                }
                var clienteDto = _mapper.Map<ClienteDto>(cliente);

                return Ok(new Response<ClienteDto>(clienteDto));
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
                var cliente = await _clienteRepository.GetCliente(id);

                if (cliente == null)
                {
                    return NotFound();
                }
                await _clienteRepository.DeleteCliente(cliente);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteDto clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);

                if (id != cliente.Id)
                {
                    return BadRequest();
                }

                var clienteItem = await _clienteRepository.GetCliente(id);

                if (clienteItem == null)
                {
                    return NotFound();
                }

                await _clienteRepository.UpdateCliente(cliente);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
