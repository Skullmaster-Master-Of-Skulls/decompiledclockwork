using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200031A RID: 794
	public class AuthEnvelopedDataParser
	{
		// Token: 0x06001CDE RID: 7390 RVA: 0x000ABF47 File Offset: 0x000AAF47
		public AuthEnvelopedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x000ABF67 File Offset: 0x000AAF67
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000ABF70 File Offset: 0x000AAF70
		public OriginatorInfo GetOriginatorInfo()
		{
			this.originatorInfoCalled = true;
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this.nextObject).TagNo == 0)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)((Asn1TaggedObjectParser)this.nextObject).GetObjectParser(16, false);
				this.nextObject = null;
				return OriginatorInfo.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000ABFEC File Offset: 0x000AAFEC
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this.originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this.nextObject;
			this.nextObject = null;
			return result;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000AC038 File Offset: 0x000AB038
		public EncryptedContentInfoParser GetAuthEncryptedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new EncryptedContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000AC084 File Offset: 0x000AB084
		public Asn1SetParser GetAuthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject is Asn1TaggedObjectParser)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000AC0DC File Offset: 0x000AB0DC
		public Asn1OctetString GetMac()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			IAsn1Convertible asn1Convertible = this.nextObject;
			this.nextObject = null;
			return Asn1OctetString.GetInstance(asn1Convertible.ToAsn1Object());
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000AC11C File Offset: 0x000AB11C
		public Asn1SetParser GetUnauthAttrs()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				IAsn1Convertible asn1Convertible = this.nextObject;
				this.nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)asn1Convertible).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x040013E0 RID: 5088
		private Asn1SequenceParser seq;

		// Token: 0x040013E1 RID: 5089
		private DerInteger version;

		// Token: 0x040013E2 RID: 5090
		private IAsn1Convertible nextObject;

		// Token: 0x040013E3 RID: 5091
		private bool originatorInfoCalled;
	}
}
