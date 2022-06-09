namespace ORM_Generics
{
    internal class FileRoot
    {
        public static string GetDefaultDirectory()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
        }
    }
}
