using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reel_hafta_3_cumartesi_part3_proje
{
    public class Program
    {
        
        public static List<urun> urunler;
        public static List<Satis> satislar = new List<Satis>();
        static void Main(string[] args)
        {
            Menu();
            Console.ReadKey();
        }
        #region yardımcımetotlar
        public static void ekrantemizle()
        {
            Console.Clear();
            header();
        }
        public static void header()
        {
            Console.WriteLine("Kaşıkçılar Manavı ");
            Console.WriteLine("-------------------------");
        }
        public static void Menu()
        {
            ekrantemizle();
            Console.WriteLine("[0] - Çıkış");
            Console.WriteLine("[1] - Listele ");
            Console.WriteLine("[2] - Ekle ");
            Console.WriteLine("[3] - Düzenle ");
            Console.WriteLine("[4] - Sil ");
            Console.WriteLine("[5] - Satış Yap");
            Console.WriteLine("[6] - Satış Listesi");
            Console.Write("Lütfen işlen seçin : ");

            var islem = Console.ReadLine();
            switch(islem)
            {
                case "1":
                    urunlerilistele();
                        break;
                case "2":
                    urunekle();
                    break;
                case"3":
                    urunguncelle();
                    break;
                case "4":
                    urunsil();
                    break;
                case "0 ":
                    cikis();
                    break;
                case "5":
                    satisyap();
                    break;
                case "6":
                    satislistesi();
                    break;
                    default:
                    Console.WriteLine("Hata işlem seçildi. ");
                    break;
            }
        }
        public static void menuyedon()
        {
            Console.Write("Menüye dönmek için lütfen bir tuşa basınız. ");
            Console.ReadKey();
            Menu();
        }
        #endregion

        #region ürün işlemleri

        public static void urunekle()
        {

                Console.Clear();
                Console.WriteLine("Yeni Ürün ");
                Console.WriteLine("-------------------------");
                var yeniurun = new urun();
                Console.Write("Ürün Adı : ".PadRight(15));
                yeniurun.urunadi = Console.ReadLine();
                Console.Write("Fiyat Girin : ".PadRight(15));
                yeniurun.fiyat = Convert.ToDouble(Console.ReadLine());
                if (urunler == null)
                urunlistesiolustur();
                urunler.Add(yeniurun);
                
                urunlerilistele($"{yeniurun.urunadi} eklendi.");

        }

        public static void urunguncelle()
        {
            urunlerilistele("Ürün güncelleme - Ürün Listesi",true);
            Console.Write("Güncellemek istediğiniz ürünün adını giriniz : ");
            var urunadi = Console.ReadLine();
            var urun=urunler.FirstOrDefault(f=>f.urunadi==urunadi);
            if (urun==null)
            {
                Console.WriteLine("Ürün bulunamadı. ");
                
            }
            Console.Write("Fiyat girin : ");
            urun.fiyat = Convert.ToDouble(Console.ReadLine());
            urunlerilistele($"{urun.urunadi}'nin güncel fiyatı {urun.fiyat}");
        }
        public static void urunsil()
        {
            urunlerilistele("Ürün Silme - Ürün Listesi", true);
            Console.Write("Silmek istediğiniz ürünün adını giriniz : ");
            var urunkodu = Console.ReadLine();
            var urun = urunler.FirstOrDefault(f => f.urunkodu == urunkodu);
            
            if (urun == null)
            {
                Console.WriteLine("Ürün bulunamadı. ");

            }
            urunler.Remove(urun);
            urunlerilistele($"{urun.urunadi}'ürünü silindi. ");
        }
        public static void satisyap()
        {
            urunlerilistele("ürün satışı ", true);
            Console.Write("Satış yapmak istediğiniz ürünün kodunu girin. ");
            var urunkodu = Console.ReadLine();
            var urun = urunler.FirstOrDefault(f => f.urunkodu == urunkodu);
            Console.Write("Kaç tane satış yapıldı ? ");
            var satismiktar=Convert.ToInt32(Console.ReadLine());
            if (urun.miktar<satismiktar)
            {
                Console.WriteLine("Yeterli ürün elimizde yok.");
            }
            
            urun.miktar -= satismiktar;
            Random rnd = new Random();
            var eklenecekgun =rnd.Next(-10, 10);
            var satis = new Satis
            {
                miktar = satismiktar,
                tutar = satismiktar * urun.fiyat,
                urunkodu = urunkodu,
                tarih = DateTime.Now.AddDays(eklenecekgun)
            };
            satislar.Add(satis);
            Console.WriteLine("Satış yapıldı. ");
            menuyedon();
        }

        public static void satislistesi()
        {
            Console.Clear();
            if (satislar.Any())
            {
                Console.WriteLine($"{"#".PadRight(5)}" +
                              $"{"Ürün Kodu ".PadRight(10)}" +
                              $"{"Ürün adı ".PadRight(15)}" +
                              $"{"Miktar".PadRight(10)}" +
                              $"{"Tutar".PadRight(10)}");
                var sayac = 1;
                foreach (var satis in satislar)
                {
                    var urun = urunler.FirstOrDefault(f => f.urunkodu == satis.urunkodu);
              Console.WriteLine($"{sayac.ToString().PadRight(5)}" +
                                $"{satis.urunkodu.PadRight(10)}" +
                                $"{urun.urunadi.PadRight(15)}" +
                                $"{satis.miktar.ToString().PadRight(10)}" +
                                $"{satis.tutar.ToString().PadRight(10)}");
                    sayac++;
                }
            }
            else
            {
                Console.WriteLine("Kayıtlı satış bulunamadı. ");
            }
            menuyedon();
        }

        public static void cikis()
        {
            Environment.Exit(0);
        }
        public static void urunlistesiolustur()
        {
            urunler = new List<urun>();

            urunler.Add(new urun
            {
                urunkodu="U1101",
                urunadi = "Pirasa",
                fiyat = 10,
                miktar=115
            });

            urunler.Add(new urun
            {
                urunkodu = "U1102",
                urunadi = "Karnabahar",
                fiyat = 15,
                miktar = 45
            });

            urunler.Add(new urun
            {
                urunkodu="U1103",
                urunadi = "brokoli",
                fiyat = 20,
                miktar=150
            });

            urunler.Add(new urun
            {
                urunkodu = "U1104",
                urunadi = "elma",
                fiyat = 12,
                miktar = 1325
            }) ;
        }
        public static void urunlerilistele(string mesaj = "",bool guncelleicin=false)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(mesaj))
            {
                Console.WriteLine(mesaj);
            }
            Console.WriteLine($"# {"ürün adı".PadRight(20)}{"fiyat"}");
            var sayac = 1;
            if (urunler==null)
            
                urunlistesiolustur();
            
            foreach (var urun in urunler)
            {
                Console.WriteLine($"{sayac} {urun.urunkodu} {urun.urunadi.PadRight(20)} {urun.fiyat.ToString().PadLeft(3)}");
                sayac++;
            }
            if (!guncelleicin)
            {
                menuyedon();
            }
        }
        #endregion
    }
}
