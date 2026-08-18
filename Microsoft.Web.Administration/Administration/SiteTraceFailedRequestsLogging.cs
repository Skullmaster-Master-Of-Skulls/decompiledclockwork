using System;
using System.Globalization;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000073 RID: 115
	public sealed class SiteTraceFailedRequestsLogging : ConfigurationElement
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00008B6F File Offset: 0x00007B6F
		internal SiteTraceFailedRequestsLogging()
		{
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00008B77 File Offset: 0x00007B77
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00008B89 File Offset: 0x00007B89
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00008B97 File Offset: 0x00007B97
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00008BA9 File Offset: 0x00007BA9
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00008BBC File Offset: 0x00007BBC
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00008BD0 File Offset: 0x00007BD0
		public long MaxLogFiles
		{
			get
			{
				return (long)base.GetAttributeValue("maxLogFiles");
			}
			set
			{
				if (value < 1L || value > 10000L)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"MaxLogFiles",
						1,
						10000
					}));
				}
				base["maxLogFiles"] = value;
			}
		}
	}
}
