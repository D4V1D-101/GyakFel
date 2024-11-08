using System;

namespace GyakFel
{
    internal class Adatok
    {
        public Adatok(string r)
        {
            // Példa: 2; nő; 1990; 65; 170; nem; német; szász ; Baden - Württemberg; 40000; Realschule; liberális; igen; 30; NA
            var v = r.Split(';');
            Id = int.Parse(v[0]);
            Gender = v[1];
            BirthDate = int.Parse(v[2]);
            Weight = int.Parse(v[3]);
            Height = int.Parse(v[4]);
            Smoking = v[5] == "igen";
            Nemzetiseg = v[6];
            Nepcsoport = v[7] != "német" ? null : v[7]; 
            Tartomany = v[8];
            Netto = int.Parse(v[9]);
            Vegzettseg = v[10];
            PolitikaiNezet = v[11];
            AktivSzavazo = v[12] == "igen";
            SorFogyasztas = v[13] == "NA" ? (int?)null : int.Parse(v[13]);
            KrumpliFogyasztas = v[14] == "NA" ? (int?)null : int.Parse(v[14]);
        }

        public int Id { get; set; }
        public string Gender { get; set; }
        public int BirthDate { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public bool Smoking { get; set; }
        public string Nemzetiseg { get; set; }
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int Netto { get; set; }
        public string Vegzettseg { get; set; }
        public string PolitikaiNezet { get; set; }
        public bool AktivSzavazo { get; set; }
        public int? SorFogyasztas { get; set; } 
        public int? KrumpliFogyasztas { get; set; }  

        public override string ToString()
        {
            return $"[{Id}] {Gender} {BirthDate} {Weight} {Height} {(Smoking ? "igen" : "nem")} {Nemzetiseg} {Nepcsoport} {Tartomany} {Netto} {Vegzettseg} {PolitikaiNezet} {(AktivSzavazo ? "igen" : "nem")} {SorFogyasztas?.ToString() ?? "NA"} {KrumpliFogyasztas?.ToString() ?? "NA"}";
        }
    }
}
