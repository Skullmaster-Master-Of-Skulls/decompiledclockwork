using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000161 RID: 353
	public class SamlSerializer
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x000353B0 File Offset: 0x000335B0
		public void PopulateDictionary(IXmlDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionary");
			}
			this.dictionaryManager = new DictionaryManager(dictionary);
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000353D1 File Offset: 0x000335D1
		internal DictionaryManager DictionaryManager
		{
			get
			{
				if (this.dictionaryManager == null)
				{
					this.dictionaryManager = new DictionaryManager();
				}
				return this.dictionaryManager;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000353EC File Offset: 0x000335EC
		public virtual SamlSecurityToken ReadToken(XmlReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			WrappedReader reader3 = new WrappedReader(reader2);
			SamlAssertion samlAssertion = this.LoadAssertion(reader3, keyInfoSerializer, outOfBandTokenResolver);
			if (samlAssertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadAssertion")));
			}
			return new SamlSecurityToken(samlAssertion);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00035447 File Offset: 0x00033647
		public virtual void WriteToken(SamlSecurityToken token, XmlWriter writer, SecurityTokenSerializer keyInfoSerializer)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			token.Assertion.WriteTo(writer, this, keyInfoSerializer);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003546C File Offset: 0x0003366C
		public virtual SamlAssertion LoadAssertion(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlAssertion samlAssertion = new SamlAssertion();
			SamlAssertion.ResetAssertionDepth();
			samlAssertion.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
			return samlAssertion;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000354A4 File Offset: 0x000336A4
		public virtual SamlCondition LoadCondition(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement(this.DictionaryManager.SamlDictionary.AudienceRestrictionCondition, this.DictionaryManager.SamlDictionary.Namespace))
			{
				SamlAudienceRestrictionCondition samlAudienceRestrictionCondition = new SamlAudienceRestrictionCondition();
				samlAudienceRestrictionCondition.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
				return samlAudienceRestrictionCondition;
			}
			if (reader.IsStartElement(this.DictionaryManager.SamlDictionary.DoNotCacheCondition, this.DictionaryManager.SamlDictionary.Namespace))
			{
				SamlDoNotCacheCondition samlDoNotCacheCondition = new SamlDoNotCacheCondition();
				samlDoNotCacheCondition.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
				return samlDoNotCacheCondition;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("SAMLUnableToLoadUnknownElement", new object[]
			{
				reader.LocalName
			})));
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00035560 File Offset: 0x00033760
		public virtual SamlConditions LoadConditions(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlConditions samlConditions = new SamlConditions();
			samlConditions.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
			return samlConditions;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00035594 File Offset: 0x00033794
		public virtual SamlAdvice LoadAdvice(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SamlAdvice samlAdvice = new SamlAdvice();
			samlAdvice.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
			return samlAdvice;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000355C8 File Offset: 0x000337C8
		public virtual SamlStatement LoadStatement(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement(this.DictionaryManager.SamlDictionary.AuthenticationStatement, this.DictionaryManager.SamlDictionary.Namespace))
			{
				SamlAuthenticationStatement samlAuthenticationStatement = new SamlAuthenticationStatement();
				samlAuthenticationStatement.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
				return samlAuthenticationStatement;
			}
			if (reader.IsStartElement(this.DictionaryManager.SamlDictionary.AttributeStatement, this.DictionaryManager.SamlDictionary.Namespace))
			{
				SamlAttributeStatement samlAttributeStatement = new SamlAttributeStatement();
				samlAttributeStatement.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
				return samlAttributeStatement;
			}
			if (reader.IsStartElement(this.DictionaryManager.SamlDictionary.AuthorizationDecisionStatement, this.DictionaryManager.SamlDictionary.Namespace))
			{
				SamlAuthorizationDecisionStatement samlAuthorizationDecisionStatement = new SamlAuthorizationDecisionStatement();
				samlAuthorizationDecisionStatement.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
				return samlAuthorizationDecisionStatement;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("SAMLUnableToLoadUnknownElement", new object[]
			{
				reader.LocalName
			})));
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000356C0 File Offset: 0x000338C0
		public virtual SamlAttribute LoadAttribute(XmlDictionaryReader reader, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			SamlAttribute samlAttribute = new SamlAttribute();
			samlAttribute.ReadXml(reader, this, keyInfoSerializer, outOfBandTokenResolver);
			return samlAttribute;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000356E0 File Offset: 0x000338E0
		internal static SecurityKeyIdentifier ReadSecurityKeyIdentifier(XmlReader reader, SecurityTokenSerializer tokenSerializer)
		{
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer", SR.GetString("SamlSerializerRequiresExternalSerializers"));
			}
			if (tokenSerializer.CanReadKeyIdentifier(reader))
			{
				return tokenSerializer.ReadKeyIdentifier(reader);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SamlSerializerUnableToReadSecurityKeyIdentifier")));
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00035734 File Offset: 0x00033934
		internal static void WriteSecurityKeyIdentifier(XmlWriter writer, SecurityKeyIdentifier ski, SecurityTokenSerializer tokenSerializer)
		{
			if (tokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenSerializer", SR.GetString("SamlSerializerRequiresExternalSerializers"));
			}
			bool flag = false;
			if (tokenSerializer.CanWriteKeyIdentifier(ski))
			{
				tokenSerializer.WriteKeyIdentifier(writer, ski);
				flag = true;
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SamlSerializerUnableToWriteSecurityKeyIdentifier", new object[]
				{
					ski.ToString()
				})));
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000357A0 File Offset: 0x000339A0
		internal static SecurityKey ResolveSecurityKey(SecurityKeyIdentifier ski, SecurityTokenResolver tokenResolver)
		{
			if (ski == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ski");
			}
			if (tokenResolver != null)
			{
				for (int i = 0; i < ski.Count; i++)
				{
					SecurityKey result = null;
					if (tokenResolver.TryResolveSecurityKey(ski[i], out result))
					{
						return result;
					}
				}
			}
			if (ski.CanCreateKey)
			{
				return ski.CreateKey();
			}
			return null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000357FC File Offset: 0x000339FC
		internal static SecurityToken ResolveSecurityToken(SecurityKeyIdentifier ski, SecurityTokenResolver tokenResolver)
		{
			SecurityToken securityToken = null;
			if (tokenResolver != null)
			{
				tokenResolver.TryResolveToken(ski, out securityToken);
			}
			RsaKeyIdentifierClause rsaKeyIdentifierClause;
			if (securityToken == null && ski.TryFind<RsaKeyIdentifierClause>(out rsaKeyIdentifierClause))
			{
				securityToken = new RsaSecurityToken(rsaKeyIdentifierClause.Rsa);
			}
			X509RawDataKeyIdentifierClause x509RawDataKeyIdentifierClause;
			if (securityToken == null && ski.TryFind<X509RawDataKeyIdentifierClause>(out x509RawDataKeyIdentifierClause))
			{
				securityToken = new X509SecurityToken(new X509Certificate2(x509RawDataKeyIdentifierClause.GetX509RawData()));
			}
			return securityToken;
		}

		// Token: 0x04000BE8 RID: 3048
		private DictionaryManager dictionaryManager;
	}
}
