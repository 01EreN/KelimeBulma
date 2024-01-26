using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OyunDeneme1
{
    internal class User
    {
        // Kayıt işleminde firebase veri göndermek için değişkenler tanımladık
        // we have defined variables to send data to firebase in registration process
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Kullanici_Adi { get; set;}
        public string Sifre { get; set; }

    }
}
