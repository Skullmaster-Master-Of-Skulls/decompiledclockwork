using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020005B9 RID: 1465
	public class SmimeEncryptionKeyPreferenceAttribute : AttributeX509
	{
		// Token: 0x0600326A RID: 12906 RVA: 0x00138DE1 File Offset: 0x00137DE1
		public SmimeEncryptionKeyPreferenceAttribute(IssuerAndSerialNumber issAndSer) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 0, issAndSer)))
		{
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x00138DFB File Offset: 0x00137DFB
		public SmimeEncryptionKeyPreferenceAttribute(RecipientKeyIdentifier rKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 1, rKeyID)))
		{
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x00138E15 File Offset: 0x00137E15
		public SmimeEncryptionKeyPreferenceAttribute(Asn1OctetString sKeyID) : base(SmimeAttributes.EncrypKeyPref, new DerSet(new DerTaggedObject(false, 2, sKeyID)))
		{
		}
	}
}
