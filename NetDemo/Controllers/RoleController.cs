using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetDemo.Data;
using NetDemo.Data.Repository;
using NetDemo.Models;
using System.Net;

namespace NetDemo.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private APIResponse _apiResponse;

        public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _apiResponse = new();
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateRole(RoleDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            Role role = _mapper.Map<Role>(dto);
            role.isDeleted = false;
            role.CreatedDate = DateTime.Now;
            role.ModifiedDate = DateTime.Now;

            var result = await _roleRepository.CreateAsync(role);
            dto.Id = result.Id;
            _apiResponse.Data = dto;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);

        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetRoles()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                _apiResponse.Data = _mapper.Map<List<RoleDTO>>(roles);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }
    }
}
