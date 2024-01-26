using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
    public partial class Oyun_Ayar : Form
    {
        IFirebaseClient client;

        public Oyun_Ayar()
        {
            InitializeComponent();
            // Firebase yapılandırma ayarları
            var config = new FirebaseConfig
            {
                BasePath = "",
                AuthSecret = ""
            };
            client = new FireSharp.FirebaseClient(config);
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse Kelime_Adet_al_1 = await client.GetAsync("5");
                Kelime_Adet_Al updatedata_1 = Kelime_Adet_al_1.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_2 = await client.GetAsync("10");
                Kelime_Adet_Al updatedata_2 = Kelime_Adet_al_2.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_3 = await client.GetAsync("15");
                Kelime_Adet_Al updatedata_3 = Kelime_Adet_al_3.ResultAs<Kelime_Adet_Al>();

                if (updatedata_1 != null && updatedata_2 != null && updatedata_3 != null)
                {

                    updatedata_1.Adet_Durum = true;
                    FirebaseResponse responseUpdate = await client.UpdateAsync("5", updatedata_1);

                    updatedata_2.Adet_Durum = false;
                    FirebaseResponse responseUpdate2 = await client.UpdateAsync("10", updatedata_2); // Burda güncelleme yapıyoruz gördüğünüz üzere veri çekmek ve yakalamaktan pek farkı yok
                                                                                                    // As you can see, we are updating here, it is not much different from pulling and burning data.

                    updatedata_3.Adet_Durum = false;
                    FirebaseResponse responseUpdate3 = await client.UpdateAsync("15", updatedata_3);


                    button1.Enabled = false; button2.Enabled = true; button3.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Firebase'den veri alınamadı veya sözlük boş.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse Kelime_Adet_al_1 = await client.GetAsync("5");
                Kelime_Adet_Al updatedata_1 = Kelime_Adet_al_1.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_2 = await client.GetAsync("10");
                Kelime_Adet_Al updatedata_2 = Kelime_Adet_al_2.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_3 = await client.GetAsync("15");
                Kelime_Adet_Al updatedata_3 = Kelime_Adet_al_3.ResultAs<Kelime_Adet_Al>();

                if (updatedata_1 != null && updatedata_2 != null && updatedata_3 != null)
                {

                    updatedata_1.Adet_Durum = false;
                    FirebaseResponse responseUpdate = await client.UpdateAsync("5", updatedata_1);

                    updatedata_2.Adet_Durum = true;
                    FirebaseResponse responseUpdate2 = await client.UpdateAsync("10", updatedata_2); // Burda güncelleme yapıyoruz gördüğünüz üzere veri çekmek ve yakalamaktan pek farkı yok
                                                                                                    // As you can see, we are updating here, it is not much different from pulling and burning data.

                    updatedata_3.Adet_Durum = false;
                    FirebaseResponse responseUpdate3 = await client.UpdateAsync("15", updatedata_3);

                    button1.Enabled = true; button2.Enabled = false; button3.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Firebase'den veri alınamadı veya sözlük boş.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse Kelime_Adet_al_1 = await client.GetAsync("5");
                Kelime_Adet_Al updatedata_1 = Kelime_Adet_al_1.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_2 = await client.GetAsync("10");
                Kelime_Adet_Al updatedata_2 = Kelime_Adet_al_2.ResultAs<Kelime_Adet_Al>();

                FirebaseResponse Kelime_Adet_al_3 = await client.GetAsync("15");
                Kelime_Adet_Al updatedata_3 = Kelime_Adet_al_3.ResultAs<Kelime_Adet_Al>();

                if (updatedata_1 != null && updatedata_2 != null && updatedata_3 != null)
                {

                    updatedata_1.Adet_Durum = false;
                    FirebaseResponse responseUpdate = await client.UpdateAsync("5", updatedata_1);

                    updatedata_2.Adet_Durum = false;
                    FirebaseResponse responseUpdate2 = await client.UpdateAsync("10", updatedata_2); // Burda güncelleme yapıyoruz gördüğünüz üzere veri çekmek ve yakalamaktan pek farkı yok
                                                                                                    // As you can see, we are updating here, it is not much different from pulling and burning data.
                    updatedata_3.Adet_Durum = true;
                    FirebaseResponse responseUpdate3 = await client.UpdateAsync("15", updatedata_3);

                    button1.Enabled = true; button2.Enabled = true; button3.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Firebase'den veri alınamadı veya sözlük boş.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {         
            try
            {         
                    this.Hide();              
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Oyun_Ayar_Load(object sender, EventArgs e)
        {

        }
    }
}
