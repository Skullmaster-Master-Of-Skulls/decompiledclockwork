using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020004AE RID: 1198
	public interface IX509AttributeCertificate : IX509Extension
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002876 RID: 10358
		int Version { get; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002877 RID: 10359
		BigInteger SerialNumber { get; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002878 RID: 10360
		DateTime NotBefore { get; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002879 RID: 10361
		DateTime NotAfter { get; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600287A RID: 10362
		AttributeCertificateHolder Holder { get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600287B RID: 10363
		AttributeCertificateIssuer Issuer { get; }

		// Token: 0x0600287C RID: 10364
		X509Attribute[] GetAttributes();

		// Token: 0x0600287D RID: 10365
		X509Attribute[] GetAttributes(string oid);

		// Token: 0x0600287E RID: 10366
		bool[] GetIssuerUniqueID();

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600287F RID: 10367
		bool IsValidNow { get; }

		// Token: 0x06002880 RID: 10368
		bool IsValid(DateTime date);

		// Token: 0x06002881 RID: 10369
		void CheckValidity();

		// Token: 0x06002882 RID: 10370
		void CheckValidity(DateTime date);

		// Token: 0x06002883 RID: 10371
		byte[] GetSignature();

		// Token: 0x06002884 RID: 10372
		void Verify(AsymmetricKeyParameter publicKey);

		// Token: 0x06002885 RID: 10373
		byte[] GetEncoded();
	}
}
