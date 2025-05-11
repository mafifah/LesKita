using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T7Pesan_Percakapan
{
    [Key]
    [PrimaryKey]
    public Guid IdDetilPesan { get; set; } = Guid.NewGuid();
    public Guid? IdPesan { get; set; }
    public Guid? IdDetilPesan_Terkutip { get; set; }
    public Guid? IdUser_Pengirim { get; set; }
    public string? IsiPesan { get; set; }
    public bool StatusDraft { get; set; } = false;
    public bool StatusPin { get; set; } = false;
    public bool StatusStar { get; set; } = false;
    public bool StatusSystem { get; set; } = false;
    public Guid? PesanTerkutip_IdUserPengirim { get; set; }
    public string? PesanTerkutip_IsiPesan { get; set; }
    public string? PesanTerkutip_Durasi { get; set; }
    public string? JenisAttachment { get; set; }
    public string? Durasi { get; set; }
    public string? LinkPreview_Title { get; set; }
    public string? LinkPreview_Description { get; set; }
    public string? LinkPreview_ImageUrl { get; set; }
    public string? LinkPreview_Url { get; set; }



    public DateTimeOffset? WaktuSend { get; set; }
    public DateTimeOffset? WaktuInsert { get; set; }

    [ForeignKey(nameof(T7Pesan_Percakapan.IdPesan))]
    public T6Pesan? T6Pesan { get; set; }

    [ForeignKey(nameof(T7Pesan_Percakapan.IdUser_Pengirim))]
    public T2User? T2User { get; set; }

    public ICollection<T8Pesan_Terima>? ListT8Pesan_Terima { get; set; }
}
