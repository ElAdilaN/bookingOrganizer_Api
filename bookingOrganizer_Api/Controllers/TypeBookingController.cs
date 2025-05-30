using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace bookingOrganizer_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TypeBookingController : ControllerBase
    {
        private readonly RepoTypeBooking _repoTypeBooking;

        public TypeBookingController(RepoTypeBooking repoTypeBooking)
        {
            _repoTypeBooking = repoTypeBooking;
        }

        [HttpGet("getTypeBookingById")]
        public IActionResult GetTypeBookingById(int id)
        {

            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            DTOTypeBooking _dtoTypeBooking = new DTOTypeBooking();

            try
            {
                _dtoTypeBooking = _repoTypeBooking.getTypeBookingById(id);


                if (_dtoTypeBooking != null)
                {
                    message = "Type Bookings Retrieved Successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {
                        {"typeBooking" , _dtoTypeBooking }
                    };

                    result = Ok(_wrap);



                }
                else
                {
                    message = "No Booking Type found for the given ID.";
                    status = "404";

                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {

                message = "An error occurred while retrieving booking Type by id . Message Error : " + ex;
                status = "500";


                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpGet("getAllBookingTypes")]
        public IActionResult GetAllBookingTypes()
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                ICollection<DTOTypeBooking> bookingTypes = _repoTypeBooking.getAllTypeBookings();

                if (bookingTypes != null)
                {
                    message = "Data retrieved successfully !";
                    status = "200";
                    _wrap.data = new Dictionary<string, object>
                    {
                        { "bookingTypes", bookingTypes }
                    };
                    result = Ok(_wrap);
                }
                else
                {
                    message = "No booking types Found ";
                    status = "404";
                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while retrieving booking types . Message Error: " + ex.Message;
                status = "500";
                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;
            return result;
        }

        [HttpPost("addBookingType")]
        public IActionResult AddBookingType(DTOTypeBooking dtoBookingType)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoTypeBooking.addTypeBooking(dtoBookingType);

                message = "Booking type added successfully !";
                status = "201";

                _wrap.data = new Dictionary<string, object>
                {
                    { "addedBookingType", dtoBookingType}
                };
                result = Created(string.Empty, _wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while adding the booking type .  Message Error : " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;
            return result;
        }

        [HttpDelete("removeBookingType/{bookingTypeId}")]
        public IActionResult RemoveBookingType(int bookingTypeId)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoTypeBooking.RemoveTypeBooking(bookingTypeId);

                message = $"Booking Type with ID {bookingTypeId} removed successfully.";
                status = "200";


                result = Ok(_wrap);


            }
            catch (Exception ex)
            {
                message = "An error occurred while removing the booking type. Message Error: " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }
            _wrap.message = message;
            _wrap.status = status;
            return result;
        }


        [HttpPut("UpdateBookingType")]
        public async Task<IActionResult> UpdateBookingType([FromBody] DTOTypeBooking bookingType)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                await _repoTypeBooking.UpdateBooking(bookingType);

                message = $"Booking type with ID {bookingType.TypeBookingId} updated successfully.";
                status = "200";

                _wrap.data = new Dictionary<string, object>
                {
                    { "updatedBookingType" ,bookingType }
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
                message = "An error occurred while updating the booking Type. Message Error: " + ex.Message;
                status = "500";
                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

    }
}
