using System;
using System.Net.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006FD RID: 1789
	public interface ISecurityCapabilities
	{
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06004481 RID: 17537
		ProtectionLevel SupportedRequestProtectionLevel { get; }

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06004482 RID: 17538
		ProtectionLevel SupportedResponseProtectionLevel { get; }

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06004483 RID: 17539
		bool SupportsClientAuthentication { get; }

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06004484 RID: 17540
		bool SupportsClientWindowsIdentity { get; }

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06004485 RID: 17541
		bool SupportsServerAuthentication { get; }
	}
}
