namespace PurrfectBlog.Constants
{
    /// <summary>
    /// Contains constant values for error messages used throughout the application.
    /// </summary>
    public static class ErrorMessages
    {
        // Authentication Errors
        /// <summary>
        /// Error message for invalid username.
        /// </summary>
        public const string InvalidUsername = "Username is invalid.";

        /// <summary>
        /// Error message for invalid password.
        /// </summary>
        public const string InvalidPassword = "Password is invalid.";

        /// <summary>
        /// Error message for username already taken.
        /// </summary>
        public const string UsernameAlreadyTaken = "Sorry, this username is pawlready taken!";

        /// <summary>
        /// Error message for author association failure.
        /// </summary>
        public const string AuthorAssociationFailed = "Oops! An error occurred. Unable to associate post with a valid author.";

        // Validation Errors
        /// <summary>
        /// Error message for required username.
        /// </summary>
        public const string UsernameRequired = "Username is required.";

        /// <summary>
        /// Error message for required password.
        /// </summary>
        public const string PasswordRequired = "Password is required.";

        /// <summary>
        /// Error message for password confirmation mismatch.
        /// </summary>
        public const string PasswordMismatch = "The password and confirmation password do not match.";

        // Success Messages
        /// <summary>
        /// Success message for post deletion.
        /// </summary>
        public const string PostDeletedSuccessfully = "Post successfully deleted.";
    }
}
