
using MachineMindApi.Data;
using MachineMindApi.Models;
using MachineMindApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace MachineMindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {

        //inyectaremos el servicio de IlLogger 

        private readonly ILogger<PlantController> _logger;

        //vamos a inyectar a nuestro dbContext al controlador 

        private readonly ApplicationDBContext _dbContext;


        public PlantController(ApplicationDBContext applicationDBContext, ILogger<PlantController> logger)
        {
            _dbContext = applicationDBContext;
            _logger = logger;
        }


        [HttpGet("getplants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Plant>> GetPLants()
        {


            _logger.LogInformation("Obteniendo registros de plantas");
            return Ok(_dbContext.Plants.ToList());

        }


        //[HttpGet("getplantbyid/{id}")]
        [HttpGet("{id}", Name = "getplantbyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Plant> GetPlant(string id)
        {

            if (string.IsNullOrEmpty(id))
            {



                return BadRequest();

            }
            var PlantSelected = _dbContext.Plants.FirstOrDefault(p => p.PlantId.Equals(id));

            if (PlantSelected == null)
            {

                _logger.LogError("No se encontro la planta con ese ID");
                return NotFound();//devolvemos un 404 si no encontramos la planta 

            }

            return Ok(PlantSelected);
        }




        //Create Plant
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<Plant> CreatePlant([FromBody] PlantDto plantdto)
        {

            //validamos el modelo
            if (plantdto == null)
            {

                return BadRequest(plantdto);

            }

            //validamos si el nombre de la planta ya existe 

            if (_dbContext.Plants.FirstOrDefault(p => p.PlantId.ToLower() == plantdto.PlantId.ToLower()) != null)
            {

                //Creamos un nuevo Tipo de error para documentar los mismo y devolverlos 

                ModelState.AddModelError("Object Exist", "Ya existe un regitro con ese ID");
                return BadRequest(ModelState);

            }


            //validamos el nombre 

            //validamos si el nombre de la planta ya existe 

            if (_dbContext.Plants.FirstOrDefault(p => p.Name.ToLower() == plantdto.Name.ToLower()) != null)
            {

                //Creamos un nuevo Tipo de error para documentar los mismo y devolverlos 

                ModelState.AddModelError("Object Exist", "Ya existe un regitro con ese Nombre");
                return BadRequest(ModelState);

            }

            Plant plant = new Plant
            {

                PlantId = plantdto.PlantId,
                Name = plantdto.Name,
                ProductionLines = new List<ProductionLine>(),

            };

            if (ModelState.IsValid)
            {
                // si va todo Bien guardamos los cambios 
                _dbContext.Plants.Add(plant);
                _dbContext.SaveChanges();


            }

            // return Ok(plant); esto no esta mal pero...en una api cuando creamos un recurso nuevo muchas veces debemos indicar la url con el recurso creado 
            return CreatedAtRoute("getplantbyid", new { id = plantdto.PlantId }, plantdto);


        }



        //Delete plant 

        [HttpDelete("DeletePlant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeletePlant(string id)
        {


            if (string.IsNullOrEmpty(id))
            {

                return BadRequest();
            }

            var plantToDelete = _dbContext.Plants.FirstOrDefault(p => p.PlantId.ToLower() == id.ToLower());

            if (plantToDelete != null)
            {

                _dbContext.Plants.Remove(plantToDelete);
                _dbContext.SaveChanges();
                return NoContent();

            }
            else
            {

                return NotFound();

            }


        }



        //Update Plant mediante metodo Put 

        [HttpPut("UpdatePlant/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePLant(string id, [FromBody] PlantDto plantDto)
        {


            if (string.IsNullOrEmpty(id))
            {

                return BadRequest();
            }

            if (plantDto == null || id != plantDto.PlantId)
            {

                return BadRequest();

            }

            var plantToUpdate = _dbContext.Plants.FirstOrDefault(p => p.PlantId == id);

            plantToUpdate.PlantId = plantDto.PlantId;
            plantToUpdate.Name = plantDto.Name;
            plantToUpdate.ProductionLines = new List<ProductionLine>();

            _dbContext.Plants.Update(plantToUpdate);
            _dbContext.SaveChanges();
            return NoContent();
        }


        //Update Plant mediante metodo Patch 

        [HttpPatch("UpdatePatchPLant/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePatchPLant(string id, JsonPatchDocument<PlantDto> patchPlantDto)
        {


            if (string.IsNullOrEmpty(id))
            {

                return BadRequest();
            }

            if (patchPlantDto == null || string.IsNullOrEmpty(id))
            {

                return BadRequest();

            }


            var plantToUpdate = _dbContext.Plants.AsNoTracking().FirstOrDefault(p => p.PlantId == id);


            //convertimos el registro a su dto
            PlantDto plantDto = new()
            {

                PlantId = plantToUpdate.PlantId,
                Name = plantToUpdate.Name,

            };

            //realizamos el cambio 
            patchPlantDto.ApplyTo(plantDto, ModelState);

            //ahora para poder guardarlo en la tabla necesitamos nuevamente psarlo a un tipo modelo 

            Plant modelo = new()
            {

                PlantId = plantDto.PlantId,
                Name = plantDto.Name,
                ProductionLines = plantToUpdate.ProductionLines,

            };

            if (ModelState.IsValid)
            {
                _dbContext.Plants.Update(modelo);
                _dbContext.SaveChanges();
                return NoContent();

            }
            else
            {

                return BadRequest(ModelState);

            }

        }

    }
}

