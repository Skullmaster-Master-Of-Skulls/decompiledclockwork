using System;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2D RID: 2605
	internal class PeerRequestSecurityToken : RequestSecurityToken
	{
		// Token: 0x0600675E RID: 26462 RVA: 0x00181FA5 File Offset: 0x001801A5
		public PeerRequestSecurityToken(PeerHashToken token)
		{
			this.token = token;
			base.TokenType = "http://schemas.microsoft.com/net/2006/05/peer/peerhashtoken";
			base.RequestType = "http://schemas.xmlsoap.org/ws/2005/02/trust/Validate";
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x00181FCA File Offset: 0x001801CA
		public PeerHashToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x00181FD4 File Offset: 0x001801D4
		public static PeerHashToken CreateHashTokenFrom(Message message)
		{
			PeerHashToken result = PeerHashToken.Invalid;
			XmlReader readerAtBodyContents = message.GetReaderAtBodyContents();
			RequestSecurityToken requestSecurityToken = RequestSecurityToken.CreateFrom(readerAtBodyContents);
			XmlElement requestSecurityTokenXml = requestSecurityToken.RequestSecurityTokenXml;
			if (requestSecurityTokenXml != null)
			{
				foreach (object obj in requestSecurityToken.RequestSecurityTokenXml.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement != null && PeerRequestSecurityToken.CompareWithNS(xmlElement.LocalName, xmlElement.NamespaceURI, "RequestedSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"))
					{
						result = PeerHashToken.CreateFrom(xmlElement);
					}
				}
			}
			return result;
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x00182088 File Offset: 0x00180288
		public PeerRequestSecurityToken CreateFrom(X509Certificate2 credential, string password)
		{
			PeerHashToken peerHashToken = new PeerHashToken(credential, password);
			return new PeerRequestSecurityToken(peerHashToken);
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x001820A4 File Offset: 0x001802A4
		protected internal override void OnWriteCustomElements(XmlWriter writer)
		{
			if (this.token == null || !this.token.IsValid)
			{
				throw Fx.AssertAndThrow("Could not construct a valid RST without token!");
			}
			string prefix = writer.LookupPrefix("http://schemas.xmlsoap.org/ws/2005/02/trust");
			writer.WriteStartElement(prefix, "RequestedSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			this.token.Write(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x00182100 File Offset: 0x00180300
		protected internal override void OnMakeReadOnly()
		{
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00182102 File Offset: 0x00180302
		internal static bool CompareWithNS(string first, string firstNS, string second, string secondNS)
		{
			return string.Compare(first, second, StringComparison.Ordinal) == 0 && string.Compare(firstNS, secondNS, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x04003B58 RID: 15192
		private PeerHashToken token;

		// Token: 0x04003B59 RID: 15193
		public const string TrustNamespace = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x04003B5A RID: 15194
		public const string PeerNamespace = "http://schemas.microsoft.com/net/2006/05/peer";

		// Token: 0x04003B5B RID: 15195
		public const string RequestElementName = "RequestSecurityToken";

		// Token: 0x04003B5C RID: 15196
		public const string RequestedSecurityTokenElementName = "RequestedSecurityToken";

		// Token: 0x04003B5D RID: 15197
		public const string PeerHashTokenElementName = "PeerHashToken";
	}
}
