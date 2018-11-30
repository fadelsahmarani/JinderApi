using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using JinderWebApp.EF;
using JinderWebApp.Model;

namespace JinderWebApp.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users/signup")]
        public HttpResponseMessage Post([FromBody] SignupInfo info)
        {
            JinderDBConnection dbContext = new JinderDBConnection();
            var usersTable = dbContext.JinderUsers;

            JinderUser jinderUser = (from user in usersTable
                       where user.Username == info.Username
                       select user).FirstOrDefault<JinderUser>();

            HttpResponseMessage message = new HttpResponseMessage();

            if (jinderUser != null)
            {
                message.StatusCode = HttpStatusCode.Conflict;
                message.Content = new StringContent("Username: " + info.Username + " is already registered.");

                return message;
            }

            JinderUser newUser = new JinderUser();

            newUser.Username = info.Username;
            newUser.Password = info.Password;
            newUser.FullName = info.FullName;
            newUser.DateOfBirth = info.DateOfBirth;
            newUser.ProfilePicture = info.ProfilePicture;
            newUser.Gender = info.Gender;
            newUser.Address = info.Address;
            newUser.UserType = info.UserType;

            usersTable.Add(newUser);
            dbContext.SaveChanges();

            if (info.UserType == "seeker")
            {
                SeekerProfile newSeekerProfile = new SeekerProfile();
                newSeekerProfile.JinderUserId = newUser.JinderUserId;
                newSeekerProfile.Education = info.Education;
                newSeekerProfile.Experience = info.Experience;
                newSeekerProfile.Skills = info.Skills;
                newSeekerProfile.Certification = info.Certification;

                var seekersTable = dbContext.SeekerProfiles;
                seekersTable.Add(newSeekerProfile);

                dbContext.SaveChanges();
            }

            message.StatusCode = HttpStatusCode.Created;
            return message;
        }

        [Route("api/users/seekers/{Username}/NotViewed")]
        public List<SimpleJobPost> Get(String Username, [FromUri] int count)
        {
            JinderDBConnection dbContext = new JinderDBConnection();
            var usersTable = dbContext.JinderUsers;

            JinderUser jinderUser = (from user in usersTable
                                     where user.Username == Username
                                     select user).FirstOrDefault<JinderUser>();
            
            if (jinderUser.UserType != "seeker")
                throw new Exception("JinderUser with username: " + jinderUser.Username + " is not a seeker!");
            
            var viewedJobPostIds = from id in dbContext.SeekerExpressionLogs
                                    where id.SeekerId == jinderUser.JinderUserId
                                    select id.JobPostId;

            var notViewedJobPosts = (from jobPost in dbContext.JobPosts
                                    where !viewedJobPostIds.Contains<int>(jobPost.JobPostId)
                                    select jobPost).Take<JobPost>(count);

            List<SimpleJobPost> simpleJobPosts = new List<SimpleJobPost>();
            foreach (JobPost post in notViewedJobPosts)
            {
                simpleJobPosts.Add(SimpleJobPost.getSimpleJobPost(post));
            }

            return simpleJobPosts;
        }

        [Route("api/users/posters/{Username}/NotViewed")]
        public List<JinderSeeker> Get(String Username, [FromUri] int count, int dummy = 0)
        {
            JinderDBConnection dbContext = new JinderDBConnection();
            var usersTable = dbContext.JinderUsers;

            JinderUser jinderUser = (from user in usersTable
                                     where user.Username == Username
                                     select user).FirstOrDefault<JinderUser>();

            if (jinderUser.UserType != "poster")
                throw new Exception("JinderUser with username: " + jinderUser.Username + " is not a poster!");

            var viewedSeekerIds = from log in dbContext.PosterExpressionLogs
                                  where log.PosterId == jinderUser.JinderUserId
                                  select log.SeekerId;


            var notViewedSeekers = (from seeker in dbContext.JinderUsers
                                   where seeker.UserType == "seeker" &&
                                   !viewedSeekerIds.Contains<int>(seeker.JinderUserId)
                                   select seeker).Take<JinderUser>(count);

            List<JinderSeeker> jinderSeekers = new List<JinderSeeker>();
            foreach (JinderUser seeker in notViewedSeekers)
            {
                jinderSeekers.Add(JinderSeeker.getJinderSeeker(seeker));
            }

            return jinderSeekers;
        }

        [Route("api/users/login")]
        public HttpResponseMessage Post([FromBody] LoginInfo loginInfo)
        {
            JinderDBConnection db = new JinderDBConnection();
            HttpResponseMessage message = new HttpResponseMessage();

            // Try to get user
            JinderUser jinderUser = (from user in db.JinderUsers
                               where user.Username == loginInfo.Username && 
                               user.Password == loginInfo.Password
                               select user).FirstOrDefault();

            if (jinderUser == null)
            {
                message.StatusCode = HttpStatusCode.BadRequest;
                return message;
            }
           

            message.StatusCode = HttpStatusCode.OK;
            return message;
        }

    }
}
