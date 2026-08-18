using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200019B RID: 411
	public class CmsAuthenticatedDataParser : CmsContentInfoParser
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x0005C26D File Offset: 0x0005B26D
		public CmsAuthenticatedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005C27C File Offset: 0x0005B27C
		public CmsAuthenticatedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this.authAttrNotRead = true;
			this.authData = new AuthenticatedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1SetParser recipientInfos = this.authData.GetRecipientInfos();
			IList list = new ArrayList();
			IAsn1Convertible asn1Convertible;
			while ((asn1Convertible = recipientInfos.ReadObject()) != null)
			{
				list.Add(RecipientInfo.GetInstance(asn1Convertible.ToAsn1Object()));
			}
			this.macAlg = this.authData.GetMacAlgorithm();
			ContentInfoParser enapsulatedContentInfo = this.authData.GetEnapsulatedContentInfo();
			Stream octetStream = ((Asn1OctetStringParser)enapsulatedContentInfo.GetContent(4)).GetOctetStream();
			IList recipientInfos2 = CmsEnvelopedHelper.ReadRecipientInfos(list, octetStream, null, this.macAlg, null);
			this._recipientInfoStore = new RecipientInformationStore(recipientInfos2);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0005C333 File Offset: 0x0005B333
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0005C33B File Offset: 0x0005B33B
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.ObjectID.Id;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0005C350 File Offset: 0x0005B350
		public Asn1Object MacAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.macAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0005C374 File Offset: 0x0005B374
		public RecipientInformationStore GetRecipientInfos()
		{
			return this._recipientInfoStore;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0005C37C File Offset: 0x0005B37C
		public byte[] GetMac()
		{
			if (this.mac == null)
			{
				this.GetAuthAttrs();
				this.mac = this.authData.GetMac().GetOctets();
			}
			return Arrays.Clone(this.mac);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0005C3B0 File Offset: 0x0005B3B0
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null && this.authAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetAuthAttrs();
				this.authAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this.authAttrs = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.authAttrs;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0005C434 File Offset: 0x0005B434
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null && this.unauthAttrNotRead)
			{
				Asn1SetParser asn1SetParser = this.authData.GetUnauthAttrs();
				this.unauthAttrNotRead = false;
				if (asn1SetParser != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = asn1SetParser.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this.unauthAttrs = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this.unauthAttrs;
		}

		// Token: 0x04000B87 RID: 2951
		internal RecipientInformationStore _recipientInfoStore;

		// Token: 0x04000B88 RID: 2952
		internal AuthenticatedDataParser authData;

		// Token: 0x04000B89 RID: 2953
		private AlgorithmIdentifier macAlg;

		// Token: 0x04000B8A RID: 2954
		private byte[] mac;

		// Token: 0x04000B8B RID: 2955
		private Org.BouncyCastle.Asn1.Cms.AttributeTable authAttrs;

		// Token: 0x04000B8C RID: 2956
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unauthAttrs;

		// Token: 0x04000B8D RID: 2957
		private bool authAttrNotRead;

		// Token: 0x04000B8E RID: 2958
		private bool unauthAttrNotRead;
	}
}
