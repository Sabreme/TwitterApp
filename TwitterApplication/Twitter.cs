using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterApplication
{
    class Twitter
    {
        private List<User> userList = new List<User>();
        private List<Tweet> tweets = new List<Tweet>();
        private const int notFound = -1;        /// 
        private const int messageMax = 140;

        /// <summary>
        /// Loads the User Text File and checks if its valid and creates Users List
        /// </summary>
        /// <param name="userFile"></param>
        public void loadUserFile(string userFile)
        {
            string[] userLines = System.IO.File.ReadAllLines(userFile);

            if (userLines.Length == 0)
            {
                System.Console.WriteLine("Empty User File");
            }
            {
                foreach (string line in userLines)
                {
                    string[] words = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    /// Check if line has User Follows User++ Format
                    if ((words[1].Equals("follows")) && (words.Length >= 3))
                    {
                        AddUserLine(words);
                    }
                    else
                        Console.Write("Invalid Line");                   
                }
            }
        }

        /// <summary>
        /// 1. First we Check to see if User is OLD in the List, then add follow users (but first check if exists)        
        /// 2. If NOT, we check is User is NEW -> then we Add user and follows && check if follows are NEW
        /// </summary>
        /// <param name="words"></param>
        public void AddUserLine(string[] words)
        {
            string newUser = words[0];
            List<string> newFollowsList = new List<string>(words);
            newFollowsList.RemoveAt(0);   /// Remove the User Name
            newFollowsList.RemoveAt(0);   /// Remove the "follows" token           

            int userPosition = checkUserExists(newUser);
            

            /// 1st Case
            /// if userPosition IS FOUND, then the user already exists and we add the new follows and to the user list
            /// if userPosition IS NOT FOUND, we add the new user and their follows to the follows and to the user list
            if (userPosition != notFound)
            {
                User oldUser = userList.ElementAt(userPosition);

                /// If A follow IS NOT in the follows list, then we do Add to follows lsit and check if is a User
                foreach (string follow in newFollowsList)
                {
                    /// If user follows a new User
                    if (!oldUser.userFollows(follow))
                        oldUser.follows.Add(follow);

                    int followUserPosition = checkUserExists(follow);

                    /// If the follow is not a User, we Create a new User from follow
                    if (followUserPosition == notFound)
                        createUserEmptyList(follow);

                }   ///foreach (string follow in newFollowList)
            }   /// If (userPosition != -1) ie: User is currently in the userList
            else
            //2nd Case, the User is NEW && We check the follows Exist
            {
                createUserWithList(newUser, newFollowsList);

                foreach (string follow in newFollowsList)
                {
                    int followUserPosition = checkUserExists(follow);
                    /// If the follow is not a User, we Create a new User from follow
                    if (followUserPosition == notFound)
                    {
                        createUserEmptyList(follow);
                    }
                }   ///foreach (string follwoer in newFollowes)
            } /// NEW USER Found and Check follows for exist                
        }

        /// <summary>
        /// Checks the current userList and returns the position of the User if found
        /// If username is NOT in the list, return NOT FOUND
        /// </summary>
        /// <param name="username"></param>
        /// <param name="follow"></param>
        /// <returns></returns>
        public int checkUserExists(string username)
        {
            int position = notFound;
            int index = 0;

            while (index < userList.Count)
            {
                if (userList.ElementAt(index).userName.Equals(username))
                {
                    position = index;
                    break;
                }
                else
                    index++;
            }            
            return position;
        }

        public void createUserEmptyList(string userName)
        {
            User newUser = new User();
            newUser.userName = userName;
            userList.Add(newUser);
        }

        public void createUserWithList(string newUser, List<string> followsList)
        {
            User newUserObject = new User();
            newUserObject.userName = newUser;
            newUserObject.follows = followsList;
            userList.Add(newUserObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tweetFile"></param>
        public void loadTweetFile(string tweetFile)
        {
            string[] tweetLines = System.IO.File.ReadAllLines(tweetFile);

            foreach (string line in tweetLines)
            {
                if (isValidTweet(line))
                {
                    Tweet newUserTweet = new Tweet();
                    newUserTweet.userName = getUserNameFromTweetLine(line);
                    newUserTweet.message = getUserMessageFromTweetLine(line);
                    tweets.Add(newUserTweet);
                }
            }
        }

        /// <summary>
        /// Assuming Valid tweet if Username is not BLANK and > is present
        /// 
        /// </summary>
        /// <param name="tweetLine"></param>
        /// <returns></returns>
        public bool isValidTweet(string tweetLine)
        {
            int gts = tweetLine.IndexOf('>');

            string userName = tweetLine.Substring(0, gts);
            string message = tweetLine.Substring(gts, tweetLine.Length - gts);

            if (gts !=0 && !userName.Equals(" ") && message != null)
                return true;
            else
                return false;
        }

        public string getUserNameFromTweetLine(string tweetLine)
        {
            int gts = tweetLine.IndexOf('>');

            string userName = tweetLine.Substring(0, gts);

            return userName;
        }

        public string getUserMessageFromTweetLine(string tweetLine)
        {
            int gts = tweetLine.IndexOf('>');
            int gtspadding = 2;

            string message = tweetLine.Substring(gts + gtspadding, tweetLine.Length - gts - gtspadding);

            if (message.Length > messageMax)
                return message.Substring(0, messageMax);
            else
                return message;
        }

        public void LoadFiles(string userFile, string tweetfile)
        {
            loadUserFile(userFile);
            loadTweetFile(tweetfile);
        }

        public void PrintTweets()
        {
            /// C# Sorted list by using LINQ to orderBy (object username) -> List again
            List<User> SortedList = userList.OrderBy(o => o.userName).ToList(); 
            
            foreach (User user in SortedList)
            {
                Console.WriteLine(user.userName);

                foreach (Tweet tweet in tweets)
                {
                    if (tweet.userName.Equals(user.userName))
                        Console.WriteLine("\t@" + user.userName + ":" + tweet.message);
                    else
                        

                    foreach (string follow in user.follows)
                    {
                        if (tweet.userName.Equals(follow))
                            Console.WriteLine("\t@" + follow + ":" + tweet.message);
                    }
                }               
            }
        }
    }
}
