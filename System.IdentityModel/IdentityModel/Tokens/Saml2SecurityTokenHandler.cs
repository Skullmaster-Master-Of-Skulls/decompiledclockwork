using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000143 RID: 323
	public class Saml2SecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0002531A File Offset: 0x0002351A
		public Saml2SecurityTokenHandler() : this(new SamlSecurityTokenRequirement())
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00025327 File Offset: 0x00023527
		public Saml2SecurityTokenHandler(SamlSecurityTokenRequirement samlSecurityTokenRequirement)
		{
			if (samlSecurityTokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlSecurityTokenRequirement");
			}
			this.samlSecurityTokenRequirement = samlSecurityTokenRequirement;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00025354 File Offset: 0x00023554
		public override void LoadCustomConfiguration(XmlNodeList customConfigElements)
		{
			if (customConfigElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("customConfigElements");
			}
			List<XmlElement> xmlElements = XmlUtil.GetXmlElements(customConfigElements);
			bool flag = false;
			foreach (XmlElement xmlElement in xmlElements)
			{
				if (!(xmlElement.LocalName != "samlSecurityTokenRequirement"))
				{
					if (flag)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7026", new object[]
						{
							"samlSecurityTokenRequirement"
						}));
					}
					this.samlSecurityTokenRequirement = new SamlSecurityTokenRequirement(xmlElement);
					flag = true;
				}
			}
			if (!flag)
			{
				this.samlSecurityTokenRequirement = new SamlSecurityTokenRequirement();
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00025408 File Offset: 0x00023608
		public override Type TokenType
		{
			get
			{
				return typeof(Saml2SecurityToken);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00025414 File Offset: 0x00023614
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00025444 File Offset: 0x00023644
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				if (this.samlSecurityTokenRequirement.CertificateValidator != null)
				{
					return this.samlSecurityTokenRequirement.CertificateValidator;
				}
				if (base.Configuration != null)
				{
					return base.Configuration.CertificateValidator;
				}
				return null;
			}
			set
			{
				this.samlSecurityTokenRequirement.CertificateValidator = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00025454 File Offset: 0x00023654
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000254C8 File Offset: 0x000236C8
		public SecurityTokenSerializer KeyInfoSerializer
		{
			get
			{
				if (this.keyInfoSerializer == null)
				{
					object obj = this.syncObject;
					lock (obj)
					{
						if (this.keyInfoSerializer == null)
						{
							SecurityTokenHandlerCollection securityTokenHandlerCollection = (base.ContainingCollection != null) ? base.ContainingCollection : SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
							this.keyInfoSerializer = new SecurityTokenSerializerAdapter(securityTokenHandlerCollection);
						}
					}
				}
				return this.keyInfoSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.keyInfoSerializer = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000254E4 File Offset: 0x000236E4
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x000254EC File Offset: 0x000236EC
		public SamlSecurityTokenRequirement SamlSecurityTokenRequirement
		{
			get
			{
				return this.samlSecurityTokenRequirement;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.samlSecurityTokenRequirement = value;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00025508 File Offset: 0x00023708
		public override SecurityKeyIdentifierClause CreateSecurityTokenReference(SecurityToken token, bool attached)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return token.CreateKeyIdentifierClause<Saml2AssertionKeyIdentifierClause>();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00025524 File Offset: 0x00023724
		public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			Saml2Assertion saml2Assertion = new Saml2Assertion(this.CreateIssuerNameIdentifier(tokenDescriptor));
			saml2Assertion.Subject = this.CreateSamlSubject(tokenDescriptor);
			saml2Assertion.SigningCredentials = this.GetSigningCredentials(tokenDescriptor);
			saml2Assertion.Conditions = this.CreateConditions(tokenDescriptor.Lifetime, tokenDescriptor.AppliesToAddress, tokenDescriptor);
			saml2Assertion.Advice = this.CreateAdvice(tokenDescriptor);
			IEnumerable<Saml2Statement> enumerable = this.CreateStatements(tokenDescriptor);
			if (enumerable != null)
			{
				foreach (Saml2Statement item in enumerable)
				{
					saml2Assertion.Statements.Add(item);
				}
			}
			saml2Assertion.EncryptingCredentials = this.GetEncryptingCredentials(tokenDescriptor);
			return new Saml2SecurityToken(saml2Assertion);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000255F4 File Offset: 0x000237F4
		public override string[] GetTokenTypeIdentifiers()
		{
			return Saml2SecurityTokenHandler.tokenTypeIdentifiers;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000255FC File Offset: 0x000237FC
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			Saml2SecurityToken saml2SecurityToken = token as Saml2SecurityToken;
			if (saml2SecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID4151"));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 786438, SR.GetString("TraceValidateToken"), new SecurityTraceRecordHelper.TokenTraceRecord(token), null, null);
				if (saml2SecurityToken.IssuerToken == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4152")));
				}
				if (saml2SecurityToken.Assertion == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID1034"));
				}
				this.ValidateConditions(saml2SecurityToken.Assertion.Conditions, this.SamlSecurityTokenRequirement.ShouldEnforceAudienceRestriction(base.Configuration.AudienceRestriction.AudienceMode, saml2SecurityToken));
				if (base.Configuration.DetectReplayedTokens)
				{
					this.DetectReplayedToken(saml2SecurityToken);
				}
				Saml2SubjectConfirmation saml2SubjectConfirmation = saml2SecurityToken.Assertion.Subject.SubjectConfirmations[0];
				if (saml2SubjectConfirmation.SubjectConfirmationData != null)
				{
					this.ValidateConfirmationData(saml2SubjectConfirmation.SubjectConfirmationData);
				}
				X509SecurityToken x509SecurityToken = saml2SecurityToken.IssuerToken as X509SecurityToken;
				if (x509SecurityToken != null)
				{
					this.CertificateValidator.Validate(x509SecurityToken.Certificate);
				}
				ClaimsIdentity claimsIdentity = null;
				if (this.samlSecurityTokenRequirement.MapToWindows)
				{
					claimsIdentity = this.CreateWindowsIdentity(this.FindUpn(claimsIdentity));
					claimsIdentity.AddClaims(this.CreateClaims(saml2SecurityToken).Claims);
				}
				else
				{
					claimsIdentity = this.CreateClaims(saml2SecurityToken);
				}
				if (base.Configuration.SaveBootstrapContext)
				{
					claimsIdentity.BootstrapContext = new BootstrapContext(token, this);
				}
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

		// Token: 0x0600093A RID: 2362 RVA: 0x000257F8 File Offset: 0x000239F8
		protected virtual WindowsIdentity CreateWindowsIdentity(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("upn");
			}
			WindowsIdentity windowsIdentity = new WindowsIdentity(upn);
			return new WindowsIdentity(windowsIdentity.Token, "Federation", WindowsAccountType.Normal, true);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00025834 File Offset: 0x00023A34
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
			Saml2SecurityToken saml2SecurityToken = token as Saml2SecurityToken;
			if (saml2SecurityToken != null)
			{
				this.WriteAssertion(writer, saml2SecurityToken.Assertion);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID4160"));
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00025898 File Offset: 0x00023A98
		public override bool CanReadToken(XmlReader reader)
		{
			return reader != null && (reader.IsStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion") || reader.IsStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion"));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000258C3 File Offset: 0x00023AC3
		public override bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			return Saml2SecurityTokenHandler.IsSaml2KeyIdentifierClause(reader);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000258CB File Offset: 0x00023ACB
		public override bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			return securityKeyIdentifierClause is Saml2AssertionKeyIdentifierClause || securityKeyIdentifierClause is WrappedSaml2AssertionKeyIdentifierClause;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000258E0 File Offset: 0x00023AE0
		public override SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!Saml2SecurityTokenHandler.IsSaml2KeyIdentifierClause(reader))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4161"));
			}
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
				{
					"SecurityTokenReference",
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"
				}));
			}
			SecurityKeyIdentifierClause result;
			try
			{
				byte[] array = null;
				int derivationLength = 0;
				string attribute = reader.GetAttribute("Nonce", "http://schemas.xmlsoap.org/ws/2005/02/sc");
				if (!string.IsNullOrEmpty(attribute))
				{
					array = Convert.FromBase64String(attribute);
					attribute = reader.GetAttribute("Length", "http://schemas.xmlsoap.org/ws/2005/02/sc");
					if (!string.IsNullOrEmpty(attribute))
					{
						derivationLength = XmlConvert.ToInt32(attribute);
					}
					else
					{
						derivationLength = 32;
					}
				}
				if (array == null)
				{
					attribute = reader.GetAttribute("Nonce", "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");
					if (!string.IsNullOrEmpty(attribute))
					{
						array = Convert.FromBase64String(attribute);
						attribute = reader.GetAttribute("Length", "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");
						if (!string.IsNullOrEmpty(attribute))
						{
							derivationLength = XmlConvert.ToInt32(attribute);
						}
						else
						{
							derivationLength = 32;
						}
					}
				}
				reader.Read();
				if (reader.IsStartElement("Reference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4126"));
				}
				if (!reader.IsStartElement("KeyIdentifier", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					reader.ReadStartElement("KeyIdentifier", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
				}
				attribute = reader.GetAttribute("ValueType");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"ValueType",
						"KeyIdentifier"
					}));
				}
				if (!StringComparer.Ordinal.Equals("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID", attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4127", new object[]
					{
						attribute
					}));
				}
				string id = reader.ReadElementString();
				reader.ReadEndElement();
				result = new Saml2AssertionKeyIdentifierClause(id, array, derivationLength);
			}
			catch (Exception ex)
			{
				if (ex is FormatException || ex is ArgumentException || ex is InvalidOperationException || ex is OverflowException)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4125"), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00025B00 File Offset: 0x00023D00
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4275"));
			}
			if (base.Configuration.ServiceTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4276"));
			}
			Saml2SecurityTokenHandler.t_currentAssertionDepth = 0;
			KeyInfo.ResetReadDepth();
			System.IdentityModel.Tokens.KeyInfoSerializer.ResetReadDepth();
			Saml2Assertion assertion = this.ReadAssertion(reader);
			ReadOnlyCollection<SecurityKey> keys = this.ResolveSecurityKeys(assertion, base.Configuration.ServiceTokenResolver);
			SecurityToken issuerToken;
			this.TryResolveIssuerToken(assertion, base.Configuration.IssuerTokenResolver, out issuerToken);
			return new Saml2SecurityToken(assertion, keys, issuerToken);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00025BA8 File Offset: 0x00023DA8
		public override void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			WrappedSaml2AssertionKeyIdentifierClause wrappedSaml2AssertionKeyIdentifierClause = securityKeyIdentifierClause as WrappedSaml2AssertionKeyIdentifierClause;
			Saml2AssertionKeyIdentifierClause saml2AssertionKeyIdentifierClause;
			if (wrappedSaml2AssertionKeyIdentifierClause != null)
			{
				saml2AssertionKeyIdentifierClause = wrappedSaml2AssertionKeyIdentifierClause.WrappedClause;
			}
			else
			{
				saml2AssertionKeyIdentifierClause = (securityKeyIdentifierClause as Saml2AssertionKeyIdentifierClause);
			}
			if (saml2AssertionKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("keyIdentifierClause", SR.GetString("ID4162"));
			}
			writer.WriteStartElement("SecurityTokenReference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			byte[] derivationNonce = saml2AssertionKeyIdentifierClause.GetDerivationNonce();
			if (derivationNonce != null)
			{
				writer.WriteAttributeString("Nonce", "http://schemas.xmlsoap.org/ws/2005/02/sc", Convert.ToBase64String(derivationNonce));
				int derivationLength = saml2AssertionKeyIdentifierClause.DerivationLength;
				if (derivationLength != 0 && derivationLength != 32)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4129")));
				}
			}
			writer.WriteAttributeString("TokenType", "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd", "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0");
			writer.WriteStartElement("KeyIdentifier", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			writer.WriteAttributeString("ValueType", "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID");
			writer.WriteString(saml2AssertionKeyIdentifierClause.Id);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00025CBC File Offset: 0x00023EBC
		internal static XmlDictionaryReader CreatePlaintextReaderFromEncryptedData(XmlDictionaryReader reader, SecurityTokenResolver serviceTokenResolver, SecurityTokenSerializer keyInfoSerializer, Collection<EncryptedKeyIdentifierClause> clauses, out EncryptingCredentials encryptingCredentials)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				}));
			}
			encryptingCredentials = null;
			XmlUtil.ValidateXsiType(reader, "EncryptedElementType", "urn:oasis:names:tc:SAML:2.0:assertion");
			reader.ReadStartElement();
			EncryptedDataElement encryptedDataElement = new EncryptedDataElement(keyInfoSerializer);
			encryptedDataElement.ReadXml(reader);
			reader.MoveToContent();
			while (reader.IsStartElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#"))
			{
				SecurityKeyIdentifierClause securityKeyIdentifierClause;
				if (keyInfoSerializer.CanReadKeyIdentifierClause(reader))
				{
					securityKeyIdentifierClause = keyInfoSerializer.ReadKeyIdentifierClause(reader);
				}
				else
				{
					EncryptedKeyElement encryptedKeyElement = new EncryptedKeyElement(keyInfoSerializer);
					encryptedKeyElement.ReadXml(reader);
					securityKeyIdentifierClause = encryptedKeyElement.GetClause();
				}
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = securityKeyIdentifierClause as EncryptedKeyIdentifierClause;
				if (encryptedKeyIdentifierClause == null)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4172"));
				}
				clauses.Add(encryptedKeyIdentifierClause);
			}
			reader.ReadEndElement();
			SecurityKey securityKey = null;
			SecurityKeyIdentifierClause securityKeyIdentifierClause2 = null;
			foreach (SecurityKeyIdentifierClause securityKeyIdentifierClause3 in encryptedDataElement.KeyIdentifier)
			{
				if (serviceTokenResolver.TryResolveSecurityKey(securityKeyIdentifierClause3, out securityKey))
				{
					securityKeyIdentifierClause2 = securityKeyIdentifierClause3;
					break;
				}
			}
			if (securityKey == null)
			{
				foreach (SecurityKeyIdentifierClause securityKeyIdentifierClause4 in clauses)
				{
					if (serviceTokenResolver.TryResolveSecurityKey(securityKeyIdentifierClause4, out securityKey))
					{
						securityKeyIdentifierClause2 = securityKeyIdentifierClause4;
						break;
					}
				}
			}
			if (securityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EncryptedTokenDecryptionFailedException());
			}
			SymmetricSecurityKey symmetricSecurityKey = securityKey as SymmetricSecurityKey;
			if (symmetricSecurityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4023")));
			}
			SymmetricAlgorithm symmetricAlgorithm = symmetricSecurityKey.GetSymmetricAlgorithm(encryptedDataElement.Algorithm);
			byte[] buffer = encryptedDataElement.Decrypt(symmetricAlgorithm);
			encryptingCredentials = new Saml2SecurityTokenHandler.ReceivedEncryptingCredentials(securityKey, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				securityKeyIdentifierClause2
			}), encryptedDataElement.Algorithm);
			return XmlDictionaryReader.CreateTextReader(buffer, reader.Quotas);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001EF60 File Offset: 0x0001D160
		internal static Exception TryWrapReadException(XmlReader reader, Exception inner)
		{
			if (inner is FormatException || inner is ArgumentException || inner is InvalidOperationException || inner is OverflowException)
			{
				return DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4125"), inner);
			}
			return null;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00025EC8 File Offset: 0x000240C8
		internal static bool IsSaml2KeyIdentifierClause(XmlReader reader)
		{
			if (!reader.IsStartElement("SecurityTokenReference", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
			{
				return false;
			}
			string attribute = reader.GetAttribute("TokenType", "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd");
			return Saml2SecurityTokenHandler.tokenTypeIdentifiers.Contains(attribute);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00025F05 File Offset: 0x00024105
		internal static bool IsSaml2Assertion(XmlReader reader)
		{
			return reader.IsStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion") || reader.IsStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion");
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00025F2C File Offset: 0x0002412C
		internal static void ReadEmptyContentElement(XmlReader reader)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			reader.Read();
			if (!isEmptyElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00025F50 File Offset: 0x00024150
		internal static Saml2Id ReadSimpleNCNameElement(XmlReader reader)
		{
			Saml2Id result;
			try
			{
				if (reader.IsEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}));
				}
				XmlUtil.ValidateXsiType(reader, "NCName", "http://www.w3.org/2001/XMLSchema");
				reader.MoveToElement();
				string value = reader.ReadElementContentAsString();
				result = new Saml2Id(value);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00025FE0 File Offset: 0x000241E0
		internal static Uri ReadSimpleUriElement(XmlReader reader)
		{
			return Saml2SecurityTokenHandler.ReadSimpleUriElement(reader, UriKind.Absolute);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00025FE9 File Offset: 0x000241E9
		internal static Uri ReadSimpleUriElement(XmlReader reader, UriKind kind)
		{
			return Saml2SecurityTokenHandler.ReadSimpleUriElement(reader, kind, false);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00025FF4 File Offset: 0x000241F4
		internal static Uri ReadSimpleUriElement(XmlReader reader, UriKind kind, bool allowLaxReading)
		{
			Uri result;
			try
			{
				if (reader.IsEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}));
				}
				XmlUtil.ValidateXsiType(reader, "anyURI", "http://www.w3.org/2001/XMLSchema");
				reader.MoveToElement();
				string text = reader.ReadElementContentAsString();
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0022"));
				}
				if (!allowLaxReading && !UriUtil.CanCreateValidUri(text, kind))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString((kind == UriKind.RelativeOrAbsolute) ? "ID0019" : "ID0013"));
				}
				result = new Uri(text, kind);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000260C8 File Offset: 0x000242C8
		protected virtual Saml2Conditions CreateConditions(Lifetime tokenLifetime, string relyingPartyAddress, SecurityTokenDescriptor tokenDescriptor)
		{
			bool flag = tokenLifetime != null;
			bool flag2 = !string.IsNullOrEmpty(relyingPartyAddress);
			if (!flag && !flag2)
			{
				return null;
			}
			Saml2Conditions saml2Conditions = new Saml2Conditions();
			if (flag)
			{
				saml2Conditions.NotBefore = tokenLifetime.Created;
				saml2Conditions.NotOnOrAfter = tokenLifetime.Expires;
			}
			if (flag2)
			{
				saml2Conditions.AudienceRestrictions.Add(new Saml2AudienceRestriction(new Uri(relyingPartyAddress)));
			}
			return saml2Conditions;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00003459 File Offset: 0x00001659
		protected virtual Saml2Advice CreateAdvice(SecurityTokenDescriptor tokenDescriptor)
		{
			return null;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00026128 File Offset: 0x00024328
		protected virtual Saml2NameIdentifier CreateIssuerNameIdentifier(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			string tokenIssuerName = tokenDescriptor.TokenIssuerName;
			if (string.IsNullOrEmpty(tokenIssuerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4138")));
			}
			return new Saml2NameIdentifier(tokenIssuerName);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00026178 File Offset: 0x00024378
		protected virtual Saml2Attribute CreateAttribute(Claim claim, SecurityTokenDescriptor tokenDescriptor)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			Saml2Attribute saml2Attribute = new Saml2Attribute(claim.Type, claim.Value);
			if (!StringComparer.Ordinal.Equals("LOCAL AUTHORITY", claim.OriginalIssuer))
			{
				saml2Attribute.OriginalIssuer = claim.OriginalIssuer;
			}
			saml2Attribute.AttributeValueXsiType = claim.ValueType;
			if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/attributename"))
			{
				string uriString = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/attributename"];
				if (!UriUtil.CanCreateValidUri(uriString, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("nameFormat", SR.GetString("ID0013"));
				}
				saml2Attribute.NameFormat = new Uri(uriString);
			}
			if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/displayname"))
			{
				saml2Attribute.FriendlyName = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/displayname"];
			}
			return saml2Attribute;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00026254 File Offset: 0x00024454
		protected virtual Saml2AttributeStatement CreateAttributeStatement(ClaimsIdentity subject, SecurityTokenDescriptor tokenDescriptor)
		{
			if (subject == null)
			{
				return null;
			}
			if (subject.Claims != null)
			{
				List<Saml2Attribute> list = new List<Saml2Attribute>();
				foreach (Claim claim in subject.Claims)
				{
					if (claim != null && claim.Type != "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
					{
						string type = claim.Type;
						if (!(type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant") && !(type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod"))
						{
							list.Add(this.CreateAttribute(claim, tokenDescriptor));
						}
					}
				}
				this.AddDelegateToAttributes(subject, list, tokenDescriptor);
				ICollection<Saml2Attribute> collection = this.CollectAttributeValues(list);
				if (collection.Count > 0)
				{
					return new Saml2AttributeStatement(collection);
				}
			}
			return null;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002631C File Offset: 0x0002451C
		protected virtual ICollection<Saml2Attribute> CollectAttributeValues(ICollection<Saml2Attribute> attributes)
		{
			Dictionary<SamlAttributeKeyComparer.AttributeKey, Saml2Attribute> dictionary = new Dictionary<SamlAttributeKeyComparer.AttributeKey, Saml2Attribute>(attributes.Count, new SamlAttributeKeyComparer());
			foreach (Saml2Attribute saml2Attribute in attributes)
			{
				if (saml2Attribute != null)
				{
					SamlAttributeKeyComparer.AttributeKey key = new SamlAttributeKeyComparer.AttributeKey(saml2Attribute);
					if (dictionary.ContainsKey(key))
					{
						using (IEnumerator<string> enumerator2 = saml2Attribute.Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string item = enumerator2.Current;
								dictionary[key].Values.Add(item);
							}
							continue;
						}
					}
					dictionary.Add(key, saml2Attribute);
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000263E0 File Offset: 0x000245E0
		protected virtual void AddDelegateToAttributes(ClaimsIdentity subject, ICollection<Saml2Attribute> attributes, SecurityTokenDescriptor tokenDescriptor)
		{
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			if (subject.Actor == null)
			{
				return;
			}
			List<Saml2Attribute> list = new List<Saml2Attribute>();
			foreach (Claim claim in subject.Actor.Claims)
			{
				if (claim != null)
				{
					list.Add(this.CreateAttribute(claim, tokenDescriptor));
				}
			}
			this.AddDelegateToAttributes(subject.Actor, list, tokenDescriptor);
			ICollection<Saml2Attribute> attributes2 = this.CollectAttributeValues(list);
			attributes.Add(this.CreateAttribute(new Claim("http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor", this.CreateXmlStringFromAttributes(attributes2), "http://www.w3.org/2001/XMLSchema#string"), tokenDescriptor));
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000264AC File Offset: 0x000246AC
		protected virtual string CreateXmlStringFromAttributes(IEnumerable<Saml2Attribute> attributes)
		{
			bool flag = false;
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
				{
					foreach (Saml2Attribute saml2Attribute in attributes)
					{
						if (saml2Attribute != null)
						{
							if (!flag)
							{
								xmlDictionaryWriter.WriteStartElement("Actor");
								flag = true;
							}
							this.WriteAttribute(xmlDictionaryWriter, saml2Attribute);
						}
					}
					if (flag)
					{
						xmlDictionaryWriter.WriteEndElement();
					}
					xmlDictionaryWriter.Flush();
				}
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00026574 File Offset: 0x00024774
		protected virtual IEnumerable<Saml2Statement> CreateStatements(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			Collection<Saml2Statement> collection = new Collection<Saml2Statement>();
			Saml2AttributeStatement saml2AttributeStatement = this.CreateAttributeStatement(tokenDescriptor.Subject, tokenDescriptor);
			if (saml2AttributeStatement != null)
			{
				collection.Add(saml2AttributeStatement);
			}
			Saml2AuthenticationStatement saml2AuthenticationStatement = this.CreateAuthenticationStatement(tokenDescriptor.AuthenticationInfo, tokenDescriptor);
			if (saml2AuthenticationStatement != null)
			{
				collection.Add(saml2AuthenticationStatement);
			}
			return collection;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000265CC File Offset: 0x000247CC
		protected virtual Saml2AuthenticationStatement CreateAuthenticationStatement(AuthenticationInformation authInfo, SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			if (tokenDescriptor.Subject == null)
			{
				return null;
			}
			string text = null;
			string text2 = null;
			IEnumerable<Claim> source = from c in tokenDescriptor.Subject.Claims
			where c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod"
			select c;
			if (source.Count<Claim>() > 0)
			{
				text = source.First<Claim>().Value;
			}
			source = from c in tokenDescriptor.Subject.Claims
			where c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant"
			select c;
			if (source.Count<Claim>() > 0)
			{
				text2 = source.First<Claim>().Value;
			}
			if (text == null && text2 == null)
			{
				return null;
			}
			if (text == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4270", new object[]
				{
					"AuthenticationMethod",
					"SAML2"
				}));
			}
			if (text2 == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4270", new object[]
				{
					"AuthenticationInstant",
					"SAML2"
				}));
			}
			Uri classReference;
			if (!UriUtil.TryCreateValidUri(this.DenormalizeAuthenticationType(text), UriKind.Absolute, out classReference))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4185", new object[]
				{
					text
				}));
			}
			Saml2AuthenticationContext authenticationContext = new Saml2AuthenticationContext(classReference);
			DateTime authenticationInstant = DateTime.ParseExact(text2, DateTimeFormats.Accepted, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
			Saml2AuthenticationStatement saml2AuthenticationStatement = new Saml2AuthenticationStatement(authenticationContext, authenticationInstant);
			if (authInfo != null)
			{
				if (!string.IsNullOrEmpty(authInfo.DnsName) || !string.IsNullOrEmpty(authInfo.Address))
				{
					saml2AuthenticationStatement.SubjectLocality = new Saml2SubjectLocality(authInfo.Address, authInfo.DnsName);
				}
				if (!string.IsNullOrEmpty(authInfo.Session))
				{
					saml2AuthenticationStatement.SessionIndex = authInfo.Session;
				}
				saml2AuthenticationStatement.SessionNotOnOrAfter = authInfo.NotOnOrAfter;
			}
			return saml2AuthenticationStatement;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00026798 File Offset: 0x00024998
		protected virtual Saml2Subject CreateSamlSubject(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			Saml2Subject saml2Subject = new Saml2Subject();
			string text = null;
			string text2 = null;
			string nameQualifier = null;
			string spprovidedId = null;
			string spnameQualifier = null;
			if (tokenDescriptor.Subject != null && tokenDescriptor.Subject.Claims != null)
			{
				foreach (Claim claim in tokenDescriptor.Subject.Claims)
				{
					if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
					{
						if (text != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4139")));
						}
						text = claim.Value;
						if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"))
						{
							text2 = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"];
						}
						if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"))
						{
							nameQualifier = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"];
						}
						if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spnamequalifier"))
						{
							spnameQualifier = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spnamequalifier"];
						}
						if (claim.Properties.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spprovidedid"))
						{
							spprovidedId = claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spprovidedid"];
						}
					}
				}
			}
			if (text != null)
			{
				Saml2NameIdentifier saml2NameIdentifier = new Saml2NameIdentifier(text);
				if (text2 != null && UriUtil.CanCreateValidUri(text2, UriKind.Absolute))
				{
					saml2NameIdentifier.Format = new Uri(text2);
				}
				saml2NameIdentifier.NameQualifier = nameQualifier;
				saml2NameIdentifier.SPNameQualifier = spnameQualifier;
				saml2NameIdentifier.SPProvidedId = spprovidedId;
				saml2Subject.NameId = saml2NameIdentifier;
			}
			Saml2SubjectConfirmation saml2SubjectConfirmation;
			if (tokenDescriptor.Proof == null)
			{
				saml2SubjectConfirmation = new Saml2SubjectConfirmation(Saml2Constants.ConfirmationMethods.Bearer);
			}
			else
			{
				saml2SubjectConfirmation = new Saml2SubjectConfirmation(Saml2Constants.ConfirmationMethods.HolderOfKey, new Saml2SubjectConfirmationData());
				saml2SubjectConfirmation.SubjectConfirmationData.KeyIdentifiers.Add(tokenDescriptor.Proof.KeyIdentifier);
			}
			saml2Subject.SubjectConfirmations.Add(saml2SubjectConfirmation);
			return saml2Subject;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00026998 File Offset: 0x00024B98
		protected virtual EncryptingCredentials GetEncryptingCredentials(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			EncryptingCredentials encryptingCredentials = null;
			if (tokenDescriptor.EncryptingCredentials != null)
			{
				encryptingCredentials = tokenDescriptor.EncryptingCredentials;
				if (encryptingCredentials.SecurityKey is AsymmetricSecurityKey)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4178")));
				}
			}
			return encryptingCredentials;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000269F1 File Offset: 0x00024BF1
		protected virtual SigningCredentials GetSigningCredentials(SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenDescriptor");
			}
			return tokenDescriptor.SigningCredentials;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00026A0C File Offset: 0x00024C0C
		protected virtual void ValidateConditions(Saml2Conditions conditions, bool enforceAudienceRestriction)
		{
			if (conditions != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (conditions.NotBefore != null && DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew) < conditions.NotBefore.Value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenNotYetValidException(SR.GetString("ID4147", new object[]
					{
						conditions.NotBefore.Value,
						utcNow
					})));
				}
				if (conditions.NotOnOrAfter != null && DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew.Negate()) >= conditions.NotOnOrAfter.Value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenExpiredException(SR.GetString("ID4148", new object[]
					{
						conditions.NotOnOrAfter.Value,
						utcNow
					})));
				}
				if (conditions.OneTimeUse)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4149")));
				}
				if (conditions.ProxyRestriction != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4150")));
				}
			}
			if (enforceAudienceRestriction)
			{
				if (base.Configuration == null || base.Configuration.AudienceRestriction.AllowedAudienceUris.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID1032")));
				}
				if (conditions == null || conditions.AudienceRestrictions.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AudienceUriValidationFailedException(SR.GetString("ID1035")));
				}
				foreach (Saml2AudienceRestriction saml2AudienceRestriction in conditions.AudienceRestrictions)
				{
					this.SamlSecurityTokenRequirement.ValidateAudienceRestriction(base.Configuration.AudienceRestriction.AllowedAudienceUris, saml2AudienceRestriction.Audiences);
				}
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00026C20 File Offset: 0x00024E20
		protected virtual string FindUpn(ClaimsIdentity claimsIdentity)
		{
			return ClaimsHelper.FindUpn(claimsIdentity);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00026C28 File Offset: 0x00024E28
		protected virtual string DenormalizeAuthenticationType(string normalizedAuthenticationType)
		{
			return AuthenticationTypeMaps.Denormalize(normalizedAuthenticationType, AuthenticationTypeMaps.Saml2);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00026C38 File Offset: 0x00024E38
		protected override void DetectReplayedToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			Saml2SecurityToken saml2SecurityToken = token as Saml2SecurityToken;
			if (saml2SecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID1064", new object[]
				{
					token.GetType().ToString()
				}));
			}
			if (saml2SecurityToken.SecurityKeys.Count != 0)
			{
				return;
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.Caches.TokenReplayCache == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4278"));
			}
			if (string.IsNullOrEmpty(saml2SecurityToken.Assertion.Id.Value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID1065")));
			}
			StringBuilder stringBuilder = new StringBuilder();
			string key;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.NewSha256HashAlgorithm())
			{
				if (string.IsNullOrEmpty(saml2SecurityToken.Assertion.Issuer.Value))
				{
					stringBuilder.AppendFormat("{0}{1}", saml2SecurityToken.Assertion.Id.Value, Saml2SecurityTokenHandler.tokenTypeIdentifiers[0]);
				}
				else
				{
					stringBuilder.AppendFormat("{0}{1}{2}", saml2SecurityToken.Assertion.Id.Value, saml2SecurityToken.Assertion.Issuer.Value, Saml2SecurityTokenHandler.tokenTypeIdentifiers[0]);
				}
				key = Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
			}
			if (base.Configuration.Caches.TokenReplayCache.Contains(key))
			{
				string text = (saml2SecurityToken.Assertion.Issuer.Value != null) ? saml2SecurityToken.Assertion.Issuer.Value : string.Empty;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenReplayDetectedException(SR.GetString("ID1066", new object[]
				{
					typeof(Saml2SecurityToken).ToString(),
					saml2SecurityToken.Assertion.Id.Value,
					text
				})));
			}
			base.Configuration.Caches.TokenReplayCache.AddOrUpdate(key, token, DateTimeUtil.Add(this.GetTokenReplayCacheEntryExpirationTime(saml2SecurityToken), base.Configuration.MaxClockSkew));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00026E78 File Offset: 0x00025078
		protected virtual DateTime GetTokenReplayCacheEntryExpirationTime(Saml2SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			DateTime? dateTime = null;
			Saml2Assertion assertion = token.Assertion;
			if (assertion != null)
			{
				if (assertion.Conditions != null && assertion.Conditions.NotOnOrAfter != null)
				{
					dateTime = new DateTime?(assertion.Conditions.NotOnOrAfter.Value);
				}
				else if (assertion.Subject != null && assertion.Subject.SubjectConfirmations != null && assertion.Subject.SubjectConfirmations.Count != 0 && assertion.Subject.SubjectConfirmations[0].SubjectConfirmationData != null && assertion.Subject.SubjectConfirmations[0].SubjectConfirmationData.NotOnOrAfter != null)
				{
					dateTime = new DateTime?(assertion.Subject.SubjectConfirmations[0].SubjectConfirmationData.NotOnOrAfter.Value);
				}
			}
			DateTime t = DateTimeUtil.Add(DateTime.UtcNow, base.Configuration.TokenReplayCacheExpirationPeriod);
			dateTime = new DateTime?(dateTime ?? DateTime.MaxValue);
			if (DateTime.Compare(t, dateTime.Value) < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID1069", new object[]
				{
					dateTime.Value.ToString(),
					base.Configuration.TokenReplayCacheExpirationPeriod.ToString()
				})));
			}
			return dateTime.Value;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00027017 File Offset: 0x00025217
		protected virtual string NormalizeAuthenticationContextClassReference(string saml2AuthenticationContextClassReference)
		{
			return AuthenticationTypeMaps.Normalize(saml2AuthenticationContextClassReference, AuthenticationTypeMaps.Saml2);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00027024 File Offset: 0x00025224
		protected virtual void ProcessSamlSubject(Saml2Subject assertionSubject, ClaimsIdentity subject, string issuer)
		{
			if (assertionSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertionSubject");
			}
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			Saml2NameIdentifier nameId = assertionSubject.NameId;
			if (nameId != null)
			{
				Claim claim = new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", nameId.Value, "http://www.w3.org/2001/XMLSchema#string", issuer);
				if (nameId.Format != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"] = nameId.Format.AbsoluteUri;
				}
				if (nameId.NameQualifier != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"] = nameId.NameQualifier;
				}
				if (nameId.SPNameQualifier != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spnamequalifier"] = nameId.SPNameQualifier;
				}
				if (nameId.SPProvidedId != null)
				{
					claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/spprovidedid"] = nameId.SPProvidedId;
				}
				subject.AddClaim(claim);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00027108 File Offset: 0x00025308
		protected virtual void ProcessAttributeStatement(Saml2AttributeStatement statement, ClaimsIdentity subject, string issuer)
		{
			if (statement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("statement");
			}
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			foreach (Saml2Attribute saml2Attribute in statement.Attributes)
			{
				if (StringComparer.Ordinal.Equals(saml2Attribute.Name, "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor"))
				{
					if (subject.Actor != null)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4218"));
					}
					this.SetDelegateFromAttribute(saml2Attribute, subject, issuer);
				}
				else
				{
					foreach (string text in saml2Attribute.Values)
					{
						if (text != null)
						{
							string originalIssuer = issuer;
							if (saml2Attribute.OriginalIssuer != null)
							{
								originalIssuer = saml2Attribute.OriginalIssuer;
							}
							Claim claim = new Claim(saml2Attribute.Name, text, saml2Attribute.AttributeValueXsiType, issuer, originalIssuer);
							if (saml2Attribute.NameFormat != null)
							{
								claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/attributename"] = saml2Attribute.NameFormat.AbsoluteUri;
							}
							if (saml2Attribute.FriendlyName != null)
							{
								claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/displayname"] = saml2Attribute.FriendlyName;
							}
							subject.AddClaim(claim);
						}
					}
				}
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00027288 File Offset: 0x00025488
		protected virtual void SetDelegateFromAttribute(Saml2Attribute attribute, ClaimsIdentity subject, string issuer)
		{
			if (subject == null || attribute == null || attribute.Values == null || attribute.Values.Count < 1)
			{
				return;
			}
			Saml2Attribute saml2Attribute = null;
			Collection<Claim> collection = new Collection<Claim>();
			foreach (string text in attribute.Values)
			{
				if (text != null)
				{
					using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(Encoding.UTF8.GetBytes(text), BoundedXmlDictionaryReaderQuotas.Quotas))
					{
						xmlDictionaryReader.MoveToContent();
						xmlDictionaryReader.ReadStartElement("Actor");
						while (xmlDictionaryReader.IsStartElement("Attribute"))
						{
							Saml2Attribute saml2Attribute2 = this.ReadAttribute(xmlDictionaryReader);
							if (saml2Attribute2 != null)
							{
								if (saml2Attribute2.Name == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
								{
									if (saml2Attribute != null)
									{
										throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4218"));
									}
									saml2Attribute = saml2Attribute2;
								}
								else
								{
									string originalIssuer = saml2Attribute2.OriginalIssuer;
									for (int i = 0; i < saml2Attribute2.Values.Count; i++)
									{
										Claim claim;
										if (string.IsNullOrEmpty(originalIssuer))
										{
											claim = new Claim(saml2Attribute2.Name, saml2Attribute2.Values[i], saml2Attribute2.AttributeValueXsiType, issuer);
										}
										else
										{
											claim = new Claim(saml2Attribute2.Name, saml2Attribute2.Values[i], saml2Attribute2.AttributeValueXsiType, issuer, originalIssuer);
										}
										if (saml2Attribute2.NameFormat != null)
										{
											claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/attributename"] = saml2Attribute2.NameFormat.AbsoluteUri;
										}
										if (saml2Attribute2.FriendlyName != null)
										{
											claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/displayname"] = saml2Attribute2.FriendlyName;
										}
										collection.Add(claim);
									}
								}
							}
						}
						xmlDictionaryReader.ReadEndElement();
					}
				}
			}
			subject.Actor = new ClaimsIdentity(collection, "Federation");
			this.SetDelegateFromAttribute(saml2Attribute, subject.Actor, issuer);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000274B0 File Offset: 0x000256B0
		protected virtual void ProcessAuthenticationStatement(Saml2AuthenticationStatement statement, ClaimsIdentity subject, string issuer)
		{
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subject");
			}
			if (statement.AuthenticationContext.DeclarationReference != null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4180"));
			}
			if (statement.AuthenticationContext.ClassReference != null)
			{
				subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", this.NormalizeAuthenticationContextClassReference(statement.AuthenticationContext.ClassReference.AbsoluteUri), "http://www.w3.org/2001/XMLSchema#string", issuer));
			}
			subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(statement.AuthenticationInstant.ToUniversalTime(), DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime", issuer));
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ProcessAuthorizationDecisionStatement(Saml2AuthorizationDecisionStatement statement, ClaimsIdentity subject, string issuer)
		{
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00027560 File Offset: 0x00025760
		protected virtual void ProcessStatement(Collection<Saml2Statement> statements, ClaimsIdentity subject, string issuer)
		{
			Collection<Saml2AuthenticationStatement> collection = new Collection<Saml2AuthenticationStatement>();
			foreach (Saml2Statement saml2Statement in statements)
			{
				Saml2AttributeStatement saml2AttributeStatement = saml2Statement as Saml2AttributeStatement;
				if (saml2AttributeStatement != null)
				{
					this.ProcessAttributeStatement(saml2AttributeStatement, subject, issuer);
				}
				else
				{
					Saml2AuthenticationStatement saml2AuthenticationStatement = saml2Statement as Saml2AuthenticationStatement;
					if (saml2AuthenticationStatement != null)
					{
						collection.Add(saml2AuthenticationStatement);
					}
					else
					{
						Saml2AuthorizationDecisionStatement saml2AuthorizationDecisionStatement = saml2Statement as Saml2AuthorizationDecisionStatement;
						if (saml2AuthorizationDecisionStatement != null)
						{
							this.ProcessAuthorizationDecisionStatement(saml2AuthorizationDecisionStatement, subject, issuer);
						}
					}
				}
			}
			foreach (Saml2AuthenticationStatement saml2AuthenticationStatement2 in collection)
			{
				if (saml2AuthenticationStatement2 != null)
				{
					this.ProcessAuthenticationStatement(saml2AuthenticationStatement2, subject, issuer);
				}
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002762C File Offset: 0x0002582C
		protected virtual ClaimsIdentity CreateClaims(Saml2SecurityToken samlToken)
		{
			if (samlToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("samlToken");
			}
			ClaimsIdentity claimsIdentity = new ClaimsIdentity("Federation", this.SamlSecurityTokenRequirement.NameClaimType, this.SamlSecurityTokenRequirement.RoleClaimType);
			Saml2Assertion assertion = samlToken.Assertion;
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("samlToken", SR.GetString("ID1034"));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerNameRegistry == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4277"));
			}
			string issuerName = base.Configuration.IssuerNameRegistry.GetIssuerName(samlToken.IssuerToken, assertion.Issuer.Value);
			if (string.IsNullOrEmpty(issuerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4175")));
			}
			this.ProcessSamlSubject(assertion.Subject, claimsIdentity, issuerName);
			this.ProcessStatement(assertion.Statements, claimsIdentity, issuerName);
			return claimsIdentity;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00027728 File Offset: 0x00025928
		protected virtual void ValidateConfirmationData(Saml2SubjectConfirmationData confirmationData)
		{
			if (confirmationData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("confirmationData");
			}
			if (confirmationData.Address != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4153")));
			}
			if (confirmationData.InResponseTo != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4154")));
			}
			if (null != confirmationData.Recipient)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4157")));
			}
			DateTime utcNow = DateTime.UtcNow;
			if (confirmationData.NotBefore != null && DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew) < confirmationData.NotBefore.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4176", new object[]
				{
					confirmationData.NotBefore.Value,
					utcNow
				})));
			}
			if (confirmationData.NotOnOrAfter != null && DateTimeUtil.Add(utcNow, base.Configuration.MaxClockSkew.Negate()) >= confirmationData.NotOnOrAfter.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4177", new object[]
				{
					confirmationData.NotOnOrAfter.Value,
					utcNow
				})));
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000278AC File Offset: 0x00025AAC
		protected virtual ReadOnlyCollection<SecurityKey> ResolveSecurityKeys(Saml2Assertion assertion, SecurityTokenResolver resolver)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			Saml2Subject subject = assertion.Subject;
			if (subject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4130")));
			}
			if (subject.SubjectConfirmations.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4131")));
			}
			if (subject.SubjectConfirmations.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4132")));
			}
			Saml2SubjectConfirmation saml2SubjectConfirmation = subject.SubjectConfirmations[0];
			ReadOnlyCollection<SecurityKey> result;
			if (Saml2Constants.ConfirmationMethods.Bearer == saml2SubjectConfirmation.Method)
			{
				if (saml2SubjectConfirmation.SubjectConfirmationData != null && saml2SubjectConfirmation.SubjectConfirmationData.KeyIdentifiers.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4133")));
				}
				result = EmptyReadOnlyCollection<SecurityKey>.Instance;
			}
			else
			{
				if (!(Saml2Constants.ConfirmationMethods.HolderOfKey == saml2SubjectConfirmation.Method))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4136", new object[]
					{
						saml2SubjectConfirmation.Method
					})));
				}
				if (saml2SubjectConfirmation.SubjectConfirmationData == null || saml2SubjectConfirmation.SubjectConfirmationData.KeyIdentifiers.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4134")));
				}
				List<SecurityKey> list = new List<SecurityKey>();
				foreach (SecurityKeyIdentifier securityKeyIdentifier in saml2SubjectConfirmation.SubjectConfirmationData.KeyIdentifiers)
				{
					SecurityKey securityKey = null;
					foreach (SecurityKeyIdentifierClause keyIdentifierClause in securityKeyIdentifier)
					{
						if (resolver != null && resolver.TryResolveSecurityKey(keyIdentifierClause, out securityKey))
						{
							list.Add(securityKey);
							break;
						}
					}
					if (securityKey == null)
					{
						if (securityKeyIdentifier.CanCreateKey)
						{
							securityKey = securityKeyIdentifier.CreateKey();
							list.Add(securityKey);
						}
						else
						{
							list.Add(new SecurityKeyElement(securityKeyIdentifier, resolver));
						}
					}
				}
				result = list.AsReadOnly();
			}
			return result;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00027AE8 File Offset: 0x00025CE8
		protected virtual SecurityToken ResolveIssuerToken(Saml2Assertion assertion, SecurityTokenResolver issuerResolver)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			SecurityToken result;
			if (this.TryResolveIssuerToken(assertion, issuerResolver, out result))
			{
				return result;
			}
			string @string = SR.GetString((assertion.SigningCredentials == null) ? "ID4141" : "ID4142");
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(@string));
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00027B40 File Offset: 0x00025D40
		protected virtual bool TryResolveIssuerToken(Saml2Assertion assertion, SecurityTokenResolver issuerResolver, out SecurityToken token)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			if (assertion.SigningCredentials == null || assertion.SigningCredentials.SigningKeyIdentifier == null || issuerResolver == null)
			{
				token = null;
				return false;
			}
			SecurityKeyIdentifier signingKeyIdentifier = assertion.SigningCredentials.SigningKeyIdentifier;
			if (signingKeyIdentifier.Count < 2 || LocalAppContextSwitches.ProcessMultipleSecurityKeyIdentifierClauses)
			{
				return issuerResolver.TryResolveToken(signingKeyIdentifier, out token);
			}
			return issuerResolver.TryResolveToken(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				signingKeyIdentifier[0]
			}), out token);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00027BC0 File Offset: 0x00025DC0
		protected virtual Saml2NameIdentifier ReadSubjectId(XmlReader reader, string parentElement)
		{
			if (reader.IsStartElement("NameID", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadNameId(reader);
			}
			if (reader.IsStartElement("EncryptedID", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadEncryptedId(reader);
			}
			if (!reader.IsStartElement("BaseID", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return null;
			}
			XmlQualifiedName xsiType = XmlUtil.GetXsiType(reader);
			if (null == xsiType || XmlUtil.EqualsQName(xsiType, "BaseIDAbstractType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4104", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				}));
			}
			if (XmlUtil.EqualsQName(xsiType, "NameIDType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadNameIdType(reader);
			}
			throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4110", new object[]
			{
				parentElement,
				xsiType.Name,
				xsiType.Namespace
			}));
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00027CAC File Offset: 0x00025EAC
		protected virtual Saml2Action ReadAction(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Action", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Action", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
				{
					"Action",
					"urn:oasis:names:tc:SAML:2.0:assertion"
				}));
			}
			Saml2Action result;
			try
			{
				XmlUtil.ValidateXsiType(reader, "ActionType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("Namespace");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Namespace",
						"Action"
					}));
				}
				if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
					{
						"Namespace",
						"Action"
					}));
				}
				Uri actionNamespace = new Uri(attribute);
				result = new Saml2Action(reader.ReadElementString(), actionNamespace);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00027DE0 File Offset: 0x00025FE0
		protected virtual void WriteAction(XmlWriter writer, Saml2Action data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (null == data.Namespace)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data.Namespace");
			}
			if (string.IsNullOrEmpty(data.Namespace.ToString()))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("data.Namespace");
			}
			writer.WriteStartElement("Action", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("Namespace", data.Namespace.AbsoluteUri);
			writer.WriteString(data.Value);
			writer.WriteEndElement();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00027E88 File Offset: 0x00026088
		protected virtual Saml2Advice ReadAdvice(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Advice", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Advice", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2Advice result;
			try
			{
				Saml2Advice saml2Advice = new Saml2Advice();
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AdviceType", "urn:oasis:names:tc:SAML:2.0:assertion");
				reader.Read();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("AssertionIDRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							saml2Advice.AssertionIdReferences.Add(Saml2SecurityTokenHandler.ReadSimpleNCNameElement(reader));
						}
						else if (reader.IsStartElement("AssertionURIRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							saml2Advice.AssertionUriReferences.Add(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader));
						}
						else if (reader.IsStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							saml2Advice.Assertions.Add(this.ReadAssertion(reader));
						}
						else if (reader.IsStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							saml2Advice.Assertions.Add(this.ReadAssertion(reader));
						}
						else
						{
							TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8006", new object[]
							{
								reader.LocalName,
								reader.NamespaceURI
							}), new object[0]);
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				result = saml2Advice;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002801C File Offset: 0x0002621C
		protected virtual void WriteAdvice(XmlWriter writer, Saml2Advice data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("Advice", "urn:oasis:names:tc:SAML:2.0:assertion");
			foreach (Saml2Id saml2Id in data.AssertionIdReferences)
			{
				writer.WriteElementString("AssertionIDRef", "urn:oasis:names:tc:SAML:2.0:assertion", saml2Id.Value);
			}
			foreach (Uri uri in data.AssertionUriReferences)
			{
				writer.WriteElementString("AssertionURIRef", "urn:oasis:names:tc:SAML:2.0:assertion", uri.AbsoluteUri);
			}
			foreach (Saml2Assertion data2 in data.Assertions)
			{
				this.WriteAssertion(writer, data2);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028144 File Offset: 0x00026344
		protected virtual Saml2Assertion ReadAssertion(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.IssuerTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4275"));
			}
			if (base.Configuration.ServiceTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4276"));
			}
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			Saml2Assertion saml2Assertion = new Saml2Assertion(new Saml2NameIdentifier("__TemporaryIssuer__"));
			if (reader.IsStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				EncryptingCredentials encryptingCredentials = null;
				xmlDictionaryReader = Saml2SecurityTokenHandler.CreatePlaintextReaderFromEncryptedData(xmlDictionaryReader, base.Configuration.ServiceTokenResolver, this.KeyInfoSerializer, saml2Assertion.ExternalEncryptedKeys, out encryptingCredentials);
				saml2Assertion.EncryptingCredentials = encryptingCredentials;
			}
			if (!xmlDictionaryReader.IsStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				xmlDictionaryReader.ReadStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			if (xmlDictionaryReader.IsEmptyElement)
			{
				throw DiagnosticUtility.ThrowHelperXml(xmlDictionaryReader, SR.GetString("ID3061", new object[]
				{
					xmlDictionaryReader.LocalName,
					xmlDictionaryReader.NamespaceURI
				}));
			}
			Saml2SecurityTokenHandler.WrappedSerializer securityTokenSerializer = new Saml2SecurityTokenHandler.WrappedSerializer(this, saml2Assertion);
			EnvelopedSignatureReader envelopedSignatureReader = new EnvelopedSignatureReader(xmlDictionaryReader, securityTokenSerializer, base.Configuration.IssuerTokenResolver, false, false, false);
			Saml2SecurityTokenHandler.t_currentAssertionDepth++;
			Saml2Assertion result;
			try
			{
				if (!LocalAppContextSwitches.AllowUnlimitedXmlRecursion && Saml2SecurityTokenHandler.t_currentAssertionDepth > 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4125"), new InvalidOperationException(SR.GetString("ID4194", new object[]
					{
						Saml2SecurityTokenHandler.t_currentAssertionDepth,
						8
					}))));
				}
				XmlUtil.ValidateXsiType(envelopedSignatureReader, "AssertionType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = envelopedSignatureReader.GetAttribute("Version");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(envelopedSignatureReader, SR.GetString("ID0001", new object[]
					{
						"Version",
						"Assertion"
					}));
				}
				if (!StringComparer.Ordinal.Equals(saml2Assertion.Version, attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(envelopedSignatureReader, SR.GetString("ID4100", new object[]
					{
						attribute
					}));
				}
				string attribute2 = envelopedSignatureReader.GetAttribute("ID");
				if (string.IsNullOrEmpty(attribute2))
				{
					throw DiagnosticUtility.ThrowHelperXml(envelopedSignatureReader, SR.GetString("ID0001", new object[]
					{
						"ID",
						"Assertion"
					}));
				}
				saml2Assertion.Id = new Saml2Id(attribute2);
				attribute2 = envelopedSignatureReader.GetAttribute("IssueInstant");
				if (string.IsNullOrEmpty(attribute2))
				{
					throw DiagnosticUtility.ThrowHelperXml(envelopedSignatureReader, SR.GetString("ID0001", new object[]
					{
						"IssueInstant",
						"Assertion"
					}));
				}
				saml2Assertion.IssueInstant = XmlConvert.ToDateTime(attribute2, DateTimeFormats.Accepted);
				envelopedSignatureReader.Read();
				saml2Assertion.Issuer = this.ReadIssuer(envelopedSignatureReader);
				envelopedSignatureReader.TryReadSignature();
				if (envelopedSignatureReader.IsStartElement("Subject", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2Assertion.Subject = this.ReadSubject(envelopedSignatureReader);
				}
				if (envelopedSignatureReader.IsStartElement("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2Assertion.Conditions = this.ReadConditions(envelopedSignatureReader);
				}
				if (envelopedSignatureReader.IsStartElement("Advice", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2Assertion.Advice = this.ReadAdvice(envelopedSignatureReader);
				}
				while (envelopedSignatureReader.IsStartElement())
				{
					Saml2Statement item;
					if (envelopedSignatureReader.IsStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						item = this.ReadStatement(envelopedSignatureReader);
					}
					else if (envelopedSignatureReader.IsStartElement("AttributeStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						item = this.ReadAttributeStatement(envelopedSignatureReader);
					}
					else if (envelopedSignatureReader.IsStartElement("AuthnStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						item = this.ReadAuthenticationStatement(envelopedSignatureReader);
					}
					else
					{
						if (!envelopedSignatureReader.IsStartElement("AuthzDecisionStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							break;
						}
						item = this.ReadAuthorizationDecisionStatement(envelopedSignatureReader);
					}
					saml2Assertion.Statements.Add(item);
				}
				envelopedSignatureReader.ReadEndElement();
				if (saml2Assertion.Subject == null)
				{
					if (saml2Assertion.Statements.Count == 0)
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4106"));
					}
					foreach (Saml2Statement saml2Statement in saml2Assertion.Statements)
					{
						if (saml2Statement is Saml2AuthenticationStatement || saml2Statement is Saml2AttributeStatement || saml2Statement is Saml2AuthorizationDecisionStatement)
						{
							throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4119"));
						}
					}
				}
				saml2Assertion.SigningCredentials = envelopedSignatureReader.SigningCredentials;
				saml2Assertion.CaptureSourceData(envelopedSignatureReader);
				result = saml2Assertion;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(envelopedSignatureReader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			finally
			{
				Saml2SecurityTokenHandler.t_currentAssertionDepth--;
			}
			return result;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028614 File Offset: 0x00026814
		protected virtual void WriteAssertion(XmlWriter writer, Saml2Assertion data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			XmlWriter xmlWriter = writer;
			MemoryStream memoryStream = null;
			XmlDictionaryWriter xmlDictionaryWriter = null;
			if (data.EncryptingCredentials != null && !(data.EncryptingCredentials is Saml2SecurityTokenHandler.ReceivedEncryptingCredentials))
			{
				memoryStream = new MemoryStream();
				xmlDictionaryWriter = (writer = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false));
			}
			else if (data.ExternalEncryptedKeys == null || data.ExternalEncryptedKeys.Count > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4173")));
			}
			if (data.CanWriteSourceData)
			{
				data.WriteSourceData(writer);
			}
			else
			{
				EnvelopedSignatureWriter envelopedSignatureWriter = null;
				if (data.SigningCredentials != null)
				{
					envelopedSignatureWriter = (writer = new EnvelopedSignatureWriter(writer, data.SigningCredentials, data.Id.Value, new Saml2SecurityTokenHandler.WrappedSerializer(this, data)));
				}
				if (data.Subject == null)
				{
					if (data.Statements == null || data.Statements.Count == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4106")));
					}
					foreach (Saml2Statement saml2Statement in data.Statements)
					{
						if (saml2Statement is Saml2AuthenticationStatement || saml2Statement is Saml2AttributeStatement || saml2Statement is Saml2AuthorizationDecisionStatement)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4119")));
						}
					}
				}
				writer.WriteStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion");
				writer.WriteAttributeString("ID", data.Id.Value);
				writer.WriteAttributeString("IssueInstant", XmlConvert.ToString(data.IssueInstant.ToUniversalTime(), DateTimeFormats.Generated));
				writer.WriteAttributeString("Version", data.Version);
				this.WriteIssuer(writer, data.Issuer);
				if (envelopedSignatureWriter != null)
				{
					envelopedSignatureWriter.WriteSignature();
				}
				if (data.Subject != null)
				{
					this.WriteSubject(writer, data.Subject);
				}
				if (data.Conditions != null)
				{
					this.WriteConditions(writer, data.Conditions);
				}
				if (data.Advice != null)
				{
					this.WriteAdvice(writer, data.Advice);
				}
				foreach (Saml2Statement data2 in data.Statements)
				{
					this.WriteStatement(writer, data2);
				}
				writer.WriteEndElement();
			}
			if (xmlDictionaryWriter != null)
			{
				((IDisposable)xmlDictionaryWriter).Dispose();
				xmlDictionaryWriter = null;
				EncryptedDataElement encryptedDataElement = new EncryptedDataElement();
				encryptedDataElement.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedDataElement.Algorithm = data.EncryptingCredentials.Algorithm;
				encryptedDataElement.KeyIdentifier = data.EncryptingCredentials.SecurityKeyIdentifier;
				SymmetricSecurityKey symmetricSecurityKey = data.EncryptingCredentials.SecurityKey as SymmetricSecurityKey;
				if (symmetricSecurityKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID3064")));
				}
				SymmetricAlgorithm symmetricAlgorithm = symmetricSecurityKey.GetSymmetricAlgorithm(data.EncryptingCredentials.Algorithm);
				encryptedDataElement.Encrypt(symmetricAlgorithm, memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				((IDisposable)memoryStream).Dispose();
				xmlWriter.WriteStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion");
				encryptedDataElement.WriteXml(xmlWriter, this.KeyInfoSerializer);
				foreach (EncryptedKeyIdentifierClause keyIdentifierClause in data.ExternalEncryptedKeys)
				{
					this.KeyInfoSerializer.WriteKeyIdentifierClause(xmlWriter, keyIdentifierClause);
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000289A8 File Offset: 0x00026BA8
		protected virtual Saml2Attribute ReadAttribute(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2Attribute result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AttributeType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("Name");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Name",
						"Attribute"
					}));
				}
				Saml2Attribute saml2Attribute = new Saml2Attribute(attribute);
				attribute = reader.GetAttribute("NameFormat");
				if (!string.IsNullOrEmpty(attribute))
				{
					if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
						{
							"Namespace",
							"Action"
						}));
					}
					saml2Attribute.NameFormat = new Uri(attribute);
				}
				saml2Attribute.FriendlyName = reader.GetAttribute("FriendlyName");
				string attribute2 = reader.GetAttribute("OriginalIssuer", "http://schemas.xmlsoap.org/ws/2009/09/identity/claims");
				if (attribute2 == null)
				{
					attribute2 = reader.GetAttribute("OriginalIssuer", "http://schemas.microsoft.com/ws/2008/06/identity");
				}
				if (attribute2 == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4252")));
				}
				saml2Attribute.OriginalIssuer = attribute2;
				reader.Read();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement("AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						bool isEmptyElement2 = reader.IsEmptyElement;
						bool flag = XmlUtil.IsNil(reader);
						string text = null;
						string text2 = null;
						string attribute3 = reader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
						if (!string.IsNullOrEmpty(attribute3))
						{
							if (attribute3.IndexOf(":", StringComparison.Ordinal) == -1)
							{
								text = reader.LookupNamespace(string.Empty);
								text2 = attribute3;
							}
							else if (attribute3.IndexOf(":", StringComparison.Ordinal) > 0 && attribute3.IndexOf(":", StringComparison.Ordinal) < attribute3.Length - 1)
							{
								string prefix = attribute3.Substring(0, attribute3.IndexOf(":", StringComparison.Ordinal));
								text = reader.LookupNamespace(prefix);
								text2 = attribute3.Substring(attribute3.IndexOf(":", StringComparison.Ordinal) + 1);
							}
						}
						if (text != null && text2 != null)
						{
							saml2Attribute.AttributeValueXsiType = text + "#" + text2;
						}
						if (flag)
						{
							reader.Read();
							if (!isEmptyElement2)
							{
								reader.ReadEndElement();
							}
							saml2Attribute.Values.Add(null);
						}
						else if (isEmptyElement2)
						{
							reader.Read();
							saml2Attribute.Values.Add(string.Empty);
						}
						else
						{
							saml2Attribute.Values.Add(this.ReadAttributeValue(reader, saml2Attribute));
						}
					}
					reader.ReadEndElement();
				}
				result = saml2Attribute;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028C90 File Offset: 0x00026E90
		protected virtual string ReadAttributeValue(XmlReader reader, Saml2Attribute attribute)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			reader.ReadStartElement("AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion");
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				text2 += reader.Value;
				reader.Read();
			}
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element)
			{
				while (reader.NodeType == XmlNodeType.Element)
				{
					text += reader.ReadOuterXml();
					reader.MoveToContent();
				}
			}
			else
			{
				text = text2;
				text += reader.ReadContentAsString();
			}
			reader.ReadEndElement();
			return text;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00028D34 File Offset: 0x00026F34
		protected virtual void WriteAttribute(XmlWriter writer, Saml2Attribute data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("Name", data.Name);
			if (null != data.NameFormat)
			{
				writer.WriteAttributeString("NameFormat", data.NameFormat.AbsoluteUri);
			}
			if (data.FriendlyName != null)
			{
				writer.WriteAttributeString("FriendlyName", data.FriendlyName);
			}
			if (data.OriginalIssuer != null)
			{
				writer.WriteAttributeString("OriginalIssuer", "http://schemas.xmlsoap.org/ws/2009/09/identity/claims", data.OriginalIssuer);
			}
			string text = null;
			string text2 = null;
			if (!StringComparer.Ordinal.Equals(data.AttributeValueXsiType, "http://www.w3.org/2001/XMLSchema#string"))
			{
				int num = data.AttributeValueXsiType.IndexOf('#');
				text = data.AttributeValueXsiType.Substring(0, num);
				text2 = data.AttributeValueXsiType.Substring(num + 1);
			}
			foreach (string text3 in data.Values)
			{
				writer.WriteStartElement("AttributeValue", "urn:oasis:names:tc:SAML:2.0:assertion");
				if (text3 == null)
				{
					writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", XmlConvert.ToString(true));
				}
				else if (text3.Length > 0)
				{
					if (text != null && text2 != null)
					{
						writer.WriteAttributeString("xmlns", "tn", null, text);
						writer.WriteAttributeString("type", "http://www.w3.org/2001/XMLSchema-instance", "tn:" + text2);
					}
					this.WriteAttributeValue(writer, text3, data);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00028EE8 File Offset: 0x000270E8
		protected virtual void WriteAttributeValue(XmlWriter writer, string value, Saml2Attribute attribute)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteString(value);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00028F04 File Offset: 0x00027104
		protected virtual Saml2AttributeStatement ReadAttributeStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			bool requireDeclaration = false;
			if (reader.IsStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				requireDeclaration = true;
			}
			else if (!reader.IsStartElement("AttributeStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("AttributeStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2AttributeStatement result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AttributeStatementType", "urn:oasis:names:tc:SAML:2.0:assertion", requireDeclaration);
				if (isEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						"AttributeStatement",
						"urn:oasis:names:tc:SAML:2.0:assertion"
					}));
				}
				Saml2AttributeStatement saml2AttributeStatement = new Saml2AttributeStatement();
				reader.Read();
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("EncryptedAttribute", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4158"));
					}
					if (!reader.IsStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						break;
					}
					saml2AttributeStatement.Attributes.Add(this.ReadAttribute(reader));
				}
				if (saml2AttributeStatement.Attributes.Count == 0)
				{
					reader.ReadStartElement("Attribute", "urn:oasis:names:tc:SAML:2.0:assertion");
				}
				reader.ReadEndElement();
				result = saml2AttributeStatement;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00029058 File Offset: 0x00027258
		protected virtual void WriteAttributeStatement(XmlWriter writer, Saml2AttributeStatement data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (data.Attributes == null || data.Attributes.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4124")));
			}
			writer.WriteStartElement("AttributeStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			foreach (Saml2Attribute data2 in data.Attributes)
			{
				this.WriteAttribute(writer, data2);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00029114 File Offset: 0x00027314
		protected virtual Saml2AudienceRestriction ReadAudienceRestriction(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			bool requireDeclaration = false;
			if (reader.IsStartElement("Condition", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				requireDeclaration = true;
			}
			else if (!reader.IsStartElement("AudienceRestriction", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("AudienceRestriction", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2AudienceRestriction result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AudienceRestrictionType", "urn:oasis:names:tc:SAML:2.0:assertion", requireDeclaration);
				if (isEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}));
				}
				reader.Read();
				if (!reader.IsStartElement("Audience", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					reader.ReadStartElement("Audience", "urn:oasis:names:tc:SAML:2.0:assertion");
				}
				Saml2AudienceRestriction saml2AudienceRestriction = new Saml2AudienceRestriction(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader, UriKind.RelativeOrAbsolute, true));
				while (reader.IsStartElement("Audience", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2AudienceRestriction.Audiences.Add(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader, UriKind.RelativeOrAbsolute, true));
				}
				reader.ReadEndElement();
				result = saml2AudienceRestriction;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002924C File Offset: 0x0002744C
		protected virtual void WriteAudienceRestriction(XmlWriter writer, Saml2AudienceRestriction data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (data.Audiences == null || data.Audiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4159")));
			}
			writer.WriteStartElement("AudienceRestriction", "urn:oasis:names:tc:SAML:2.0:assertion");
			foreach (Uri uri in data.Audiences)
			{
				writer.WriteElementString("Audience", "urn:oasis:names:tc:SAML:2.0:assertion", uri.OriginalString);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00029314 File Offset: 0x00027514
		protected virtual Saml2AuthenticationContext ReadAuthenticationContext(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("AuthnContext", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("AuthnContext", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2AuthenticationContext result;
			try
			{
				if (reader.IsEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						"AuthnContext",
						"urn:oasis:names:tc:SAML:2.0:assertion"
					}));
				}
				XmlUtil.ValidateXsiType(reader, "AuthnContextType", "urn:oasis:names:tc:SAML:2.0:assertion");
				reader.ReadStartElement();
				Uri uri = null;
				Uri declarationReference = null;
				if (reader.IsStartElement("AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					uri = Saml2SecurityTokenHandler.ReadSimpleUriElement(reader);
				}
				if (reader.IsStartElement("AuthnContextDecl", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4118"));
				}
				if (reader.IsStartElement("AuthnContextDeclRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					declarationReference = Saml2SecurityTokenHandler.ReadSimpleUriElement(reader);
				}
				else if (null == uri)
				{
					reader.ReadStartElement("AuthnContextDeclRef", "urn:oasis:names:tc:SAML:2.0:assertion");
				}
				Saml2AuthenticationContext saml2AuthenticationContext = new Saml2AuthenticationContext(uri, declarationReference);
				while (reader.IsStartElement("AuthenticatingAuthority", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2AuthenticationContext.AuthenticatingAuthorities.Add(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader));
				}
				reader.ReadEndElement();
				result = saml2AuthenticationContext;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00029478 File Offset: 0x00027678
		protected virtual void WriteAuthenticationContext(XmlWriter writer, Saml2AuthenticationContext data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (null == data.ClassReference && null == data.DeclarationReference)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4117")));
			}
			writer.WriteStartElement("AuthnContext", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (null != data.ClassReference)
			{
				writer.WriteElementString("AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion", data.ClassReference.AbsoluteUri);
			}
			if (null != data.DeclarationReference)
			{
				writer.WriteElementString("AuthnContextDeclRef", "urn:oasis:names:tc:SAML:2.0:assertion", data.DeclarationReference.AbsoluteUri);
			}
			foreach (Uri uri in data.AuthenticatingAuthorities)
			{
				writer.WriteElementString("AuthenticatingAuthority", "urn:oasis:names:tc:SAML:2.0:assertion", uri.AbsoluteUri);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00029598 File Offset: 0x00027798
		protected virtual Saml2AuthenticationStatement ReadAuthenticationStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			bool requireDeclaration = false;
			if (reader.IsStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				requireDeclaration = true;
			}
			else if (!reader.IsStartElement("AuthnStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("AuthnStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2AuthenticationStatement result;
			try
			{
				DateTime? sessionNotOnOrAfter = null;
				Saml2SubjectLocality subjectLocality = null;
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AuthnStatementType", "urn:oasis:names:tc:SAML:2.0:assertion", requireDeclaration);
				if (isEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						"AuthnStatement",
						"urn:oasis:names:tc:SAML:2.0:assertion"
					}));
				}
				string attribute = reader.GetAttribute("AuthnInstant");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"AuthnInstant",
						"AuthnStatement"
					}));
				}
				DateTime authenticationInstant = XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted);
				string attribute2 = reader.GetAttribute("SessionIndex");
				attribute = reader.GetAttribute("SessionNotOnOrAfter");
				if (!string.IsNullOrEmpty(attribute))
				{
					sessionNotOnOrAfter = new DateTime?(XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted));
				}
				reader.Read();
				if (reader.IsStartElement("SubjectLocality", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					subjectLocality = this.ReadSubjectLocality(reader);
				}
				Saml2AuthenticationContext authenticationContext = this.ReadAuthenticationContext(reader);
				reader.ReadEndElement();
				result = new Saml2AuthenticationStatement(authenticationContext, authenticationInstant)
				{
					SessionIndex = attribute2,
					SessionNotOnOrAfter = sessionNotOnOrAfter,
					SubjectLocality = subjectLocality
				};
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00029758 File Offset: 0x00027958
		protected virtual void WriteAuthenticationStatement(XmlWriter writer, Saml2AuthenticationStatement data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("AuthnStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("AuthnInstant", XmlConvert.ToString(data.AuthenticationInstant.ToUniversalTime(), DateTimeFormats.Generated));
			if (data.SessionIndex != null)
			{
				writer.WriteAttributeString("SessionIndex", data.SessionIndex);
			}
			if (data.SessionNotOnOrAfter != null)
			{
				writer.WriteAttributeString("SessionNotOnOrAfter", XmlConvert.ToString(data.SessionNotOnOrAfter.Value.ToUniversalTime(), DateTimeFormats.Generated));
			}
			if (data.SubjectLocality != null)
			{
				this.WriteSubjectLocality(writer, data.SubjectLocality);
			}
			this.WriteAuthenticationContext(writer, data.AuthenticationContext);
			writer.WriteEndElement();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002983C File Offset: 0x00027A3C
		protected virtual Saml2AuthorizationDecisionStatement ReadAuthorizationDecisionStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			bool requireDeclaration = false;
			if (reader.IsStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				requireDeclaration = true;
			}
			else if (!reader.IsStartElement("AuthzDecisionStatement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("AuthzDecisionStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2AuthorizationDecisionStatement result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "AuthzDecisionStatementType", "urn:oasis:names:tc:SAML:2.0:assertion", requireDeclaration);
				if (isEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						"AuthzDecisionStatement",
						"urn:oasis:names:tc:SAML:2.0:assertion"
					}));
				}
				string attribute = reader.GetAttribute("Decision");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Decision",
						"AuthzDecisionStatement"
					}));
				}
				SamlAccessDecision decision;
				if (StringComparer.Ordinal.Equals(SamlAccessDecision.Permit.ToString(), attribute))
				{
					decision = SamlAccessDecision.Permit;
				}
				else if (StringComparer.Ordinal.Equals(SamlAccessDecision.Deny.ToString(), attribute))
				{
					decision = SamlAccessDecision.Deny;
				}
				else
				{
					if (!StringComparer.Ordinal.Equals(SamlAccessDecision.Indeterminate.ToString(), attribute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4123", new object[]
						{
							attribute
						}));
					}
					decision = SamlAccessDecision.Indeterminate;
				}
				attribute = reader.GetAttribute("Resource");
				if (attribute == null)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Resource",
						"AuthzDecisionStatement"
					}));
				}
				Uri resource;
				if (attribute.Length == 0)
				{
					resource = Saml2AuthorizationDecisionStatement.EmptyResource;
				}
				else
				{
					if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4121"));
					}
					resource = new Uri(attribute);
				}
				Saml2AuthorizationDecisionStatement saml2AuthorizationDecisionStatement = new Saml2AuthorizationDecisionStatement(resource, decision);
				reader.Read();
				do
				{
					saml2AuthorizationDecisionStatement.Actions.Add(this.ReadAction(reader));
				}
				while (reader.IsStartElement("Action", "urn:oasis:names:tc:SAML:2.0:assertion"));
				if (reader.IsStartElement("Evidence", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2AuthorizationDecisionStatement.Evidence = this.ReadEvidence(reader);
				}
				reader.ReadEndElement();
				result = saml2AuthorizationDecisionStatement;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00029AA4 File Offset: 0x00027CA4
		protected virtual void WriteAuthorizationDecisionStatement(XmlWriter writer, Saml2AuthorizationDecisionStatement data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (data.Actions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4122")));
			}
			writer.WriteStartElement("AuthzDecisionStatement", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("Decision", data.Decision.ToString());
			writer.WriteAttributeString("Resource", data.Resource.Equals(Saml2AuthorizationDecisionStatement.EmptyResource) ? data.Resource.ToString() : data.Resource.AbsoluteUri);
			foreach (Saml2Action data2 in data.Actions)
			{
				this.WriteAction(writer, data2);
			}
			if (data.Evidence != null)
			{
				this.WriteEvidence(writer, data.Evidence);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00029BC0 File Offset: 0x00027DC0
		protected virtual Saml2Conditions ReadConditions(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2Conditions result;
			try
			{
				Saml2Conditions saml2Conditions = new Saml2Conditions();
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "ConditionsType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("NotBefore");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2Conditions.NotBefore = new DateTime?(XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted));
				}
				attribute = reader.GetAttribute("NotOnOrAfter");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2Conditions.NotOnOrAfter = new DateTime?(XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted));
				}
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("Condition", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							XmlQualifiedName xsiType = XmlUtil.GetXsiType(reader);
							if (null == xsiType || XmlUtil.EqualsQName(xsiType, "ConditionAbstractType", "urn:oasis:names:tc:SAML:2.0:assertion"))
							{
								throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4104", new object[]
								{
									reader.LocalName,
									reader.NamespaceURI
								}));
							}
							if (XmlUtil.EqualsQName(xsiType, "AudienceRestrictionType", "urn:oasis:names:tc:SAML:2.0:assertion"))
							{
								saml2Conditions.AudienceRestrictions.Add(this.ReadAudienceRestriction(reader));
							}
							else if (XmlUtil.EqualsQName(xsiType, "OneTimeUseType", "urn:oasis:names:tc:SAML:2.0:assertion"))
							{
								if (saml2Conditions.OneTimeUse)
								{
									throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4115", new object[]
									{
										"OneTimeUse"
									}));
								}
								Saml2SecurityTokenHandler.ReadEmptyContentElement(reader);
								saml2Conditions.OneTimeUse = true;
							}
							else
							{
								if (!XmlUtil.EqualsQName(xsiType, "ProxyRestrictionType", "urn:oasis:names:tc:SAML:2.0:assertion"))
								{
									throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4113"));
								}
								if (saml2Conditions.ProxyRestriction != null)
								{
									throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4115", new object[]
									{
										"ProxyRestriction"
									}));
								}
								saml2Conditions.ProxyRestriction = this.ReadProxyRestriction(reader);
							}
						}
						else if (reader.IsStartElement("AudienceRestriction", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							saml2Conditions.AudienceRestrictions.Add(this.ReadAudienceRestriction(reader));
						}
						else if (reader.IsStartElement("OneTimeUse", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							if (saml2Conditions.OneTimeUse)
							{
								throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4115", new object[]
								{
									"OneTimeUse"
								}));
							}
							Saml2SecurityTokenHandler.ReadEmptyContentElement(reader);
							saml2Conditions.OneTimeUse = true;
						}
						else
						{
							if (!reader.IsStartElement("ProxyRestriction", "urn:oasis:names:tc:SAML:2.0:assertion"))
							{
								break;
							}
							if (saml2Conditions.ProxyRestriction != null)
							{
								throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4115", new object[]
								{
									"ProxyRestriction"
								}));
							}
							saml2Conditions.ProxyRestriction = this.ReadProxyRestriction(reader);
						}
					}
					reader.ReadEndElement();
				}
				result = saml2Conditions;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00029EC0 File Offset: 0x000280C0
		protected virtual void WriteConditions(XmlWriter writer, Saml2Conditions data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (data.NotBefore != null)
			{
				writer.WriteAttributeString("NotBefore", XmlConvert.ToString(data.NotBefore.Value.ToUniversalTime(), DateTimeFormats.Generated));
			}
			if (data.NotOnOrAfter != null)
			{
				writer.WriteAttributeString("NotOnOrAfter", XmlConvert.ToString(data.NotOnOrAfter.Value.ToUniversalTime(), DateTimeFormats.Generated));
			}
			foreach (Saml2AudienceRestriction data2 in data.AudienceRestrictions)
			{
				this.WriteAudienceRestriction(writer, data2);
			}
			if (data.OneTimeUse)
			{
				writer.WriteStartElement("OneTimeUse", "urn:oasis:names:tc:SAML:2.0:assertion");
				writer.WriteEndElement();
			}
			if (data.ProxyRestriction != null)
			{
				this.WriteProxyRestriction(writer, data.ProxyRestriction);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00029FF4 File Offset: 0x000281F4
		protected virtual Saml2Evidence ReadEvidence(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Evidence", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Evidence", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
				{
					"Evidence",
					"urn:oasis:names:tc:SAML:2.0:assertion"
				}));
			}
			Saml2Evidence result;
			try
			{
				Saml2Evidence saml2Evidence = new Saml2Evidence();
				XmlUtil.ValidateXsiType(reader, "EvidenceType", "urn:oasis:names:tc:SAML:2.0:assertion");
				reader.Read();
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("AssertionIDRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						saml2Evidence.AssertionIdReferences.Add(Saml2SecurityTokenHandler.ReadSimpleNCNameElement(reader));
					}
					else if (reader.IsStartElement("AssertionURIRef", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						saml2Evidence.AssertionUriReferences.Add(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader));
					}
					else if (reader.IsStartElement("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						saml2Evidence.Assertions.Add(this.ReadAssertion(reader));
					}
					else
					{
						if (!reader.IsStartElement("EncryptedAssertion", "urn:oasis:names:tc:SAML:2.0:assertion"))
						{
							break;
						}
						saml2Evidence.Assertions.Add(this.ReadAssertion(reader));
					}
				}
				if (saml2Evidence.AssertionIdReferences.Count == 0 && saml2Evidence.Assertions.Count == 0 && saml2Evidence.AssertionUriReferences.Count == 0)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4120"));
				}
				reader.ReadEndElement();
				result = saml2Evidence;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002A1A0 File Offset: 0x000283A0
		protected virtual void WriteEvidence(XmlWriter writer, Saml2Evidence data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if ((data.AssertionIdReferences == null || data.AssertionIdReferences.Count == 0) && (data.Assertions == null || data.Assertions.Count == 0) && (data.AssertionUriReferences == null || data.AssertionUriReferences.Count == 0))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4120")));
			}
			writer.WriteStartElement("Evidence", "urn:oasis:names:tc:SAML:2.0:assertion");
			foreach (Saml2Id saml2Id in data.AssertionIdReferences)
			{
				writer.WriteElementString("AssertionIDRef", "urn:oasis:names:tc:SAML:2.0:assertion", saml2Id.Value);
			}
			foreach (Uri uri in data.AssertionUriReferences)
			{
				writer.WriteElementString("AssertionURIRef", "urn:oasis:names:tc:SAML:2.0:assertion", uri.AbsoluteUri);
			}
			foreach (Saml2Assertion data2 in data.Assertions)
			{
				this.WriteAssertion(writer, data2);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002A320 File Offset: 0x00028520
		protected virtual Saml2NameIdentifier ReadIssuer(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Issuer", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			return this.ReadNameIdType(reader);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002A360 File Offset: 0x00028560
		protected virtual void WriteIssuer(XmlWriter writer, Saml2NameIdentifier data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
			this.WriteNameIdType(writer, data);
			writer.WriteEndElement();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002A3B1 File Offset: 0x000285B1
		protected virtual SecurityKeyIdentifier ReadSubjectKeyInfo(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this.KeyInfoSerializer.ReadKeyIdentifier(reader);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002A3D4 File Offset: 0x000285D4
		protected virtual SecurityKeyIdentifier ReadSigningKeyInfo(XmlReader reader, Saml2Assertion assertion)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SecurityKeyIdentifier securityKeyIdentifier;
			if (this.KeyInfoSerializer.CanReadKeyIdentifier(reader))
			{
				securityKeyIdentifier = this.KeyInfoSerializer.ReadKeyIdentifier(reader);
			}
			else
			{
				KeyInfo keyInfo = new KeyInfo(this.KeyInfoSerializer);
				keyInfo.ReadXml(XmlDictionaryReader.CreateDictionaryReader(reader));
				securityKeyIdentifier = keyInfo.KeyIdentifier;
			}
			if (securityKeyIdentifier.Count == 0)
			{
				return new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					new Saml2SecurityKeyIdentifierClause(assertion)
				});
			}
			return securityKeyIdentifier;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002A44E File Offset: 0x0002864E
		protected virtual void WriteSubjectKeyInfo(XmlWriter writer, SecurityKeyIdentifier data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			this.KeyInfoSerializer.WriteKeyIdentifier(writer, data);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002A484 File Offset: 0x00028684
		protected virtual void WriteSigningKeyInfo(XmlWriter writer, SecurityKeyIdentifier data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (this.KeyInfoSerializer.CanWriteKeyIdentifier(data))
			{
				this.KeyInfoSerializer.WriteKeyIdentifier(writer, data);
				return;
			}
			throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4221", new object[]
			{
				data
			}));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002A4EC File Offset: 0x000286EC
		protected virtual Saml2NameIdentifier ReadNameId(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("NameID", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("NameID", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			return this.ReadNameIdType(reader);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002A52C File Offset: 0x0002872C
		protected virtual void WriteNameId(XmlWriter writer, Saml2NameIdentifier data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (data.EncryptingCredentials != null)
			{
				EncryptingCredentials encryptingCredentials = data.EncryptingCredentials;
				SymmetricSecurityKey symmetricSecurityKey = encryptingCredentials.SecurityKey as SymmetricSecurityKey;
				if (symmetricSecurityKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID3284")));
				}
				MemoryStream memoryStream = null;
				try
				{
					memoryStream = new MemoryStream();
					using (XmlWriter xmlWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
					{
						xmlWriter.WriteStartElement("NameID", "urn:oasis:names:tc:SAML:2.0:assertion");
						this.WriteNameIdType(xmlWriter, data);
						xmlWriter.WriteEndElement();
					}
					EncryptedDataElement encryptedDataElement = new EncryptedDataElement();
					encryptedDataElement.Type = "http://www.w3.org/2001/04/xmlenc#Element";
					encryptedDataElement.Algorithm = encryptingCredentials.Algorithm;
					encryptedDataElement.KeyIdentifier = encryptingCredentials.SecurityKeyIdentifier;
					SymmetricAlgorithm symmetricAlgorithm = symmetricSecurityKey.GetSymmetricAlgorithm(encryptingCredentials.Algorithm);
					encryptedDataElement.Encrypt(symmetricAlgorithm, memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
					((IDisposable)memoryStream).Dispose();
					writer.WriteStartElement("EncryptedID", "urn:oasis:names:tc:SAML:2.0:assertion");
					encryptedDataElement.WriteXml(writer, this.KeyInfoSerializer);
					foreach (EncryptedKeyIdentifierClause keyIdentifierClause in data.ExternalEncryptedKeys)
					{
						this.KeyInfoSerializer.WriteKeyIdentifierClause(writer, keyIdentifierClause);
					}
					writer.WriteEndElement();
					return;
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Dispose();
						memoryStream = null;
					}
				}
			}
			writer.WriteStartElement("NameID", "urn:oasis:names:tc:SAML:2.0:assertion");
			this.WriteNameIdType(writer, data);
			writer.WriteEndElement();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002A6E8 File Offset: 0x000288E8
		protected virtual Saml2NameIdentifier ReadNameIdType(XmlReader reader)
		{
			Saml2NameIdentifier result;
			try
			{
				reader.MoveToContent();
				Saml2NameIdentifier saml2NameIdentifier = new Saml2NameIdentifier("__TemporaryName__");
				XmlUtil.ValidateXsiType(reader, "NameIDType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("Format");
				if (!string.IsNullOrEmpty(attribute))
				{
					if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
						{
							"Format",
							"NameID"
						}));
					}
					saml2NameIdentifier.Format = new Uri(attribute);
				}
				attribute = reader.GetAttribute("NameQualifier");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2NameIdentifier.NameQualifier = attribute;
				}
				attribute = reader.GetAttribute("SPNameQualifier");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2NameIdentifier.SPNameQualifier = attribute;
				}
				attribute = reader.GetAttribute("SPProvidedID");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2NameIdentifier.SPProvidedId = attribute;
				}
				saml2NameIdentifier.Value = reader.ReadElementString();
				if (saml2NameIdentifier.Format != null && StringComparer.Ordinal.Equals(saml2NameIdentifier.Format.AbsoluteUri, Saml2Constants.NameIdentifierFormats.Entity.AbsoluteUri))
				{
					if (!UriUtil.CanCreateValidUri(saml2NameIdentifier.Value, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4262", new object[]
						{
							saml2NameIdentifier.Value,
							Saml2Constants.NameIdentifierFormats.Entity.AbsoluteUri
						}));
					}
					if (!string.IsNullOrEmpty(saml2NameIdentifier.NameQualifier) || !string.IsNullOrEmpty(saml2NameIdentifier.SPNameQualifier) || !string.IsNullOrEmpty(saml2NameIdentifier.SPProvidedId))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4263", new object[]
						{
							saml2NameIdentifier.Value,
							Saml2Constants.NameIdentifierFormats.Entity.AbsoluteUri
						}));
					}
				}
				result = saml2NameIdentifier;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		protected virtual Saml2NameIdentifier ReadEncryptedId(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!reader.IsStartElement("EncryptedID", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("EncryptedID", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Collection<EncryptedKeyIdentifierClause> collection = new Collection<EncryptedKeyIdentifierClause>();
			EncryptingCredentials encryptingCredentials = null;
			Saml2NameIdentifier saml2NameIdentifier = null;
			using (StringReader stringReader = new StringReader(reader.ReadOuterXml()))
			{
				using (XmlDictionaryReader xmlDictionaryReader = new WrappedXmlDictionaryReader(XmlReader.Create(stringReader), BoundedXmlDictionaryReaderQuotas.Quotas))
				{
					XmlReader reader2 = Saml2SecurityTokenHandler.CreatePlaintextReaderFromEncryptedData(xmlDictionaryReader, base.Configuration.ServiceTokenResolver, this.KeyInfoSerializer, collection, out encryptingCredentials);
					saml2NameIdentifier = this.ReadNameIdType(reader2);
					saml2NameIdentifier.EncryptingCredentials = encryptingCredentials;
					foreach (EncryptedKeyIdentifierClause item in collection)
					{
						saml2NameIdentifier.ExternalEncryptedKeys.Add(item);
					}
				}
			}
			return saml2NameIdentifier;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002A9DC File Offset: 0x00028BDC
		protected virtual void WriteNameIdType(XmlWriter writer, Saml2NameIdentifier data)
		{
			if (null != data.Format)
			{
				writer.WriteAttributeString("Format", data.Format.AbsoluteUri);
			}
			if (!string.IsNullOrEmpty(data.NameQualifier))
			{
				writer.WriteAttributeString("NameQualifier", data.NameQualifier);
			}
			if (!string.IsNullOrEmpty(data.SPNameQualifier))
			{
				writer.WriteAttributeString("SPNameQualifier", data.SPNameQualifier);
			}
			if (!string.IsNullOrEmpty(data.SPProvidedId))
			{
				writer.WriteAttributeString("SPProvidedID", data.SPProvidedId);
			}
			writer.WriteString(data.Value);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002AA74 File Offset: 0x00028C74
		protected virtual Saml2ProxyRestriction ReadProxyRestriction(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			bool requireDeclaration = false;
			if (reader.IsStartElement("Condition", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				requireDeclaration = true;
			}
			else if (!reader.IsStartElement("ProxyRestriction", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("ProxyRestriction", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2ProxyRestriction result;
			try
			{
				Saml2ProxyRestriction saml2ProxyRestriction = new Saml2ProxyRestriction();
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "ProxyRestrictionType", "urn:oasis:names:tc:SAML:2.0:assertion", requireDeclaration);
				string attribute = reader.GetAttribute("Count");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2ProxyRestriction.Count = new int?(XmlConvert.ToInt32(attribute));
				}
				reader.Read();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement("Audience", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						saml2ProxyRestriction.Audiences.Add(Saml2SecurityTokenHandler.ReadSimpleUriElement(reader));
					}
					reader.ReadEndElement();
				}
				result = saml2ProxyRestriction;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002AB80 File Offset: 0x00028D80
		protected virtual void WriteProxyRestriction(XmlWriter writer, Saml2ProxyRestriction data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("ProxyRestriction", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (data.Count != null)
			{
				writer.WriteAttributeString("Count", XmlConvert.ToString(data.Count.Value));
			}
			foreach (Uri uri in data.Audiences)
			{
				writer.WriteElementString("Audience", uri.AbsoluteUri);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002AC44 File Offset: 0x00028E44
		protected virtual Saml2Statement ReadStatement(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Statement", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			XmlQualifiedName xsiType = XmlUtil.GetXsiType(reader);
			if (null == xsiType || XmlUtil.EqualsQName(xsiType, "StatementAbstractType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4104", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				}));
			}
			if (XmlUtil.EqualsQName(xsiType, "AttributeStatementType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadAttributeStatement(reader);
			}
			if (XmlUtil.EqualsQName(xsiType, "AuthnStatementType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadAuthenticationStatement(reader);
			}
			if (XmlUtil.EqualsQName(xsiType, "AuthzDecisionStatementType", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				return this.ReadAuthorizationDecisionStatement(reader);
			}
			throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4105", new object[]
			{
				xsiType.Name,
				xsiType.Namespace
			}));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002AD48 File Offset: 0x00028F48
		protected virtual void WriteStatement(XmlWriter writer, Saml2Statement data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			Saml2AttributeStatement saml2AttributeStatement = data as Saml2AttributeStatement;
			if (saml2AttributeStatement != null)
			{
				this.WriteAttributeStatement(writer, saml2AttributeStatement);
				return;
			}
			Saml2AuthenticationStatement saml2AuthenticationStatement = data as Saml2AuthenticationStatement;
			if (saml2AuthenticationStatement != null)
			{
				this.WriteAuthenticationStatement(writer, saml2AuthenticationStatement);
				return;
			}
			Saml2AuthorizationDecisionStatement saml2AuthorizationDecisionStatement = data as Saml2AuthorizationDecisionStatement;
			if (saml2AuthorizationDecisionStatement != null)
			{
				this.WriteAuthorizationDecisionStatement(writer, saml2AuthorizationDecisionStatement);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4107", new object[]
			{
				data.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002ADE4 File Offset: 0x00028FE4
		protected virtual Saml2Subject ReadSubject(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("Subject", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("Subject", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2Subject result;
			try
			{
				if (reader.IsEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID3061", new object[]
					{
						reader.LocalName,
						reader.NamespaceURI
					}));
				}
				XmlUtil.ValidateXsiType(reader, "SubjectType", "urn:oasis:names:tc:SAML:2.0:assertion");
				Saml2Subject saml2Subject = new Saml2Subject();
				reader.Read();
				saml2Subject.NameId = this.ReadSubjectId(reader, "Subject");
				while (reader.IsStartElement("SubjectConfirmation", "urn:oasis:names:tc:SAML:2.0:assertion"))
				{
					saml2Subject.SubjectConfirmations.Add(this.ReadSubjectConfirmation(reader));
				}
				reader.ReadEndElement();
				if (saml2Subject.NameId == null && saml2Subject.SubjectConfirmations.Count == 0)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4108"));
				}
				result = saml2Subject;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002AF08 File Offset: 0x00029108
		protected virtual void WriteSubject(XmlWriter writer, Saml2Subject data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (data.NameId == null && data.SubjectConfirmations.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4108")));
			}
			writer.WriteStartElement("Subject", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (data.NameId != null)
			{
				this.WriteNameId(writer, data.NameId);
			}
			foreach (Saml2SubjectConfirmation data2 in data.SubjectConfirmations)
			{
				this.WriteSubjectConfirmation(writer, data2);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002AFD8 File Offset: 0x000291D8
		protected virtual Saml2SubjectConfirmation ReadSubjectConfirmation(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("SubjectConfirmation", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("SubjectConfirmation", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2SubjectConfirmation result;
			try
			{
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "SubjectConfirmationType", "urn:oasis:names:tc:SAML:2.0:assertion");
				string attribute = reader.GetAttribute("Method");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0001", new object[]
					{
						"Method",
						"SubjectConfirmation"
					}));
				}
				if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
					{
						"Method",
						"SubjectConfirmation"
					}));
				}
				Saml2SubjectConfirmation saml2SubjectConfirmation = new Saml2SubjectConfirmation(new Uri(attribute));
				reader.Read();
				if (!isEmptyElement)
				{
					saml2SubjectConfirmation.NameIdentifier = this.ReadSubjectId(reader, "SubjectConfirmation");
					if (reader.IsStartElement("SubjectConfirmationData", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						saml2SubjectConfirmation.SubjectConfirmationData = this.ReadSubjectConfirmationData(reader);
					}
					reader.ReadEndElement();
				}
				result = saml2SubjectConfirmation;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002B120 File Offset: 0x00029320
		protected virtual void WriteSubjectConfirmation(XmlWriter writer, Saml2SubjectConfirmation data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			if (null == data.Method)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data.Method");
			}
			if (string.IsNullOrEmpty(data.Method.ToString()))
			{
				throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("data.Method");
			}
			writer.WriteStartElement("SubjectConfirmation", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteAttributeString("Method", data.Method.AbsoluteUri);
			if (data.NameIdentifier != null)
			{
				this.WriteNameId(writer, data.NameIdentifier);
			}
			if (data.SubjectConfirmationData != null)
			{
				this.WriteSubjectConfirmationData(writer, data.SubjectConfirmationData);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002B1E4 File Offset: 0x000293E4
		protected virtual Saml2SubjectConfirmationData ReadSubjectConfirmationData(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("SubjectConfirmationData", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("SubjectConfirmationData", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2SubjectConfirmationData result;
			try
			{
				Saml2SubjectConfirmationData saml2SubjectConfirmationData = new Saml2SubjectConfirmationData();
				bool isEmptyElement = reader.IsEmptyElement;
				bool flag = false;
				XmlQualifiedName xsiType = XmlUtil.GetXsiType(reader);
				if (null != xsiType)
				{
					if (XmlUtil.EqualsQName(xsiType, "KeyInfoConfirmationDataType", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						flag = true;
					}
					else if (!XmlUtil.EqualsQName(xsiType, "SubjectConfirmationDataType", "urn:oasis:names:tc:SAML:2.0:assertion"))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4112", new object[]
						{
							xsiType.Name,
							xsiType.Namespace
						}));
					}
				}
				if (flag && isEmptyElement)
				{
					throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString(SR.GetString("ID4111")));
				}
				string attribute = reader.GetAttribute("Address");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2SubjectConfirmationData.Address = attribute;
				}
				attribute = reader.GetAttribute("InResponseTo");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2SubjectConfirmationData.InResponseTo = new Saml2Id(attribute);
				}
				attribute = reader.GetAttribute("NotBefore");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2SubjectConfirmationData.NotBefore = new DateTime?(XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted));
				}
				attribute = reader.GetAttribute("NotOnOrAfter");
				if (!string.IsNullOrEmpty(attribute))
				{
					saml2SubjectConfirmationData.NotOnOrAfter = new DateTime?(XmlConvert.ToDateTime(attribute, DateTimeFormats.Accepted));
				}
				attribute = reader.GetAttribute("Recipient");
				if (!string.IsNullOrEmpty(attribute))
				{
					if (!UriUtil.CanCreateValidUri(attribute, UriKind.Absolute))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID0011", new object[]
						{
							"Recipient",
							"SubjectConfirmationData"
						}));
					}
					saml2SubjectConfirmationData.Recipient = new Uri(attribute);
				}
				reader.Read();
				if (!isEmptyElement)
				{
					if (flag)
					{
						saml2SubjectConfirmationData.KeyIdentifiers.Add(this.ReadSubjectKeyInfo(reader));
					}
					while (reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#"))
					{
						saml2SubjectConfirmationData.KeyIdentifiers.Add(this.ReadSubjectKeyInfo(reader));
					}
					if (!flag && XmlNodeType.EndElement != reader.NodeType)
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4114", new object[]
						{
							"SubjectConfirmationData"
						}));
					}
					reader.ReadEndElement();
				}
				result = saml2SubjectConfirmationData;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002B454 File Offset: 0x00029654
		protected virtual void WriteSubjectConfirmationData(XmlWriter writer, Saml2SubjectConfirmationData data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("SubjectConfirmationData", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (data.KeyIdentifiers != null && data.KeyIdentifiers.Count > 0)
			{
				writer.WriteAttributeString("type", "http://www.w3.org/2001/XMLSchema-instance", "KeyInfoConfirmationDataType");
			}
			if (!string.IsNullOrEmpty(data.Address))
			{
				writer.WriteAttributeString("Address", data.Address);
			}
			if (data.InResponseTo != null)
			{
				writer.WriteAttributeString("InResponseTo", data.InResponseTo.Value);
			}
			if (data.NotBefore != null)
			{
				writer.WriteAttributeString("NotBefore", XmlConvert.ToString(data.NotBefore.Value.ToUniversalTime(), DateTimeFormats.Generated));
			}
			if (data.NotOnOrAfter != null)
			{
				writer.WriteAttributeString("NotOnOrAfter", XmlConvert.ToString(data.NotOnOrAfter.Value.ToUniversalTime(), DateTimeFormats.Generated));
			}
			if (null != data.Recipient)
			{
				writer.WriteAttributeString("Recipient", data.Recipient.OriginalString);
			}
			foreach (SecurityKeyIdentifier data2 in data.KeyIdentifiers)
			{
				this.WriteSubjectKeyInfo(writer, data2);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002B5E0 File Offset: 0x000297E0
		protected virtual Saml2SubjectLocality ReadSubjectLocality(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!reader.IsStartElement("SubjectLocality", "urn:oasis:names:tc:SAML:2.0:assertion"))
			{
				reader.ReadStartElement("SubjectLocality", "urn:oasis:names:tc:SAML:2.0:assertion");
			}
			Saml2SubjectLocality result;
			try
			{
				Saml2SubjectLocality saml2SubjectLocality = new Saml2SubjectLocality();
				bool isEmptyElement = reader.IsEmptyElement;
				XmlUtil.ValidateXsiType(reader, "SubjectLocalityType", "urn:oasis:names:tc:SAML:2.0:assertion");
				saml2SubjectLocality.Address = reader.GetAttribute("Address");
				saml2SubjectLocality.DnsName = reader.GetAttribute("DNSName");
				reader.Read();
				if (!isEmptyElement)
				{
					reader.ReadEndElement();
				}
				result = saml2SubjectLocality;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Exception ex2 = Saml2SecurityTokenHandler.TryWrapReadException(reader, ex);
				if (ex2 == null)
				{
					throw;
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002B6A4 File Offset: 0x000298A4
		protected virtual void WriteSubjectLocality(XmlWriter writer, Saml2SubjectLocality data)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (data == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("data");
			}
			writer.WriteStartElement("SubjectLocality", "urn:oasis:names:tc:SAML:2.0:assertion");
			if (data.Address != null)
			{
				writer.WriteAttributeString("Address", data.Address);
			}
			if (data.DnsName != null)
			{
				writer.WriteAttributeString("DNSName", data.DnsName);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000B63 RID: 2915
		public const string TokenProfile11ValueType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID";

		// Token: 0x04000B64 RID: 2916
		private const string Actor = "Actor";

		// Token: 0x04000B65 RID: 2917
		private const string Attribute = "Attribute";

		// Token: 0x04000B66 RID: 2918
		private static string[] tokenTypeIdentifiers = new string[]
		{
			"urn:oasis:names:tc:SAML:2.0:assertion",
			"http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"
		};

		// Token: 0x04000B67 RID: 2919
		private SamlSecurityTokenRequirement samlSecurityTokenRequirement;

		// Token: 0x04000B68 RID: 2920
		private SecurityTokenSerializer keyInfoSerializer;

		// Token: 0x04000B69 RID: 2921
		private const int MaxAssertionNestingDepth = 8;

		// Token: 0x04000B6A RID: 2922
		[ThreadStatic]
		private static int t_currentAssertionDepth;

		// Token: 0x04000B6B RID: 2923
		private const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		// Token: 0x04000B6C RID: 2924
		private object syncObject = new object();

		// Token: 0x02000267 RID: 615
		internal class WrappedSerializer : SecurityTokenSerializer
		{
			// Token: 0x06001269 RID: 4713 RVA: 0x000504B4 File Offset: 0x0004E6B4
			public WrappedSerializer(Saml2SecurityTokenHandler parent, Saml2Assertion assertion)
			{
				this.assertion = assertion;
				this.parent = parent;
			}

			// Token: 0x0600126A RID: 4714 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
			{
				return false;
			}

			// Token: 0x0600126B RID: 4715 RVA: 0x00002434 File Offset: 0x00000634
			protected override bool CanReadKeyIdentifierCore(XmlReader reader)
			{
				return true;
			}

			// Token: 0x0600126C RID: 4716 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanReadTokenCore(XmlReader reader)
			{
				return false;
			}

			// Token: 0x0600126D RID: 4717 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				return false;
			}

			// Token: 0x0600126E RID: 4718 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
			{
				return false;
			}

			// Token: 0x0600126F RID: 4719 RVA: 0x00002D09 File Offset: 0x00000F09
			protected override bool CanWriteTokenCore(SecurityToken token)
			{
				return false;
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06001271 RID: 4721 RVA: 0x000504CA File Offset: 0x0004E6CA
			protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
			{
				return this.parent.ReadSigningKeyInfo(reader, this.assertion);
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x000504DE File Offset: 0x0004E6DE
			protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
			{
				this.parent.WriteSigningKeyInfo(writer, keyIdentifier);
			}

			// Token: 0x06001275 RID: 4725 RVA: 0x00002D0C File Offset: 0x00000F0C
			protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x040010C9 RID: 4297
			private Saml2SecurityTokenHandler parent;

			// Token: 0x040010CA RID: 4298
			private Saml2Assertion assertion;
		}

		// Token: 0x02000268 RID: 616
		internal class ReceivedEncryptingCredentials : EncryptingCredentials
		{
			// Token: 0x06001276 RID: 4726 RVA: 0x000504ED File Offset: 0x0004E6ED
			public ReceivedEncryptingCredentials(SecurityKey key, SecurityKeyIdentifier keyIdentifier, string algorithm) : base(key, keyIdentifier, algorithm)
			{
			}
		}
	}
}
