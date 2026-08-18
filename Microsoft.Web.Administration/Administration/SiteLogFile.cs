using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000072 RID: 114
	public sealed class SiteLogFile : ConfigurationElement
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00008A1C File Offset: 0x00007A1C
		internal SiteLogFile()
		{
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008A24 File Offset: 0x00007A24
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00008A36 File Offset: 0x00007A36
		public string Directory
		{
			get
			{
				return (string)base.GetAttributeValue("directory");
			}
			set
			{
				base["directory"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00008A44 File Offset: 0x00007A44
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00008A56 File Offset: 0x00007A56
		public bool LocalTimeRollover
		{
			get
			{
				return (bool)base.GetAttributeValue("localTimeRollover");
			}
			set
			{
				base["localTimeRollover"] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008A69 File Offset: 0x00007A69
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00008A7B File Offset: 0x00007A7B
		public LogExtFileFlags LogExtFileFlags
		{
			get
			{
				return (LogExtFileFlags)base.GetAttributeValue("logExtFileFlags");
			}
			set
			{
				base["logExtFileFlags"] = (int)value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00008A8E File Offset: 0x00007A8E
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00008AA0 File Offset: 0x00007AA0
		public bool Enabled
		{
			get
			{
				return (bool)base.GetAttributeValue("enabled");
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008AB4 File Offset: 0x00007AB4
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00008AE6 File Offset: 0x00007AE6
		public Guid CustomLogPluginClsid
		{
			get
			{
				string text = (string)base.GetAttributeValue("customLogPluginClsid");
				if (!string.IsNullOrEmpty(text))
				{
					return new Guid(text);
				}
				return Guid.Empty;
			}
			set
			{
				base["customLogPluginClsid"] = value.ToString();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008B00 File Offset: 0x00007B00
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00008B12 File Offset: 0x00007B12
		public LoggingRolloverPeriod Period
		{
			get
			{
				return (LoggingRolloverPeriod)base.GetAttributeValue("period");
			}
			set
			{
				base["period"] = (int)value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008B25 File Offset: 0x00007B25
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00008B37 File Offset: 0x00007B37
		public LogFormat LogFormat
		{
			get
			{
				return (LogFormat)base.GetAttributeValue("logFormat");
			}
			set
			{
				base["logFormat"] = (int)value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008B4A File Offset: 0x00007B4A
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00008B5C File Offset: 0x00007B5C
		public long TruncateSize
		{
			get
			{
				return (long)base.GetAttributeValue("truncateSize");
			}
			set
			{
				base["truncateSize"] = value;
			}
		}
	}
}
