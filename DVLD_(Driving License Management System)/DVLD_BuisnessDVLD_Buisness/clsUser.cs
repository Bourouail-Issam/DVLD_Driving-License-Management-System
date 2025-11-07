using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int UserID { set; get; }
        public int PersonID { set; get; }

        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        // ###################   Constructors   ###################
        public clsUser()
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = true;
            _Mode = enMode.AddNew;
        }
         
        public clsUser(
             int UserID, int PersonID,
            string UserName, string Password,
            bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = isActive;

            _Mode = enMode.Update;
        }

        // ###################   CURD Methods   ###################
        public static clsUser FindByUsernameAndPassword
            (string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;

            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword
                                (UserName, Password, 
                                ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindByUserID (int UserID)
        {
            string UserName = "", Password = "";
            int PersonID = -1;

            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUserID
                                (ref UserName, ref Password,
                                 UserID, ref PersonID, ref IsActive
                                );

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName,
                this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        private bool _UpdateUser()
        {
            //call DataAccess Layer 

            return clsUserData.UpdateUser(this.UserID, this.UserName,
                this.Password, this.IsActive);
        }
        // 
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }



        // ###################   Exist Methods   ###################

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
    }
}
