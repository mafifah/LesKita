using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T1Order
{
    [Key]
    [PrimaryKey]
    public Guid IdOrder { get; set; } = NewId.NextGuid();
    public Guid? IdSiswa { get; set; }
    public Guid? IdMentor { get; set; }
    public int? JumlahSesi { get; set; }
    public double? Harga { get; set; }
    public double? Total { get; set; }
    public DateTimeOffset? TanggalPemesanan { get; set; }
    public string? Siswa_Nama { get; set; }
    public DateTimeOffset? Siswa_TanggalLahir { get; set; }
    public DateTimeOffset? Siswa_Usia { get; set; }
    public string? Siswa_Alamat { get; set; }
    public string? Siswa_Email { get; set; }
    public string? Siswa_NoTelepon { get; set; }

    public string? Mentor_Nama { get; set; }
    public DateTimeOffset? Mentor_TanggalLahir { get; set; }
    public DateTimeOffset? Mentor_Usia { get; set; }
    public string? Mentor_Alamat { get; set; }
    public string? Mentor_Email { get; set; }
    public string? Mentor_NoTelepon { get; set; }
    public bool IsPaid { get; set; } = false;
    public bool IsAktif { get; set; } = false;
    public bool IsCancel { get; set; } = false;
    public bool IsDone { get; set; } = false;

    [ForeignKey("IdSiswa")]
    public T0Siswa? T0Siswa { get; set; }
    [ForeignKey("IdMentor")]
    public T0Mentor T0Mentor { get; set; }
    public List<T2Jadwal>? ListT2Jadwal { get; set; }


}
