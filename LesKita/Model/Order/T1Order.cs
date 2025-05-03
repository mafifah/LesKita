using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T1Order
{
    [Key]
    [PrimaryKey]
    public Guid IdOrder { get; set; } = NewId.NextGuid();
    public Guid? IdSiswa { get; set; }
    public Guid? IdGuru { get; set; }
    public int? JumlahSesi { get; set; }
    public DateTimeOffset? TanggalPemesanan { get; set; }
    public bool IsPaid { get; set; }

    public string? Siswa_Nama { get; set; }
    public DateTimeOffset? Siswa_TanggalLahir { get; set; }
    public DateTimeOffset? Siswa_Usia { get; set; }
    public string? Siswa_Alamat { get; set; }

    public string? Guru_Nama { get; set; }
    public DateTimeOffset? GUru_TanggalLahir { get; set; }
    public DateTimeOffset? Guru_Usia { get; set; }
    public string? Guru_Alamat { get; set; }

    [ForeignKey("IdSiswa")]
    public T0Siswa? T0Siswa { get; set; }
    [ForeignKey("IdGuru")]
    public T0Guru T0Guru { get; set; }
    public List<T2Jadwal>? ListT2Jadwal { get; set; }


}
