using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Xml;
using ClockWorkLogger;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200001B RID: 27
	public abstract class TokenXmlSerializer
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004920 File Offset: 0x00002B20
		public ICertStoreManager CertStoreManager
		{
			get
			{
				ICertStoreManager result;
				if ((result = this._certStoreManager) == null)
				{
					result = (this._certStoreManager = new CertStoreManager());
				}
				return result;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004948 File Offset: 0x00002B48
		protected virtual bool GetCertsForDecryptionV2(XmlElement securityTokenXml, out List<SecurityToken> securityTokens)
		{
			securityTokens = new List<SecurityToken>();
			EncryptedData encryptedData = new EncryptedData();
			bool flag = securityTokenXml != null;
			if (flag)
			{
				encryptedData.LoadXml(securityTokenXml["xenc:EncryptedData"]);
			}
			KeyInfoX509Data keyInfoX509Data;
			if (encryptedData == null)
			{
				keyInfoX509Data = null;
			}
			else
			{
				KeyInfo keyInfo = encryptedData.KeyInfo;
				if (keyInfo == null)
				{
					keyInfoX509Data = null;
				}
				else
				{
					IEnumerator enumerator = keyInfo.GetEnumerator();
					if (enumerator == null)
					{
						keyInfoX509Data = null;
					}
					else
					{
						KeyInfoEncryptedKey firstValue = enumerator.GetFirstValue<KeyInfoEncryptedKey>();
						if (firstValue == null)
						{
							keyInfoX509Data = null;
						}
						else
						{
							EncryptedKey encryptedKey = firstValue.EncryptedKey;
							if (encryptedKey == null)
							{
								keyInfoX509Data = null;
							}
							else
							{
								KeyInfo keyInfo2 = encryptedKey.KeyInfo;
								if (keyInfo2 == null)
								{
									keyInfoX509Data = null;
								}
								else
								{
									IEnumerator enumerator2 = keyInfo2.GetEnumerator();
									keyInfoX509Data = ((enumerator2 != null) ? enumerator2.GetFirstValue<KeyInfoX509Data>() : null);
								}
							}
						}
					}
				}
			}
			KeyInfoX509Data keyInfoX509Data2 = keyInfoX509Data;
			object obj;
			if (keyInfoX509Data2 == null)
			{
				obj = null;
			}
			else
			{
				ArrayList issuerSerials = keyInfoX509Data2.IssuerSerials;
				obj = ((issuerSerials != null) ? issuerSerials[0] : null);
			}
			object obj2 = obj;
			X509IssuerSerial? x509IssuerSerial = obj2 as X509IssuerSerial?;
			string text = ((x509IssuerSerial != null) ? x509IssuerSerial.GetValueOrDefault().SerialNumber : null) ?? string.Empty;
			CWLogger.Logger.Debug("TokenXmlSerializer:GetCertsForDecryptionV2:serialNumber={0}", text ?? "NULL");
			bool flag2 = !string.IsNullOrEmpty(text);
			if (flag2)
			{
				IList<X509Store> list = this.CertStoreManager.OpenSupportedStores();
				try
				{
					X509Certificate2 x509Certificate = this.CertStoreManager.LookupCertFromSupportedStores(list, X509FindType.FindBySerialNumber, text);
					bool flag3 = x509Certificate != null;
					if (flag3)
					{
						CWLogger.Logger.Debug("Common.Security.Saml:GetCertsForDecryptionV2:locatedCertificate:serial={0}", text);
						securityTokens.Add(new X509SecurityToken(x509Certificate));
						return true;
					}
					CWLogger.Logger.Debug("Common.Security.Saml:GetCertsForDecryptionV2:Failed to locate cert");
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Debug("Common.Security.Saml:GetCertsForDecryptionV2:error={0}", ex.ToString());
					return false;
				}
				finally
				{
					this.CertStoreManager.CloseSupportedStores(list);
				}
			}
			securityTokens = new List<SecurityToken>();
			return false;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004B14 File Offset: 0x00002D14
		protected virtual bool GetCertsForValidatingSignature(XmlElement securityTokenXml, out List<SecurityToken> securityTokens)
		{
			CWLogger.Logger.Debug("GetCertsForValidatingSignature:Start:securityTokenXml={0}", ((securityTokenXml != null) ? securityTokenXml.ToString() : null) ?? "NULL");
			securityTokens = new List<SecurityToken>();
			try
			{
				SignedXml signedXml = new SignedXml(securityTokenXml);
				CWLogger.Logger.Debug("GetCertsForValidatingSignature:signedXml={0}", signedXml.ToString());
				XmlElement xmlElement = securityTokenXml["ds:Signature"] ?? securityTokenXml["Signature"];
				CWLogger.Logger.Debug("GetCertsForValidatingSignature:ds:Signature={0}", ((xmlElement != null) ? xmlElement.ToString() : null) ?? "NULL");
				signedXml.LoadXml(xmlElement);
				XmlElement xmlElement2 = signedXml.KeyInfo.GetXml()["X509Data"];
				string text;
				if (xmlElement2 == null)
				{
					text = null;
				}
				else
				{
					XmlElement xmlElement3 = xmlElement2["X509Certificate"];
					text = ((xmlElement3 != null) ? xmlElement3.InnerText : null);
				}
				string text2 = text;
				CWLogger.Logger.Debug("GetCertsForValidatingSignature:ds:certValue={0}", ((text2 != null) ? text2.ToString() : null) ?? "NULL");
				bool flag = !string.IsNullOrEmpty(text2);
				if (flag)
				{
					X509Certificate2 x509Certificate = new X509Certificate2();
					x509Certificate.Import(Convert.FromBase64String(text2));
					IList<X509Store> list = this.CertStoreManager.OpenSupportedStores();
					try
					{
						X509Certificate2 x509Certificate2 = this.CertStoreManager.LookupCertFromSupportedStores(list, X509FindType.FindByThumbprint, x509Certificate.Thumbprint);
						bool flag2 = x509Certificate2 != null;
						if (flag2)
						{
							securityTokens.Add(new X509SecurityToken(x509Certificate2));
							return true;
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Debug("GetCertsForValidatingSignature:FailedEx:ex={0}", ex.ToString());
						return false;
					}
					finally
					{
						this.CertStoreManager.CloseSupportedStores(list);
					}
				}
				securityTokens = new List<SecurityToken>();
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Debug("GetCertsForValidatingSignature:FailedEx:ex2={0}", ex2.ToString());
			}
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004D28 File Offset: 0x00002F28
		protected virtual bool GetCertsForDecryption(XmlElement securityTokenXml, out List<SecurityToken> securityTokens)
		{
			XmlNodeList elementsByTagName = securityTokenXml.GetElementsByTagName("SecurityTokenReference", WSTrustStandards.Wss10NamespaceUri);
			bool result = true;
			X509Certificate2 x509Certificate = null;
			Dictionary<string, SecurityToken> dictionary = new Dictionary<string, SecurityToken>();
			IList<X509Store> list = this.CertStoreManager.OpenSupportedStores();
			try
			{
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					bool flag = xmlNode.FirstChild.LocalName == "KeyIdentifier" && xmlNode.FirstChild.Attributes["ValueType"].Value.Contains("ThumbprintSHA1");
					if (flag)
					{
						string findValue = string.Join("", Array.ConvertAll<byte, string>(Convert.FromBase64String(xmlNode.InnerText), (byte thumbprintByte) => thumbprintByte.ToString("X2")));
						x509Certificate = this.CertStoreManager.LookupCertFromSupportedStores(list, X509FindType.FindByThumbprint, findValue);
					}
					else
					{
						bool flag2 = xmlNode.FirstChild.LocalName == "X509Data" && xmlNode.FirstChild.FirstChild.LocalName == "X509IssuerSerial";
						if (flag2)
						{
							XmlNodeList childNodes = xmlNode.FirstChild.FirstChild.ChildNodes;
							XmlNode xmlNode2 = null;
							XmlNode xmlNode3 = null;
							foreach (object obj2 in childNodes)
							{
								XmlNode xmlNode4 = (XmlNode)obj2;
								bool flag3 = xmlNode4.LocalName == "X509IssuerName";
								if (flag3)
								{
									xmlNode2 = xmlNode4;
								}
								else
								{
									bool flag4 = xmlNode4.LocalName == "X509SerialNumber";
									if (flag4)
									{
										xmlNode3 = xmlNode4;
									}
								}
							}
							bool flag5 = xmlNode2 != null && xmlNode3 != null;
							if (flag5)
							{
								X509Certificate2Collection x509Certificate2Collection = this.CertStoreManager.LookupCertsFromSupportedStores(list, X509FindType.FindBySerialNumber, xmlNode3.InnerText);
								bool flag6 = x509Certificate2Collection != null && x509Certificate2Collection.Count > 0;
								if (flag6)
								{
									foreach (X509Certificate2 x509Certificate2 in x509Certificate2Collection)
									{
										bool flag7 = x509Certificate2.IssuerName.Name == xmlNode2.InnerText || Regex.Replace(x509Certificate2.IssuerName.Name, ",\\s+", ",") == xmlNode2.InnerText;
										if (flag7)
										{
											x509Certificate = x509Certificate2;
											break;
										}
									}
								}
							}
						}
					}
					bool flag8 = x509Certificate != null;
					if (flag8)
					{
						bool flag9 = !dictionary.ContainsKey(x509Certificate.Thumbprint);
						if (flag9)
						{
							dictionary.Add(x509Certificate.Thumbprint, new X509SecurityToken(x509Certificate));
						}
					}
					else
					{
						result = false;
					}
					x509Certificate = null;
				}
			}
			catch (Exception ex)
			{
				result = false;
			}
			finally
			{
				this.CertStoreManager.CloseSupportedStores(list);
			}
			securityTokens = dictionary.Values.ToList<SecurityToken>();
			return result;
		}

		// Token: 0x060000F0 RID: 240
		protected abstract SecurityToken DecryptToken(List<SecurityToken> tokens, XmlElement securityTokenXml);

		// Token: 0x060000F1 RID: 241
		protected abstract GenericXmlSecurityToken CreateGenericXmlToken(XmlElement securityTokenXml, SecurityToken proofToken, SecurityKeyIdentifierClause internalTokenReference, SecurityKeyIdentifierClause externalTokenReference);

		// Token: 0x060000F2 RID: 242
		public abstract bool VerifySamlBearerTokenSignature(XmlElement securityTokenElement, SecurityToken securityToken, List<SecurityToken> tokens, SecurityTokenElement tokenIssuer);

		// Token: 0x060000F3 RID: 243
		public abstract bool VerifySamlBearerEncryptedTokenSignature(XmlElement securityTokenElement, SecurityToken securityToken, List<SecurityToken> tokens, SecurityTokenElement tokenIssuer);

		// Token: 0x060000F4 RID: 244 RVA: 0x00005090 File Offset: 0x00003290
		public virtual SecurityToken DeserializeToken(XmlElement securityTokenXml, out List<SecurityToken> tokens)
		{
			bool certsForValidatingSignature = this.GetCertsForValidatingSignature(securityTokenXml, out tokens);
			SecurityToken result;
			try
			{
				result = this.DecryptToken(tokens, securityTokenXml);
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000050CC File Offset: 0x000032CC
		public virtual SecurityToken DeserializeEncryptedToken(XmlElement securityTokenXml, out List<SecurityToken> tokens)
		{
			bool certsForDecryptionV = this.GetCertsForDecryptionV2(securityTokenXml, out tokens);
			SecurityToken result;
			try
			{
				result = this.DecryptToken(tokens, securityTokenXml);
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005108 File Offset: 0x00003308
		public virtual SecurityToken DeserializeToken(XmlElement securityTokenXml, SecurityToken proofToken, SecurityKeyIdentifierClause attachedReference, SecurityKeyIdentifierClause unattachedReference)
		{
			List<SecurityToken> tokens;
			bool certsForDecryption = this.GetCertsForDecryption(securityTokenXml, out tokens);
			bool flag = certsForDecryption;
			SecurityToken result;
			if (flag)
			{
				result = this.DecryptToken(tokens, securityTokenXml);
			}
			else
			{
				result = this.CreateGenericXmlToken(securityTokenXml, proofToken, attachedReference, unattachedReference);
			}
			return result;
		}

		// Token: 0x04000054 RID: 84
		private ICertStoreManager _certStoreManager;
	}
}
