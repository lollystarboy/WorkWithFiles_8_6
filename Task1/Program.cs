namespace Task1
{
    using System;
    using System.IO;

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("введите путь к файлу");
            var filepath = Console.ReadLine();

            DeleteFile(filepath);
        }
        static void DeleteFile(string filepath)
        {
            // Проверяем, существует ли файл
            if (File.Exists(filepath))
            {
                try
                {
                    // Удаляем файл
                    File.Delete(filepath);
                    Console.WriteLine($"Файл '{filepath}' успешно удален.");
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
           
        }
    }//C:\Users\Загрей\Desktop\delete.me
}
