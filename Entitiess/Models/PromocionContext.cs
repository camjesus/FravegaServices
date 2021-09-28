using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FravegaService.Models
{

        public partial class PromocionContext : DbContext
        {
            public PromocionContext()
            {
            }

            public PromocionContext(DbContextOptions<PromocionContext> options)
                : base(options)
            {
            }

            public virtual DbSet<Promocion> Promocion { get; set; }

        }
    }
