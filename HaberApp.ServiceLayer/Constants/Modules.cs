namespace HaberApp.ServiceLayer.Constants
{
    public static class Modules
    {
        public static class CategoryModule
        {
            public const string Create = "Permission.Kategori.Create";
            public const string Read = "Permission.Kategori.Read";
            public const string Update = "Permission.Kategori.Update";
            public const string Delete = "Permission.Kategori.Delete";

        }

        public static class NewsModule
        {
            public const string Create = "Permission.Haber.Create";
            public const string Read = "Permission.Haber.Read";
            public const string Update = "Permission.Haber.Update";
            public const string Delete = "Permission.Haber.Delete";

        }

        public static class CategoryUrlsModule
        {
            public const string Create = "Permission.KategoriUrl.Create";
            public const string Read = "Permission.KategoriUrl.Read";
            public const string Update = "Permission.KategoriUrl.Update";
            public const string Delete = "Permission.KategoriUrl.Delete";
        }
        public static class SiteSettings
        {
            public const string Create = "Permission.SiteAyarları.Create";
            public const string Read = "Permission.SiteAyarları.Read";
            public const string Update = "Permission.SiteAyarları.Update";
            public const string Delete = "Permission.SiteAyarları.Delete";
        }
    }
}
