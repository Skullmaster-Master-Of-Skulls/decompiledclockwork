using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000380 RID: 896
	internal class WindowsUserNameCachingSecurityTokenAuthenticator : WindowsUserNameSecurityTokenAuthenticator, ILogonTokenCacheManager, IDisposable
	{
		// Token: 0x0600212D RID: 8493 RVA: 0x0007B0D8 File Offset: 0x000792D8
		public WindowsUserNameCachingSecurityTokenAuthenticator(bool includeWindowsGroups, int maxCachedLogonTokens, TimeSpan cachedLogonTokenLifetime) : base(includeWindowsGroups)
		{
			this.logonTokenCache = new LogonTokenCache(maxCachedLogonTokens, cachedLogonTokenLifetime);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0007B0EE File Offset: 0x000792EE
		public void Dispose()
		{
			this.FlushLogonTokenCache();
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0007B0F8 File Offset: 0x000792F8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password)
		{
			LogonToken logonToken;
			if (this.logonTokenCache.TryGetTokenCache(userName, out logonToken))
			{
				if (logonToken.PasswordEquals(password))
				{
					return logonToken.GetAuthorizationPolicies();
				}
				this.logonTokenCache.TryRemoveTokenCache(userName);
			}
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = base.ValidateUserNamePasswordCore(userName, password);
			this.logonTokenCache.TryAddTokenCache(userName, password, readOnlyCollection);
			return readOnlyCollection;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0007B14B File Offset: 0x0007934B
		public bool RemoveCachedLogonToken(string username)
		{
			return this.logonTokenCache != null && this.logonTokenCache.TryRemoveTokenCache(username);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0007B163 File Offset: 0x00079363
		public void FlushLogonTokenCache()
		{
			if (this.logonTokenCache != null)
			{
				this.logonTokenCache.Flush();
			}
		}

		// Token: 0x04001F38 RID: 7992
		private LogonTokenCache logonTokenCache;
	}
}
