using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C4 RID: 2500
	internal class UriCache
	{
		// Token: 0x0600623A RID: 25146 RVA: 0x0016DA63 File Offset: 0x0016BC63
		public UriCache()
		{
			this.entries = new UriCache.Entry[8];
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x0016DA78 File Offset: 0x0016BC78
		public Uri CreateUri(string uriString)
		{
			Uri uri = this.Get(uriString);
			if (uri == null)
			{
				uri = new Uri(uriString);
				this.Set(uriString, uri);
			}
			return uri;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x0016DAA8 File Offset: 0x0016BCA8
		private Uri Get(string key)
		{
			if (key.Length > 128)
			{
				return null;
			}
			for (int i = this.count - 1; i >= 0; i--)
			{
				if (this.entries[i].Key == key)
				{
					return this.entries[i].Value;
				}
			}
			return null;
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x0016DB04 File Offset: 0x0016BD04
		private void Set(string key, Uri value)
		{
			if (key.Length > 128)
			{
				return;
			}
			if (this.count < this.entries.Length)
			{
				UriCache.Entry[] array = this.entries;
				int num = this.count;
				this.count = num + 1;
				array[num] = new UriCache.Entry(key, value);
				return;
			}
			Array.Copy(this.entries, 1, this.entries, 0, this.entries.Length - 1);
			this.entries[this.count - 1] = new UriCache.Entry(key, value);
		}

		// Token: 0x04003902 RID: 14594
		private const int MaxKeyLength = 128;

		// Token: 0x04003903 RID: 14595
		private const int MaxEntries = 8;

		// Token: 0x04003904 RID: 14596
		private UriCache.Entry[] entries;

		// Token: 0x04003905 RID: 14597
		private int count;

		// Token: 0x02000E47 RID: 3655
		private struct Entry
		{
			// Token: 0x060082D2 RID: 33490 RVA: 0x001E3A3E File Offset: 0x001E1C3E
			public Entry(string key, Uri value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x17001CEA RID: 7402
			// (get) Token: 0x060082D3 RID: 33491 RVA: 0x001E3A4E File Offset: 0x001E1C4E
			public string Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17001CEB RID: 7403
			// (get) Token: 0x060082D4 RID: 33492 RVA: 0x001E3A56 File Offset: 0x001E1C56
			public Uri Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04004A48 RID: 19016
			private string key;

			// Token: 0x04004A49 RID: 19017
			private Uri value;
		}
	}
}
