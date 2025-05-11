namespace LesKita;

public class PesanSignalR
{
    public string Klien { get; set; }
    public string Divisi { get; set; }
    public object IdUser { get; set; }
    public object IdUser_Penerima { get; set; }
    public string? UserPengirim_NamaPanggilan { get; set; }
    public string? UserPenerima_NamaPanggilan { get; set; }
    public bool StatusBroadcast { get; set; }
    public Guid IdSignal { get; set; } // Sebelumnya KodeInstruksi
    public string NamaForm { get; set; }
    public string StatusAction { get; set; }
    public string NamaFieldPK { get; set; } // Ditambahkan baru
    public object NilaiPK { get; set; } // Sebelumnya PrimaryKey
    public DateTimeOffset WaktuProses { get; set; }
    public string IsiPesan { get; set; }
    public string ValueKolomUtama { get; set; }
    public string JenisPesan { get; set; }
    public string NamaFolderThumbnail { get; set; }
    public string NamaFileThumbnail { get; set; }
    public string TimeInfo { get; set; }

}
