using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000555 RID: 1365
	public class RecipientID : X509CertStoreSelector
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x00124723 File Offset: 0x00123723
		// (set) Token: 0x06002F0D RID: 12045 RVA: 0x00124730 File Offset: 0x00123730
		public byte[] KeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.keyIdentifier);
			}
			set
			{
				this.keyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00124740 File Offset: 0x00123740
		public override int GetHashCode()
		{
			int num = Arrays.GetHashCode(this.keyIdentifier) ^ Arrays.GetHashCode(base.SubjectKeyIdentifier);
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

		// Token: 0x06002F0F RID: 12047 RVA: 0x0012478C File Offset: 0x0012378C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecipientID recipientID = obj as RecipientID;
			return recipientID != null && (Arrays.AreEqual(this.keyIdentifier, recipientID.keyIdentifier) && Arrays.AreEqual(base.SubjectKeyIdentifier, recipientID.SubjectKeyIdentifier) && object.Equals(base.SerialNumber, recipientID.SerialNumber)) && X509CertStoreSelector.IssuersMatch(base.Issuer, recipientID.Issuer);
		}

		// Token: 0x0400205C RID: 8284
		private byte[] keyIdentifier;
	}
}
