﻿using Microsoft.EntityFrameworkCore;

namespace corewebappapi
{
    public class TicketItem
    {
        public long Id { get; set; }
        public string Concert { get; set; }
        public string Artist { get; set; }
        public bool Available { get; set; }
    }

    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }

        public DbSet<TicketItem> TicketItems { get; set; }
    }
}