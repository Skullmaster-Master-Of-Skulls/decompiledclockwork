using System;
using System.IO;
using System.Text;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000014 RID: 20
	public class AuthRequest
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003C08 File Offset: 0x00001E08
		public AuthRequest(string assertionConsumerServiceUrl, string issuer)
		{
			this._assertionConsumerServiceUrl = assertionConsumerServiceUrl;
			this._issuer = issuer;
			this.id = "_" + Guid.NewGuid().ToString();
			this.issue_instant = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003C70 File Offset: 0x00001E70
		public string GetRequest(AuthRequest.AuthRequestFormat format)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlWriterSettings settings = new XmlWriterSettings
				{
					OmitXmlDeclaration = true
				};
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
				{
					xmlWriter.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
					xmlWriter.WriteAttributeString("ID", this.id);
					xmlWriter.WriteAttributeString("Version", "2.0");
					xmlWriter.WriteAttributeString("IssueInstant", this.issue_instant);
					xmlWriter.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
					xmlWriter.WriteAttributeString("AssertionConsumerServiceURL", this._assertionConsumerServiceUrl);
					xmlWriter.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
					xmlWriter.WriteString(this._issuer);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
					xmlWriter.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:2.0:nameid-format:unspecified");
					xmlWriter.WriteAttributeString("AllowCreate", "true");
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("samlp", "RequestedAuthnContext", "urn:oasis:names:tc:SAML:2.0:protocol");
					xmlWriter.WriteAttributeString("Comparison", "exact");
					xmlWriter.WriteEndElement();
					xmlWriter.WriteStartElement("saml", "AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion");
					xmlWriter.WriteString("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}
				if (format != AuthRequest.AuthRequestFormat.PlainText)
				{
					if (format != AuthRequest.AuthRequestFormat.Base64)
					{
						result = null;
					}
					else
					{
						byte[] bytes = Encoding.ASCII.GetBytes(stringWriter.ToString());
						result = Convert.ToBase64String(bytes);
					}
				}
				else
				{
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x0400004C RID: 76
		public string id;

		// Token: 0x0400004D RID: 77
		private string issue_instant;

		// Token: 0x0400004E RID: 78
		private string _assertionConsumerServiceUrl;

		// Token: 0x0400004F RID: 79
		private string _issuer;

		// Token: 0x0200002C RID: 44
		public enum AuthRequestFormat
		{
			// Token: 0x0400007A RID: 122
			PlainText,
			// Token: 0x0400007B RID: 123
			Base64
		}
	}
}
