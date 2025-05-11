namespace LesKita;

public class uimT2Jadwal : T2Jadwal
{
    public string? Siswa_Nama { get; set; }
    public DateTimeOffset? Siswa_TanggalLahir { get; set; }
    public DateTimeOffset? Siswa_Usia { get; set; }
    public string? Siswa_Alamat { get; set; }
    public string? Siswa_Email { get; set; }

    public string? Mentor_Nama { get; set; }
    public DateTimeOffset? Mentor_TanggalLahir { get; set; }
    public DateTimeOffset? Mentor_Usia { get; set; }
    public string? Mentor_Alamat { get; set; }
    public string? Mentor_Email { get; set; }
    public string WarnaKode { get; set; }
}
