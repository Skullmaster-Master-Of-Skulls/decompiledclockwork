using System;

namespace Telerik.Licensing
{
	// Token: 0x0200041E RID: 1054
	internal class DefaultKey : ILicenseKey
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x0007D0FA File Offset: 0x0007B2FA
		public DefaultKey()
		{
			this.Key = "default@telerik.com";
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x0007D10D File Offset: 0x0007B30D
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x0007D115 File Offset: 0x0007B315
		public string Key { get; set; }

		// Token: 0x06002602 RID: 9730 RVA: 0x0007D11E File Offset: 0x0007B31E
		public bool IsValid()
		{
			return true;
		}
	}
}
