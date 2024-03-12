namespace OOP_Backup
{
    class Backup
    {
        private string sourcePath;
        private string destinationPath;
        private string logFilePath;
        private bool generateLogfile;

        public Backup(string sourceFolder, string destinationFolder)
        {
            sourcePath = sourceFolder;
            destinationPath = destinationFolder;
            generateLogfile = false;
        }
        public Backup(string sourceFolder, string destinationFolder, string Logfile)
        {
            sourcePath = sourceFolder;
            destinationPath = destinationFolder;
            logFilePath = Logfile;
            generateLogfile = true;
        }
        private bool checkPath()
        {
            if (!String.IsNullOrEmpty(sourcePath) && !String.IsNullOrEmpty(destinationPath))
                return true;

            return false;
        }
        public void Start()
        {
            if(checkPath())
            {
                File.Copy(sourcePath, destinationPath, true);
            }
        }
        ~Backup() {
            Console.WriteLine("Objekt zerstört!");
        }


    }

}
