using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020001A0 RID: 416
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class QueryCacheElement : ConfigurationElement
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0003E847 File Offset: 0x0003CA47
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x0003E859 File Offset: 0x0003CA59
		[ConfigurationProperty("size")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int Size
		{
			get
			{
				return (int)base["size"];
			}
			set
			{
				base["size"] = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003E86C File Offset: 0x0003CA6C
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x0003E87E File Offset: 0x0003CA7E
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("cleaningIntervalInSeconds")]
		public int CleaningIntervalInSeconds
		{
			get
			{
				return (int)base["cleaningIntervalInSeconds"];
			}
			set
			{
				base["cleaningIntervalInSeconds"] = value;
			}
		}

		// Token: 0x040003C7 RID: 967
		private const string SizeKey = "size";

		// Token: 0x040003C8 RID: 968
		private const string CleaningIntervalInSecondsKey = "cleaningIntervalInSeconds";
	}
}
