using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003DF RID: 991
	public class CmsSignedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x000D610D File Offset: 0x000D510D
		public CmsSignedDataParser(byte[] sigBlock) : this(new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000D611C File Offset: 0x000D511C
		public CmsSignedDataParser(CmsTypedStream signedContent, byte[] sigBlock) : this(signedContent, new MemoryStream(sigBlock, false))
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000D612C File Offset: 0x000D512C
		public CmsSignedDataParser(Stream sigData) : this(null, sigData)
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000D6138 File Offset: 0x000D5138
		public CmsSignedDataParser(CmsTypedStream signedContent, Stream sigData) : base(sigData)
		{
			try
			{
				this._signedContent = signedContent;
				this._signedData = SignedDataParser.GetInstance(this.contentInfo.GetContent(16));
				this._digests = new Hashtable();
				this._digestOids = new HashSet();
				Asn1SetParser digestAlgorithms = this._signedData.GetDigestAlgorithms();
				IAsn1Convertible asn1Convertible;
				while ((asn1Convertible = digestAlgorithms.ReadObject()) != null)
				{
					AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance(asn1Convertible.ToAsn1Object());
					try
					{
						string id = instance.ObjectID.Id;
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(id);
						if (!this._digests.Contains(digestAlgName))
						{
							this._digests[digestAlgName] = CmsSignedDataParser.Helper.GetDigestInstance(digestAlgName);
							this._digestOids.Add(id);
						}
					}
					catch (SecurityUtilityException)
					{
					}
				}
				ContentInfoParser encapContentInfo = this._signedData.GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				if (asn1OctetStringParser != null)
				{
					CmsTypedStream cmsTypedStream = new CmsTypedStream(encapContentInfo.ContentType.Id, asn1OctetStringParser.GetOctetStream());
					if (this._signedContent == null)
					{
						this._signedContent = cmsTypedStream;
					}
					else
					{
						cmsTypedStream.Drain();
					}
				}
				this._signedContentType = ((this._signedContent == null) ? encapContentInfo.ContentType : new DerObjectIdentifier(this._signedContent.ContentType));
			}
			catch (IOException ex)
			{
				throw new CmsException("io exception: " + ex.Message, ex);
			}
			if (this._digests.Count < 1)
			{
				throw new CmsException("no digests could be created for message.");
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000D62DC File Offset: 0x000D52DC
		public int Version
		{
			get
			{
				return this._signedData.Version.Value.IntValue;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000D62F3 File Offset: 0x000D52F3
		public ISet DigestOids
		{
			get
			{
				return new HashSet(this._digestOids);
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000D6300 File Offset: 0x000D5300
		public SignerInformationStore GetSignerInfos()
		{
			if (this._signerInfoStore == null)
			{
				this.PopulateCertCrlSets();
				IList list = new ArrayList();
				IDictionary dictionary = new Hashtable();
				foreach (object key in this._digests.Keys)
				{
					dictionary[key] = DigestUtilities.DoFinal((IDigest)this._digests[key]);
				}
				try
				{
					Asn1SetParser signerInfos = this._signedData.GetSignerInfos();
					IAsn1Convertible asn1Convertible;
					while ((asn1Convertible = signerInfos.ReadObject()) != null)
					{
						SignerInfo instance = SignerInfo.GetInstance(asn1Convertible.ToAsn1Object());
						string digestAlgName = CmsSignedDataParser.Helper.GetDigestAlgName(instance.DigestAlgorithm.ObjectID.Id);
						byte[] digest = (byte[])dictionary[digestAlgName];
						list.Add(new SignerInformation(instance, this._signedContentType, null, new BaseDigestCalculator(digest)));
					}
				}
				catch (IOException ex)
				{
					throw new CmsException("io exception: " + ex.Message, ex);
				}
				this._signerInfoStore = new SignerInformationStore(list);
			}
			return this._signerInfoStore;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000D6440 File Offset: 0x000D5440
		public IX509Store GetAttributeCertificates(string type)
		{
			if (this._attributeStore == null)
			{
				this.PopulateCertCrlSets();
				this._attributeStore = CmsSignedDataParser.Helper.CreateAttributeStore(type, this._certSet);
			}
			return this._attributeStore;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000D646D File Offset: 0x000D546D
		public IX509Store GetCertificates(string type)
		{
			if (this._certificateStore == null)
			{
				this.PopulateCertCrlSets();
				this._certificateStore = CmsSignedDataParser.Helper.CreateCertificateStore(type, this._certSet);
			}
			return this._certificateStore;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000D649A File Offset: 0x000D549A
		public IX509Store GetCrls(string type)
		{
			if (this._crlStore == null)
			{
				this.PopulateCertCrlSets();
				this._crlStore = CmsSignedDataParser.Helper.CreateCrlStore(type, this._crlSet);
			}
			return this._crlStore;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000D64C8 File Offset: 0x000D54C8
		private void PopulateCertCrlSets()
		{
			if (this._isCertCrlParsed)
			{
				return;
			}
			this._isCertCrlParsed = true;
			try
			{
				this._certSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCertificates());
				this._crlSet = CmsSignedDataParser.GetAsn1Set(this._signedData.GetCrls());
			}
			catch (IOException e)
			{
				throw new CmsException("problem parsing cert/crl sets", e);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000D6530 File Offset: 0x000D5530
		public DerObjectIdentifier SignedContentType
		{
			get
			{
				return this._signedContentType;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000D6538 File Offset: 0x000D5538
		public CmsTypedStream GetSignedContent()
		{
			if (this._signedContent == null)
			{
				return null;
			}
			Stream stream = this._signedContent.ContentStream;
			foreach (object obj in this._digests.Values)
			{
				IDigest readDigest = (IDigest)obj;
				stream = new DigestStream(stream, readDigest, null);
			}
			return new CmsTypedStream(this._signedContent.ContentType, stream);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000D65C0 File Offset: 0x000D55C0
		public static Stream ReplaceSigners(Stream original, SignerInformationStore signerInformationStore, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddSigners(signerInformationStore);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			cmsSignedDataStreamGenerator.AddAttributeCertificates(cmsSignedDataParser.GetAttributeCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCertificates(cmsSignedDataParser.GetCertificates("Collection"));
			cmsSignedDataStreamGenerator.AddCrls(cmsSignedDataParser.GetCrls("Collection"));
			stream.Close();
			return outStr;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000D6650 File Offset: 0x000D5650
		public static Stream ReplaceCertificatesAndCrls(Stream original, IX509Store x509Certs, IX509Store x509Crls, IX509Store x509AttrCerts, Stream outStr)
		{
			CmsSignedDataStreamGenerator cmsSignedDataStreamGenerator = new CmsSignedDataStreamGenerator();
			CmsSignedDataParser cmsSignedDataParser = new CmsSignedDataParser(original);
			cmsSignedDataStreamGenerator.AddDigests(cmsSignedDataParser.DigestOids);
			CmsTypedStream signedContent = cmsSignedDataParser.GetSignedContent();
			bool flag = signedContent != null;
			Stream stream = cmsSignedDataStreamGenerator.Open(outStr, cmsSignedDataParser.SignedContentType.Id, flag);
			if (flag)
			{
				Streams.PipeAll(signedContent.ContentStream, stream);
			}
			if (x509AttrCerts != null)
			{
				cmsSignedDataStreamGenerator.AddAttributeCertificates(x509AttrCerts);
			}
			if (x509Certs != null)
			{
				cmsSignedDataStreamGenerator.AddCertificates(x509Certs);
			}
			if (x509Crls != null)
			{
				cmsSignedDataStreamGenerator.AddCrls(x509Crls);
			}
			cmsSignedDataStreamGenerator.AddSigners(cmsSignedDataParser.GetSignerInfos());
			stream.Close();
			return outStr;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000D66DE File Offset: 0x000D56DE
		private static Asn1Set GetAsn1Set(Asn1SetParser asn1SetParser)
		{
			if (asn1SetParser != null)
			{
				return Asn1Set.GetInstance(asn1SetParser.ToAsn1Object());
			}
			return null;
		}

		// Token: 0x040017A6 RID: 6054
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x040017A7 RID: 6055
		private SignedDataParser _signedData;

		// Token: 0x040017A8 RID: 6056
		private DerObjectIdentifier _signedContentType;

		// Token: 0x040017A9 RID: 6057
		private CmsTypedStream _signedContent;

		// Token: 0x040017AA RID: 6058
		private IDictionary _digests;

		// Token: 0x040017AB RID: 6059
		private ISet _digestOids;

		// Token: 0x040017AC RID: 6060
		private SignerInformationStore _signerInfoStore;

		// Token: 0x040017AD RID: 6061
		private Asn1Set _certSet;

		// Token: 0x040017AE RID: 6062
		private Asn1Set _crlSet;

		// Token: 0x040017AF RID: 6063
		private bool _isCertCrlParsed;

		// Token: 0x040017B0 RID: 6064
		private IX509Store _attributeStore;

		// Token: 0x040017B1 RID: 6065
		private IX509Store _certificateStore;

		// Token: 0x040017B2 RID: 6066
		private IX509Store _crlStore;
	}
}
