using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000133 RID: 307
	public class SignerID : X509CertStoreSelector
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0003F8C4 File Offset: 0x0003E8C4
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(base.SubjectKeyIdentifier);
			BigInteger serialNumber = base.SerialNumber;
			if (serialNumber != null)
			{
				num ^= serialNumber.GetHashCode();
			}
			X509Name issuer = base.Issuer;
			if (issuer != null)
			{
				num ^= issuer.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003F904 File Offset: 0x0003E904
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return false;
			}
			SignerID signerID = obj as SignerID;
			return signerID != null && (Arrays.AreEqual(base.SubjectKeyIdentifier, signerID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, signerID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, signerID.Issuer);
		}
	}
}
