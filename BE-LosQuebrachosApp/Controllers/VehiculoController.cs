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
    public class VehiculoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehiculoRepsitory _vehiculoRepsitory;

        public VehiculoController(IMapper mapper, IVehiculoRepsitory vehiculoRepsitory)
        {
            _mapper = mapper;
            _vehiculoRepsitory = vehiculoRepsitory;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VehiculoDto vehiculoDto)
        {
            try
            {
                var vehiculo = _mapper.Map<Vehiculo>(vehiculoDto);

                vehiculo = await _vehiculoRepsitory.AddVehiculo(vehiculo);

                var vehiculoItemDto = _mapper.Map<VehiculoDto>(vehiculo);

                return CreatedAtAction("Get", new {id = vehiculoItemDto.Id}, vehiculoItemDto);
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
                var listVehiculo = await _vehiculoRepsitory.GetListVehiculos(filter, route);

                var listVehiculoDto = _mapper.Map<IEnumerable<VehiculoDto>>(listVehiculo);

                return Ok(listVehiculoDto);
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
                var vehiculo = await _vehiculoRepsitory.GetVehiculo(id);

                if (vehiculo == null)
                {
                    return NotFound();
                }

                var vehiculoDto = _mapper.Map<VehiculoDto>(vehiculo);

                return Ok( new Response<VehiculoDto>(vehiculoDto));
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
                var vehiculo = await _vehiculoRepsitory.GetVehiculo(id);

                if (vehiculo == null)
                {
                    return NotFound();
                }
                await _vehiculoRepsitory.DeleteVehiculo(vehiculo);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VehiculoDto vehiculoDto)
        {
            try
            {
                var vehiculo = _mapper.Map<Vehiculo>(vehiculoDto);

                if (id != vehiculo.Id)
                {
                    return BadRequest();
                }

                var vehiculoItem = await _vehiculoRepsitory.GetVehiculo(id);

                if (vehiculoItem == null)
                {
                    return NotFound();
                }

                await _vehiculoRepsitory.UpdateVehiculo(vehiculo);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
