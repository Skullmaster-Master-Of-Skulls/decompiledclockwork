using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020001FD RID: 509
	public class CmsEnvelopedDataParser : CmsContentInfoParser
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x00071E00 File Offset: 0x00070E00
		public CmsEnvelopedDataParser(byte[] envelopedData) : this(new MemoryStream(envelopedData, false))
		{
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00071E10 File Offset: 0x00070E10
		public CmsEnvelopedDataParser(Stream envelopedData) : base(envelopedData)
		{
			this._attrNotRead = true;
			this.envelopedData = new EnvelopedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
			Asn1SetParser recipientInfos = this.envelopedData.GetRecipientInfos();
			IList list = new ArrayList();
			IAsn1Convertible asn1Convertible;
			while ((asn1Convertible = recipientInfos.ReadObject()) != null)
			{
				list.Add(RecipientInfo.GetInstance(asn1Convertible.ToAsn1Object()));
			}
			EncryptedContentInfoParser encryptedContentInfo = this.envelopedData.GetEncryptedContentInfo();
			this._encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			Stream octetStream = ((Asn1OctetStringParser)encryptedContentInfo.GetEncryptedContent(4)).GetOctetStream();
			IList recipientInfos2 = CmsEnvelopedHelper.ReadRecipientInfos(list, octetStream, this._encAlg, null, null);
			this.recipientInfoStore = new RecipientInformationStore(recipientInfos2);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00071EC2 File Offset: 0x00070EC2
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this._encAlg;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00071ECA File Offset: 0x00070ECA
		public string EncryptionAlgOid
		{
			get
			{
				return this._encAlg.ObjectID.Id;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00071EDC File Offset: 0x00070EDC
		public Asn1Object EncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this._encAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00071F00 File Offset: 0x00070F00
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00071F08 File Offset: 0x00070F08
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this._unprotectedAttributes == null && this._attrNotRead)
			{
				Asn1SetParser unprotectedAttrs = this.envelopedData.GetUnprotectedAttrs();
				this._attrNotRead = false;
				if (unprotectedAttrs != null)
				{
					Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = unprotectedAttrs.ReadObject()) != null)
					{
						Asn1SequenceParser asn1SequenceParser = (Asn1SequenceParser)asn1Convertible;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							asn1SequenceParser.ToAsn1Object()
						});
					}
					this._unprotectedAttributes = new Org.BouncyCastle.Asn1.Cms.AttributeTable(new DerSet(asn1EncodableVector));
				}
			}
			return this._unprotectedAttributes;
		}

		// Token: 0x04000DB0 RID: 3504
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000DB1 RID: 3505
		internal EnvelopedDataParser envelopedData;

		// Token: 0x04000DB2 RID: 3506
		private AlgorithmIdentifier _encAlg;

		// Token: 0x04000DB3 RID: 3507
		private Org.BouncyCastle.Asn1.Cms.AttributeTable _unprotectedAttributes;

		// Token: 0x04000DB4 RID: 3508
		private bool _attrNotRead;
	}
}
