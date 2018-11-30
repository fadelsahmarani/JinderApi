using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JinderWebApp.EF;

namespace JinderWebApp.Model
{
    public class SimpleJobPost
    {
        public int JobPostId { get; set; }
        public int PosterId { get; set; }
        public string JobDescription { get; set; }
        public string RequiredSkills { get; set; }
        public string SalaryRange { get; set; }
        public string OperationHours { get; set; }
        public string Location { get; set; }

        public static SimpleJobPost getSimpleJobPost(JobPost post)
        {
            SimpleJobPost model = new SimpleJobPost();
            model.JobPostId = post.JobPostId;
            model.PosterId = post.PosterId;
            model.JobDescription = post.JobDescription;
            model.RequiredSkills = post.RequiredSkills;
            model.SalaryRange = post.SalaryRange;
            model.OperationHours = post.OperationHours;
            model.Location = post.Location;

            return model;
        }
    }
}