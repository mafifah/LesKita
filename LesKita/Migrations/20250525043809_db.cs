using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LesKita.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T0Mentor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TanggalLahir = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Usia = table.Column<int>(type: "int", nullable: true),
                    JenisKelamin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Tarif = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoTelepon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T0Mentor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T0Siswa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TanggalLahir = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Usia = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoTelepon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T0Siswa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T2User",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StatusNotifikasi = table.Column<bool>(type: "bit", nullable: false),
                    WaktuInsert = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T2User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "T6Pesan",
                columns: table => new
                {
                    IdPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusGrup = table.Column<bool>(type: "bit", nullable: false),
                    NamaGrup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconGrup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeskripsiGrup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaktuCreate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T6Pesan", x => x.IdPesan);
                });

            migrationBuilder.CreateTable(
                name: "T9Notifikasi",
                columns: table => new
                {
                    IdNotifikasi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser_Pengirim = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdUser_Penerima = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NilaiPK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsiPesan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JenisPesan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    WaktuProsesNotifikasi = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WaktuProsesData = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserPengirim_NamaPanggilan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPenerima_NamaPanggilan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusSistem = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T9Notifikasi", x => x.IdNotifikasi);
                });

            migrationBuilder.CreateTable(
                name: "T1JadwalMentor",
                columns: table => new
                {
                    IdJadwalMentor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMentor = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Hari = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JamMulai = table.Column<TimeSpan>(type: "time", nullable: true),
                    JamSelesai = table.Column<TimeSpan>(type: "time", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    T0MentorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T1JadwalMentor", x => x.IdJadwalMentor);
                    table.ForeignKey(
                        name: "FK_T1JadwalMentor_T0Mentor_T0MentorId",
                        column: x => x.T0MentorId,
                        principalTable: "T0Mentor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T1Order",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSiswa = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdMentor = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JumlahSesi = table.Column<int>(type: "int", nullable: true),
                    Harga = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    TanggalPemesanan = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Siswa_Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Siswa_TanggalLahir = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Siswa_Usia = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Siswa_Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Siswa_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Siswa_NoTelepon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mentor_Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mentor_TanggalLahir = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Mentor_Usia = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Mentor_Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mentor_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mentor_NoTelepon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsAktif = table.Column<bool>(type: "bit", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T1Order", x => x.IdOrder);
                    table.ForeignKey(
                        name: "FK_T1Order_T0Mentor_IdMentor",
                        column: x => x.IdMentor,
                        principalTable: "T0Mentor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T1Order_T0Siswa_IdSiswa",
                        column: x => x.IdSiswa,
                        principalTable: "T0Siswa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T7Pesan_Anggota",
                columns: table => new
                {
                    IdDetilPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdUser_Anggota = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusCreator = table.Column<bool>(type: "bit", nullable: false),
                    StatusAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    StatusArchive = table.Column<bool>(type: "bit", nullable: false),
                    StatusMute = table.Column<bool>(type: "bit", nullable: false),
                    StatusPin = table.Column<bool>(type: "bit", nullable: false),
                    WaktuJoin = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WaktuLeave = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T7Pesan_Anggota", x => x.IdDetilPesan);
                    table.ForeignKey(
                        name: "FK_T7Pesan_Anggota_T2User_IdUser_Anggota",
                        column: x => x.IdUser_Anggota,
                        principalTable: "T2User",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_T7Pesan_Anggota_T6Pesan_IdPesan",
                        column: x => x.IdPesan,
                        principalTable: "T6Pesan",
                        principalColumn: "IdPesan");
                });

            migrationBuilder.CreateTable(
                name: "T7Pesan_Percakapan",
                columns: table => new
                {
                    IdDetilPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdDetilPesan_Terkutip = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdUser_Pengirim = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsiPesan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDraft = table.Column<bool>(type: "bit", nullable: false),
                    StatusPin = table.Column<bool>(type: "bit", nullable: false),
                    StatusStar = table.Column<bool>(type: "bit", nullable: false),
                    StatusSystem = table.Column<bool>(type: "bit", nullable: false),
                    PesanTerkutip_IdUserPengirim = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PesanTerkutip_IsiPesan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PesanTerkutip_Durasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JenisAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPreview_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPreview_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPreview_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPreview_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaktuSend = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WaktuInsert = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T7Pesan_Percakapan", x => x.IdDetilPesan);
                    table.ForeignKey(
                        name: "FK_T7Pesan_Percakapan_T2User_IdUser_Pengirim",
                        column: x => x.IdUser_Pengirim,
                        principalTable: "T2User",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_T7Pesan_Percakapan_T6Pesan_IdPesan",
                        column: x => x.IdPesan,
                        principalTable: "T6Pesan",
                        principalColumn: "IdPesan");
                });

            migrationBuilder.CreateTable(
                name: "T2Jadwal",
                columns: table => new
                {
                    IdJadwal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tanggal = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    JamMulai = table.Column<TimeSpan>(type: "time", nullable: true),
                    JamSelesai = table.Column<TimeSpan>(type: "time", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T2Jadwal", x => x.IdJadwal);
                    table.ForeignKey(
                        name: "FK_T2Jadwal_T1Order_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "T1Order",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateTable(
                name: "T2Rating",
                columns: table => new
                {
                    IdRating = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Skor = table.Column<int>(type: "int", nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T2Rating", x => x.IdRating);
                    table.ForeignKey(
                        name: "FK_T2Rating_T1Order_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "T1Order",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateTable(
                name: "T8Pesan_Terima",
                columns: table => new
                {
                    IdDetilDetilPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDetilPesan = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdUser_Penerima = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusDeleted = table.Column<bool>(type: "bit", nullable: false),
                    WaktuDelivered = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    WaktuRead = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T8Pesan_Terima", x => x.IdDetilDetilPesan);
                    table.ForeignKey(
                        name: "FK_T8Pesan_Terima_T2User_IdUser_Penerima",
                        column: x => x.IdUser_Penerima,
                        principalTable: "T2User",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_T8Pesan_Terima_T7Pesan_Percakapan_IdDetilPesan",
                        column: x => x.IdDetilPesan,
                        principalTable: "T7Pesan_Percakapan",
                        principalColumn: "IdDetilPesan");
                });

            migrationBuilder.CreateTable(
                name: "T3Materi",
                columns: table => new
                {
                    IdMateri = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdJadwal = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deskripsi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T3Materi", x => x.IdMateri);
                    table.ForeignKey(
                        name: "FK_T3Materi_T2Jadwal_IdJadwal",
                        column: x => x.IdJadwal,
                        principalTable: "T2Jadwal",
                        principalColumn: "IdJadwal");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T1JadwalMentor_T0MentorId",
                table: "T1JadwalMentor",
                column: "T0MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_T1Order_IdMentor",
                table: "T1Order",
                column: "IdMentor");

            migrationBuilder.CreateIndex(
                name: "IX_T1Order_IdSiswa",
                table: "T1Order",
                column: "IdSiswa");

            migrationBuilder.CreateIndex(
                name: "IX_T2Jadwal_IdOrder",
                table: "T2Jadwal",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_T2Rating_IdOrder",
                table: "T2Rating",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_T3Materi_IdJadwal",
                table: "T3Materi",
                column: "IdJadwal");

            migrationBuilder.CreateIndex(
                name: "IX_T7Pesan_Anggota_IdPesan",
                table: "T7Pesan_Anggota",
                column: "IdPesan");

            migrationBuilder.CreateIndex(
                name: "IX_T7Pesan_Anggota_IdUser_Anggota",
                table: "T7Pesan_Anggota",
                column: "IdUser_Anggota");

            migrationBuilder.CreateIndex(
                name: "IX_T7Pesan_Percakapan_IdPesan",
                table: "T7Pesan_Percakapan",
                column: "IdPesan");

            migrationBuilder.CreateIndex(
                name: "IX_T7Pesan_Percakapan_IdUser_Pengirim",
                table: "T7Pesan_Percakapan",
                column: "IdUser_Pengirim");

            migrationBuilder.CreateIndex(
                name: "IX_T8Pesan_Terima_IdDetilPesan",
                table: "T8Pesan_Terima",
                column: "IdDetilPesan");

            migrationBuilder.CreateIndex(
                name: "IX_T8Pesan_Terima_IdUser_Penerima",
                table: "T8Pesan_Terima",
                column: "IdUser_Penerima");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T1JadwalMentor");

            migrationBuilder.DropTable(
                name: "T2Rating");

            migrationBuilder.DropTable(
                name: "T3Materi");

            migrationBuilder.DropTable(
                name: "T7Pesan_Anggota");

            migrationBuilder.DropTable(
                name: "T8Pesan_Terima");

            migrationBuilder.DropTable(
                name: "T9Notifikasi");

            migrationBuilder.DropTable(
                name: "T2Jadwal");

            migrationBuilder.DropTable(
                name: "T7Pesan_Percakapan");

            migrationBuilder.DropTable(
                name: "T1Order");

            migrationBuilder.DropTable(
                name: "T2User");

            migrationBuilder.DropTable(
                name: "T6Pesan");

            migrationBuilder.DropTable(
                name: "T0Mentor");

            migrationBuilder.DropTable(
                name: "T0Siswa");
        }
    }
}
