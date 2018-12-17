using System.IO;

namespace azdo_init
{

    public class InitFile
    {
        private readonly ILogger logger;

        public InitFile(ILogger logger)
{
            this.logger = logger;
        }

        public static RepositoryOptions Deserialize(string path)
        {
            return null;
        }

        public bool Exists()
        {
            return File.Exists("azdo-init.yml");
        }

        public void Create() {
            var fullPath = Path.GetFullPath("azdo-init.yml");
            using (var stream = typeof(InitFile).Assembly.GetManifestResourceStream("azdo_init.template.yml"))
            using (var reader = new StreamReader(stream))
            {
                File.WriteAllText(fullPath, reader.ReadToEnd());
            }
            logger.WriteAction($"created {fullPath}");
        }

    }
}