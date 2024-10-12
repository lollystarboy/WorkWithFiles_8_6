using System.Runtime;

namespace Task3
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
                DeleteFile(filepath);
                
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

        private static long  DeleteFile(string filepath)
        {
            long freedSpace = 0; // Переменная для хранения объема освобожденного пространства

            if (File.Exists(filepath))
            {
                try
                {
                    // Получаем размер файла перед удалением
                    FileInfo file = new FileInfo(filepath);
                    freedSpace += file.Length; // Сохраняем размер файла

                    // Удаляем файл
                    File.Delete(filepath);
                    Console.WriteLine($"Файл '{filepath}' успешно удален.");
                    Console.WriteLine($"{freedSpace} {file.Length}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Файл '{filepath}' не найден.");
            }

            return freedSpace; // Возвращаем объем освобожденного пространства
        }
    }
}