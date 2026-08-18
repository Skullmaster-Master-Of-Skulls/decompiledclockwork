using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000028 RID: 40
	public class CmsEnvelopedData
	{
		// Token: 0x0600011D RID: 285 RVA: 0x000086DD File Offset: 0x000076DD
		public CmsEnvelopedData(byte[] envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000086EB File Offset: 0x000076EB
		public CmsEnvelopedData(Stream envelopedData) : this(CmsUtilities.ReadContentInfo(envelopedData))
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000086FC File Offset: 0x000076FC
		public CmsEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			EnvelopedData instance = EnvelopedData.GetInstance(contentInfo.Content);
			EncryptedContentInfo encryptedContentInfo = instance.EncryptedContentInfo;
			this.encAlg = encryptedContentInfo.ContentEncryptionAlgorithm;
			byte[] octets = encryptedContentInfo.EncryptedContent.GetOctets();
			IList recipientInfos = CmsEnvelopedHelper.ReadRecipientInfos(instance.RecipientInfos, octets, this.encAlg, null, null);
			this.recipientInfoStore = new RecipientInformationStore(recipientInfos);
			this.unprotectedAttributes = instance.UnprotectedAttrs;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000876E File Offset: 0x0000776E
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this.encAlg;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00008776 File Offset: 0x00007776
		public string EncryptionAlgOid
		{
			get
			{
				return this.encAlg.ObjectID.Id;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008788 File Offset: 0x00007788
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00008790 File Offset: 0x00007790
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008798 File Offset: 0x00007798
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnprotectedAttributes()
		{
			if (this.unprotectedAttributes == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unprotectedAttributes);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000087AF File Offset: 0x000077AF
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04000094 RID: 148
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000095 RID: 149
		internal ContentInfo contentInfo;

		// Token: 0x04000096 RID: 150
		private AlgorithmIdentifier encAlg;

		// Token: 0x04000097 RID: 151
		private Asn1Set unprotectedAttributes;
	}
}
