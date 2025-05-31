using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T2Rating
{
    [Key]
    [PrimaryKey]
    public Guid IdRating { get; set; } = Guid.NewGuid();
    public Guid? IdOrder { get; set; }
    public int? Skor { get; set; } // 1–5
    public string? Komentar { get; set; }

    [ForeignKey("IdOrder")]
    public T1Order? T1Order { get; set; }
}
