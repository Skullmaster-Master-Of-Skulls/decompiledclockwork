using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020001FC RID: 508
	public class OriginatorID : X509CertStoreSelector
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00071D60 File Offset: 0x00070D60
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

		// Token: 0x060013AF RID: 5039 RVA: 0x00071DA0 File Offset: 0x00070DA0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return false;
			}
			OriginatorID originatorID = obj as OriginatorID;
			return originatorID != null && (Arrays.AreEqual(base.SubjectKeyIdentifier, originatorID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, originatorID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, originatorID.Issuer);
		}
	}
}
