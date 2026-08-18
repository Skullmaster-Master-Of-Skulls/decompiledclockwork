using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A2 RID: 162
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public struct X509IssuerSerial
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0001047C File Offset: 0x0000F47C
		internal X509IssuerSerial(string issuerName, string serialNumber)
		{
			if (issuerName == null || issuerName.Length == 0)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Arg_EmptyOrNullString"), "issuerName");
			}
			if (serialNumber == null || serialNumber.Length == 0)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Arg_EmptyOrNullString"), "serialNumber");
			}
			this.issuerName = issuerName;
			this.serialNumber = serialNumber;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000104D7 File Offset: 0x0000F4D7
		// (set) Token: 0x0600031E RID: 798 RVA: 0x000104DF File Offset: 0x0000F4DF
		public string IssuerName
		{
			get
			{
				return this.issuerName;
			}
			set
			{
				this.issuerName = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000104E8 File Offset: 0x0000F4E8
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000104F0 File Offset: 0x0000F4F0
		public string SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x04000507 RID: 1287
		private string issuerName;

		// Token: 0x04000508 RID: 1288
		private string serialNumber;
	}
}
