using System.Collections.Generic;
using System.Linq;
using Educative.Core;

namespace Educative.Infrastructure.QueryExtension
{
    public static class CourseExtension
    {
        public static IQueryable<Course> SortCourse(this IQueryable<Course> query, string orderBy)
        {

            if(string.IsNullOrEmpty(orderBy)){
                return query.OrderBy(c => c.CourseName);
            }
           

            query = orderBy switch
            {
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(c => c.CourseName)
            };

            return query;
        }

        public static IQueryable<Course> SearchCourses(this IQueryable<Course> query, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm)){
                return query;
            }

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(c => c.CourseName.ToLower().Contains(lowerCaseSearchTerm));
        } 

        public static IQueryable<Course> FilterCourse(this IQueryable<Course> query, string courseName, string courseTopic)
        {
            var courseNameList = new List<string>();
            var courseTopicList = new List<string>();
            
            if(!string.IsNullOrEmpty(courseName))
                courseNameList.AddRange(courseName.ToLower().Split(",").ToList());

            if(!string.IsNullOrEmpty(courseTopic))
                courseTopicList.AddRange(courseTopic.ToLower().Split(",").ToList());

            query = query.Where(c => courseNameList.Count == 0 || courseNameList.Contains(c.CourseName.ToLower()));

            query = query.Where(c => courseTopicList.Count == 0 || courseTopicList.Contains(c.CourseTopic.ToLower()));

            return query;

        }
    }
}