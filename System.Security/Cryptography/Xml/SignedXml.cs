using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Win32;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A9 RID: 169
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class SignedXml
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00012705 File Offset: 0x00011705
		public SignedXml()
		{
			this.Initialize(null);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012714 File Offset: 0x00011714
		public SignedXml(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.Initialize(document.DocumentElement);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012736 File Offset: 0x00011736
		public SignedXml(XmlElement elem)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			this.Initialize(elem);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012754 File Offset: 0x00011754
		private void Initialize(XmlElement element)
		{
			this.m_containingDocument = ((element == null) ? null : element.OwnerDocument);
			this.m_context = element;
			this.m_signature = new Signature();
			this.m_signature.SignedXml = this;
			this.m_signature.SignedInfo = new SignedInfo();
			this.m_signingKey = null;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000127A8 File Offset: 0x000117A8
		// (set) Token: 0x06000385 RID: 901 RVA: 0x000127B0 File Offset: 0x000117B0
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

		// Token: 0x170000A9 RID: 169
		// (set) Token: 0x06000386 RID: 902 RVA: 0x000127B9 File Offset: 0x000117B9
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			set
			{
				this.m_xmlResolver = value;
				this.m_bResolverSet = true;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000127C9 File Offset: 0x000117C9
		internal bool ResolverSet
		{
			get
			{
				return this.m_bResolverSet;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000127D1 File Offset: 0x000117D1
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000127D9 File Offset: 0x000117D9
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000127E2 File Offset: 0x000117E2
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00012803 File Offset: 0x00011803
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0001280C File Offset: 0x0001180C
		public Signature Signature
		{
			get
			{
				return this.m_signature;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00012814 File Offset: 0x00011814
		public SignedInfo SignedInfo
		{
			get
			{
				return this.m_signature.SignedInfo;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00012821 File Offset: 0x00011821
		public string SignatureMethod
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureMethod;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00012833 File Offset: 0x00011833
		public string SignatureLength
		{
			get
			{
				return this.m_signature.SignedInfo.SignatureLength;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00012845 File Offset: 0x00011845
		public byte[] SignatureValue
		{
			get
			{
				return this.m_signature.SignatureValue;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00012852 File Offset: 0x00011852
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0001285F File Offset: 0x0001185F
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

		// Token: 0x06000393 RID: 915 RVA: 0x0001286D File Offset: 0x0001186D
		public XmlElement GetXml()
		{
			if (this.m_containingDocument != null)
			{
				return this.m_signature.GetXml(this.m_containingDocument);
			}
			return this.m_signature.GetXml();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00012894 File Offset: 0x00011894
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_signature.LoadXml(value);
			this.m_context = value;
			this.bCacheValid = false;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000128BE File Offset: 0x000118BE
		public void AddReference(Reference reference)
		{
			this.m_signature.SignedInfo.AddReference(reference);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000128D1 File Offset: 0x000118D1
		public void AddObject(DataObject dataObject)
		{
			this.m_signature.AddObject(dataObject);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000128E0 File Offset: 0x000118E0
		public bool CheckSignature()
		{
			bool flag = false;
			AsymmetricAlgorithm publicKey;
			do
			{
				publicKey = this.GetPublicKey();
				if (publicKey != null)
				{
					flag = this.CheckSignature(publicKey);
				}
			}
			while (publicKey != null && !flag);
			return flag;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00012908 File Offset: 0x00011908
		public bool CheckSignatureReturningKey(out AsymmetricAlgorithm signingKey)
		{
			bool flag = false;
			AsymmetricAlgorithm publicKey;
			do
			{
				publicKey = this.GetPublicKey();
				if (publicKey != null)
				{
					flag = this.CheckSignature(publicKey);
				}
			}
			while (publicKey != null && !flag);
			signingKey = publicKey;
			return flag;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00012935 File Offset: 0x00011935
		public bool CheckSignature(AsymmetricAlgorithm key)
		{
			return SignedXml.DefaultSignatureFormatValidator(this) && this.CheckSignedInfo(key) && this.CheckDigestedReferences();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00012952 File Offset: 0x00011952
		public bool CheckSignature(KeyedHashAlgorithm macAlg)
		{
			return SignedXml.DefaultSignatureFormatValidator(this) && this.CheckSignedInfo(macAlg) && this.CheckDigestedReferences();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00012970 File Offset: 0x00011970
		[ComVisible(false)]
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
						if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.None)
						{
							return false;
						}
						break;
					}
				}
				X509Chain x509Chain = new X509Chain();
				x509Chain.ChainPolicy.ExtraStore.AddRange(this.BuildBagOfCerts());
				if (!x509Chain.Build(certificate))
				{
					return false;
				}
			}
			return SignedXml.DefaultSignatureFormatValidator(this) && this.CheckSignedInfo(certificate.PublicKey.Key) && this.CheckDigestedReferences();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00012A3C File Offset: 0x00011A3C
		public void ComputeSignature()
		{
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
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
					}
				}
			}
			SignatureDescription signatureDescription = this.CreateSignatureDescriptionFromName(this.SignedInfo.SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureDescriptionNotCreated"));
			}
			HashAlgorithm hashAlgorithm = signatureDescription.CreateDigest();
			if (hashAlgorithm == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CreateHashAlgorithmFailed"));
			}
			this.GetC14NDigest(hashAlgorithm);
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = signatureDescription.CreateFormatter(signingKey);
			this.m_signature.SignatureValue = asymmetricSignatureFormatter.CreateSignature(hashAlgorithm);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012B28 File Offset: 0x00011B28
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
			string hashName;
			if ((hashName = hmac.HashName) != null)
			{
				if (<PrivateImplementationDetails>{80398E67-FB28-4E58-AF6D-2852A02E717C}.$$method0x6000399-1 == null)
				{
					<PrivateImplementationDetails>{80398E67-FB28-4E58-AF6D-2852A02E717C}.$$method0x6000399-1 = new Dictionary<string, int>(6)
					{
						{
							"SHA1",
							0
						},
						{
							"SHA256",
							1
						},
						{
							"SHA384",
							2
						},
						{
							"SHA512",
							3
						},
						{
							"MD5",
							4
						},
						{
							"RIPEMD160",
							5
						}
					};
				}
				int num2;
				if (<PrivateImplementationDetails>{80398E67-FB28-4E58-AF6D-2852A02E717C}.$$method0x6000399-1.TryGetValue(hashName, out num2))
				{
					switch (num2)
					{
					case 0:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
						break;
					case 1:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
						break;
					case 2:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
						break;
					case 3:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";
						break;
					case 4:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";
						break;
					case 5:
						this.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";
						break;
					default:
						goto IL_19E;
					}
					byte[] c14NDigest = this.GetC14NDigest(hmac);
					this.m_signature.SignatureValue = new byte[num / 8];
					Buffer.BlockCopy(c14NDigest, 0, this.m_signature.SignatureValue, 0, num / 8);
					return;
				}
			}
			IL_19E:
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureMethodKeyMismatch"));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00012D14 File Offset: 0x00011D14
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

		// Token: 0x0600039F RID: 927 RVA: 0x00012E04 File Offset: 0x00011E04
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

		// Token: 0x060003A0 RID: 928 RVA: 0x00012E80 File Offset: 0x00011E80
		private AsymmetricAlgorithm GetNextCertificatePublicKey()
		{
			while (this.m_x509Enum.MoveNext())
			{
				X509Certificate2 x509Certificate = (X509Certificate2)this.m_x509Enum.Current;
				if (x509Certificate != null)
				{
					return x509Certificate.PublicKey.Key;
				}
			}
			return null;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00012EBD File Offset: 0x00011EBD
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00012EC8 File Offset: 0x00011EC8
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

		// Token: 0x060003A3 RID: 931 RVA: 0x00012F8C File Offset: 0x00011F8C
		private byte[] GetC14NDigest(HashAlgorithm hash)
		{
			if (!this.bCacheValid || !this.SignedInfo.CacheValid)
			{
				string text = (this.m_containingDocument == null) ? null : this.m_containingDocument.BaseURI;
				XmlResolver xmlResolver = this.m_bResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text);
				XmlDocument xmlDocument = Utils.PreProcessElementInput(this.SignedInfo.GetXml(), xmlResolver, text);
				CanonicalXmlNodeList namespaces = (this.m_context == null) ? null : Utils.GetPropagatedAttributes(this.m_context);
				Utils.AddNamespaces(xmlDocument.DocumentElement, namespaces);
				Transform canonicalizationMethodObject = this.SignedInfo.CanonicalizationMethodObject;
				canonicalizationMethodObject.Resolver = xmlResolver;
				canonicalizationMethodObject.BaseURI = text;
				canonicalizationMethodObject.LoadInput(xmlDocument);
				this._digestedSignedInfo = canonicalizationMethodObject.GetDigestedOutput(hash);
				this.bCacheValid = true;
			}
			return this._digestedSignedInfo;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001305C File Offset: 0x0001205C
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

		// Token: 0x060003A5 RID: 933 RVA: 0x00013174 File Offset: 0x00012174
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
					reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
				}
				reference.UpdateHashValue(this.m_containingDocument, canonicalXmlNodeList);
				if (reference.Id != null)
				{
					canonicalXmlNodeList.Add(reference.GetXml());
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000132FC File Offset: 0x000122FC
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
				byte[] a = null;
				try
				{
					a = reference.CalculateHashValue(this.m_containingDocument, this.m_signature.ReferencedItems);
				}
				catch (CryptoSignedXmlRecursionException)
				{
					return false;
				}
				if (!SignedXml.CryptographicEquals(a, reference.DigestValue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001338C File Offset: 0x0001238C
		private static bool CryptographicEquals(byte[] a, byte[] b)
		{
			int num = 0;
			if (a.Length != b.Length)
			{
				return false;
			}
			int num2 = a.Length;
			for (int i = 0; i < num2; i++)
			{
				num |= (int)(a[i] - b[i]);
			}
			return 0 == num;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000133C4 File Offset: 0x000123C4
		private bool CheckSignedInfo(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SignatureDescription signatureDescription = this.CreateSignatureDescriptionFromName(this.SignatureMethod);
			if (signatureDescription == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureDescriptionNotCreated"));
			}
			Type type = Type.GetType(signatureDescription.KeyAlgorithm);
			Type type2 = key.GetType();
			if (type != type2 && !type.IsSubclassOf(type2) && !type2.IsSubclassOf(type))
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
			return asymmetricSignatureDeformatter.VerifySignature(c14NDigest, this.m_signature.SignatureValue);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001346C File Offset: 0x0001246C
		private bool CheckSignedInfo(KeyedHashAlgorithm macAlg)
		{
			if (macAlg == null)
			{
				throw new ArgumentNullException("macAlg");
			}
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
			return SignedXml.CryptographicEquals(this.m_signature.SignatureValue, c14NDigest);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00013578 File Offset: 0x00012578
		private static bool AllowHmacTruncation
		{
			get
			{
				if (SignedXml.s_allowHmacTruncation == null)
				{
					SignedXml.s_allowHmacTruncation = new bool?(SignedXml.ReadHmacTruncationSetting());
				}
				return SignedXml.s_allowHmacTruncation.Value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000135A0 File Offset: 0x000125A0
		private static IList<string> SafeCanonicalizationMethods
		{
			get
			{
				if (SignedXml.s_safeCanonicalizationMethods == null)
				{
					List<string> list = SignedXml.ReadAdditionalSafeCanonicalizationMethods();
					list.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
					list.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments");
					list.Add("http://www.w3.org/2001/10/xml-exc-c14n#");
					list.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments");
					SignedXml.s_safeCanonicalizationMethods = list;
				}
				return SignedXml.s_safeCanonicalizationMethods;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000135F4 File Offset: 0x000125F4
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

		// Token: 0x060003AD RID: 941 RVA: 0x00013645 File Offset: 0x00012645
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static List<string> ReadAdditionalSafeCanonicalizationMethods()
		{
			return SignedXml.ReadFxSecurityStringValues("SafeCanonicalizationMethods");
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00013651 File Offset: 0x00012651
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static List<string> ReadAdditionalSafeTransformMethods()
		{
			return SignedXml.ReadFxSecurityStringValues("SafeTransformMethods");
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00013660 File Offset: 0x00012660
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
								if (!string.IsNullOrEmpty(text))
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

		// Token: 0x060003B0 RID: 944 RVA: 0x00013704 File Offset: 0x00012704
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool ReadHmacTruncationSetting()
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
				{
					if (registryKey == null)
					{
						result = false;
					}
					else
					{
						object value = registryKey.GetValue("AllowHMACTruncation");
						if (value == null)
						{
							result = false;
						}
						else if (registryKey.GetValueKind("AllowHMACTruncation") != RegistryValueKind.DWord)
						{
							result = false;
						}
						else
						{
							result = ((int)value != 0);
						}
					}
				}
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001378C File Offset: 0x0001278C
		private static bool DefaultSignatureFormatValidator(SignedXml signedXml)
		{
			return (SignedXml.AllowHmacTruncation || !signedXml.DoesSignatureUseTruncatedHmac()) && signedXml.DoesSignatureUseSafeCanonicalizationMethod();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000137AC File Offset: 0x000127AC
		private bool DoesSignatureUseSafeCanonicalizationMethod()
		{
			foreach (string a in SignedXml.SafeCanonicalizationMethods)
			{
				if (string.Equals(a, this.SignedInfo.CanonicalizationMethod, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001380C File Offset: 0x0001280C
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

		// Token: 0x060003B4 RID: 948 RVA: 0x0001384C File Offset: 0x0001284C
		private bool IsSafeTransform(string transformAlgorithm)
		{
			foreach (string a in SignedXml.SafeCanonicalizationMethods)
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
			return false;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000138EC File Offset: 0x000128EC
		private bool DoesSignatureUseTruncatedHmac()
		{
			if (this.SignedInfo == null || this.SignedInfo.SignatureLength == null)
			{
				return false;
			}
			HMAC hmac = Utils.CreateFromName<HMAC>(this.SignatureMethod);
			if (hmac == null)
			{
				if (string.Equals(this.SignatureMethod, "http://www.w3.org/2000/09/xmldsig#hmac-sha1", StringComparison.Ordinal))
				{
					hmac = new HMACSHA1();
				}
				else if (string.Equals(this.SignatureMethod, "http://www.w3.org/2001/04/xmldsig-more#hmac-md5", StringComparison.Ordinal))
				{
					hmac = new HMACMD5();
				}
			}
			if (hmac == null)
			{
				return false;
			}
			int num = 0;
			if (!int.TryParse(this.SignedInfo.SignatureLength, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return true;
			}
			int num2 = Math.Max(80, hmac.HashSize / 2);
			return num < num2;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001398C File Offset: 0x0001298C
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

		// Token: 0x060003B7 RID: 951 RVA: 0x00013A18 File Offset: 0x00012A18
		private SignatureDescription CreateSignatureDescriptionFromName(string name)
		{
			SignatureDescription signatureDescription = Utils.CreateFromName<SignatureDescription>(name);
			if (signatureDescription != null)
			{
				return signatureDescription;
			}
			StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
			if (name.Equals("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", comparisonType))
			{
				return new RSAPKCS1SHA256SignatureDescription();
			}
			if (name.Equals("http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", comparisonType))
			{
				return new RSAPKCS1SHA384SignatureDescription();
			}
			if (name.Equals("http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", comparisonType))
			{
				return new RSAPKCS1SHA512SignatureDescription();
			}
			return null;
		}

		// Token: 0x0400052F RID: 1327
		private const string XmlDsigMoreHMACMD5Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-md5";

		// Token: 0x04000530 RID: 1328
		private const string XmlDsigMoreHMACSHA256Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x04000531 RID: 1329
		private const string XmlDsigMoreHMACSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x04000532 RID: 1330
		private const string XmlDsigMoreHMACSHA512Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x04000533 RID: 1331
		private const string XmlDsigMoreHMACRIPEMD160Url = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x04000534 RID: 1332
		public const string XmlDsigNamespaceUrl = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04000535 RID: 1333
		public const string XmlDsigMinimalCanonicalizationUrl = "http://www.w3.org/2000/09/xmldsig#minimal";

		// Token: 0x04000536 RID: 1334
		public const string XmlDsigCanonicalizationUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04000537 RID: 1335
		public const string XmlDsigCanonicalizationWithCommentsUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x04000538 RID: 1336
		public const string XmlDsigSHA1Url = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04000539 RID: 1337
		public const string XmlDsigDSAUrl = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x0400053A RID: 1338
		public const string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x0400053B RID: 1339
		internal const string XmlDsigSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x0400053C RID: 1340
		internal const string XmlDsigRSASHA256Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x0400053D RID: 1341
		internal const string XmlDsigSHA384Url = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x0400053E RID: 1342
		internal const string XmlDsigRSASHA384Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x0400053F RID: 1343
		internal const string XmlDsigSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000540 RID: 1344
		internal const string XmlDsigRSASHA512Url = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		// Token: 0x04000541 RID: 1345
		public const string XmlDsigHMACSHA1Url = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x04000542 RID: 1346
		public const string XmlDsigC14NTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04000543 RID: 1347
		public const string XmlDsigC14NWithCommentsTransformUrl = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x04000544 RID: 1348
		public const string XmlDsigExcC14NTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000545 RID: 1349
		public const string XmlDsigExcC14NWithCommentsTransformUrl = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000546 RID: 1350
		public const string XmlDsigBase64TransformUrl = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x04000547 RID: 1351
		public const string XmlDsigXPathTransformUrl = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x04000548 RID: 1352
		public const string XmlDsigXsltTransformUrl = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x04000549 RID: 1353
		public const string XmlDsigEnvelopedSignatureTransformUrl = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x0400054A RID: 1354
		public const string XmlDecryptionTransformUrl = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x0400054B RID: 1355
		public const string XmlLicenseTransformUrl = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";

		// Token: 0x0400054C RID: 1356
		private const string AllowHMACTruncationValue = "AllowHMACTruncation";

		// Token: 0x0400054D RID: 1357
		protected Signature m_signature;

		// Token: 0x0400054E RID: 1358
		protected string m_strSigningKeyName;

		// Token: 0x0400054F RID: 1359
		private AsymmetricAlgorithm m_signingKey;

		// Token: 0x04000550 RID: 1360
		private XmlDocument m_containingDocument;

		// Token: 0x04000551 RID: 1361
		private IEnumerator m_keyInfoEnum;

		// Token: 0x04000552 RID: 1362
		private X509Certificate2Collection m_x509Collection;

		// Token: 0x04000553 RID: 1363
		private IEnumerator m_x509Enum;

		// Token: 0x04000554 RID: 1364
		private bool[] m_refProcessed;

		// Token: 0x04000555 RID: 1365
		private int[] m_refLevelCache;

		// Token: 0x04000556 RID: 1366
		internal XmlResolver m_xmlResolver;

		// Token: 0x04000557 RID: 1367
		internal XmlElement m_context;

		// Token: 0x04000558 RID: 1368
		private bool m_bResolverSet;

		// Token: 0x04000559 RID: 1369
		private EncryptedXml m_exml;

		// Token: 0x0400055A RID: 1370
		private static bool? s_allowHmacTruncation;

		// Token: 0x0400055B RID: 1371
		private static List<string> s_safeCanonicalizationMethods;

		// Token: 0x0400055C RID: 1372
		private static List<string> s_defaultSafeTransformMethods;

		// Token: 0x0400055D RID: 1373
		private bool bCacheValid;

		// Token: 0x0400055E RID: 1374
		private byte[] _digestedSignedInfo;

		// Token: 0x020000AA RID: 170
		private class ReferenceLevelSortOrder : IComparer
		{
			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00013A78 File Offset: 0x00012A78
			// (set) Token: 0x060003BA RID: 954 RVA: 0x00013A80 File Offset: 0x00012A80
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

			// Token: 0x060003BB RID: 955 RVA: 0x00013A8C File Offset: 0x00012A8C
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

			// Token: 0x0400055F RID: 1375
			private ArrayList m_references;
		}
	}
}
