using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace bookingOrganizer_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly RepoGroupMember _repoGroupMemeber;
        public GroupMemberController(RepoGroupMember repoGroupMember)
        {
            _repoGroupMemeber = repoGroupMember;
        }
        [HttpGet("GetGroupMembersByGroupId")]
        public IActionResult GetGroupMembersByGroupId(int groupId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            ICollection<DTOGroupMember> dTOGroupMembers = new List<DTOGroupMember>(); ;
            try
            {
                dTOGroupMembers = _repoGroupMemeber.GetGroupMembersByGroupId(groupId);
                if (dTOGroupMembers != null)
                {
                    message = "Data retrieved successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {
                        {"groupMembers" , dTOGroupMembers }
                    };

                    result = Ok(_wrap);
                }
                else
                {
                    message = "No GroupMembers found for the given Group Id ";
                    status = "404";

                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while retrieving GroupMembers By Id . Message Error : " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;
            return result;
        }

        [HttpGet("GetGroupMemberById")]
        public IActionResult GetGroupMemberById(int groupMemberId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            DTOGroupMember dtoGroupMember = new DTOGroupMember();
            try
            {
                dtoGroupMember = _repoGroupMemeber.GetGroupMemberById(groupMemberId);

                if (dtoGroupMember != null)
                {
                    message = "Data retrieved successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {
                        { "GroupMember", dtoGroupMember }
                    };

                    result = Ok(_wrap);
                }
                else
                {
                    message = "No Group Member found for the given Id. Message Error : ";
                    status = "404";
                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {
                message = "An Error occurred while retrieving Group Member by the given id . Message Error : " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpPost("AddGroupMember")]
        public IActionResult AddGroupMember(int groupId, int userId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoGroupMemeber.AddGroupMember(groupId, userId);

                message = "Group Member Added Successfully !";
                status = "201";

                _wrap.data = new Dictionary<string, object>
                {
                    { "AddedGroupMember Group Id ", groupId  },
                    { "AddedGroupMember User Id ", userId},
                };
                result = Created(string.Empty, _wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while adding the Group  Member . Message Error :" + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpDelete("RemoveGroupMember/{groupId}")]
        public IActionResult RemoveGroupMember(int groupId)
        {
            IActionResult result = null; 
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;


            try
            {
                _repoGroupMemeber.RemoveGroupMember(groupId);

                message = $"Group Member with id {groupId} removed successfully !";
                status = "200";

                result = Ok(_wrap);
            }catch(Exception ex)
            {
                message = "An error occurred while removing the Group Member . Message Error :"+ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;

        }

    }
}