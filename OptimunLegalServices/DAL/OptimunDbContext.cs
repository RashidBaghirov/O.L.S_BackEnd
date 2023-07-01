﻿using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.DAL
{
    public class OptimunDbContext:DbContext
    {
        public OptimunDbContext(DbContextOptions<OptimunDbContext> options):base(options)
        {

        }

       public DbSet<PracticeArea> PracticeAreas { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Setting> Settings { get; set; }




    }
}
