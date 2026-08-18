using System;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200012B RID: 299
	internal sealed class LocalDBInstanceElement : ConfigurationElement
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00089D48 File Offset: 0x00089148
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return base["name"] as string;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00089D68 File Offset: 0x00089168
		[ConfigurationProperty("version", IsRequired = true)]
		public string Version
		{
			get
			{
				return base["version"] as string;
			}
		}
	}
}
