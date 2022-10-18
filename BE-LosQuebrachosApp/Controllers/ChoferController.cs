using AutoMapper;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_LosQuebrachosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoferController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChoferRepository _choferRepository;

        public ChoferController(IMapper mapper, IChoferRepository choferRepository)
        {
            _mapper = mapper;
            _choferRepository = choferRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChoferDto choferDto)
        {
            try
            {
                var chofer = _mapper.Map<Chofer>(choferDto);

                chofer = await _choferRepository.AddChofer(chofer);

                var choferItemDto = _mapper.Map<ChoferDto>(chofer);

                return CreatedAtAction("Get", new { id = choferItemDto.Id }, choferItemDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listChoferes = await _choferRepository.GetListChoferes();

            var listChoferesDto = _mapper.Map<IEnumerable<ChoferDto>>(listChoferes);

            return Ok(listChoferesDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var chofer = await _choferRepository.GetChofer(id);

                if(chofer == null)
                {
                    return NotFound();
                }
                var choferDto = _mapper.Map<ChoferDto>(chofer);

                return Ok(choferDto);
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
                var chofer = await _choferRepository.GetChofer(id);

                if (chofer == null)
                {
                    return NotFound();
                }
                await _choferRepository.DeleteChofer(chofer);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ChoferDto choferDto)
        {
            try
            {
                var chofer = _mapper.Map<Chofer>(choferDto);

                if (id != chofer.Id)
                {
                    return BadRequest();
                }

                var choferItem = await _choferRepository.GetChofer(id);

                if (choferItem == null)
                {
                    return NotFound();
                }

                await _choferRepository.UpdateChofer(chofer);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
