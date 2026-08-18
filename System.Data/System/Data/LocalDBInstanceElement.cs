using System;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200033C RID: 828
	internal sealed class LocalDBInstanceElement : ConfigurationElement
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x002C38C8 File Offset: 0x002C2CC8
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return base["name"] as string;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x002C38E8 File Offset: 0x002C2CE8
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
