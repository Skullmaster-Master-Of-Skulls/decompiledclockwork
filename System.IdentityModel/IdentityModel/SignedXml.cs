using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200007D RID: 125
	internal sealed class SignedXml : ISignatureValueSecurityElement, ISecurityElement
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00010201 File Offset: 0x0000E401
		public SignedXml(DictionaryManager dictionaryManager, SecurityTokenSerializer tokenSerializer) : this(new StandardSignedInfo(dictionaryManager), dictionaryManager, tokenSerializer)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00010214 File Offset: 0x0000E414
		internal SignedXml(SignedInfo signedInfo, DictionaryManager dictionaryManager, SecurityTokenSerializer tokenSerializer)
		{
			if (signedInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("signedInfo"));
			}
			if (dictionaryManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryManager");
			}
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer");
			}
			this.transformFactory = StandardTransformFactory.Instance;
			this.tokenSerializer = tokenSerializer;
			this.signature = new Signature(this, signedInfo);
			this.dictionaryManager = dictionaryManager;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00002434 File Offset: 0x00000634
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001028B File Offset: 0x0000E48B
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00010298 File Offset: 0x0000E498
		public string Id
		{
			get
			{
				return this.signature.Id;
			}
			set
			{
				this.signature.Id = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x000102A6 File Offset: 0x0000E4A6
		public SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000102AE File Offset: 0x0000E4AE
		public Signature Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000102B6 File Offset: 0x0000E4B6
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000102BE File Offset: 0x0000E4BE
		public TransformFactory TransformFactory
		{
			get
			{
				return this.transformFactory;
			}
			set
			{
				this.transformFactory = value;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000102C8 File Offset: 0x0000E4C8
		private void ComputeSignature(HashAlgorithm hash, AsymmetricSignatureFormatter formatter, string signatureMethod)
		{
			this.Signature.SignedInfo.ComputeReferenceDigests();
			this.Signature.SignedInfo.ComputeHash(hash);
			byte[] signatureValue;
			if (SecurityUtils.RequiresFipsCompliance && signatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
			{
				formatter.SetHashAlgorithm("SHA256");
				signatureValue = formatter.CreateSignature(hash.Hash);
			}
			else
			{
				signatureValue = formatter.CreateSignature(hash);
			}
			this.Signature.SetSignatureValue(signatureValue);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00010338 File Offset: 0x0000E538
		private void ComputeSignature(KeyedHashAlgorithm hash)
		{
			this.Signature.SignedInfo.ComputeReferenceDigests();
			this.Signature.SignedInfo.ComputeHash(hash);
			byte[] hash2 = hash.Hash;
			this.Signature.SetSignatureValue(hash2);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001037C File Offset: 0x0000E57C
		public void ComputeSignature(SecurityKey signingKey)
		{
			string signatureMethod = this.Signature.SignedInfo.SignatureMethod;
			SymmetricSecurityKey symmetricSecurityKey = signingKey as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				using (KeyedHashAlgorithm keyedHashAlgorithm = symmetricSecurityKey.GetKeyedHashAlgorithm(signatureMethod))
				{
					if (keyedHashAlgorithm == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToCreateKeyedHashAlgorithm", new object[]
						{
							symmetricSecurityKey,
							signatureMethod
						})));
					}
					this.ComputeSignature(keyedHashAlgorithm);
					return;
				}
			}
			AsymmetricSecurityKey asymmetricSecurityKey = signingKey as AsymmetricSecurityKey;
			if (asymmetricSecurityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownICryptoType", new object[]
				{
					signingKey
				})));
			}
			using (HashAlgorithm hashAlgorithmForSignature = asymmetricSecurityKey.GetHashAlgorithmForSignature(signatureMethod))
			{
				if (hashAlgorithmForSignature == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToCreateHashAlgorithmFromAsymmetricCrypto", new object[]
					{
						signatureMethod,
						asymmetricSecurityKey
					})));
				}
				AsymmetricSignatureFormatter signatureFormatter = asymmetricSecurityKey.GetSignatureFormatter(signatureMethod);
				if (signatureFormatter == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToCreateSignatureFormatterFromAsymmetricCrypto", new object[]
					{
						signatureMethod,
						asymmetricSecurityKey
					})));
				}
				this.ComputeSignature(hashAlgorithmForSignature, signatureFormatter, signatureMethod);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000104BC File Offset: 0x0000E6BC
		public void CompleteSignatureVerification()
		{
			this.Signature.SignedInfo.EnsureAllReferencesVerified();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000104CE File Offset: 0x0000E6CE
		public void EnsureDigestValidity(string id, object resolvedXmlSource)
		{
			this.Signature.SignedInfo.EnsureDigestValidity(id, resolvedXmlSource);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000104E2 File Offset: 0x0000E6E2
		public bool EnsureDigestValidityIfIdMatches(string id, object resolvedXmlSource)
		{
			return this.Signature.SignedInfo.EnsureDigestValidityIfIdMatches(id, resolvedXmlSource);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000104F6 File Offset: 0x0000E6F6
		public byte[] GetSignatureValue()
		{
			return this.Signature.GetSignatureBytes();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010503 File Offset: 0x0000E703
		public void ReadFrom(XmlReader reader)
		{
			this.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010511 File Offset: 0x0000E711
		public void ReadFrom(XmlDictionaryReader reader)
		{
			this.signature.ReadFrom(reader, this.dictionaryManager);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010525 File Offset: 0x0000E725
		private void VerifySignature(KeyedHashAlgorithm hash)
		{
			this.Signature.SignedInfo.ComputeHash(hash);
			if (!CryptoHelper.FixedTimeEquals(hash.Hash, this.GetSignatureValue()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("SignatureVerificationFailed")));
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010568 File Offset: 0x0000E768
		private void VerifySignature(HashAlgorithm hash, AsymmetricSignatureDeformatter deformatter, string signatureMethod)
		{
			this.Signature.SignedInfo.ComputeHash(hash);
			bool flag;
			if (SecurityUtils.RequiresFipsCompliance && signatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
			{
				deformatter.SetHashAlgorithm("SHA256");
				flag = deformatter.VerifySignature(hash.Hash, this.GetSignatureValue());
			}
			else
			{
				flag = deformatter.VerifySignature(hash, this.GetSignatureValue());
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("SignatureVerificationFailed")));
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public void StartSignatureVerification(SecurityKey verificationKey)
		{
			string signatureMethod = this.Signature.SignedInfo.SignatureMethod;
			SymmetricSecurityKey symmetricSecurityKey = verificationKey as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				using (KeyedHashAlgorithm keyedHashAlgorithm = symmetricSecurityKey.GetKeyedHashAlgorithm(signatureMethod))
				{
					if (keyedHashAlgorithm == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToCreateKeyedHashAlgorithmFromSymmetricCrypto", new object[]
						{
							signatureMethod,
							symmetricSecurityKey
						})));
					}
					this.VerifySignature(keyedHashAlgorithm);
					return;
				}
			}
			AsymmetricSecurityKey asymmetricSecurityKey = verificationKey as AsymmetricSecurityKey;
			if (asymmetricSecurityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownICryptoType", new object[]
				{
					verificationKey
				})));
			}
			using (HashAlgorithm hashAlgorithmForSignature = asymmetricSecurityKey.GetHashAlgorithmForSignature(signatureMethod))
			{
				if (hashAlgorithmForSignature == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToCreateHashAlgorithmFromAsymmetricCrypto", new object[]
					{
						signatureMethod,
						asymmetricSecurityKey
					})));
				}
				AsymmetricSignatureDeformatter signatureDeformatter = asymmetricSecurityKey.GetSignatureDeformatter(signatureMethod);
				if (signatureDeformatter == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToCreateSignatureDeformatterFromAsymmetricCrypto", new object[]
					{
						signatureMethod,
						asymmetricSecurityKey
					})));
				}
				this.VerifySignature(hashAlgorithmForSignature, signatureDeformatter, signatureMethod);
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010728 File Offset: 0x0000E928
		public void WriteTo(XmlDictionaryWriter writer)
		{
			this.WriteTo(writer, this.dictionaryManager);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010737 File Offset: 0x0000E937
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.signature.WriteTo(writer, dictionaryManager);
		}

		// Token: 0x0400039D RID: 925
		internal const string DefaultPrefix = "";

		// Token: 0x0400039E RID: 926
		private SecurityTokenSerializer tokenSerializer;

		// Token: 0x0400039F RID: 927
		private readonly Signature signature;

		// Token: 0x040003A0 RID: 928
		private TransformFactory transformFactory;

		// Token: 0x040003A1 RID: 929
		private DictionaryManager dictionaryManager;
	}
}
