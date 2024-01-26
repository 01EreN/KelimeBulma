using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Linq;
using System.Windows.Forms;
using FireSharp;
using System.Collections.Generic;

namespace OyunDeneme1
{

    public partial class Giris_From2 : Form
    {
        string Kullanici_Adi_Al;
        IFirebaseConfig config = new FirebaseConfig
        {
            // Firebase projesinin url adresi
            BasePath = "",
            // Firebase setting sayfasindan aldigimiz secret key
            AuthSecret = ""
        };
            
        FirebaseClient client;
        Dictionary<string, FireBase_User_Pulling> PullingDictionary; // Oluşturulan "FireBase_User_Pulling" sınıfını Dictionary olarak tanımladık 
                                                                     // We defined the created "FireBase_User_Pulling" class as Dictionary
        public Giris_From2()
        {
            InitializeComponent();

           
            client = new FireSharp.FirebaseClient(config);

        }


        private void Giris_From2_Load(object sender, EventArgs e)
        {

        }


        private async void Giris_button2_Click(object sender, EventArgs e)
        {
            try
            {
                // class tanımlaması yapıyoruz 
                // class  defining 
                User_Check user_Check = new User_Check()
                {
                    Kullanici_Adi_O = Kullanici_adi_textBox1.Text,
                    Sifre_O = Sifre_textBox2.Text,
                };

                // bir kullanıcı anahtarı  çekiyoruz 
                // a users key pullding
                var kullanici_Nick_Name = await client.GetAsync("Users");

                // çekeceğimiz veri ile aynı türde tanımladığımız değişkenin içine kullanıcı anahtarını alıyoruz alıyoruz
                //We take the user key into the variable we defined, which is the same type as the data we will retrieve.
                PullingDictionary = kullanici_Nick_Name.ResultAs<Dictionary<string, FireBase_User_Pulling>>();

                if (PullingDictionary != null) // dolu mu diye bakıyoruz 
                {
                    bool loginSuccessful = false; // Döngü oluşturmak için bool açıyoruz ki doğru karşılaştırma yapıldığında döngüden çıkabilelim ve istediğmiz giriş işlemini gerçekleştirelim 
                                                  // we open bool to create a loop so that when the correct comparison is made, we can exit the loop and perform the input operation we want. 

                    foreach (var user_Get in PullingDictionary)
                    { 
                        if (user_Get.Value.Kullanici_Adi == user_Check.Kullanici_Adi_O && user_Get.Value.Sifre == user_Check.Sifre_O)  // çekilen anahtarın altındaki verileri elimizdeki ile karşılaştırıyoruz
                        {                                                                                                              // we compare the data under the pulled key with the one we have
                            loginSuccessful = true;
                            Giris_button2.Enabled = false;
                            Kullanici_Adi_Al = user_Get.Value.Kullanici_Adi.ToString();
                            break;
                        }
                    }

                    if (loginSuccessful) // işlemler doğruysa giriş sağlıyoruz 
                    {                    // If the transactions are correct, we log in.
                        Ana_From Ana_From = new Ana_From(Kullanici_Adi_Al);
                        Ana_From.Show();
                        this.Hide();                     
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                        Giris_button2.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Böyle bir kullanıcı bulunamadı");
                    Giris_button2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş sırasında hata oluştu: " + ex.Message);
            }
        }

        private void Kayit_button1_Click(object sender, EventArgs e)
        {
            Kayit_Form1 Kayit_Form1 = new Kayit_Form1();
            Kayit_Form1.Show();
            this.Hide();
        }

    
    }
}