using LesKita.Model;

namespace LesKita;

public class uimT9Notifikasi : T9Notifikasi
{
    public string StatusAction { get; set; }
    public string TimeInfo { get; set; }
    public string LinkUrl { get; set; }
    public bool IsBeingRemoved { get; set; } = false;
}
