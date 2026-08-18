using System;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000016 RID: 22
	public class SamlpRequestWriter
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004060 File Offset: 0x00002260
		public void WriteToSamlp(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
			this.WriteAssertionConsumerUrl(writer, tokenRequest);
			this.WriteDestination(writer, tokenRequest);
			this.WriteID(writer, tokenRequest);
			this.WriteIssueInstant(writer, tokenRequest);
			this.WriteProtocolBinding(writer, tokenRequest);
			this.WriteVersion(writer, tokenRequest);
			this.WriteIssuer(writer, tokenRequest);
			this.WriteNameIDPolicy(writer, tokenRequest);
			writer.WriteEndElement();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000040D3 File Offset: 0x000022D3
		protected void WriteAssertionConsumerUrl(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("AssertionConsumerServiceURL", tokenRequest.AssertionConsumerServiceURL);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000040E8 File Offset: 0x000022E8
		protected void WriteDestination(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("Destination", tokenRequest.Destination);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000040FD File Offset: 0x000022FD
		protected void WriteID(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("ID", tokenRequest.ID);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004114 File Offset: 0x00002314
		protected void WriteIssueInstant(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("IssueInstant", DateTime.UtcNow.ToString("o"));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004140 File Offset: 0x00002340
		protected void WriteProtocolBinding(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004154 File Offset: 0x00002354
		protected void WriteVersion(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteAttributeString("Version", "2.0");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004168 File Offset: 0x00002368
		protected void WriteIssuer(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
			writer.WriteString(tokenRequest.Issuer);
			writer.WriteEndElement();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004195 File Offset: 0x00002395
		protected void WriteNameIDPolicy(XmlWriter writer, SamlpRequest tokenRequest)
		{
			writer.WriteStartElement("samlp", "NameIDPolicy");
			writer.WriteAttributeString("AllowCreate", tokenRequest.NameIDPolicy_AllowCreation ? "1" : "0");
			writer.WriteEndElement();
		}
	}
}
