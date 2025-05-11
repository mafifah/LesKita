global using LesKita.Model;

namespace LesKita;

public class uimT6Pesan : T6Pesan
{
    public string FullName { get; set; }
    public string Notes { get; set; }
    public string ImageFileName { get; set; }
    public string StatusMessage { get; set; }
    public string TotalUnreadMessage { get; set; }
    public bool IsOnline { get; set; }
    public string? JenisPesan { get; set; }
    public bool? StatusView { get; set; }
    public bool IsMine { get; set; }
    public int TotalChat { get; set; }
    public DateTimeOffset? WaktuProses { get; set; }
    public List<uimT7Pesan_Percakapan> DtT7Pesan_Percakapan { get; set; }
    public string Kategori { get; set; } = "Chat";
    public string Durasi { get; set; } = "";
}
