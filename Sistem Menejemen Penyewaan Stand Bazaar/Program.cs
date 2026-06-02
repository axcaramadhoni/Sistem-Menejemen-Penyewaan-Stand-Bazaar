

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

