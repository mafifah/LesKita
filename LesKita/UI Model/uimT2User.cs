namespace LesKita;

public class uimT2User : T2User
{
    public double? Rating { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Tarif { get; set; } = 0.0;
    public string? FormattedTarif {
        get => string.Format("{0:N0}", Tarif); // misalnya 1000000 jadi "1.000.000"
        set
        {
            if (decimal.TryParse(value.Replace(".", ""), out var result))
            {
                Tarif = (double)result;
            }
        }
    }
    public string? NoTelepon { get; set; }
    public string? JenisKelamin { get; set; }
    public string? Alamat { get; set; }
    public Guid? IdSiswa { get; set; }
    public Guid? IdMentor { get; set; }
    public List<T1JadwalMentor>? ListJadwal { get; set; } = new();
}
