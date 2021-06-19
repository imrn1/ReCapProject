using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kullanıcı kaydedildi";
        public static string UserAlreadyExists = "Kullanıcı zaten kayıtlı";
        public static string AccessTokenCreated = "Access Token oluşturuldu";

        public static string BrandAdded = "Marka eklendi";
        public static string BrandNameInvalid = "Marka adı geçersiz";

        public static string ColorAdded = "Renk eklendi";
        public static string ColorNameInvalid = "Renk adı geçersiz";

        public static string CarAdded = "Araba eklendi";
        public static string CarNameInvalid = "Araba adı geçersiz";

        public static string MaintenanceTime = "Bakım zamanı işleminiz yapılamıyor.";
        public static string InvalidId = "Geçersiz id";

        public static string InvalidName = "Geçersiz isim";

        public static string SuccessDelete = "silindi";
        public static string SuccessUpdate = "güncellendi";

        public static string UserNotFound = "Kullanıcı bulunamadı";

        public static string WrongPassword = "Şifre hatalı";

        public static string SuccessLogin = "Giriş başarılı";
    }
}
