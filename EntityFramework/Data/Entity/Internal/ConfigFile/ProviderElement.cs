using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020002A0 RID: 672
	internal class ProviderElement : ConfigurationElement
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00078EF3 File Offset: 0x000770F3
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00078F05 File Offset: 0x00077105
		[ConfigurationProperty("invariantName", IsRequired = true)]
		public string InvariantName
		{
			get
			{
				return (string)base["invariantName"];
			}
			set
			{
				base["invariantName"] = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00078F13 File Offset: 0x00077113
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00078F25 File Offset: 0x00077125
		[ConfigurationProperty("type", IsRequired = true)]
		public string ProviderTypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x0400085A RID: 2138
		private const string InvariantNameKey = "invariantName";

		// Token: 0x0400085B RID: 2139
		private const string TypeKey = "type";
	}
}
