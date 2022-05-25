using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Commander.Controllers
{
    //api/controller
    //[Route("api/[controller]")]
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //private readonly MockCommanderRepo commanderRepo = new MockCommanderRepo();
        private readonly ICommanderRepo _commanderRepo ;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo commanderRepo, IMapper mapper )
        {
            _commanderRepo = commanderRepo ;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItem = _commanderRepo.GetAllCommands();
            var res = _mapper.Map<IEnumerable<CommandReadDto>>(commandItem);
            return Ok(res);
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var command = _commanderRepo.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
                //return Ok(command);
            }
            return NotFound(); 
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _commanderRepo.CreateCommand(commandModel);
            _commanderRepo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
             var dd = CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto); 
             return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto); 
            //return Ok(commandReadDto) ;
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _commanderRepo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
             _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _commanderRepo.UpdateCommand(commandModelFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();
        }

        //Patch api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _commanderRepo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _commanderRepo.UpdateCommand(commandModelFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();

        }

        //Delete api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _commanderRepo.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            
            _commanderRepo.DeleteCommand(commandModelFromRepo);
            _commanderRepo.SaveChanges();

            return NoContent();
        }
    }
}
