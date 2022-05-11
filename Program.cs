using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;

namespace SampleChapter14Demo
{
    class StudentApp
    {
        const char DELIM = ',';
        const string END = "end";
        // what is the filename
        const string FILENAME = "Student.txt ";
        // stuff is wrong with this next line...
        public static List<Student> stuList = new List<Student>();

        static void Main()
        {


            //load file, fix this line
            if (loadFile(FILENAME)==false)
            {
                WriteLine("File Loaded Successfully");

                Student stu = new Student();

                Write("Add Student number or '" + END + "' to quit ");
                stu.StuNum = ReadLine();
                while (stu.StuNum != END)
                {
                    Write("Enter last name ");
                    stu.LName = ReadLine();
                    Write("Enter first name ");
                    stu.FName = ReadLine();
                    Write("Enter gpa ");
                    stu.Gpa = Convert.ToDouble(ReadLine());
                    // what do I need to do next? add one line of code.
                    stuList.Add(stu);
                    //

                    Write("Enter Student number or " + END + " to quit ");
                    stu.StuNum = ReadLine();
                }

                if (saveFile(FILENAME) == false)
                {
                    WriteLine("File Saved successfully.");
                }
                else
                {
                    WriteLine("File Save Error");
                }



            } else
            {
                WriteLine("File Load Error");
            }

        }

        private static bool saveFile(string outFileName)
        {
            bool error = false;

            try
            {
                FileStream outFile = new FileStream(FILENAME, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(outFile);

                // different construct used to loop through List
                foreach(Student curStu in stuList)
                {
                    string outRec = curStu.StuNum;
                    outRec += "," + curStu.LName;
                    outRec += "," + curStu.FName;
                    outRec += "," + curStu.Gpa.ToString();

                    writer.WriteLine(outRec);
                }
                outFile.Close();
            }
            catch
            {
                error = true;
            }
            return error;
        }

        public static bool loadFile(string inFileName)
        {
            WriteLine("\n{0,-5}{1,-12}{2,8}\n", "Num", "Name", "GPA");

            bool error = false;

            try
            {
                FileStream inFile = new FileStream(inFileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);

                String recordIn;
                string[] fields;
                recordIn = reader.ReadLine();


                while (recordIn != null)
                {
                    // get the record pulled in
                    fields= recordIn.Split(DELIM);

                    Student stu = new Student();
                    // how to parse out 
                    stu.StuNum =fields[0] ;
                    stu.FName = fields[2];
                    stu.LName = fields[1];
                    stu.Gpa = Convert.ToDouble(fields[3]);
                    stuList.Add(stu);



                    WriteLine("{0,-5}{1,-12}{2,8}", stu.StuNum, stu.LName, stu.FName, stu.Gpa.ToString("F2"));

                    recordIn = reader.ReadLine();

                }
                inFile.Close();
            }
            catch
            {
                error = true;
            }
            return error;
        }

    }
    public class Student
    {
        private string stuNum;
        private string lname;
        private string fname;
        private double gpa;
        private const double MINGPA = 0.0;
        private const double MAXGPA = 4.0;
        public string StuNum
        {
            get { return stuNum; }
            set { stuNum = value; }
        }
        public string LName
        {
            get { return lname; }
            set { lname = value; }
        }
        public string FName
        {
            get { return fname; }
            set { fname = value; }
        }
        public double Gpa
        {
            get { return gpa; }
            set
            {
                if (value >= MINGPA && value <= MAXGPA)
                    gpa = value;
                else
                    gpa = 0;
            }
        }
    }
}
