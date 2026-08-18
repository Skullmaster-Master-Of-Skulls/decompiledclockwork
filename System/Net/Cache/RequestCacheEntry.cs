using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x02000568 RID: 1384
	internal class RequestCacheEntry
	{
		// Token: 0x06002A74 RID: 10868 RVA: 0x000B48CC File Offset: 0x000B38CC
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000B4908 File Offset: 0x000B3908
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
			this.m_IsPartialEntry = ((entry.Info.EntryType & _WinInetCache.EntryType.Sparse) != (_WinInetCache.EntryType)0);
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000B4A8C File Offset: 0x000B3A8C
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x000B4A94 File Offset: 0x000B3A94
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

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000B4A9D File Offset: 0x000B3A9D
		// (set) Token: 0x06002A79 RID: 10873 RVA: 0x000B4AA5 File Offset: 0x000B3AA5
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

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000B4AAE File Offset: 0x000B3AAE
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000B4AB6 File Offset: 0x000B3AB6
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

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000B4ABF File Offset: 0x000B3ABF
		// (set) Token: 0x06002A7D RID: 10877 RVA: 0x000B4AC7 File Offset: 0x000B3AC7
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

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000B4AD0 File Offset: 0x000B3AD0
		// (set) Token: 0x06002A7F RID: 10879 RVA: 0x000B4AD8 File Offset: 0x000B3AD8
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

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000B4AE1 File Offset: 0x000B3AE1
		// (set) Token: 0x06002A81 RID: 10881 RVA: 0x000B4AE9 File Offset: 0x000B3AE9
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

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000B4AF2 File Offset: 0x000B3AF2
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x000B4AFA File Offset: 0x000B3AFA
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

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000B4B03 File Offset: 0x000B3B03
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x000B4B0B File Offset: 0x000B3B0B
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

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000B4B14 File Offset: 0x000B3B14
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x000B4B1C File Offset: 0x000B3B1C
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

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000B4B25 File Offset: 0x000B3B25
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000B4B2D File Offset: 0x000B3B2D
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

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000B4B36 File Offset: 0x000B3B36
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x000B4B3E File Offset: 0x000B3B3E
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

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000B4B47 File Offset: 0x000B3B47
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x000B4B4F File Offset: 0x000B3B4F
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

		// Token: 0x06002A8E RID: 10894 RVA: 0x000B4B58 File Offset: 0x000B3B58
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

		// Token: 0x04002904 RID: 10500
		private bool m_IsPrivateEntry;

		// Token: 0x04002905 RID: 10501
		private long m_StreamSize;

		// Token: 0x04002906 RID: 10502
		private DateTime m_ExpiresUtc;

		// Token: 0x04002907 RID: 10503
		private int m_HitCount;

		// Token: 0x04002908 RID: 10504
		private DateTime m_LastAccessedUtc;

		// Token: 0x04002909 RID: 10505
		private DateTime m_LastModifiedUtc;

		// Token: 0x0400290A RID: 10506
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x0400290B RID: 10507
		private TimeSpan m_MaxStale;

		// Token: 0x0400290C RID: 10508
		private int m_UsageCount;

		// Token: 0x0400290D RID: 10509
		private bool m_IsPartialEntry;

		// Token: 0x0400290E RID: 10510
		private StringCollection m_EntryMetadata;

		// Token: 0x0400290F RID: 10511
		private StringCollection m_SystemMetadata;
	}
}
