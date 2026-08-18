using System;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x0200000B RID: 11
	public class HashingTypeAttribute : Attribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002403 File Offset: 0x00000603
		public HashingTypeAttribute()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000240B File Offset: 0x0000060B
		public HashingTypeAttribute(string providerHashClassName)
		{
			this.ProviderHashClassName = providerHashClassName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002422 File Offset: 0x00000622
		public string ProviderHashClassName { get; set; }
	}
}
