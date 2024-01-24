namespace SpicyTemplate.Utulities.Extentions
{
    public static class FileValidator
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool CheckSize(this IFormFile file, int size)
        {
            if (file.Length < size * 1024 * 1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CreafeFileAsync(this IFormFile file, string root, params string[] folders)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            };
            return fileName;
        }
    }
}
