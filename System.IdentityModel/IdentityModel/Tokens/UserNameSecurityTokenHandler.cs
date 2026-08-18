using System;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000188 RID: 392
	public abstract class UserNameSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0003B9A7 File Offset: 0x00039BA7
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0003B9AF File Offset: 0x00039BAF
		public virtual bool RetainPassword
		{
			get
			{
				return this._retainPassword;
			}
			set
			{
				this._retainPassword = value;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003B9B8 File Offset: 0x00039BB8
		public override bool CanReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003B9DD File Offset: 0x00039BDD
		public override Type TokenType
		{
			get
			{
				return typeof(UserNameSecurityToken);
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003B9E9 File Offset: 0x00039BE9
		public override string[] GetTokenTypeIdentifiers()
		{
			return new string[]
			{
				SecurityTokenTypes.UserName
			};
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0003B9FC File Offset: 0x00039BFC
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!this.CanReadToken(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4065", new object[]
				{
					"Username",
					"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			string text = null;
			string password = null;
			reader.MoveToContent();
			string attribute = reader.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			reader.ReadStartElement("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement("Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					text = reader.ReadElementString();
				}
				else if (reader.IsStartElement("Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					string attribute2 = reader.GetAttribute("Type", null);
					if (!string.IsNullOrEmpty(attribute2) && !StringComparer.Ordinal.Equals(attribute2, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID4059", new object[]
						{
							attribute2,
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"
						})));
					}
					password = reader.ReadElementString();
				}
				else if (reader.IsStartElement("Nonce", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					reader.Skip();
				}
				else
				{
					if (!reader.IsStartElement("Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ID4060", new object[]
						{
							reader.LocalName,
							reader.NamespaceURI,
							"UsernameToken",
							"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"
						})));
					}
					reader.Skip();
				}
			}
			reader.ReadEndElement();
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4061"));
			}
			if (!string.IsNullOrEmpty(attribute))
			{
				return new UserNameSecurityToken(text, password, attribute);
			}
			return new UserNameSecurityToken(text, password);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003BBE0 File Offset: 0x00039DE0
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
			UserNameSecurityToken userNameSecurityToken = token as UserNameSecurityToken;
			if (userNameSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(UserNameSecurityToken)
				}));
			}
			writer.WriteStartElement("UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			if (!string.IsNullOrEmpty(token.Id))
			{
				writer.WriteAttributeString("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", token.Id);
			}
			writer.WriteElementString("Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", userNameSecurityToken.UserName);
			if (userNameSecurityToken.Password != null)
			{
				writer.WriteStartElement("Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
				writer.WriteAttributeString("Type", null, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");
				writer.WriteString(userNameSecurityToken.Password);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.Flush();
		}

		// Token: 0x04000C9C RID: 3228
		private bool _retainPassword;
	}
}
