﻿using TwitterApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Moq;

namespace TwitterAppUnitTests
{
    
    
    /// <summary>
    ///This is a test class for TwitterTest and is intended
    ///to contain all TwitterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TwitterTest
    {
        
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddUserLine
        ///</summary>
        [TestMethod()]
        public void AddUserLineTest()
        {
            Twitter target = new Twitter(); // TODO: Initialize to an appropriate value
            string[] words = null; // TODO: Initialize to an appropriate value
            target.AddUserLine(words);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkUserExists
        ///</summary>
        [TestMethod()]
        public void checkUserExistsTest()
        {
            Twitter target = new Twitter(); 
            string username = "Alan"; 
            int expected = 1; 
            int actual;
            actual = target.checkUserExists(username);
            Assert.AreEqual(expected, actual);      
        }

        /// <summary>
        ///A test for LoadFiles
        ///</summary>
        [TestMethod()]
        public void LoadFilesTest()
        {
            Twitter target = new Twitter(); // TODO: Initialize to an appropriate value
            var assembly = typeof(TwitterTest).Assembly;
            var assemblyPath = Path.GetDirectoryName(new Uri(assembly.EscapedCodeBase).LocalPath);

            var userPath = Path.Combine(assemblyPath, "user.txt");
            var tweetPath = Path.Combine(assemblyPath, "tweet.txt");
          
            target.LoadFiles(userPath, tweetPath);  
            
        }

        /// <summary>
        ///A test for createUserEmptyList
        ///</summary>
        [TestMethod()]
        public void createUserEmptyListTest()
        {
            Twitter target = new Twitter(); 
            string userName = "Martin"; 
            int oldSize = target.userList.Count;
            target.createUserEmptyList(userName);
            Assert.AreNotEqual(oldSize, target.userList.Count);            
        }

        /// <summary>
        ///A test for createUserWithList
        ///</summary>
        [TestMethod()]
        public void createUserWithListTest()
        {
            Twitter target = new Twitter(); 
            string newUser = "Ward"; 
            int oldSize = target.userList.Count;
            List<string> followsList = new List<string> {"Alan"}; 
            target.createUserWithList(newUser, followsList);            
            Assert.AreNotEqual(oldSize, target.userList.Count);
        }

        /// <summary>
        ///A test for loadTweetFile
        ///</summary>
        [TestMethod()]
        public void loadTweetFileTest()
        {
            Twitter target = new Twitter(); // TODO: Initialize to an appropriate value
            string tweetFile = string.Empty; // TODO: Initialize to an appropriate value
            target.loadTweetFile(tweetFile);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for loadUserFile
        ///</summary>
        [TestMethod()]
        public void loadUserFileTest()
        {
            Twitter target = new Twitter(); // TODO: Initialize to an appropriate value
            string userFile = string.Empty; // TODO: Initialize to an appropriate value
            target.loadUserFile(userFile);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
