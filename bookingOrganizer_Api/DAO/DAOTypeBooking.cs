﻿using bookingOrganizer_Api.DTO;
using bookingOrganizer_Api.Exceptions;
using bookingOrganizer_Api.IDAO;
using bookingOrganizer_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace bookingOrganizer_Api.DAO
{
    public class DAOTypeBooking : IDAOTypeBooking
    {
        private readonly BookingContext _context;
        public DAOTypeBooking(BookingContext context)
        {
            _context = context;
        }
        public ICollection<TypeBooking> getAllTypeBookings()
        {
            try
            {
                return _context.TypeBookings.ToList();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to retrieve all booking types. Error: " + ex.Message, ex);
            }
        }

        public TypeBooking getTypeBookingById(int id)
        {
            try
            {
                TypeBooking tb = _context.TypeBookings.Where(t => t.TypeBookingId == id).FirstOrDefault();
                return tb;

            }
            catch (Exception ex)
            {
                throw new DAOException($"Failed to retrieve Booking type with id {id}. Error Message : ", ex);
            }
        }

        public async Task addTypeBooking(TypeBooking typeBooking)
        {
            try
            {
                _context.TypeBookings.Add(typeBooking);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to add booking type ", ex);
            }
        }

        public async Task  RemoveTypeBooking(int id)
        {
            try
            {

                var typeBooking = _context.TypeBookings.FirstOrDefault(b => b.TypeBookingId == id);
                if (typeBooking == null)
                    throw new NotFoundException($"Booking Type with ID {id} was not found.");

                _context.TypeBookings.Remove(typeBooking);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DAOException("Failed to Remove  booking type ", ex);
            }
        }

        public async Task UpdateBooking(TypeBooking bookingType)
        {
            try
            {


                var existingBookingType = await _context.TypeBookings.FirstOrDefaultAsync(b => b.TypeBookingId == bookingType.TypeBookingId);

                if (existingBookingType == null)
                    throw new NotFoundException($"Booking Type  with Id {bookingType.TypeBookingId} not found.");

                // Update fields
                existingBookingType.TypeName = bookingType.TypeName;


                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw new DAOException("Failed To Update Booking ", ex);
            }
        }

    }
}
