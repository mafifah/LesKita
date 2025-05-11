namespace LesKita.Model;

public class T6Pesan
{
    [Key]
    [PrimaryKey]
    public Guid IdPesan { get; set; } = Guid.NewGuid();
    public bool StatusGrup { get; set; } = false;
    public string? NamaGrup { get; set; }
    public string? IconGrup { get; set; }
    public string? DeskripsiGrup { get; set; }
    public DateTimeOffset? WaktuCreate { get; set; }

    public ICollection<T7Pesan_Anggota>? ListT7Pesan_Anggota { get; set; }
    public ICollection<T7Pesan_Percakapan>? ListT7Pesan_Percakapan { get; set; }
}
