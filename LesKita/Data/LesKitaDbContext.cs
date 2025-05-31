using Microsoft.EntityFrameworkCore;

namespace LesKita;

public class LesKitaDbContext : DbContext
{
    public LesKitaDbContext(DbContextOptions options) : base(options) { }

    //Mentor
    public DbSet<T0Mentor> T0Mentor { get; set; }
    public DbSet<T1JadwalMentor> T1JadwalMentor { get; set; }

    public DbSet<T0Siswa> T0Siswa { get; set; }

    public DbSet<T1Order> T1Order { get; set; }
    public DbSet<T2Rating> T2Rating { get; set; }
    public DbSet<T2Jadwal> T2Jadwal { get; set; }
    public DbSet<T3Materi> T3Materi { get; set; }

    //Utilities
    public DbSet<T2User> T2User { get; set; }
    public DbSet<T6Pesan> T6Pesan { get; set; }
    public DbSet<T7Pesan_Anggota> T7Pesan_Anggota { get; set; }
    public DbSet<T7Pesan_Percakapan> T7Pesan_Percakapan { get; set; }
    public DbSet<T8Pesan_Terima> T8Pesan_Terima { get; set; }
    public DbSet<T9Notifikasi> T9Notifikasi { get; set; }
}
