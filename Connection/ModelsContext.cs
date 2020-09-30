using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Nexus_Service_Marketing_System.Models;

namespace Nexus_Service_Marketing_System.Connection
{
    public class ModelsContext : DbContext
    {
        public DbSet<accounts> accounts { set; get; }
        public DbSet<connections> connections { set; get; }
        public DbSet<plans> plans { set; get; }
        public DbSet<plans_orders> plans_orders { set; get; }
        public DbSet<equipments> equipments { set; get; }
        public DbSet<equipments_orders> equipments_orders { set; get; }
        public DbSet<orders> orders { set; get; }
        public DbSet<bills> bills { set; get; }
        public DbSet<tracks> tracks { set; get; }
        public DbSet<feedbacks> feedbacks { set; get; }
        public DbSet<replies> replies { set; get; }
        public DbSet<retailShowrooms> retailShowrooms { set; get; }
        public DbSet<vendors> vendors { set; get; }
        public DbSet<cities> cities { set; get; }        

        /*
         * Database Information
         * Database Used: SQLServer
         * Database Name: NexusDatabase
         * Server: .
         * Trusted Connection: True
         */        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("server=.; database=NexusDB; trusted_connection=true; MultipleActiveResultSets=True");
        }        
    }
}
