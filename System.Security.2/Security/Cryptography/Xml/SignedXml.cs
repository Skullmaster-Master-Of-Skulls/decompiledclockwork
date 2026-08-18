using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Win32;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000052 RID: 82
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class SignedXml
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		public SignedXml()
		{
			this.Initialize(null);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C9FA File Offset: 0x0000ABFA
		public SignedXml(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.Initialize(document.DocumentElement);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000CA2E File Offset: 0x0000AC2E
		public SignedXml(XmlElement elem)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			this.Initialize(elem);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000CA60 File Offset: 0x0000AC60
		private void Initialize(XmlElement element)
		{
			this.m_containingDocument = ((element == null) ? null : element.OwnerDocument);
			this.m_context = element;
			this.m_signature = new Signature();
			this.m_signature.SignedXml = this;
			this.m_signature.SignedInfo = new SignedInfo();
			this.m_signingKey = null;
			this.m_safeCanonicalizationMethods = new Collection<string>(SignedXml.KnownCanonicalizationMethods);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public string SigningKeyName
		{
			get
			{
				return this.m_strSigningKeyName;
			}
			set
			{
				this.m_strSigningKeyName = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			set
			{
				this.m_xmlResolver = value;
				this.m_bResolverSet = true;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
		internal bool ResolverSet
		{
			get
			{
				return this.m_bResolverSet;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000CAED File Offset: 0x0000ACED
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000CAF5 File Offset: 0x0000ACF5
		public Func<SignedXml, bool> SignatureFormatValidator
		{
			get
			{
				return this.m_signatureFormatValidator;
			}
			set
			{
				this.m_signatureFormatValidator = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		public Collection<string> SafeCanonicalizationMethods
		{
			get
			{
				return this.m_safeCanonicalizationMethods;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000CB06 File Offset: 0x0000AD06
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		public AsymmetricAlgorithm SigningKey
		{
			get
			{
				return this.m_signingKey;
			}
			set
			{
				this.m_signingKey = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000CB17 File Offset: 0x0000AD17
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000CB38 File Offset: 0x0000AD38
		[ComVisible(false)]
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (this.m_exml == null)
				{
					this.m_exml = new EncryptedXml(this.m_containingDocument);
				}
				return this.m_exml;
			}
			set
			{
				this.m_exml = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000CB41 File Offset: 0x0000AD41
		public Signature Signature
		{
			get
			{
				return this.m_signature;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000CB49 File Offset: 0x0000AD49
		public SignedInfo SignedInfo
		{
			get
			{
				return this.m_signature.SignedInfo;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000CB56 File Offset: 0x0000AD56
		public string SignatureMethod
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureMethod;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public string SignatureLength
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureLength;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000CB7A File Offset: 0x0000AD7A
		public byte[] SignatureValue
		{
			get
			{
				return this.m_signature.SignatureValue;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000CB87 File Offset: 0x0000AD87
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public KeyInfo KeyInfo
		{
			get
			{
				return this.m_signature.KeyInfo;
			}
			set
			{
				this.m_signature.KeyInfo = value;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
		public XmlElement GetXml()
		{
			if (this.m_containingDocument != null)
			{
				return this.m_signature.GetXml(this.m_containingDocument);
			}
			return this.m_signature.GetXml();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CBC9 File Offset: 0x0000ADC9
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_signature.LoadXml(value);
			if (this.m_context == null)
			{
				this.m_context = value;
			}
			this.bCacheValid = false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public void AddReference(Reference reference)
		{
			this.m_signature.SignedInfo.AddReference(reference);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		public void AddObject(DataObject dataObject)
		{
			this.m_signature.AddObject(dataObject);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public bool CheckSignature()
		{
			AsymmetricAlgorithm asymmetricAlgorithm;
			return this.CheckSignatureReturningKey(out asymmetricAlgorithm);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CC34 File Offset: 0x0000AE34
		public bool CheckSignatureReturningKey(out AsymmetricAlgorithm signingKey)
		{
			SignedXmlDebugLog.LogBeginSignatureVerification(this, this.m_context);
			signingKey = null;
			bool flag = false;
			if (!this.CheckSignatureFormat())
			{
				return false;
			}
			AsymmetricAlgorithm publicKey;
			do
			{
				publicKey = this.GetPublicKey();
				if (publicKey != null)
				{
					flag = this.CheckSignature(publicKey);
					SignedXmlDebugLog.LogVerificationResult(this, publicKey, flag);
				}
			}
			while (publicKey != null && !flag);
			signingKey = publicKey;
			return flag;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CC84 File Offset: 0x0000AE84
		public bool CheckSignature(AsymmetricAlgorithm key)
		{
			if (!this.CheckSignatureFormat())
			{
				return false;
			}
			if (!this.CheckSignedInfo(key))
			{
				SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_SignedInfo"));
				return false;
			}
			if (!this.CheckDigestedReferences())
			{
				SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_References"));
				return false;
			}
			SignedXmlDebugLog.LogVerificationResult(this, key, true);
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public bool CheckSignature(KeyedHashAlgorithm macAlg)
		{
			if (!this.CheckSignatureFormat())
			{
				return false;
			}
			if (!this.CheckSignedInfo(macAlg))
			{
				SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_SignedInfo"));
				return false;
			}
			if (!this.CheckDigestedReferences())
			{
				SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_References"));
				return false;
			}
			SignedXmlDebugLog.LogVerificationResult(this, macAlg, true);
			return true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CD34 File Offset: 0x0000AF34
		[ComVisible(false)]
		[SecuritySafeCritical]
		public bool CheckSignature(X509Certificate2 certificate, bool verifySignatureOnly)
		{
			if (!verifySignatureOnly)
			{
				foreach (X509Extension x509Extension in certificate.Extensions)
				{
					if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
					{
						X509KeyUsageExtension x509KeyUsageExtension = new X509KeyUsageExtension();
						x509KeyUsageExtension.CopyFrom(x509Extension);
						SignedXmlDebugLog.LogVerifyKeyUsage(this, certificate, x509KeyUsageExtension);
						if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) <= X509KeyUsageFlags.None)
						{
							SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_X509KeyUsage"));
							return false;
						}
						break;
					}
				}
				X509Chain x509Chain = new X509Chain();
				x509Chain.ChainPolicy.ExtraStore.AddRange(this.BuildBagOfCerts());
				bool flag = x509Chain.Build(certificate);
				SignedXmlDebugLog.LogVerifyX509Chain(this, x509Chain, certificate);
				if (!flag)
				{
					SignedXmlDebugLog.LogVerificationFailure(this, SecurityResources.GetResourceString("Log_VerificationFailed_X509Chain"));
					return false;
				}
			}
			if (!this.CheckSignature(certificate.GetAnyPublicKey()))
			{
				return false;
			}
			SignedXmlDebugLog.LogVerificationResult(this, certificate, true);
			return true;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CE28 File Offset: 0x0000B028
		public void ComputeSignature()
		{
			SignedXmlDebugLog.LogBeginSignatureComputation(this, this.m_context);
			this.BuildDigestedReferences();
			AsymmetricAlgorithm signingKey = this.SigningKey;
			if (signingKey == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_LoadKeyFailed"));
			}
			if (this.SignedInfo.SignatureMethod == null)
			{
				if (signingKey is DSA)
				{
					this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
				}
				else
				{
					if (!(signingKey is RSA))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CreatedKeyFailed"));
					}
					if (this.SignedInfo.SignatureMethod == null)
					{
						this.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSADefault;
					}
				}
			}
			SignatureDescription signatureDescription = Utils.CreateFromName<SignatureDescription>(this.SignedInfo.SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureDescriptionNotCreated"));
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CreateHashAlgorithmFailed"));
			}
			byte[] c14NDigest = this.GetC14NDigest(hashAlgorithm);
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = signatureDescription.CreateFormatter(signingKey);
			SignedXmlDebugLog.LogSigning(this, signingKey, signatureDescription, hashAlgorithm, asymmetricSignatureFormatter);
			this.m_signature.SignatureValue = asymmetricSignatureFormatter.CreateSignature(hashAlgorithm);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public void ComputeSignature(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			HMAC hmac = macAlg as HMAC;
			if (hmac == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureMethodKeyMismatch"));
			}
			int num;
			if (this.m_signature.SignedInfo.SignatureLength == null)
			{
				num = hmac.HashSize;
			}
			else
			{
				num = Convert.ToInt32(this.m_signature.SignedInfo.SignatureLength, null);
			}
			if (num < 0 || num > hmac.HashSize)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidSignatureLength"));
			}
			if (num % 8 != 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidSignatureLength2"));
			}
			this.BuildDigestedReferences();
			string hashName = hmac.HashName;
			if (!(hashName == "SHA1"))
			{
				if (!(hashName == "SHA256"))
				{
					if (!(hashName == "SHA384"))
					{
						if (!(hashName == "SHA512"))
						{
							if (!(hashName == "MD5"))
							{
								if (!(hashName == "RIPEMD160"))
								{
									throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureMethodKeyMismatch"));
								}
								this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
							}
							else
							{
								this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";
							}
						}
						else
						{
							this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
						}
					}
					else
					{
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
					}
				}
				else
				{
					this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
				}
			}
			else
			{
				this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
			}
			byte[] c14NDigest = this.GetC14NDigest(hmac);
			SignedXmlDebugLog.LogSigning(this, hmac);
			this.m_signature.SignatureValue = new byte[num / 8];
			Buffer.BlockCopy(c14NDigest, 0, this.m_signature.SignatureValue, 0, num / 8);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		protected virtual AsymmetricAlgorithm GetPublicKey()
		{
			if (this.KeyInfo == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_KeyInfoRequired"));
			}
			if (this.m_x509Enum != null)
			{
				AsymmetricAlgorithm nextCertificatePublicKey = this.GetNextCertificatePublicKey();
				if (nextCertificatePublicKey != null)
				{
					return nextCertificatePublicKey;
				}
			}
			if (this.m_keyInfoEnum == null)
			{
				this.m_keyInfoEnum = this.KeyInfo.GetEnumerator();
			}
			while (this.m_keyInfoEnum.MoveNext())
			{
				RSAKeyValue rsakeyValue = this.m_keyInfoEnum.Current as RSAKeyValue;
				if (rsakeyValue != null)
				{
					return rsakeyValue.Key;
				}
				DSAKeyValue dsakeyValue = this.m_keyInfoEnum.Current as DSAKeyValue;
				if (dsakeyValue != null)
				{
					return dsakeyValue.Key;
				}
				KeyInfoX509Data keyInfoX509Data = this.m_keyInfoEnum.Current as KeyInfoX509Data;
				if (keyInfoX509Data != null)
				{
					this.m_x509Collection = Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Verification);
					if (this.m_x509Collection.Count > 0)
					{
						this.m_x509Enum = this.m_x509Collection.GetEnumerator();
						AsymmetricAlgorithm nextCertificatePublicKey2 = this.GetNextCertificatePublicKey();
						if (nextCertificatePublicKey2 != null)
						{
							return nextCertificatePublicKey2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		private X509Certificate2Collection BuildBagOfCerts()
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			if (this.KeyInfo != null)
			{
				foreach (object obj in this.KeyInfo)
				{
					KeyInfoClause keyInfoClause = (KeyInfoClause)obj;
					KeyInfoX509Data keyInfoX509Data = keyInfoClause as KeyInfoX509Data;
					if (keyInfoX509Data != null)
					{
						x509Certificate2Collection.AddRange(Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Verification));
					}
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D244 File Offset: 0x0000B444
		private AsymmetricAlgorithm GetNextCertificatePublicKey()
		{
			while (this.m_x509Enum.MoveNext())
			{
				X509Certificate2 x509Certificate = (X509Certificate2)this.m_x509Enum.Current;
				if (x509Certificate != null)
				{
					if (!LocalAppContextSwitches.SignedXmlUseLegacyCertificatePrivateKey)
					{
						return x509Certificate.GetAnyPublicKey();
					}
					return x509Certificate.PublicKey.Key;
				}
			}
			return null;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008A8F File Offset: 0x00006C8F
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D290 File Offset: 0x0000B490
		internal static XmlElement DefaultGetIdElement(XmlDocument document, string idValue)
		{
			if (document == null)
			{
				return null;
			}
			if (Utils.RequireNCNameIdentifier())
			{
				try
				{
					XmlConvert.VerifyNCName(idValue);
				}
				catch (XmlException)
				{
					return null;
				}
			}
			XmlElement xmlElement = document.GetElementById(idValue);
			if (xmlElement != null)
			{
				if (!Utils.AllowAmbiguousReferenceTargets())
				{
					XmlDocument xmlDocument = (XmlDocument)document.CloneNode(true);
					XmlElement elementById = xmlDocument.GetElementById(idValue);
					if (elementById != null)
					{
						elementById.Attributes.RemoveAll();
						XmlElement elementById2 = xmlDocument.GetElementById(idValue);
						if (elementById2 != null)
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
						}
					}
				}
				return xmlElement;
			}
			xmlElement = SignedXml.GetSingleReferenceTarget(document, "Id", idValue);
			if (xmlElement != null)
			{
				return xmlElement;
			}
			xmlElement = SignedXml.GetSingleReferenceTarget(document, "id", idValue);
			if (xmlElement != null)
			{
				return xmlElement;
			}
			return SignedXml.GetSingleReferenceTarget(document, "ID", idValue);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000D354 File Offset: 0x0000B554
		private static bool DefaultSignatureFormatValidator(SignedXml signedXml)
		{
			return !signedXml.DoesSignatureUseTruncatedHmac() && signedXml.DoesSignatureUseSafeCanonicalizationMethod();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D36C File Offset: 0x0000B56C
		private bool DoesSignatureUseTruncatedHmac()
		{
			if (this.SignedInfo.SignatureLength == null)
			{
				return false;
			}
			HMAC hmac = Utils.CreateFromName<HMAC>(this.SignatureMethod);
			if (hmac == null)
			{
				return false;
			}
			int num = 0;
			return !int.TryParse(this.SignedInfo.SignatureLength, out num) || num != hmac.HashSize;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		private bool DoesSignatureUseSafeCanonicalizationMethod()
		{
			foreach (string a in this.SafeCanonicalizationMethods)
			{
				if (string.Equals(a, this.SignedInfo.CanonicalizationMethod, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			SignedXmlDebugLog.LogUnsafeCanonicalizationMethod(this, this.SignedInfo.CanonicalizationMethod, this.SafeCanonicalizationMethods);
			return false;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000D438 File Offset: 0x0000B638
		private bool ReferenceUsesSafeTransformMethods(Reference reference)
		{
			TransformChain transformChain = reference.TransformChain;
			int count = transformChain.Count;
			for (int i = 0; i < count; i++)
			{
				Transform transform = transformChain[i];
				if (!this.IsSafeTransform(transform.Algorithm))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000D478 File Offset: 0x0000B678
		private bool IsSafeTransform(string transformAlgorithm)
		{
			foreach (string a in this.SafeCanonicalizationMethods)
			{
				if (string.Equals(a, transformAlgorithm, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			foreach (string a2 in SignedXml.DefaultSafeTransformMethods)
			{
				if (string.Equals(a2, transformAlgorithm, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			SignedXmlDebugLog.LogUnsafeTransformMethod(this, transformAlgorithm, this.SafeCanonicalizationMethods, SignedXml.DefaultSafeTransformMethods);
			return false;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000D528 File Offset: 0x0000B728
		private static IList<string> KnownCanonicalizationMethods
		{
			get
			{
				if (SignedXml.s_knownCanonicalizationMethods == null)
				{
					List<string> list = SignedXml.ReadAdditionalSafeCanonicalizationMethods();
					list.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
					list.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments");
					list.Add("http://www.w3.org/2001/10/xml-exc-c14n#");
					list.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments");
					SignedXml.s_knownCanonicalizationMethods = list;
				}
				return SignedXml.s_knownCanonicalizationMethods;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000D57C File Offset: 0x0000B77C
		private static IList<string> DefaultSafeTransformMethods
		{
			get
			{
				if (SignedXml.s_defaultSafeTransformMethods == null)
				{
					List<string> list = SignedXml.ReadAdditionalSafeTransformMethods();
					list.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature");
					list.Add("http://www.w3.org/2000/09/xmldsig#base64");
					list.Add("urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform");
					list.Add("http://www.w3.org/2002/07/decrypt#XML");
					SignedXml.s_defaultSafeTransformMethods = list;
				}
				return SignedXml.s_defaultSafeTransformMethods;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D5CD File Offset: 0x0000B7CD
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static List<string> ReadAdditionalSafeCanonicalizationMethods()
		{
			return SignedXml.ReadFxSecurityStringValues("SafeCanonicalizationMethods");
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000D5D9 File Offset: 0x0000B7D9
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static List<string> ReadAdditionalSafeTransformMethods()
		{
			return SignedXml.ReadFxSecurityStringValues("SafeTransformMethods");
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		private static List<string> ReadFxSecurityStringValues(string subkey)
		{
			List<string> list = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\Security\\" + subkey, false))
				{
					if (registryKey != null)
					{
						foreach (string name in registryKey.GetValueNames())
						{
							if (registryKey.GetValueKind(name) == RegistryValueKind.String)
							{
								string text = registryKey.GetValue(name) as string;
								if (!string.IsNullOrWhiteSpace(text))
								{
									list.Add(text);
								}
							}
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return list;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000D688 File Offset: 0x0000B888
		private byte[] GetC14NDigest(HashAlgorithm hash)
		{
			if (!this.bCacheValid || !this.SignedInfo.CacheValid)
			{
				string text = (this.m_containingDocument == null) ? null : this.m_containingDocument.BaseURI;
				XmlResolver xmlResolver = this.m_bResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text);
				XmlDocument xmlDocument = Utils.PreProcessElementInput(this.SignedInfo.GetXml(), xmlResolver, text);
				CanonicalXmlNodeList namespaces = (this.m_context == null) ? null : Utils.GetPropagatedAttributes(this.m_context);
				SignedXmlDebugLog.LogNamespacePropagation(this, namespaces);
				Utils.AddNamespaces(xmlDocument.DocumentElement, namespaces);
				Transform canonicalizationMethodObject = this.SignedInfo.CanonicalizationMethodObject;
				canonicalizationMethodObject.Resolver = xmlResolver;
				canonicalizationMethodObject.BaseURI = text;
				SignedXmlDebugLog.LogBeginCanonicalization(this, canonicalizationMethodObject);
				canonicalizationMethodObject.LoadInput(xmlDocument);
				SignedXmlDebugLog.LogCanonicalizedOutput(this, canonicalizationMethodObject);
				this._digestedSignedInfo = canonicalizationMethodObject.GetDigestedOutput(hash);
				this.bCacheValid = true;
			}
			return this._digestedSignedInfo;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000D770 File Offset: 0x0000B970
		private int GetReferenceLevel(int index, ArrayList references)
		{
			if (this.m_refProcessed[index])
			{
				return this.m_refLevelCache[index];
			}
			this.m_refProcessed[index] = true;
			Reference reference = (Reference)references[index];
			if (reference.Uri == null || reference.Uri.Length == 0 || (reference.Uri.Length > 0 && reference.Uri[0] != '#'))
			{
				this.m_refLevelCache[index] = 0;
				return 0;
			}
			if (reference.Uri.Length <= 0 || reference.Uri[0] != '#')
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
			}
			string text = Utils.ExtractIdFromLocalUri(reference.Uri);
			if (text == "xpointer(/)")
			{
				this.m_refLevelCache[index] = 0;
				return 0;
			}
			for (int i = 0; i < references.Count; i++)
			{
				if (((Reference)references[i]).Id == text)
				{
					this.m_refLevelCache[index] = this.GetReferenceLevel(i, references) + 1;
					return this.m_refLevelCache[index];
				}
			}
			this.m_refLevelCache[index] = 0;
			return 0;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D888 File Offset: 0x0000BA88
		private void BuildDigestedReferences()
		{
			ArrayList references = this.SignedInfo.References;
			this.m_refProcessed = new bool[references.Count];
			this.m_refLevelCache = new int[references.Count];
			SignedXml.ReferenceLevelSortOrder referenceLevelSortOrder = new SignedXml.ReferenceLevelSortOrder();
			referenceLevelSortOrder.References = references;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in references)
			{
				Reference value = (Reference)obj;
				arrayList.Add(value);
			}
			arrayList.Sort(referenceLevelSortOrder);
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			foreach (object obj2 in this.m_signature.ObjectList)
			{
				DataObject dataObject = (DataObject)obj2;
				canonicalXmlNodeList.Add(dataObject.GetXml());
			}
			foreach (object obj3 in arrayList)
			{
				Reference reference = (Reference)obj3;
				if (reference.DigestMethod == null)
				{
					reference.DigestMethod = SignedXml.XmlDsigDigestDefault;
				}
				SignedXmlDebugLog.LogSigningReference(this, reference);
				reference.UpdateHashValue(this.m_containingDocument, canonicalXmlNodeList);
				if (reference.Id != null)
				{
					canonicalXmlNodeList.Add(reference.GetXml());
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000DA18 File Offset: 0x0000BC18
		private bool CheckDigestedReferences()
		{
			ArrayList references = this.m_signature.SignedInfo.References;
			for (int i = 0; i < references.Count; i++)
			{
				Reference reference = (Reference)references[i];
				if (!this.ReferenceUsesSafeTransformMethods(reference))
				{
					return false;
				}
				SignedXmlDebugLog.LogVerifyReference(this, reference);
				byte[] array = null;
				try
				{
					array = reference.CalculateHashValue(this.m_containingDocument, this.m_signature.ReferencedItems);
				}
				catch (CryptoSignedXmlRecursionException)
				{
					SignedXmlDebugLog.LogSignedXmlRecursionLimit(this, reference);
					return false;
				}
				SignedXmlDebugLog.LogVerifyReferenceHash(this, reference, array, reference.DigestValue);
				if (!SignedXml.CryptographicEquals(array, reference.DigestValue, array.Length))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static bool CryptographicEquals(byte[] a, byte[] b, int count)
		{
			int num = 0;
			if (a.Length < count || b.Length < count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				num |= (int)(a[i] - b[i]);
			}
			return num == 0;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		private bool CheckSignatureFormat()
		{
			if (this.m_signatureFormatValidator == null)
			{
				return true;
			}
			SignedXmlDebugLog.LogBeginCheckSignatureFormat(this, this.m_signatureFormatValidator);
			bool result = this.m_signatureFormatValidator(this);
			SignedXmlDebugLog.LogFormatValidationResult(this, result);
			return result;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000DB34 File Offset: 0x0000BD34
		private bool CheckSignedInfo(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SignedXmlDebugLog.LogBeginCheckSignedInfo(this, this.m_signature.SignedInfo);
			SignatureDescription signatureDescription = Utils.CreateFromName<SignatureDescription>(this.SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureDescriptionNotCreated"));
			}
			Type type = Type.GetType(signatureDescription.KeyAlgorithm);
			if (!SignedXml.IsKeyTheCorrectAlgorithm(key, type))
			{
				return false;
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CreateHashAlgorithmFailed"));
			}
			byte[] c14NDigest = this.GetC14NDigest(hashAlgorithm);
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = signatureDescription.CreateDeformatter(key);
			SignedXmlDebugLog.LogVerifySignedInfo(this, key, signatureDescription, hashAlgorithm, asymmetricSignatureDeformatter, c14NDigest, this.m_signature.SignatureValue);
			return asymmetricSignatureDeformatter.VerifySignature(c14NDigest, this.m_signature.SignatureValue);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		private bool CheckSignedInfo(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
			SignedXmlDebugLog.LogBeginCheckSignedInfo(this, this.m_signature.SignedInfo);
			int num;
			if (this.m_signature.SignedInfo.SignatureLength == null)
			{
				num = macAlg.HashSize;
			}
			else
			{
				num = Convert.ToInt32(this.m_signature.SignedInfo.SignatureLength, null);
			}
			if (num < 0 || num > macAlg.HashSize)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidSignatureLength"));
			}
			if (num % 8 != 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidSignatureLength2"));
			}
			if (this.m_signature.SignatureValue == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureValueRequired"));
			}
			if (this.m_signature.SignatureValue.Length != num / 8)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidSignatureLength"));
			}
			byte[] c14NDigest = this.GetC14NDigest(macAlg);
			SignedXmlDebugLog.LogVerifySignedInfo(this, macAlg, c14NDigest, this.m_signature.SignatureValue);
			if (Utils.GetAllowUnsafeTruncatedHmacSignatureVerification())
			{
				for (int i = 0; i < this.m_signature.SignatureValue.Length; i++)
				{
					if (this.m_signature.SignatureValue[i] != c14NDigest[i])
					{
						return false;
					}
				}
				return true;
			}
			return SignedXml.CryptographicEquals(this.m_signature.SignatureValue, c14NDigest, c14NDigest.Length);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DD20 File Offset: 0x0000BF20
		private static XmlElement GetSingleReferenceTarget(XmlDocument document, string idAttributeName, string idValue)
		{
			string xpath = string.Concat(new string[]
			{
				"//*[@",
				idAttributeName,
				"=\"",
				idValue,
				"\"]"
			});
			if (Utils.AllowAmbiguousReferenceTargets())
			{
				return document.SelectSingleNode(xpath) as XmlElement;
			}
			XmlNodeList xmlNodeList = document.SelectNodes(xpath);
			if (xmlNodeList == null || xmlNodeList.Count == 0)
			{
				return null;
			}
			if (xmlNodeList.Count == 1)
			{
				return xmlNodeList[0] as XmlElement;
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		private static bool IsKeyTheCorrectAlgorithm(AsymmetricAlgorithm key, Type expectedType)
		{
			Type type = key.GetType();
			if (type == expectedType)
			{
				return true;
			}
			if (expectedType.IsSubclassOf(type))
			{
				return true;
			}
			while (expectedType != null && expectedType.BaseType != typeof(AsymmetricAlgorithm))
			{
				expectedType = expectedType.BaseType;
			}
			return !(expectedType == null) && type.IsSubclassOf(expectedType);
		}

		// Token: 0x04000419 RID: 1049
		protected Signature m_signature;

		// Token: 0x0400041A RID: 1050
		protected string m_strSigningKeyName;

		// Token: 0x0400041B RID: 1051
		private AsymmetricAlgorithm m_signingKey;

		// Token: 0x0400041C RID: 1052
		private XmlDocument m_containingDocument;

		// Token: 0x0400041D RID: 1053
		private IEnumerator m_keyInfoEnum;

		// Token: 0x0400041E RID: 1054
		private X509Certificate2Collection m_x509Collection;

		// Token: 0x0400041F RID: 1055
		private IEnumerator m_x509Enum;

		// Token: 0x04000420 RID: 1056
		private bool[] m_refProcessed;

		// Token: 0x04000421 RID: 1057
		private int[] m_refLevelCache;

		// Token: 0x04000422 RID: 1058
		internal XmlResolver m_xmlResolver;

		// Token: 0x04000423 RID: 1059
		internal XmlElement m_context;

		// Token: 0x04000424 RID: 1060
		private bool m_bResolverSet;

		// Token: 0x04000425 RID: 1061
		private Func<SignedXml, bool> m_signatureFormatValidator = new Func<SignedXml, bool>(SignedXml.DefaultSignatureFormatValidator);

		// Token: 0x04000426 RID: 1062
		private Collection<string> m_safeCanonicalizationMethods;

		// Token: 0x04000427 RID: 1063
		private static IList<string> s_knownCanonicalizationMethods = null;

		// Token: 0x04000428 RID: 1064
		private static IList<string> s_defaultSafeTransformMethods = null;

		// Token: 0x04000429 RID: 1065
		private const string XmlDsigMoreHMACMD5Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";

		// Token: 0x0400042A RID: 1066
		private const string XmlDsigMoreHMACSHA256Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x0400042B RID: 1067
		private const string XmlDsigMoreHMACSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x0400042C RID: 1068
		private const string XmlDsigMoreHMACSHA512Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x0400042D RID: 1069
		private const string XmlDsigMoreHMACRIPEMD160Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x0400042E RID: 1070
		private EncryptedXml m_exml;

		// Token: 0x0400042F RID: 1071
		public const string XmlDsigNamespaceUrl = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000430 RID: 1072
		public const string XmlDsigMinimalCanonicalizationUrl = "http://www.w3.org/2000/09/xmldsig#minimal";

		// Token: 0x04000431 RID: 1073
		public const string XmlDsigCanonicalizationUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04000432 RID: 1074
		public const string XmlDsigCanonicalizationWithCommentsUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x04000433 RID: 1075
		public const string XmlDsigSHA1Url = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04000434 RID: 1076
		public const string XmlDsigDSAUrl = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04000435 RID: 1077
		public const string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04000436 RID: 1078
		public const string XmlDsigHMACSHA1Url = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x04000437 RID: 1079
		public const string XmlDsigSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04000438 RID: 1080
		public const string XmlDsigRSASHA256Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04000439 RID: 1081
		public const string XmlDsigSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x0400043A RID: 1082
		public const string XmlDsigRSASHA384Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x0400043B RID: 1083
		public const string XmlDsigSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x0400043C RID: 1084
		public const string XmlDsigRSASHA512Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		// Token: 0x0400043D RID: 1085
		internal static readonly string XmlDsigDigestDefault = LocalAppContextSwitches.XmlUseInsecureHashAlgorithms ? "http://www.w3.org/2000/09/xmldsig#sha1" : "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x0400043E RID: 1086
		internal static readonly string XmlDsigRSADefault = LocalAppContextSwitches.XmlUseInsecureHashAlgorithms ? "http://www.w3.org/2000/09/xmldsig#rsa-sha1" : "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x0400043F RID: 1087
		public const string XmlDsigC14NTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04000440 RID: 1088
		public const string XmlDsigC14NWithCommentsTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x04000441 RID: 1089
		public const string XmlDsigExcC14NTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000442 RID: 1090
		public const string XmlDsigExcC14NWithCommentsTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000443 RID: 1091
		public const string XmlDsigBase64TransformUrl = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x04000444 RID: 1092
		public const string XmlDsigXPathTransformUrl = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x04000445 RID: 1093
		public const string XmlDsigXsltTransformUrl = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x04000446 RID: 1094
		public const string XmlDsigEnvelopedSignatureTransformUrl = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x04000447 RID: 1095
		public const string XmlDecryptionTransformUrl = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x04000448 RID: 1096
		public const string XmlLicenseTransformUrl = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";

		// Token: 0x04000449 RID: 1097
		private bool bCacheValid;

		// Token: 0x0400044A RID: 1098
		private byte[] _digestedSignedInfo;

		// Token: 0x020000DB RID: 219
		private class ReferenceLevelSortOrder : IComparer
		{
			// Token: 0x1700011D RID: 285
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001BFE2 File Offset: 0x0001A1E2
			// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001BFEA File Offset: 0x0001A1EA
			public ArrayList References
			{
				get
				{
					return this.m_references;
				}
				set
				{
					this.m_references = value;
				}
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
			public int Compare(object a, object b)
			{
				Reference reference = a as Reference;
				Reference reference2 = b as Reference;
				int index = 0;
				int index2 = 0;
				int num = 0;
				foreach (object obj in this.References)
				{
					Reference reference3 = (Reference)obj;
					if (reference3 == reference)
					{
						index = num;
					}
					if (reference3 == reference2)
					{
						index2 = num;
					}
					num++;
				}
				int referenceLevel = reference.SignedXml.GetReferenceLevel(index, this.References);
				int referenceLevel2 = reference2.SignedXml.GetReferenceLevel(index2, this.References);
				return referenceLevel.CompareTo(referenceLevel2);
			}

			// Token: 0x0400066C RID: 1644
			private ArrayList m_references;
		}
	}
}
