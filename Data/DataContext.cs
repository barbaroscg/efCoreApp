using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<KursKayit> KursKayitlari { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }

        // public DbSet<Kurs> Kurslar => Set<Kurs>();
        // public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        // public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
    }
}