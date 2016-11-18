using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ultimate.Utilities.Tests
{
    [TestFixture]
    public class CollectionUtilsTest
    {
        public List<string> StringList = new List<string>();
        public List<string> StrColl1 = new List<string>();
        public List<string> StrColl2 = new List<string>();

        [SetUp]
        protected void OnSetup()
        {
            StringList = CollectionsMockBuilder.GetValidStringsList();
            StrColl1 = new List<string> { "Satish", "Kumar" };
            StrColl2 = new List<string> { "Satish", "KK" };
        }

        #region AddAll

        [Test]
        public void TestAddAll()
        {
            var collection = new List<string>{ "Satish", "Kumar" };
            var elements = new[] { "Roronoa", "Zoro" };
            Assert.True(CollectionUtils.AddAll(collection, elements));
            Assert.True(collection.Contains("Roronoa"));
        }

        [Test]
        public void TestAddAllByEnumerable()
        {
            var collection = new List<string> { "Satish", "Kumar" };
            IList<string> elements = new List<string> { "Roronoa", "Zoro" };
            Assert.True(CollectionUtils.AddAll(collection, elements));
            Assert.True(collection.Contains("Roronoa"));
        }

        [Test]
        public void TestAddAllByList()
        {
            var collection = new List<string> { "Satish", "Kumar" };
            var elements = new[] { "Roronoa", "Zoro" };
            collection.AddRange(elements);
            Assert.True(collection.Contains("Roronoa"));
        }

        [Test]
        public void TestAddAllByNullCollection()
        {
            var elements = new[] { "Roronoa", "Zoro" };
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddAll(null, elements));
        }

        [Test]
        public void TestAddAllByNullElements()
        {
            var collection = new List<string> { "Satish", "Kumar" };
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddAll(collection, null));
        }

        [Test]
        public void TestAddAllByInt()
        {
            var collection = new List<int> { 1, 2 };
            var elements = new[] { 3, 4 };
            Assert.True(CollectionUtils.AddAll(collection, elements));
            Assert.True(collection.Contains(3));
        }

        #endregion

        #region AddAllIgnoreNull

        [Test]
        public void TestAddAllIgnoreNullSuccess()
        {
            var elements = new List<string> { "Roronoa", "Zoro", null };
            Assert.True(CollectionUtils.AddAllIgnoreNull(StringList, elements));
            Assert.True(StringList.Contains("Roronoa"));
            Assert.True(!StringList.Contains(null));

        }

        [Test]
        public void TestAddAllIgnoreNullByNullElementsCollThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => CollectionUtils.AddAllIgnoreNull(StringList, null));
        }

        [Test]
        public void TestAddAllIgnoreNullByNullMainCollThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddAllIgnoreNull(null, StringList));
        }


        #endregion

        #region AddAllAtIndex

        [Test]
        public void TestAddAllAtIndexSuccess()
        {
            var elements = new List<string> { "Roronoa", "Zoro", null };
            Assert.True(CollectionUtils.AddAllAtIndex(StringList, elements, 1));
            Assert.True(StringList.Contains("Roronoa"));
            Assert.AreEqual(1, StringList.IndexOf("Roronoa"));
            Assert.AreEqual(5, StringList.Count);
            Assert.True(StringList.Contains(null));

        }

        [Test]
        public void TestAddAllAtIndexWithIncludeNullsFalseSuccess()
        {
            var elements = new List<string> { "Roronoa", "Zoro", null };
            Assert.True(CollectionUtils.AddAllAtIndex(StringList, elements, 1, false));
            Assert.True(!StringList.Contains(null));

        }

        [Test]
        public void TestAddAllAtIndexByNullElementsCollThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddAllAtIndex(StringList, null, 1));
        }

        [Test]
        public void TestAddAllAtIndexByNullMainCollThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddAllAtIndex(null, StringList, 1));
        }

        #endregion

        #region AddIgnoreNull

        [Test]
        public void TestAddIgnoreNullSuccess()
        {
            Assert.True(CollectionUtils.AddIgnoreNull(StringList, "SS"));
            Assert.False(CollectionUtils.AddIgnoreNull(StringList, null));
            Assert.True(StringList.Contains("SS"));
            Assert.True(!StringList.Contains(null));

        }


        [Test]
        public void TestAddgnoreNullByNullMainCollThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.AddIgnoreNull(null, "SS"));
        }


        #endregion

        #region Collate

        [Test]
        public void TestCollateByInt()
        {
            var collection = new List<int> { 1, 2 };
            var elements = new[] { 3, 4 };
            var expected = new List<int> { 1, 2, 3, 4 };
            var actual = CollectionUtils.Collate(collection, elements);
            Assert.AreEqual(expected, actual);
            Assert.True(actual[2] == 3);
        }

        [Test]
        public void TestCollateByString()
        {
            var collection = new List<string> { "Satish", "Kumar", "Zoro" };
            IList<string> elements = new List<string> { "Roronoa", "Zoro" };
            var actual = CollectionUtils.Collate(collection, elements);
            var expected = new List<string> { "Kumar", "Roronoa", "Satish", "Zoro" };
            Assert.AreEqual(expected, actual);
            Assert.True(actual[2] == "Satish");
        }

        [Test]
        public void TestCollateByStringWithDuplicates()
        {
            var collection = new List<string> { "Satish", "Kumar", "Zoro" };
            IList<string> elements = new List<string> { "Roronoa", "Zoro" };
            var actual = CollectionUtils.Collate(collection, elements, false);
            var expected = new List<string> {  "Kumar",  "Roronoa", "Satish", "Zoro", "Zoro" };
            Assert.AreEqual(expected, actual);
            Assert.True(actual[3] == "Zoro");
        }

        [Test]
        public void TestCollateByStringByNullCollectionThrowsException()
        {
            var collection = new List<string> { "Satish", "Kumar", "Zoro" };
            IList<string> elements = new List<string> { "Roronoa", "Zoro" };
            Assert.Throws<ArgumentNullException>(() => CollectionUtils.Collate(null, elements));
            Assert.Throws<ArgumentNullException>(() => CollectionUtils.Collate(collection, null));
        }

        #endregion

        #region CollateWithComparer

        [Test]
        public void TestCollateWithComaparerSuccess()
        {
            var students1 = CollectionsMockBuilder.GetValidStudentsList();
            var students2 = new List<Student>
            {
                new Student() {Name = "AA", Age = 17, ClassName = "SSC"},
                new Student() {Name = "BB", Age = 23, ClassName = "SSC"},
                new Student() {Name = "CC", Age = 14, ClassName = "SSC"}
            };
            var actual = CollectionUtils.Collate(students1, students2, new StudentComparer(), false);
            Assert.IsNotNull(actual);
            Assert.AreEqual(6, actual.Count);

        }

        [Test]
        public void TestCollateWithComaparerByNullCollectionsThrowsException()
        {
            var students1 = CollectionsMockBuilder.GetValidStudentsList();
            var students2 = new List<Student>
            {
                new Student() {Name = "AA", Age = 17, ClassName = "SSC"},
                new Student() {Name = "BB", Age = 23, ClassName = "SSC"},
                new Student() {Name = "CC", Age = 14, ClassName = "SSC"}
            };
            Assert.Throws<ArgumentNullException>(
                () => CollectionUtils.Collate(null, students2, new StudentComparer(), false));
            Assert.Throws<ArgumentNullException>(
                () => CollectionUtils.Collate(students1, null, new StudentComparer(), false));
            Assert.Throws<ArgumentException>(() => CollectionUtils.Collate(students1, students2, null, false));

        }

        #endregion

        #region CollectFromParent

        [Test]
        public void TestCollectFromParentSuccess()
        {
            var parentList = CollectionsMockBuilder.GetValidParentList();
            var actual = CollectionUtils.CollectFromParent<Parent, Child>(parentList);
            Assert.IsTrue(actual.Any(i => i.ParentName.Equals("Parent1")));
        }

        [Test]
        public void TestCollectFromParentWithInvalidInputThrowsException()
        {
            var parentList = CollectionsMockBuilder.GetValidParentList();
            parentList.Add(new Parent());
            Assert.Throws<InvalidCastException>(() => CollectionUtils.CollectFromParent<Parent, Child>(parentList));
        }


        [Test]
        public void TestCollectFromParentWithNullInputThrowsException()
        {
            var parentList = CollectionsMockBuilder.GetValidParentList();
            parentList.Add(new Parent());
            Assert.Throws<NullReferenceException>(() => CollectionUtils.CollectFromParent<Parent, Child>(null));
        }


        #endregion

        #region CollectFromChild

        [Test]
        public void TestCollectFromChildSuccess()
        {
            var childList = CollectionsMockBuilder.GetValidChildList();
            var actual = CollectionUtils.CollectFromChild<Child, Parent>(childList);
            Assert.IsTrue(actual.Any(i => i.ParentName.Equals("Parent1")));

        }

        [Test]
        public void TestCollectFromChildWithNullInputThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.CollectFromParent<Parent, Child>(null));
        }


        #endregion

        #region ContainsAll
        [Test]
        public void TestContainsAllByNullColl2()
        {

            Assert.Throws<ArgumentNullException>(() => CollectionUtils.ContainsAll(StringList, null));
        }

        [Test]
        public void TestContainsAllByNullColl1()
        {

            Assert.Throws<ArgumentNullException>(() => CollectionUtils.ContainsAll(null, StringList));
        }

        [Test]
        public void TestContainsAllSuccess()
        {

            Assert.IsTrue(CollectionUtils.ContainsAll(StrColl1, StringList));
        }
        #endregion

        #region ContainsAny
        [Test]
        public void TestContainsAnySuccess()
        {

            Assert.IsTrue(CollectionUtils.ContainsAny(StrColl1, StrColl2));
        }

        [Test]
        public void TestContainsAnyReturnsFalse()
        {

            var coll2 = new List<string> { "KK" };
            Assert.IsFalse(CollectionUtils.ContainsAny(StrColl1, coll2));
        }

        #endregion

        #region Disjunction
        [Test]
        public void TestDisjunctionSuccess()
        {
            var expected = new List<string> { "Kumar", "KK" };
            Assert.AreEqual(expected, CollectionUtils.Disjunction(StrColl1, StrColl2));
            Assert.IsTrue(!CollectionUtils.Disjunction(StrColl1, StrColl1).Contains("Satish"));
        }

        [Test]
        public void TestDisjunctionByNullInputThrowsException()
        {

        }

        #endregion

        #region EmptyCollection
        [Test]
        public void TestEmptyCollectionSuccess()
        {
            var actual = CollectionUtils.EmptyCollection<String>();
            Assert.AreEqual(new List<String>(), actual);
            Assert.Throws<NotSupportedException>(() => actual.Add("Satish"));
        }
        #endregion

        #region EmptyIfNull

        [Test]
        public void TestEmptyIfNullByNullCollection()
        {
            Assert.IsEmpty(CollectionUtils.EmptyIfNull<string>(null));
        }

        [Test]
        public void TestEmptyIfNullByProperCollection()
        {
            Assert.IsNotEmpty(CollectionUtils.EmptyIfNull(StringList));
        }

        #endregion

        #region IsEmpty

        [Test]
        public void TestIsEmptySuccess()
        {
            Assert.IsTrue(CollectionUtils.IsEmpty((List<Student>) null));
            Assert.IsTrue(CollectionUtils.IsEmpty(new List<Student>()));
            Assert.IsFalse(CollectionUtils.IsEmpty(StringList));
        }
        #endregion

        #region IsNotEmpty

        [Test]
        public void TestIsNotEmptySuccess()
        {
            Assert.IsFalse(CollectionUtils.IsNotEmpty((List<Student>) null));
            Assert.IsFalse(CollectionUtils.IsNotEmpty(new List<Student>()));
            Assert.IsTrue(CollectionUtils.IsNotEmpty(StringList));
        }
        #endregion

        #region GetCardinalityMap

        [Test]
        public void TestGetCardinalityMapSuccess()
        {
            var strList = new List<string> { "satish", "satish", "kumar", "SK" };
            var map = CollectionUtils.GetCardinalityMap(strList);
            Assert.AreEqual(2, map["satish"]);
        }

        [Test]
        public void TestGetCardinalityMapByNullCollThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => CollectionUtils.GetCardinalityMap((List<Student>) null));
        }

        #endregion

        #region IsSubCollection

        [Test]
        public void TestIsSubCollectionSuccess()
        {
            var coll1 = new List<string> { "Satish", "Kumar", "KK" };
            var coll2 = new List<string> { "Satish", "KK" };
            Assert.IsTrue(CollectionUtils.IsSubCollection(coll2, coll1));
        }

        #endregion

        #region IsProperSubCollection

        [Test]
        public void TestIsProperSubCollectionSuccess()
        {
            var coll1 = new List<string> { "Satish", "Kumar", "KK" };
            var coll2 = new List<string> { "Satish", "KK" };
            Assert.IsTrue(CollectionUtils.IsProperSubCollection(coll2, coll1));
        }

        [Test]
        public void TestIsProperSubCollectionByEquals()
        {
            var coll1 = new List<string> { "Satish", "KK" };
            var coll2 = new List<string> { "Satish", "KK" };
            Assert.IsFalse(CollectionUtils.IsProperSubCollection(coll2, coll1));
        }



        #endregion

        #region IsEqualCollection

        [Test]
        public void TestIsEqualCollectionSuccess()
        {
            var coll1 = new List<string> { "Satish", "KK" };
            var coll2 = new List<string> { "KK", "Satish" };
            Assert.IsFalse(coll1 == coll2);
            Assert.IsTrue(CollectionUtils.IsEqualCollection(coll2, coll1));
        }

        #endregion

        #region IsEqualCollectionWithPredicate

        [Test]
        public void TestIsEqualCollectionWithPredicateSuccess()
        {
            var coll1 = new List<string> { "Satish", "KK" };
            var coll2 = new List<string> { "KK", "Satish" };
            var std1 = CollectionsMockBuilder.GetValidStudentsList();
            var std2 = new List<Student>
            {
                new Student() {Name = "satish", Age = 10, ClassName = "SSC"},
                new Student() {Name = "kumar", Age = 11, ClassName = "SSC"},
                new Student() {Name = "rama", Age = 12, ClassName = "SSC"}
            };

            Assert.IsFalse(coll1 == coll2);
            Assert.IsTrue(CollectionUtils.IsEqualCollection(coll1, coll2, (x) => coll2.Contains(x)));
            Assert.IsTrue(CollectionUtils.IsEqualCollection(std1, std2, (x) => std2.Any(i => i.Age == x.Age && i.Name == x.Name && i.ClassName.Equals(x.ClassName))));
        }

        #endregion

        #region Clone

        [Test]
        public void TestCloneSuccess()
        {
            var studentList = CollectionsMockBuilder.GetValidStudentsList();
            var actual = CollectionUtils.Clone(studentList);
            var newStudent = new Student("newName", 1, "10th");
            actual.Add(newStudent);
            Assert.IsTrue(!studentList.Contains(newStudent));
            Assert.IsTrue(actual.Contains(newStudent));
        }
        #endregion

        #region RemoveAll

        [Test]
        public void TestRemoveAll()
        {
            var strList = new List<string>{"sa","sb","sc","sd","se"};
            var removeList = new List<string>{"sb","sc"};
            var actual = CollectionUtils.RemoveAll(strList, removeList);
            Assert.IsTrue(!actual.Contains("sb"));
            Assert.IsTrue(!actual.Contains("sc"));
            Assert.IsTrue(strList.Contains("sb"));
        }

        #endregion

        #region FindWithRegEx

        [Test]
        public void TestFindWithRegex()
        {
            var emails = new List<string>{"satish049@gmail.com","sadadada@gmail.com","sadasfswf","dasdgadjgas","kumar@gmail.com"};
            var actual = CollectionUtils.FindWithRegEx(emails, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var expected = new List<string> { "satish049@gmail.com", "sadadada@gmail.com",  "kumar@gmail.com" };
            Assert.AreEqual(expected,actual);

        }


        #endregion


    }
}
