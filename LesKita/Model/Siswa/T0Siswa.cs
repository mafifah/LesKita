namespace LesKita.Model;

public class T0Siswa
{
    [Key]
    [PrimaryKey]
    public Guid Id { get; set; } = NewId.NextGuid();
    public Guid? IdUser { get; set; }
    public string? Nama { get; set; }
    public DateTimeOffset? TanggalLahir { get; set; }
    public DateTimeOffset? Usia { get; set; }
    public string? Alamat { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public List<T1Order>? ListT1Order { get; set; }

}
