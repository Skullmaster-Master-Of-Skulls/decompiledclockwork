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
	// Token: 0x02000041 RID: 65
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EncryptedXml
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00008631 File Offset: 0x00006831
		public EncryptedXml() : this(new XmlDocument())
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000863E File Offset: 0x0000683E
		public EncryptedXml(XmlDocument document) : this(document, null)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008648 File Offset: 0x00006848
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

		// Token: 0x060001F4 RID: 500 RVA: 0x000086A0 File Offset: 0x000068A0
		private bool IsOverXmlDsigRecursionLimit()
		{
			return this.m_xmlDsigSearchDepthCounter > this.XmlDSigSearchDepth;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000086B3 File Offset: 0x000068B3
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000086BB File Offset: 0x000068BB
		public int XmlDSigSearchDepth
		{
			get
			{
				return this.m_xmlDsigSearchDepth;
			}
			set
			{
				this.m_xmlDsigSearchDepth = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000086C4 File Offset: 0x000068C4
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000086CC File Offset: 0x000068CC
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000086D5 File Offset: 0x000068D5
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000086DD File Offset: 0x000068DD
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000086E6 File Offset: 0x000068E6
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000086EE File Offset: 0x000068EE
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000086F7 File Offset: 0x000068F7
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000086FF File Offset: 0x000068FF
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008708 File Offset: 0x00006908
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00008710 File Offset: 0x00006910
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008719 File Offset: 0x00006919
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00008734 File Offset: 0x00006934
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

		// Token: 0x06000203 RID: 515 RVA: 0x00008740 File Offset: 0x00006940
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

		// Token: 0x06000204 RID: 516 RVA: 0x000089C8 File Offset: 0x00006BC8
		private void DownloadCipherValue(CipherData cipherData, out Stream inputStream, out Stream decInputStream, out WebResponse response)
		{
			PermissionSet standardSandbox = SecurityManager.GetStandardSandbox(this.m_evidence);
			standardSandbox.PermitOnly();
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

		// Token: 0x06000205 RID: 517 RVA: 0x00008A8F File Offset: 0x00006C8F
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008A98 File Offset: 0x00006C98
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
			int num;
			if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#des-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
			{
				if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotSupported"));
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

		// Token: 0x06000207 RID: 519 RVA: 0x00008B5C File Offset: 0x00006D5C
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

		// Token: 0x06000208 RID: 520 RVA: 0x00008D74 File Offset: 0x00006F74
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
						foreach (X509Certificate2 certificate in x509Certificate2Collection)
						{
							using (RSA rsaprivateKey = certificate.GetRSAPrivateKey())
							{
								if (rsaprivateKey != null)
								{
									if (!Utils.GetLeaveCipherValueUnchecked() && (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null))
									{
										throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_MissingAlgorithm"));
									}
									useOAEP = (encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
									return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, rsaprivateKey, useOAEP);
								}
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

		// Token: 0x06000209 RID: 521 RVA: 0x00009068 File Offset: 0x00007268
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

		// Token: 0x0600020A RID: 522 RVA: 0x000090BE File Offset: 0x000072BE
		public void ClearKeyNameMappings()
		{
			this.m_keyNameMapping.Clear();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000090CC File Offset: 0x000072CC
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
			EncryptedData result;
			using (RSA rsapublicKey = certificate.GetRSAPublicKey())
			{
				if (rsapublicKey == null)
				{
					throw new NotSupportedException(SecurityResources.GetResourceString("NotSupported_KeyAlgorithm"));
				}
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
				EncryptedKey encryptedKey = new EncryptedKey();
				encryptedKey.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
				encryptedKey.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
				using (Aes aes = Aes.Create())
				{
					encryptedKey.CipherData.CipherValue = EncryptedXml.EncryptKey(aes.Key, rsapublicKey, false);
					KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
					encryptedData.KeyInfo.AddClause(clause);
					encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, aes, false);
				}
				result = encryptedData;
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000091D8 File Offset: 0x000073D8
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
				if (!(symmetricAlgorithm is Rijndael) && !(symmetricAlgorithm is Aes))
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
			using (Aes aes = Aes.Create())
			{
				encryptedKey.CipherData.CipherValue = ((symmetricAlgorithm == null) ? EncryptedXml.EncryptKey(aes.Key, rsa, false) : EncryptedXml.EncryptKey(aes.Key, symmetricAlgorithm));
				KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
				encryptedData.KeyInfo.AddClause(clause);
				encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, aes, false);
			}
			return encryptedData;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009380 File Offset: 0x00007580
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

		// Token: 0x0600020E RID: 526 RVA: 0x00009458 File Offset: 0x00007658
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

		// Token: 0x0600020F RID: 527 RVA: 0x00009524 File Offset: 0x00007724
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

		// Token: 0x06000210 RID: 528 RVA: 0x00009580 File Offset: 0x00007780
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

		// Token: 0x06000211 RID: 529 RVA: 0x00009658 File Offset: 0x00007858
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
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				string @string = this.m_encoding.GetString(decryptedData);
				using (StringReader stringReader = new StringReader(@string))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, Utils.GetSecureXmlReaderSettings(this.m_xmlResolver)))
					{
						xmlDocument.Load(xmlReader);
					}
				}
				XmlNode newChild = inputElement.OwnerDocument.ImportNode(xmlDocument.DocumentElement, true);
				parentNode.RemoveChild(inputElement);
				parentNode.AppendChild(newChild);
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

		// Token: 0x06000212 RID: 530 RVA: 0x000097B8 File Offset: 0x000079B8
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
			if (content)
			{
				Utils.RemoveAllChildren(inputElement);
				inputElement.AppendChild(xml);
				return;
			}
			XmlNode parentNode = inputElement.ParentNode;
			parentNode.ReplaceChild(xml, inputElement);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009810 File Offset: 0x00007A10
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
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000987C File Offset: 0x00007A7C
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

		// Token: 0x06000215 RID: 533 RVA: 0x000098C8 File Offset: 0x00007AC8
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
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_NotSupportedCryptographicTransform"));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009934 File Offset: 0x00007B34
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

		// Token: 0x06000217 RID: 535 RVA: 0x00009980 File Offset: 0x00007B80
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

		// Token: 0x06000218 RID: 536 RVA: 0x000099C8 File Offset: 0x00007BC8
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009A20 File Offset: 0x00007C20
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

		// Token: 0x040003C9 RID: 969
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x040003CA RID: 970
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		// Token: 0x040003CB RID: 971
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		// Token: 0x040003CC RID: 972
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		// Token: 0x040003CD RID: 973
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x040003CE RID: 974
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x040003CF RID: 975
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x040003D0 RID: 976
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x040003D1 RID: 977
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x040003D2 RID: 978
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x040003D3 RID: 979
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x040003D4 RID: 980
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x040003D5 RID: 981
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x040003D6 RID: 982
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x040003D7 RID: 983
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x040003D8 RID: 984
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040003D9 RID: 985
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040003DA RID: 986
		private XmlDocument m_document;

		// Token: 0x040003DB RID: 987
		private Evidence m_evidence;

		// Token: 0x040003DC RID: 988
		private XmlResolver m_xmlResolver;

		// Token: 0x040003DD RID: 989
		private const int m_capacity = 4;

		// Token: 0x040003DE RID: 990
		private Hashtable m_keyNameMapping;

		// Token: 0x040003DF RID: 991
		private PaddingMode m_padding;

		// Token: 0x040003E0 RID: 992
		private CipherMode m_mode;

		// Token: 0x040003E1 RID: 993
		private Encoding m_encoding;

		// Token: 0x040003E2 RID: 994
		private string m_recipient;

		// Token: 0x040003E3 RID: 995
		private int m_xmlDsigSearchDepthCounter;

		// Token: 0x040003E4 RID: 996
		private int m_xmlDsigSearchDepth;

		// Token: 0x040003E5 RID: 997
		private static IList<string> s_defaultSafeTransformMethods;
	}
}
