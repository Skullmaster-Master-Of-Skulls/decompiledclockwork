using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000268 RID: 616
	public class EnvelopedDataParser
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00085A74 File Offset: 0x00084A74
		public EnvelopedDataParser(Asn1SequenceParser seq)
		{
			this._seq = seq;
			this._version = (DerInteger)seq.ReadObject();
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00085A94 File Offset: 0x00084A94
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00085A9C File Offset: 0x00084A9C
		public OriginatorInfo GetOriginatorInfo()
		{
			this._originatorInfoCalled = true;
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject is Asn1TaggedObjectParser && ((Asn1TaggedObjectParser)this._nextObject).TagNo == 0)
			{
				Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)((Asn1TaggedObjectParser)this._nextObject).GetObjectParser(16, false);
				this._nextObject = null;
				return OriginatorInfo.GetInstance(asn1SequenceParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00085B18 File Offset: 0x00084B18
		public Asn1SetParser GetRecipientInfos()
		{
			if (!this._originatorInfoCalled)
			{
				this.GetOriginatorInfo();
			}
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			Asn1SetParser result = (Asn1SetParser)this._nextObject;
			this._nextObject = null;
			return result;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00085B64 File Offset: 0x00084B64
		public EncryptedContentInfoParser GetEncryptedContentInfo()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				Asn1SequenceParser seq = (Asn1SequenceParser)this._nextObject;
				this._nextObject = null;
				return new EncryptedContentInfoParser(seq);
			}
			return null;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00085BB0 File Offset: 0x00084BB0
		public Asn1SetParser GetUnprotectedAttrs()
		{
			if (this._nextObject == null)
			{
				this._nextObject = this._seq.ReadObject();
			}
			if (this._nextObject != null)
			{
				IAsn1Convertible nextObject = this._nextObject;
				this._nextObject = null;
				return (Asn1SetParser)((Asn1TaggedObjectParser)nextObject).GetObjectParser(17, false);
			}
			return null;
		}

		// Token: 0x04000FEC RID: 4076
		private Asn1SequenceParser _seq;

		// Token: 0x04000FED RID: 4077
		private DerInteger _version;

		// Token: 0x04000FEE RID: 4078
		private IAsn1Convertible _nextObject;

		// Token: 0x04000FEF RID: 4079
		private bool _originatorInfoCalled;
	}
}
