using cw2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] argumenty = new string[3];


            if (args.Length >= 1)
                argumenty[0] = args[0];
            else
                argumenty[0] = @"data.csv";

            if (args.Length >= 2)
                argumenty[1] = args[1];
            else
                argumenty[1] = @"żesult.xml";


            if (args.Length >= 3)
                argumenty[2] = args[2];
            else
                argumenty[2] = @"xml";

            for (int i = 0; i < argumenty.Length; i++)
                Console.WriteLine(argumenty[i]);

            

            var log = File.Create("łog.txt");

            using (StreamWriter writer = new StreamWriter(log)) 
            {

                try
                {

                    var setStudentow = new System.Collections.Generic.HashSet<Student>(new OwnComparator());


                    using (var stream = new StreamReader(File.OpenRead(Path.GetFullPath(argumenty[0]))))
                    {
                        string line = null;
                        var regex = new Regex("$\\s+");

                        while ((line = stream.ReadLine()) != null)
                        {

                            bool wpisywac = true;

                            string[] student = line.Split(',');


                            for (int i = 0; i < student.Length; i++)
                            {
                                var matches = regex.Matches(student[i]);
                                if (matches.Count >= 1)
                                {
                                    writer.WriteLine(line);
                                    wpisywac = false;
                                    break;
                                }
                            }

                            if (student.Length == 9 && wpisywac == true)
                            {
                                var st = new Student
                                {
                                    index = student[4],
                                    fName = student[0],
                                    lName = student[1],
                                    birthdate = student[5],
                                    email = student[6],
                                    mothersName = student[7],
                                    fathersName = student[8],
                                    studiesName = student[2],
                                    studiesMode = student[3]


                                };


                                if (!setStudentow.Add(st))
                                {
                                    writer.WriteLine(line);
                                }


                            }

                            FileStream Typewriter = new FileStream(argumenty[1], FileMode.Create);
                            

                            if (argumenty[2] == "xml")
                            {
                                StreamWriter Bwriter = new StreamWriter(Typewriter);
                                XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>), new XmlRootAttribute("uczelnia"));
                                serializer.Serialize(Typewriter, setStudentow);
                                Bwriter.Dispose();

                            }
                            else if (argumenty[2] == "json")
                            {
                                using (StreamWriter Jsonwriter = new StreamWriter(Typewriter))
                                {
                                    foreach (var stud in setStudentow)
                                        Jsonwriter.WriteLine(JsonConvert.SerializeObject(stud));

                                }
                            
                            }

                            Typewriter.Close();
                            Typewriter.Dispose();

                        }
                    }

                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Podana ścieżka jest niepoprawna");
                    writer.WriteLine("Podana ścieżka jest niepoprawna");
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Plik nazwa nie istnieje");
                    writer.WriteLine("Plik nazwa nie istnieje");
                }
            }

        }
    }
}
