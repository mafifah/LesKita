global using System.ComponentModel.DataAnnotations;
global using SQLite;
global using MassTransit;

namespace LesKita.Model;

public class T0Mentor
{
    [Key]
    [PrimaryKey]
    public Guid Id { get; set; } = NewId.NextGuid();
    public Guid? IdUser { get; set; }
    public string? Nama { get; set; }
    public DateTimeOffset? TanggalLahir { get; set; }
    public int? Usia { get; set; }
    public string? JenisKelamin { get; set; }
    public string? Alamat { get; set; }
    public double? Rating { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Tarif { get; set; }
    public string? Email { get; set; }
    public string? NoTelepon { get; set; }

    //public Pengguna Pengguna { get; set; }
    public List<T1Order>? ListT1Order { get; set; }
    public List<T1JadwalMentor>? ListT1JadwalMentor { get; set; }
}
