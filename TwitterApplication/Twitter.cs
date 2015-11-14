using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterApplication
{
    class Twitter
    {
        private List<User> userList = new List<User>();

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
                    string[] words = line.Split(new[] { ',' , ' '}, StringSplitOptions.RemoveEmptyEntries);

                    /// Check if line has User Follows User++ Format
                    if ((words[1].Equals("follows")) && (words.Length >= 3))
                    {
                        AddUserLine(words);


                        Console.Write("User: " + words[0]);
                        Console.Write("Follows Token: " + words[1]);
                        Console.Write("Next User: " + words[2]);
                    }
                    else
                        Console.Write("Invalid Line");                
                    Console.WriteLine("\n");
                }
            }
        }

        /// <summary>
        /// 1. First we Check to see if User is Empty -> Then we add user and follows && make follows users
        /// 2. If NOT, we check is User is new -> then we Add user and follows && check if follows are new
        ///-- 3. If NOT, We follows to that list
        ///-- 3. Otherwise we Insert A new User follow line
        /// </summary>
        /// <param name="words"></param>
        public void AddUserLine(string[] words)
        {
            string newUser = words[0];               
            List<string> newFollowsList = new List<string>(words);
            newFollowsList.RemoveAt(0);   /// Remove the User Name
            newFollowsList.RemoveAt(0);   /// Remove the "follows" token           

            /// 1st Case,  the userList is empty
            if (userList.Count == 0)
            {
                                
                User newUserObject = new User();
                newUserObject.userName = newUser;
                newUserObject.follows = newFollowsList;
                userList.Add(newUserObject);


                foreach (string follow in newFollowsList)
                {
                    User newfollowUser = new User();
                    newfollowUser.userName = follow;
                    userList.Add(newfollowUser);
                }

            }
            else
                ///2nd Case, the User is Old && We check the follows Exist
            {
                int userPosition = checkUserExists(newUser);

                /// if userPosition IS NOT -1 then the user already exists and we add the new follows and to the user list
                /// if userPosition IS  -1, we add the new user and their follows to the follows and to the user list

                if (userPosition != -1)
                {
                    User oldUser = userList.ElementAt(userPosition);

                    /// If A follow IS NOT in the follows list, then we do Add to follows lsit and check if is a User
                    foreach (string follow in newFollowsList)
                    {
                        if (!oldUser.userHasfollow(follow))
                        {
                            int followUserPosition = checkUserExists(follow);

                            oldUser.follows.Add(follow);

                            /// If the follow is not a User, we Create a new User from follow
                            if (followUserPosition == -1)
                            {
                                User newfollowUser = new User();
                                newfollowUser.userName = follow;
                                userList.Add(newfollowUser);
                            }
                        }
                        /// The follow is an existing follow and need to check if existing User
                        else
                        {
                            int followUserPosition = checkUserExists(follow);   

                            /// If the follow is not a User, we Create a new User from follow
                            if (followUserPosition == -1)
                            {
                                User newfollowUser = new User();
                                newfollowUser.userName = follow;
                                userList.Add(newfollowUser);
                            }
                        }
                    }   ///foreach (string follwoer in newFollowes)
                }   /// If (userPosition != -1) ie: User is currently in the userList
                else
                //3nd Case, the User is NEW && We check the follows Exist
                {
                    User newUserObject = new User();
                    newUserObject.userName = newUser;
                    newUserObject.follows = newFollowsList;
                    userList.Add(newUserObject);

                    foreach (string follow in newFollowsList)
                    {
                        int followUserPosition = checkUserExists(follow);
                        /// If the follow is not a User, we Create a new User from follow
                        if (followUserPosition == -1)
                        {
                            User newfollowUser = new User();
                            newfollowUser.userName = follow;
                            userList.Add(newfollowUser);
                        }
                    }   ///foreach (string follwoer in newFollowes)
                } /// NEW USER Found and Check follows for exist                
            }                        
        }

        /// <summary>
        /// Checks the current userList and returns the position of the username if found
        /// If username is NOT in the list, return -1
        /// </summary>
        /// <param name="username"></param>
        /// <param name="follow"></param>
        /// <returns></returns>
        public int checkUserExists(string username)
        {
            int position = -1; 
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
            //User currentUser  = userList.se
            return position;
        }

        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tweetFile"></param>
        public void loadTweetFile(string tweetFile)
        {
            string[] tweetLines = System.IO.File.ReadAllLines(tweetFile);

            System.Console.WriteLine("Tweets found = " + tweetLines.Length);

            foreach (string line in tweetLines)
            {
                Console.WriteLine(line + "\n");
            }
        }

        public void LoadFiles(string userFile, string tweetfile)
        {
            loadUserFile(userFile);
            loadTweetFile(tweetfile);
        }
    }
}
