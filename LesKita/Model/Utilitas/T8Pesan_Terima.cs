using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T8Pesan_Terima
{
    [Key]
    [PrimaryKey]
    public Guid IdDetilDetilPesan { get; set; } = Guid.NewGuid();
    public Guid? IdDetilPesan { get; set; }
    public Guid? IdUser_Penerima { get; set; }
    public bool StatusDeleted { get; set; } = false;
    public DateTimeOffset? WaktuDelivered { get; set; }
    public DateTimeOffset? WaktuRead { get; set; }

    [ForeignKey(nameof(T8Pesan_Terima.IdDetilPesan))]
    public T7Pesan_Percakapan? T7Pesan_Percakapan { get; set; }

    [ForeignKey(nameof(T8Pesan_Terima.IdUser_Penerima))]
    public T2User? T2User { get; set; }
}
