using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200030C RID: 780
	public class EncryptionScheme : AlgorithmIdentifier
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x000AB035 File Offset: 0x000AA035
		internal EncryptionScheme(Asn1Sequence seq) : base(seq)
		{
			this.objectID = (Asn1Object)seq[0];
			this.obj = (Asn1Object)seq[1];
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000AB062 File Offset: 0x000AA062
		public Asn1Object Asn1Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000AB06C File Offset: 0x000AA06C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.objectID,
				this.obj
			});
		}

		// Token: 0x040013AA RID: 5034
		private readonly Asn1Object objectID;

		// Token: 0x040013AB RID: 5035
		private readonly Asn1Object obj;
	}
}
