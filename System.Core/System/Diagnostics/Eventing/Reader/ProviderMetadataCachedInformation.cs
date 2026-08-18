using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002CB RID: 715
	internal class ProviderMetadataCachedInformation
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x000600DE File Offset: 0x0005E2DE
		public ProviderMetadataCachedInformation(EventLogSession session, string logfile, int maximumCacheSize)
		{
			this.session = session;
			this.logfile = logfile;
			this.cache = new Dictionary<ProviderMetadataCachedInformation.ProviderMetadataId, ProviderMetadataCachedInformation.CacheItem>();
			this.maximumCacheSize = maximumCacheSize;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00060106 File Offset: 0x0005E306
		private bool IsCacheFull()
		{
			return this.cache.Count == this.maximumCacheSize;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006011B File Offset: 0x0005E31B
		private bool IsProviderinCache(ProviderMetadataCachedInformation.ProviderMetadataId key)
		{
			return this.cache.ContainsKey(key);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0006012C File Offset: 0x0005E32C
		private void DeleteCacheEntry(ProviderMetadataCachedInformation.ProviderMetadataId key)
		{
			if (!this.IsProviderinCache(key))
			{
				return;
			}
			ProviderMetadataCachedInformation.CacheItem cacheItem = this.cache[key];
			this.cache.Remove(key);
			cacheItem.ProviderMetadata.Dispose();
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00060168 File Offset: 0x0005E368
		private void AddCacheEntry(ProviderMetadataCachedInformation.ProviderMetadataId key, ProviderMetadata pm)
		{
			if (this.IsCacheFull())
			{
				this.FlushOldestEntry();
			}
			ProviderMetadataCachedInformation.CacheItem value = new ProviderMetadataCachedInformation.CacheItem(pm);
			this.cache.Add(key, value);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00060198 File Offset: 0x0005E398
		private void FlushOldestEntry()
		{
			double num = -10.0;
			DateTime now = DateTime.Now;
			ProviderMetadataCachedInformation.ProviderMetadataId providerMetadataId = null;
			foreach (KeyValuePair<ProviderMetadataCachedInformation.ProviderMetadataId, ProviderMetadataCachedInformation.CacheItem> keyValuePair in this.cache)
			{
				TimeSpan timeSpan = now.Subtract(keyValuePair.Value.TheTime);
				if (timeSpan.TotalMilliseconds >= num)
				{
					num = timeSpan.TotalMilliseconds;
					providerMetadataId = keyValuePair.Key;
				}
			}
			if (providerMetadataId != null)
			{
				this.DeleteCacheEntry(providerMetadataId);
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00060230 File Offset: 0x0005E430
		private static void UpdateCacheValueInfoForHit(ProviderMetadataCachedInformation.CacheItem cacheItem)
		{
			cacheItem.TheTime = DateTime.Now;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00060240 File Offset: 0x0005E440
		private ProviderMetadata GetProviderMetadata(ProviderMetadataCachedInformation.ProviderMetadataId key)
		{
			if (!this.IsProviderinCache(key))
			{
				ProviderMetadata providerMetadata;
				try
				{
					providerMetadata = new ProviderMetadata(key.ProviderName, this.session, key.TheCultureInfo, this.logfile);
				}
				catch (EventLogNotFoundException)
				{
					providerMetadata = new ProviderMetadata(key.ProviderName, this.session, key.TheCultureInfo);
				}
				this.AddCacheEntry(key, providerMetadata);
				return providerMetadata;
			}
			ProviderMetadataCachedInformation.CacheItem cacheItem = this.cache[key];
			ProviderMetadata providerMetadata2 = cacheItem.ProviderMetadata;
			try
			{
				providerMetadata2.CheckReleased();
				ProviderMetadataCachedInformation.UpdateCacheValueInfoForHit(cacheItem);
			}
			catch (EventLogException)
			{
				this.DeleteCacheEntry(key);
				try
				{
					providerMetadata2 = new ProviderMetadata(key.ProviderName, this.session, key.TheCultureInfo, this.logfile);
				}
				catch (EventLogNotFoundException)
				{
					providerMetadata2 = new ProviderMetadata(key.ProviderName, this.session, key.TheCultureInfo);
				}
				this.AddCacheEntry(key, providerMetadata2);
			}
			return providerMetadata2;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00060334 File Offset: 0x0005E534
		[SecuritySafeCritical]
		public string GetFormatDescription(string ProviderName, EventLogHandle eventHandle)
		{
			string result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				try
				{
					ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
					result = NativeWrapper.EvtFormatMessageRenderName(providerMetadata.Handle, eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageEvent);
				}
				catch (EventLogNotFoundException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000603A0 File Offset: 0x0005E5A0
		public string GetFormatDescription(string ProviderName, EventLogHandle eventHandle, string[] values)
		{
			string result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
				try
				{
					result = NativeWrapper.EvtFormatMessageFormatDescription(providerMetadata.Handle, eventHandle, values);
				}
				catch (EventLogNotFoundException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0006040C File Offset: 0x0005E60C
		[SecuritySafeCritical]
		public string GetLevelDisplayName(string ProviderName, EventLogHandle eventHandle)
		{
			string result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
				result = NativeWrapper.EvtFormatMessageRenderName(providerMetadata.Handle, eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageLevel);
			}
			return result;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00060468 File Offset: 0x0005E668
		[SecuritySafeCritical]
		public string GetOpcodeDisplayName(string ProviderName, EventLogHandle eventHandle)
		{
			string result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
				result = NativeWrapper.EvtFormatMessageRenderName(providerMetadata.Handle, eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageOpcode);
			}
			return result;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000604C4 File Offset: 0x0005E6C4
		[SecuritySafeCritical]
		public string GetTaskDisplayName(string ProviderName, EventLogHandle eventHandle)
		{
			string result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
				result = NativeWrapper.EvtFormatMessageRenderName(providerMetadata.Handle, eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageTask);
			}
			return result;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00060520 File Offset: 0x0005E720
		[SecuritySafeCritical]
		public IEnumerable<string> GetKeywordDisplayNames(string ProviderName, EventLogHandle eventHandle)
		{
			IEnumerable<string> result;
			lock (this)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId key = new ProviderMetadataCachedInformation.ProviderMetadataId(ProviderName, CultureInfo.CurrentCulture);
				ProviderMetadata providerMetadata = this.GetProviderMetadata(key);
				result = NativeWrapper.EvtFormatMessageRenderKeywords(providerMetadata.Handle, eventHandle, UnsafeNativeMethods.EvtFormatMessageFlags.EvtFormatMessageKeyword);
			}
			return result;
		}

		// Token: 0x04000CB1 RID: 3249
		private Dictionary<ProviderMetadataCachedInformation.ProviderMetadataId, ProviderMetadataCachedInformation.CacheItem> cache;

		// Token: 0x04000CB2 RID: 3250
		private int maximumCacheSize;

		// Token: 0x04000CB3 RID: 3251
		private EventLogSession session;

		// Token: 0x04000CB4 RID: 3252
		private string logfile;

		// Token: 0x0200046B RID: 1131
		private class ProviderMetadataId
		{
			// Token: 0x06002014 RID: 8212 RVA: 0x000701B0 File Offset: 0x0006E3B0
			public ProviderMetadataId(string providerName, CultureInfo cultureInfo)
			{
				this.providerName = providerName;
				this.cultureInfo = cultureInfo;
			}

			// Token: 0x06002015 RID: 8213 RVA: 0x000701C8 File Offset: 0x0006E3C8
			public override bool Equals(object obj)
			{
				ProviderMetadataCachedInformation.ProviderMetadataId providerMetadataId = obj as ProviderMetadataCachedInformation.ProviderMetadataId;
				return providerMetadataId != null && (this.providerName.Equals(providerMetadataId.providerName) && this.cultureInfo == providerMetadataId.cultureInfo);
			}

			// Token: 0x06002016 RID: 8214 RVA: 0x00070205 File Offset: 0x0006E405
			public override int GetHashCode()
			{
				return this.providerName.GetHashCode() ^ this.cultureInfo.GetHashCode();
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06002017 RID: 8215 RVA: 0x0007021E File Offset: 0x0006E41E
			public string ProviderName
			{
				get
				{
					return this.providerName;
				}
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06002018 RID: 8216 RVA: 0x00070226 File Offset: 0x0006E426
			public CultureInfo TheCultureInfo
			{
				get
				{
					return this.cultureInfo;
				}
			}

			// Token: 0x0400134D RID: 4941
			private string providerName;

			// Token: 0x0400134E RID: 4942
			private CultureInfo cultureInfo;
		}

		// Token: 0x0200046C RID: 1132
		private class CacheItem
		{
			// Token: 0x06002019 RID: 8217 RVA: 0x0007022E File Offset: 0x0006E42E
			public CacheItem(ProviderMetadata pm)
			{
				this.pm = pm;
				this.theTime = DateTime.Now;
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x0600201A RID: 8218 RVA: 0x00070248 File Offset: 0x0006E448
			// (set) Token: 0x0600201B RID: 8219 RVA: 0x00070250 File Offset: 0x0006E450
			public DateTime TheTime
			{
				get
				{
					return this.theTime;
				}
				set
				{
					this.theTime = value;
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x0600201C RID: 8220 RVA: 0x00070259 File Offset: 0x0006E459
			public ProviderMetadata ProviderMetadata
			{
				get
				{
					return this.pm;
				}
			}

			// Token: 0x0400134F RID: 4943
			private ProviderMetadata pm;

			// Token: 0x04001350 RID: 4944
			private DateTime theTime;
		}
	}
}
