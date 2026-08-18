using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200055F RID: 1375
	public class ReasonFlags : DerBitString
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x00125FAC File Offset: 0x00124FAC
		public ReasonFlags(int reasons) : base(DerBitString.GetBytes(reasons), DerBitString.GetPadBits(reasons))
		{
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x00125FC0 File Offset: 0x00124FC0
		public ReasonFlags(DerBitString reasons) : base(reasons.GetBytes(), reasons.PadBits)
		{
		}

		// Token: 0x040020A0 RID: 8352
		public const int Unused = 128;

		// Token: 0x040020A1 RID: 8353
		public const int KeyCompromise = 64;

		// Token: 0x040020A2 RID: 8354
		public const int CACompromise = 32;

		// Token: 0x040020A3 RID: 8355
		public const int AffiliationChanged = 16;

		// Token: 0x040020A4 RID: 8356
		public const int Superseded = 8;

		// Token: 0x040020A5 RID: 8357
		public const int CessationOfOperation = 4;

		// Token: 0x040020A6 RID: 8358
		public const int CertificateHold = 2;

		// Token: 0x040020A7 RID: 8359
		public const int PrivilegeWithdrawn = 1;

		// Token: 0x040020A8 RID: 8360
		public const int AACompromise = 32768;
	}
}
