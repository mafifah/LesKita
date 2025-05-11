namespace LesKita
{
    public class uimT7Pesan_Percakapan : T7Pesan_Percakapan
    {
        public bool IsMine { get; set; } = false;
        public bool IsRead { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        public string? NamaDokumen { get; set; }
        public string? SizeDokumen { get; set; }
        public DateTimeOffset? WaktuProses { get; set; }
        public string TanggalPesan { get; set; }
    }
}
