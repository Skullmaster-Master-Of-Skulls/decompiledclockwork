using System;
using System.Globalization;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000071 RID: 113
	public sealed class SiteLimits : ConfigurationElement
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00008901 File Offset: 0x00007901
		internal SiteLimits()
		{
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00008909 File Offset: 0x00007909
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000891B File Offset: 0x0000791B
		public TimeSpan ConnectionTimeout
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("connectionTimeout");
			}
			set
			{
				base["connectionTimeout"] = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000892E File Offset: 0x0000792E
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00008940 File Offset: 0x00007940
		public long MaxBandwidth
		{
			get
			{
				return (long)base.GetAttributeValue("maxBandwidth");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"MaxBandwidth",
						0U,
						uint.MaxValue
					}));
				}
				base["maxBandwidth"] = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000089A4 File Offset: 0x000079A4
		// (set) Token: 0x06000337 RID: 823 RVA: 0x000089B8 File Offset: 0x000079B8
		public long MaxConnections
		{
			get
			{
				return (long)base.GetAttributeValue("maxConnections");
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"MaxConnections",
						0U,
						uint.MaxValue
					}));
				}
				base["maxConnections"] = value;
			}
		}
	}
}
