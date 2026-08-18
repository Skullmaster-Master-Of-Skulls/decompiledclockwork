using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000011 RID: 17
	public class SignInManager<TUser, TKey> : IDisposable where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004063 File Offset: 0x00002263
		public SignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager)
		{
			if (userManager == null)
			{
				throw new ArgumentNullException("userManager");
			}
			if (authenticationManager == null)
			{
				throw new ArgumentNullException("authenticationManager");
			}
			this.UserManager = userManager;
			this.AuthenticationManager = authenticationManager;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004095 File Offset: 0x00002295
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000040A6 File Offset: 0x000022A6
		public string AuthenticationType
		{
			get
			{
				return this._authType ?? "ApplicationCookie";
			}
			set
			{
				this._authType = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000040AF File Offset: 0x000022AF
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000040B7 File Offset: 0x000022B7
		public UserManager<TUser, TKey> UserManager { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000040C0 File Offset: 0x000022C0
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000040C8 File Offset: 0x000022C8
		public IAuthenticationManager AuthenticationManager { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x000040D1 File Offset: 0x000022D1
		public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(TUser user)
		{
			return this.UserManager.CreateIdentityAsync(user, this.AuthenticationType);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000040E5 File Offset: 0x000022E5
		public virtual string ConvertIdToString(TKey id)
		{
			return Convert.ToString(id, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000040F8 File Offset: 0x000022F8
		public virtual TKey ConvertIdFromString(string id)
		{
			if (id == null)
			{
				return default(TKey);
			}
			return (TKey)((object)Convert.ChangeType(id, typeof(TKey), CultureInfo.InvariantCulture));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004300 File Offset: 0x00002500
		public virtual async Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
		{
			ClaimsIdentity userIdentity = await this.CreateUserIdentityAsync(user).WithCurrentCulture<ClaimsIdentity>();
			this.AuthenticationManager.SignOut(new string[]
			{
				"ExternalCookie",
				"TwoFactorCookie"
			});
			if (rememberBrowser)
			{
				ClaimsIdentity claimsIdentity = this.AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(this.ConvertIdToString(user.Id));
				this.AuthenticationManager.SignIn(new AuthenticationProperties
				{
					IsPersistent = isPersistent
				}, new ClaimsIdentity[]
				{
					userIdentity,
					claimsIdentity
				});
			}
			else
			{
				this.AuthenticationManager.SignIn(new AuthenticationProperties
				{
					IsPersistent = isPersistent
				}, new ClaimsIdentity[]
				{
					userIdentity
				});
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004594 File Offset: 0x00002794
		public virtual async Task<bool> SendTwoFactorCodeAsync(string provider)
		{
			TKey userId = await this.GetVerifiedUserIdAsync().WithCurrentCulture<TKey>();
			bool result;
			if (userId == null)
			{
				result = false;
			}
			else
			{
				string token = await this.UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture<string>();
				await this.UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture<IdentityResult>();
				result = true;
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000472C File Offset: 0x0000292C
		public async Task<TKey> GetVerifiedUserIdAsync()
		{
			AuthenticateResult result = await this.AuthenticationManager.AuthenticateAsync("TwoFactorCookie").WithCurrentCulture<AuthenticateResult>();
			TKey result2;
			if (result != null && result.Identity != null && !string.IsNullOrEmpty(result.Identity.GetUserId()))
			{
				result2 = this.ConvertIdFromString(result.Identity.GetUserId());
			}
			else
			{
				result2 = default(TKey);
			}
			return result2;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000485C File Offset: 0x00002A5C
		public async Task<bool> HasBeenVerifiedAsync()
		{
			return await this.GetVerifiedUserIdAsync().WithCurrentCulture<TKey>() != null;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004D44 File Offset: 0x00002F44
		public virtual async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
		{
			TKey userId = await this.GetVerifiedUserIdAsync().WithCurrentCulture<TKey>();
			SignInStatus result;
			if (userId == null)
			{
				result = SignInStatus.Failure;
			}
			else
			{
				TUser user = await this.UserManager.FindByIdAsync(userId).WithCurrentCulture<TUser>();
				if (user == null)
				{
					result = SignInStatus.Failure;
				}
				else if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>())
				{
					result = SignInStatus.LockedOut;
				}
				else if (await this.UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture<bool>())
				{
					await this.UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture<IdentityResult>();
					await this.SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
					result = SignInStatus.Success;
				}
				else
				{
					await this.UserManager.AccessFailedAsync(user.Id).WithCurrentCulture<IdentityResult>();
					result = SignInStatus.Failure;
				}
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004FE8 File Offset: 0x000031E8
		public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
		{
			TUser user = await this.UserManager.FindAsync(loginInfo.Login).WithCurrentCulture<TUser>();
			SignInStatus result;
			if (user == null)
			{
				result = SignInStatus.Failure;
			}
			else if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>())
			{
				result = SignInStatus.LockedOut;
			}
			else
			{
				result = await this.SignInOrTwoFactor(user, isPersistent).WithCurrentCulture<SignInStatus>();
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000535C File Offset: 0x0000355C
		private async Task<SignInStatus> SignInOrTwoFactor(TUser user, bool isPersistent)
		{
			string id = Convert.ToString(user.Id);
			SignInStatus result;
			if (await this.UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture<bool>() && (await this.UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture<IList<string>>()).Count > 0 && !(await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture<bool>()))
			{
				ClaimsIdentity claimsIdentity = new ClaimsIdentity("TwoFactorCookie");
				claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", id));
				this.AuthenticationManager.SignIn(new ClaimsIdentity[]
				{
					claimsIdentity
				});
				result = SignInStatus.RequiresVerification;
			}
			else
			{
				await this.SignInAsync(user, isPersistent, false).WithCurrentCulture();
				result = SignInStatus.Success;
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005858 File Offset: 0x00003A58
		public virtual async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
		{
			SignInStatus result;
			if (this.UserManager == null)
			{
				result = SignInStatus.Failure;
			}
			else
			{
				TUser user = await this.UserManager.FindByNameAsync(userName).WithCurrentCulture<TUser>();
				if (user == null)
				{
					result = SignInStatus.Failure;
				}
				else if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>())
				{
					result = SignInStatus.LockedOut;
				}
				else if (await this.UserManager.CheckPasswordAsync(user, password).WithCurrentCulture<bool>())
				{
					await this.UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture<IdentityResult>();
					result = await this.SignInOrTwoFactor(user, isPersistent).WithCurrentCulture<SignInStatus>();
				}
				else
				{
					if (shouldLockout)
					{
						await this.UserManager.AccessFailedAsync(user.Id).WithCurrentCulture<IdentityResult>();
						if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture<bool>())
						{
							return SignInStatus.LockedOut;
						}
					}
					result = SignInStatus.Failure;
				}
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000058BF File Offset: 0x00003ABF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000058CE File Offset: 0x00003ACE
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04000014 RID: 20
		private string _authType;
	}
}
