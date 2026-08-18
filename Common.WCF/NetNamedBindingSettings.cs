using System;
using System.Net.Security;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class NetNamedBindingSettings : BindingSettings
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000236F File Offset: 0x0000056F
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002377 File Offset: 0x00000577
		public ProtectionLevel ProtectionLevel { get; set; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002380 File Offset: 0x00000580
		public NetNamedBindingSettings()
		{
			this.ProtectionLevel = ProtectionLevel.EncryptAndSign;
		}
	}
}
