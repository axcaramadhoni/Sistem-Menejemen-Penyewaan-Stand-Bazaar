using System;
using System.Collections.Generic;
using System.Linq;

class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand(string namaStand, double hargaSewaPerHari)
    {
        NamaStand = namaStand;
        HargaSewaPerHari = hargaSewaPerHari;
        _isAvailable = true;
    }

    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Nama stand tidak boleh kosong!");
            _namaStand = value;
        }
    }

    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value <= 0)
                throw new Exception("Harga harus lebih dari 0!");
            _hargaSewaPerHari = value;
        }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
    }

    public void DisplayInfo()
    {
        string status = _isAvailable ? "Tersedia" : "Disewa";
        Console.WriteLine("Nama Stand : " + _namaStand);
        Console.WriteLine("Harga/Hari : Rp" + _hargaSewaPerHari);
        Console.WriteLine("Status     : " + status);
    }

    public void UbahStatus()
    {
        if (_isAvailable == true)
            _isAvailable = false;
        else
            _isAvailable = true;
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        return _hargaSewaPerHari * jumlahHari;
    }
}

class StandOutdoor : Stand
{
    protected double _biayaTenda = 75000;

    public StandOutdoor(string namaStand, double hargaSewaPerHari)
        : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaTenda
    {
        get { return _biayaTenda; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        double total = (_hargaSewaPerHari * jumlahHari) + (_biayaTenda * jumlahHari);
        return total;
    }
}

class StandIndoor : Stand
{
    protected double _biayaListrik = 100000;

    public StandIndoor(string namaStand, double hargaSewaPerHari)
        : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        double total = (_hargaSewaPerHari * jumlahHari) + (_biayaListrik * jumlahHari);
        return total;
    }
}

class StandPremium : Stand
{
    protected double _biayaKeamanan = 300000;

    public StandPremium(string namaStand, double hargaSewaPerHari)
        : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaKeamanan
    {
        get { return _biayaKeamanan; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        double total = (_hargaSewaPerHari * jumlahHari) + _biayaKeamanan;
        return total;
    }
}

class Program
{
    static List<Stand> daftarStand = new List<Stand>();

    static void Main(string[] args)
    {
        daftarStand.Add(new StandOutdoor("Outdoor-1", 400000));
        daftarStand.Add(new StandOutdoor("Outdoor-2", 500000));
        daftarStand.Add(new StandIndoor("Indoor-1", 700000));
        daftarStand.Add(new StandIndoor("Indoor-2", 800000));
        daftarStand.Add(new StandPremium("Premium-1", 1800000));
        daftarStand.Add(new StandPremium("Premium-2", 2000000));

        string pilihan = "";
        while (pilihan != "3")
        {
            Console.Clear();
            Console.WriteLine("=== Moklet Expo Management Center ===");
            Console.WriteLine("Daftar Stand Tersedia");
            Console.WriteLine();

            for (int i = 0; i < daftarStand.Count; i++)
            {
                if (daftarStand[i].IsAvailable == true)
                {
                    Console.WriteLine(daftarStand[i].NamaStand + "\t\t| Rp " + daftarStand[i].HargaSewaPerHari + " / hari\t| Tersedia");
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. Sewa Stand");
            Console.WriteLine("2. Akhiri Sewa Stand");
            Console.WriteLine("3. Keluar");
            Console.Write("\nPilih Menu: ");
            pilihan = Console.ReadLine();

            if (pilihan == "1")
            {
                SewaStand();
            }
            else if (pilihan == "2")
            {
                AkhiriSewa();
            }
            else if (pilihan == "3")
            {
                Console.WriteLine("\nTerima kasih...");
                Console.WriteLine("Tekan ENTER...");
                Console.ReadLine();
            }
        }
    }

    static void SewaStand()
    {
        Console.Write("\nMasukkan nama stand: ");
        string nama = Console.ReadLine();

        Stand stand = daftarStand.FirstOrDefault(s => s.NamaStand.ToLower() == nama.ToLower());

        if (stand == null)
        {
            Console.WriteLine("Stand tidak ditemukan.");
            Console.WriteLine("Tekan ENTER...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Stand ditemukan: " + stand.NamaStand + " | Rp " + stand.HargaSewaPerHari + " / hari");

        if (stand.IsAvailable == false)
        {
            Console.WriteLine("Stand sedang tidak tersedia.");
            Console.WriteLine("Tekan ENTER...");
            Console.ReadLine();
            return;
        }

        Console.Write("Masukkan jumlah hari: ");
        int jumlahHari = int.Parse(Console.ReadLine());

        double totalBiaya = stand.HitungTotal(jumlahHari);
        Console.WriteLine("\nTotal Biaya: Rp " + totalBiaya);

        stand.UbahStatus();
        Console.WriteLine("\nStand " + stand.NamaStand + " berhasil disewakan selama " + jumlahHari + " hari");
        Console.WriteLine("Tekan ENTER...");
        Console.ReadLine();
    }

    static void AkhiriSewa()
    {
        Console.WriteLine("\nDaftar Stand yang Sedang Disewakan");

        bool ada = false;
        for (int i = 0; i < daftarStand.Count; i++)
        {
            if (daftarStand[i].IsAvailable == false)
            {
                Console.WriteLine(daftarStand[i].NamaStand + "\t\t| Rp " + daftarStand[i].HargaSewaPerHari + " / hari\t| Tidak tersedia");
                ada = true;
            }
        }

        if (ada == false)
        {
            Console.WriteLine("Tidak ada stand yang sedang disewa.");
            Console.ReadLine();
            return;
        }

        Console.Write("\nMasukkan nama stand: ");
        string nama = Console.ReadLine();

        Stand stand = daftarStand.FirstOrDefault(s => s.NamaStand.ToLower() == nama.ToLower());

        if (stand == null)
        {
            Console.WriteLine("Stand tidak ditemukan.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Stand ditemukan: " + stand.NamaStand + " | Rp " + stand.HargaSewaPerHari + " / hari");

        if (stand.IsAvailable == true)
        {
            Console.WriteLine("Stand belum disewa.");
            Console.ReadLine();
            return;
        }

        stand.UbahStatus();
        Console.WriteLine("\nSewa stand berhasil diakhiri.");
        Console.WriteLine("Tekan ENTER...");
        Console.ReadLine();
    }
}
