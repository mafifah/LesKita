using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T3Materi
{
    [Key]
    [PrimaryKey]
    public Guid IdMateri { get; set; } = NewId.NextGuid();
    public Guid? IdJadwal { get; set; }
    public string? UrlFile { get; set; }
    public string? Deskripsi { get; set; }

    [ForeignKey("IdJadwal")]
    public T2Jadwal? T2Jadwal { get; set; }
}
