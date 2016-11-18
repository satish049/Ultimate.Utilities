using System.Collections.Generic;

namespace Ultimate.Utilities.Tests
{
    public class MockBuilders
    {

    }

    #region Mock Builders

    public static class CollectionsMockBuilder
    {
        public static List<string> GetValidStringsList()
        {
            return new List<string> { "Satish", "Kumar" };
        }

        public static Student GetValidStudent()
        {
            return new Student() { Name = "satish", Age = 10, ClassName = "SSC" };
        }

        public static IList<Student> GetValidStudentsList()
        {
            return new List<Student>()
            {
                new Student() {Name = "satish", Age = 10, ClassName = "SSC"},
                new Student() {Name = "kumar", Age = 11, ClassName = "SSC"},
                new Student() {Name = "rama", Age = 12, ClassName = "SSC"}
            };
        }

        public static IList<Parent> GetValidParentList()
        {
            return new List<Parent>()
            {
                new Child(){ParentName = "Parent1",ParentAge = 31},
                new Child(){ParentName = "Parent2",ParentAge = 32}
            };
        }


        public static IList<Child> GetValidChildList()
        {

            return new List<Child>()
            {
                new Child(){ParentName = "Parent1",ParentAge = 31,ChildName = "Child1",ChildAge = 1},
                new Child(){ParentName = "Parent2",ParentAge = 32,ChildName = "Child2",ChildAge = 2}
            };
        }
    }

    public static class ObjectsMockBuilder
    {
        public static Student GetValidStudent()
        {
            return new Student() { Name = "satish", Age = 10, ClassName = "SSC" };
        }

        public static IList<Student> GetValidStudentsList()
        {
            return new List<Student>()
            {
                new Student() {Name = "satish", Age = 10, ClassName = "SSC"},
                new Student() {Name = "kumar", Age = 11, ClassName = "SSC"},
                new Student() {Name = "rama", Age = 12, ClassName = "SSC"}
            };
        }

        public static StudentDuplicate GetValidStudentDuplicate()
        {
            return new StudentDuplicate() { Name = "satish", Age = 10, ClassName = "SSC" };
        }

        public static IList<StudentDuplicate> GetValidStudentDuplicateList()
        {
            return new List<StudentDuplicate>()
            {
                new StudentDuplicate() {Name = "satish", Age = 10, ClassName = "SSC"},
                new StudentDuplicate() {Name = "kumar", Age = 11, ClassName = "SSC"},
                new StudentDuplicate() {Name = "rama", Age = 12, ClassName = "SSC"}
            };
        }
    }

    #endregion




    #region Classes

    public class StudentComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x.Age == y.Age)
                return 0;
            if (x.Age < y.Age)
                return -1;
            return 1;
        }
    }

    public class Parent
    {
        public string ParentName { get; set; }

        public int ParentAge { get; set; }


    }

    public class Child : Parent
    {

        public string ChildName { get; set; }

        public int ChildAge { get; set; }

    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Student() { }

        public Student(string name, int age, string className)
        {
            Name = name;
            Age = age;
            ClassName = className;
        }

        public string ClassName { get; set; }
        public object Clone()
        {
            return new Student(Name, Age, ClassName);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class StudentDuplicate
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public StudentDuplicate() { }

        public StudentDuplicate(string name, int age, string className)
        {
            Name = name;
            Age = age;
            ClassName = className;
        }

        public string ClassName { get; set; }
 
        public override string ToString()
        {
            return Name;
        }
    }


    #endregion
}
