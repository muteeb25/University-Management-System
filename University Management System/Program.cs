using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace University_Management_System
{
        internal class Program
        {
            static void Main(string[] args)
            {
                const string teacherFilePath = "C:\\Users\\DELL\\Desktop\\cp\\FacultyModule.txt";

                // Load teacher data from file
                var teacherData = ReadTeacherData(teacherFilePath);

                string[,] studentData = {
            {"MUTEEB", "037", "1st", "100%", "4.00", "030**"},
            {"KHAQAN", "040", "1st", "98%", "4.00", "030**"},
            {"AHSAN", "061", "1st", "100%", "4.00", "030**"},
            {"HASSAN", "077", "1st", "30%", "4.00", "030**"}
                };

                char s;
                do
                {
                    DisplayHeader();

                    Console.Write("Enter department name (BSE, BCS, BBA, BCE): ");
                    string department = Console.ReadLine().ToLower();

                    if (department == "bse")
                    {
                        HandleBSE(studentData, teacherData, teacherFilePath);
                    }
                    else if (department == "bce" || department == "bcs" || department == "bba")
                    {
                        DisplayGenericMessage();
                    }
                    else
                    {
                        Console.WriteLine("Invalid department. Exiting program.");
                    }

                    Console.WriteLine("Press Y to restart and N to exit:");
                    s = Convert.ToChar(Console.ReadLine().ToLower());
                } while (s == 'y');
            }

            static void DisplayHeader()
            {
                Console.WriteLine("**");
                Console.WriteLine("*                        Bahria  University                        *");
                Console.WriteLine("*                          Karachi Campus                          *");
                Console.WriteLine("**");
                Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}\t\t\t                    Time: {DateTime.Now.ToLongTimeString()}");
                Console.WriteLine("**");
            }

            static List<string[]> ReadTeacherData(string filePath)
            {
                var teacherList = new List<string[]>();
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        teacherList.Add(line.Split(','));
                    }
                }
                else
                {
                    Console.WriteLine($"File '{filePath}' not found. Starting with empty data.");
                }
                return teacherList;
            }

            static void WriteTeacherData(string filePath, List<string[]> teacherList)
            {
                var lines = new List<string>();
                foreach (var teacher in teacherList)
                {
                    lines.Add(string.Join(",", teacher));
                }
                File.WriteAllLines(filePath, lines);
            }

            static void HandleBSE(string[,] studentData, List<string[]> teacherData, string teacherFilePath)
            {
                Console.Write("Enter type (student or faculty): ");
                string type = Console.ReadLine().ToLower();

                if (type == "student")
                {
                    HandleStudent(studentData);
                }
                else if (type == "faculty")
                {
                    HandleFaculty(studentData, teacherData, teacherFilePath);
                }
                else
                {
                    Console.WriteLine("Invalid type. Exiting program.");
                }
            }

            static void HandleStudent(string[,] studentData)
            {
                Console.Write("Enter student name: ");
                string studentName = Console.ReadLine().ToLower();

                for (int i = 0; i < studentData.GetLength(0); i++)
                {
                    if (studentData[i, 0].ToLower() == studentName)
                    {
                        DisplayStudentData(studentData, i);
                        return;
                    }
                }

                Console.WriteLine("Student not found.");
            }

            static void HandleFaculty(string[,] studentData, List<string[]> teacherData, string teacherFilePath)
            {
                Console.Write("Enter faculty type (HOD or Teacher): ");
                string facultyType = Console.ReadLine().ToLower();

                if (facultyType == "teacher")
                {
                    Console.Write("Enter teacher name: ");
                    string teacherName = Console.ReadLine().ToLower();

                    for (int i = 0; i < teacherData.Count; i++)
                    {
                        if (teacherData[i][0].ToLower() == teacherName)
                        {
                            DisplayTeacherData(teacherData, i);
                            return;
                        }
                    }

                    Console.WriteLine("Teacher not found.");
                }
                else if (facultyType == "hod")
                {
                    Console.Write("Enter HOD name: ");
                    string hodName = Console.ReadLine().ToLower();

                    Console.WriteLine("WELCOME TO HOD system, {0}", hodName);
                    HandleHOD(studentData, teacherData, teacherFilePath);
                }
                else
                {
                    Console.WriteLine("Invalid faculty type. Exiting program.");
                }
            }

            static void HandleHOD(string[,] studentData, List<string[]> teacherData, string teacherFilePath)
            {
                int choice;
                do
                {
                    Console.WriteLine("Select option:");
                    Console.WriteLine("1. Inspect");
                    Console.WriteLine("2. Update");
                    Console.WriteLine("3. Exit");

                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            HandleInspect(studentData, teacherData);
                            break;
                        case 2:
                            HandleUpdate(studentData, teacherData, teacherFilePath);
                            break;
                        case 3:
                            Console.WriteLine("Exiting HOD system.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                } while (choice != 3);
            }

            static void HandleInspect(string[,] studentData, List<string[]> teacherData)
            {
                Console.Write("Search for (student or teacher): ");
                string searchType = Console.ReadLine().ToLower();

                if (searchType == "student")
                {
                    DisplayStudentDataArray(studentData);
                }
                else if (searchType == "teacher")
                {
                    DisplayTeacherDataArray(teacherData);
                }
                else
                {
                    Console.WriteLine("Invalid search type.");
                }
            }

            static void HandleUpdate(string[,] studentData, List<string[]> teacherData, string teacherFilePath)
            {
                Console.Write("Update data for (student or teacher): ");
                string updateType = Console.ReadLine().ToLower();

                if (updateType == "student")
                {
                    Console.Write("Enter student name to update: ");
                    string updateStudentName = Console.ReadLine().ToLower();

                    for (int i = 0; i < studentData.GetLength(0); i++)
                    {
                        if (studentData[i, 0].ToLower() == updateStudentName)
                        {
                            UpdateStudentData(studentData, i);
                            return; // Exit after updating and displaying student data
                        }
                    }

                    Console.WriteLine("Student not found.");
                }
                else if (updateType == "teacher")
                {
                    Console.Write("Enter teacher name to update: ");
                    string updateTeacherName = Console.ReadLine().ToLower();

                    for (int i = 0; i < teacherData.Count; i++)
                    {
                        if (teacherData[i][0].ToLower() == updateTeacherName)
                        {
                            UpdateTeacherData(teacherData, i);

                            // Save the updated teacher data to file
                            WriteTeacherData(teacherFilePath, teacherData);
                            Console.WriteLine("Teacher data has been updated and saved to file.");
                            return; // Exit after updating and displaying teacher data
                        }
                    }

                    Console.WriteLine("Teacher not found.");
                }
                else
                {
                    Console.WriteLine("Invalid update type.");
                }
            }

            static void DisplayStudentData(string[,] data, int index)
            {
                Console.WriteLine("**");
                Console.WriteLine("S.NO | NAME   | Enroll | SEMISTER  | ATTENDANCE  | GPA  | PHONE");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("{0,4} | {1,-6} | {2,6} | {3,9} | {4,11} | {5,4} | {6}", index + 1, data[index, 0], data[index, 1], data[index, 2], data[index, 3], data[index, 4], data[index, 5]);
                Console.WriteLine("**");
            }

            static void DisplayTeacherData(List<string[]> data, int index)
            {
                Console.WriteLine("*");
                Console.WriteLine("S.No  | NAME         |EMPLOYEE No.  | SUBJECT    | ATTENDANCE  | PHONE");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("{0,3}   | {1,-12} | {2,12} | {3,-10} | {4,11} | {5}", index + 1, data[index][0], data[index][1], data[index][2], data[index][3], data[index][4]);
                Console.WriteLine("**");
            }

            static void DisplayStudentDataArray(string[,] data)
            {
                Console.WriteLine("**");
                Console.WriteLine("S.No  | NAME         | Enroll | SEMESTER  | ATTENDANCE | GPA | PHONE");
                Console.WriteLine("-------------------------------------------------------------------");
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    Console.WriteLine("{0,3}   | {1,-12} | {2,6} | {3,-10} | {4,11} | {5,4} | {6}", i + 1, data[i, 0], data[i, 1], data[i, 2], data[i, 3], data[i, 4], data[i, 5]);
                }
                Console.WriteLine("**");
            }

            static void DisplayTeacherDataArray(List<string[]> data)
            {
                Console.WriteLine("**");
                Console.WriteLine("S.No | NAME      | EMPLOYEE No.  | SUBJECT    | ATTENDANCE | PHONE");
                Console.WriteLine("-------------------------------------------------------------------");
                for (int i = 0; i < data.Count; i++)
                {
                    // Check if the row has the expected number of columns
                    if (data[i].Length == 5)
                    {
                        Console.WriteLine("{0,3}   | {1,-10} | {2,12} | {3,-10} | {4,11} | {5}",
                            i + 1, data[i][0], data[i][1], data[i][2], data[i][3], data[i][4]);
                    }
                    else
                    {
                        Console.WriteLine("{0,3}   | Invalid data entry at line {1}", i + 1, i + 1);
                    }
                }
                Console.WriteLine("**");
            }
            static void UpdateStudentData(string[,] data, int index)
            {
                Console.WriteLine("Select field to update:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Enroll");
                Console.WriteLine("3. Semester");
                Console.WriteLine("4. Attendance");
                Console.WriteLine("5. GPA");
                Console.WriteLine("6. Phone");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.Write("Enter updated value: ");
                    string updatedValue = Console.ReadLine();

                    switch (choice)
                    {
                        case 1:
                            data[index, 0] = updatedValue;
                            break;
                        case 2:
                            data[index, 1] = updatedValue;
                            break;
                        case 3:
                            data[index, 2] = updatedValue;
                            break;
                        case 4:
                            data[index, 3] = updatedValue;
                            break;
                        case 5:
                            data[index, 4] = updatedValue;
                            break;
                        case 6:
                            data[index, 5] = updatedValue;
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            return; // Exit early to avoid extra output
                    }

                    Console.WriteLine("\nUpdated student data:");
                    DisplayStudentData(data, index); // Single call to display updated data
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            static void UpdateTeacherData(List<string[]> data, int index)
            {
                Console.WriteLine("Select field to update:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Employee Number");
                Console.WriteLine("3. Subject");
                Console.WriteLine("4. Attendance");
                Console.WriteLine("5. Phone Number");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.Write("Enter updated value: ");
                    string updatedValue = Console.ReadLine();

                    switch (choice)
                    {
                        case 1:
                            data[index][0] = updatedValue;
                            break;
                        case 2:
                            data[index][1] = updatedValue;
                            break;
                        case 3:
                            data[index][2] = updatedValue;
                            break;
                        case 4:
                            data[index][3] = updatedValue;
                            break;
                        case 5:
                            data[index][4] = updatedValue;
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            return; // Exit early to prevent extra output
                    }

                    Console.WriteLine("\nUpdated teacher data:");
                    DisplayTeacherData(data, index); // Single call to display updated data
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }

            static void DisplayGenericMessage()
            {
                Console.WriteLine("This department's data is not available at the moment.");
            }
        }

    }

