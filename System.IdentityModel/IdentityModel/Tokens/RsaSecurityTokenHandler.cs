using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000130 RID: 304
	public class RsaSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00023DB5 File Offset: 0x00021FB5
		public override bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00023DDA File Offset: 0x00021FDA
		public override string[] GetTokenTypeIdentifiers()
		{
			return RsaSecurityTokenHandler._tokenTypeIdentifiers;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00023DE4 File Offset: 0x00021FE4
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			if (!xmlDictionaryReader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"KeyInfo",
					"http://www.w3.org/2000/09/xmldsig#",
					xmlDictionaryReader.LocalName,
					xmlDictionaryReader.NamespaceURI
				})));
			}
			xmlDictionaryReader.ReadStartElement();
			if (!xmlDictionaryReader.IsStartElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"KeyValue",
					"http://www.w3.org/2000/09/xmldsig#",
					xmlDictionaryReader.LocalName,
					xmlDictionaryReader.NamespaceURI
				})));
			}
			xmlDictionaryReader.ReadStartElement();
			RSA rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(xmlDictionaryReader.ReadOuterXml());
			xmlDictionaryReader.ReadEndElement();
			xmlDictionaryReader.ReadEndElement();
			return new RsaSecurityToken(rsa);
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00023EE3 File Offset: 0x000220E3
		public override Type TokenType
		{
			get
			{
				return typeof(RsaSecurityToken);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00023EF0 File Offset: 0x000220F0
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			RsaSecurityToken rsaSecurityToken = (RsaSecurityToken)token;
			if (rsaSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(RsaSecurityToken)
				}));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
				{
					new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa", rsaSecurityToken.Rsa.ToXmlString(false), "http://www.w3.org/2000/09/xmldsig#RSAKeyValue", "LOCAL AUTHORITY")
				}, "Signature");
				claimsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime"));
				claimsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/signature"));
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

		// Token: 0x06000898 RID: 2200 RVA: 0x00024034 File Offset: 0x00022234
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
			RsaSecurityToken rsaSecurityToken = token as RsaSecurityToken;
			if (rsaSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(RsaSecurityToken)
				}));
			}
			RSAParameters rsaparameters = rsaSecurityToken.Rsa.ExportParameters(false);
			writer.WriteStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			writer.WriteStartElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			writer.WriteStartElement("RsaKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			writer.WriteStartElement("Modulus", "http://www.w3.org/2000/09/xmldsig#");
			byte[] modulus = rsaparameters.Modulus;
			writer.WriteBase64(modulus, 0, modulus.Length);
			writer.WriteEndElement();
			writer.WriteStartElement("Exponent", "http://www.w3.org/2000/09/xmldsig#");
			byte[] exponent = rsaparameters.Exponent;
			writer.WriteBase64(exponent, 0, exponent.Length);
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x04000B24 RID: 2852
		private static string[] _tokenTypeIdentifiers = new string[]
		{
			SecurityTokenTypes.Rsa
		};
	}
}
