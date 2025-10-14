using System.Collections.Generic;
using System.Linq;

namespace PurrfectBlog.Models.ViewModels
{
    /// <summary>
    /// View model for the user dashboard displaying posts and statistics.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// Gets or sets the list of posts created by the user.
        /// </summary>
        public List<Post> UserPosts { get; set; }

        /// <summary>
        /// Gets or sets the total number of posts created by the user.
        /// </summary>
        public int TotalPosts { get; set; }

        /// <summary>
        /// Gets or sets the total number of comments received on the user's posts.
        /// </summary>
        public int TotalCommentsReceived { get; set; }

        /// <summary>
        /// Gets or sets the username of the current user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Initializes a new instance of the DashboardViewModel class.
        /// </summary>
        public DashboardViewModel()
        {
            UserPosts = new List<Post>();
        }
    }
}
