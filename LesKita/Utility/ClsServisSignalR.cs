using Microsoft.AspNetCore.SignalR.Client;

namespace LesKita;

public class ClsServisSignalR
{
    private HubConnection _hc { get; set; }

    public string Klien = ""; // GWR, SSC, GMA, ASD, JWI, ISJ, LMJ
    public string Divisi = ""; // Sales, Purchasing, Finance

    public ClsServisSignalR() // constructor
    {
        //var hch = new HttpClientHandler() { ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, System.Security.Cryptography.X509Certificates.X509Certificate2, System.Security.Cryptography.X509Certificates.X509Chain, System.Net.Security.SslPolicyErrors, bool>((sender, cert, chain, sslPolicyErrors) => true) };
        var URL_SignalR = "https://localhost:7068/signalrhub"; //Beri nilai default jika URL_SignalR null
        var hc = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri(URL_SignalR) };

        _hc = new HubConnectionBuilder().WithUrl(hc.BaseAddress).WithAutomaticReconnect().Build();
    }

    public async Task MulaiKoneksi(string klien)
    {
        Klien = klien;
        const int maxRetry = 10;
        int retryCount = 0;

        while (retryCount < maxRetry)
        {
            try
            {
                // Pastikan koneksi dalam keadaan benar-benar Disconnected
                await MemastikanKoneksiTerhenti();

                await _hc.StartAsync();
                await _hc.InvokeAsync("MulaiKoneksi", Klien);

                Console.WriteLine("Koneksi berhasil.");
                break; // Berhasil, keluar dari loop
            }
            catch (HttpRequestException ex)
            {
                retryCount++;
                Console.WriteLine($"[Retry {retryCount}] HttpRequestException: {ex.Message}");
            }
            catch (InvalidOperationException ex) when (_hc.State != HubConnectionState.Disconnected)
            {
                retryCount++;
                Console.WriteLine($"[Retry {retryCount}] InvalidOperationException: Hub masih dalam state {_hc.State}, menunggu...");
            }
            catch (Exception ex)
            {
                retryCount++;
                Console.WriteLine($"[Retry {retryCount}] General Error: {ex.Message}");
            }

            await Task.Delay(1000); // Tunggu 1 detik sebelum retry
        }
    }
    public async Task MemastikanKoneksiTerhenti()
    {
        if (_hc is null) return;

        if (_hc.State == HubConnectionState.Connected)
        {
            try
            {
                await _hc.InvokeAsync("StopKoneksi", Klien);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal kirim StopKoneksi ke server: {ex.Message}");
            }
        }

        if (_hc.State != HubConnectionState.Disconnected)
        {
            try
            {
                await _hc.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal memanggil StopAsync: {ex.Message}");
            }
        }

        // Tunggu hingga benar-benar Disconnected
        while (_hc.State != HubConnectionState.Disconnected)
        {
            await Task.Delay(200);
        }
    }
    public async Task MemastikanKoneksi()
    {
        while (_hc.State == HubConnectionState.Connecting || _hc.State == HubConnectionState.Reconnecting)
        {
            await Task.Delay(200);
        }

        if (_hc.State == HubConnectionState.Connected)
        {
            return;
        }

        if (_hc.State == HubConnectionState.Disconnected)
        {
            await _hc.StartAsync();
            await _hc.InvokeAsync("MulaiKoneksi", Klien);
        }
    }
    public async Task StopKoneksi()
    {
        try
        {
            if (_hc is null) return;

            if (_hc.State == HubConnectionState.Connected)
            {
                // Panggil StopKoneksi dari server jika masih terkoneksi
                await _hc.InvokeAsync("StopKoneksi", Klien);
            }

            if (_hc.State != HubConnectionState.Disconnected)
            {
                await _hc.StopAsync();
            }

            Console.WriteLine("Koneksi berhasil dihentikan.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal menghentikan koneksi: {ex.Message}");
            // Bisa di-log atau dilewatkan, tergantung pentingnya
        }
    }

    public async Task KirimPesan(string jenisPesan, string namaForm, string statusAction, string namaFieldPK, object nilaiPK, DateTimeOffset waktuProses, string isiPesan, Guid idUser, string User_NamaPanggilan, Guid idUserPenerima, string userPenerima_NamaPanggilan)
    {
        await MemastikanKoneksi();

        var psr = new PesanSignalR()
        {
            Klien = Klien,
            Divisi = null,
            IdUser = idUser,
            UserPengirim_NamaPanggilan = User_NamaPanggilan,
            IdSignal = Guid.NewGuid(),
            NamaForm = namaForm,
            StatusAction = statusAction,
            NamaFieldPK = namaFieldPK,
            NilaiPK = nilaiPK,
            WaktuProses = waktuProses,
            IsiPesan = isiPesan,
            JenisPesan = jenisPesan,
            IdUser_Penerima = idUserPenerima,
            UserPenerima_NamaPanggilan = userPenerima_NamaPanggilan,
        };

        const int maxRetry = 10;
        int tryCount = 0;
        while (tryCount < maxRetry)
        {
            try
            {
                if (_hc.State == HubConnectionState.Connected)
                {
                    await _hc.InvokeAsync("KirimPesan", psr);
                    return;
                }
            }
            catch (Exception ex)
            {
                tryCount++;
                Console.WriteLine($"Gagal kirim pesan (percobaan {tryCount}): {ex.Message}");
                await Task.Delay(1000);
            }
        }

        Console.WriteLine("Gagal mengirim pesan setelah 3 percobaan.");
    }

    private Action<PesanSignalR>? _pesanHandler;
    public void TerimaPesan(Action<PesanSignalR> a_psr, bool isBroadcast = false)
    {
        _pesanHandler = a_psr;

        void PasangHandler()
        {
            _hc.Remove("KirimPesan");
            _hc.Remove("KirimPesanBroadcast");
            _hc.Remove("TerimaPesan");

            if (isBroadcast)
                _hc.On("KirimPesanBroadcast", _pesanHandler);
            else
                _hc.On("KirimPesan", _pesanHandler);

            _hc.On("TerimaPesan", _pesanHandler);
        }

        PasangHandler();

        _hc.Reconnected += connectionId =>
        {
            Console.WriteLine("Reconnected, memasang ulang handler.");
            PasangHandler();
            return Task.CompletedTask;
        };
    }

    
}

