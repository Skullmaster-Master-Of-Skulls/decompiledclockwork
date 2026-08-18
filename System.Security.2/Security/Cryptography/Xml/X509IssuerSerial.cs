using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004B RID: 75
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public struct X509IssuerSerial
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000A7A8 File Offset: 0x000089A8
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A803 File Offset: 0x00008A03
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000A80B File Offset: 0x00008A0B
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A814 File Offset: 0x00008A14
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A81C File Offset: 0x00008A1C
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

		// Token: 0x040003F1 RID: 1009
		private string issuerName;

		// Token: 0x040003F2 RID: 1010
		private string serialNumber;
	}
}
