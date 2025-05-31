namespace LesKita;

public class uimT2Jadwal : T2Jadwal
{
    public string? Siswa_Nama => T1Order?.Siswa_Nama;
    public DateTimeOffset? Siswa_TanggalLahir => T1Order?.Siswa_TanggalLahir;
    public DateTimeOffset? Siswa_Usia => T1Order?.Siswa_Usia;
    public string? Siswa_Alamat => T1Order?.Siswa_Alamat;
    public string? Siswa_Email => T1Order?.Siswa_Email;

    public string? Mentor_Nama => T1Order?.Mentor_Nama;
    public DateTimeOffset? Mentor_TanggalLahir => T1Order?.Mentor_TanggalLahir;
    public DateTimeOffset? Mentor_Usia => T1Order?.Mentor_Usia;
    public string? Mentor_Alamat => T1Order?.Mentor_Alamat;
    public string? Mentor_Email => T1Order?.Mentor_Email;
    public string WarnaKode { get; set; }
}
