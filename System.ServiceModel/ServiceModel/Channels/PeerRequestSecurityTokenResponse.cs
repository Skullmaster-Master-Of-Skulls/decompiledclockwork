using System;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2E RID: 2606
	internal class PeerRequestSecurityTokenResponse : RequestSecurityTokenResponse
	{
		// Token: 0x06006765 RID: 26469 RVA: 0x0018211B File Offset: 0x0018031B
		public PeerRequestSecurityTokenResponse() : this(null)
		{
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x00182124 File Offset: 0x00180324
		public PeerRequestSecurityTokenResponse(PeerHashToken token)
		{
			this.token = token;
			this.isValid = (token != null && token.IsValid);
		}

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06006767 RID: 26471 RVA: 0x00182145 File Offset: 0x00180345
		public PeerHashToken Token
		{
			get
			{
				if (!this.isValid)
				{
					throw Fx.AssertAndThrow("should not be called when the token is invalid!");
				}
				return this.token;
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06006768 RID: 26472 RVA: 0x00182160 File Offset: 0x00180360
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x00182168 File Offset: 0x00180368
		public static PeerHashToken CreateHashTokenFrom(Message message)
		{
			PeerHashToken result = PeerHashToken.Invalid;
			RequestSecurityTokenResponse requestSecurityTokenResponse = RequestSecurityTokenResponse.CreateFrom(message.GetReaderAtBodyContents(), MessageSecurityVersion.Default, new PeerSecurityTokenSerializer());
			if (string.Compare(requestSecurityTokenResponse.TokenType, "http://schemas.microsoft.com/net/2006/05/peer/peerhashtoken", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return result;
			}
			XmlElement requestSecurityTokenResponseXml = requestSecurityTokenResponse.RequestSecurityTokenResponseXml;
			if (requestSecurityTokenResponseXml != null)
			{
				foreach (object obj in requestSecurityTokenResponseXml.ChildNodes)
				{
					XmlElement xmlElement = (XmlElement)obj;
					if (PeerRequestSecurityToken.CompareWithNS(xmlElement.LocalName, xmlElement.NamespaceURI, "Status", "http://schemas.xmlsoap.org/ws/2005/02/trust"))
					{
						if (xmlElement.ChildNodes.Count == 1)
						{
							XmlElement xmlElement2 = xmlElement.ChildNodes[0] as XmlElement;
							if (PeerRequestSecurityToken.CompareWithNS(xmlElement2.LocalName, xmlElement2.NamespaceURI, "Code", "http://schemas.xmlsoap.org/ws/2005/02/trust"))
							{
								string strA = XmlHelper.ReadTextElementAsTrimmedString(xmlElement2);
								if (string.Compare(strA, "http://schemas.xmlsoap.org/ws/2005/02/trust/status/valid", StringComparison.OrdinalIgnoreCase) != 0)
								{
									break;
								}
							}
						}
					}
					else if (PeerRequestSecurityToken.CompareWithNS(xmlElement.LocalName, xmlElement.NamespaceURI, "RequestedSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust"))
					{
						result = PeerHashToken.CreateFrom(xmlElement);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x001822A8 File Offset: 0x001804A8
		public static RequestSecurityTokenResponse CreateFrom(X509Certificate2 credential, string password)
		{
			PeerHashToken peerHashToken = new PeerHashToken(credential, password);
			return new PeerRequestSecurityTokenResponse(peerHashToken);
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x001822C4 File Offset: 0x001804C4
		protected internal override void OnWriteCustomElements(XmlWriter writer)
		{
			string prefix = writer.LookupPrefix("http://schemas.xmlsoap.org/ws/2005/02/trust");
			writer.WriteStartElement(prefix, "TokenType", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			writer.WriteString("http://schemas.microsoft.com/net/2006/05/peer/peerhashtoken");
			writer.WriteEndElement();
			writer.WriteStartElement(prefix, "Status", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			writer.WriteStartElement(prefix, "Code", "http://schemas.xmlsoap.org/ws/2005/02/trust");
			if (!this.IsValid)
			{
				writer.WriteString("http://schemas.xmlsoap.org/ws/2005/02/trust/status/invalid");
			}
			else
			{
				writer.WriteString("http://schemas.xmlsoap.org/ws/2005/02/trust/status/valid");
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
			if (this.IsValid)
			{
				writer.WriteStartElement(prefix, "RequestedSecurityToken", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				this.token.Write(writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x04003B5E RID: 15198
		public const string Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate";

		// Token: 0x04003B5F RID: 15199
		public const string ValidString = "http://schemas.xmlsoap.org/ws/2005/02/trust/status/valid";

		// Token: 0x04003B60 RID: 15200
		public const string InvalidString = "http://schemas.xmlsoap.org/ws/2005/02/trust/status/invalid";

		// Token: 0x04003B61 RID: 15201
		public const string StatusString = "Status";

		// Token: 0x04003B62 RID: 15202
		public const string CodeString = "Code";

		// Token: 0x04003B63 RID: 15203
		private PeerHashToken token;

		// Token: 0x04003B64 RID: 15204
		private bool isValid;
	}
}
