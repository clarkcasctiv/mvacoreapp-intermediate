﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace corewebappapi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[ApiController]

    public class TicketController : ControllerBase
    {
        private TicketContext _context;

        public TicketController(TicketContext context)
        {
            _context = context;

            if (_context.TicketItems.Count() == 0)
            {
                _context.TicketItems.Add(new TicketItem
                {
                    Concert = "Green Day Concert"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return new ObjectResult(ticket);
        }

        [HttpPost]
        public IActionResult Create([FromBody]TicketItem ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            _context.Add(ticket);
            _context.SaveChanges();

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TicketItem ticket)
        {
            if (ticket == null || ticket.Id != id)
            {
                return BadRequest();
            }

            var tic = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (tic == null)
            {
                return NotFound();
            }

            tic.Concert = ticket.Concert;
            tic.Artist = ticket.Artist;
            tic.Available = ticket.Available;

            _context.TicketItems.Update(tic);

            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.TicketItems.Remove(ticket);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}