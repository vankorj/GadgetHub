namespace GadgetHub.Domain.Abstract
{
	public interface IAuthProvider
	{
		bool Authenticate(string username, string password);
		void SetAuthCookie(string username);
		void SignOut();
	}
}