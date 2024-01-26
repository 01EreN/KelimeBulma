using System;
using System.Windows.Forms;
using FirebaseAdmin.Auth;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;
using FirebaseAdmin;
using System.Collections.Generic;

namespace OyunDeneme1
{
    public partial class Kayit_Form1 : Form
    {

       

        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = ""
        };
        // Firebase client
        IFirebaseClient client;

        Dictionary<string, FireBase_User_Pulling> PullingDictionary;

        public Kayit_Form1()
        {
            InitializeComponent();

            //bağlantı client değişkenine atıyoruz 
            //We assign it to the connection client variable
            client = new FireSharp.FirebaseClient(config);

        }


        private void Kayit_Form1_Load(object sender, EventArgs e)
        {

        }

        private void Giris_button2_Click(object sender, EventArgs e)
        {
            Giris_From2 Giris_From2 = new Giris_From2();
            Giris_From2.Show();
            this.Hide();
        }

        private async void Kayit_button1_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    // class tanımlaması yapıyoruz 
                    // class  defining 
                    Ad = Ad_textBox1.Text,
                    Soyad = Soyad_textBox2.Text,
                    Kullanici_Adi = Kullanici_adi_textBox3.Text,
                    Sifre = Sifre_textBox4.Text
                };

                if (!string.IsNullOrEmpty(user.Ad) && !string.IsNullOrEmpty(user.Soyad) &&
                    !string.IsNullOrEmpty(user.Kullanici_Adi) && !string.IsNullOrEmpty(user.Sifre))
                {



                    var kullanici_Nick_Name_check = await client.GetAsync("Users");

                    // çekeceğimiz veri ile aynı türde tanımladığımız değişkenin içine kullanıcı anahtarını alıyoruz alıyoruz
                    //We take the user key into the variable we defined, which is the same type as the data we will retrieve.
                    PullingDictionary = kullanici_Nick_Name_check.ResultAs<Dictionary<string, FireBase_User_Pulling>>();

                    if (PullingDictionary != null) // dolu mu diye bakıyoruz 
                    {
                        bool loginSuccessful = true; // Döngü oluşturmak için bool açıyoruz ki doğru karşılaştırma yapıldığında döngüden çıkabilelim ve istediğmiz giriş işlemini gerçekleştirelim 
                                                     // we open bool to create a loop so that when the correct comparison is made, we can exit the loop and perform the input operation we want. 

                        foreach (var user_Get in PullingDictionary)
                        {
                            if (user_Get.Value.Kullanici_Adi == user.Kullanici_Adi)  // çekilen anahtarın altındaki verileri elimizdeki ile karşılaştırıyoruz
                            {                                                          // we compare the data under the pulled key with the one we have
                                loginSuccessful = false;
                                break;

                            }
                        }

                        if (loginSuccessful) // işlemler doğruysa giriş sağlıyoruz 
                        {                    // If the transactions are correct, we log in.



                            // Tablo adı ve kullanıcı için belirleyeceğiniz id veya key (primary key) atıyoruz
                            string firebasePath = "Users/" + user.Kullanici_Adi; // This assumes using the username as the key

                            FirebaseResponse getResponse = await client.GetAsync(firebasePath);
                            if (getResponse != null || getResponse.Body != "null")
                            {
                                // user classı ve firebasepath değişkenlerini gönderiyoruz 
                                // We send the user class and firebasepath variables
                                SetResponse setResponse = await client.SetAsync(firebasePath, user);
                                Ana_From Ana_From = new Ana_From(null);
                                Ana_From.Show();
                                this.Hide();
                                MessageBox.Show("Kayıt başarılı!");
                            }
                            else
                            {
                                MessageBox.Show("Kayıt gerçekleştirilemdi bilgilerinizi kontrol ediniz");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı dah önce alınmıştır tekrar deneyiniz");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tekrar deneyin");
                    }

                }
                else
                {
                    MessageBox.Show("Bilgileri eksiksiz giriniz");
                }
            }
            catch (FirebaseException ex)
            {
                MessageBox.Show("Kayıt sırasında hata oluştu: " + ex.Message);
            }
        }
    }
}