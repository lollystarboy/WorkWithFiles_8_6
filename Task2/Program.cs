namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к директории:");
            var filepath = Console.ReadLine();

            // Создаем объект DirectoryInfo
            DirectoryInfo dirInfo = new DirectoryInfo(filepath);

            // Проверяем, существует ли директория
            if (dirInfo.Exists)
            {
                long size = DirSize(dirInfo);
                Console.WriteLine($"Размер директории: {size} байт");
            }
            else
            {
                Console.WriteLine("Директория не найдена.");
            }
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;

            FileInfo[] fls = d.GetFiles();
            foreach (FileInfo f in fls)
            {
                size += f.Length;
            }

            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
