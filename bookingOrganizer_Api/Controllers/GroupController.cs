using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingOrganizer_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly RepoGroup _repoGroup;
        public GroupController(RepoGroup repoGroup)
        {
            _repoGroup = repoGroup;
        }

        [HttpGet("GetAllGroups")]
        public IActionResult getAllGroup()
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            ICollection<DTOGroup> _dtoGroups = new List<DTOGroup>();
            try
            {
                _dtoGroups = _repoGroup.getAllGroups();
                if (_dtoGroups != null)
                {
                    message = "Data Retrieved Successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {
                        {"groups", _dtoGroups }
                    };

                    result = Ok(_wrap);
                }
                else
                {
                    message = "No Groups found ";
                    status = "404";

                    result = NotFound(_wrap);
                }

            }
            catch (Exception ex)
            {
                message = "An error occurred while retreving Groups . Message Error : " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }


        [HttpGet("getGroupById")]
        public IActionResult getGroupById(int groupId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            DTOGroup _dtoGroup = new DTOGroup();

            try
            {
                _dtoGroup = _repoGroup.getGroupById(groupId);

                if (_dtoGroup != null)
                {
                    message = "Data retrieved Successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {
                        {"group", _dtoGroup }
                    };

                    result = Ok(_wrap);

                }
                else
                {
                    message = "No group found for the given ID";
                    status = "404";

                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while retrieving Group By the given ID" + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpPost("AddGroup")]
        public IActionResult AddGroup([FromBody] DTOGroup dtoGroup)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoGroup.addGroup(dtoGroup);

                message = "Group Added successfully !";
                status = "201";

                _wrap.data = new Dictionary<string, object>
                {
                    {"addedGroup" , dtoGroup}
                };

                result = Created(string.Empty, _wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while adding the Group. Message Error: " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }
        [HttpDelete("RemoveGroup/{groupId}")]
        public IActionResult RemoveGroup(int groupId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoGroup.removeGroup(groupId);

                message = $"Group with ID {groupId} removed successfully.";
                status = "200";

                result = Ok(_wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while removing the Group. Message Error: " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;
            return result;
        }

        [HttpPut("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup([FromBody] DTOGroup dTOGroup)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                await _repoGroup.UpdateGroup(dTOGroup);

                message = $"Group with ID {dTOGroup.GroupId} updated successfully.";
                status = "200";

                _wrap.data = new Dictionary<string, object>
                {
                    {"updatedGroup"  , dTOGroup }
                };

                result = Ok(_wrap);
            }
            catch (NotFoundException nfEx)
            {
                message = nfEx.Message;
                status = "404";
                result = NotFound(_wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while updating the Group. Message Error :" + ex.Message;
                status = "500";
                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;
            return result;
        }
    }
}
