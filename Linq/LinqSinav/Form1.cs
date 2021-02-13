using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace LinqSinav
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnSoru3_Click(object sender, EventArgs e)
        {

            int[] Sayilar = { 1,45,8,6,9,11,7,9 };
            bool oddNumber = false;
            int kalan;
            if (oddNumber) kalan = 1; else kalan = 0;
            var query = from sayi in Sayilar where sayi % 2 == kalan select sayi;          
            int sonuc = query.Max();
            MessageBox.Show(sonuc.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
