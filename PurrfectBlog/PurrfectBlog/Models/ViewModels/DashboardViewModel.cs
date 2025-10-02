using System.Collections.Generic;
using System.Linq;

namespace PurrfectBlog.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Post> UserPosts { get; set; }
        public int TotalPosts { get; set; }
        public int TotalCommentsReceived { get; set; }
        public string UserName { get; set; }

        public DashboardViewModel()
        {
            UserPosts = new List<Post>();
        }
    }
}
