using Google.Apis.Drive.v3.Data;

namespace LesKita;

public class uimT1JadwalMentor : T1JadwalMentor
{
    public string StatusAction { get; set; }
    public DateTime? JamMulaiDateTime
    {
        get => JamMulai.HasValue ? DateTime.Today.Add(JamMulai.Value) : null;
        set => JamMulai = value?.TimeOfDay;
    }

    public DateTime? JamSelesaiDateTime
    {
        get => JamSelesai.HasValue ? DateTime.Today.Add(JamSelesai.Value) : null;
        set => JamSelesai = value?.TimeOfDay;
    }
}
