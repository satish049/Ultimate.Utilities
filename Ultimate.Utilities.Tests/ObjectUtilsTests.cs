using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ultimate.Utilities.Tests
{
    [TestFixture]
    public class ObjectUtilsTests
    {
        private Student _student;
        private StudentDuplicate _studentDuplicate;
        private IList<Student> _studentList;
        private IList<StudentDuplicate> _studentDuplicateList;

        [OneTimeSetUp]
        protected void OnSetup()
        {
            _student = ObjectsMockBuilder.GetValidStudent();
            _studentDuplicate = ObjectsMockBuilder.GetValidStudentDuplicate();
            _studentList = ObjectsMockBuilder.GetValidStudentsList();
            _studentDuplicateList = ObjectsMockBuilder.GetValidStudentDuplicateList();
        }

        #region IfNotNull

        [Test]
        public void IfNotNulll()
        {
            Student student = null;
            Assert.AreEqual(student.IfNotNull(x=>x.Age),0);
            Assert.AreEqual(student.IfNotNull(x => x.Name), null);
        }

        #endregion

        #region IfNotNull

        [Test]
        public void DeepClone()
        {
            var expectedStudent = ObjectUtils.DeepClone(_student);
            Assert.AreNotSame(expectedStudent,_student);
            Assert.AreEqual(expectedStudent.Name,_student.Name);
        }

        #endregion

        #region IfNotNull

        [Test]
        public void MapObjects()
        {
            var expectedStudent = new StudentDuplicate();
            ObjectUtils.MapObjects(_student,expectedStudent);
            Assert.AreEqual(expectedStudent.Name, _student.Name);
            Assert.AreEqual(expectedStudent.Age, _student.Age);
        }

        [Test]
        public void MapObjectsList()
        {
            IList<StudentDuplicate> expectedStudentList = ObjectUtils.MapObjectsList<Student, StudentDuplicate>(_studentList);
            Assert.AreEqual(expectedStudentList.Count, _studentList.Count);
            Assert.AreEqual(expectedStudentList[0].Name, _studentList[0].Name);
            Assert.AreEqual(expectedStudentList[1].Age, _studentList[1].Age);
        }

        #endregion



    }
}
