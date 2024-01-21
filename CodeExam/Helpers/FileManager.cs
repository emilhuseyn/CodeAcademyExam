namespace CodeExam.Helpers
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file, string envpath,string foldername)
        {
            string filename=file.FileName;
            filename=Guid.NewGuid().ToString()+filename;
            string path=envpath+foldername+filename;
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
        public static bool CheckType(this IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                return false;
            }
            return true;
        }
        public static bool CheckLength(this IFormFile file)
        {
            if (file.Length > 1024 * 1024)
            {
                return false;
            }
            return true;
        }
    }
}
