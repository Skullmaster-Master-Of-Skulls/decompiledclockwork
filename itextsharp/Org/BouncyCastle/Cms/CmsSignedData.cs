using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002B0 RID: 688
	public class CmsSignedData
	{
		// Token: 0x06001A00 RID: 6656 RVA: 0x0009A79F File Offset: 0x0009979F
		private CmsSignedData(CmsSignedData c)
		{
			this.signedData = c.signedData;
			this.contentInfo = c.contentInfo;
			this.signedContent = c.signedContent;
			this.signerInfoStore = c.signerInfoStore;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0009A7D7 File Offset: 0x000997D7
		public CmsSignedData(byte[] sigBlock) : this(CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0009A7EB File Offset: 0x000997EB
		public CmsSignedData(CmsProcessable signedContent, byte[] sigBlock) : this(signedContent, CmsUtilities.ReadContentInfo(new MemoryStream(sigBlock, false)))
		{
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0009A800 File Offset: 0x00099800
		public CmsSignedData(IDictionary hashes, byte[] sigBlock) : this(hashes, CmsUtilities.ReadContentInfo(sigBlock))
		{
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0009A80F File Offset: 0x0009980F
		public CmsSignedData(CmsProcessable signedContent, Stream sigData) : this(signedContent, CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0009A81E File Offset: 0x0009981E
		public CmsSignedData(Stream sigData) : this(CmsUtilities.ReadContentInfo(sigData))
		{
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0009A82C File Offset: 0x0009982C
		public CmsSignedData(CmsProcessable signedContent, ContentInfo sigData)
		{
			this.signedContent = signedContent;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0009A858 File Offset: 0x00099858
		public CmsSignedData(IDictionary hashes, ContentInfo sigData)
		{
			this.hashes = hashes;
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0009A884 File Offset: 0x00099884
		public CmsSignedData(ContentInfo sigData)
		{
			this.contentInfo = sigData;
			this.signedData = SignedData.GetInstance(this.contentInfo.Content);
			if (this.signedData.EncapContentInfo.Content != null)
			{
				this.signedContent = new CmsProcessableByteArray(((Asn1OctetString)this.signedData.EncapContentInfo.Content).GetOctets());
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0009A8EB File Offset: 0x000998EB
		public int Version
		{
			get
			{
				return this.signedData.Version.Value.IntValue;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0009A904 File Offset: 0x00099904
		public SignerInformationStore GetSignerInfos()
		{
			if (this.signerInfoStore == null)
			{
				IList list = new ArrayList();
				Asn1Set signerInfos = this.signedData.SignerInfos;
				foreach (object obj in signerInfos)
				{
					SignerInfo instance = SignerInfo.GetInstance(obj);
					DerObjectIdentifier contentType = this.signedData.EncapContentInfo.ContentType;
					if (this.hashes == null)
					{
						list.Add(new SignerInformation(instance, contentType, this.signedContent, null));
					}
					else
					{
						byte[] digest = (byte[])this.hashes[instance.DigestAlgorithm.ObjectID.Id];
						list.Add(new SignerInformation(instance, contentType, null, new BaseDigestCalculator(digest)));
					}
				}
				this.signerInfoStore = new SignerInformationStore(list);
			}
			return this.signerInfoStore;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0009A9F8 File Offset: 0x000999F8
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this.attrCertStore == null)
			{
				this.attrCertStore = CmsSignedData.Helper.CreateAttributeStore(type, this.signedData.Certificates);
			}
			return this.attrCertStore;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0009AA24 File Offset: 0x00099A24
		public IX509Store GetCertificates(string type)
		{
			if (this.certificateStore == null)
			{
				this.certificateStore = CmsSignedData.Helper.CreateCertificateStore(type, this.signedData.Certificates);
			}
			return this.certificateStore;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0009AA50 File Offset: 0x00099A50
		public IX509Store GetCrls(string type)
		{
			if (this.crlStore == null)
			{
				this.crlStore = CmsSignedData.Helper.CreateCrlStore(type, this.signedData.CRLs);
			}
			return this.crlStore;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0009AA7C File Offset: 0x00099A7C
		[Obsolete("Use 'SignedContentType' property instead.")]
		public string SignedContentTypeOid
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType.Id;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0009AA93 File Offset: 0x00099A93
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this.signedData.EncapContentInfo.ContentType;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0009AAA5 File Offset: 0x00099AA5
		public CmsProcessable SignedContent
		{
			get
			{
				return this.signedContent;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0009AAAD File Offset: 0x00099AAD
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0009AAB5 File Offset: 0x00099AB5
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0009AAC4 File Offset: 0x00099AC4
		public static CmsSignedData ReplaceSigners(CmsSignedData signedData, SignerInformationStore signerInformationStore)
		{
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			cmsSignedData.signerInfoStore = signerInformationStore;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in signerInformationStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					CmsSignedData.Helper.FixAlgID(signerInformation.DigestAlgorithmID)
				});
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					signerInformation.ToSignerInfo()
				});
			}
			Asn1Set asn1Set = new DerSet(asn1EncodableVector);
			Asn1Set asn1Set2 = new DerSet(asn1EncodableVector2);
			Asn1Sequence asn1Sequence = (Asn1Sequence)signedData.signedData.ToAsn1Object();
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
			{
				asn1Sequence[0],
				asn1Set
			});
			for (int num = 2; num != asn1Sequence.Count - 1; num++)
			{
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					asn1Sequence[num]
				});
			}
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				asn1Set2
			});
			cmsSignedData.signedData = SignedData.GetInstance(new BerSequence(asn1EncodableVector2));
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0009AC40 File Offset: 0x00099C40
		public static CmsSignedData ReplaceCertificatesAndCrls(CmsSignedData signedData, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts)
		{
			if (x509AttrCerts != null)
			{
				throw Platform.CreateNotImplementedException("Currently can't replace attribute certificates");
			}
			CmsSignedData cmsSignedData = new CmsSignedData(signedData);
			Asn1Set certificates = null;
			try
			{
				Asn1Set asn1Set = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCertificatesFromStore(x509Certs));
				if (asn1Set.Count != 0)
				{
					certificates = asn1Set;
				}
			}
			catch (X509StoreException e)
			{
				throw new CmsException("error getting certificates from store", e);
			}
			Asn1Set crls = null;
			try
			{
				Asn1Set asn1Set2 = CmsUtilities.CreateBerSetFromList(CmsUtilities.GetCrlsFromStore(x509Crls));
				if (asn1Set2.Count != 0)
				{
					crls = asn1Set2;
				}
			}
			catch (X509StoreException e2)
			{
				throw new CmsException("error getting CRLs from store", e2);
			}
			SignedData signedData2 = signedData.signedData;
			cmsSignedData.signedData = new SignedData(signedData2.DigestAlgorithms, signedData2.EncapContentInfo, certificates, crls, signedData2.SignerInfos);
			cmsSignedData.contentInfo = new ContentInfo(cmsSignedData.contentInfo.ContentType, cmsSignedData.signedData);
			return cmsSignedData;
		}

		// Token: 0x04001155 RID: 4437
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04001156 RID: 4438
		private readonly CmsProcessable signedContent;

		// Token: 0x04001157 RID: 4439
		private SignedData signedData;

		// Token: 0x04001158 RID: 4440
		private ContentInfo contentInfo;

		// Token: 0x04001159 RID: 4441
		private SignerInformationStore signerInfoStore;

		// Token: 0x0400115A RID: 4442
		private IX509Store attrCertStore;

		// Token: 0x0400115B RID: 4443
		private IX509Store certificateStore;

		// Token: 0x0400115C RID: 4444
		private IX509Store crlStore;

		// Token: 0x0400115D RID: 4445
		private IDictionary hashes;
	}
}
