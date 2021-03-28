using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
        //Constant = Sabit
    {
        public static string ProductAddedMessage = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductGettedByCategory = "Ürünler seçilen kategoriye göre listelendi";
        public static string ProductCantGettedByCategory = "Ürünler seçilen kategoriye göre listelenemedi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 adet ürün olabilir.";
        public static string ProductNameExists = "Girdiğiniz ürün ismi önceden alınmış";
        public static string CategoryLimitError = "Kategori limiti aşıldı";

        public static string AuthorizationDenied = "Erişim reddi";
        internal static string UserRegistered = "Kullanıcı Kayıt Oldu";
        internal static string UserNotFound = "Kullanıcı bulunamadı";
        internal static string PasswordError = "Şifre hatası";
        internal static string SuccessfulLogin = "Başarılı Giriş";
        internal static string UserAlreadyExists = "Kullanıcı Mevcut";
        internal static string AccessTokenCreated = "Token Oluşturuldu";
        internal static string NotRegistered = "Kullanıcı kaydı oluşturulurken bir hata ile karşılaşıldı.";
    }
}
