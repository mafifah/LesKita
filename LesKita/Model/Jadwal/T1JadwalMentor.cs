using ColumnAttribute = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

namespace LesKita.Model;

public class T1JadwalMentor
{
    [Key]
    [PrimaryKey]
    public Guid IdJadwalMentor { get; set; } = NewId.NextGuid();
    public Guid? IdMentor { get; set; }
    public string? Hari { get; set; }
    [Column(TypeName = "time")]
    public TimeSpan? JamMulai { get; set; }
    [Column(TypeName = "time")]
    public TimeSpan? JamSelesai { get; set; }
    public bool Status { get; set; }
}
