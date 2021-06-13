using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] {"this","is","hard","code" };
        //}
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        [HttpGet("{id}", Name = "GetCommandById ")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChange();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModel);
            _repository.SaveChange();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }
            var commandUpdateDto = _mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandUpdateDto, ModelState);
            if (!TryValidateModel(commandUpdateDto))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandUpdateDto,commandModel);
            //_repository.UpdateCommand(commandModel);
            _repository.SaveChange();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModel);
            _repository.SaveChange();
            return NoContent();
        }
    }
}
