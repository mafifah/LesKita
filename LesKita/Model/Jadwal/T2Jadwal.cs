using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T2Jadwal
{
    [Key]
    [PrimaryKey]
    public Guid IdJadwal { get; set; } = NewId.NextGuid();
    public Guid? IdOrder { get; set; }
    public DateTimeOffset? TanggalPertemuan { get; set; }
    public bool Status { get; set; }

    public List<T3Materi>? ListT3Materi { get; set; }
    [ForeignKey("IdOrder")]
    public T1Order? T1Order { get; set; }
}
