using System;
using System.Security.Claims;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000002 RID: 2
	public static class SignInManagerExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public static ClaimsIdentity CreateUserIdentity<TUser, TKey>(this SignInManager<TUser, TKey> manager, TUser user) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<ClaimsIdentity>(() => manager.CreateUserIdentityAsync(user));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002158 File Offset: 0x00000358
		public static void SignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, TUser user, bool isPersistent, bool rememberBrowser) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AsyncHelper.RunSync(() => manager.SignInAsync(user, isPersistent, rememberBrowser));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021C8 File Offset: 0x000003C8
		public static bool SendTwoFactorCode<TUser, TKey>(this SignInManager<TUser, TKey> manager, string provider) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.SendTwoFactorCodeAsync(provider));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002224 File Offset: 0x00000424
		public static TKey GetVerifiedUserId<TUser, TKey>(this SignInManager<TUser, TKey> manager) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TKey>(() => manager.GetVerifiedUserIdAsync());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002278 File Offset: 0x00000478
		public static bool HasBeenVerified<TUser, TKey>(this SignInManager<TUser, TKey> manager) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.HasBeenVerifiedAsync());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022E4 File Offset: 0x000004E4
		public static SignInStatus TwoFactorSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, string provider, string code, bool isPersistent, bool rememberBrowser) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<SignInStatus>(() => manager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002360 File Offset: 0x00000560
		public static SignInStatus ExternalSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, ExternalLoginInfo loginInfo, bool isPersistent) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<SignInStatus>(() => manager.ExternalSignInAsync(loginInfo, isPersistent));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023DC File Offset: 0x000005DC
		public static SignInStatus PasswordSignIn<TUser, TKey>(this SignInManager<TUser, TKey> manager, string userName, string password, bool isPersistent, bool shouldLockout) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<SignInStatus>(() => manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout));
		}
	}
}
