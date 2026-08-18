using System;
using System.IO;
using System.Text;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000015 RID: 21
	public class LogoutRequest
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003E60 File Offset: 0x00002060
		public LogoutRequest(string assertionConsumerServiceUrl, string issuer)
		{
			this._assertionConsumerServiceUrl = assertionConsumerServiceUrl;
			this._issuer = issuer;
			this.id = "_" + Guid.NewGuid().ToString();
			this.issue_instant = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EC8 File Offset: 0x000020C8
		public string GetRequest(LogoutRequest.LogoutRequestFormat format)
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
					xmlWriter.WriteStartElement("samlp", "LogoutRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
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
					xmlWriter.WriteEndElement();
				}
				if (format != LogoutRequest.LogoutRequestFormat.PlainText)
				{
					if (format != LogoutRequest.LogoutRequestFormat.Base64)
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

		// Token: 0x04000050 RID: 80
		public string id;

		// Token: 0x04000051 RID: 81
		private string issue_instant;

		// Token: 0x04000052 RID: 82
		private string _assertionConsumerServiceUrl;

		// Token: 0x04000053 RID: 83
		private string _issuer;

		// Token: 0x0200002D RID: 45
		public enum LogoutRequestFormat
		{
			// Token: 0x0400007D RID: 125
			PlainText,
			// Token: 0x0400007E RID: 126
			Base64
		}
	}
}
