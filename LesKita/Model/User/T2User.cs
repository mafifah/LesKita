namespace LesKita.Model;

public class T2User
{
    [Key]
    [PrimaryKey]
    public Guid IdUser { get; set; } = NewId.NextGuid();
    public string? Role { get; set; } // "Siswa" atau "Mentor"
    public string? Nama { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

}
