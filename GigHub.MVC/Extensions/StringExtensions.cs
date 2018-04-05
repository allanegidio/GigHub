namespace GigHub.MVC.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string text) => !string.IsNullOrEmpty(text) ? true : false; 
    }
}