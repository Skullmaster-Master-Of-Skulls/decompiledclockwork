using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002FA RID: 762
	internal sealed class SecurityContextTokenCache : TimeBoundedCache
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x00060C1F File Offset: 0x0005EE1F
		public SecurityContextTokenCache(int capacity, bool replaceOldestEntries) : this(capacity, replaceOldestEntries, SecurityProtocolFactory.defaultMaxClockSkew)
		{
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00060C2E File Offset: 0x0005EE2E
		public SecurityContextTokenCache(int capacity, bool replaceOldestEntries, TimeSpan clockSkew) : base(SecurityContextTokenCache.lowWaterMark, capacity, null, PurgingMode.TimerBasedPurge, SecurityContextTokenCache.purgingInterval, true)
		{
			this.replaceOldestEntries = replaceOldestEntries;
			this.clockSkew = clockSkew;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00060C59 File Offset: 0x0005EE59
		public void AddContext(SecurityContextSecurityToken token)
		{
			this.TryAddContext(token, true);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00060C64 File Offset: 0x0005EE64
		public bool TryAddContext(SecurityContextSecurityToken token)
		{
			return this.TryAddContext(token, false);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00060C70 File Offset: 0x0005EE70
		private bool TryAddContext(SecurityContextSecurityToken token, bool throwOnFailure)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (!SecurityUtils.IsCurrentlyTimeEffective(token.ValidFrom, token.ValidTo, this.clockSkew))
			{
				if (token.KeyGeneration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SecurityContextExpiredNoKeyGeneration", new object[]
					{
						token.ContextId
					}));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SecurityContextExpired", new object[]
				{
					token.ContextId,
					token.KeyGeneration.ToString()
				}));
			}
			else if (!SecurityUtils.IsCurrentlyTimeEffective(token.KeyEffectiveTime, token.KeyExpirationTime, this.clockSkew))
			{
				if (token.KeyGeneration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SecurityContextKeyExpiredNoKeyGeneration", new object[]
					{
						token.ContextId
					}));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SecurityContextKeyExpired", new object[]
				{
					token.ContextId,
					token.KeyGeneration.ToString()
				}));
			}
			else
			{
				object hashKey = this.GetHashKey(token.ContextId, token.KeyGeneration);
				bool flag = base.TryAddItem(hashKey, token.Clone(), false);
				if (flag || !throwOnFailure)
				{
					return flag;
				}
				if (token.KeyGeneration == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextAlreadyRegisteredNoKeyGeneration", new object[]
					{
						token.ContextId
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextAlreadyRegistered", new object[]
				{
					token.ContextId,
					token.KeyGeneration.ToString()
				})));
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00060E21 File Offset: 0x0005F021
		private object GetHashKey(UniqueId contextId, UniqueId generation)
		{
			if (generation == null)
			{
				return contextId;
			}
			return new SecurityContextTokenCache.ContextAndGenerationKey(contextId, generation);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00060E3A File Offset: 0x0005F03A
		public void ClearContexts()
		{
			base.ClearItems();
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00060E44 File Offset: 0x0005F044
		public SecurityContextSecurityToken GetContext(UniqueId contextId, UniqueId generation)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			object hashKey = this.GetHashKey(contextId, generation);
			SecurityContextSecurityToken securityContextSecurityToken = (SecurityContextSecurityToken)base.GetItem(hashKey);
			if (securityContextSecurityToken == null)
			{
				return null;
			}
			return securityContextSecurityToken.Clone();
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00060E8C File Offset: 0x0005F08C
		public void RemoveContext(UniqueId contextId, UniqueId generation, bool throwIfNotPresent)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			object hashKey = this.GetHashKey(contextId, generation);
			if (base.TryRemoveItem(hashKey) || !throwIfNotPresent)
			{
				return;
			}
			if (generation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextNotPresentNoKeyGeneration", new object[]
				{
					contextId
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextNotPresent", new object[]
			{
				contextId,
				generation.ToString()
			})));
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00060F24 File Offset: 0x0005F124
		private ArrayList GetMatchingKeys(UniqueId contextId)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			ArrayList arrayList = new ArrayList(2);
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					base.CacheLock.AcquireReaderLock(-1);
					flag = true;
				}
				foreach (object obj in base.Entries.Keys)
				{
					bool flag2;
					if (obj is UniqueId)
					{
						flag2 = ((UniqueId)obj == contextId);
					}
					else
					{
						flag2 = (((SecurityContextTokenCache.ContextAndGenerationKey)obj).ContextId == contextId);
					}
					if (flag2)
					{
						arrayList.Add(obj);
					}
				}
			}
			finally
			{
				if (flag)
				{
					base.CacheLock.ReleaseReaderLock();
				}
			}
			return arrayList;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00061014 File Offset: 0x0005F214
		public void RemoveAllContexts(UniqueId contextId)
		{
			ArrayList matchingKeys = this.GetMatchingKeys(contextId);
			for (int i = 0; i < matchingKeys.Count; i++)
			{
				base.TryRemoveItem(matchingKeys[i]);
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00061048 File Offset: 0x0005F248
		public void UpdateContextCachingTime(SecurityContextSecurityToken token, DateTime expirationTime)
		{
			if (token.ValidTo <= expirationTime.ToUniversalTime())
			{
				return;
			}
			base.TryReplaceItem(this.GetHashKey(token.ContextId, token.KeyGeneration), token, expirationTime);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0006107C File Offset: 0x0005F27C
		public Collection<SecurityContextSecurityToken> GetAllContexts(UniqueId contextId)
		{
			ArrayList matchingKeys = this.GetMatchingKeys(contextId);
			Collection<SecurityContextSecurityToken> collection = new Collection<SecurityContextSecurityToken>();
			for (int i = 0; i < matchingKeys.Count; i++)
			{
				SecurityContextSecurityToken securityContextSecurityToken = base.GetItem(matchingKeys[i]) as SecurityContextSecurityToken;
				if (securityContextSecurityToken != null)
				{
					collection.Add(securityContextSecurityToken);
				}
			}
			return collection;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000610C8 File Offset: 0x0005F2C8
		protected override ArrayList OnQuotaReached(Hashtable cacheTable)
		{
			if (!this.replaceOldestEntries)
			{
				SecurityTraceRecordHelper.TraceSecurityContextTokenCacheFull(base.Capacity, 0);
				return base.OnQuotaReached(cacheTable);
			}
			List<SecurityContextSecurityToken> list = new List<SecurityContextSecurityToken>(cacheTable.Count);
			foreach (object obj in cacheTable.Values)
			{
				TimeBoundedCache.IExpirableItem val = (TimeBoundedCache.IExpirableItem)obj;
				SecurityContextSecurityToken item = (SecurityContextSecurityToken)base.ExtractItem(val);
				list.Add(item);
			}
			list.Sort(SecurityContextTokenCache.sctEffectiveTimeComparer);
			int num = (int)((double)base.Capacity * SecurityContextTokenCache.pruningFactor);
			num = ((num <= 0) ? base.Capacity : num);
			ArrayList arrayList = new ArrayList(num);
			for (int i = 0; i < num; i++)
			{
				arrayList.Add(this.GetHashKey(list[i].ContextId, list[i].KeyGeneration));
				this.OnRemove(list[i]);
			}
			SecurityTraceRecordHelper.TraceSecurityContextTokenCacheFull(base.Capacity, num);
			return arrayList;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000611E0 File Offset: 0x0005F3E0
		protected override void OnRemove(object item)
		{
			((IDisposable)item).Dispose();
			base.OnRemove(item);
		}

		// Token: 0x04001CD9 RID: 7385
		private static int lowWaterMark = 50;

		// Token: 0x04001CDA RID: 7386
		private static TimeSpan purgingInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x04001CDB RID: 7387
		private static double pruningFactor = 0.2;

		// Token: 0x04001CDC RID: 7388
		private bool replaceOldestEntries = true;

		// Token: 0x04001CDD RID: 7389
		private static SecurityContextTokenCache.SctEffectiveTimeComparer sctEffectiveTimeComparer = new SecurityContextTokenCache.SctEffectiveTimeComparer();

		// Token: 0x04001CDE RID: 7390
		private TimeSpan clockSkew;

		// Token: 0x02000B64 RID: 2916
		private sealed class SctEffectiveTimeComparer : IComparer<SecurityContextSecurityToken>
		{
			// Token: 0x06007242 RID: 29250 RVA: 0x001AA9B8 File Offset: 0x001A8BB8
			public int Compare(SecurityContextSecurityToken sct1, SecurityContextSecurityToken sct2)
			{
				if (sct1 == sct2)
				{
					return 0;
				}
				if (sct1.ValidFrom.ToUniversalTime() < sct2.ValidFrom.ToUniversalTime())
				{
					return -1;
				}
				if (sct1.ValidFrom.ToUniversalTime() > sct2.ValidFrom.ToUniversalTime())
				{
					return 1;
				}
				if (sct1.KeyEffectiveTime.ToUniversalTime() < sct2.KeyEffectiveTime.ToUniversalTime())
				{
					return -1;
				}
				if (sct1.KeyEffectiveTime.ToUniversalTime() > sct2.KeyEffectiveTime.ToUniversalTime())
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000B65 RID: 2917
		private struct ContextAndGenerationKey
		{
			// Token: 0x06007244 RID: 29252 RVA: 0x001AAA68 File Offset: 0x001A8C68
			public ContextAndGenerationKey(UniqueId contextId, UniqueId generation)
			{
				this.contextId = contextId;
				this.generation = generation;
			}

			// Token: 0x17001A83 RID: 6787
			// (get) Token: 0x06007245 RID: 29253 RVA: 0x001AAA78 File Offset: 0x001A8C78
			public UniqueId ContextId
			{
				get
				{
					return this.contextId;
				}
			}

			// Token: 0x17001A84 RID: 6788
			// (get) Token: 0x06007246 RID: 29254 RVA: 0x001AAA80 File Offset: 0x001A8C80
			public UniqueId Generation
			{
				get
				{
					return this.generation;
				}
			}

			// Token: 0x06007247 RID: 29255 RVA: 0x001AAA88 File Offset: 0x001A8C88
			public override int GetHashCode()
			{
				return this.contextId.GetHashCode() ^ this.generation.GetHashCode();
			}

			// Token: 0x06007248 RID: 29256 RVA: 0x001AAAA4 File Offset: 0x001A8CA4
			public override bool Equals(object obj)
			{
				if (obj is SecurityContextTokenCache.ContextAndGenerationKey)
				{
					SecurityContextTokenCache.ContextAndGenerationKey contextAndGenerationKey = (SecurityContextTokenCache.ContextAndGenerationKey)obj;
					return contextAndGenerationKey.ContextId == this.contextId && contextAndGenerationKey.Generation == this.generation;
				}
				return false;
			}

			// Token: 0x06007249 RID: 29257 RVA: 0x001AAAEA File Offset: 0x001A8CEA
			public static bool operator ==(SecurityContextTokenCache.ContextAndGenerationKey a, SecurityContextTokenCache.ContextAndGenerationKey b)
			{
				if (a == null)
				{
					return b == null;
				}
				return a.Equals(b);
			}

			// Token: 0x0600724A RID: 29258 RVA: 0x001AAB11 File Offset: 0x001A8D11
			public static bool operator !=(SecurityContextTokenCache.ContextAndGenerationKey a, SecurityContextTokenCache.ContextAndGenerationKey b)
			{
				return !(a == b);
			}

			// Token: 0x040040A9 RID: 16553
			private UniqueId contextId;

			// Token: 0x040040AA RID: 16554
			private UniqueId generation;
		}
	}
}
