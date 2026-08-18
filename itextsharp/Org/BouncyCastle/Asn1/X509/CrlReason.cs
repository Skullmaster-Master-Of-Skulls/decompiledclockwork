using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000446 RID: 1094
	public class CrlReason : DerEnumerated
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000E1005 File Offset: 0x000E0005
		public CrlReason(int reason) : base(reason)
		{
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000E100E File Offset: 0x000E000E
		public CrlReason(DerEnumerated reason) : base(reason.Value.IntValue)
		{
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000E1024 File Offset: 0x000E0024
		public override string ToString()
		{
			int intValue = base.Value.IntValue;
			string str = (intValue < 0 || intValue > 10) ? "Invalid" : CrlReason.ReasonString[intValue];
			return "CrlReason: " + str;
		}

		// Token: 0x040019D4 RID: 6612
		public const int Unspecified = 0;

		// Token: 0x040019D5 RID: 6613
		public const int KeyCompromise = 1;

		// Token: 0x040019D6 RID: 6614
		public const int CACompromise = 2;

		// Token: 0x040019D7 RID: 6615
		public const int AffiliationChanged = 3;

		// Token: 0x040019D8 RID: 6616
		public const int Superseded = 4;

		// Token: 0x040019D9 RID: 6617
		public const int CessationOfOperation = 5;

		// Token: 0x040019DA RID: 6618
		public const int CertificateHold = 6;

		// Token: 0x040019DB RID: 6619
		public const int RemoveFromCrl = 8;

		// Token: 0x040019DC RID: 6620
		public const int PrivilegeWithdrawn = 9;

		// Token: 0x040019DD RID: 6621
		public const int AACompromise = 10;

		// Token: 0x040019DE RID: 6622
		private static readonly string[] ReasonString = new string[]
		{
			"Unspecified",
			"KeyCompromise",
			"CACompromise",
			"AffiliationChanged",
			"Superseded",
			"CessationOfOperation",
			"CertificateHold",
			"Unknown",
			"RemoveFromCrl",
			"PrivilegeWithdrawn",
			"AACompromise"
		};
	}
}
