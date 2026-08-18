using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020003C3 RID: 963
	public class X509KeyUsage : Asn1Encodable
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x000CA271 File Offset: 0x000C9271
		public X509KeyUsage(int usage)
		{
			this.usage = usage;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000CA280 File Offset: 0x000C9280
		public override Asn1Object ToAsn1Object()
		{
			return new KeyUsage(this.usage);
		}

		// Token: 0x04001715 RID: 5909
		public const int DigitalSignature = 128;

		// Token: 0x04001716 RID: 5910
		public const int NonRepudiation = 64;

		// Token: 0x04001717 RID: 5911
		public const int KeyEncipherment = 32;

		// Token: 0x04001718 RID: 5912
		public const int DataEncipherment = 16;

		// Token: 0x04001719 RID: 5913
		public const int KeyAgreement = 8;

		// Token: 0x0400171A RID: 5914
		public const int KeyCertSign = 4;

		// Token: 0x0400171B RID: 5915
		public const int CrlSign = 2;

		// Token: 0x0400171C RID: 5916
		public const int EncipherOnly = 1;

		// Token: 0x0400171D RID: 5917
		public const int DecipherOnly = 32768;

		// Token: 0x0400171E RID: 5918
		private readonly int usage;
	}
}
