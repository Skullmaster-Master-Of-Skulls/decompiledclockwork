using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Management;
using System.Web.Resources;
using System.Web.Security;

namespace System.Web.ApplicationServices
{
	// Token: 0x0200011D RID: 285
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[ServiceContract(Namespace = "http://asp.net/ApplicationServices/v200")]
	[ServiceBehavior(Namespace = "http://asp.net/ApplicationServices/v200", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class AuthenticationService
	{
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000EE8 RID: 3816 RVA: 0x00035E64 File Offset: 0x00034064
		// (remove) Token: 0x06000EE9 RID: 3817 RVA: 0x00035EB4 File Offset: 0x000340B4
		public static event EventHandler<AuthenticatingEventArgs> Authenticating
		{
			add
			{
				object authenticatingEventHandlerLock = AuthenticationService._authenticatingEventHandlerLock;
				lock (authenticatingEventHandlerLock)
				{
					AuthenticationService._authenticating = (EventHandler<AuthenticatingEventArgs>)Delegate.Combine(AuthenticationService._authenticating, value);
				}
			}
			remove
			{
				object authenticatingEventHandlerLock = AuthenticationService._authenticatingEventHandlerLock;
				lock (authenticatingEventHandlerLock)
				{
					AuthenticationService._authenticating = (EventHandler<AuthenticatingEventArgs>)Delegate.Remove(AuthenticationService._authenticating, value);
				}
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000EEA RID: 3818 RVA: 0x00035F04 File Offset: 0x00034104
		// (remove) Token: 0x06000EEB RID: 3819 RVA: 0x00035F54 File Offset: 0x00034154
		public static event EventHandler<CreatingCookieEventArgs> CreatingCookie
		{
			add
			{
				object creatingCookieEventHandlerLock = AuthenticationService._creatingCookieEventHandlerLock;
				lock (creatingCookieEventHandlerLock)
				{
					AuthenticationService._creatingCookie = (EventHandler<CreatingCookieEventArgs>)Delegate.Combine(AuthenticationService._creatingCookie, value);
				}
			}
			remove
			{
				object creatingCookieEventHandlerLock = AuthenticationService._creatingCookieEventHandlerLock;
				lock (creatingCookieEventHandlerLock)
				{
					AuthenticationService._creatingCookie = (EventHandler<CreatingCookieEventArgs>)Delegate.Remove(AuthenticationService._creatingCookie, value);
				}
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00035FA4 File Offset: 0x000341A4
		private void OnAuthenticating(AuthenticatingEventArgs e)
		{
			EventHandler<AuthenticatingEventArgs> authenticating = AuthenticationService._authenticating;
			if (authenticating != null)
			{
				authenticating(this, e);
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00035FC4 File Offset: 0x000341C4
		private void OnCreatingCookie(CreatingCookieEventArgs e)
		{
			EventHandler<CreatingCookieEventArgs> creatingCookie = AuthenticationService._creatingCookie;
			if (creatingCookie != null)
			{
				creatingCookie(this, e);
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00035FE2 File Offset: 0x000341E2
		[OperationContract]
		public bool ValidateUser(string username, string password, string customCredential)
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, true);
			return this.LoginInternal(username, password, customCredential, false, false);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00035FFA File Offset: 0x000341FA
		[OperationContract]
		public bool Login(string username, string password, string customCredential, bool isPersistent)
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, true);
			return this.LoginInternal(username, password, customCredential, isPersistent, true);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00036013 File Offset: 0x00034213
		[OperationContract]
		public bool IsLoggedIn()
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, true);
			return HttpContext.Current.User.Identity.IsAuthenticated;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00029BA9 File Offset: 0x00027DA9
		[OperationContract]
		public void Logout()
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, false);
			FormsAuthentication.SignOut();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00036034 File Offset: 0x00034234
		private bool LoginInternal(string username, string password, string customCredential, bool isPersistent, bool setCookie)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			AuthenticatingEventArgs authenticatingEventArgs = new AuthenticatingEventArgs(username, password, customCredential);
			try
			{
				this.OnAuthenticating(authenticatingEventArgs);
				if (!authenticatingEventArgs.AuthenticationIsComplete)
				{
					AuthenticationService.MembershipValidate(authenticatingEventArgs);
				}
				if (!authenticatingEventArgs.Authenticated)
				{
					this.Logout();
				}
				if (authenticatingEventArgs.Authenticated && setCookie)
				{
					CreatingCookieEventArgs creatingCookieEventArgs = new CreatingCookieEventArgs(username, password, isPersistent, customCredential);
					this.OnCreatingCookie(creatingCookieEventArgs);
					if (!creatingCookieEventArgs.CookieIsSet)
					{
						AuthenticationService.SetCookie(username, isPersistent);
					}
				}
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return authenticatingEventArgs.Authenticated;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000360D8 File Offset: 0x000342D8
		private static void MembershipValidate(AuthenticatingEventArgs e)
		{
			e.Authenticated = Membership.ValidateUser(e.UserName, e.Password);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000360F1 File Offset: 0x000342F1
		private static void SetCookie(string username, bool isPersistent)
		{
			FormsAuthentication.SetAuthCookie(username, isPersistent);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000360FC File Offset: 0x000342FC
		private void LogException(Exception e)
		{
			WebServiceErrorEvent webServiceErrorEvent = new WebServiceErrorEvent(AtlasWeb.UnhandledExceptionEventLogMessage, this, e);
			webServiceErrorEvent.Raise();
		}

		// Token: 0x04000435 RID: 1077
		private static object _authenticatingEventHandlerLock = new object();

		// Token: 0x04000436 RID: 1078
		private static EventHandler<AuthenticatingEventArgs> _authenticating;

		// Token: 0x04000437 RID: 1079
		private static object _creatingCookieEventHandlerLock = new object();

		// Token: 0x04000438 RID: 1080
		private static EventHandler<CreatingCookieEventArgs> _creatingCookie;
	}
}
