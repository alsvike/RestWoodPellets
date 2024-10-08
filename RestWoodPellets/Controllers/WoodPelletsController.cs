using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WoodPelletsLib;

namespace RestWoodPellets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WoodPelletsController : Controller
    {
        private readonly WoodPelletRepository _woodPelletRepository;

        public WoodPelletsController(WoodPelletRepository woodPelletRepository)
        {
            _woodPelletRepository = woodPelletRepository;
        }

        // GET: WoodPelletsController
        [HttpGet]
        public IEnumerable<WoodPellet> Get()
        {
            return _woodPelletRepository.GetAll();
        }

        [HttpGet("{id}")]
        // GET: WoodPelletsController/api/woodpellets/5
        public ActionResult<WoodPellet> GetById(int id)
        {
            var woodPellet = _woodPelletRepository.GetById(id);
            if (woodPellet == null)
            {
                return NotFound();
            }
            return Ok(woodPellet);
        }

        [HttpPost]
        public ActionResult<WoodPellet> Post([FromBody] WoodPellet woodPellet)
        {
            var createdWoodPellet = _woodPelletRepository.Add(woodPellet);
            return CreatedAtAction(nameof(GetById), new { id = createdWoodPellet.Id }, createdWoodPellet);
        }

        [HttpPut("{id}")]
        public ActionResult<WoodPellet> Put(int id, [FromBody] WoodPellet woodPellet)
        {
            var existingWoodPellet = _woodPelletRepository.GetById(id);
            if (existingWoodPellet == null)
            {
                return NotFound();
            }

            var updatedWoodPellet = _woodPelletRepository.Update(woodPellet);
            return Ok(updatedWoodPellet);
        }
    }
}
