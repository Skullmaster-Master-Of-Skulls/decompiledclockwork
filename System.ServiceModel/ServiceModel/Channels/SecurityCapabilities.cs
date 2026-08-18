using System;
using System.Net.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006FE RID: 1790
	internal class SecurityCapabilities : ISecurityCapabilities
	{
		// Token: 0x06004486 RID: 17542 RVA: 0x00102534 File Offset: 0x00100734
		public SecurityCapabilities(bool supportsClientAuth, bool supportsServerAuth, bool supportsClientWindowsIdentity, ProtectionLevel requestProtectionLevel, ProtectionLevel responseProtectionLevel)
		{
			this.supportsClientAuth = supportsClientAuth;
			this.supportsServerAuth = supportsServerAuth;
			this.supportsClientWindowsIdentity = supportsClientWindowsIdentity;
			this.requestProtectionLevel = requestProtectionLevel;
			this.responseProtectionLevel = responseProtectionLevel;
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x00102561 File Offset: 0x00100761
		public ProtectionLevel SupportedRequestProtectionLevel
		{
			get
			{
				return this.requestProtectionLevel;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x00102569 File Offset: 0x00100769
		public ProtectionLevel SupportedResponseProtectionLevel
		{
			get
			{
				return this.responseProtectionLevel;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x00102571 File Offset: 0x00100771
		public bool SupportsClientAuthentication
		{
			get
			{
				return this.supportsClientAuth;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600448A RID: 17546 RVA: 0x00102579 File Offset: 0x00100779
		public bool SupportsClientWindowsIdentity
		{
			get
			{
				return this.supportsClientWindowsIdentity;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x00102581 File Offset: 0x00100781
		public bool SupportsServerAuthentication
		{
			get
			{
				return this.supportsServerAuth;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x00102589 File Offset: 0x00100789
		private static SecurityCapabilities None
		{
			get
			{
				return new SecurityCapabilities(false, false, false, ProtectionLevel.None, ProtectionLevel.None);
			}
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x00102598 File Offset: 0x00100798
		internal static bool IsEqual(ISecurityCapabilities capabilities1, ISecurityCapabilities capabilities2)
		{
			if (capabilities1 == null)
			{
				capabilities1 = SecurityCapabilities.None;
			}
			if (capabilities2 == null)
			{
				capabilities2 = SecurityCapabilities.None;
			}
			return capabilities1.SupportedRequestProtectionLevel == capabilities2.SupportedRequestProtectionLevel && capabilities1.SupportedResponseProtectionLevel == capabilities2.SupportedResponseProtectionLevel && capabilities1.SupportsClientAuthentication == capabilities2.SupportsClientAuthentication && capabilities1.SupportsClientWindowsIdentity == capabilities2.SupportsClientWindowsIdentity && capabilities1.SupportsServerAuthentication == capabilities2.SupportsServerAuthentication;
		}

		// Token: 0x04002D36 RID: 11574
		internal bool supportsServerAuth;

		// Token: 0x04002D37 RID: 11575
		internal bool supportsClientAuth;

		// Token: 0x04002D38 RID: 11576
		internal bool supportsClientWindowsIdentity;

		// Token: 0x04002D39 RID: 11577
		internal ProtectionLevel requestProtectionLevel;

		// Token: 0x04002D3A RID: 11578
		internal ProtectionLevel responseProtectionLevel;
	}
}
