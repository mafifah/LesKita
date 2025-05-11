using System.ComponentModel.DataAnnotations.Schema;

namespace LesKita.Model;

public class T7Pesan_Anggota
{
    [Key]
    [PrimaryKey]
    public Guid IdDetilPesan { get; set; } = Guid.NewGuid();
    public Guid? IdPesan { get; set; }
    public Guid? IdUser_Anggota { get; set; }
    public bool StatusCreator { get; set; } = false;
    public bool StatusAdministrator { get; set; } = false;
    public bool StatusArchive { get; set; } = false;
    public bool StatusMute { get; set; } = false;
    public bool StatusPin { get; set; } = false;
    public DateTimeOffset? WaktuJoin { get; set; }
    public DateTimeOffset? WaktuLeave { get; set; }

    [ForeignKey(nameof(T7Pesan_Anggota.IdPesan))]
    public T6Pesan? T6Pesan { get; set; }

    [ForeignKey(nameof(T7Pesan_Anggota.IdUser_Anggota))]
    public T2User? T2User { get; set; }
}
