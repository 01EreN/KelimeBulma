using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OyunDeneme1
{
    public partial class Oda_1 : Form
    {
        int i = 0;
        string GlobalKelime;
        string Kullanici_adi;
        int Masa1_Puan, Masa2_Puan, Masa3_Puan, Masa4_Puan;
        int puan_hangi_masanın;
        private int geriSayimSure = 30; // Geri sayım süresi (saniye cinsinden)
        public Timer geriSayimTimer;
        public Timer toplampuangetirTimer;
        public Timer oyunbaslangickontrolTimer;
        public Timer oda1masakullaniciadialTimer;
        Dictionary<string, Kelime_Adet_Al> PullingDictionary;
        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = ""
        };
        IFirebaseClient client;
        IFirebaseClient firebaseClient;
        IFirebaseClient firebaseClient_masa_kontrol;

        private void Oda_1_Load(object sender, EventArgs e)
        {
        }

        public Oda_1(string kullanici_adi)
        {
            InitializeComponent();
            this.Kullanici_adi = kullanici_adi;

            var config2 = new FirebaseConfig
            {
                BasePath = "",
                AuthSecret = ""
            };
            firebaseClient = new FireSharp.FirebaseClient(config2);

            var config_masa_kontrol = new FirebaseConfig
            {
                BasePath = "",
                AuthSecret = ""
            };
            firebaseClient_masa_kontrol = new FireSharp.FirebaseClient(config_masa_kontrol);

            oyundurumduzenle();
            Oda1MasaAdiDuzenle();

            geriSayimTimer = new Timer();
            geriSayimTimer.Interval = 1100; // Timer'ın her bir tick'i 1 saniye (1250 milisaniye) //Each click of the Timer is 1 second (1250 milliseconds)
            geriSayimTimer.Tick += YeniKelimeGetir;

            toplampuangetirTimer = new Timer();
            toplampuangetirTimer.Interval = 500;
            toplampuangetirTimer.Tick += ToplamVeriGetirme;

            oyunbaslangickontrolTimer = new Timer();
            oyunbaslangickontrolTimer.Interval = 100;
            oyunbaslangickontrolTimer.Start();
            oyunbaslangickontrolTimer.Tick += OyunBaslangicKontrol;

            oda1masakullaniciadialTimer = new Timer();
            oda1masakullaniciadialTimer.Interval = 1000;
            oda1masakullaniciadialTimer.Tick += Oda1MasaKullaniciAdiGetir;
            //oda1masakullaniciadialTimer.Start();
        }

        public async void Oda1MasaKullaniciAdiGetir(object sender, EventArgs e)
        {
            FirebaseResponse masa_1_kullanici_adi_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
            Oda_1_Kullanici_Adi_kontrol getdata_masa_1_kullanici_adi = masa_1_kullanici_adi_kontrol.ResultAs<Oda_1_Kullanici_Adi_kontrol>();
            Masa1_button1.Text = "" + getdata_masa_1_kullanici_adi.Masa_Kullanici;
            FirebaseResponse response_masa_1_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1_kullanici_adi);

            FirebaseResponse masa_2_kullanici_adi_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
            Oda_1_Kullanici_Adi_kontrol getdata_masa_2_kullanici_adi = masa_2_kullanici_adi_kontrol.ResultAs<Oda_1_Kullanici_Adi_kontrol>();
            Masa2_button2.Text = "" + getdata_masa_2_kullanici_adi.Masa_Kullanici;
            FirebaseResponse response_masa_2_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2_kullanici_adi);

            FirebaseResponse masa_3_kullanici_adi_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
            Oda_1_Kullanici_Adi_kontrol getdata_masa_3_kullanici_adi = masa_3_kullanici_adi_kontrol.ResultAs<Oda_1_Kullanici_Adi_kontrol>();
            Masa_3button3.Text = "" + getdata_masa_3_kullanici_adi.Masa_Kullanici;
            FirebaseResponse response_masa_3_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3_kullanici_adi);

            FirebaseResponse masa_4_kullanici_adi_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
            Oda_1_Kullanici_Adi_kontrol getdata_masa_4_kullanici_adi = masa_4_kullanici_adi_kontrol.ResultAs<Oda_1_Kullanici_Adi_kontrol>();
            Masa4_button4.Text = "" + getdata_masa_4_kullanici_adi.Masa_Kullanici;
            FirebaseResponse response_masa_4_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4_kullanici_adi);

            //oda1masakullaniciadialTimer.Stop();
        }

        public async void Oda1MasaAdiDuzenle()
        {
            FirebaseResponse masa_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
            Oda_1_Masa_Kontrol getdata_masa_1 = masa_1_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
            getdata_masa_1.Masa_Kullanici = Masa1_button1.Text;
            FirebaseResponse response_masa_1_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1);

            FirebaseResponse masa_2_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
            Oda_1_Masa_Kontrol getdata_masa_2 = masa_2_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
            getdata_masa_2.Masa_Kullanici = Masa2_button2.Text;
            FirebaseResponse response_masa_2_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2);

            FirebaseResponse masa_3_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
            Oda_1_Masa_Kontrol getdata_masa_3 = masa_3_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
            getdata_masa_3.Masa_Kullanici = Masa_3button3.Text;
            FirebaseResponse response_masa_3_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3);

            FirebaseResponse masa_4_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
            Oda_1_Masa_Kontrol getdata_masa_4 = masa_4_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
            getdata_masa_4.Masa_Kullanici = Masa4_button4.Text;
            FirebaseResponse response_masa_4_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4);

        }

        public async void oyundurumduzenle()
        {
            FirebaseResponse Oda_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Oda_1");
            Oda_1_Oyun_Baslangic_Kontrol getdata_oda_1_durum = Oda_1_kontrol.ResultAs<Oda_1_Oyun_Baslangic_Kontrol>();
            getdata_oda_1_durum.Oyun_Aktif_durum = false;
            FirebaseResponse response_oda_1_Update = await firebaseClient_masa_kontrol.UpdateAsync("Oda_1", getdata_oda_1_durum);
        }

        public async void OyunBaslangicKontrol( object sender, EventArgs e)
        {
            FirebaseResponse Oda_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Oda_1");
            Oda_1_Oyun_Baslangic_Kontrol getdata_oda_1_durum = Oda_1_kontrol.ResultAs<Oda_1_Oyun_Baslangic_Kontrol>();
            if (getdata_oda_1_durum.Oyun_Aktif_durum == true)
            {
                oyundurumduzenle();
                RastgeleVeriAlama();
                oyunbaslangickontrolTimer.Stop();
            }
        }

        public async void ToplamVeriGetirme(object sender , EventArgs e)
        {
            FirebaseResponse masa_1_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
            Masa_Toplam_Puan getdata_masa_1_puan = masa_1_paun.ResultAs<Masa_Toplam_Puan>();
            Masa1_Puan_label8.Text = "" + getdata_masa_1_puan.Masa_Puani.ToString();

            FirebaseResponse masa_2_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
            Masa_Toplam_Puan getdata_masa_2_puan = masa_2_paun.ResultAs<Masa_Toplam_Puan>();
            Masa2_Puan_label9.Text = "" + getdata_masa_2_puan.Masa_Puani.ToString();

            FirebaseResponse masa_3_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
            Masa_Toplam_Puan getdata_masa_3_puan = masa_3_paun.ResultAs<Masa_Toplam_Puan>();
            Masa3_Puan_label10.Text = "" + getdata_masa_3_puan.Masa_Puani.ToString();

            FirebaseResponse masa_4_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
            Masa_Toplam_Puan getdata_masa_4_puan = masa_4_paun.ResultAs<Masa_Toplam_Puan>();
            Masa4_Puan_label11.Text = "" + getdata_masa_4_puan.Masa_Puani.ToString();
            toplampuangetirTimer.Stop();
        }

        public async void RastgeleVeriAlama()
        {        
            Random rnd = new Random();   // Bu bölümde kelime depomuzdaki kelime sayıları aralığına göre rastgele bir sayı üretiyoruz ve aynı id'ye sahip verileri çekiyoruz.
            int id;                     // In this part, we generate a random number according to the range of word numbers in our word storage and pull data with the same id.

            id = 0;
            id = rnd.Next(0,21);

            try
            {
                client = new FireSharp.FirebaseClient(config);

                if (client == null)
                {

                    MessageBox.Show("Bağlantı hatasi.");
                }
                else
                {
                    FirebaseResponse response = await client.GetAsync(id.ToString());
                    if (response != null)
                    {
                        //List<Kelimelik_Rastgele_Alama> Kelime_Al = response.ResultAs<List<Kelimelik_Rastgele_Alama>>();
                        //PullingDictionary = response.ResultAs<Dictionary<string, Kelimelik_Rastgele_Alama>>();
                        Kelimelik_Rastgele_Alama result = response.ResultAs<Kelimelik_Rastgele_Alama>();

                        if (result == null)
                        {
                            MessageBox.Show("Veri Bulunamadı");

                        }
                        else
                        {
                            string kelime;
                            kelime = "" + result.Kelime.Substring(0,1);
                            GlobalKelime = result.Kelime;
                            Kelime_textBox1.Text = "" + kelime;
                            Anlami_textBox2.Text = "" + result.Anlami;  
                            geriSayimTimer.Start();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veri bulunamdı 2");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

                                                                        // Bu bölümde öncelikle kaç kelime üretileceğini kontrol eder; süre dolduğunda // In this section, it first checks how many words will be produced; produces new words if time runs out;
                                                                       // yeni kelimeler üretir; Aksi takdirde kontrol süreci yeniden başlar.         // If not, the control process starts again. 
        public async void YeniKelimeGetir(object sender, EventArgs e) // Son olarak üretilecek toplam kelime sayısı tamamlanırsa oyun sona erer.    // Finally, if the total number of words to be produced is completed, the game ends.   
        {
            pictureBox2.Visible = false;                
            pictureBox5.Visible = false;
            try
            {
                FirebaseResponse Uretilecek_Kelime_Adet_Kontrol_1 = await firebaseClient.GetAsync("5");
                Kelime_Adet_Al getdata = Uretilecek_Kelime_Adet_Kontrol_1.ResultAs<Kelime_Adet_Al>();
                if (getdata.Adet_Durum == true)
                {                                      
                    if (i <= 5)
                    {
                        geriSayimSure--; // Her tick'te geriSayimSure değerini bir azalt
                        Timer_label8.Text = geriSayimSure.ToString();
                        

                        if (geriSayimSure <= 0)
                        {
                            geriSayimTimer.Stop();
                            geriSayimSure = 30;
                            RastgeleVeriAlama();
                            el_cevap_textBox.Enabled = true;
                            pictureBox1.Enabled = true;
                            i++;
                        }
                    }
                    else
                    {
                        geriSayimTimer.Stop();
                        MessageBox.Show("Oyun BİTTİ!!!!");
                        oyundurumduzenle();
                        Baslat_button.Visible = true;
                    }
                }
                FirebaseResponse Uretilecek_Kelime_Adet_Kontrol_2 = await firebaseClient.GetAsync("10");
                Kelime_Adet_Al getdata_2 = Uretilecek_Kelime_Adet_Kontrol_2.ResultAs<Kelime_Adet_Al>();
                if (getdata_2.Adet_Durum == true)
                {                  
                    if (i <= 10)
                    {
                        geriSayimSure--; // Her tick'te geriSayimSure değerini bir azalt
                        Timer_label8.Text = geriSayimSure.ToString();

                        if (geriSayimSure <= 0)
                        {
                            geriSayimTimer.Stop();
                            geriSayimSure = 30;
                            RastgeleVeriAlama();
                            el_cevap_textBox.Enabled = true;
                            pictureBox1.Enabled = true;
                            i++;
                        }
                    }
                    else
                    {
                        geriSayimTimer.Stop();
                        MessageBox.Show("Oyun BİTTİ!!!!");
                        Baslat_button.Visible = true;
                        oyundurumduzenle();
                    }
                }

                FirebaseResponse Uretilecek_Kelime_Adet_Kontrol_3 = await firebaseClient.GetAsync("15");
                Kelime_Adet_Al getdata_3 = Uretilecek_Kelime_Adet_Kontrol_3.ResultAs<Kelime_Adet_Al>();
                if (getdata_3.Adet_Durum == true)
                {                  
                    if (i <= 15)
                    {
                        geriSayimSure--; // Her tick'te geriSayimSure değerini bir azalt
                        Timer_label8.Text = geriSayimSure.ToString();

                        if (geriSayimSure <= 0)
                        {
                            geriSayimTimer.Stop();
                            geriSayimSure = 30;
                            RastgeleVeriAlama();

                            i++;
                        }
                    }
                    else
                    {
                        geriSayimTimer.Stop();
                        MessageBox.Show("Oyun BİTTİ!!!!");
                        Baslat_button.Visible = true;
                        oyundurumduzenle();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void pictureBox1_Click(object sender, EventArgs e)    // Verilen çevapların kontrolü sağlanıyor büyük ve küçük harf dikkate alınmıyor bu kontrolde 
        {                                                            // The answers given are checked, uppercase and lowercase letters are not observed in this check.
            try
            {
                if (geriSayimSure >= 1)
                {
                    if (el_cevap_textBox.Text != null)
                    {
                        bool esitMi = el_cevap_textBox.Text.Equals(GlobalKelime, StringComparison.OrdinalIgnoreCase);

                        if (puan_hangi_masanın == 1)
                        {
                            if (esitMi)
                            { 
                                Masa1_Puan = Masa1_Puan + 3;
                                //Masa1_Puan_label8.Text = Masa1_Puan.ToString();

                                FirebaseResponse masa_1_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                                Masa_Toplam_Puan getdata_masa_1_puan = masa_1_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_1_puan.Masa_Puani = Masa1_Puan;
                                FirebaseResponse response_masa_1_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                el_cevap_textBox.Enabled = false;
                                pictureBox1.Enabled = false;
                                pictureBox2.Visible = true;

                            }
                            else
                            {
                                Masa1_Puan--;
                                //Masa1_Puan_label8.Text = Masa1_Puan.ToString();

                                FirebaseResponse masa_1_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                                Masa_Toplam_Puan getdata_masa_1_puan = masa_1_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_1_puan.Masa_Puani = Masa1_Puan;
                                FirebaseResponse response_masa_1_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                pictureBox5.Visible = true;
                            }
                        }
                        else if (puan_hangi_masanın == 2)
                        {
                            if (esitMi)
                            {
                                Masa2_Puan = Masa2_Puan + 3;
                                //Masa2_Puan_label9.Text = Masa2_Puan.ToString();

                                FirebaseResponse masa_2_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                                Masa_Toplam_Puan getdata_masa_2_puan = masa_2_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_2_puan.Masa_Puani = Masa2_Puan;
                                FirebaseResponse response_masa_2_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                el_cevap_textBox.Enabled = false;
                                pictureBox1.Enabled = false;
                                pictureBox2.Visible = true;
                            }
                            else
                            {
                                Masa2_Puan--;
                                //Masa2_Puan_label9.Text = Masa2_Puan.ToString();

                                FirebaseResponse masa_2_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                                Masa_Toplam_Puan getdata_masa_2_puan = masa_2_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_2_puan.Masa_Puani = Masa2_Puan;
                                FirebaseResponse response_masa_2_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                pictureBox5.Visible = true;
                            }
                        }
                        else if (puan_hangi_masanın == 3)
                        {
                            if (esitMi)
                            {
                                Masa3_Puan = Masa3_Puan + 3;
                                //Masa3_Puan_label10.Text = Masa3_Puan.ToString();

                                FirebaseResponse masa_3_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                                Masa_Toplam_Puan getdata_masa_3_puan = masa_3_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_3_puan.Masa_Puani = Masa3_Puan;
                                FirebaseResponse response_masa_3_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                el_cevap_textBox.Enabled = false;
                                pictureBox1.Enabled = false;
                                pictureBox2.Visible = true;
                            }
                            else
                            {
                                Masa3_Puan--;
                                //Masa3_Puan_label10.Text = Masa3_Puan.ToString();

                                FirebaseResponse masa_3_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                                Masa_Toplam_Puan getdata_masa_3_puan = masa_3_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_3_puan.Masa_Puani = Masa3_Puan;
                                FirebaseResponse response_masa_3_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                pictureBox5.Visible = true;
                            }
                        }
                        else if(puan_hangi_masanın == 4)
                        {
                            if (esitMi)
                            {
                                Masa4_Puan = Masa4_Puan + 3;
                                //Masa4_Puan_label11.Text = Masa4_Puan.ToString();

                                FirebaseResponse masa_4_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                                Masa_Toplam_Puan getdata_masa_4_puan = masa_4_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_4_puan.Masa_Puani = Masa4_Puan;
                                FirebaseResponse response_masa_4_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                el_cevap_textBox.Enabled = false;
                                pictureBox1.Enabled = false;
                                pictureBox2.Visible = true;
                            }
                            else
                            {
                                Masa4_Puan--;
                                //Masa4_Puan_label11.Text = Masa4_Puan.ToString();

                                FirebaseResponse masa_4_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                                Masa_Toplam_Puan getdata_masa_4_puan = masa_4_paun.ResultAs<Masa_Toplam_Puan>();
                                getdata_masa_4_puan.Masa_Puani = Masa4_Puan;
                                FirebaseResponse response_masa_4_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4_puan);
                                toplampuangetirTimer.Start();
                                el_cevap_textBox.Text = "";
                                pictureBox5.Visible = true;
                            }                     
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Oyunu başlatmadınız...");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private async void Baslat_button_Click(object sender, EventArgs e)
        {
            FirebaseResponse masa_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
            Oda_1_Masa_Dolumu getdata_masa_1 = masa_1_kontrol.ResultAs<Oda_1_Masa_Dolumu>();
         
            if (getdata_masa_1.Masa_Durum == false)
            {
                FirebaseResponse masa_2_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                Oda_1_Masa_Dolumu getdata_masa_2 = masa_2_kontrol.ResultAs<Oda_1_Masa_Dolumu>();

                if (getdata_masa_2.Masa_Durum == false)
                {
                    FirebaseResponse masa_3_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                    Oda_1_Masa_Dolumu getdata_masa_3 = masa_3_kontrol.ResultAs<Oda_1_Masa_Dolumu>();

                    if ( getdata_masa_3.Masa_Durum == false)
                    {
                        FirebaseResponse masa_4_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                        Oda_1_Masa_Dolumu getdata_masa_4 = masa_4_kontrol.ResultAs<Oda_1_Masa_Dolumu>();

                        if (getdata_masa_4.Masa_Durum == false)
                        {
                            MessageBox.Show("Lütfen bir masa seçiniz");
                        }
                    }
                }          
            }
            else
            {
                i = 0;

                FirebaseResponse masa_1_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                Masa_Toplam_Puan getdata_masa_1_puan = masa_1_paun.ResultAs<Masa_Toplam_Puan>();
                getdata_masa_1_puan.Masa_Puani = 0;
                FirebaseResponse response_masa_1_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1_puan);

                FirebaseResponse masa_2_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                Masa_Toplam_Puan getdata_masa_2_puan = masa_2_paun.ResultAs<Masa_Toplam_Puan>();
                getdata_masa_2_puan.Masa_Puani = 0;
                FirebaseResponse response_masa_2_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2_puan);

                FirebaseResponse masa_3_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                Masa_Toplam_Puan getdata_masa_3_puan = masa_3_paun.ResultAs<Masa_Toplam_Puan>();
                getdata_masa_3_puan.Masa_Puani = 0;
                FirebaseResponse response_masa_3_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3_puan);

                FirebaseResponse masa_4_paun = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                Masa_Toplam_Puan getdata_masa_4_puan = masa_4_paun.ResultAs<Masa_Toplam_Puan>();
                getdata_masa_4_puan.Masa_Puani = 0;
                FirebaseResponse response_masa_4_puan_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4_puan);

                FirebaseResponse Oda_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Oda_1");
                Oda_1_Oyun_Baslangic_Kontrol getdata_oda_1_durum = Oda_1_kontrol.ResultAs<Oda_1_Oyun_Baslangic_Kontrol>();
                getdata_oda_1_durum.Oyun_Aktif_durum = true;
                FirebaseResponse response_oda_1_Update = await firebaseClient_masa_kontrol.UpdateAsync("Oda_1", getdata_oda_1_durum);

                toplampuangetirTimer.Start();
                Baslat_button.Visible = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Oyun_Ayar oyunBaslangicAyar = new Oyun_Ayar();
            oyunBaslangicAyar.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e, string a)
        {
            Ana_From ana_From = new Ana_From(a);
            ana_From.Show();
            this.Hide();
        }

        private async void Masa1_button1_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse masa_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                Oda_1_Masa_Kontrol getdata_masa_1 = masa_1_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
                if (getdata_masa_1.Masa_Durum == false)
                {
                    getdata_masa_1.Masa_Durum = true;
                    FirebaseResponse response_masa_1_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1);
                    puan_hangi_masanın = 1;
                    Masa2_button2.Enabled = false;
                    Masa_3button3.Enabled = false;
                    Masa4_button4.Enabled = false;
                    Masa1_button1.Text = Kullanici_adi;
                    Baslat_button.Visible = true;
                    pictureBox6.Visible = true;
                    label8.Visible = true;


                    FirebaseResponse masa_1_kullanici_adi = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                    Oda_1_Masa_Kullanici_Kontrol getdata_masa_1_kullanici = masa_1_kontrol.ResultAs<Oda_1_Masa_Kullanici_Kontrol>();
                    getdata_masa_1_kullanici.Masa_Kullanici = Masa1_button1.Text;
                    FirebaseResponse response_masa_1_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1_kullanici);
                }
                else
                {
                    MessageBox.Show("Masa dolu!!!");
                }           
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void Masa2_button2_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse masa_2_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                Oda_1_Masa_Kontrol getdata_masa_2 = masa_2_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
                if (getdata_masa_2.Masa_Durum == false)
                {
                    getdata_masa_2.Masa_Durum = true;
                    FirebaseResponse response_masa_2_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2);
                    puan_hangi_masanın = 2;
                    Masa1_button1.Enabled = false;
                    Masa_3button3.Enabled = false;
                    Masa4_button4.Enabled = false;
                    Masa2_button2.Text = Kullanici_adi;
                    pictureBox6.Visible = true;
                    label8.Visible = true;

                    FirebaseResponse masa_2_kullanici_adi = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                    Oda_1_Masa_Kullanici_Kontrol getdata_masa_2_kullanici = masa_2_kontrol.ResultAs<Oda_1_Masa_Kullanici_Kontrol>();
                    getdata_masa_2_kullanici.Masa_Kullanici = Masa2_button2.Text;
                    FirebaseResponse response_masa_2_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2_kullanici);
                }
                else
                {
                    MessageBox.Show("Masa dolu!!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
   
        private async void Masa_3button3_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse masa_3_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                Oda_1_Masa_Kontrol getdata_masa_3 = masa_3_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
                if (getdata_masa_3.Masa_Durum == false)
                {
                    getdata_masa_3.Masa_Durum = true;
                    FirebaseResponse response_masa_3_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3);
                    puan_hangi_masanın = 3;
                    Masa1_button1.Enabled = false;
                    Masa2_button2.Enabled = false;
                    Masa4_button4.Enabled = false;
                    Masa_3button3.Text = Kullanici_adi;
                    pictureBox6.Visible = true;
                    label8.Visible = true;

                    FirebaseResponse masa_3_kullanici_adi = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                    Oda_1_Masa_Kullanici_Kontrol getdata_masa_3_kullanici = masa_3_kontrol.ResultAs<Oda_1_Masa_Kullanici_Kontrol>();
                    getdata_masa_3_kullanici.Masa_Kullanici = Masa_3button3.Text;
                    FirebaseResponse response_masa_3_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3_kullanici);
                }
                else
                {
                    MessageBox.Show("Masa dolu!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void Masa4_button4_Click(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse masa_4_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                Oda_1_Masa_Kontrol getdata_masa_4 = masa_4_kontrol.ResultAs<Oda_1_Masa_Kontrol>();
                if (getdata_masa_4.Masa_Durum == false)
                {
                    getdata_masa_4.Masa_Durum = true;
                    FirebaseResponse response_masa_3_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4);
                    puan_hangi_masanın = 4;
                    Masa1_button1.Enabled = false;
                    Masa2_button2.Enabled = false;
                    Masa_3button3.Enabled = false;
                    Masa4_button4.Text = Kullanici_adi;
                    pictureBox6.Visible = true;
                    label8.Visible = true;

                    FirebaseResponse masa_4_kullanici_adi = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                    Oda_1_Masa_Kullanici_Kontrol getdata_masa_4_kullanici = masa_4_kontrol.ResultAs<Oda_1_Masa_Kullanici_Kontrol>();
                    getdata_masa_4_kullanici.Masa_Kullanici = Masa4_button4.Text;
                    FirebaseResponse response_masa_4_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4_kullanici);
                }
                else
                {
                    MessageBox.Show("Masa dolu!!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Masa1_button1.Text == Kullanici_adi)
                {
                    FirebaseResponse masa_1_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_1");
                    Oda_1_Masa_Durum_Duzenle getdata_masa_1 = masa_1_kontrol.ResultAs<Oda_1_Masa_Durum_Duzenle>();
                    if (getdata_masa_1.Masa_Durum == true)
                    {
                        getdata_masa_1.Masa_Durum = false;
                        FirebaseResponse response_masa_1_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1);
                        Masa2_button2.Enabled = true;
                        Masa_3button3.Enabled = true;
                        Masa4_button4.Enabled = true;
                        Masa1_button1.Text = "Masa 1";
                        getdata_masa_1.Masa_Kullanici = Masa1_button1.Text;
                        FirebaseResponse response_masa_1_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_1", getdata_masa_1);
                        pictureBox6.Visible = false;
                        label8.Visible = false;
                        Baslat_button.Visible = false;
                    }
                }
                else if (Masa2_button2.Text == Kullanici_adi)
                {
                    FirebaseResponse masa_2_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_2");
                    Oda_1_Masa_Durum_Duzenle getdata_masa_2 = masa_2_kontrol.ResultAs<Oda_1_Masa_Durum_Duzenle>();
                    if (getdata_masa_2.Masa_Durum == true)
                    {
                        getdata_masa_2.Masa_Durum = false;
                        FirebaseResponse response_masa_2_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2);
                        Masa1_button1.Enabled = true;
                        Masa_3button3.Enabled = true;
                        Masa4_button4.Enabled = true;
                        Masa2_button2.Text = "Masa 2";
                        getdata_masa_2.Masa_Kullanici = Masa2_button2.Text;
                        FirebaseResponse response_masa_2_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_2", getdata_masa_2);
                        pictureBox6.Visible = false;
                        label8.Visible = false;
                    }
                }
                else if (Masa_3button3.Text == Kullanici_adi)
                {
                    FirebaseResponse masa_3_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_3");
                    Oda_1_Masa_Durum_Duzenle getdata_masa_3 = masa_3_kontrol.ResultAs<Oda_1_Masa_Durum_Duzenle>();
                    if (getdata_masa_3.Masa_Durum == true)
                    {
                        getdata_masa_3.Masa_Durum = false;
                        FirebaseResponse response_masa_3_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3);
                        Masa1_button1.Enabled = true;
                        Masa2_button2.Enabled = true;
                        Masa4_button4.Enabled = true;
                        Masa_3button3.Text = "Masa 3";
                        getdata_masa_3.Masa_Kullanici = Masa_3button3.Text;
                        FirebaseResponse response_masa_3_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_3", getdata_masa_3);
                        pictureBox6.Visible = false;
                        label8.Visible = false;
                    }
                }
                else if (Masa4_button4.Text == Kullanici_adi)
                {
                    FirebaseResponse masa_4_kontrol = await firebaseClient_masa_kontrol.GetAsync("Masa_4");
                    Oda_1_Masa_Durum_Duzenle getdata_masa_4 = masa_4_kontrol.ResultAs<Oda_1_Masa_Durum_Duzenle>();
                    if (getdata_masa_4.Masa_Durum == true)
                    {
                        getdata_masa_4.Masa_Durum = false;
                        FirebaseResponse response_masa_3_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4);
                        Masa1_button1.Enabled = true;
                        Masa2_button2.Enabled = true;
                        Masa_3button3.Enabled = true;
                        Masa4_button4.Text = "Masa 4";
                        getdata_masa_4.Masa_Kullanici = Masa4_button4.Text;
                        FirebaseResponse response_masa_4_kullanici_Update = await firebaseClient_masa_kontrol.UpdateAsync("Masa_4", getdata_masa_4);
                        pictureBox6.Visible = false;
                        label8.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void pictureBox4_Click(object sender, EventArgs e)
        {         

            if (Masa1_button1.Text == Kullanici_adi || Masa2_button2.Text == Kullanici_adi || Masa_3button3.Text == Kullanici_adi || Masa4_button4.Text == Kullanici_adi)
            {
                MessageBox.Show("Litfen önce masadan ayrılınız");
            }
            else
            {
                Ana_From ana_From = new Ana_From(Kullanici_adi);
                ana_From.Show();
                this.Hide();
            }
        }
    }
}
