using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Infrastructure.Seed
{
    public class PlatfromRulesSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Rules>()
                .HasData(
                    new Rules
                    {
                        Id = Guid.Parse("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                        Currency = CurrencyEnum.MAD,
                        OnOutOfStock = OnOutOfStockEnum.Available,
                        ShippingFees = 0,
                        WarrantyDays = 0
                    }
                );
        }
    }
}
