using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ClockWorkLogger;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000017 RID: 23
	public class SamlResponseReader
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000041D0 File Offset: 0x000023D0
		public Saml2XmlSerializer TokenXmlSerializer
		{
			get
			{
				return new Saml2XmlSerializer();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000041D8 File Offset: 0x000023D8
		public void DeserializeSamlResponse(XmlReader xmlReader, Saml2Response saml2Response, SecurityTokenElement tokenIssuer)
		{
			bool flag = !xmlReader.Read();
			if (flag)
			{
				throw new SecurityTokenValidationException("The XML data representation of a Saml2Response object does not contain any information.");
			}
			saml2Response.IssueInstant = Convert.ToDateTime(xmlReader.GetAttribute("IssueInstant"), CultureInfo.InvariantCulture);
			saml2Response.InResponseTo = xmlReader.GetAttribute("InResponseTo");
			saml2Response.Version = xmlReader.GetAttribute("Version");
			saml2Response.Destination = xmlReader.GetAttribute("Destination");
			saml2Response.ID = xmlReader.GetAttribute("ID");
			while (xmlReader.Read())
			{
				bool flag2 = XmlNodeType.Element != xmlReader.NodeType;
				if (!flag2)
				{
					CWLogger.Logger.Debug("DeserializeSamlResponse:xmlReader.LocalName={0}", xmlReader.LocalName ?? "NULL");
					string localName = xmlReader.LocalName;
					string a = localName;
					if (!(a == "Status"))
					{
						if (!(a == "Issuer"))
						{
							if (!(a == "Assertion"))
							{
								if (a == "EncryptedAssertion")
								{
									this.ProcessEncryptedAssertion(xmlReader, saml2Response, tokenIssuer);
								}
							}
							else
							{
								this.ProcessAssertion(xmlReader, saml2Response, tokenIssuer);
							}
						}
						else
						{
							this.ProcessIssuer(xmlReader, saml2Response);
						}
					}
					else
					{
						this.ProcessStatus(xmlReader, saml2Response);
					}
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000431C File Offset: 0x0000251C
		private void ProcessEncryptedAssertion(XmlReader xmlReader, Saml2Response saml2Response, SecurityTokenElement tokenIssuer)
		{
			CWLogger.Logger.Debug("Common.Security.Saml:ProcessEncryptedAssertion:start");
			bool flag = !xmlReader.IsEmptyElement && xmlReader.NamespaceURI.Equals("urn:oasis:names:tc:SAML:2.0:assertion");
			if (flag)
			{
				CWLogger.Logger.Debug("Common.Security.Saml:ProcessEncryptedAssertion:start2");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);
				List<SecurityToken> tokens;
				Saml2SecurityToken saml2SecurityToken = this.TokenXmlSerializer.DeserializeEncryptedToken(xmlDocument.DocumentElement, out tokens) as Saml2SecurityToken;
				bool flag2 = saml2SecurityToken == null;
				if (flag2)
				{
					throw new SecurityTokenException("The SAML Token embedded in the Saml2Response token couldn't be deserialized");
				}
				bool flag3 = !this.TokenXmlSerializer.VerifySamlBearerEncryptedTokenSignature(xmlDocument.DocumentElement, saml2SecurityToken, tokens, tokenIssuer);
				if (flag3)
				{
					throw new SecurityTokenValidationException("The SAML token signature of the SAML Token embedded in the Saml2Response token couldn't be verified");
				}
				saml2Response.Assertion = saml2SecurityToken;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000043D8 File Offset: 0x000025D8
		private void ProcessIssuer(XmlReader xmlReader, Saml2Response saml2Response)
		{
			bool flag = !xmlReader.IsEmptyElement && xmlReader.NamespaceURI.Equals("urn:oasis:names:tc:SAML:2.0:assertion");
			if (flag)
			{
				saml2Response.Issuer = xmlReader.ReadInnerXml();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004414 File Offset: 0x00002614
		private void ProcessAssertion(XmlReader xmlReader, Saml2Response saml2Response, SecurityTokenElement tokenIssuer)
		{
			bool flag = !xmlReader.IsEmptyElement && xmlReader.NamespaceURI.Equals("urn:oasis:names:tc:SAML:2.0:assertion");
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);
				List<SecurityToken> tokens;
				Saml2SecurityToken saml2SecurityToken = this.TokenXmlSerializer.DeserializeToken(xmlDocument.DocumentElement, out tokens) as Saml2SecurityToken;
				bool flag2 = saml2SecurityToken == null;
				if (flag2)
				{
					throw new SecurityTokenException("The SAML Token embedded in the Saml2Response token couldn't be deserialized");
				}
				bool flag3 = !this.TokenXmlSerializer.VerifySamlBearerTokenSignature(xmlDocument.DocumentElement, saml2SecurityToken, tokens, tokenIssuer);
				if (flag3)
				{
					throw new SecurityTokenValidationException("The SAML token signature of the SAML Token embedded in the Saml2Response token couldn't be verified");
				}
				saml2Response.Assertion = saml2SecurityToken;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000044B0 File Offset: 0x000026B0
		private void ProcessStatus(XmlReader xmlReader, Saml2Response saml2Response)
		{
			bool flag = !xmlReader.IsEmptyElement && xmlReader.NamespaceURI.Equals("urn:oasis:names:tc:SAML:2.0:protocol");
			if (flag)
			{
				XElement xelement = XElement.Load(xmlReader.ReadSubtree(), LoadOptions.SetBaseUri);
				XElement xelement2 = xelement.Descendants().FirstOrDefault((XElement descendant) => descendant.Name.LocalName == "StatusCode" && descendant.Name.Namespace == "urn:oasis:names:tc:SAML:2.0:protocol");
				bool flag2 = xelement2 != null;
				if (flag2)
				{
					XAttribute xattribute = xelement2.Attributes().FirstOrDefault((XAttribute att) => att.Name.LocalName == "Value");
					bool flag3 = xattribute != null;
					if (flag3)
					{
						string value = xattribute.Value;
						bool flag4 = value.StartsWith("urn:oasis:names:tc:SAML:2.0:status:");
						if (flag4)
						{
							saml2Response.StatusCode = new SamlResponseStatusCode?((SamlResponseStatusCode)Enum.Parse(typeof(SamlResponseStatusCode), xattribute.Value.Split(new char[]
							{
								':'
							}).Last<string>()));
						}
					}
				}
			}
		}
	}
}
