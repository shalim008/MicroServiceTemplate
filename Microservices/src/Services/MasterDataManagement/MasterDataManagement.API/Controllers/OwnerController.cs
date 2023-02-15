using AutoMapper;
using MasterDataManagement.API.Dtos;
using MasterDataManagement.API.Errors;
using MasterDataManagement.API.Helpers;
using MasterDataManagement.Core.Entities;
using MasterDataManagement.Core.IRepository;
using MasterDataManagement.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataManagement.API.Controllers
{

    [Route("api/v1/[controller]")]

    [Authorize]
    public class OwnerController : BaseApiController
    {
        private readonly IGenericRepository<SysOwner> _ownerRepo;
        private readonly IMapper _mapper;

        public OwnerController(IGenericRepository<SysOwner> ownerRepo, IMapper mapper)
        {
            _ownerRepo = ownerRepo;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<SysOwnerDto>>> GetAll()
        {
            var data = await _ownerRepo.ReadOnlyListAsync();
            if (data.Count == 0) return Ok(new ApiResponse(204));
            return Ok(_mapper.Map<IReadOnlyList<SysOwner>, IReadOnlyList<SysOwnerDto>>(data));
        }


        [HttpGet("getAllWithServerSidePagination")]
        public async Task<ActionResult<Pagination<SysOwnerDto>>> GetAllWithServerSidePagination([FromQuery] OwnerListSpecificationParams param)
        {
            var spec = new OwnerListSpecification(param);
            var countSpec = new OwnerListWithFiltersForCountSpecificication(param);
            var totalItems = await _ownerRepo.CountAsync(countSpec);
            var dataSet = await _ownerRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<SysOwnerDto>>(dataSet);
            return Ok(new Pagination<SysOwnerDto>(param.PageIndex, param.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SysOwnerDto>> GetById(Int64 id)
        {
            var data = await _ownerRepo.GetByIdAsync(id);
            if (data == null) return Ok(new ApiResponse(204));
            return Ok(_mapper.Map<SysOwner, SysOwnerDto>(data));
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SysOwner>> Create(SysOwnerDto _object)
        {
            var data = _mapper.Map<SysOwnerDto, SysOwner>(_object);
            await _ownerRepo.AddAsync(data);
            int status = await _ownerRepo.Complete();
            if (status != 1) return BadRequest(new ApiResponse(400, "Data Save Faild"));
            return Ok(data);
        }

        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Int64 id)
        {
            var data = await _ownerRepo.GetByIdAsync(id);

            if (data == null) return Ok(new ApiResponse(204));
            await _ownerRepo.Delete(data);

            try
            {
                int status = await _ownerRepo.Complete();
                if (status == 0) return BadRequest(new ApiResponse(400, "Data Delete Faild"));
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(500, ex.InnerException != null ? ex.InnerException.Message : ex.Message));
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(SysOwnerDto SysOwner)
        {
            var existData = await _ownerRepo.GetByIdAsync(SysOwner.Id);

            if (existData == null) return Ok(new ApiResponse(204));
            existData.OwnerCode = SysOwner.OwnerCode;
            existData.OwnerName = SysOwner.OwnerName;
            existData.ParentOwnerId = SysOwner.ParentOwnerId;
            existData.Description = SysOwner.Description;
            int status = await _ownerRepo.Complete();
            if (status == 1) return Ok(status);
            return NotFound(new ApiResponse(500));
        }
    }
}
