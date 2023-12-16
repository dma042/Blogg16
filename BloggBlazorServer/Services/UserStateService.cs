namespace BloggBlazorServer.Services
{
    public class UserStateService
    {
        public bool IsLoggedIn { get; private set; }
        public string CurrentUserId { get; private set; }
        public string CurrentUserEmail { get; private set; }
        public string CurrentUserName { get; private set; } 

        public event Action OnChange;

        public void Login(string userId, string userEmail, string userName)
        {
            IsLoggedIn = true;
            CurrentUserId = userId;
            CurrentUserEmail = userEmail;
            CurrentUserName = userName;
            NotifyStateChanged();
            Console.WriteLine("User logged in.");
        }

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUserId = null;
            CurrentUserEmail = null;
            CurrentUserName = null;
            NotifyStateChanged();
            Console.WriteLine("User logged out.");
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
