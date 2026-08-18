using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000020 RID: 32
	public class TotpSecurityStampBasedTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000294A File Offset: 0x00000B4A
		public virtual Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002952 File Offset: 0x00000B52
		public virtual Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return Task.FromResult<bool>(manager.SupportsUserSecurityStamp);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B38 File Offset: 0x00000D38
		public virtual async Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
		{
			SecurityToken token = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture<SecurityToken>();
			string modifier = await this.GetUserModifierAsync(purpose, manager, user).WithCurrentCulture<string>();
			return Rfc6238AuthenticationService.GenerateCode(token, modifier).ToString("D6", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D78 File Offset: 0x00000F78
		public virtual async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user)
		{
			int code;
			bool result;
			if (!int.TryParse(token, out code))
			{
				result = false;
			}
			else
			{
				SecurityToken securityToken = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture<SecurityToken>();
				string modifier = await this.GetUserModifierAsync(purpose, manager, user).WithCurrentCulture<string>();
				result = (securityToken != null && Rfc6238AuthenticationService.ValidateCode(securityToken, code, modifier));
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public virtual Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
		{
			return Task.FromResult<string>(string.Concat(new object[]
			{
				"Totp:",
				purpose,
				":",
				user.Id
			}));
		}
	}
}
