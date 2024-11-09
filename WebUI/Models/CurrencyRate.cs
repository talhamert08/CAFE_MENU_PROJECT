using System.Text.Json.Serialization;

namespace WebUI.Models
{
    public class Developer
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
    }

    public class CurrencyRate
    {
        public string Isim { get; set; } // Türkçe isim
        public string CurrencyName { get; set; } // İngilizce isim
        public object ForexBuying { get; set; } // Döviz alış kuru
        public object ForexSelling { get; set; } // Döviz satış kuru
        //public object? BanknoteBuying { get; set; } // Efektif alış kuru (optional)
        //public object? BanknoteSelling { get; set; } // Efektif satış kuru (optional)
        //public object? CrossRateUSD { get; set; } // Çapraz kur USD (optional)
        //public object? CrossRateOther { get; set; } // Diğer çapraz kur (optional)
    }

    public class CurrencyApiResponse
    {
        public Developer Developer { get; set; }
        public List<CurrencyRate> TCMB_AnlikKurBilgileri { get; set; }
    }

}
