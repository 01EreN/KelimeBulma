using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OyunDeneme1
{
    public partial class Ana_From : Form
    {
        private string kullaniciAdi;
        string Kullanici_Adi_Al;
        public Ana_From(string kullaniciadi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciadi;
            if (Kullanici_Adi_label_1.Text ==  "" || Kullanici_Adi_label_1.Text == kullaniciAdi)
            {
                Kullanici_Adi_label_1.Text = "" + kullaniciAdi;
            }       
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Ayarlar ayarlar = new Ayarlar();    
            ayarlar.Show(); 
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici_Adi_Al = Kullanici_Adi_label_1.Text;
            Oda_1 oda_1 = new Oda_1(Kullanici_Adi_Al);  
            oda_1.Show();
            this.Hide();    
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Giris_From2 giris = new Giris_From2();  
            giris.Show();
            this.Hide();
        }

        private void Ana_From_Load(object sender, EventArgs e)
        {
            
        }
    }
}
