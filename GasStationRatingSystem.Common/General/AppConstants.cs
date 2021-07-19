using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.Common
{
  public  static class AppConstants
    {


        public struct StatusCodes
        {
            public const int Success = 200;
            public const int Error = 200;

        }
        public static readonly object[] EmptyValues = { Guid.Empty, string.Empty, null };

        public const string _SuperUserIdCookie = "App.Super.GasStationRatingSystem.UserId";
        public const string _UserIdCookie = "App.GasStationRatingSystem.UserId";
        public const string LanguageCodeCookie = "App.GasStationRatingSystem.LanguageCode";
        public const string LanguageIdCookie = "App.GasStationRatingSystem.LanguageId";
        public const string LanguageRtlCookie = "App.GasStationRatingSystem.Language.IsRtl";
        public const string DefaultPassword = "mtx@123";
        public static readonly string EncryptKey = "n1xdl54xsefeghk9z3xodibpmctoneyj";
        public const string SuccessUrl = "~/Setting/Xero/Success";
        public const string TrackingBranches = "Branches";
        public const string XeroWarehouse = "INVENTORY";
        public static  Guid InvoicePageId = Guid.Parse("c43e42f4-b60e-422a-b25f-e2c66ff5d0f8");
        public const string ProductsExcelName = "Products.xlsx";
        public const string TempProductsExcelName = "TempProducts.xlsx";

        public struct Areas
        {
            public const string api = nameof(api);

            public const  string Guide = nameof(Guide);
            public const  string Setting = nameof(Setting);
            public const string People = nameof(People);
            public const string Page = nameof(Page);
            public const string Operation = nameof(Operation);
            public const string Report = nameof(Report);



        }
        public struct Words
        {

            public const string Enabled = "مفعل";
            public const string Disabled = "غير مفعل";
            public const string Client = "عميل";
            public const string Branch = "فرع";


        }
        public struct Messages
        {

            public const string SavedSuccess = "تمت عملية الحفظ بنجاح";
            public const string SavedFailed = "حدث خطا";
            public const string RequiredMessage = "هذا الحقل مطلوب";

            public const string StopTitle = "توقف";
            public const string DeletedSuccess = "تم الحذف بنجاح";
            public const string DeletedFailed = "حدث خطا اثناء الحذف";
            public const string ChangedStatusSuccess = "تم تغيير الحالة بنجاح";
            public const string ChangedStatusFailed = "حدث خطا اثناء تغيير الحالة";
            public const string NameAlreadyExists = "الاسم موجود من قبل";
            public const string NameRequired = "الاسم مطلوب";
            public const string PreparationSuccess = "تمت عملية تجهيز النظام بنجاح";
            public const string InvoiceCodeAlreadyExists = "كود الفاتورة موجود من قبل";
            public const string ConfigXero = "من فضلك قم بالدخول على صفحة تجهيز النظام لتحديث كود الصلاحية";
            public const string ErrorInXeroWhenAddInvoice = "حدث خطا اثناء اضافة الفاتورة في نظام Xero";
            public const string DataRequired = "من فضلك قم بادخال البيانات الناقصة";
            public const string ProductNotScheduledOnToday = "اليوم غير مجدول لهذا المنتج";

            public const string ExportedFailed = "حدث خطا اثناء التصدير";
            public const string ExportedSuccess = "تمت عملية التصدير بنجاح";

            public const string ImportedFailed = "حدث خطا اثناء الاستيراد";
            public const string ImportedSuccess = "تمت عملية الاستيراد بنجاح";
            public const string ProductIsExists = "المنتج تم اضافته من قبل";

        }
        public struct UserMessages
        {

            public const string UsernameAlreadyExists = "اسم المستخدم موجود من قبل";
            public const string EmailAlreadyExists = "البريد الالكتروني موجود من قبل";


        }

        public struct ClientMessages
        {

            public const string ClientUniqueNameAlreadyExists = "اسم المستخدم موجود من قبل";
            public const string FullNameAlreadyExists = "الاسم موجود من قبل";


        }


    }
}
