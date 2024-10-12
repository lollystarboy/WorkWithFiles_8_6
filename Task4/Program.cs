namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    namespace StudentDataLoader
    {
        [Serializable]
        public class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal AverageGrade { get; set; }
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                
                string binaryFilePath = @"BinaryReadWrite-master/BinaryReadWrite/SampleDataFile/students.dat";

                try
                {
                    // Считываем данные о студентах
                    var students = LoadStudentsFromBinaryFile(binaryFilePath);

                    // Путь к директории на рабочем столе
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string studentsDirectory = Path.Combine(desktopPath, "Students");

                    // Создаем директорию "Students" на рабочем столе
                    Directory.CreateDirectory(studentsDirectory);

                    // Записываем данные о студентах в текстовые файлы по группам
                    SaveStudentsToGroupFiles(students, studentsDirectory);

                    Console.WriteLine("Данные успешно загружены и сохранены в директории 'Students'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            static List<Student> LoadStudentsFromBinaryFile(string filePath)
            {
                var students = new List<Student>();

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int studentCount = reader.ReadInt32(); // Читаем количество студентов

                        for (int i = 0; i < studentCount; i++)
                        {
                            var student = new Student
                            {
                                Name = reader.ReadString(),
                                Group = reader.ReadString(),
                                DateOfBirth = DateTime.FromBinary(reader.ReadInt64()),
                                AverageGrade = reader.ReadDecimal()
                            };
                            students.Add(student);
                        }
                    }
                }
                return students;
            }

            static void SaveStudentsToGroupFiles(List<Student> students, string directoryPath)
            {
                // Словарь для хранения студентов по группам
                var groups = new Dictionary<string, List<Student>>();

                // Разделяем студентов по группам
                foreach (var student in students)
                {
                    if (!groups.ContainsKey(student.Group))
                    {
                        groups[student.Group] = new List<Student>();
                    }
                    groups[student.Group].Add(student);
                }

                // Записываем студентов в текстовые файлы по группам
                foreach (var group in groups)
                {
                    string groupFilePath = Path.Combine(directoryPath, $"{group.Key}.txt");

                    using (StreamWriter writer = new StreamWriter(groupFilePath))
                    {
                        foreach (var student in group.Value)
                        {
                            writer.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}, {student.AverageGrade}");
                        }
                    }
                }
            }
        }
    }
}