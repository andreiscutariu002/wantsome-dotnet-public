﻿namespace Hotels.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Resources;
    using Services;

    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly INotificationService notificationService;

        public HotelsController(ApiDbContext context, INotificationService notificationService)
        {
            this.context = context;
            this.notificationService = notificationService;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelResource>>> GetTodoItems()
        {
            var hotels = await this.context.Hotels.ToListAsync();
            return this.Ok(hotels.Select(x => x.MapToResource()));
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelResource>> GetHotel(int id)
        {
            var hotel = await this.context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return this.NotFound();
            }

            return hotel.MapToResource();
        }

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, HotelResource hotel)
        {
            if (id < 0)
            {
                throw new ArgumentException("Negative ID");
            }

            var entity = await this.context.Hotels.FindAsync(id);

            entity.City = hotel.City;
            entity.Name = hotel.Name;

            this.context.Entry(entity).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.HotelExists(id)) return this.NotFound();
                throw;
            }

            return this.NoContent();
        }

        // POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<HotelResource>> PostHotel(HotelResource hotel)
        {
            var entity = hotel.MapToEntity();
            this.context.Hotels.Add(entity);

            await this.context.SaveChangesAsync();

            this.notificationService.Notify("message");

            return this.CreatedAtAction("GetHotel", new { id = hotel.Id }, entity.MapToResource());
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteTodoItem(int id)
        {
            var hotel = await this.context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            this.context.Hotels.Remove(hotel);
            await this.context.SaveChangesAsync();
            return hotel;
        }

        private bool HotelExists(int id)
        {
            return this.context.Hotels.Any(e => e.Id == id);
        }
    }
}
