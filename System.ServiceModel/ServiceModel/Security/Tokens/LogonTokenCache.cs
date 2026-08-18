using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Cryptography;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037E RID: 894
	internal class LogonTokenCache : TimeBoundedCache
	{
		// Token: 0x06002120 RID: 8480 RVA: 0x0007AE88 File Offset: 0x00079088
		public LogonTokenCache(int maxCachedLogonTokens, TimeSpan cachedLogonTokenLifetime) : base(maxCachedLogonTokens * 75 / 100, maxCachedLogonTokens, StringComparer.OrdinalIgnoreCase, PurgingMode.TimerBasedPurge, TimeSpan.FromTicks(cachedLogonTokenLifetime.Ticks >> 2), true)
		{
			this.cachedLogonTokenLifetime = cachedLogonTokenLifetime;
			this.random = new RNGCryptoServiceProvider();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0007AEBF File Offset: 0x000790BF
		public bool TryGetTokenCache(string userName, out LogonToken token)
		{
			token = (LogonToken)base.GetItem(userName);
			return token != null;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0007AED4 File Offset: 0x000790D4
		public bool TryAddTokenCache(string userName, string password, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			byte[] array = new byte[256];
			this.random.GetBytes(array);
			LogonToken item = new LogonToken(userName, password, array, authorizationPolicies);
			DateTime expirationTime = DateTime.UtcNow.Add(this.cachedLogonTokenLifetime);
			return base.TryAddItem(userName, item, expirationTime, true);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0007AF20 File Offset: 0x00079120
		protected override ArrayList OnQuotaReached(Hashtable cacheTable)
		{
			List<TimeBoundedCache.IExpirableItem> list = new List<TimeBoundedCache.IExpirableItem>(cacheTable.Count);
			foreach (object obj in cacheTable.Values)
			{
				TimeBoundedCache.IExpirableItem item = (TimeBoundedCache.IExpirableItem)obj;
				list.Add(item);
			}
			list.Sort(TimeBoundedCache.ExpirableItemComparer.Default);
			int num = list.Count * 25 / 100;
			num = ((num <= 0) ? list.Count : num);
			ArrayList arrayList = new ArrayList(num);
			for (int i = 0; i < num; i++)
			{
				LogonToken logonToken = (LogonToken)base.ExtractItem(list[i]);
				arrayList.Add(logonToken.UserName);
				this.OnRemove(logonToken);
			}
			return arrayList;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0007AFF4 File Offset: 0x000791F4
		public bool TryRemoveTokenCache(string userName)
		{
			return base.TryRemoveItem(userName);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0007AFFD File Offset: 0x000791FD
		public void Flush()
		{
			base.ClearItems();
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0007B005 File Offset: 0x00079205
		protected override void OnRemove(object item)
		{
			((LogonToken)item).Dispose();
			base.OnRemove(item);
		}

		// Token: 0x04001F30 RID: 7984
		private const int lowWaterMarkFactor = 75;

		// Token: 0x04001F31 RID: 7985
		private const int saltSize = 256;

		// Token: 0x04001F32 RID: 7986
		private TimeSpan cachedLogonTokenLifetime;

		// Token: 0x04001F33 RID: 7987
		private RNGCryptoServiceProvider random;
	}
}
