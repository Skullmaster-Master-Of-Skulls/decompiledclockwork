using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Selectors;
using System.Runtime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000196 RID: 406
	public class X509SecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x0003DD8B File Offset: 0x0003BF8B
		public X509SecurityTokenHandler() : this(false, null)
		{
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0003DD95 File Offset: 0x0003BF95
		public X509SecurityTokenHandler(X509CertificateValidator certificateValidator) : this(false, certificateValidator)
		{
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003DD9F File Offset: 0x0003BF9F
		public X509SecurityTokenHandler(bool mapToWindows) : this(mapToWindows, null)
		{
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003DDA9 File Offset: 0x0003BFA9
		public X509SecurityTokenHandler(bool mapToWindows, X509CertificateValidator certificateValidator)
		{
			this.mapToWindows = mapToWindows;
			this.certificateValidator = certificateValidator;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003DDD8 File Offset: 0x0003BFD8
		public override void LoadCustomConfiguration(XmlNodeList customConfigElements)
		{
			if (customConfigElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("customConfigElements");
			}
			List<XmlElement> xmlElements = XmlUtil.GetXmlElements(customConfigElements);
			bool flag = false;
			bool flag2 = false;
			X509RevocationMode revocationMode = X509SecurityTokenHandler.defaultRevocationMode;
			X509CertificateValidationMode x509CertificateValidationMode = X509SecurityTokenHandler.defaultValidationMode;
			StoreLocation trustedStoreLocation = X509SecurityTokenHandler.defaultStoreLocation;
			string text = null;
			foreach (XmlElement xmlElement in xmlElements)
			{
				if (StringComparer.Ordinal.Equals(xmlElement.LocalName, "x509SecurityTokenHandlerRequirement"))
				{
					if (flag)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7026", new object[]
						{
							"x509SecurityTokenHandlerRequirement"
						}));
					}
					foreach (object obj in xmlElement.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "mapToWindows"))
						{
							this.mapToWindows = XmlConvert.ToBoolean(xmlAttribute.Value.ToLowerInvariant());
						}
						else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "certificateValidator"))
						{
							text = xmlAttribute.Value.ToString();
						}
						else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "revocationMode"))
						{
							flag2 = true;
							string x = xmlAttribute.Value.ToString();
							if (StringComparer.OrdinalIgnoreCase.Equals(x, "NoCheck"))
							{
								revocationMode = X509RevocationMode.NoCheck;
							}
							else if (StringComparer.OrdinalIgnoreCase.Equals(x, "Offline"))
							{
								revocationMode = X509RevocationMode.Offline;
							}
							else
							{
								if (!StringComparer.OrdinalIgnoreCase.Equals(x, "Online"))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
									{
										xmlAttribute.LocalName,
										xmlElement.LocalName
									})));
								}
								revocationMode = X509RevocationMode.Online;
							}
						}
						else if (StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "certificateValidationMode"))
						{
							flag2 = true;
							string x2 = xmlAttribute.Value.ToString();
							if (StringComparer.OrdinalIgnoreCase.Equals(x2, "ChainTrust"))
							{
								x509CertificateValidationMode = X509CertificateValidationMode.ChainTrust;
							}
							else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "PeerOrChainTrust"))
							{
								x509CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
							}
							else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "PeerTrust"))
							{
								x509CertificateValidationMode = X509CertificateValidationMode.PeerTrust;
							}
							else if (StringComparer.OrdinalIgnoreCase.Equals(x2, "None"))
							{
								x509CertificateValidationMode = X509CertificateValidationMode.None;
							}
							else
							{
								if (!StringComparer.OrdinalIgnoreCase.Equals(x2, "Custom"))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
									{
										xmlAttribute.LocalName,
										xmlElement.LocalName
									})));
								}
								x509CertificateValidationMode = X509CertificateValidationMode.Custom;
							}
						}
						else
						{
							if (!StringComparer.OrdinalIgnoreCase.Equals(xmlAttribute.LocalName, "trustedStoreLocation"))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7004", new object[]
								{
									xmlAttribute.LocalName,
									xmlElement.LocalName
								})));
							}
							flag2 = true;
							string x3 = xmlAttribute.Value.ToString();
							if (StringComparer.OrdinalIgnoreCase.Equals(x3, "CurrentUser"))
							{
								trustedStoreLocation = StoreLocation.CurrentUser;
							}
							else
							{
								if (!StringComparer.OrdinalIgnoreCase.Equals(x3, "LocalMachine"))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID7011", new object[]
									{
										xmlAttribute.LocalName,
										xmlElement.LocalName
									})));
								}
								trustedStoreLocation = StoreLocation.LocalMachine;
							}
						}
					}
					flag = true;
				}
			}
			if (x509CertificateValidationMode != X509CertificateValidationMode.Custom)
			{
				if (flag2)
				{
					this.certificateValidator = X509Util.CreateCertificateValidator(x509CertificateValidationMode, revocationMode, trustedStoreLocation);
				}
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7028"));
			}
			Type type = Type.GetType(text, true);
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID7007", new object[]
				{
					type
				}));
			}
			this.certificateValidator = CustomTypeElement.Resolve<X509CertificateValidator>(new CustomTypeElement(type));
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0003E234 File Offset: 0x0003C434
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0003E23C File Offset: 0x0003C43C
		public bool MapToWindows
		{
			get
			{
				return this.mapToWindows;
			}
			set
			{
				this.mapToWindows = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003E245 File Offset: 0x0003C445
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0003E26B File Offset: 0x0003C46B
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				if (this.certificateValidator != null)
				{
					return this.certificateValidator;
				}
				if (base.Configuration != null)
				{
					return base.Configuration.CertificateValidator;
				}
				return null;
			}
			set
			{
				this.certificateValidator = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003E274 File Offset: 0x0003C474
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0003E27C File Offset: 0x0003C47C
		public X509NTAuthChainTrustValidator X509NTAuthChainTrustValidator
		{
			get
			{
				return this.x509NTAuthChainTrustValidator;
			}
			set
			{
				this.x509NTAuthChainTrustValidator = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003E285 File Offset: 0x0003C485
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0003E28D File Offset: 0x0003C48D
		public bool WriteXmlDSigDefinedClauseTypes
		{
			get
			{
				return this.writeXmlDSigDefinedClauseTypes;
			}
			set
			{
				this.writeXmlDSigDefinedClauseTypes = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003E296 File Offset: 0x0003C496
		public override bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.x509DataKeyIdentifierClauseSerializer.CanReadKeyIdentifierClause(reader);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003E2B8 File Offset: 0x0003C4B8
		public override bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement("BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
			{
				string attribute = reader.GetAttribute("ValueType", null);
				return StringComparer.Ordinal.Equals(attribute, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
			}
			return false;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003E309 File Offset: 0x0003C509
		public override bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifierClause");
			}
			return this.writeXmlDSigDefinedClauseTypes && this.x509DataKeyIdentifierClauseSerializer.CanWriteKeyIdentifierClause(securityKeyIdentifierClause);
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003E334 File Offset: 0x0003C534
		public override Type TokenType
		{
			get
			{
				return typeof(X509SecurityToken);
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003E340 File Offset: 0x0003C540
		public override SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.x509DataKeyIdentifierClauseSerializer.ReadKeyIdentifierClause(reader);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003E364 File Offset: 0x0003C564
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			if (!xmlDictionaryReader.IsStartElement("BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"BinarySecurityToken",
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
					xmlDictionaryReader.LocalName,
					xmlDictionaryReader.NamespaceURI
				})));
			}
			string attribute = xmlDictionaryReader.GetAttribute("ValueType", null);
			if (!StringComparer.Ordinal.Equals(attribute, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4066", new object[]
				{
					"BinarySecurityToken",
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
					"ValueType",
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3",
					attribute
				})));
			}
			string attribute2 = xmlDictionaryReader.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			string attribute3 = xmlDictionaryReader.GetAttribute("EncodingType", null);
			byte[] rawData;
			if (attribute3 == null || StringComparer.Ordinal.Equals(attribute3, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"))
			{
				rawData = xmlDictionaryReader.ReadElementContentAsBase64();
			}
			else
			{
				if (!StringComparer.Ordinal.Equals(attribute3, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4068")));
				}
				rawData = SoapHexBinary.Parse(xmlDictionaryReader.ReadElementContentAsString()).Value;
			}
			X509Helper.VerifyNotPfx(rawData);
			if (!string.IsNullOrEmpty(attribute2))
			{
				return new X509SecurityToken(new X509Certificate2(rawData), attribute2);
			}
			return new X509SecurityToken(new X509Certificate2(rawData));
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003E4E9 File Offset: 0x0003C6E9
		public override string[] GetTokenTypeIdentifiers()
		{
			return new string[]
			{
				SecurityTokenTypes.X509Certificate
			};
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003E4FC File Offset: 0x0003C6FC
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			X509SecurityToken x509SecurityToken = token as X509SecurityToken;
			if (x509SecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(X509SecurityToken)
				}));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				try
				{
					this.CertificateValidator.Validate(x509SecurityToken.Certificate);
				}
				catch (SecurityTokenValidationException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4257", new object[]
					{
						X509Util.GetCertificateId(x509SecurityToken.Certificate)
					}), innerException));
				}
				if (base.Configuration.IssuerNameRegistry == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4277"));
				}
				string certificateIssuerName = X509Util.GetCertificateIssuerName(x509SecurityToken.Certificate, base.Configuration.IssuerNameRegistry);
				if (string.IsNullOrEmpty(certificateIssuerName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4175")));
				}
				ClaimsIdentity claimsIdentity;
				if (!this.mapToWindows)
				{
					claimsIdentity = new ClaimsIdentity("X509");
					claimsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509"));
				}
				else
				{
					X509WindowsSecurityToken x509WindowsSecurityToken = token as X509WindowsSecurityToken;
					WindowsIdentity windowsIdentity;
					if (x509WindowsSecurityToken != null && x509WindowsSecurityToken.WindowsIdentity != null)
					{
						windowsIdentity = new WindowsIdentity(x509WindowsSecurityToken.WindowsIdentity.Token, x509WindowsSecurityToken.AuthenticationType);
					}
					else
					{
						if (this.x509NTAuthChainTrustValidator == null)
						{
							object obj = this.lockObject;
							lock (obj)
							{
								if (this.x509NTAuthChainTrustValidator == null)
								{
									this.x509NTAuthChainTrustValidator = new X509NTAuthChainTrustValidator();
								}
							}
						}
						this.x509NTAuthChainTrustValidator.Validate(x509SecurityToken.Certificate);
						windowsIdentity = ClaimsHelper.CertificateLogon(x509SecurityToken.Certificate);
					}
					windowsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509"));
					claimsIdentity = windowsIdentity;
				}
				if (base.Configuration.SaveBootstrapContext)
				{
					claimsIdentity.BootstrapContext = new BootstrapContext(token, this);
				}
				claimsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime"));
				claimsIdentity.AddClaims(X509Util.GetClaimsFromCertificate(x509SecurityToken.Certificate, certificateIssuerName));
				base.TraceTokenValidationSuccess(token);
				result = new List<ClaimsIdentity>(1)
				{
					claimsIdentity
				}.AsReadOnly();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.TraceTokenValidationFailure(token, ex.Message);
				throw ex;
			}
			return result;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		public override void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifierClause");
			}
			if (!this.writeXmlDSigDefinedClauseTypes)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4261"));
			}
			this.x509DataKeyIdentifierClauseSerializer.WriteKeyIdentifierClause(writer, securityKeyIdentifierClause);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003E810 File Offset: 0x0003CA10
		public override void WriteToken(XmlWriter writer, SecurityToken token)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			X509SecurityToken x509SecurityToken = token as X509SecurityToken;
			if (x509SecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(X509SecurityToken)
				}));
			}
			writer.WriteStartElement("BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			if (!string.IsNullOrEmpty(x509SecurityToken.Id))
			{
				writer.WriteAttributeString("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", x509SecurityToken.Id);
			}
			writer.WriteAttributeString("ValueType", null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
			writer.WriteAttributeString("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
			byte[] rawCertData = x509SecurityToken.Certificate.GetRawCertData();
			writer.WriteBase64(rawCertData, 0, rawCertData.Length);
			writer.WriteEndElement();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003E8EB File Offset: 0x0003CAEB
		internal static WindowsIdentity KerberosCertificateLogon(X509Certificate2 certificate)
		{
			return X509SecurityTokenAuthenticator.KerberosCertificateLogon(certificate);
		}

		// Token: 0x04000CBC RID: 3260
		private static X509RevocationMode defaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04000CBD RID: 3261
		private static X509CertificateValidationMode defaultValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04000CBE RID: 3262
		private static StoreLocation defaultStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04000CBF RID: 3263
		private X509NTAuthChainTrustValidator x509NTAuthChainTrustValidator;

		// Token: 0x04000CC0 RID: 3264
		private object lockObject = new object();

		// Token: 0x04000CC1 RID: 3265
		private bool mapToWindows;

		// Token: 0x04000CC2 RID: 3266
		private X509CertificateValidator certificateValidator;

		// Token: 0x04000CC3 RID: 3267
		private bool writeXmlDSigDefinedClauseTypes;

		// Token: 0x04000CC4 RID: 3268
		private X509DataSecurityKeyIdentifierClauseSerializer x509DataKeyIdentifierClauseSerializer = new X509DataSecurityKeyIdentifierClauseSerializer();
	}
}
