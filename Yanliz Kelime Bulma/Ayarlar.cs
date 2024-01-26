using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections;
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
    public partial class Ayarlar : Form
    {
        int i;

        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = ""
        };
        IFirebaseClient client;

        public Ayarlar()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);
            
        }
        public async void KacTeneVeriVar()
        {
            var kacten_veri_var = await client.GetAsync("");

            // Burda liste olarak çekiyoruz çünkü birden fazla veri döndüğü için Dictionary sınıfı bizim işimize burda yaramaz 
            // void içindeki amaçımız databaseye kaç tane veri eklendiğini bulup yeni eklenen veriye eklenen sayıdan 1 fazlasını id olara atıyoruz ---
            // -- böylece ileri veri çekerken bu id lere göre çekicez 

            // Since the Dictionary class is not useful for returning more than one data, we draw it here as a list
            //void Our purpose in the content is to find out how much data has been added to the database. We assigned the +1 version of the amount as ID to the new data ---
            // -- so when we pull forward, we will pull according to this identity


            List<Oyun_Kelimelik> kelimelerListesi = kacten_veri_var.ResultAs<List<Oyun_Kelimelik>>();

            if (kelimelerListesi == null)
            {
                i = 0;
                id_textBox3.Text = "" + i;
            }
            else
            {
                i = kelimelerListesi.Count;
                id_textBox3.Text = "" + i;
            }
        }

        public void Ayarlar_Load(object sender, EventArgs e)
        {
            KacTeneVeriVar();
        }

        private async void Gonder_button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                Oyun_Kelimelik oyun_Kelimelik = new Oyun_Kelimelik()
                {
                    Kelime = Kelime_textBox1.Text,
                    Anlami = Anlami_textBox2.Text
                };



                if (!string.IsNullOrEmpty(Kelime_textBox1.Text) && !string.IsNullOrEmpty(Anlami_textBox2.Text))
                {
                    i = Convert.ToInt32(id_textBox3.Text);
                    // Tablo adı ve kelimeler için belirleyeceğiniz id veya key (primary key) atıyoruz  
                    string firebasePath =  i.ToString(); // This assumes using the username as the key

                    FirebaseResponse getResponse = await client.GetAsync(firebasePath);
                    if (getResponse != null || getResponse.Body != "null")
                    {
                        // user classı ve firebasepath değişkenlerini gönderiyoruz 
                        // We send the user class and firebasepath variables
                        SetResponse setResponse = await client.SetAsync(firebasePath, oyun_Kelimelik);
                        MessageBox.Show("Kelime Göderildi. Desteğniz için TEŞEKKÜRLER. ");
                        i = 0;
                        KacTeneVeriVar();
                    }
                    else
                    {
                        MessageBox.Show("İşelm Gerçekleşmedi!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Tüm Alanları Doldurunuz");
                }                              
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e, string a)
        {
            Ana_From ana_From = new Ana_From(a); 
            ana_From.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Ana_From ana_From = new Ana_From(null);
            ana_From.Show();
            this.Hide();
        }
    }
}
