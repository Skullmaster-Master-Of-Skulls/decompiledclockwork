using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x0200030E RID: 782
	internal class RequestCacheEntry
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x00085710 File Offset: 0x00083910
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0008574C File Offset: 0x0008394C
		internal RequestCacheEntry(_WinInetCache.Entry entry, bool isPrivateEntry)
		{
			this.m_IsPrivateEntry = isPrivateEntry;
			this.m_StreamSize = ((long)entry.Info.SizeHigh << 32 | (long)entry.Info.SizeLow);
			this.m_ExpiresUtc = (entry.Info.ExpireTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.ExpireTime.ToLong()));
			this.m_HitCount = entry.Info.HitRate;
			this.m_LastAccessedUtc = (entry.Info.LastAccessTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastAccessTime.ToLong()));
			this.m_LastModifiedUtc = (entry.Info.LastModifiedTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastModifiedTime.ToLong()));
			this.m_LastSynchronizedUtc = (entry.Info.LastSyncTime.IsNull ? DateTime.MinValue : DateTime.FromFileTimeUtc(entry.Info.LastSyncTime.ToLong()));
			this.m_MaxStale = TimeSpan.FromSeconds((double)entry.Info.U.ExemptDelta);
			if (this.m_MaxStale == WinInetCache.s_MaxTimeSpanForInt32)
			{
				this.m_MaxStale = TimeSpan.MaxValue;
			}
			this.m_UsageCount = entry.Info.UseCount;
			this.m_IsPartialEntry = ((entry.Info.EntryType & _WinInetCache.EntryType.Sparse) > (_WinInetCache.EntryType)0);
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x000858CD File Offset: 0x00083ACD
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x000858D5 File Offset: 0x00083AD5
		internal bool IsPrivateEntry
		{
			get
			{
				return this.m_IsPrivateEntry;
			}
			set
			{
				this.m_IsPrivateEntry = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000858DE File Offset: 0x00083ADE
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x000858E6 File Offset: 0x00083AE6
		internal long StreamSize
		{
			get
			{
				return this.m_StreamSize;
			}
			set
			{
				this.m_StreamSize = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000858EF File Offset: 0x00083AEF
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x000858F7 File Offset: 0x00083AF7
		internal DateTime ExpiresUtc
		{
			get
			{
				return this.m_ExpiresUtc;
			}
			set
			{
				this.m_ExpiresUtc = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00085900 File Offset: 0x00083B00
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x00085908 File Offset: 0x00083B08
		internal DateTime LastAccessedUtc
		{
			get
			{
				return this.m_LastAccessedUtc;
			}
			set
			{
				this.m_LastAccessedUtc = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00085911 File Offset: 0x00083B11
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00085919 File Offset: 0x00083B19
		internal DateTime LastModifiedUtc
		{
			get
			{
				return this.m_LastModifiedUtc;
			}
			set
			{
				this.m_LastModifiedUtc = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00085922 File Offset: 0x00083B22
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0008592A File Offset: 0x00083B2A
		internal DateTime LastSynchronizedUtc
		{
			get
			{
				return this.m_LastSynchronizedUtc;
			}
			set
			{
				this.m_LastSynchronizedUtc = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00085933 File Offset: 0x00083B33
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0008593B File Offset: 0x00083B3B
		internal TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
			set
			{
				this.m_MaxStale = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x00085944 File Offset: 0x00083B44
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x0008594C File Offset: 0x00083B4C
		internal int HitCount
		{
			get
			{
				return this.m_HitCount;
			}
			set
			{
				this.m_HitCount = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00085955 File Offset: 0x00083B55
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x0008595D File Offset: 0x00083B5D
		internal int UsageCount
		{
			get
			{
				return this.m_UsageCount;
			}
			set
			{
				this.m_UsageCount = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00085966 File Offset: 0x00083B66
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x0008596E File Offset: 0x00083B6E
		internal bool IsPartialEntry
		{
			get
			{
				return this.m_IsPartialEntry;
			}
			set
			{
				this.m_IsPartialEntry = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00085977 File Offset: 0x00083B77
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x0008597F File Offset: 0x00083B7F
		internal StringCollection EntryMetadata
		{
			get
			{
				return this.m_EntryMetadata;
			}
			set
			{
				this.m_EntryMetadata = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00085988 File Offset: 0x00083B88
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x00085990 File Offset: 0x00083B90
		internal StringCollection SystemMetadata
		{
			get
			{
				return this.m_SystemMetadata;
			}
			set
			{
				this.m_SystemMetadata = value;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0008599C File Offset: 0x00083B9C
		internal virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("\r\nIsPrivateEntry   = ").Append(this.IsPrivateEntry);
			stringBuilder.Append("\r\nIsPartialEntry   = ").Append(this.IsPartialEntry);
			stringBuilder.Append("\r\nStreamSize       = ").Append(this.StreamSize);
			stringBuilder.Append("\r\nExpires          = ").Append((this.ExpiresUtc == DateTime.MinValue) ? "" : this.ExpiresUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastAccessed     = ").Append((this.LastAccessedUtc == DateTime.MinValue) ? "" : this.LastAccessedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastModified     = ").Append((this.LastModifiedUtc == DateTime.MinValue) ? "" : this.LastModifiedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastSynchronized = ").Append((this.LastSynchronizedUtc == DateTime.MinValue) ? "" : this.LastSynchronizedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nMaxStale(sec)    = ").Append((this.MaxStale == TimeSpan.MinValue) ? "" : ((int)this.MaxStale.TotalSeconds).ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nHitCount         = ").Append(this.HitCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nUsageCount       = ").Append(this.UsageCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\n");
			if (verbose)
			{
				stringBuilder.Append("EntryMetadata:\r\n");
				if (this.m_EntryMetadata != null)
				{
					foreach (string value in this.m_EntryMetadata)
					{
						stringBuilder.Append(value).Append("\r\n");
					}
				}
				stringBuilder.Append("---\r\nSystemMetadata:\r\n");
				if (this.m_SystemMetadata != null)
				{
					foreach (string value2 in this.m_SystemMetadata)
					{
						stringBuilder.Append(value2).Append("\r\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B43 RID: 6979
		private bool m_IsPrivateEntry;

		// Token: 0x04001B44 RID: 6980
		private long m_StreamSize;

		// Token: 0x04001B45 RID: 6981
		private DateTime m_ExpiresUtc;

		// Token: 0x04001B46 RID: 6982
		private int m_HitCount;

		// Token: 0x04001B47 RID: 6983
		private DateTime m_LastAccessedUtc;

		// Token: 0x04001B48 RID: 6984
		private DateTime m_LastModifiedUtc;

		// Token: 0x04001B49 RID: 6985
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x04001B4A RID: 6986
		private TimeSpan m_MaxStale;

		// Token: 0x04001B4B RID: 6987
		private int m_UsageCount;

		// Token: 0x04001B4C RID: 6988
		private bool m_IsPartialEntry;

		// Token: 0x04001B4D RID: 6989
		private StringCollection m_EntryMetadata;

		// Token: 0x04001B4E RID: 6990
		private StringCollection m_SystemMetadata;
	}
}
