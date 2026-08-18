using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000557 RID: 1367
	public class CmsSignedDataGenerator : CmsSignedGenerator
	{
		// Token: 0x06002F1F RID: 12063 RVA: 0x0012514C File Offset: 0x0012414C
		public CmsSignedDataGenerator()
		{
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0012515F File Offset: 0x0012415F
		public CmsSignedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x00125173 File Offset: 0x00124173
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID)
		{
			this.AddSigner(privateKey, cert, base.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00125188 File Offset: 0x00124188
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), digestOID, encryptionOID, new DefaultSignedAttributeTableGenerator(), null, null));
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x001251B8 File Offset: 0x001241B8
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID)
		{
			this.AddSigner(privateKey, subjectKeyID, base.GetEncOid(privateKey, digestOID), digestOID);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x001251CC File Offset: 0x001241CC
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), digestOID, encryptionOID, new DefaultSignedAttributeTableGenerator(), null, null));
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x001251FC File Offset: 0x001241FC
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, base.GetEncOid(privateKey, digestOID), digestOID, signedAttr, unsignedAttr);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x00125214 File Offset: 0x00124214
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), digestOID, encryptionOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr));
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0012524D File Offset: 0x0012424D
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOID, base.GetEncOid(privateKey, digestOID), new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00125270 File Offset: 0x00124270
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), digestOID, encryptionOID, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr), signedAttr));
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x001252A9 File Offset: 0x001242A9
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, cert, base.GetEncOid(privateKey, digestOID), digestOID, signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x001252C0 File Offset: 0x001242C0
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), digestOID, encryptionOID, signedAttrGen, unsignedAttrGen, null));
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x001252EE File Offset: 0x001242EE
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOID, base.GetEncOid(privateKey, digestOID), signedAttrGen, unsignedAttrGen);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00125308 File Offset: 0x00124308
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOID, string digestOID, CmsAttributeTableGenerator signedAttrGen, CmsAttributeTableGenerator unsignedAttrGen)
		{
			this.signerInfs.Add(new CmsSignedDataGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), digestOID, encryptionOID, signedAttrGen, unsignedAttrGen, null));
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x00125336 File Offset: 0x00124336
		public CmsSignedData Generate(CmsProcessable content)
		{
			return this.Generate(content, false);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x00125340 File Offset: 0x00124340
		public CmsSignedData Generate(string signedContentType, CmsProcessable content, bool encapsulate)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			this._digests.Clear();
			foreach (object obj in this._signers)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					CmsSignedDataGenerator.Helper.FixAlgID(signerInformation.DigestAlgorithmID)
				});
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					signerInformation.ToSignerInfo()
				});
			}
			bool flag = signedContentType == null;
			DerObjectIdentifier contentType = flag ? CmsObjectIdentifiers.Data : new DerObjectIdentifier(signedContentType);
			foreach (object obj2 in this.signerInfs)
			{
				CmsSignedDataGenerator.SignerInf signerInf = (CmsSignedDataGenerator.SignerInf)obj2;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						signerInf.DigestAlgorithmID
					});
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						signerInf.ToSignerInfo(contentType, content, this.rand, flag)
					});
				}
				catch (IOException e)
				{
					throw new CmsException("encoding error.", e);
				}
				catch (InvalidKeyException e2)
				{
					throw new CmsException("key inappropriate for signature.", e2);
				}
				catch (SignatureException e3)
				{
					throw new CmsException("error creating signature.", e3);
				}
				catch (CertificateEncodingException e4)
				{
					throw new CmsException("error creating sid.", e4);
				}
			}
			Asn1Set certificates = null;
			if (this._certs.Count != 0)
			{
				certificates = CmsUtilities.CreateBerSetFromList(this._certs);
			}
			Asn1Set crls = null;
			if (this._crls.Count != 0)
			{
				crls = CmsUtilities.CreateBerSetFromList(this._crls);
			}
			Asn1OctetString content2 = null;
			if (encapsulate)
			{
				MemoryStream memoryStream = new MemoryStream();
				if (content != null)
				{
					try
					{
						content.Write(memoryStream);
					}
					catch (IOException e5)
					{
						throw new CmsException("encapsulation error.", e5);
					}
				}
				content2 = new BerOctetString(memoryStream.ToArray());
			}
			ContentInfo contentInfo = new ContentInfo(contentType, content2);
			SignedData content3 = new SignedData(new DerSet(asn1EncodableVector), contentInfo, certificates, crls, new DerSet(asn1EncodableVector2));
			ContentInfo sigData = new ContentInfo(CmsObjectIdentifiers.SignedData, content3);
			return new CmsSignedData(content, sigData);
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x001255C8 File Offset: 0x001245C8
		public CmsSignedData Generate(CmsProcessable content, bool encapsulate)
		{
			return this.Generate(CmsSignedGenerator.Data, content, encapsulate);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x001255D7 File Offset: 0x001245D7
		public SignerInformationStore GenerateCounterSigners(SignerInformation signer)
		{
			return this.Generate(null, new CmsProcessableByteArray(signer.GetSignature()), false).GetSignerInfos();
		}

		// Token: 0x04002061 RID: 8289
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04002062 RID: 8290
		private readonly ArrayList signerInfs = new ArrayList();

		// Token: 0x02000558 RID: 1368
		private class SignerInf
		{
			// Token: 0x06002F32 RID: 12082 RVA: 0x00125600 File Offset: 0x00124600
			internal SignerInf(CmsSignedGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable)
			{
				this.outer = outer;
				this.key = key;
				this.signerIdentifier = signerIdentifier;
				this.digestOID = digestOID;
				this.encOID = encOID;
				this.sAttr = sAttr;
				this.unsAttr = unsAttr;
				this.baseSignedTable = baseSignedTable;
			}

			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00125650 File Offset: 0x00124650
			internal AlgorithmIdentifier DigestAlgorithmID
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this.digestOID), DerNull.Instance);
				}
			}

			// Token: 0x17000811 RID: 2065
			// (get) Token: 0x06002F34 RID: 12084 RVA: 0x00125667 File Offset: 0x00124667
			internal CmsAttributeTableGenerator SignedAttributes
			{
				get
				{
					return this.sAttr;
				}
			}

			// Token: 0x17000812 RID: 2066
			// (get) Token: 0x06002F35 RID: 12085 RVA: 0x0012566F File Offset: 0x0012466F
			internal CmsAttributeTableGenerator UnsignedAttributes
			{
				get
				{
					return this.unsAttr;
				}
			}

			// Token: 0x06002F36 RID: 12086 RVA: 0x00125678 File Offset: 0x00124678
			internal SignerInfo ToSignerInfo(DerObjectIdentifier contentType, CmsProcessable content, SecureRandom random, bool isCounterSignature)
			{
				AlgorithmIdentifier digestAlgorithmID = this.DigestAlgorithmID;
				string digestAlgName = CmsSignedDataGenerator.Helper.GetDigestAlgName(this.digestOID);
				IDigest digestInstance = CmsSignedDataGenerator.Helper.GetDigestInstance(digestAlgName);
				string algorithm = digestAlgName + "with" + CmsSignedDataGenerator.Helper.GetEncryptionAlgName(this.encOID);
				ISigner signatureInstance = CmsSignedDataGenerator.Helper.GetSignatureInstance(algorithm);
				if (content != null)
				{
					content.Write(new CmsSignedGenerator.DigOutputStream(digestInstance));
				}
				byte[] array = DigestUtilities.DoFinal(digestInstance);
				this.outer._digests.Add(this.digestOID, array.Clone());
				Asn1Set asn1Set = null;
				byte[] array2;
				if (this.sAttr != null)
				{
					IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = this.sAttr.GetAttributes(baseParameters);
					if (isCounterSignature)
					{
						Hashtable hashtable = attributeTable.ToHashtable();
						hashtable.Remove(CmsAttributes.ContentType);
						attributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(hashtable);
					}
					asn1Set = this.outer.GetAttributeSet(attributeTable);
					array2 = asn1Set.GetEncoded("DER");
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					if (content != null)
					{
						content.Write(memoryStream);
					}
					array2 = memoryStream.ToArray();
				}
				signatureInstance.Init(true, new ParametersWithRandom(this.key, random));
				signatureInstance.BlockUpdate(array2, 0, array2.Length);
				byte[] array3 = signatureInstance.GenerateSignature();
				Asn1Set unauthenticatedAttributes = null;
				if (this.unsAttr != null)
				{
					IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					baseParameters2[CmsAttributeTableParameter.Signature] = array3.Clone();
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this.unsAttr.GetAttributes(baseParameters2);
					unauthenticatedAttributes = this.outer.GetAttributeSet(attributes);
				}
				Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
				AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedGenerator.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this.encOID), defaultX509Parameters);
				return new SignerInfo(this.signerIdentifier, digestAlgorithmID, asn1Set, encAlgorithmIdentifier, new DerOctetString(array3), unauthenticatedAttributes);
			}

			// Token: 0x04002063 RID: 8291
			private readonly CmsSignedGenerator outer;

			// Token: 0x04002064 RID: 8292
			private readonly AsymmetricKeyParameter key;

			// Token: 0x04002065 RID: 8293
			private readonly SignerIdentifier signerIdentifier;

			// Token: 0x04002066 RID: 8294
			private readonly string digestOID;

			// Token: 0x04002067 RID: 8295
			private readonly string encOID;

			// Token: 0x04002068 RID: 8296
			private readonly CmsAttributeTableGenerator sAttr;

			// Token: 0x04002069 RID: 8297
			private readonly CmsAttributeTableGenerator unsAttr;

			// Token: 0x0400206A RID: 8298
			private readonly Org.BouncyCastle.Asn1.Cms.AttributeTable baseSignedTable;
		}
	}
}
