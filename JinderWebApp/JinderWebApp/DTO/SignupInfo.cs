using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinderWebApp.Model
{
    public class SignupInfo
    {
        /* Properties common to both a job seeker and a job poster */
        
        public string Username { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }

        /* Properties exclusive for a job seeker */

        public string Education { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string Certification { get; set; }

    }
}