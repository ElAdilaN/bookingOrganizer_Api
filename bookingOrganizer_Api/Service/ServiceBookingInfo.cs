﻿using AutoMapper;
using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Repository;
using bookingOrganizer_Api.UTILS;

namespace bookingOrganizer_Api.Service
{
    public class ServiceBookingInfo  : RepoBookingInfo
    {

        private readonly IDAOBookingInfo _daoBookingInfo;
        private readonly IMapper _mapper; 
        public ServiceBookingInfo(IDAOBookingInfo daoBookingInfo , IMapper mapper)
        {
            _daoBookingInfo = daoBookingInfo;
            _mapper = mapper;
        }


        public DTOBookingInfo getBookingInfoById(int id)
        {
            try
            {
                //*  THIS ONE ALSO CAN BE USED *//
               //return UTILSBookingInfo.ConvertBookingToDTOBooking(_daoBookingInfo.GetBookingInfoById(id));

                var booking = _daoBookingInfo.GetBookingInfoById(id);
                return _mapper.Map<DTOBookingInfo>(booking);

            }
            catch (NotFoundException)
            {
                throw  ;
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error retrieving booking by ID.", ex);
            }
        }

        public ICollection<DTOBookingInfo> getBookings(
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
            try
            {
                return UTILSBookingInfo.ConvertBookingsToDTOBookings(_daoBookingInfo.GetBookings(bookingId, groupId, typeBookingId, date, startDate, endDate, address, purchaseMethod, seatNumber, notes));
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error retrieving bookings.", ex);
            }
        }
        public void AddBooking(DTOBookingInfo booking)
        {
            try
            {
                BookingInfo bookingInfo = UTILSBookingInfo.ConvertDTOBookingToBooking(booking);
                _daoBookingInfo.AddBooking(bookingInfo);
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error adding booking.", ex);
            }
        }

        public void RemoveBooking(int bookingId)
        {
            try
            {
                _daoBookingInfo.RemoveBooking(bookingId);
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Booking with ID {bookingId} not found.");
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error removing booking.", ex);
            }
        }

        public async Task UpdateBooking(DTOBookingInfo dtoBooking)
        {
            try
            {
                BookingInfo bookingInfo = UTILSBookingInfo.ConvertDTOBookingToBooking(dtoBooking);
                await _daoBookingInfo.UpdateBooking(bookingInfo);
            }
            catch (KeyNotFoundException) // assuming DAO throws this if booking doesn't exist
            {
                throw new NotFoundException($"Booking with ID {dtoBooking.BookingId } not found.");
            }
            catch (Exception ex)
            {
                throw new BookingServiceException("Error updating booking.", ex);
            }
        }

    }
}
