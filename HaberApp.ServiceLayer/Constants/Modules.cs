namespace HaberApp.ServiceLayer.Constants
{
    public static class Modules
    {
        public static class CategoryModule
        {
            public const string Create = "Permission.Category.Create";
            public const string Read = "Permission.Category.Read";
            public const string Update = "Permission.Category.Update";
            public const string Delete = "Permission.Category.Delete";

        }

        public static class NewsModule
        {
            public const string Create = "Permission.News.Create";
            public const string Read = "Permission.News.Read";
            public const string Update = "Permission.News.Update";
            public const string Delete = "Permission.News.Delete";

        }

        public static class CategoryUrlsModule
        {
            public const string Create = "Permission.CategoryUrl.Create";
            public const string Read = "Permission.CategoryUrl.Read";
            public const string Update = "Permission.CategoryUrl.Update";
            public const string Delete = "Permission.CategoryUrl.Delete";
        }
        public static class SiteSettings
        {
            public const string Create = "Permission.SiteSettings.Create";
            public const string Read = "Permission.SiteSettings.Read";
            public const string Update = "Permission.SiteSettings.Update";
            public const string Delete = "Permission.SiteSettings.Delete";
        }
    }
}
