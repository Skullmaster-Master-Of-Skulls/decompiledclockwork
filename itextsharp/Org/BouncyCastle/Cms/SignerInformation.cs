using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200050E RID: 1294
	public class SignerInformation
	{
		// Token: 0x06002C35 RID: 11317 RVA: 0x0010D4E4 File Offset: 0x0010C4E4
		internal SignerInformation(Org.BouncyCastle.Asn1.Cms.SignerInfo info, DerObjectIdentifier contentType, CmsProcessable content, IDigestCalculator digestCalculator)
		{
			this.info = info;
			this.sid = new SignerID();
			this.contentType = contentType;
			try
			{
				SignerIdentifier signerID = info.SignerID;
				if (signerID.IsTagged)
				{
					Asn1OctetString instance = Asn1OctetString.GetInstance(signerID.ID);
					this.sid.SubjectKeyIdentifier = instance.GetEncoded();
				}
				else
				{
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber instance2 = Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber.GetInstance(signerID.ID);
					this.sid.Issuer = instance2.Name;
					this.sid.SerialNumber = instance2.SerialNumber.Value;
				}
			}
			catch (IOException)
			{
				throw new ArgumentException("invalid sid in SignerInfo");
			}
			this.digestAlgorithm = info.DigestAlgorithm;
			this.signedAttributeSet = info.AuthenticatedAttributes;
			this.unsignedAttributeSet = info.UnauthenticatedAttributes;
			this.encryptionAlgorithm = info.DigestEncryptionAlgorithm;
			this.signature = info.EncryptedDigest.GetOctets();
			this.content = content;
			this.digestCalculator = digestCalculator;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x0010D5E0 File Offset: 0x0010C5E0
		public SignerID SignerID
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x0010D5E8 File Offset: 0x0010C5E8
		public int Version
		{
			get
			{
				return this.info.Version.Value.IntValue;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x0010D5FF File Offset: 0x0010C5FF
		public AlgorithmIdentifier DigestAlgorithmID
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x0010D607 File Offset: 0x0010C607
		public string DigestAlgOid
		{
			get
			{
				return this.digestAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x0010D61C File Offset: 0x0010C61C
		public Asn1Object DigestAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.digestAlgorithm.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0010D640 File Offset: 0x0010C640
		public byte[] GetContentDigest()
		{
			if (this.resultDigest == null)
			{
				throw new InvalidOperationException("method can only be called after verify.");
			}
			return (byte[])this.resultDigest.Clone();
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x0010D665 File Offset: 0x0010C665
		public AlgorithmIdentifier EncryptionAlgorithmID
		{
			get
			{
				return this.encryptionAlgorithm;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x0010D66D File Offset: 0x0010C66D
		public string EncryptionAlgOid
		{
			get
			{
				return this.encryptionAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x0010D680 File Offset: 0x0010C680
		public Asn1Object EncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.encryptionAlgorithm.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x0010D6A4 File Offset: 0x0010C6A4
		public Org.BouncyCastle.Asn1.Cms.AttributeTable SignedAttributes
		{
			get
			{
				if (this.signedAttributeSet != null && this.signedAttributeTable == null)
				{
					this.signedAttributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.signedAttributeSet);
				}
				return this.signedAttributeTable;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x0010D6CD File Offset: 0x0010C6CD
		public Org.BouncyCastle.Asn1.Cms.AttributeTable UnsignedAttributes
		{
			get
			{
				if (this.unsignedAttributeSet != null && this.unsignedAttributeTable == null)
				{
					this.unsignedAttributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unsignedAttributeSet);
				}
				return this.unsignedAttributeTable;
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0010D6F6 File Offset: 0x0010C6F6
		public byte[] GetSignature()
		{
			return (byte[])this.signature.Clone();
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0010D708 File Offset: 0x0010C708
		public SignerInformationStore GetCounterSignatures()
		{
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes == null)
			{
				return new SignerInformationStore(new ArrayList(0));
			}
			IList list = new ArrayList();
			Asn1EncodableVector all = unsignedAttributes.GetAll(CmsAttributes.CounterSignature);
			foreach (object obj in all)
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
				Asn1Set attrValues = attribute.AttrValues;
				int count = attrValues.Count;
				foreach (object obj2 in attrValues)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
					Org.BouncyCastle.Asn1.Cms.SignerInfo instance = Org.BouncyCastle.Asn1.Cms.SignerInfo.GetInstance(asn1Encodable.ToAsn1Object());
					string digestAlgName = CmsSignedHelper.Instance.GetDigestAlgName(instance.DigestAlgorithm.ObjectID.Id);
					list.Add(new SignerInformation(instance, CmsAttributes.CounterSignature, null, new CounterSignatureDigestCalculator(digestAlgName, this.GetSignature())));
				}
			}
			return new SignerInformationStore(list);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0010D834 File Offset: 0x0010C834
		public byte[] GetEncodedSignedAttributes()
		{
			if (this.signedAttributeSet != null)
			{
				return this.signedAttributeSet.GetEncoded("DER");
			}
			return null;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0010D850 File Offset: 0x0010C850
		private bool DoVerify(AsymmetricKeyParameter key)
		{
			string digestAlgName = SignerInformation.Helper.GetDigestAlgName(this.DigestAlgOid);
			IDigest digestInstance = SignerInformation.Helper.GetDigestInstance(digestAlgName);
			DerObjectIdentifier objectID = this.encryptionAlgorithm.ObjectID;
			Asn1Encodable parameters = this.encryptionAlgorithm.Parameters;
			ISigner signer;
			if (objectID.Equals(PkcsObjectIdentifiers.IdRsassaPss))
			{
				if (parameters == null)
				{
					throw new CmsException("RSASSA-PSS signature must specify algorithm parameters");
				}
				try
				{
					RsassaPssParameters instance = RsassaPssParameters.GetInstance(parameters.ToAsn1Object());
					if (!instance.HashAlgorithm.ObjectID.Equals(this.digestAlgorithm.ObjectID))
					{
						throw new CmsException("RSASSA-PSS signature parameters specified incorrect hash algorithm");
					}
					if (!instance.MaskGenAlgorithm.ObjectID.Equals(PkcsObjectIdentifiers.IdMgf1))
					{
						throw new CmsException("RSASSA-PSS signature parameters specified unknown MGF");
					}
					IDigest digest = DigestUtilities.GetDigest(instance.HashAlgorithm.ObjectID);
					int intValue = instance.SaltLength.Value.IntValue;
					byte b = (byte)instance.TrailerField.Value.IntValue;
					if (b != 1)
					{
						throw new CmsException("RSASSA-PSS signature parameters must have trailerField of 1");
					}
					signer = new PssSigner(new RsaBlindedEngine(), digest, intValue);
					goto IL_142;
				}
				catch (Exception e)
				{
					throw new CmsException("failed to set RSASSA-PSS signature parameters", e);
				}
			}
			string algorithm = digestAlgName + "with" + SignerInformation.Helper.GetEncryptionAlgName(this.EncryptionAlgOid);
			signer = SignerInformation.Helper.GetSignatureInstance(algorithm);
			try
			{
				IL_142:
				if (this.digestCalculator != null)
				{
					this.resultDigest = this.digestCalculator.GetDigest();
				}
				else
				{
					if (this.content != null)
					{
						this.content.Write(new CmsSignedGenerator.DigOutputStream(digestInstance));
					}
					else if (this.signedAttributeSet == null)
					{
						throw new CmsException("data not encapsulated in signature - use detached constructor.");
					}
					this.resultDigest = DigestUtilities.DoFinal(digestInstance);
				}
			}
			catch (IOException e2)
			{
				throw new CmsException("can't process mime object to create signature.", e2);
			}
			bool flag = this.contentType.Equals(CmsAttributes.CounterSignature);
			Asn1Object singleValuedSignedAttribute = this.GetSingleValuedSignedAttribute(CmsAttributes.ContentType, "content-type");
			if (singleValuedSignedAttribute == null)
			{
				if (!flag && this.signedAttributeSet != null)
				{
					throw new CmsException("The content-type attribute type MUST be present whenever signed attributes are present in signed-data");
				}
			}
			else
			{
				if (flag)
				{
					throw new CmsException("[For counter signatures,] the signedAttributes field MUST NOT contain a content-type attribute");
				}
				if (!(singleValuedSignedAttribute is DerObjectIdentifier))
				{
					throw new CmsException("content-type attribute value not of ASN.1 type 'OBJECT IDENTIFIER'");
				}
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)singleValuedSignedAttribute;
				if (!derObjectIdentifier.Equals(this.contentType))
				{
					throw new CmsException("content-type attribute value does not match eContentType");
				}
			}
			Asn1Object singleValuedSignedAttribute2 = this.GetSingleValuedSignedAttribute(CmsAttributes.MessageDigest, "message-digest");
			if (singleValuedSignedAttribute2 == null)
			{
				if (this.signedAttributeSet != null)
				{
					throw new CmsException("the message-digest signed attribute type MUST be present when there are any signed attributes present");
				}
			}
			else
			{
				if (!(singleValuedSignedAttribute2 is Asn1OctetString))
				{
					throw new CmsException("message-digest attribute value not of ASN.1 type 'OCTET STRING'");
				}
				Asn1OctetString asn1OctetString = (Asn1OctetString)singleValuedSignedAttribute2;
				if (!Arrays.AreEqual(this.resultDigest, asn1OctetString.GetOctets()))
				{
					throw new CmsException("message-digest attribute value does not match calculated value");
				}
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributes = this.SignedAttributes;
			if (signedAttributes != null && signedAttributes.GetAll(CmsAttributes.CounterSignature).Count > 0)
			{
				throw new CmsException("A countersignature attribute MUST NOT be a signed attribute");
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes != null)
			{
				foreach (object obj in unsignedAttributes.GetAll(CmsAttributes.CounterSignature))
				{
					Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
					if (attribute.AttrValues.Count < 1)
					{
						throw new CmsException("A countersignature attribute MUST contain at least one AttributeValue");
					}
				}
			}
			bool result;
			try
			{
				signer.Init(false, key);
				if (this.signedAttributeSet == null)
				{
					if (this.digestCalculator != null)
					{
						return this.VerifyDigest(this.resultDigest, key, this.GetSignature());
					}
					if (this.content != null)
					{
						this.content.Write(new CmsSignedGenerator.SigOutputStream(signer));
					}
				}
				else
				{
					byte[] encodedSignedAttributes = this.GetEncodedSignedAttributes();
					signer.BlockUpdate(encodedSignedAttributes, 0, encodedSignedAttributes.Length);
				}
				result = signer.VerifySignature(this.GetSignature());
			}
			catch (InvalidKeyException e3)
			{
				throw new CmsException("key not appropriate to signature in message.", e3);
			}
			catch (IOException e4)
			{
				throw new CmsException("can't process mime object to create signature.", e4);
			}
			catch (SignatureException ex)
			{
				throw new CmsException("invalid signature format in message: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0010DC80 File Offset: 0x0010CC80
		private bool IsNull(Asn1Encodable o)
		{
			return o is Asn1Null || o == null;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0010DC90 File Offset: 0x0010CC90
		private DigestInfo DerDecode(byte[] encoding)
		{
			if (encoding[0] != 48)
			{
				throw new IOException("not a digest info object");
			}
			DigestInfo instance = DigestInfo.GetInstance(Asn1Object.FromByteArray(encoding));
			if (instance.GetEncoded().Length != encoding.Length)
			{
				throw new CmsException("malformed RSA signature");
			}
			return instance;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0010DCD4 File Offset: 0x0010CCD4
		private bool VerifyDigest(byte[] digest, AsymmetricKeyParameter key, byte[] signature)
		{
			string encryptionAlgName = SignerInformation.Helper.GetEncryptionAlgName(this.EncryptionAlgOid);
			bool result;
			try
			{
				if (encryptionAlgName.Equals("RSA"))
				{
					IBufferedCipher cipher = CipherUtilities.GetCipher("RSA//PKCS1Padding");
					cipher.Init(false, key);
					byte[] encoding = cipher.DoFinal(signature);
					DigestInfo digestInfo = this.DerDecode(encoding);
					if (!digestInfo.AlgorithmID.ObjectID.Equals(this.digestAlgorithm.ObjectID))
					{
						result = false;
					}
					else if (!this.IsNull(digestInfo.AlgorithmID.Parameters))
					{
						result = false;
					}
					else
					{
						byte[] digest2 = digestInfo.GetDigest();
						result = Arrays.ConstantTimeAreEqual(digest, digest2);
					}
				}
				else
				{
					if (!encryptionAlgName.Equals("DSA"))
					{
						throw new CmsException("algorithm: " + encryptionAlgName + " not supported in base signatures.");
					}
					ISigner signer = SignerUtilities.GetSigner("NONEwithDSA");
					signer.Init(false, key);
					signer.BlockUpdate(digest, 0, digest.Length);
					result = signer.VerifySignature(signature);
				}
			}
			catch (SecurityUtilityException ex)
			{
				throw ex;
			}
			catch (GeneralSecurityException ex2)
			{
				throw new CmsException("Exception processing signature: " + ex2, ex2);
			}
			catch (IOException ex3)
			{
				throw new CmsException("Exception decoding signature: " + ex3, ex3);
			}
			return result;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0010DE24 File Offset: 0x0010CE24
		public bool Verify(AsymmetricKeyParameter pubKey)
		{
			if (pubKey.IsPrivate)
			{
				throw new ArgumentException("Expected public key", "pubKey");
			}
			this.GetSigningTime();
			return this.DoVerify(pubKey);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0010DE4C File Offset: 0x0010CE4C
		public bool Verify(X509Certificate cert)
		{
			Org.BouncyCastle.Asn1.Cms.Time signingTime = this.GetSigningTime();
			if (signingTime != null)
			{
				cert.CheckValidity(signingTime.Date);
			}
			return this.DoVerify(cert.GetPublicKey());
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0010DE7B File Offset: 0x0010CE7B
		public Org.BouncyCastle.Asn1.Cms.SignerInfo ToSignerInfo()
		{
			return this.info;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0010DE84 File Offset: 0x0010CE84
		private Asn1Object GetSingleValuedSignedAttribute(DerObjectIdentifier attrOID, string printableName)
		{
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = this.UnsignedAttributes;
			if (unsignedAttributes != null && unsignedAttributes.GetAll(attrOID).Count > 0)
			{
				throw new CmsException("The " + printableName + " attribute MUST NOT be an unsigned attribute");
			}
			Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributes = this.SignedAttributes;
			if (signedAttributes == null)
			{
				return null;
			}
			Asn1EncodableVector all = signedAttributes.GetAll(attrOID);
			switch (all.Count)
			{
			case 0:
				return null;
			case 1:
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)all[0];
				Asn1Set attrValues = attribute.AttrValues;
				if (attrValues.Count != 1)
				{
					throw new CmsException("A " + printableName + " attribute MUST have a single attribute value");
				}
				return attrValues[0].ToAsn1Object();
			}
			default:
				throw new CmsException("The SignedAttributes in a signerInfo MUST NOT include multiple instances of the " + printableName + " attribute");
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0010DF48 File Offset: 0x0010CF48
		private Org.BouncyCastle.Asn1.Cms.Time GetSigningTime()
		{
			Asn1Object singleValuedSignedAttribute = this.GetSingleValuedSignedAttribute(CmsAttributes.SigningTime, "signing-time");
			if (singleValuedSignedAttribute == null)
			{
				return null;
			}
			Org.BouncyCastle.Asn1.Cms.Time instance;
			try
			{
				instance = Org.BouncyCastle.Asn1.Cms.Time.GetInstance(singleValuedSignedAttribute);
			}
			catch (ArgumentException)
			{
				throw new CmsException("signing-time attribute value not a valid 'Time' structure");
			}
			return instance;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0010DF94 File Offset: 0x0010CF94
		public static SignerInformation ReplaceUnsignedAttributes(SignerInformation signerInformation, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes)
		{
			Org.BouncyCastle.Asn1.Cms.SignerInfo signerInfo = signerInformation.info;
			Asn1Set unauthenticatedAttributes = null;
			if (unsignedAttributes != null)
			{
				unauthenticatedAttributes = new DerSet(unsignedAttributes.ToAsn1EncodableVector());
			}
			return new SignerInformation(new Org.BouncyCastle.Asn1.Cms.SignerInfo(signerInfo.SignerID, signerInfo.DigestAlgorithm, signerInfo.AuthenticatedAttributes, signerInfo.DigestEncryptionAlgorithm, signerInfo.EncryptedDigest, unauthenticatedAttributes), signerInformation.contentType, signerInformation.content, null);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0010DFF0 File Offset: 0x0010CFF0
		public static SignerInformation AddCounterSigners(SignerInformation signerInformation, SignerInformationStore counterSigners)
		{
			Org.BouncyCastle.Asn1.Cms.SignerInfo signerInfo = signerInformation.info;
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = signerInformation.UnsignedAttributes;
			Asn1EncodableVector asn1EncodableVector;
			if (unsignedAttributes != null)
			{
				asn1EncodableVector = unsignedAttributes.ToAsn1EncodableVector();
			}
			else
			{
				asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			}
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in counterSigners.GetSigners())
			{
				SignerInformation signerInformation2 = (SignerInformation)obj;
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					signerInformation2.ToSignerInfo()
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.CounterSignature, new DerSet(asn1EncodableVector2))
			});
			return new SignerInformation(new Org.BouncyCastle.Asn1.Cms.SignerInfo(signerInfo.SignerID, signerInfo.DigestAlgorithm, signerInfo.AuthenticatedAttributes, signerInfo.DigestEncryptionAlgorithm, signerInfo.EncryptedDigest, new DerSet(asn1EncodableVector)), signerInformation.contentType, signerInformation.content, null);
		}

		// Token: 0x04001E78 RID: 7800
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x04001E79 RID: 7801
		private SignerID sid;

		// Token: 0x04001E7A RID: 7802
		private Org.BouncyCastle.Asn1.Cms.SignerInfo info;

		// Token: 0x04001E7B RID: 7803
		private AlgorithmIdentifier digestAlgorithm;

		// Token: 0x04001E7C RID: 7804
		private AlgorithmIdentifier encryptionAlgorithm;

		// Token: 0x04001E7D RID: 7805
		private readonly Asn1Set signedAttributeSet;

		// Token: 0x04001E7E RID: 7806
		private readonly Asn1Set unsignedAttributeSet;

		// Token: 0x04001E7F RID: 7807
		private CmsProcessable content;

		// Token: 0x04001E80 RID: 7808
		private byte[] signature;

		// Token: 0x04001E81 RID: 7809
		private DerObjectIdentifier contentType;

		// Token: 0x04001E82 RID: 7810
		private IDigestCalculator digestCalculator;

		// Token: 0x04001E83 RID: 7811
		private byte[] resultDigest;

		// Token: 0x04001E84 RID: 7812
		private Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttributeTable;

		// Token: 0x04001E85 RID: 7813
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributeTable;
	}
}
