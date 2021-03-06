﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PxTransform.Auto.Data.Domain.Accretive;

namespace PxTransform.Auto.Data.Data
{
    public class AccretiveContext : DbContext
    {

        public AccretiveContext(DbContextOptions<AccretiveContext> options) : base(options)
        {
        }

        
        public DbSet<Location> Locations { get; set; }
        public virtual DbSet<HIPAAConnector> HIPAAConnectors { get; set; }
        public virtual DbSet<HIPAAPayorConnector> HIPAAPayorConnectors { get; set; }
        public virtual DbSet<Payor> Payors { get; set; }
    }
}
