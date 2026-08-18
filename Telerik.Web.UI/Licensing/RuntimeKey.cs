using System;

namespace Telerik.Licensing
{
	// Token: 0x02000420 RID: 1056
	internal class RuntimeKey : ILicenseKey
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x0007D15F File Offset: 0x0007B35F
		public RuntimeKey(string key)
		{
			this.Key = key;
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x0007D16E File Offset: 0x0007B36E
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x0007D176 File Offset: 0x0007B376
		public string Key { get; set; }

		// Token: 0x0600260C RID: 9740 RVA: 0x0007D17F File Offset: 0x0007B37F
		public bool IsValid()
		{
			return true;
		}
	}
}
