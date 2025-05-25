namespace LesKita.Components.Pages;

using DevExpress.Blazor;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using Contact = LesKita.Contact;

public partial class RcpChat
{
    [Inject]
    public IJSRuntime JSR { get; set; }
    [CascadingParameter]
    public RcpUtama? RcpUtama { get; set; }
    public Guid IdUser { get; set; }
    [Parameter]
    public bool TampilkanListMessageSection { get; set; } = true;
    [Parameter]
    public EventCallback TampilkanMessageSection_StateChanged { get; set; }
    [Parameter]
    public EventCallback TampilkanListMessageSection_StateChanged { get; set; }


    public bool TampilkanDropdownDismissAllChat { get; set; } = false;
    public bool TampilkanChatDropdown { get; set; } = false;
    public bool TampilkanAttachDropdown { get; set; } = false;

    private string messageUrl = "Message";

    public string DisplayPersonalMessageSection { get => TampilkanPersonalMessageSection ? "flex" : "none"; }

    public bool TampilkanPersonalMessageSection = false;

    public List<Contact> DtContact = new();
    public List<Contact> DtContact_Ditampilkan = new();
    public List<Contact> DtRecentContact = new();
    public List<uimT6Pesan> DtPesan = new();
    public List<uimT6Pesan> DtPesan_Ditampilkan = new();
    public List<uimT7Pesan_Anggota> DtAnggota = new();
    public List<uimT7Pesan_Anggota> DtAnggota_GrupBaru = new();
    public List<uimT7Pesan_Percakapan> DtPercakapan = new();

    public uimT6Pesan DtPesanAktif = new();
    public uimT7Pesan_Percakapan PercakapanDipilih = new();
    uimT7Pesan_Percakapan PesanDikutip { get; set; } = null;
    LinkPreviewModel DrLinkPreview { get; set; } = null;
    Guid? IdDetilPesanAktif { get; set; }
    Guid? IdPesanDihover { get; set; }

    public List<uimT8Pesan_Terima> DtStatusPesan { get; set; } = new();
    public DxMemo MessageBox { get; set; }
    public DxTextBox TxbSearchContact1 { get; set; }
    public DxTextBox TxbSearchContact2 { get; set; }
    public DxTextBox TxbBoxNamaGrup { get; set; }

    string[] colorPalette = { "crimson", "darkcyan", "chocolate", "coral", "cornflowerblue", "blueviolet", "brown", "cadetblue" };

    private bool _firstRender = true;
    private bool _selesaiInisialisasi = false;


