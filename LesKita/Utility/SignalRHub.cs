using Microsoft.AspNetCore.SignalR;

namespace LesKita;

public class SignalRHub : Hub
{
    public async Task MulaiKoneksi(string Klien)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Klien); //memasukkan id koneksi ke grup
    }

    public async Task KirimPesan(PesanSignalR psr)
    {

        //await Clients.Client(Context.ConnectionId).SendAsync("KirimPesan",psr); //kirim pesan ke diri sendiri

        await Clients.OthersInGroup(psr.Klien).SendAsync("KirimPesan", psr); //kirim pesan ke divisi yang sama
    }

    public async Task KirimPesanBroadcast(PesanSignalR psr)
    {
        await Clients.Client(Context.ConnectionId).SendAsync("KirimPesanBroadcast", psr); //kirim pesan ke diri sendiri
        await Clients.All.SendAsync("KirimPesanBroadcast", psr); //kirim pesan broadcast ke semua orang
    }

    public async Task StopKoneksi(string klien)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, klien); //menghapus id koneksi dari grup
    }

    
}
