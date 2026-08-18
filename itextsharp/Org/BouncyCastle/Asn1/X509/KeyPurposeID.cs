using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000307 RID: 775
	public sealed class KeyPurposeID : DerObjectIdentifier
	{
		// Token: 0x06001C68 RID: 7272 RVA: 0x000AA657 File Offset: 0x000A9657
		private KeyPurposeID(string id) : base(id)
		{
		}

		// Token: 0x04001391 RID: 5009
		private const string IdKP = "1.3.6.1.5.5.7.3";

		// Token: 0x04001392 RID: 5010
		public static readonly KeyPurposeID AnyExtendedKeyUsage = new KeyPurposeID(X509Extensions.ExtendedKeyUsage.Id + ".0");

		// Token: 0x04001393 RID: 5011
		public static readonly KeyPurposeID IdKPServerAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.1");

		// Token: 0x04001394 RID: 5012
		public static readonly KeyPurposeID IdKPClientAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.2");

		// Token: 0x04001395 RID: 5013
		public static readonly KeyPurposeID IdKPCodeSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.3");

		// Token: 0x04001396 RID: 5014
		public static readonly KeyPurposeID IdKPEmailProtection = new KeyPurposeID("1.3.6.1.5.5.7.3.4");

		// Token: 0x04001397 RID: 5015
		public static readonly KeyPurposeID IdKPIpsecEndSystem = new KeyPurposeID("1.3.6.1.5.5.7.3.5");

		// Token: 0x04001398 RID: 5016
		public static readonly KeyPurposeID IdKPIpsecTunnel = new KeyPurposeID("1.3.6.1.5.5.7.3.6");

		// Token: 0x04001399 RID: 5017
		public static readonly KeyPurposeID IdKPIpsecUser = new KeyPurposeID("1.3.6.1.5.5.7.3.7");

		// Token: 0x0400139A RID: 5018
		public static readonly KeyPurposeID IdKPTimeStamping = new KeyPurposeID("1.3.6.1.5.5.7.3.8");

		// Token: 0x0400139B RID: 5019
		public static readonly KeyPurposeID IdKPOcspSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.9");

		// Token: 0x0400139C RID: 5020
		public static readonly KeyPurposeID IdKPSmartCardLogon = new KeyPurposeID("1.3.6.1.4.1.311.20.2.2");
	}
}
