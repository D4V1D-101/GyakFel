using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GyakFel
{
    public partial class MainWindow : Window
    {
        List<Adatok> Lakossag = new List<Adatok>();
        private Dictionary<int, Action> F;

        public MainWindow()
        {
            InitializeComponent();

            
            using StreamReader sr = new($"../../../src/bevolkerung.txt", Encoding.UTF8);
            string line = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                Lakossag.Add(new Adatok(sr.ReadLine()));
            }

            
            for (int i = 1; i <= 45; i++)
            {
                listaCombo.Items.Add(i);
            }

            
            F = new Dictionary<int, Action>();
            #region hosszu pelda
        //    F = new Dictionary<int, Action>
        //    {
        //        { 1, f1 },
        //        { 2, f2 },
        //        { 3, f3 },
        //        { 4, f4 },
        //        { 5, f5 },
        //        { 6, f6 },
        //        { 7, f7 },
        //        { 8, f8 },
        //        { 9, f9 },
        //        { 10, f10 },
        //        { 11, f11 },
        //        { 12, f12 },
        //        { 13, f13 },
        //        { 14, f14 },
        //        { 15, f15 },
        //        { 16, f16 },
        //        { 17, f17 },
        //        { 18, f18 },
        //        { 19, f19 },
        //        { 20, f20 },
        //        { 21, f21 },
        //        { 22, f22 },
        //        { 23, f23 },
        //        { 24, f24 },
        //        { 25, f25 },
        //        { 26, f26 },
        //        { 27, f27 },
        //        { 28, f28 },
        //        { 29, f29 },
        //        { 30, f30 },
        //        { 31, f31 },
        //        { 32, f32 },
        //        { 33, f33 },
        //        { 34, f34 },
        //        { 35, f35 },
        //        { 36, f36 },
        //        { 37, f37 },
        //        { 38, f38 },
        //        { 39, f39 },
        //        { 40, f40 },
        //        //{ 41, f41 },
        //        //{ 42, f42 },
        //        //{ 43, f43 },
        //        //{ 44, f44 },
        //        //{ 45, f45 }



        //};
        //}
        #endregion
        var methods = this.GetType()
                              .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                              .Where(m => m.Name.StartsWith("f") && int.TryParse(m.Name.Substring(1), out _))
                              .OrderBy(m => int.Parse(m.Name.Substring(1)));

            foreach (var method in methods)
            {
                int taskNumber = int.Parse(method.Name.Substring(1));
                F[taskNumber] = (Action)Delegate.CreateDelegate(typeof(Action), this, method);
            }
        }

        private void Feladatok(object sender, SelectionChangedEventArgs e)
        {
            Clear();
            if (listaCombo.SelectedItem is int selectedNumber && F.ContainsKey(selectedNumber))
            {
                F[selectedNumber].Invoke();
            }
        }

        private void Clear()
        {
            MegoldasLista.Items.Clear();
            MegoldasMondatban.Content = "";
            MegoldasTeljes.Items.Clear();
        }


        private void f1()
        {
            var max = Lakossag.MaxBy(l => l.Netto);
            MegoldasMondatban.Content = $"A legnagyobb nettó éves jövedelem: {max.Netto}";
        }
        private void f2()
        {
            var f = Lakossag.Average(l => l.Netto);
            MegoldasMondatban.Content = $"A lakosok átlaeg nettő jövedelme: {f}";
        }
        private void f3()
        {
            var f = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(g => g.Key, g => new
            {
                szam = g.Key.Count()
            });
            foreach (var item in f)
            {
                MegoldasLista.Items.Add($"{item.Key} : {item.Value.szam}");
            }

        }
        private void f4()
        {
            var f = Lakossag.Where(l => l.Nemzetiseg == "angolai").ToList();
            foreach (var item in f)
            {
                MegoldasTeljes.Items.Add(item);
            }

        }
        public void f5()
        {
            var f = Lakossag.Where(l => l.Age().Equals(Lakossag.Min(l => l.Age())));
            foreach (var j in f)
            {
                MegoldasTeljes.Items.Add(j);
            }
        }
        public void f6()
        {
            var f = Lakossag.Where(l => l.Smoking == false);
            foreach (var item in f)
            {
                MegoldasLista.Items.Add($"{item.Id} : {item.nettoHavi()}");
            }

        }
        public void f7()
        {
            var f = Lakossag.Where(l => l.Tartomany == "Bajorország" && l.Netto > 30000).OrderBy(l => l.Vegzettseg).ToList();
            foreach (var j in f)
            {
                MegoldasTeljes.Items.Add(f);
            }

        }
        public void f8()
        {
            var f = Lakossag.Where(l => l.Gender == "férfi");
            foreach (var item in f) MegoldasLista.Items.Add(item.ToString(true));
        }
        public void f9()
        {
            var f = Lakossag.Where(l => l.Gender == "nő" && l.Tartomany == "Bajorország");
            foreach (var item in f) MegoldasLista.Items.Add(item.ToString(false));
        }
        public void f10()
        {
            var f = Lakossag.Where(l => l.Smoking == false).OrderBy(l => l.Netto).ToList();

            for (int i = 0; i < 10; i++)
            {
                MegoldasTeljes.Items.Add(f[i]);
            }
        }
        public void f11()
        {
            var f = Lakossag.OrderByDescending(l => l.Age()).ToList();
            for (int i = 0; i < 5; i++)
            {
                MegoldasTeljes.Items.Add(f[i]);
            }
        }
        public void f12()
        {
            var f = Lakossag.Where(l => l.Nemzetiseg == "német").OrderBy(l => l.Tartomany).DistinctBy(l => l.Tartomany);
            
            foreach (var j in f)
            {
                MegoldasLista.Items.Add(j.Tartomany);
                var f2 = f.Where(f => f.Tartomany.Equals(j.Tartomany)).ToList();
                foreach (var k in f2) MegoldasLista.Items.Add(k.Id + " - " + (k.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó"));

            }

        }
        public void f13()
        {
            var f = Lakossag.Where(l=>l.Gender == "férfi");
            MegoldasMondatban.Content = $"étlagos évi féfi sörfogyasztás: {f.Average(l=>l.SorFogyasztas):0.00} l";

        }
        public void f14() 
        {
            var f = Lakossag.OrderBy(l => l.Vegzettseg).ToList();
            foreach (var j in f) MegoldasTeljes.Items.Add(j);
        }
        public void f15()
        {
            var fmin = Lakossag.OrderBy(l => l.Netto).ToList();
            var fmax = Lakossag.OrderByDescending(l => l.Netto).ToList();
            for (int i = 0; i < 3; i++)
            {
                MegoldasLista.Items.Add(fmin[i].ToString(false));
                MegoldasLista.Items.Add(fmax[i].ToString(false));
            }
        }
        public void f16()
        {
            
            var f = Lakossag.Where(l => l.AktivSzavazo == true).ToList();
            MegoldasMondatban.Content = $"Ennyi százaléka az aktív szavazóknak: {((double)f.Count() / Lakossag.Count()) * 100:0.0} %";
        }
        public void f17() 
        {
            var f = Lakossag.Where(l => l.AktivSzavazo).OrderBy(l => l.Tartomany);
            foreach (var j in f) MegoldasTeljes.Items.Add(j);
        }
        public void f18()
        {
            var f = Lakossag.Average(l => l.Age());
            MegoldasMondatban.Content = $"Az állampolgárok átlagos életkora: {(double)f:0}";
        }
        public void f19()
        {
            var grouping = Lakossag.GroupBy(l => l.Tartomany).Select(g => new
            {
                tartomany = g.Key,
                nettoAvarage = g.Average(l=>l.Netto),
                db = g.Count()

            }).ToList();
            var maxNetto = grouping.Max(g=>g.nettoAvarage);
            var f = grouping.Where(l=>l.nettoAvarage == maxNetto).OrderByDescending(g=>g.db).First();
            MegoldasMondatban.Content = $"{f.tartomany} - {f.nettoAvarage}";

        }
        public void f20()
        {
            var f = Lakossag.Select(l => l.Weight).OrderBy(w => w).ToList();
            var avgWeight = f.Average();
            double median = f.Count % 2 == 0 ? (f[f.Count / 2 - 1] + f[f.Count / 2]) / 2
                : f[f.Count / 2];

            MegoldasMondatban.Content = $"A lakosok átlag súlya: {avgWeight:0.0}, a medián pedig: {median}";

        }
        public void f21() 
        {
            var aktiv = Lakossag.Where(l => l.AktivSzavazo == true).Sum(l=>l.SorFogyasztas);
            var nemaktiv = Lakossag.Where(l => l.AktivSzavazo == false).Sum(l => l.SorFogyasztas);

            MegoldasMondatban.Content = aktiv>nemaktiv?"az aktív szavazók több sirt isznak": "a nem aktív szavazók több sirt isznak";

        }
        public void f22()
        {
            var ferfiAtlagMagassag = Lakossag.Where(l => l.Gender.Equals("férfi")).Average(l => l.Height);
            var noiAtlagMagassag = Lakossag.Where(l => l.Gender.Equals("nő")).Average(l => l.Height);
            MegoldasMondatban.Content = $"A férfiak átlag magassága {ferfiAtlagMagassag:0} cm a nők átlag magassága {noiAtlagMagassag:0} cm";
        }
       


        public void f23()
        {
            var f = Lakossag.Where(l => l.Nepcsoport != null).GroupBy(l => l.Nepcsoport).ToDictionary(l => l.Key, l => l.Count());
            MegoldasMondatban.Content = $"A {f.First().Key} népcsoportba tartoznak a legtöbben: {f.First().Value} fő";
        }
        public void f24()
        {
            var f1 = Lakossag.Where(l => l.Smoking == true).Average(l=>l.Netto);
            var f2 = Lakossag.Where(l => l.Smoking == false).Average(l => l.Netto);
            MegoldasMondatban.Content = f1>f2? $"a dohányzók éves átlag jövedelme több mint a nem dohányzőké {f1:0} eur" : $"a nem dohányzók éves átlag jövedelme több mint a nem dohányzőké: {f2:0} eur" ;
        }
        public void f25()
        {
            var atlagkrumplif = Lakossag.Average(l => l.KrumpliFogyasztas);
            var f = Lakossag.Where(l => l.KrumpliFogyasztas > atlagkrumplif).ToList();
            for (int i = 0; i < 15; i++)
            {
                MegoldasTeljes.Items.Add(f[i]);
            }

        }
        public void f26() 
        {
            var f = Lakossag.GroupBy(l => l.Tartomany).Select(g => new {
                tartomany = g.Key,
                avgAge = g.Average(l=>l.Age())
            }).ToList();
            foreach (var j in f)
            {
                MegoldasLista.Items.Add($"{j.tartomany} - {j.avgAge:0}");
            }
        }
        public void f27()
        {
            var f = Lakossag.Where(l => l.Age() > 50).ToList();
            foreach (var j in f) 
            {
                MegoldasLista.Items.Add(j.ToString(true));
            } 
            MegoldasLista.Items.Add(f.Count());
        }
        public void f28()
        {
            var dohanyozoNok = Lakossag.Where(l => l.Gender == "nő" && l.Smoking).ToList();

            if (dohanyozoNok.Any())
            {
                var maxNetto = dohanyozoNok.Max(l => l.Netto);
                foreach (var no in dohanyozoNok)
                {
                    MegoldasLista.Items.Add(no.ToString(false));
                }
                MegoldasMondatban.Content = $"Maximális nettó éves jövedelem: {maxNetto} Ft";
            }
            else
            {
                MegoldasMondatban.Content = "Nincs dohányzó nő az adatbázisban.";
            }
        }
        public void f29()
        {
            var f = Lakossag
                .GroupBy(l => l.Tartomany)
                .Select(g => new
                {
                    Tartomany = g.Key,
                    LegnagyobbSorFogyaszto = g.OrderByDescending(l => l.SorFogyasztas).First()
                })
                .ToList();

            foreach (var j in f)
            {
                if (j.LegnagyobbSorFogyaszto != null)
                {
                    MegoldasLista.Items.Add(
                        $"{j.Tartomany} - {j.LegnagyobbSorFogyaszto.Id}"
                    );
                }
            }
        }
        public void f30()
        {
            var legidosebbno = Lakossag.Where(l=>l.Gender == "nő").OrderByDescending(l => l.Age()).First();
            var legidosebbferfi = Lakossag.Where(l => l.Gender == "férfi").OrderByDescending(l => l.Age()).First();
            MegoldasLista.Items.Add(legidosebbno.ToString(true));
            MegoldasLista.Items.Add(legidosebbferfi.ToString(true));
        }
        public void f31()
        {
            var f = Lakossag.DistinctBy(l=>l.Nemzetiseg).OrderByDescending(l=>l.Nemzetiseg).ToList();
            foreach (var j in f)
            {
                MegoldasLista.Items.Add(j.Nemzetiseg);
            }

        }
        public void f32()
        {
            var f = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.Key.Count()).OrderByDescending(g => g.Value).ToList();
            foreach (var j in f)
            {
                MegoldasLista.Items.Add(j.Key);
            }

        }
        public void f33()
        {
            var f = Lakossag.OrderByDescending(l => l.Netto).ToList();
            for (int i = 0; i < 3; i++)
            {
                MegoldasLista.Items.Add($"{f[i].Id} - {f[i].Netto}");
            }
        }
        public void f34() 
        {
            
            var f1 = Lakossag.Where(l => l.Gender == "férfi" && l.KrumpliFogyasztas > 55).Average(l=>l.Weight);
            
            MegoldasMondatban.Content = f1;
            
        }
        public void f35()
        {
            var f = Lakossag.GroupBy(l => l.Tartomany).Select(l => new
            {
                Lakossag = l.Key,
                minEletkor = l.OrderBy(l => l.Age()).First(),
            }).ToList();

            foreach (var j in f)
            {
                MegoldasLista.Items.Add($"{j.Lakossag} - {j.minEletkor.Age()}");
            }


        }
        public void f36()
        {
            var f = Lakossag.Select(l => new
            {
                nemzetiseg = l.Nemzetiseg,
                tartomany = l.Tartomany,


            }).Distinct().OrderBy(l=>l.nemzetiseg).ToList();
            foreach (var j in f)
            {
                MegoldasLista.Items.Add($"{j.nemzetiseg} - {j.tartomany}");
            }
            MegoldasMondatban.Content = f.Count();
                
        }
        public void f37() 
        {
            var avgJov = Lakossag.Average(l => l.Netto);
            var f = Lakossag.Where(l => l.Netto > avgJov).ToList();
            foreach (var j in f)
            {
                MegoldasLista.Items.Add(j.ToString(false));
            }
        
        }
        public void f38()
        {
            var Fc = Lakossag.Where(l => l.Gender == "férfi").Count();
            var Nc = Lakossag.Where(l => l.Gender == "nő").Count();
            MegoldasMondatban.Content = $"f: {Fc} n: {Nc}";

        }
        public void f39()
        {
            var f = Lakossag.GroupBy(l => l.Tartomany).Select(g => new
             {
                 tartomany = g.Key,
                 legmagasabb = g.OrderByDescending(l => l.Netto).FirstOrDefault()
             }).OrderByDescending(x => x.legmagasabb.Netto) .ToList();

            foreach (var j in f)
            {
                MegoldasLista.Items.Add($"{j.tartomany} - {j.legmagasabb.Netto}");
            }
        }
        public void f40()
        {
            var n = Lakossag.Where(l => l.Nemzetiseg == "német").Sum(l => l.nettoHavi()) / Lakossag.Sum(l => l.nettoHavi()) * 100;
            var nn = Lakossag.Where(l => l.Nemzetiseg != "német").Sum(l => l.nettoHavi()) / Lakossag.Sum(l => l.nettoHavi()) * 100;
            MegoldasMondatban.Content = $"német havi: {n:0.0} %, nem német havi: {nn:0.0} %";

        }

        public void f43()
        {
            double avg = Lakossag.Average(l => l.Netto);
            var f = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MinBy(l => l.Netto).Netto).Where(l => l.Value > avg);
            MegoldasLista.Items.Add($"Az átlag: {avg:0.00}");
            foreach (var j in f)
            {
                MegoldasLista.Items.Add($"{j.Key} - {j.Value}");
            }

        }

        public void f44()
        {
            var feladat = Lakossag.Where(l => l.Vegzettseg == null).ToList();
            for (int i = 0; i < 3; i++)
            {
                var item = feladat[Random.Shared.Next(0, feladat.Count())];
                MegoldasTeljes.Items.Add(item);
                feladat.Remove(item);
            }
        }

        public void f45()
        {
            var f = Lakossag.Where(l => l.Gender == "nő" && l.Vegzettseg == "Universität" && !l.Nemzetiseg.Equals("bajor")).ToList();
            for (int i = 0; i < 5; i++) 
            {
                MegoldasTeljes.Items.Add(f[i]);
             }
            var feladat2 = Lakossag.Where(l => l.Gender == "nő" && l.Nemzetiseg == "német" && l.Netto > f.First().Netto).ToList();
            for (int i = 0; i < 3; i++)
            {
                var item = f[Random.Shared.Next(0, f.Count())];
                MegoldasLista.Items.Add(item.ToString(false));
                f.Remove(item);
            }
        }


    }
}


       