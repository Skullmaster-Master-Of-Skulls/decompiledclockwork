using System;
using System.Collections.Generic;
using System.Deployment.Internal.CodeSigning;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.Xml;
using ClockWorkLogger;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200001C RID: 28
	public class Saml2XmlSerializer : TokenXmlSerializer
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005140 File Offset: 0x00003340
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000516A File Offset: 0x0000336A
		protected ServiceTokenProvider ServiceTokenProvider
		{
			get
			{
				ServiceTokenProvider result;
				if ((result = this._serviceTokenProvider) == null)
				{
					result = (this._serviceTokenProvider = new ServiceTokenProvider());
				}
				return result;
			}
			set
			{
				this._serviceTokenProvider = value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005174 File Offset: 0x00003374
		protected override GenericXmlSecurityToken CreateGenericXmlToken(XmlElement securityTokenXml, SecurityToken proofToken, SecurityKeyIdentifierClause internalTokenReference, SecurityKeyIdentifierClause externalTokenReference)
		{
			XmlNodeList elementsByTagName = securityTokenXml.GetElementsByTagName("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion");
			DateTime effectiveTime = DateTime.MinValue;
			DateTime expirationTime = DateTime.MaxValue;
			bool flag = elementsByTagName.Count > 0 && elementsByTagName[0].Attributes != null && elementsByTagName[0].Attributes.Count > 0;
			if (flag)
			{
				XmlAttribute xmlAttribute = elementsByTagName[0].Attributes["NotBefore"];
				XmlAttribute xmlAttribute2 = elementsByTagName[0].Attributes["NotOnOrAfter"];
				bool flag2 = xmlAttribute != null && xmlAttribute.Value != null;
				if (flag2)
				{
					effectiveTime = Convert.ToDateTime(xmlAttribute.Value);
				}
				bool flag3 = xmlAttribute2 != null && xmlAttribute2.Value != null;
				if (flag3)
				{
					expirationTime = Convert.ToDateTime(xmlAttribute2.Value);
				}
			}
			return new GenericXmlSecurityToken(securityTokenXml, proofToken, effectiveTime, expirationTime, internalTokenReference, externalTokenReference, null);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005260 File Offset: 0x00003460
		protected override SecurityToken DecryptToken(List<SecurityToken> tokens, XmlElement securityTokenXml)
		{
			XmlReader reader = new XmlTextReader(new StringReader(securityTokenXml.OuterXml));
			Saml2SecurityTokenHandler saml2SecurityTokenHandler = new Saml2SecurityTokenHandler
			{
				Configuration = new SecurityTokenHandlerConfiguration
				{
					IssuerTokenResolver = new IssuerTokenResolver(SecurityTokenResolver.CreateDefaultSecurityTokenResolver(tokens.AsReadOnly(), true)),
					ServiceTokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(tokens.AsReadOnly(), true)
				}
			};
			return saml2SecurityTokenHandler.ReadToken(reader);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000052CC File Offset: 0x000034CC
		public override bool VerifySamlBearerTokenSignature(XmlElement securityTokenXml, SecurityToken securityToken, List<SecurityToken> tokens, SecurityTokenElement tokenIssuer)
		{
			bool result;
			try
			{
				Saml2SecurityToken saml2SecurityToken = securityToken as Saml2SecurityToken;
				bool flag = saml2SecurityToken == null || saml2SecurityToken.Assertion.Subject == null || saml2SecurityToken.Assertion.Subject.SubjectConfirmations == null || saml2SecurityToken.Assertion.Subject.SubjectConfirmations.Count == 0 || saml2SecurityToken.Assertion.Subject.SubjectConfirmations[0].Method.AbsoluteUri != "urn:oasis:names:tc:SAML:2.0:cm:bearer" || tokens == null || tokens.Count != 1;
				if (flag)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "Common.Security.Saml:VerifySamlBearerTokenSignature:Failed1:saml20SecurityToken={0}:.Assertion.Subject={1}:.SubjectConfirmations={2}:Bearer={3}:tokensCount={4}";
					object[] array = new object[5];
					array[0] = ((saml2SecurityToken == null) ? "NULL" : "not null");
					int num = 1;
					bool flag2;
					if (saml2SecurityToken == null)
					{
						flag2 = (null != null);
					}
					else
					{
						Saml2Assertion assertion = saml2SecurityToken.Assertion;
						flag2 = (((assertion != null) ? assertion.Subject : null) != null);
					}
					array[num] = ((!flag2) ? "NULL" : "not null");
					int num2 = 2;
					bool flag3;
					if (saml2SecurityToken == null)
					{
						flag3 = (null != null);
					}
					else
					{
						Saml2Assertion assertion2 = saml2SecurityToken.Assertion;
						if (assertion2 == null)
						{
							flag3 = (null != null);
						}
						else
						{
							Saml2Subject subject = assertion2.Subject;
							flag3 = (((subject != null) ? subject.SubjectConfirmations : null) != null);
						}
					}
					object obj;
					if (flag3)
					{
						obj = string.Join(" | ", saml2SecurityToken.Assertion.Subject.SubjectConfirmations.Select(delegate(Saml2SubjectConfirmation h)
						{
							string text3;
							if (h == null)
							{
								text3 = null;
							}
							else
							{
								Uri method = h.Method;
								text3 = ((method != null) ? method.AbsoluteUri : null);
							}
							return text3 ?? "NULL";
						}).ToArray<string>());
					}
					else
					{
						obj = "NULL";
					}
					array[num2] = obj;
					array[3] = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
					array[4] = (((tokens != null) ? tokens.Count.ToString() : null) ?? "NULL");
					logger.Error(message, array);
					result = false;
				}
				else
				{
					DateTime utcNow = DateTime.UtcNow;
					DateTime validTo = saml2SecurityToken.ValidTo;
					DateTime validFrom = saml2SecurityToken.ValidFrom;
					bool flag4 = (validTo != DateTime.MaxValue && validTo != DateTime.MinValue && validTo < utcNow.AddMinutes(-1.0)) || validFrom > utcNow.AddMinutes(1.0);
					if (flag4)
					{
						CWLogger.Logger.Warn(string.Format("TokenXmlSerializer:Saml2Response Security Token is no longer valid:validFrom={0}:validTo={1}", saml2SecurityToken.ValidFrom, saml2SecurityToken.ValidTo));
						result = false;
					}
					else
					{
						CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), new string[]
						{
							"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"
						});
						SignedXml signedXml = new SignedXml(securityTokenXml);
						try
						{
							signedXml.LoadXml(securityTokenXml["Signature"]);
						}
						catch
						{
							signedXml.LoadXml(securityTokenXml["ds:Signature"]);
						}
						XmlElement xmlElement = signedXml.KeyInfo.GetXml()["X509Data"];
						string text;
						if (xmlElement == null)
						{
							text = null;
						}
						else
						{
							XmlElement xmlElement2 = xmlElement["X509Certificate"];
							text = ((xmlElement2 != null) ? xmlElement2.InnerText : null);
						}
						string text2 = text;
						bool flag5 = string.IsNullOrEmpty(text2);
						if (flag5)
						{
							CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:failed2:empty certvalue:securityTokenXml={0}", ((securityTokenXml != null) ? securityTokenXml.ToString() : null) ?? "NULL");
							result = false;
						}
						else
						{
							X509Certificate2 x509Certificate = new X509Certificate2();
							x509Certificate.Import(Convert.FromBase64String(text2));
							bool flag6 = !signedXml.CheckSignature(x509Certificate, true);
							if (flag6)
							{
								CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:failed3:failed CheckSignature");
								result = false;
							}
							else
							{
								string value = saml2SecurityToken.Assertion.Issuer.Value;
								X509SecurityToken issuerServiceToken = this.ServiceTokenProvider.GetIssuerServiceToken(new EndpointAddress(value), tokenIssuer);
								bool flag7 = (tokens[0] as X509SecurityToken).Certificate.SubjectName.Name == issuerServiceToken.Certificate.SubjectName.Name;
								if (flag7)
								{
									result = true;
								}
								else
								{
									CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:failed4:(tokens[0] as X509SecurityToken).Certificate.SubjectName.Name={0}:signCert.Certificate.SubjectName.Name={1}", (tokens[0] as X509SecurityToken).Certificate.SubjectName.Name ?? "NULL", issuerServiceToken.Certificate.SubjectName.Name ?? "NULL");
									result = false;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:err={0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005704 File Offset: 0x00003904
		public override bool VerifySamlBearerEncryptedTokenSignature(XmlElement securityTokenXml, SecurityToken securityToken, List<SecurityToken> tokens, SecurityTokenElement tokenIssuer)
		{
			bool result;
			try
			{
				Saml2SecurityToken saml2SecurityToken = securityToken as Saml2SecurityToken;
				bool flag = saml2SecurityToken == null || saml2SecurityToken.Assertion.Subject == null || saml2SecurityToken.Assertion.Subject.SubjectConfirmations == null || saml2SecurityToken.Assertion.Subject.SubjectConfirmations.Count == 0 || saml2SecurityToken.Assertion.Subject.SubjectConfirmations[0].Method.AbsoluteUri != "urn:oasis:names:tc:SAML:2.0:cm:bearer" || tokens == null || tokens.Count != 1;
				if (flag)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "Common.Security.Saml:VerifySamlBearerTokenSignature:Failed1:saml20SecurityToken={0}:.Assertion.Subject={1}:.SubjectConfirmations={2}:Bearer={3}:tokensCount={4}";
					object[] array = new object[5];
					array[0] = ((saml2SecurityToken == null) ? "NULL" : "not null");
					int num = 1;
					bool flag2;
					if (saml2SecurityToken == null)
					{
						flag2 = (null != null);
					}
					else
					{
						Saml2Assertion assertion = saml2SecurityToken.Assertion;
						flag2 = (((assertion != null) ? assertion.Subject : null) != null);
					}
					array[num] = ((!flag2) ? "NULL" : "not null");
					int num2 = 2;
					bool flag3;
					if (saml2SecurityToken == null)
					{
						flag3 = (null != null);
					}
					else
					{
						Saml2Assertion assertion2 = saml2SecurityToken.Assertion;
						if (assertion2 == null)
						{
							flag3 = (null != null);
						}
						else
						{
							Saml2Subject subject = assertion2.Subject;
							flag3 = (((subject != null) ? subject.SubjectConfirmations : null) != null);
						}
					}
					object obj;
					if (flag3)
					{
						obj = string.Join(" | ", saml2SecurityToken.Assertion.Subject.SubjectConfirmations.Select(delegate(Saml2SubjectConfirmation h)
						{
							string text;
							if (h == null)
							{
								text = null;
							}
							else
							{
								Uri method = h.Method;
								text = ((method != null) ? method.AbsoluteUri : null);
							}
							return text ?? "NULL";
						}).ToArray<string>());
					}
					else
					{
						obj = "NULL";
					}
					array[num2] = obj;
					array[3] = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
					array[4] = (((tokens != null) ? tokens.Count.ToString() : null) ?? "NULL");
					logger.Error(message, array);
					result = false;
				}
				else
				{
					string value = saml2SecurityToken.Assertion.Issuer.Value;
					X509SecurityToken issuerServiceToken = this.ServiceTokenProvider.GetIssuerServiceToken(new EndpointAddress(value), tokenIssuer);
					Saml2SecurityToken saml2SecurityToken2 = securityToken as Saml2SecurityToken;
					X509SecurityToken x509SecurityToken = saml2SecurityToken2.IssuerToken as X509SecurityToken;
					bool flag4 = ((x509SecurityToken != null) ? x509SecurityToken.Certificate.SubjectName.Name : null) == issuerServiceToken.Certificate.SubjectName.Name;
					if (flag4)
					{
						result = true;
					}
					else
					{
						CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:failed4:(tokens[0] as X509SecurityToken).Certificate.SubjectName.Name={0}:signCert.Certificate.SubjectName.Name={1}", (tokens[0] as X509SecurityToken).Certificate.SubjectName.Name ?? "NULL", issuerServiceToken.Certificate.SubjectName.Name ?? "NULL");
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Security.Saml:VerifySamlBearerTokenSignature:err={0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x04000055 RID: 85
		private ServiceTokenProvider _serviceTokenProvider;

		// Token: 0x04000056 RID: 86
		public const string Bearer = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
	}
}
