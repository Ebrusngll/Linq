using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqSınavvv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSoru3_Click(object sender, EventArgs e)
        {
            int[] Sayilar = { 1, 45, 8, 6, 9, 11, 7, 9 };
            bool oddNumber = false;
            int kalan;
            if (oddNumber) kalan = 1; else kalan = 0;
            var query = from sayi in Sayilar where sayi % 2 == kalan select sayi;
            int sonuc = query.Max();
            MessageBox.Show(sonuc.ToString());
        }

        private void btnSoru4_Click(object sender, EventArgs e)
        {

            MessageBox.Show("first bize ilk veriyi getirrken single ise isteğimize uygun olan varsa onlardan bir tanesini getirir.");

        }

        private void btnSoru5_Click(object sender, EventArgs e)
        {

            string[] players = { "Tsubasa Ozora", "Misaki Taro", "Kojiro Hyuga" };
            string searchedPlayer = "Ozora";
            var oyuncular = from oyuncu in players
                            where oyuncu.EndsWith(searchedPlayer)
                            select oyuncu;
            if (oyuncular.Count() > 0)
            {
                MessageBox.Show($"{searchedPlayer} mevcut");
            }
            else
            {
                MessageBox.Show($"{searchedPlayer} mevcut değil.");

            }

        }

        private void btnSoru6_Click(object sender, EventArgs e)
        {
            string[] languages = { "java", "python", "Cobol", "C++", "c#", "php" };
            List<string> cIleBaslayanlar = new List<string>();
            var listem = from q in languages
                         where q.StartsWith("C")
                         select q;
            cIleBaslayanlar = listem.ToList();
            foreach (var item in cIleBaslayanlar)
            {
                MessageBox.Show(item);
            }
        }

        List<Product> GetAllProducts()
        {
            using (var ctx = new NorthwindEntities())
            {
                var list = from p in ctx.Products select p;
                return list.ToList();


            }
        }

        List<Category> GetAllCategory()
        {
            using (var ctx = new NorthwindEntities())
            {
                var list = from c in ctx.Categories select c;
                return list.ToList();
            }
        }
        private void btnSoru7_Click(object sender, EventArgs e)
        {
                       
            List<Product> ProductListe = GetAllProducts();
            List<Category> CategoryListe = GetAllCategory();

            var ProductListem = from p in ProductListe where p.UnitPrice > 0 select p;


            dgrCozum.DataSource = ProductListem.ToList();

            var ProductListesi = from p in ProductListe where p.UnitPrice <= 0 select p;
                    
        }

        private void btnSoru8_Click(object sender, EventArgs e)
        {           
            List<Product> beverages = new List<Product>();
            using (var ctx = new NorthwindEntities())
            {
                var listem = ctx.Categories.Join(ctx.Products,
                            c => c.CategoryID,
                            t => t.CategoryID,
                            (c, t) => new
                            {
                                proID = t.ProductID,
                                proName = t.ProductName,
                                proCat = c.CategoryName
                            }).Where(z1 => z1.proCat.Equals("Beverages"));
                dgrCozum.DataSource = listem.ToList();
            }
        }

        private void btnSoru9_Click(object sender, EventArgs e)
        {
            List<Product> ProductListe = GetAllProducts();
            List<Category> CategoryListe = GetAllCategory();
            using (var ctx = new NorthwindEntities())
            {
                var EnYuksekFiyatliUrun = CategoryListe.Join(ProductListe,
                    c => c.CategoryID,
                    p => p.CategoryID,
                    (c, p) => new
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        Category = c.CategoryName,
                        CategoryID = c.CategoryID,
                        UnitPrice = p.UnitPrice,
                    }).OrderByDescending(z => z.UnitPrice).First();
                MessageBox.Show(EnYuksekFiyatliUrun.ToString());
            }
        }

        private void btnSoru10_Click(object sender, EventArgs e)
        {
            List<Product> ProductListe = GetAllProducts();
            List<Category> CategoryListe = GetAllCategory();

            using (var ctx = new NorthwindEntities())
            {
                var UcuzUrunum = CategoryListe.Join(ProductListe,
                   c => c.CategoryID,
                   p => p.CategoryID,
                   (c, p) => new
                   {
                       ProductID = p.ProductID,
                       ProductName = p.ProductName,
                       Category = c.CategoryName,
                       CategoryID = c.CategoryID,
                       UnitPrice = p.UnitPrice,
                   }).Where(ortak => ortak.CategoryID.Equals("Dairy Products")).OrderBy(ortak => ortak.UnitPrice).Take(3);
                foreach (var item in UcuzUrunum)
                {
                    MessageBox.Show(UcuzUrunum.ToString());
                }
                
            }
        }

        private void btnSoru11_Click(object sender, EventArgs e)
        {
            using (var ctx = new NorthwindEntities())
            {
                var AraSıralamaYap = ctx.Products.Where(p => p.UnitPrice > 20 && p.UnitPrice < 50).ToList();
                dgrCozum.DataSource = AraSıralamaYap;
            }
        }

        private void btnSoru12_Click(object sender, EventArgs e)
        {
            using ( var ctx = new NorthwindEntities())
            
            {
                var liste = from o in ctx.Orders
                            join c in ctx.Customers on o.CustomerID equals c.CustomerID
                            join s in ctx.Shippers on o.ShipVia equals s.ShipperID
                            join emp in ctx.Employees on o.EmployeeID equals emp.EmployeeID
                            select new
                            {
                                FistName = emp.FirstName,
                                LastName = emp.LastName,
                                ShipCompanyName = s.CompanyName,
                                OrderDate = o.OrderDate,
                                CusCompanyName = c.CompanyName
                            };
                dgrCozum.DataSource = liste.ToList();
                                                      
            }
            
        }
        private void btnSoru13_Click(object sender, EventArgs e)
        {

            //Kategorilerine göre stoktaki ürünleri adeti kaçtır?

            using (var ctx = new NorthwindEntities())
            {

                var KategoriVeStokGetir = ctx.Products.Join(ctx.Categories,
                                          Urun => Urun.CategoryID,
                                          Kategori => Kategori.CategoryID,
                                          (Urun, Kategori) => new
                                          {
                                              kategori = Kategori.CategoryID,
                                              UrunAdet = Urun.UnitsInStock
                                          }

                    );

                var Grupla = ctx.Categories.GroupJoin(ctx.Products,
                             Kategori => Kategori.CategoryID,
                             Urun => Urun.CategoryID,
                             (Kategori,Urun) => new
                             {
                                 kategori = Kategori.CategoryID,
                                 Urun
                             }
                    );


                foreach (var grup in Grupla)
                {
                    lbSonuc.Items.Add("Kategori ID " + grup.kategori);
                    //MessageBox.Show("Grup Adı : " + grup.kategori);
                    foreach (var product in grup.Urun)
                    {
                        lbSonuc.Items.Add("Urun Stok -->" + product.UnitsInStock);

                        //MessageBox.Show(item.UnitsInStock.ToString());
                    }
                }   //grup join içinde iki tabloyu birleştirip iç içe formatta getirdik.
                               
            }
        }


        private void btnSoru14_Click(object sender, EventArgs e)
        {
            //Çalışanların ad, soyad, doğum tarihi ve yaş bilgilerini listeleyiniz. Yaşa göre büyükten küçüğe sıralayınız.

            using (var ctx = new NorthwindEntities())
            {
                var CalisanBilgilerim = (from emp in ctx.Employees
                                         orderby emp.BirthDate
                                         select new
                                         {
                                             Calisanim = emp.FirstName + " " + emp.LastName,
                                             DogumTarihi = emp.BirthDate,
                                             CalisanYasi = DateTime.Now.Year - emp.BirthDate.Value.Year
                                         }).ToList();
                dgrCozum.DataSource = CalisanBilgilerim;

            }

        }

        private void btnSoru15_Click(object sender, EventArgs e)
        {
            //Kargo şirketlerinden adi SpeedyExpress olan şirketin tüm bilgilerini listeleyiniz.

            using ( var ctx = new NorthwindEntities())
            {
                var KargoSirketi = ctx.Shippers.Where(x => x.CompanyName == "Speedy Express").Select(x => x).ToList<Shipper>();
                      
               dgrCozum.DataSource = KargoSirketi;        
            }           
        }

        private void btnSoru16_Click(object sender, EventArgs e)
        {
            //Stokta bulunan toplam ürün sayısını getiriniz.

            using (var ctx = new NorthwindEntities())
            {
                var StokUrunToplam = ctx.Products.Where(x => x.UnitsInStock > 0).Sum(x => x.UnitsInStock);              
                MessageBox.Show(StokUrunToplam.ToString());

            }
        }

        private void btnSoru17_Click(object sender, EventArgs e)
        {
            //Tüm cirom ne kadar?
            using (var ctx = new NorthwindEntities())
            {
                var Toplams = (from od in ctx.Order_Details
                               group od by 1 into x
                               select new
                               {
                                   Ciro = x.Sum(y => (double)(y.UnitPrice) * (y.Quantity) * (1 - y.Discount))
                               }).ToList();

                dgrCozum.DataSource = Toplams;
            }
        }

        private void btnSoru18_Click(object sender, EventArgs e)
        {
            //1997'de tüm cirom ne kadar?
            using (var ctx = new NorthwindEntities())
            {

                var Toplams = (from od in ctx.Order_Details
                               where od.Order.OrderDate.Value.Year == 1997
                               group od by 1 into x
                              
                               select new
                               {
                                   Ciro = x.Sum(y => (double)(y.UnitPrice) * (y.Quantity) * (1 - y.Discount))
                               }).ToList();

                dgrCozum.DataSource = Toplams;
            }

        }

        private void btnSoru19_Click(object sender, EventArgs e)
        {
            //Bugün doğum günü olan çalışanlarım kimler?
            using (var ctx = new NorthwindEntities())
            {
                var DogumGunu = ctx.Employees.Where(emp => emp.BirthDate.Value.Day == DateTime.Now.Day).ToList<Employee>();
                dgrCozum.DataSource = DogumGunu;
            }
        }

        private void btnSoru20_Click(object sender, EventArgs e)
        {
            //Hangi çalışanım hangi çalışanıma bağlı ?
            // employeenin reports to sunda hangi employeenin ıd si varsa ona bağlı.

            using (var ctx = new NorthwindEntities())
            {
                var BagliCalisan = (from emp in ctx.Employees
                              select new
                              {                                  
                                  Employee = emp.FirstName,
                                  EmployeeID = emp.EmployeeID,
                                  ReportsTo = emp.ReportsTo
                              });
                dgrCozum.DataSource = BagliCalisan.ToList();
            }


        }

        private void btnSoru21_Click(object sender, EventArgs e)
        {
            //Çalışanlarım toplam ne kadarlık satış yapmışlar?

            using ( var ctx = new NorthwindEntities())
            {
                var CalisanlarinYaptigiToplamSatis = from od in ctx.Order_Details
                                                     join o in ctx.Orders on od.OrderID equals o.OrderID
                                                     group od by od.OrderID into x
                                                     select new { ToplamSatis = x.Sum(y => y.UnitPrice) };
                dgrCozum.DataSource = CalisanlarinYaptigiToplamSatis.ToList();

                #region
                //var liste = from o in ctx.Orders
                //            join c in ctx.Customers on o.CustomerID equals c.CustomerID
                //            join s in ctx.Shippers on o.ShipVia equals s.ShipperID
                //            join emp in ctx.Employees on o.EmployeeID equals emp.EmployeeID
                //            select new
                //            {
                //                FistName = emp.FirstName,
                //                LastName = emp.LastName,
                //                ShipCompanyName = s.CompanyName,
                //                OrderDate = o.OrderDate,
                //                CusCompanyName = c.CompanyName
                //            };


                //var CalisanlarinYaptigiToplamSatis = ctx.Order_Details.Join(ctx.Orders,
                //                                     Odetail => Odetail.OrderID,
                //                                     od => od.OrderID,
                //                                     (Odetail, od) => new
                //                                     {
                //                                         CalisanAdi = od.Employee.FirstName,
                //                                         CalisanSoyadi = od.Employee.LastName,
                //                                         UrunFiyati = Odetail.UnitPrice,
                //                                     }
                //    )

                //var grupla = ctx.Orders.GroupJoin(ctx.Order_Details,
                //             od => od.OrderID,
                //             Odetail => Odetail.OrderID,
                //             (od,Odetail) => new
                //             {
                //                 CalisanAD = od.Employee.FirstName,
                //                 CalisanSoyad = od.Employee.LastName,
                //                 Odetail                                
                //             }
                //    ).Sum()
                #endregion

            }
        }

        private void btnSoru22_Click(object sender, EventArgs e)
        {
            //Hangi ülkelere ihracat yapıyorum?
            using (var ctx = new NorthwindEntities())
            {
                var İhracat = (from o in ctx.Orders
                             group new { o } by new { o.ShipCountry } into g
                             select new
                             {
                                 g.Key.ShipCountry
                             }).ToList();
                dgrCozum.DataSource = İhracat;
            }
        }

        private void btnSoru23_Click(object sender, EventArgs e)
        {
            //Ürünlere göre satışım nasıl? satılan urun sayısı

            using (var ctx = new NorthwindEntities())
            {
                var UrunlereGoreSatis = from p in ctx.Products
                             join od in ctx.Order_Details
                             on p.ProductID equals od.ProductID
                             group new { p } by new { p.ProductName, od.ProductID } into g
                             select new
                             {
                                 g.Key.ProductName,
                                 ToplamSatış = g.Count(x => x.p.ProductID > 0)
                             };
                dgrCozum.DataSource = UrunlereGoreSatis.ToList();
            }


        }

        private void btnSoru24_Click(object sender, EventArgs e)
        {
            //Ürün kategorilerine göre satışlarım nasıl? (para bazında)

            using (var ctx = new NorthwindEntities())
            {
                var KategoriyeGoreSatis = (from od in ctx.Order_Details
                             join p in ctx.Products
                             on od.ProductID equals p.ProductID
                             join c in ctx.Categories
                             on p.CategoryID equals c.CategoryID
                             group new { c, p } by new { c.CategoryName } into g
                             select new
                             {
                                 g.Key.CategoryName,
                                 TotalPrice = g.Sum(p => p.p.UnitPrice)
                             }).ToList();
                dgrCozum.DataSource = KategoriyeGoreSatis;

            }
        }

        private void btnSoru25_Click(object sender, EventArgs e)
        {
            //Ürün kategorilerine göre satışlarım nasıl? (sayı bazında)

            using (var ctx = new NorthwindEntities())
            {
                var SayiBazindaSatis = (from od in ctx.Order_Details
                             join p in ctx.Products
                             on od.ProductID equals p.ProductID
                             join c in ctx.Categories
                             on p.CategoryID equals c.CategoryID
                             group new { c, od ,p } by new { c.CategoryName } into x
                             select new
                             {
                                 x.Key.CategoryName,
                                 TotalCount = x.Count()
                             }).ToList();

                dgrCozum.DataSource = SayiBazindaSatis;
            }


        }

        private void btnSoru26_Click(object sender, EventArgs e)
        {
            //Çalışanlar ürün bazında ne kadarlık satış yapmışlar?

            using(var ctx = new NorthwindEntities())
            {
                var UrunBazindaYapilanSatis = from od in ctx.Order_Details
                                              join p in ctx.Products on od.ProductID equals p.ProductID
                                              join o in ctx.Orders on od.OrderID equals o.OrderID
                                              join emp in ctx.Employees on o.EmployeeID equals emp.EmployeeID
                                              group p by p.ProductID into x
                                              select new { CalisanID = x.Key, UrunToplam = x.Sum(a => a.UnitPrice )};
                dgrCozum.DataSource = UrunBazindaYapilanSatis.ToList();
            }
        }

        private void btnSoru27_Click(object sender, EventArgs e)
        {
            //Çalışanlarım para olarak en fazla hangi ürünü satmışlar? Kişi bazında bir rapor istiyorum.                   // ÇALIŞMIYOR TEKRAR BAK

            using (var ctx = new NorthwindEntities())
            {
                var CalisanlarEnFazlaUrunSatisi = (from od in ctx.Order_Details
                                                   join o in ctx.Orders
                                                   on od.OrderID equals o.OrderID
                                                   join emp in ctx.Employees
                                                   on o.EmployeeID equals emp.EmployeeID
                                                   join p in ctx.Products
                                                   on od.ProductID equals p.ProductID
                                                   select new
                                                   {
                                                       CalisanAdi = emp.FirstName + " " + emp.LastName,
                                                       Urun = p.ProductName,
                                                       UrunFiyat = p.UnitPrice
                                                   }).GroupBy(k => k.CalisanAdi, k => k.UrunFiyat).Max(a => a.Key).ToList();

                dgrCozum.DataSource = CalisanlarEnFazlaUrunSatisi;


            }
        }

        private void btnSoru28_Click(object sender, EventArgs e)
        {
            //Hangi kargo şirketine toplam ne kadar ödeme yapmışım?

            using (var ctx = new NorthwindEntities())
            {
                var KargoOdemeTutar = from siparis in ctx.Orders
                                      join shprs in ctx.Shippers
                                      on siparis.ShipVia equals shprs.ShipperID
                                      group new
                                      {
                                          siparis,    // burda ekleme yaparken select new in içinde kullanıcağımı şeyleri eklioruz.
                                          shprs 
                                      } by new
                                      {                                         
                                          KargoId = shprs.ShipperID    // burası normal sql sorgusunda select in yanına yazan yer ,grupladığım yer yani
                                      } into SirketID
                                      select new
                                      {                                          
                                          sirketID = SirketID.Key.KargoId,
                                          toplam = SirketID.Sum(x => (x.siparis.Freight))
                                      };
                dgrCozum.DataSource = KargoOdemeTutar.ToList();

            }

            

        }

        private void btnSoru29_Click(object sender, EventArgs e)
        {
            //Tost yapmayı seven çalışanım hangisi?

            using (var ctx = new NorthwindEntities())
            {
                var TostSevenArkadas = from emp in ctx.Employees
                                       where emp.Notes.Contains("Toast")
                                       select new
                                       {
                                           Calisan = emp.FirstName + " " + emp.LastName,
                                           notlar = emp.Notes
                                       };

                dgrCozum.DataSource = TostSevenArkadas.ToList();
            }

        }

        private void btnSoru30_Click(object sender, EventArgs e)
        {
            //Hangi tedarikçiden aldığım ürünlerden ne kadar satmışım.

            using (var ctx = new NorthwindEntities())
            {
                var NeKadarSattim = (from od in ctx.Order_Details
                                    join o in ctx.Orders
                                    on od.OrderID equals o.OrderID
                                    join p in ctx.Products
                                    on od.ProductID equals p.ProductID
                                    join s in ctx.Suppliers
                                    on o.ShipVia equals s.SupplierID
                                    group new { od, o, p, s } by new { s.CompanyName, p.ProductName } into x
                                    select new
                                    {
                                        x.Key.CompanyName,
                                        UrunAdi = x.Key.ProductName,
                                        Toplam = x.Count()
                                    }).ToList();
                dgrCozum.DataSource = NeKadarSattim;
                                                         
            }

        }

        private void btnSoru31_Click(object sender, EventArgs e)
        {
            //En değerli müşterim hangisi? (en fazla satış yaptığım müşteri)
            // reorder level  * units on order ı en yüksek olanı bul 

            using ( var ctx = new NorthwindEntities())
            {
                var EnDegerliMusteri = (from od in ctx.Order_Details
                                        join c in ctx.Customers
                                        on od.Order.CustomerID equals c.CustomerID
                                        join p in ctx.Products
                                        on od.ProductID equals p.ProductID
                                        select new
                                        {
                                            Musteri = c.CompanyName,
                                            EnCokSatis = p.ReorderLevel * p.UnitsOnOrder

                                        }).OrderByDescending(p => p.EnCokSatis).Take(1).ToList();

                dgrCozum.DataSource = EnDegerliMusteri;
            }                      
        }

        private void btnSoru32_Click(object sender, EventArgs e)
        {
            //Hangi müşteriler para bazında en fazla hangi ürünü almışlar?                 

            using ( var ctx = new NorthwindEntities())
            {
                var ParaBazindaEnFazlaUrun = (from od in ctx.Order_Details
                                              join o in ctx.Orders
                                              on od.OrderID equals o.OrderID
                                              join c in ctx.Customers
                                              on o.CustomerID equals c.CustomerID
                                              join p in ctx.Products
                                              on od.ProductID equals p.ProductID
                                              group new {od,o,c,p} by new {p.ProductName ,c.CompanyName} into x
                                              select new
                                              {
                                                 x.Key.CompanyName,
                                                 x.Key.ProductName,
                                                 ToplamTutar = x.Sum( p =>p.p.UnitPrice)
                                              }).OrderByDescending(a => a.ToplamTutar).ToList();
                dgrCozum.DataSource = ParaBazindaEnFazlaUrun;
            }

        }

        private void btnSoru33_Click(object sender, EventArgs e)
        {
            //Hangi ülkelere ne kadarlık satış yapmışım?

            using ( var ctx = new NorthwindEntities())
            {
                var NeKadarSatis = (from od in ctx.Order_Details
                                    join o in ctx.Orders
                                    on od.OrderID equals o.OrderID
                                    group new { o, od } by new { o.ShipCountry } into x
                                    select new
                                    {
                                        x.Key.ShipCountry,
                                        SatisFiyat = x.Sum(od => od.od.UnitPrice * od.od.Quantity)
                                    }).ToList();
                dgrCozum.DataSource = NeKadarSatis;
            }
        }

        private void btnSoru34_Click(object sender, EventArgs e)
        {
            //Zamanında teslim edemediğim siparişlerim idleri nelerdir ve kaç gün geç göndermişim?
            using(var ctx = new NorthwindEntities())
            {
                var ZamanindaTeslimEdilmemisSip = (from od in ctx.Order_Details
                                                   join o in ctx.Orders
                                                   on od.OrderID equals o.OrderID
                                                   join p in ctx.Products
                                                   on od.ProductID equals p.ProductID
                                                   where (o.ShippedDate > o.RequiredDate) //geç teslim olanlar 
                                                   select new
                                                   {
                                                       ProductID = p.ProductID,
                                                       Day = (o.ShippedDate.Value.Year - o.RequiredDate.Value.Year) * 365 + (o.ShippedDate.Value.Month - o.RequiredDate.Value.Month) * 30 + (o.ShippedDate.Value.Day - o.RequiredDate.Value.Day)
                                                   }).ToList();
                dgrCozum.DataSource = ZamanindaTeslimEdilmemisSip;
            }


        }

        private void btnSoru35_Click(object sender, EventArgs e)
        {
            //Ortalama satış miktarının üzerine çıkan satışlarım hangisi?                    virgüllerimde sıkıntı var 
            using (var ctx = new NorthwindEntities())
            {

                var ort = (from od in ctx.Order_Details
                              orderby od.OrderID
                              select new
                              {
                                  average = od.Quantity * od.UnitPrice
                              }).Average(x => x.average);

                var ort2 = (from od in ctx.Order_Details
                               select new
                               {
                                   avg = od.UnitPrice * od.Quantity
                               }).Where(x => x.avg > ort).ToList();

                dgrCozum.DataSource = ort2;
            }
        }

        private void btnSoru36_Click(object sender, EventArgs e)
        {
            //Satışlarımı kaç günde teslim etmişim?

            using (var ctx = new NorthwindEntities())
            {
                var KacGundeTeslimEdildi = (from o in ctx.Orders
                                            where (o.ShippedDate.Value.Day - o.OrderDate.Value.Day > 0)   // null dönmemesi için böyle yaptım yoksa null dönenler oldu ve hata verdi.
                              select new
                              {
                                  o.CustomerID,
                                  o.EmployeeID,
                                  o.OrderID,
                                  o.ShippedDate,
                                  o.OrderDate,
                                  ShippedDays = o.ShippedDate.Value.Day - o.OrderDate.Value.Day
                              }).ToList(); 
                dgrCozum.DataSource = KacGundeTeslimEdildi;
            }
        }

        private void btnSoru37_Click(object sender, EventArgs e)
        {
            //Sipariş verilip de stoğumun yetersiz olduğu ürünler hangisidir? Bu ürünlerden kaç tane eksiğim vardır?

            using ( var ctx = new NorthwindEntities())
            {
                var YetersizUrun = (from od in ctx.Order_Details
                                    join p in ctx.Products
                                    on od.ProductID equals p.ProductID
                                    where (od.Quantity > p.UnitsInStock)
                                    select new
                                    {
                                        Urun = p.ProductName,
                                        UrunID = p.ProductID,
                                        Siparis = od.OrderID,
                                        EksimSayim = od.Quantity - p.UnitsInStock

                                    }).ToList();
                dgrCozum.DataSource = YetersizUrun;

            }
        }
    }
   
           

}
