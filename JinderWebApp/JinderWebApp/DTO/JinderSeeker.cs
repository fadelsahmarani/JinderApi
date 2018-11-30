using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JinderWebApp.EF;

namespace JinderWebApp.Model
{
    public class JinderSeeker
    {
        public int JinderUserId { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public int SeekerProfileId { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string Certification { get; set; }

        public static JinderSeeker getJinderSeeker(JinderUser user)
        {
            if (user.UserType != "seeker")
                throw new Exception("Provided JinderUser: " + user.Username + " is not a seeker!");

            JinderDBConnection dbContext = new JinderDBConnection();
            SeekerProfile seekerProfile = (from profile in dbContext.SeekerProfiles
                                           where profile.JinderUserId == user.JinderUserId
                                           select profile).FirstOrDefault<SeekerProfile>();

            if (seekerProfile == null)
                throw new Exception("Could not find a SeekerProfile for JinderUser: " + user.Username);

            JinderSeeker jinderSeeker = new JinderSeeker();
            jinderSeeker.JinderUserId = user.JinderUserId;
            jinderSeeker.FullName = user.FullName;
            jinderSeeker.DateOfBirth = user.DateOfBirth;
            jinderSeeker.ProfilePicture = user.ProfilePicture;
            jinderSeeker.Gender = user.Gender;
            jinderSeeker.Address = user.Address;
            jinderSeeker.UserType = user.UserType;
            jinderSeeker.Password = user.Password;
            jinderSeeker.Username = user.Username;

            jinderSeeker.SeekerProfileId = seekerProfile.SeekerProfileId;
            jinderSeeker.Education = seekerProfile.Education;
            jinderSeeker.Experience = seekerProfile.Experience;
            jinderSeeker.Skills = seekerProfile.Skills;
            jinderSeeker.Certification = seekerProfile.Certification;

            return jinderSeeker;
        }
    }
}