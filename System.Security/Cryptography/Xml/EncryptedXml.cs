using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C8 RID: 200
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EncryptedXml
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0001842B File Offset: 0x0001742B
		public EncryptedXml() : this(new XmlDocument())
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018438 File Offset: 0x00017438
		public EncryptedXml(XmlDocument document) : this(document, null)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00018444 File Offset: 0x00017444
		public EncryptedXml(XmlDocument document, Evidence evidence)
		{
			this.m_document = document;
			this.m_evidence = evidence;
			this.m_xmlResolver = null;
			this.m_padding = PaddingMode.ISO10126;
			this.m_mode = CipherMode.CBC;
			this.m_encoding = Encoding.UTF8;
			this.m_keyNameMapping = new Hashtable(4);
			this.m_xmlDsigSearchDepth = Utils.GetXmlDsigSearchDepth();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001849C File Offset: 0x0001749C
		private bool IsOverXmlDsigRecursionLimit()
		{
			return this.m_xmlDsigSearchDepthCounter > this.m_xmlDsigSearchDepth;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000184AF File Offset: 0x000174AF
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x000184B7 File Offset: 0x000174B7
		public Evidence DocumentEvidence
		{
			get
			{
				return this.m_evidence;
			}
			set
			{
				this.m_evidence = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000184C0 File Offset: 0x000174C0
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000184C8 File Offset: 0x000174C8
		public XmlResolver Resolver
		{
			get
			{
				return this.m_xmlResolver;
			}
			set
			{
				this.m_xmlResolver = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000184D1 File Offset: 0x000174D1
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000184D9 File Offset: 0x000174D9
		public PaddingMode Padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000184E2 File Offset: 0x000174E2
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000184EA File Offset: 0x000174EA
		public CipherMode Mode
		{
			get
			{
				return this.m_mode;
			}
			set
			{
				this.m_mode = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000184F3 File Offset: 0x000174F3
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000184FB File Offset: 0x000174FB
		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018504 File Offset: 0x00017504
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001851F File Offset: 0x0001751F
		public string Recipient
		{
			get
			{
				if (this.m_recipient == null)
				{
					this.m_recipient = string.Empty;
				}
				return this.m_recipient;
			}
			set
			{
				this.m_recipient = value;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00018528 File Offset: 0x00017528
		private byte[] GetCipherValue(CipherData cipherData)
		{
			if (cipherData == null)
			{
				throw new ArgumentNullException("cipherData");
			}
			WebResponse webResponse = null;
			Stream stream = null;
			if (cipherData.CipherValue != null)
			{
				return cipherData.CipherValue;
			}
			if (cipherData.CipherReference == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingCipherData"));
			}
			if (cipherData.CipherReference.CipherValue != null)
			{
				return cipherData.CipherReference.CipherValue;
			}
			Stream stream2 = null;
			if (!Utils.GetLeaveCipherValueUnchecked() && cipherData.CipherReference.Uri == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
			}
			if (cipherData.CipherReference.Uri.Length == 0)
			{
				string baseUri = (this.m_document == null) ? null : this.m_document.BaseURI;
				TransformChain transformChain = cipherData.CipherReference.TransformChain;
				if (!Utils.GetLeaveCipherValueUnchecked() && transformChain == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
				}
				if (!EncryptedXml.ReferenceUsesSafeTransformMethods(cipherData.CipherReference))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
				}
				stream2 = transformChain.TransformToOctetStream(this.m_document, this.m_xmlResolver, baseUri);
			}
			else if (cipherData.CipherReference.Uri[0] == '#')
			{
				string idValue = Utils.ExtractIdFromLocalUri(cipherData.CipherReference.Uri);
				if (Utils.GetLeaveCipherValueUnchecked())
				{
					stream = new MemoryStream(this.m_encoding.GetBytes(this.GetIdElement(this.m_document, idValue).OuterXml));
				}
				else
				{
					XmlElement idElement = this.GetIdElement(this.m_document, idValue);
					if (idElement == null || idElement.OuterXml == null)
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
					}
					stream = new MemoryStream(this.m_encoding.GetBytes(idElement.OuterXml));
				}
				string baseUri2 = (this.m_document == null) ? null : this.m_document.BaseURI;
				TransformChain transformChain2 = cipherData.CipherReference.TransformChain;
				if (!Utils.GetLeaveCipherValueUnchecked() && transformChain2 == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
				}
				if (!EncryptedXml.ReferenceUsesSafeTransformMethods(cipherData.CipherReference))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
				}
				stream2 = transformChain2.TransformToOctetStream(stream, this.m_xmlResolver, baseUri2);
			}
			else
			{
				this.DownloadCipherValue(cipherData, out stream, out stream2, out webResponse);
			}
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Utils.Pump(stream2, memoryStream);
				array = memoryStream.ToArray();
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
				stream2.Close();
			}
			cipherData.CipherReference.CipherValue = array;
			return array;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000187B4 File Offset: 0x000177B4
		private void DownloadCipherValue(CipherData cipherData, out Stream inputStream, out Stream decInputStream, out WebResponse response)
		{
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(this.m_evidence);
			permissionSet.PermitOnly();
			WebRequest webRequest = WebRequest.Create(cipherData.CipherReference.Uri);
			if (webRequest == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), cipherData.CipherReference.Uri);
			}
			response = webRequest.GetResponse();
			if (response == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), cipherData.CipherReference.Uri);
			}
			inputStream = response.GetResponseStream();
			if (inputStream == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), cipherData.CipherReference.Uri);
			}
			TransformChain transformChain = cipherData.CipherReference.TransformChain;
			decInputStream = transformChain.TransformToOctetStream(inputStream, this.m_xmlResolver, cipherData.CipherReference.Uri);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001887B File Offset: 0x0001787B
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00018884 File Offset: 0x00017884
		public virtual byte[] GetDecryptionIV(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			string a;
			if ((a = symmetricAlgorithmUri) != null)
			{
				int num;
				if (!(a == "http://www.w3.org/2001/04/xmlenc#des-cbc") && !(a == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
				{
					if (!(a == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(a == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(a == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
					{
						goto IL_89;
					}
					num = 16;
				}
				else
				{
					num = 8;
				}
				byte[] array = new byte[num];
				byte[] cipherValue = this.GetCipherValue(encryptedData.CipherData);
				Buffer.BlockCopy(cipherValue, 0, array, 0, array.Length);
				return array;
			}
			IL_89:
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001894C File Offset: 0x0001794C
		public virtual SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (encryptedData.KeyInfo == null)
			{
				return null;
			}
			IEnumerator enumerator = encryptedData.KeyInfo.GetEnumerator();
			EncryptedKey encryptedKey = null;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				KeyInfoName keyInfoName = obj as KeyInfoName;
				if (keyInfoName != null)
				{
					string value = keyInfoName.Value;
					if ((SymmetricAlgorithm)this.m_keyNameMapping[value] != null)
					{
						return (SymmetricAlgorithm)this.m_keyNameMapping[value];
					}
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_document.NameTable);
					xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
					XmlNodeList xmlNodeList = this.m_document.SelectNodes("//enc:EncryptedKey", xmlNamespaceManager);
					if (xmlNodeList == null)
					{
						break;
					}
					using (IEnumerator enumerator2 = xmlNodeList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode = (XmlNode)obj2;
							XmlElement value2 = xmlNode as XmlElement;
							EncryptedKey encryptedKey2 = new EncryptedKey();
							encryptedKey2.LoadXml(value2);
							if (encryptedKey2.CarriedKeyName == value && encryptedKey2.Recipient == this.Recipient)
							{
								encryptedKey = encryptedKey2;
								break;
							}
						}
						break;
					}
				}
				KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
				if (keyInfoRetrievalMethod != null)
				{
					string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
					encryptedKey = new EncryptedKey();
					encryptedKey.LoadXml(this.GetIdElement(this.m_document, idValue));
					break;
				}
				KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
				if (keyInfoEncryptedKey != null)
				{
					encryptedKey = keyInfoEncryptedKey.EncryptedKey;
					break;
				}
			}
			if (encryptedKey == null)
			{
				return null;
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			byte[] array = this.DecryptEncryptedKey(encryptedKey);
			if (array == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingDecryptionKey"));
			}
			SymmetricAlgorithm symmetricAlgorithm = Utils.CreateFromName<SymmetricAlgorithm>(symmetricAlgorithmUri);
			if (symmetricAlgorithm == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
			}
			symmetricAlgorithm.Key = array;
			return symmetricAlgorithm;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018B64 File Offset: 0x00017B64
		public virtual byte[] DecryptEncryptedKey(EncryptedKey encryptedKey)
		{
			if (encryptedKey == null)
			{
				throw new ArgumentNullException("encryptedKey");
			}
			if (encryptedKey.KeyInfo == null)
			{
				return null;
			}
			foreach (object obj in encryptedKey.KeyInfo)
			{
				KeyInfoName keyInfoName = obj as KeyInfoName;
				bool useOAEP;
				if (keyInfoName == null)
				{
					IEnumerator enumerator;
					KeyInfoX509Data keyInfoX509Data = enumerator.Current as KeyInfoX509Data;
					if (keyInfoX509Data != null)
					{
						X509Certificate2Collection x509Certificate2Collection = Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Decryption);
						foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
						{
							RSA rsa = x509Certificate.PrivateKey as RSA;
							if (rsa != null)
							{
								if (!Utils.GetLeaveCipherValueUnchecked() && (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null))
								{
									throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
								}
								useOAEP = (encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
								return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, rsa, useOAEP);
							}
						}
						break;
					}
					KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
					EncryptedKey encryptedKey2;
					if (keyInfoRetrievalMethod != null)
					{
						string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
						encryptedKey2 = new EncryptedKey();
						encryptedKey2.LoadXml(this.GetIdElement(this.m_document, idValue));
						try
						{
							this.m_xmlDsigSearchDepthCounter++;
							if (this.IsOverXmlDsigRecursionLimit())
							{
								throw new CryptoSignedXmlRecursionException();
							}
							return this.DecryptEncryptedKey(encryptedKey2);
						}
						finally
						{
							this.m_xmlDsigSearchDepthCounter--;
						}
					}
					KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
					if (keyInfoEncryptedKey == null)
					{
						continue;
					}
					encryptedKey2 = keyInfoEncryptedKey.EncryptedKey;
					byte[] array = this.DecryptEncryptedKey(encryptedKey2);
					if (array == null)
					{
						continue;
					}
					SymmetricAlgorithm symmetricAlgorithm = Utils.CreateFromName<SymmetricAlgorithm>(encryptedKey.EncryptionMethod.KeyAlgorithm);
					if (symmetricAlgorithm == null)
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
					}
					symmetricAlgorithm.Key = array;
					if (!Utils.GetLeaveCipherValueUnchecked() && (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null))
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
					}
					return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, symmetricAlgorithm);
				}
				string value = keyInfoName.Value;
				object obj2 = this.m_keyNameMapping[value];
				if (obj2 == null)
				{
					break;
				}
				if (!Utils.GetLeaveCipherValueUnchecked() && (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
				}
				if (obj2 is SymmetricAlgorithm)
				{
					return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (SymmetricAlgorithm)obj2);
				}
				useOAEP = (encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
				return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj2, useOAEP);
			}
			return null;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018E44 File Offset: 0x00017E44
		public void AddKeyNameMapping(string keyName, object keyObject)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (keyObject == null)
			{
				throw new ArgumentNullException("keyObject");
			}
			if (!(keyObject is SymmetricAlgorithm) && !(keyObject is RSA))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
			}
			this.m_keyNameMapping.Add(keyName, keyObject);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018E9A File Offset: 0x00017E9A
		public void ClearKeyNameMappings()
		{
			this.m_keyNameMapping.Clear();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00018EA8 File Offset: 0x00017EA8
		public EncryptedData Encrypt(XmlElement inputElement, X509Certificate2 certificate)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (X509Utils.OidToAlgId(certificate.PublicKey.Oid.Value) != 41984U)
			{
				throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_KeyAlgorithm"));
			}
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
			encryptedKey.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = EncryptedXml.EncryptKey(rijndaelManaged.Key, certificate.PublicKey.Key as RSA, false);
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(clause);
			encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
			return encryptedData;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00018F9C File Offset: 0x00017F9C
		public EncryptedData Encrypt(XmlElement inputElement, string keyName)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			object obj = null;
			if (this.m_keyNameMapping != null)
			{
				obj = this.m_keyNameMapping[keyName];
			}
			if (obj == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingEncryptionKey"));
			}
			SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
			RSA rsa = obj as RSA;
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			string algorithm = null;
			if (symmetricAlgorithm == null)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
			}
			else if (symmetricAlgorithm is TripleDES)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
			}
			else
			{
				if (!(symmetricAlgorithm is Rijndael))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
				}
				int keySize = symmetricAlgorithm.KeySize;
				if (keySize != 128)
				{
					if (keySize != 192)
					{
						if (keySize == 256)
						{
							algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes256";
						}
					}
					else
					{
						algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes192";
					}
				}
				else
				{
					algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				}
			}
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod(algorithm);
			encryptedKey.KeyInfo.AddClause(new KeyInfoName(keyName));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = ((symmetricAlgorithm == null) ? EncryptedXml.EncryptKey(rijndaelManaged.Key, rsa, false) : EncryptedXml.EncryptKey(rijndaelManaged.Key, symmetricAlgorithm));
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(clause);
			encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
			return encryptedData;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001911C File Offset: 0x0001811C
		public void DecryptDocument()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_document.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNodeList xmlNodeList = this.m_document.SelectNodes("//enc:EncryptedData", xmlNamespaceManager);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = xmlNode as XmlElement;
					EncryptedData encryptedData = new EncryptedData();
					encryptedData.LoadXml(xmlElement);
					SymmetricAlgorithm decryptionKey = this.GetDecryptionKey(encryptedData, null);
					if (decryptionKey == null)
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingDecryptionKey"));
					}
					byte[] decryptedData = this.DecryptData(encryptedData, decryptionKey);
					this.ReplaceData(xmlElement, decryptedData);
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000191F4 File Offset: 0x000181F4
		public byte[] EncryptData(byte[] plaintext, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (plaintext == null)
			{
				throw new ArgumentNullException("plaintext");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] array = null;
			try
			{
				symmetricAlgorithm.Mode = this.m_mode;
				symmetricAlgorithm.Padding = this.m_padding;
				ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor();
				array = cryptoTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
			}
			byte[] array2;
			if (this.m_mode == CipherMode.ECB)
			{
				array2 = array;
			}
			else
			{
				byte[] iv = symmetricAlgorithm.IV;
				array2 = new byte[array.Length + iv.Length];
				Buffer.BlockCopy(iv, 0, array2, 0, iv.Length);
				Buffer.BlockCopy(array, 0, array2, iv.Length, array.Length);
			}
			return array2;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000192C4 File Offset: 0x000182C4
		public byte[] EncryptData(XmlElement inputElement, SymmetricAlgorithm symmetricAlgorithm, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] plaintext = content ? this.m_encoding.GetBytes(inputElement.InnerXml) : this.m_encoding.GetBytes(inputElement.OuterXml);
			return this.EncryptData(plaintext, symmetricAlgorithm);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019320 File Offset: 0x00018320
		public byte[] DecryptData(EncryptedData encryptedData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] cipherValue = this.GetCipherValue(encryptedData.CipherData);
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] iv = symmetricAlgorithm.IV;
			byte[] array = null;
			if (this.m_mode != CipherMode.ECB)
			{
				array = this.GetDecryptionIV(encryptedData, null);
			}
			byte[] result = null;
			try
			{
				int num = 0;
				if (array != null)
				{
					symmetricAlgorithm.IV = array;
					num = array.Length;
				}
				symmetricAlgorithm.Mode = this.m_mode;
				symmetricAlgorithm.Padding = this.m_padding;
				ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor();
				result = cryptoTransform.TransformFinalBlock(cipherValue, num, cipherValue.Length - num);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
				symmetricAlgorithm.IV = iv;
			}
			return result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000193F8 File Offset: 0x000183F8
		public void ReplaceData(XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlNode parentNode = inputElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				parentNode.InnerXml = this.m_encoding.GetString(decryptedData);
				return;
			}
			XmlNode xmlNode = parentNode.OwnerDocument.CreateElement(parentNode.Prefix, parentNode.LocalName, parentNode.NamespaceURI);
			try
			{
				parentNode.AppendChild(xmlNode);
				xmlNode.InnerXml = this.m_encoding.GetString(decryptedData);
				XmlNode xmlNode2 = xmlNode.FirstChild;
				XmlNode nextSibling = inputElement.NextSibling;
				while (xmlNode2 != null)
				{
					XmlNode nextSibling2 = xmlNode2.NextSibling;
					parentNode.InsertBefore(xmlNode2, nextSibling);
					xmlNode2 = nextSibling2;
				}
			}
			finally
			{
				parentNode.RemoveChild(xmlNode);
			}
			parentNode.RemoveChild(inputElement);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000194CC File Offset: 0x000184CC
		public static void ReplaceElement(XmlElement inputElement, EncryptedData encryptedData, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			XmlElement xml = encryptedData.GetXml(inputElement.OwnerDocument);
			switch (content)
			{
			case false:
			{
				XmlNode parentNode = inputElement.ParentNode;
				parentNode.ReplaceChild(xml, inputElement);
				return;
			}
			case true:
				Utils.RemoveAllChildren(inputElement);
				inputElement.AppendChild(xml);
				return;
			default:
				return;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00019534 File Offset: 0x00018534
		public static byte[] EncryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00019598 File Offset: 0x00018598
		public static byte[] EncryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				RSAOAEPKeyExchangeFormatter rsaoaepkeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(rsa);
				return rsaoaepkeyExchangeFormatter.CreateKeyExchange(keyData);
			}
			RSAPKCS1KeyExchangeFormatter rsapkcs1KeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(rsa);
			return rsapkcs1KeyExchangeFormatter.CreateKeyExchange(keyData);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000195E4 File Offset: 0x000185E4
		public static byte[] DecryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00019648 File Offset: 0x00018648
		public static byte[] DecryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				RSAOAEPKeyExchangeDeformatter rsaoaepkeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(rsa);
				return rsaoaepkeyExchangeDeformatter.DecryptKeyExchange(keyData);
			}
			RSAPKCS1KeyExchangeDeformatter rsapkcs1KeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(rsa);
			return rsapkcs1KeyExchangeDeformatter.DecryptKeyExchange(keyData);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00019694 File Offset: 0x00018694
		private static bool ReferenceUsesSafeTransformMethods(CipherReference reference)
		{
			if (Utils.GetEncryptedXmlAllowDangerousTransforms())
			{
				return true;
			}
			TransformChain transformChain = reference.TransformChain;
			int count = transformChain.Count;
			for (int i = 0; i < count; i++)
			{
				Transform transform = transformChain[i];
				if (!EncryptedXml.IsSafeTransform(transform.Algorithm))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000196DC File Offset: 0x000186DC
		private static bool IsSafeTransform(string transformAlgorithm)
		{
			foreach (string a in EncryptedXml.DefaultSafeTransformMethods)
			{
				if (string.Equals(a, transformAlgorithm, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00019734 File Offset: 0x00018734
		private static IList<string> DefaultSafeTransformMethods
		{
			get
			{
				if (EncryptedXml.s_defaultSafeTransformMethods == null)
				{
					EncryptedXml.s_defaultSafeTransformMethods = new List<string>(7)
					{
						"http://www.w3.org/TR/2001/REC-xml-c14n-20010315",
						"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments",
						"http://www.w3.org/2001/10/xml-exc-c14n#",
						"http://www.w3.org/2001/10/xml-exc-c14n#WithComments",
						"http://www.w3.org/2000/09/xmldsig#base64",
						"urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform",
						"http://www.w3.org/2002/07/decrypt#XML"
					};
				}
				return EncryptedXml.s_defaultSafeTransformMethods;
			}
		}

		// Token: 0x040005BC RID: 1468
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x040005BD RID: 1469
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		// Token: 0x040005BE RID: 1470
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		// Token: 0x040005BF RID: 1471
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		// Token: 0x040005C0 RID: 1472
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x040005C1 RID: 1473
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x040005C2 RID: 1474
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x040005C3 RID: 1475
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x040005C4 RID: 1476
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x040005C5 RID: 1477
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x040005C6 RID: 1478
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x040005C7 RID: 1479
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x040005C8 RID: 1480
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x040005C9 RID: 1481
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x040005CA RID: 1482
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x040005CB RID: 1483
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040005CC RID: 1484
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040005CD RID: 1485
		private const int m_capacity = 4;

		// Token: 0x040005CE RID: 1486
		private XmlDocument m_document;

		// Token: 0x040005CF RID: 1487
		private Evidence m_evidence;

		// Token: 0x040005D0 RID: 1488
		private XmlResolver m_xmlResolver;

		// Token: 0x040005D1 RID: 1489
		private Hashtable m_keyNameMapping;

		// Token: 0x040005D2 RID: 1490
		private PaddingMode m_padding;

		// Token: 0x040005D3 RID: 1491
		private CipherMode m_mode;

		// Token: 0x040005D4 RID: 1492
		private Encoding m_encoding;

		// Token: 0x040005D5 RID: 1493
		private string m_recipient;

		// Token: 0x040005D6 RID: 1494
		private int m_xmlDsigSearchDepthCounter;

		// Token: 0x040005D7 RID: 1495
		private int m_xmlDsigSearchDepth;

		// Token: 0x040005D8 RID: 1496
		private static IList<string> s_defaultSafeTransformMethods;
	}
}
