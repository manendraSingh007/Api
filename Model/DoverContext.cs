using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Model
{
    
        public class DoverContext : DbContext
        {
            public DoverContext(DbContextOptions<DoverContext> options)
                : base(options)
            {
            }

            //public DbSet<OutputParameter> OutputParameter { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OutputParameter>(en=>en.HasNoKey());
               


        //}
        
    }
}
