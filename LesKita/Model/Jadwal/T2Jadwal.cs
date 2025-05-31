using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

namespace LesKita.Model;

public class T2Jadwal
{
    [Key]
    [PrimaryKey]
    public Guid IdJadwal { get; set; } = NewId.NextGuid();
    public Guid? IdOrder { get; set; }
    public DateTimeOffset? Tanggal { get; set; }
    [Column(TypeName = "time")]
    public TimeSpan? JamMulai { get; set; }
    [Column(TypeName = "time")]
    public TimeSpan? JamSelesai { get; set; }
    public bool Status { get; set; }
    public string Deskripsi { get; set; }
    public List<T3Materi>? ListT3Materi { get; set; }
    [ForeignKey("IdOrder")]
    public T1Order? T1Order { get; set; }
}
