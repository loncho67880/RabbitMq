namespace Common.ReadTemplateHelper
{
    public class ReadTemplateHelper : IReadTemplateHelper
    {
        public string ReadTemplate(string path)
        {
            string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            return File.ReadAllText(pathFile);
        }
    }
}
