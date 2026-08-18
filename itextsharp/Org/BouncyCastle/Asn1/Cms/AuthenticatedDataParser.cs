using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000581 RID: 1409
	public class AuthenticatedDataParser
	{
		// Token: 0x06002FF4 RID: 12276 RVA: 0x00127CD7 File Offset: 0x00126CD7
		public AuthenticatedDataParser(Asn1SequenceParser seq)
		{
			this.seq = seq;
			this.version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x00127CF7 File Offset: 0x00126CF7
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00127D00 File Offset: 0x00126D00
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

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00127D7C File Offset: 0x00126D7C
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

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00127DC8 File Offset: 0x00126DC8
		public AlgorithmIdentifier GetMacAlgorithm()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return AlgorithmIdentifier.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00127E18 File Offset: 0x00126E18
		public ContentInfoParser GetEnapsulatedContentInfo()
		{
			if (this.nextObject == null)
			{
				this.nextObject = this.seq.ReadObject();
			}
			if (this.nextObject != null)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)this.nextObject;
				this.nextObject = null;
				return new ContentInfoParser(asn1SequenceParser);
			}
			return null;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x00127E64 File Offset: 0x00126E64
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

		// Token: 0x06002FFB RID: 12283 RVA: 0x00127EBC File Offset: 0x00126EBC
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

		// Token: 0x06002FFC RID: 12284 RVA: 0x00127EFC File Offset: 0x00126EFC
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

		// Token: 0x040020E8 RID: 8424
		private Asn1SequenceParser seq;

		// Token: 0x040020E9 RID: 8425
		private DerInteger version;

		// Token: 0x040020EA RID: 8426
		private IAsn1Convertible nextObject;

		// Token: 0x040020EB RID: 8427
		private bool originatorInfoCalled;
	}
}
