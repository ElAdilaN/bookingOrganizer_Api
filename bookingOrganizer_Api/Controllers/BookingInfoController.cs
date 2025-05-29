using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.UTILS;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace bookingOrganizer_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BookingInfoController : ControllerBase
    {

        private readonly RepoBookingInfo _repoBookingInfo;

        public BookingInfoController(RepoBookingInfo repoBookingInfo)
        {
            _repoBookingInfo = repoBookingInfo;
        }


        [HttpGet("getBookingInfoById")]
        public IActionResult getBookingInfoById(int id)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            //            IEnumerable<DTOBookingInfo> _dtoBookingInfo = new List<DTOBookingInfo>();
            DTOBookingInfo _dtoBookingInfo = new DTOBookingInfo();

            try
            {
                _dtoBookingInfo = _repoBookingInfo.getBookingInfoById(id);

                if (_dtoBookingInfo != null)
                {

                    message = "Data Retrieved Successfully !";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
                    {

                        {"bookingInfo" , _dtoBookingInfo }
                    };

                    result = Ok(_wrap);

                }
                else
                {
                    message = "No booking info found for the given ID.";
                    status = "404";

                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {

                message = "An error occurred while retrieving booking info. Message Error : " + ex;
                status = "500";


                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpGet("getBookings")]
        public IActionResult GetBookings(
        int? bookingId = null,
        int? groupId = null,
        int? typeBookingId = null,
        DateTime? date = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? address = null,
        string? purchaseMethod = null,
        string? seatNumber = null,
        string? notes = null)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                ICollection<DTOBookingInfo> bookings = _repoBookingInfo.getBookings(
                    bookingId, groupId, typeBookingId, date, startDate, endDate,
                    address, purchaseMethod, seatNumber, notes
                );

                if (bookings != null && bookings.Any())
                {
                    message = "Data retrieved successfully!";
                    status = "200";

                    _wrap.data = new Dictionary<string, object>
            {
                { "bookingList", bookings }
            };

                    result = Ok(_wrap);
                }
                else
                {
                    message = "No bookings found for the given parameters.";
                    status = "404";

                    result = NotFound(_wrap);
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while retrieving bookings. Message Error: " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }

        [HttpPost("addBooking")]
        public IActionResult AddBooking([FromBody] BookingInfo booking)
        {
            IActionResult result = null;
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoBookingInfo.AddBooking(booking);

                message = "Booking added successfully!";
                status = "201";

                _wrap.data = new Dictionary<string, object>
        {
            { "addedBooking", booking }
        };

                result = Created(string.Empty, _wrap); // 201 Created
            }
            catch (Exception ex)
            {
                message = "An error occurred while adding the booking. Message Error: " + ex.Message;
                status = "500";

                result = BadRequest(_wrap);
            }

            _wrap.message = message;
            _wrap.status = status;

            return result;
        }


        [HttpDelete("removeBooking/{bookingId}")]
        public IActionResult RemoveBooking(int bookingId)
        {
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                _repoBookingInfo.RemoveBooking(bookingId);

                message = $"Booking with ID {bookingId} removed successfully.";
                status = "200";

                _wrap.message = message;
                _wrap.status = status;

                return Ok(_wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while removing the booking. Message Error: " + ex.Message;
                status = "500";

                _wrap.message = message;
                _wrap.status = status;

                return BadRequest(_wrap);
            }
        }

        [HttpPut("updateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingInfo booking)
        {
            Wrapper _wrap = new Wrapper();
            string message = string.Empty;
            string status = string.Empty;

            try
            {
                await _repoBookingInfo.UpdateBooking(booking);

                message = $"Booking with ID {booking.BookingId} updated successfully.";
                status = "200";

                _wrap.message = message;
                _wrap.status = status;
                _wrap.data = new Dictionary<string, object>
                {
                    { "updatedBooking", booking }
                };

                return Ok(_wrap);
            }
            catch (NotFoundException nfEx)
            {
                message = nfEx.Message;
                status = "404";

                _wrap.message = message;
                _wrap.status = status;

                return NotFound(_wrap);
            }
            catch (Exception ex)
            {
                message = "An error occurred while updating the booking. Message Error: " + ex.Message;
                status = "500";

                _wrap.message = message;
                _wrap.status = status;

                return BadRequest(_wrap);
            }
        }















    }
}
