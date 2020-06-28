using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NfcVehicleParkingAPi.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
              : base(options)
        {
        }

        public DbSet<AppUser>  appUsers { get; set; }
        public DbSet<AppRole>  appRoles { get; set; }
        public DbSet<User>  users { get; set; }
        public DbSet<Parking> parkings { get; set; }
        public DbSet<Slot>  slots { get; set; }
        public DbSet<SlotReservation>  slotReservations { get; set; }
        public DbSet<Payment>  payments { get; set; }
        public DbSet<ParkingReview>  parkingReviews { get; set; }
        public DbSet<ParkingImages>  ParkingImages { get; set; }
        public DbSet<ParkingFacilities>  ParkingFacilities { get; set; }
        public DbSet<CityMapLocation>  cityMapLocations { get; set; }
        public DbSet<ParkingGoogleMap>  parkingGoogleMaps { get; set; }

    }
}