    private async Task Inisialisasi()
    {
        _selesaiInisialisasi = true;
        //IdUser = RcpUtama.IdUser;
        DotNet = DotNetObjectReference.Create(this);
        //DtContact = (await _svcPesan.GetContact()).Adapt<List<Contact>>();
        //DtPesan = (await _svcPesan.GetDataPesan(IdUser)).OrderByDescending(x => x.WaktuProses).ToList();
        for (int i = 1; i <= 3; i++)
        {
            var idPesan = Guid.NewGuid();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            DtContact.Add(new Contact
            {
                IdUser = user1,
                NamaPanggilan = $"User {i}A",
                UrlProfile = null,
                IsOnline = true,
                IsChecked = false
            });
            DtContact.Add(new Contact
            {
                IdUser = user2,
                NamaPanggilan = $"User {i}A",
                UrlProfile = null,
                IsOnline = true,
                IsChecked = false
            });
            var pesan = new uimT6Pesan
            {
                IdPesan = idPesan,
                StatusGrup = false,
                NamaGrup = null,
                DeskripsiGrup = null,
                WaktuCreate = DateTimeOffset.Now.AddMinutes(-i * 10),
                FullName = $"User Chat {i}",
                Notes = $"Dummy notes {i}",
                ImageFileName = "default.png",
                StatusMessage = "Online",
                TotalUnreadMessage = "2",
                IsOnline = true,
                JenisPesan = "Text",
                StatusView = true,
                IsMine = false,
                TotalChat = 2,
                WaktuProses = DateTimeOffset.Now,
                Durasi = "00:01:00",
                ListT7Pesan_Percakapan = new List<T7Pesan_Percakapan>
                {
                    new T7Pesan_Percakapan
                    {
                        IdDetilPesan = Guid.NewGuid(),
                        IdPesan = idPesan,
                        IdUser_Pengirim = user1,
                        IsiPesan = $"Halo ini pesan pertama dari user1 di chat {i}",
                        StatusDraft = false,
                        StatusPin = false,
                        StatusStar = false,
                        StatusSystem = false,
                        WaktuSend = DateTimeOffset.Now.AddMinutes(-9),
                        WaktuInsert = DateTimeOffset.Now.AddMinutes(-9),
                    },
                    new uimT7Pesan_Percakapan
                    {
                        IdDetilPesan = Guid.NewGuid(),
                        IdPesan = idPesan,
                        IdUser_Pengirim = user2,
                        IsiPesan = $"Balasan dari user2 untuk chat {i}",
                        StatusDraft = false,
                        StatusPin = false,
                        StatusStar = false,
                        StatusSystem = false,
                        WaktuSend = DateTimeOffset.Now.AddMinutes(-8),
                        WaktuInsert = DateTimeOffset.Now.AddMinutes(-8),
                    }
                },
                ListT7Pesan_Anggota = new List<T7Pesan_Anggota>
                {
                    new uimT7Pesan_Anggota
                    {
                        IdDetilPesan = Guid.NewGuid(),
                        IdPesan = idPesan,
                        IdUser_Anggota = user1,
                        StatusCreator = true,
                        StatusAdministrator = true,
                        StatusArchive = false,
                        StatusMute = false,
                        StatusPin = false,
                        WaktuJoin = DateTimeOffset.Now.AddMinutes(-10),
                        Nama = $"User {i}A",
                        Warna = "#FF5733",
                        UrlProfile = "https://example.com/profile1.png"
                    },
                    new uimT7Pesan_Anggota
                    {
                        IdDetilPesan = Guid.NewGuid(),
                        IdPesan = idPesan,
                        IdUser_Anggota = user2,
                        StatusCreator = false,
                        StatusAdministrator = false,
                        StatusArchive = false,
                        StatusMute = false,
                        StatusPin = false,
                        WaktuJoin = DateTimeOffset.Now.AddMinutes(-9),
                        Nama = $"User {i}B",
                        Warna = "#33A1FF",
                        UrlProfile = "https://example.com/profile2.png"
                    }
                }
            };

            DtPesan.Add(pesan);
        }
        DtContact_Ditampilkan = DtContact;
        var pesan1 = DtPesan.FirstOrDefault();
        pesan1.IsMine = false;
        pesan1.StatusMessage = "Unread Multiple";
        pesan1.TotalUnreadMessage = "3";
        DtPesan_Ditampilkan = DtPesan;

        var pesan2 = DtPesan.LastOrDefault();
        pesan2.IsMine = true;
        pesan2.StatusMessage = "read";
        if (DtPesan.Count > 0)
        {
            DtAnggota.AddRange(DtPesan.SelectMany(x => x.ListT7Pesan_Anggota).Adapt<List<uimT7Pesan_Anggota>>());
            var recentlyContacted = DtPesan.Where(y => y.StatusGrup == false).OrderBy(x => x.WaktuProses).Take(3).ToList();
            foreach (var item in recentlyContacted)
            {
                var contactKaryawan = DtContact.FirstOrDefault(y => y.IdUser == item.ListT7Pesan_Anggota.FirstOrDefault(x => x.IdUser_Anggota != IdUser).IdUser_Anggota);
                DtRecentContact.Add(contactKaryawan);
            }

            //foreach (var item in DtPesan)
            //{
            //    var messageBelumDelivered = item.ListT7Pesan_Percakapan.Where(x => !x.ListT8Pesan_Terima.Any(y => y.IdUser_Penerima == IdUser)).ToList();
            //    foreach (var item2 in messageBelumDelivered)
            //    {
            //        var t8PesanTerima = new uimT8Pesan_Terima
            //        {
            //            IdDetilPesan = item2.IdDetilPesan,
            //            IdUser_Penerima = IdUser,
            //            WaktuDelivered = DateTimeOffset.UtcNow,
            //        };
            //        if (item2.IdUser_Pengirim != null) DtStatusPesan.Add(t8PesanTerima);
            //        //DtStatusPesan.Add(t8PesanTerima);
            //    }
            //}

            //await _svcPesan.InsertStatusPesan(DtStatusPesan);
        }

        //RcpUtama.SimpanPesan_Diterima += ProsesTerimaPesanBaru;
        //RcpUtama.SimpanChat_Diterima += ProsesTerimaChatBaru;
        //RcpUtama.StatusPesan_Diterima += ProsesPerbaruiStatusPesan;

        await InvokeAsync(StateHasChanged);
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (!_selesaiInisialisasi) await Inisialisasi();

            await JSR.InvokeVoidAsync("autoResizeMemoUp", "memo-textarea-grow-up");
            await JSR.InvokeVoidAsync("chatScrollInterop.observeScroll", DotNet);
        }
    }

    private async void OnEnterPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            if (e.ShiftKey)
            {
                // Shift + Enter = Tambah baris baru
                MessageText += Environment.NewLine;
            }
            else
            {
                // Enter = Kirim pesan
                await ProsesKirimPesan();
                MessageText = "";
                await InvokeAsync(StateHasChanged);
                await Task.Delay(50);
                await JSR.InvokeVoidAsync("autoResizeMemoUp", "memo-textarea-grow-up", "chat-list-box");
                //lbxPercakapan.MakeItemVisible(DtPercakapan.IndexOf(DtPercakapan.Last()) + 1);
            }
        }

    }
    public async Task ProsesPilihKontak(Guid id, bool isGrup = false)
    {
        DtAnggota_GrupBaru.Clear();
        if (isGrup)
        {
            DtPesanAktif = DtPesan.FirstOrDefault(x => x.IdPesan == id);
            await ProsesPilihPesan(DtPesanAktif);
        }
        else
        {
            var isSudahAdaPerckapan = DtPesan.FirstOrDefault(x => x.StatusGrup == false && x.ListT7Pesan_Anggota.FirstOrDefault(x => x.IdUser_Anggota == id) != null);
            if (isSudahAdaPerckapan != null)
            {
                DtPesanAktif = DtPesan.FirstOrDefault(x => x.IdPesan == isSudahAdaPerckapan.IdPesan);
                await ProsesPilihPesan(DtPesanAktif);
            }
            else
            {
                var contact = DtContact.FirstOrDefault(x => x.IdUser == id);
                DtPesanAktif = new uimT6Pesan { JenisPesan = "Baru", FullName = contact.NamaPanggilan, NamaGrup = "", StatusGrup = false, IsOnline = true };
                DtAnggota_GrupBaru.Add(new uimT7Pesan_Anggota { IdPesan = DtPesanAktif.IdPesan, IdUser_Anggota = contact.IdUser });
                DtAnggota_GrupBaru.Add(new uimT7Pesan_Anggota { IdPesan = DtPesanAktif.IdPesan, IdUser_Anggota = IdUser });
                DtPesanAktif.ListT7Pesan_Anggota = DtAnggota_GrupBaru.Adapt<ICollection<T7Pesan_Anggota>>();
                DtPercakapan = new();
            }
        }
        TampilkanPersonalMessageSection = true;
        DtContact_Ditampilkan = DtContact;
        TextValue = "";
        StateHasChanged();
        await Task.Yield();
        await JSR.InvokeVoidAsync("autoResizeMemoUp", "memo-textarea-grow-up", "chat-list-box");
        await MessageBox.FocusAsync();
        await FetchPreview();
    }
    public async Task ProsesPilihPesan(uimT6Pesan item)
    {
        if (item.Kategori == "Contacts") await ProsesPilihKontak(Guid.Parse(item.JenisPesan));
        if (item.Kategori == "Groups") await ProsesPilihKontak(item.IdPesan, true);
        else
        {
            IsTampilkanButtonScrollToBottom = false;
            ChatAktif = null;
            GrupSudahAda = null;
            DtPercakapan.Clear();

            var dtToUpdate = DtPesan.FirstOrDefault(x => x.IdPesan == item.IdPesan);
            DtAnggota_GrupBaru = item.ListT7Pesan_Anggota.Adapt<List<uimT7Pesan_Anggota>>();

            for (int i = 0; i < DtAnggota_GrupBaru.Count; i++)
            {
                DtAnggota_GrupBaru[i].Warna = colorPalette[i];
            }

            dtToUpdate.TotalUnreadMessage = "0";
            DtPesanAktif = item;

            // Tambahkan semua pesan biasa (non-system)
            DtPercakapan.AddRange(item.ListT7Pesan_Percakapan
                .Where(x => x.StatusSystem == false)
                .OrderBy(x => x.WaktuSend)
                .Adapt<List<uimT7Pesan_Percakapan>>());

            // Tambahkan pesan keterangan grup, jika ada
            if (DtPesanAktif.StatusGrup)
            {
                var creator = DtPesanAktif.ListT7Pesan_Anggota
                    .FirstOrDefault(x => x.StatusCreator == true);

                uimT7Pesan_Percakapan keteranganGrup = null;

                if (creator?.IdUser_Anggota == IdUser)
                {
                    keteranganGrup = DtPesanAktif.ListT7Pesan_Percakapan
                        .FirstOrDefault(x => x.StatusSystem && x.IsiPesan.Contains("membuat"))
                        ?.Adapt<uimT7Pesan_Percakapan>();
                }
                else
                {
                    keteranganGrup = DtPesanAktif.ListT7Pesan_Percakapan
                        .FirstOrDefault(x => x.StatusSystem && x.IsiPesan.Contains("menambahkan"))
                        ?.Adapt<uimT7Pesan_Percakapan>();
                }

                if (keteranganGrup != null)
                {
                    DtPercakapan.Add(keteranganGrup);
                }
            }

            // Urutkan ulang berdasarkan WaktuSend
            DtPercakapan = DtPercakapan.OrderBy(x => x.WaktuSend).ToList();

            // Set properti tambahan setelah urutan benar
            foreach (var x in DtPercakapan)
            {
                x.TanggalPesan = x.WaktuSend?.Date.ToString("yyyy-MM-dd"); // Untuk GroupFieldName
                if (x.IdUser_Pengirim == IdUser || x.IsiPesan.Contains("Balasan"))
                {
                    x.IsMine = true;
                }
            }

            StateHasChanged();
            TampilkanPersonalMessageSection = true;

            await Task.Yield();
            if (!string.IsNullOrEmpty(SearchTextPesan) && item.Kategori == "Messages")
            {
                OnSearchTextChanged(SearchTextPesan);
                IsTampilkanPanelSearchPesan = true;
                TampilkanPencarianSelanjutnya();
                SearchTextPesan = "";
                FilterTextPesan = "";
            }
            else
            {
                lbxPercakapan.MakeItemVisible(DtPercakapan.IndexOf(DtPercakapan.Last()) + 1);
                SearchTextPesan = "";
                FilterTextPesan = "";
            }
            StateHasChanged();
            MessageBox.FocusAsync();

            //await _svcPesan.UpdateStatusPesan(DtPesanAktif.IdPesan, IdUser);
            //await RcpUtama.SSR.KirimPesan(0, "Pesan", "Read", "", DtPesanAktif.IdPesan, DateTimeOffset.UtcNow, "", IdUser, "");
            await RcpUtama.SSR.KirimPesan(jenisPesan: "Pesan", namaForm: "", statusAction: "Read", namaFieldPK: "", nilaiPK: DtPesanAktif.IdPesan, waktuProses: DateTime.UtcNow, isiPesan: "", IdUser, User_NamaPanggilan: "AUTO", idUserPenerima: Guid.Parse("00000001-0000-0000-0000-000000000000"), userPenerima_NamaPanggilan: "AUTO"); //perlu sesuaikan user nama panggilan dan user penerima
        }
    }


    public void ProsesReplyPercakapan()
    {
        IdPesanDihover = null;
        PesanDikutip = new();
        PesanDikutip = DtPercakapan.FirstOrDefault(x => x.IdDetilPesan == IdDetilPesanAktif);
        TampilkanChatDropdown = !TampilkanChatDropdown;
        MessageBox.FocusAsync();
        StateHasChanged();
    }

    public DxListBox<uimT7Pesan_Percakapan, uimT7Pesan_Percakapan> lbxPercakapan { get; set; }

    public async Task ProsesKirimPesan()
    {
        if (IsRecording || AudioPreviewUrl != null)
        {
            await ProsesHasilRecording();
        }
        var drLinkPreview = DrLinkPreview;
        DrLinkPreview = null;
        await JSR.InvokeVoidAsync("autoResizeMemoUp", "memo-textarea-grow-up", "chat-list-box");
        if (DtPesanAktif.JenisPesan == "Baru")
        {
            //RcpUtama.TampilkanMessageSection = true;
            //RcpUtama.TampilkanPersonalMessageSection = true;
            if (DtPesanAktif.StatusGrup)
            {
                var blobName = $"Grup-icon-{DtPesanAktif.IdPesan}-{DtPesanAktif.NamaGrup}";
                if (FilePreview != null)
                {
                    using var stream = FilePreview.OpenReadStream();
                    var urlIcon = await UploadDataToStorage(stream, blobName, FilePreview.ContentType);
                    DtPesanAktif.IconGrup = urlIcon;
                }
                DtAnggota_GrupBaru.Add(new uimT7Pesan_Anggota { IdPesan = DtPesanAktif.IdPesan, IdUser_Anggota = IdUser, StatusAdministrator = true, StatusCreator = true, WaktuJoin = DateTime.Now });
                DtPercakapan.Add(new uimT7Pesan_Percakapan
                {
                    IdPesan = DtPesanAktif.IdPesan,
                    IsiPesan = $"Anda membuat grup ini",
                    IdUser_Pengirim = IdUser,
                    WaktuSend = DateTimeOffset.UtcNow,
                    TanggalPesan = DateTimeOffset.UtcNow.Date.ToString("yyyy-MM-dd"),
                    IdDetilPesan_Terkutip = null,
                    IsMine = false,
                    StatusSystem = true,

                });
                var creator = DtContact.FirstOrDefault(X => X.IdUser == IdUser);
                DtPercakapan.Add(new uimT7Pesan_Percakapan
                {
                    IdPesan = DtPesanAktif.IdPesan,
                    IsiPesan = $"{creator.NamaPanggilan} menambahkan Anda",
                    IdUser_Pengirim = IdUser,
                    WaktuSend = DateTimeOffset.UtcNow,
                    TanggalPesan = DateTimeOffset.UtcNow.Date.ToString("yyyy-MM-dd"),
                    IsMine = false,
                    StatusSystem = true,
                    IsVisible = false,
                });
                //DtPesanAktif.ListT7Pesan_Percakapan = DtPercakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
                DtPesanAktif.Notes = "Anda membuat grup ini";
            }
            else
            {
                if (MessageText == "" && AudioUrl == null) return;
                if (AudioUrl != null) MessageText = AudioUrl;
                var chatBaru = new uimT7Pesan_Percakapan
                {
                    IdPesan = DtPesanAktif.IdPesan,
                    IsiPesan = $"{MessageText.Replace("\n", "new-line")}",
                    IdUser_Pengirim = IdUser,
                    WaktuSend = DateTimeOffset.UtcNow,
                    TanggalPesan = DateTimeOffset.UtcNow.Date.ToString("yyyy-MM-dd"),
                    IdDetilPesan_Terkutip = null,
                    IsMine = true,
                    LinkPreview_Title = drLinkPreview?.Title,
                    LinkPreview_Description = drLinkPreview?.Description,
                    LinkPreview_ImageUrl = drLinkPreview?.ImageUrl,
                    LinkPreview_Url = drLinkPreview?.Url
                };
                DtPesanAktif.Notes = MessageText.Replace("\n", " ");
                if (AudioUrl != null) { chatBaru.Durasi = Durasi; DtPesanAktif.Durasi = Durasi; }
                DtPercakapan.Add(chatBaru);

            }
            DtPesanAktif.ListT7Pesan_Anggota = DtAnggota_GrupBaru.Adapt<ICollection<T7Pesan_Anggota>>();
            DtPesanAktif.ListT7Pesan_Percakapan = DtPercakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
            DtPesanAktif.WaktuProses = DateTimeOffset.UtcNow;
            DtPesanAktif.WaktuCreate = DateTimeOffset.UtcNow;
            //await _svcPesan.InsertPesan(DtPesanAktif);
            await RcpUtama.SSR.KirimPesan(jenisPesan: "Pesan", namaForm: "", statusAction: "Pesan", namaFieldPK: "", nilaiPK: DtPesanAktif.IdPesan, waktuProses: DateTime.UtcNow, isiPesan: "", IdUser, User_NamaPanggilan: "AUTO", idUserPenerima: Guid.Parse("00000001-0000-0000-0000-000000000000"), userPenerima_NamaPanggilan: "AUTO"); //perlu sesuaikan user nama panggilan dan user penerima
            DtPesanAktif.JenisPesan = "";
            DtPesanAktif.IsMine = true;

            DtPesan.Add(DtPesanAktif);

        }
        else
        {
            if (MessageText == "" && AudioUrl == null) return;
            if (AudioUrl != null) MessageText = AudioUrl;
            var isiPesan = MessageText.Replace("\n", "new-line");
            var pesanBaru = new uimT7Pesan_Percakapan
            {
                IdPesan = DtPesanAktif.IdPesan,
                IsiPesan = isiPesan,
                IdUser_Pengirim = IdUser,
                WaktuSend = DateTimeOffset.UtcNow,
                TanggalPesan = DateTimeOffset.UtcNow.Date.ToString("yyyy-MM-dd"),
                IdDetilPesan_Terkutip = PesanDikutip?.IdDetilPesan,
                PesanTerkutip_IdUserPengirim = PesanDikutip?.IdUser_Pengirim,
                PesanTerkutip_IsiPesan = PesanDikutip?.IsiPesan,
                PesanTerkutip_Durasi = PesanDikutip?.Durasi,
                IsMine = true,
                LinkPreview_Title = drLinkPreview?.Title,
                LinkPreview_Description = drLinkPreview?.Description,
                LinkPreview_ImageUrl = drLinkPreview?.ImageUrl,
                LinkPreview_Url = drLinkPreview?.Url
            };
            DtPesanAktif.Notes = MessageText.Replace("\n", " ");
            if (AudioUrl != null) { pesanBaru.Durasi = Durasi; DtPesanAktif.Durasi = Durasi; }
            if (DtPesanAktif.StatusGrup) DtPesanAktif.Notes = $"Anda: {MessageText.Replace("\n", " ")}";
            DtPesanAktif.WaktuProses = DateTimeOffset.UtcNow;
            DtPercakapan.Add(pesanBaru);
            StateHasChanged();
            PesanDikutip = null;
            lbxPercakapan.MakeItemVisible(DtPercakapan.IndexOf(DtPercakapan.Last()) + 1);
            StateHasChanged();
            //await _svcPesan.InsertPercakapan(pesanBaru);
            await RcpUtama.SSR.KirimPesan(jenisPesan: "Pesan", namaForm: "", statusAction: "Chat", namaFieldPK: "", nilaiPK: DtPesanAktif.IdPesan, waktuProses: DateTime.UtcNow, isiPesan: "", IdUser, User_NamaPanggilan: "AUTO", idUserPenerima: Guid.Parse("00000001-0000-0000-0000-000000000000"), userPenerima_NamaPanggilan: "AUTO"); //perlu sesuaikan user nama panggilan dan user penerima
            var pesanToUpdate = DtPesan.FirstOrDefault(x => x.IdPesan == DtPesanAktif.IdPesan);
            pesanToUpdate.ListT7Pesan_Percakapan = DtPercakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
            pesanToUpdate.IsMine = true;
            pesanToUpdate.StatusMessage = "unread";
        }
        IsRecording = false;
        AudioUrl = null;
        AudioPreviewUrl = null;
        MessageText = "";
        StateHasChanged();

    }

    public async void ProsesBukaLinkPreview(string url)
    {
        await JSR.InvokeVoidAsync("openNewTab", url);
    }

    CheckBoxContentAlignment Alignment { get; set; } = CheckBoxContentAlignment.Left;
    LabelPosition LabelPosition { get; set; } = LabelPosition.Right;

    #region 'Buat dan Validasi Grup'
    uimT6Pesan GrupSudahAda { get; set; } = null;

    private void ToggleCheckBox(Contact? contact)
    {
        DtPesanAktif.JenisPesan = "Baru";
        var anggotaBaru = new uimT7Pesan_Anggota();
        anggotaBaru.IdPesan = DtPesanAktif.IdPesan;
        anggotaBaru.WaktuJoin = DateTimeOffset.UtcNow;
        contact.IsChecked = !contact.IsChecked;
        anggotaBaru.IdUser_Anggota = contact.IdUser;
        anggotaBaru.Nama = contact.NamaPanggilan;
        anggotaBaru.UrlProfile = contact.UrlProfile;
        if (contact.IsChecked) DtAnggota_GrupBaru.Add(anggotaBaru);
        else
        {
            var anggota = DtAnggota_GrupBaru.FirstOrDefault(y => y.IdUser_Anggota == contact.IdUser);
            DtAnggota_GrupBaru.Remove(anggota);
        }
        // **Cek apakah ada grup dengan anggota yang sama**
        if (DtAnggota_GrupBaru.Count() >= 2) CekGrupSudahAda(DtAnggota_GrupBaru);
    }
    private void OnCheckedContact(bool value, Contact? contact)
    {
        DtPesanAktif.JenisPesan = "Baru";
        var anggotaBaru = new uimT7Pesan_Anggota();
        anggotaBaru.IdPesan = DtPesanAktif.IdPesan;
        if (value)
        {
            contact.IsChecked = value;
            anggotaBaru.IdUser_Anggota = contact.IdUser;
            anggotaBaru.Nama = contact.NamaPanggilan;
            anggotaBaru.WaktuJoin = DateTimeOffset.UtcNow;
            anggotaBaru.UrlProfile = contact.UrlProfile;
            DtAnggota_GrupBaru.Add(anggotaBaru);
        }
        else
        {

            contact.IsChecked = value;
            anggotaBaru = DtAnggota.FirstOrDefault(x => x.IdUser_Anggota == contact.IdUser);
            DtAnggota_GrupBaru.Remove(anggotaBaru);
        }

        // **Cek apakah ada grup dengan anggota yang sama**
        if (DtAnggota_GrupBaru.Count() >= 2) CekGrupSudahAda(DtAnggota_GrupBaru);
    }

    private void RemoveAnggota(uimT7Pesan_Anggota anggota)
    {
        var contactUnchecked = DtContact.FirstOrDefault(x => x.IdUser == anggota.IdUser_Anggota);
        contactUnchecked.IsChecked = false;
        DtAnggota_GrupBaru.Remove(anggota);

        // **Cek apakah ada grup dengan anggota yang sama**
        if (DtAnggota_GrupBaru.Count() >= 2) CekGrupSudahAda(DtAnggota_GrupBaru);
    }

    // **Method untuk mengecek apakah ada grup dengan anggota yang sama**
    private void CekGrupSudahAda(List<uimT7Pesan_Anggota> anggotaGrupBaru)
    {
        // Ambil ID karyawan yang dipilih
        var IdUserDipilih = anggotaGrupBaru.Select(a => a.IdUser_Anggota).OrderBy(id => id).ToList();

        // Cek di daftar grup yang sudah ada
        foreach (var grup in DtPesan.Where(x => x.StatusGrup)) // Misalnya DtGrup adalah daftar semua grup yang ada
        {
            var IdUserGrupLama = grup.ListT7Pesan_Anggota.Where(x => x.IdUser_Anggota != IdUser).Select(a => a.IdUser_Anggota).OrderBy(id => id).ToList();

            // **Bandingkan list anggota yang dipilih dengan anggota grup yang ada**
            if (IdUserDipilih.SequenceEqual(IdUserGrupLama))
            {
                GrupSudahAda = grup; // Grup dengan anggota yang sama sudah ada
                StateHasChanged();
                return;
            }
        }

        GrupSudahAda = null; // Grup dengan anggota yang sama belum ada
        StateHasChanged();
    }
    private async void LihatGrupSudahAda()
    {
        await ProsesPilihPesan(GrupSudahAda);
        StateHasChanged();
    }
    #endregion

    #region 'SignalR'
    public async void ProsesTerimaPesanBaru(uimT6Pesan drPesanBaru)
    {
        var isPesanSudahAda = DtPesan.FirstOrDefault(x => x.IdPesan == drPesanBaru.IdPesan);
        if (isPesanSudahAda != null) return;
        drPesanBaru.JenisPesan = "";
        var cekAnggota = drPesanBaru.ListT7Pesan_Anggota.FirstOrDefault(x => x.IdUser_Anggota == IdUser && x.StatusCreator == false);
        if (cekAnggota == null) return;
        //Update Recent Contact
        if (!drPesanBaru.StatusGrup)
        {
            var contact = DtContact.FirstOrDefault(x => x.IdUser == drPesanBaru.ListT7Pesan_Percakapan.FirstOrDefault().IdUser_Pengirim);
            if (DtRecentContact.Any(x => x.IdUser == contact.IdUser)) DtRecentContact.Remove(contact);
            if (DtRecentContact.Count() > 2) DtRecentContact.RemoveAt(0);
            DtRecentContact.Add(contact);
        }

        //Update Keterangan grup
        if (drPesanBaru.StatusGrup && drPesanBaru.ListT7Pesan_Percakapan.Count() < 3) drPesanBaru.Notes = drPesanBaru.ListT7Pesan_Percakapan.FirstOrDefault(x => x.IsiPesan.Contains("menambahkan")).IsiPesan;
        else drPesanBaru.Notes = drPesanBaru.ListT7Pesan_Percakapan.LastOrDefault().IsiPesan.Replace("new-line", " ");
        drPesanBaru.StatusView = false;
        drPesanBaru.WaktuProses = DateTimeOffset.UtcNow;
        drPesanBaru.StatusMessage = "Unread Multiple";
        drPesanBaru.TotalUnreadMessage = "1";

        //Buat Status Pesan Delivered
        var messageBelumDelivered = drPesanBaru.ListT7Pesan_Percakapan.Where(x => x.ListT8Pesan_Terima == null || x.ListT8Pesan_Terima.Count() < 1).ToList();
        var listT8Range = new List<uimT8Pesan_Terima>();
        foreach (var item in messageBelumDelivered)
        {
            var listT8 = new List<uimT8Pesan_Terima>();
            var t8PesanTerima = new uimT8Pesan_Terima
            {
                IdDetilPesan = item.IdDetilPesan,
                IdUser_Penerima = IdUser,
                WaktuDelivered = DateTimeOffset.UtcNow,
            };
            listT8.Add(t8PesanTerima);
            if (item.IdUser_Pengirim != null) listT8Range.Add(t8PesanTerima);
            drPesanBaru.ListT7Pesan_Percakapan.FirstOrDefault(x => x.IdDetilPesan == item.IdDetilPesan).ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
        }


        DtPesan.Add(drPesanBaru);
        //await _svcPesan.InsertStatusPesan(listT8Range);

    }

    public async Task ProsesTerimaChatBaru(uimT7Pesan_Percakapan drChatBaru)
    {
        var isChatSudahAda = DtPercakapan.FirstOrDefault(x => x.IdDetilPesan == drChatBaru.IdDetilPesan);
        if (isChatSudahAda != null) return;
        var user = IdUser;
        drChatBaru.WaktuProses = DateTimeOffset.UtcNow;
        drChatBaru.TanggalPesan = drChatBaru.WaktuSend.Value.Date.ToString("yyyy-MM-dd");
        if (drChatBaru.IdUser_Pengirim == IdUser) drChatBaru.IsMine = true;
        else drChatBaru.IsMine = false;
        var pesanLama = DtPesan.Where(x => x.IdPesan == drChatBaru.IdPesan).FirstOrDefault();
        if (pesanLama == null) return;

        var contact = DtContact.FirstOrDefault(x => x.IdUser == drChatBaru.IdUser_Pengirim);
        //Buat Status Pesan Delivered
        var listT8 = new List<uimT8Pesan_Terima>();
        var t8PesanTerima = new uimT8Pesan_Terima
        {
            IdDetilPesan = drChatBaru.IdDetilPesan,
            IdUser_Penerima = IdUser,
            WaktuDelivered = DateTimeOffset.UtcNow,
        };
        if (DtPesanAktif.IdPesan == drChatBaru.IdPesan) t8PesanTerima.WaktuRead = DateTimeOffset.UtcNow;
        listT8.Add(t8PesanTerima);
        drChatBaru.ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
        if (DtPesanAktif.IdPesan == drChatBaru.IdPesan)
        {
            DtPercakapan.Add(drChatBaru);
            await RcpUtama.SSR.KirimPesan(jenisPesan: "Pesan", namaForm: "", statusAction: "Read", namaFieldPK: "", nilaiPK: DtPesanAktif.IdPesan, waktuProses: DateTime.UtcNow, isiPesan: "", IdUser, User_NamaPanggilan: "AUTO", idUserPenerima: Guid.Parse("00000001-0000-0000-0000-000000000000"), userPenerima_NamaPanggilan: "AUTO"); //perlu sesuaikan user nama panggilan dan user penerima
        }
        //Update Recent Contact
        if (!pesanLama.StatusGrup)
        {
            if (DtRecentContact.Any(x => x.IdUser == contact.IdUser)) DtRecentContact.Remove(contact);
            if (DtRecentContact.Count() > 2) DtRecentContact.RemoveAt(3);
            DtRecentContact.Add(contact);
        }



        var pesanBaru = pesanLama;
        pesanBaru.StatusView = false;

        //Add Percakapan Baru
        var percakapan = pesanBaru.ListT7Pesan_Percakapan.Adapt<List<uimT7Pesan_Percakapan>>();
        percakapan.Add(drChatBaru);
        pesanBaru.ListT7Pesan_Percakapan = percakapan.Adapt<ICollection<T7Pesan_Percakapan>>();

        //Update Pesan
        pesanBaru.WaktuProses = DateTimeOffset.UtcNow;
        pesanBaru.Notes = drChatBaru.IsiPesan.Replace("new-line", " ");
        if (pesanBaru.StatusGrup)
        {
            if (drChatBaru.IdUser_Pengirim != IdUser) pesanBaru.Notes = $"{contact.NamaPanggilan}: {drChatBaru.IsiPesan.Replace("new-line", " ")}";
            else pesanBaru.Notes = $"Anda: {drChatBaru.IsiPesan.Replace("new-line", " ")}";
        }

        //Hitung Unread Message
        var unreadMessage = pesanBaru?.ListT7Pesan_Percakapan.Where(x => x.IdUser_Pengirim != IdUser && x.ListT8Pesan_Terima != null &&
        (x.ListT8Pesan_Terima.Count() < 1 ||
                    x.ListT8Pesan_Terima.Any(y => y.WaktuRead is null))).Count() ?? 0;

        pesanBaru.TotalUnreadMessage = unreadMessage.ToString();
        pesanBaru.StatusMessage = unreadMessage > 0 ? "Unread Multiple" : "";

        //DtPesan.Remove(pesanLama);
        //DtPesan.Add(pesanBaru);



        //await _svcPesan.InsertStatusPesan(t8PesanTerima);

        if (DtPesanAktif.IdPesan == drChatBaru.IdPesan) lbxPercakapan.MakeItemVisible(DtPercakapan.IndexOf(DtPercakapan.Last()) + 1);
    }

    public async Task ProsesPerbaruiStatusPesan(Guid idPesan, Guid idUserPenerima)
    {
        var pesanToUpdate = DtPesan.FirstOrDefault(x => x.IdPesan == idPesan);
        if (pesanToUpdate == null) return;
        var pesanToAdd = pesanToUpdate;
        if (pesanToAdd.StatusGrup == false)
        {
            if (DtPesanAktif.IdPesan == idPesan)
            {
                var percakapan = DtPercakapan;
                percakapan.ForEach(x =>
                {
                    var listT8 = x.ListT8Pesan_Terima.Adapt<List<uimT8Pesan_Terima>>();
                    if (listT8 == null)
                    {
                        var terimaPesan = new uimT8Pesan_Terima
                        {
                            IdDetilPesan = x.IdDetilPesan,
                            IdUser_Penerima = idUserPenerima,
                            WaktuRead = DateTimeOffset.UtcNow,
                        };
                        listT8 = new List<uimT8Pesan_Terima>();
                        listT8.Add(terimaPesan);
                    }
                    else
                    {
                        var t8TerimaPesan = listT8.FirstOrDefault(y => y.IdUser_Penerima == idUserPenerima);
                        if (t8TerimaPesan != null)
                            t8TerimaPesan.WaktuRead = DateTimeOffset.UtcNow;
                        else
                        {
                            var terimaPesan = new uimT8Pesan_Terima
                            {
                                IdDetilPesan = x.IdDetilPesan,
                                IdUser_Penerima = idUserPenerima,
                                WaktuRead = DateTimeOffset.UtcNow,
                            };
                            if (listT8 == null) listT8 = new List<uimT8Pesan_Terima>();
                            listT8.Add(terimaPesan);
                        }
                    }
                    x.ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
                });
                DtPesanAktif.ListT7Pesan_Percakapan = percakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
            }
            else
            {

                var percakapan = pesanToUpdate.ListT7Pesan_Percakapan.Adapt<List<uimT7Pesan_Percakapan>>();
                percakapan.ForEach(x =>
                {
                    var listT8 = x.ListT8Pesan_Terima.Adapt<List<uimT8Pesan_Terima>>();
                    if (listT8 == null)
                    {
                        var terimaPesan = new uimT8Pesan_Terima
                        {
                            IdDetilPesan = x.IdDetilPesan,
                            IdUser_Penerima = idUserPenerima,
                            WaktuRead = DateTimeOffset.UtcNow,
                        };
                        listT8 = new List<uimT8Pesan_Terima>();
                        listT8.Add(terimaPesan);
                    }
                    else
                    {
                        var t8TerimaPesan = listT8.FirstOrDefault(y => y.IdUser_Penerima == idUserPenerima);
                        if (t8TerimaPesan != null)
                            t8TerimaPesan.WaktuRead = DateTimeOffset.UtcNow;
                        else
                        {
                            var terimaPesan = new uimT8Pesan_Terima
                            {
                                IdDetilPesan = x.IdDetilPesan,
                                IdUser_Penerima = idUserPenerima,
                                WaktuRead = DateTimeOffset.UtcNow,
                            };
                            if (listT8 == null) listT8 = new List<uimT8Pesan_Terima>();
                            listT8.Add(terimaPesan);
                        }
                    }
                    x.ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
                });
                pesanToAdd.ListT7Pesan_Percakapan = percakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
            }
            pesanToAdd.StatusMessage = "read";
            pesanToAdd.TotalUnreadMessage = "0";

        }
        else
        {
            if (DtPesanAktif.IdPesan == idPesan)
            {
                var percakapanToUpdate = DtPercakapan.Where(x => x.IdUser_Pengirim == IdUser || x.IdUser_Pengirim != null).ToList();

                foreach (var item in percakapanToUpdate)
                {
                    var listT8 = item.ListT8Pesan_Terima.Adapt<List<uimT8Pesan_Terima>>();
                    if (listT8 == null)
                    {
                        var t8TerimaPesan = new uimT8Pesan_Terima
                        {
                            IdDetilPesan = item.IdDetilPesan,
                            IdUser_Penerima = idUserPenerima,
                            WaktuRead = DateTimeOffset.UtcNow,
                        };
                        listT8 = new List<uimT8Pesan_Terima>();
                        listT8.Add(t8TerimaPesan);
                    }
                    else
                    {
                        var t8TerimaPesan = listT8.FirstOrDefault(y => y.IdUser_Penerima == idUserPenerima);
                        if (t8TerimaPesan != null)
                            t8TerimaPesan.WaktuRead = DateTimeOffset.UtcNow;
                        else
                        {
                            var terimaPesan = new uimT8Pesan_Terima
                            {
                                IdDetilPesan = item.IdDetilPesan,
                                IdUser_Penerima = idUserPenerima,
                                WaktuRead = DateTimeOffset.UtcNow,
                            };
                            if (listT8 == null) listT8 = new List<uimT8Pesan_Terima>();
                            listT8.Add(terimaPesan);
                        }
                    }

                    item.ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
                }

                pesanToAdd.ListT7Pesan_Percakapan = percakapanToUpdate.Adapt<ICollection<T7Pesan_Percakapan>>();
            }
            else
            {
                var percakapanToUpdate = pesanToUpdate.ListT7Pesan_Percakapan.Where(x => x.IdUser_Pengirim == IdUser || x.IdUser_Pengirim != null).ToList();

                pesanToAdd.TotalUnreadMessage = "0";

                foreach (var item in percakapanToUpdate)
                {
                    var listT8 = item.ListT8Pesan_Terima.Adapt<List<uimT8Pesan_Terima>>();
                    if (listT8 == null)
                    {
                        var t8TerimaPesan = new uimT8Pesan_Terima
                        {
                            IdDetilPesan = item.IdDetilPesan,
                            IdUser_Penerima = idUserPenerima,
                            WaktuRead = DateTimeOffset.UtcNow,
                        };
                        listT8 = new List<uimT8Pesan_Terima>();
                        listT8.Add(t8TerimaPesan);
                    }
                    else
                    {
                        var t8TerimaPesan = listT8.FirstOrDefault(y => y.IdUser_Penerima == idUserPenerima);
                        if (t8TerimaPesan != null)
                            t8TerimaPesan.WaktuRead = DateTimeOffset.UtcNow;
                        else
                        {
                            var terimaPesan = new uimT8Pesan_Terima
                            {
                                IdDetilPesan = item.IdDetilPesan,
                                IdUser_Penerima = idUserPenerima,
                                WaktuRead = DateTimeOffset.UtcNow,
                            };
                            if (listT8 == null) listT8 = new List<uimT8Pesan_Terima>();
                            listT8.Add(terimaPesan);
                        }
                    }

                    item.ListT8Pesan_Terima = listT8.Adapt<ICollection<T8Pesan_Terima>>();
                }

                pesanToAdd.ListT7Pesan_Percakapan = percakapanToUpdate.Adapt<ICollection<T7Pesan_Percakapan>>();
            }


            //Hitung Total Anggota yang belum membaca pesan
            var totalAnggota = pesanToAdd.ListT7Pesan_Anggota.Count() - 1;
            var lastMessage = pesanToAdd.ListT7Pesan_Percakapan.LastOrDefault();
            var totalAnggotaSudahBaca = lastMessage.ListT8Pesan_Terima.Where(x => x.WaktuRead != null).ToList().Count();
            if (totalAnggotaSudahBaca >= totalAnggota) pesanToAdd.StatusMessage = "read";
            else pesanToAdd.StatusMessage = "unread";
        }
    }
    #endregion
    #region 'Event scroll'
    public DateTime MinDate = DateTime.Now;
    public async Task ProsesMuatPesanLainnya(DateTime? tanggal = null, bool isSearchAktif = false)
    {
        await Task.Delay(1000);

        var pesanPalingAwal = DtPercakapan.OrderBy(x => x.WaktuSend).FirstOrDefault(x => x.StatusSystem == false);
        if (pesanPalingAwal == null)
        {
            IsLoadingMoreMessages = false;
            StateHasChanged();
            return;
        }
        if (tanggal == null) tanggal = pesanPalingAwal.WaktuSend.Value.Date;
        else tanggal = tanggal.Value.Date;
        //var percakapanSebelumnya = _svcPesan.GetDataPercakapan(DtPesanAktif.IdPesan, tanggal.ToString(), isSearchAktif);
        //await foreach (var x in percakapanSebelumnya)
        //{
        //    x.ForEach(p =>
        //    {
        //        if (p.IdUser_Pengirim == IdUser) p.IsMine = true;
        //        p.TanggalPesan = p.WaktuSend.Value.Date.ToString("yyyy-MM-dd");
        //    });


        //    var pesanBaru = x.Where(p => !DtPercakapan.Any(existing => existing.IdDetilPesan == p.IdDetilPesan)).ToList();

        //    if (pesanBaru.Any())
        //    {
        //        DtPercakapan.AddRange(pesanBaru);
        //        var pesanToUpdate = DtPesan.FirstOrDefault(x => x.IdPesan == DtPesanAktif.IdPesan);
        //        if (pesanToUpdate != null) pesanToUpdate.ListT7Pesan_Percakapan = DtPercakapan.Adapt<ICollection<T7Pesan_Percakapan>>();
        //    }
        //}

        var percakapanAwal = DtPercakapan.Where(x => x.StatusSystem == false).OrderBy(x => x.WaktuSend).FirstOrDefault();
        //if (percakapanAwal != null) MinDate = (DateTime)percakapanAwal.WaktuSend.ToString().ToDateTime();

        //JSR.InvokeVoidAsync("scrollToPesan", $"{pesanPalingAwal.IdDetilPesan}");
        StateHasChanged();
    }
    public DotNetObjectReference<RcpChat>? DotNet;
    public bool IsLoadingMoreMessages = false;
    [JSInvokable]
    public async Task OnScroll(string value)
    {
        if (value == null) return;

        if (value == "matrix(1, 0, 0, 1, 0, 0)")
        {
            if (DtPesanAktif.TotalChat <= DtPercakapan.Count()) return;
            IsLoadingMoreMessages = true;
            StateHasChanged(); // Update UI agar spinner muncul
            await ProsesMuatPesanLainnya(isSearchAktif: false);
            IsLoadingMoreMessages = false;
            StateHasChanged(); // Update UI agar spinner hilang setelah loading selesai
            //var indexPesanAwal = DtPercakapan.OrderBy(x => x.WaktuSend).ToList().IndexOf(pesanPalingAwal);
            //Pastikan scroll tetap di posisi awal pesan lama
            //lbxPercakapan.MakeItemVisible(indexPesanAwal);
            //StateHasChanged();
        }
    }

    bool IsTampilkanButtonScrollToBottom { get; set; } = false;
    [JSInvokable]
    public async Task TampilkanButtonScrollToBottom(bool value)
    {
        IsTampilkanButtonScrollToBottom = !value;
        StateHasChanged();
    }
    #endregion

    #region 'Search Chat'
    bool IsTampilkanPanelSearchPesan { get; set; } = false;
    bool IsTampilkanCalendarSearchPesan { get; set; } = false;
    int JumlahChatDitemukan = 0;
    public List<uimT7Pesan_Percakapan> DtPercakapanSearch { get; set; } = new();
    async Task TampilkanPanelSearch()
    {
        IsTampilkanPanelSearchPesan = !IsTampilkanPanelSearchPesan;
        if (IsTampilkanPanelSearchPesan)
        {
            await Task.Delay(100);
            await TxbSearchChat.FocusAsync();
            await ProsesMuatPesanLainnya(isSearchAktif: true);
        }
        else
        {
            JumlahChatDitemukan = 0;
            SearchText = "";
            DtPercakapanSearch.Clear();
            ChatAktif = null;
        }

        StateHasChanged();
    }
    DateTime SelectedDate { get; set; } = DateTime.Now;
    async void OnSelectedDateChanged(DateTime selectedDate)
    {
        SelectedDate = selectedDate;
        var drpesan = DtPercakapan.OrderBy(x => x.WaktuSend).FirstOrDefault(x => x.WaktuSend.Value.Date == selectedDate.Date);
        var indexPesan = DtPercakapan.OrderBy(x => x.WaktuSend).ToList().IndexOf(drpesan);
        IsTampilkanCalendarSearchPesan = false;
        IsTampilkanPanelSearchPesan = false;
        JumlahChatDitemukan = 0;
        SearchText = "";
        DtPercakapanSearch.Clear();
        ChatAktif = null;
        StateHasChanged();
        lbxPercakapan.MakeItemVisible(indexPesan);
    }

    public string SearchText { get; set; }
    public DxTextBox TxbSearchChat { get; set; }
    public uimT7Pesan_Percakapan ChatAktif { get; set; } = null;
    async void OnSearchTextChanged(string newValue)
    {
        SearchText = newValue;
        if (string.IsNullOrEmpty(newValue))
        {
            DtPercakapanSearch.Clear();
            JumlahChatDitemukan = 0;
            ChatAktif = null;
            return;
        }
        DtPercakapanSearch = DtPercakapan.OrderBy(x => x.WaktuSend).Where(x => x.StatusSystem == false && x.IsiPesan.Replace("new-line", "").ToLower().Contains(newValue.ToLower())).ToList();

        JumlahChatDitemukan = DtPercakapanSearch.Count();
        if (JumlahChatDitemukan > 0) return;
    }
    public async void TampilkanPencarianSebelumnya()
    {
        if (ChatAktif == null)
        {
            var chatToFocus = DtPercakapanSearch.OrderBy(x => x.WaktuSend).FirstOrDefault();
            lbxPercakapan.MakeItemVisible(DtPercakapan.OrderBy(x => x.WaktuSend).ToList().IndexOf(chatToFocus));
            ChatAktif = chatToFocus;

        }
        else
        {
            var indexChatAktifSebelumnya = DtPercakapanSearch.OrderBy(x => x.WaktuSend).ToList().IndexOf(ChatAktif);
            if (indexChatAktifSebelumnya < 1) return;
            var chatToFocus = DtPercakapanSearch.OrderBy(x => x.WaktuSend).ToList()[indexChatAktifSebelumnya - 1];
            if (chatToFocus == null) return;
            JSR.InvokeVoidAsync("scrollToPesan", $"{chatToFocus.IdDetilPesan}");
            //lbxPercakapan.MakeItemVisible(DtPercakapan.OrderBy(x => x.WaktuSend).ToList().IndexOf(chatToFocus));
            ChatAktif = chatToFocus;
        }
        JSR.InvokeVoidAsync("highlightChat", $"{ChatAktif.IdDetilPesan}");
        StateHasChanged();
    }
    public async void TampilkanPencarianSelanjutnya()
    {
        if (ChatAktif == null)
        {
            var chatToFocus = DtPercakapanSearch.OrderBy(x => x.WaktuSend).LastOrDefault();
            lbxPercakapan.MakeItemVisible(DtPercakapan.OrderBy(x => x.WaktuSend).ToList().IndexOf(chatToFocus));
            ChatAktif = chatToFocus;
        }
        else
        {
            var indexChatAktifSebelumnya = DtPercakapanSearch.OrderBy(x => x.WaktuSend).ToList().IndexOf(ChatAktif);
            if (indexChatAktifSebelumnya + 1 >= DtPercakapanSearch.Count()) return;
            var chatToFocus = DtPercakapanSearch.OrderBy(x => x.WaktuSend).ToList()[indexChatAktifSebelumnya + 1];
            if (chatToFocus == null)
            {
                ChatAktif = null;
                return;
            }
            JSR.InvokeVoidAsync("scrollToPesan", $"{chatToFocus.IdDetilPesan}");
            ChatAktif = chatToFocus;
        }
        JSR.InvokeVoidAsync("highlightChat", $"{ChatAktif.IdDetilPesan}");
        StateHasChanged();
    }

    async void ScrollPesanTerkutip(Guid idDetilPesan)
    {

        var chat = DtPercakapan.FirstOrDefault(x => x.IdDetilPesan == idDetilPesan);
        if (chat == null)
        {
            ProsesMuatPesanLainnya(isSearchAktif: true);
            chat = DtPercakapan.FirstOrDefault(x => x.IdDetilPesan == idDetilPesan);
        }
        if (chat == null) return;
        JSR.InvokeVoidAsync("scrollToPesan", $"{idDetilPesan}");
        JSR.InvokeVoidAsync("highlightChat", $"{idDetilPesan}");
        StateHasChanged();
    }
    #endregion
    private string HighlightText(string text, string keyword, bool searchPesan = false)
    {
        if (string.IsNullOrEmpty(keyword))
            return text.Replace("new-line", "<br />");

        // Ganti dulu new-line ke placeholder agar tidak rusak saat di-highlight
        text = text.Replace("new-line", "___BR___");

        var regex = new System.Text.RegularExpressions.Regex(
            Regex.Escape(keyword),
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );

        string color = searchPesan ? "#409ADF" : "#ffa500";

        string highlightedText = regex.Replace(text, match =>
            $"<span style='background-color: {(!searchPesan ? color : "transparent")}; color: {(!searchPesan ? "" : color)};'>{match.Value}</span>"
        );

        // Ganti kembali placeholder ke <br />
        return highlightedText.Replace("___BR___", "<br />");
    }

    public string MessageText { get; set; } = "";
    private CancellationTokenSource _cts;

    private async void UpdateRows(ChangeEventArgs e)
    {
        MessageText = e.Value?.ToString() ?? "";

        await JSR.InvokeVoidAsync("autoResizeMemoUp", "memo-textarea-grow-up", "chat-list-box");
        //lbxPercakapan.MakeItemVisible(DtPercakapan.IndexOf(DtPercakapan.Last()) + 1);

        _cts?.Cancel(); // Batalkan request sebelumnya jika masih berjalan
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            await Task.Delay(200, token); // Tunggu 500ms sebelum fetch data

            if (!token.IsCancellationRequested)
            {
                await FetchPreview();
            }
        }
        catch (TaskCanceledException)
        {
            // Task dibatalkan, tidak perlu melakukan apa-apa
        }
    }

    private async Task FetchPreview()
    {
        if (string.IsNullOrWhiteSpace(MessageText))
        {
            DrLinkPreview = null;
            await InvokeAsync(StateHasChanged);
            return;
        }


        await InvokeAsync(StateHasChanged);
    }
    async void OnSearchContactChanged(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
        {
            DtContact_Ditampilkan = DtContact;
            StateHasChanged();
            return;
        }
        DtContact_Ditampilkan = DtContact.OrderBy(x => x.NamaPanggilan).Where(x => x.NamaPanggilan.ToLower().Contains(newValue.ToLower())).ToList();
        StateHasChanged();
    }
    public string TextValue = "";
    public async Task ProsesBukaPanelPesanBaru()
    {
        DtPesanAktif.StatusGrup = false;
        GrupSudahAda = null;
        TextValue = "";
        DtContact_Ditampilkan = DtContact;
        await Task.Delay(100);
        await TxbSearchContact1.FocusAsync();
        StateHasChanged();
    }


    #region Recording

    private bool IsRecording = false;
    private bool IsPaused = false;
    private string RecordingDuration = "00:00";
    private string Durasi = "00:00";
    private System.Timers.Timer? RecordingTimer;
    private int RecordingSeconds = 0;
    private void ProsesStartRecording()
    {
        IsRecording = true;
        StartRecordingTimer();
        JSR.InvokeVoidAsync("startRecording", DotNet, Guid.NewGuid().ToString());
    }

    private void StartRecordingTimer()
    {
        RecordingSeconds = 0;
        RecordingTimer = new System.Timers.Timer(1000);
        RecordingTimer.Elapsed += (s, e) =>
        {
            RecordingSeconds++;
            RecordingDuration = TimeSpan.FromSeconds(RecordingSeconds).ToString(@"mm\:ss");
            Durasi = RecordingDuration;
            InvokeAsync(StateHasChanged);
        };
        RecordingTimer.Start();
    }

    private async void ProsesStopRecording()
    {
        IsRecording = false;
        IsPaused = false;
        RecordingDuration = "00:00";
        RecordingTimer?.Stop();
        RecordingTimer?.Dispose();
        await JSR.InvokeVoidAsync("stopRecording");
    }

    private async void ProsesResetRecording()
    {
        IsRecording = false;
        if (IsRecording) ProsesStopRecording();
        if (!string.IsNullOrEmpty(AudioPreviewUrl))
        {
            // Pastikan ini adalah URL lokal seperti /uploads/recording.webm
            var localFilePath = Path.Combine("wwwroot", AudioPreviewUrl.TrimStart('/'));
            if (File.Exists(localFilePath)) File.Delete(localFilePath);

        }
        AudioPreviewUrl = null;
    }

    private string AudioPreviewUrl { get; set; }
    private string AudioUrl { get; set; } // ini hasil dari UploadAudioAsync
    private string? AudioBase64;
    private List<string> audioChunks;
    private int expectedChunks;

    [JSInvokable]
    public async Task SetAudioUrl(string audioUrl)
    {
        AudioPreviewUrl = audioUrl;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ProsesHasilRecording()
    {
        if (IsRecording) { ProsesStopRecording(); await Task.Delay(1000); }

        // Lanjutkan proses upload
        if (!string.IsNullOrEmpty(AudioPreviewUrl))
        {
            // Pastikan ini adalah URL lokal seperti /uploads/recording.webm
            var localFilePath = Path.Combine("wwwroot", AudioPreviewUrl.TrimStart('/'));
            var fileName = $"pesan-suara-{Guid.NewGuid()}.webm";

            if (File.Exists(localFilePath))
            {
                using (var fileStream = File.OpenRead(localFilePath))
                {
                    AudioUrl = await UploadDataToStorage(fileStream, fileName, "audio/webm");
                }
                // Hapus file lokal jika sudah diupload
                File.Delete(localFilePath);
            }
        }
    }
    #endregion

    #region Upload Icon Grup
    private async Task TriggerInputFile()
    {
        await JSR.InvokeVoidAsync("triggerInputFile", "input-grup-icon");

    }
    private IBrowserFile FilePreview;
    private async void ProsesUploadGambar(InputFileChangeEventArgs e)
    {
        FilePreview = e.File;
        // Optional: validasi file
        if (FilePreview == null || FilePreview.Size == 0)
            return;
        // Validasi ukuran maksimal 10 MB
        long maxSize = 10 * 1024 * 1024; // 10 MB
        if (FilePreview.Size > maxSize)
        {
            // Bisa pakai alert, toast, atau just console log
            Console.WriteLine("Ukuran file terlalu besar. Maksimal 10 MB.");
            return;
        }

        using var stream = FilePreview.OpenReadStream();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var base64 = Convert.ToBase64String(memoryStream.ToArray());
        DtPesanAktif.IconGrup = $"data:{FilePreview.ContentType};base64,{base64}";
        StateHasChanged();
    }


    #endregion
    #region 'Search Pesan'
    private DxTextBox txbSearchPesan;
    string SearchTextPesan { get; set; } = "";

    bool SearchPesanFocus = false;
    private string _filterTextPesan;
    public string FilterTextPesan
    {
        get => _filterTextPesan;
        set
        {
            if (_filterTextPesan != value)
            {
                _filterTextPesan = value;
                OnSearchPesanTextChanged(value);
            }
        }
    }
    async void OnSearchPesanTextChanged(string newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue))
        {
            DtPesan_Ditampilkan = DtPesan;
            SearchTextPesan = "";
            FilterTextPesan = "";
            StateHasChanged();
            return;
        }
        string keyword = newValue.ToLower();

        var hasil = new List<uimT6Pesan>();

        foreach (var pesan in DtPesan)
        {
            // Cek apakah nama grup cocok
            bool cocokGrup = pesan.StatusGrup && !string.IsNullOrEmpty(pesan.NamaGrup) && pesan.NamaGrup.ToLower().Contains(keyword);

            // Cek apakah ada anggota (selain user) yang cocok namanya (untuk chat pribadi)
            bool cocokAnggota = false;
            if (!pesan.StatusGrup)
            {
                var penerima = DtContact.FirstOrDefault(x => x.IdUser == pesan.ListT7Pesan_Anggota.FirstOrDefault(y => y.IdUser_Anggota != IdUser).IdUser_Anggota).NamaPanggilan.ToLower();
                if (penerima.Contains(keyword)) cocokAnggota = true;
            }

            if (cocokGrup || cocokAnggota)
            {
                pesan.Kategori = "Chats";
                hasil.Add(pesan);
            }
            else
            {
                if (keyword.Length > 1)
                {
                    // Ambil semua percakapan yang mengandung keyword
                    var percakapanCocok = pesan.ListT7Pesan_Percakapan?
                        .Where(p => p.StatusSystem == false && !string.IsNullOrEmpty(p.IsiPesan) && !p.IsiPesan.Contains("pesan-suara-") && p.IsiPesan.Replace("new-line", "").ToLower().Contains(keyword))
                        .ToList();

                    if (percakapanCocok != null)
                    {
                        SearchTextPesan = keyword;
                        percakapanCocok.ForEach(p =>
                        {
                            var clonePesan = pesan.Adapt<uimT6Pesan>();
                            clonePesan.Notes = p.IsiPesan.Replace("new-line", "");
                            clonePesan.WaktuProses = p.WaktuSend;
                            clonePesan.Kategori = "Messages";
                            hasil.Add(clonePesan);
                        });
                    }
                }
            }
        }
        var dtContact = DtContact.Where(x => x.IdUser != IdUser && !x.NamaPanggilan.Contains("UnitTest") && x.NamaPanggilan.ToLower().Contains(keyword)).ToList();
        if (dtContact != null && hasil.Where(x => x.Kategori == "Chats").Count() < 1)
        {
            dtContact.ForEach(p =>
            {
                var pesan = new uimT6Pesan();
                pesan.Notes = "";
                pesan.WaktuProses = null;
                pesan.Kategori = "Contacts";
                pesan.JenisPesan = p.IdUser.ToString();

                var listAnggota = new List<uimT7Pesan_Anggota>();
                listAnggota.Add(new uimT7Pesan_Anggota
                {
                    IdUser_Anggota = p.IdUser,
                });
                pesan.ListT7Pesan_Anggota = listAnggota.Adapt<ICollection<T7Pesan_Anggota>>();
                hasil.Add(pesan);

                // Cari pesan grup yang mengandung contact ini sebagai anggota
                var listGrup = DtPesan
                .Where(x => x.StatusGrup && x.ListT7Pesan_Anggota.Any(a => a.IdUser_Anggota == p.IdUser)).ToList();
                foreach (var grup in listGrup)
                {
                    if (!hasil.Any(x => x.IdPesan == grup.IdPesan))
                    {
                        var cloneGrup = grup.Adapt<uimT6Pesan>();
                        cloneGrup.Notes = $"{p.NamaPanggilan} juga ada di grup ini";
                        cloneGrup.Kategori = "Groups";
                        cloneGrup.WaktuProses = null;
                        hasil.Add(cloneGrup);
                    }
                }

            });
        }
        else
        {
            var pesan = hasil.Where(x => x.Kategori == "Chats").ToList();
            foreach (var chat in pesan)
            {
                var karyawan = DtContact.FirstOrDefault(x => x.IdUser == chat.ListT7Pesan_Anggota.FirstOrDefault(y => y.IdUser_Anggota != IdUser).IdUser_Anggota);
                if (karyawan.NamaPanggilan.ToLower().Contains(keyword))
                {
                    // Cari pesan grup yang mengandung contact ini sebagai anggota
                    var listGrup = DtPesan
                    .Where(x => x.StatusGrup && x.ListT7Pesan_Anggota.Any(a => a.IdUser_Anggota == karyawan.IdUser)).ToList();
                    foreach (var grup in listGrup)
                    {
                        if (!hasil.Any(x => x.IdPesan == grup.IdPesan))
                        {
                            var cloneGrup = grup.Adapt<uimT6Pesan>();
                            cloneGrup.Notes = $"{karyawan.NamaPanggilan} juga ada di grup ini";
                            cloneGrup.Kategori = "Groups";
                            cloneGrup.WaktuProses = null;
                            hasil.Add(cloneGrup);
                        }
                    }
                }
            }

        }
        DtPesan_Ditampilkan = hasil;

        StateHasChanged();
    }
    #endregion




    public async Task<string> UploadDataToStorage(Stream audioStream, string fileName, string contentType)
    {
        try
        {

            return "";
        }
        catch (Exception ex)
        {
            // Log error di sini kalau mau
            return "";
        }
    }

}