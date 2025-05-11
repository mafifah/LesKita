namespace LesKita.Model;

public class T9Notifikasi
{

    [Key]
    [PrimaryKey]
    public Guid IdNotifikasi { get; set; } = Guid.NewGuid();
    public Guid? IdUser_Pengirim { get; set; }
    public Guid? IdUser_Penerima { get; set; }
    public string? NilaiPK { get; set; } // Sebelumnya PrimaryKey
    public string? IsiPesan { get; set; }
    public string? JenisPesan { get; set; }
    public bool? IsRead { get; set; }
    public DateTimeOffset? WaktuProsesNotifikasi { get; set; }
    public DateTimeOffset? WaktuProsesData { get; set; }
    public string? UserPengirim_NamaPanggilan { get; set; }
    public string? UserPenerima_NamaPanggilan { get; set; }
    public string? Form { get; set; }
    public bool StatusSistem { get; set; } = false;
}
