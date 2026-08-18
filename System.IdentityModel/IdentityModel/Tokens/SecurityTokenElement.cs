using System;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000172 RID: 370
	public class SecurityTokenElement
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00036F7C File Offset: 0x0003517C
		public SecurityTokenElement(SecurityToken securityToken)
		{
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityToken");
			}
			GenericXmlSecurityToken genericXmlSecurityToken = securityToken as GenericXmlSecurityToken;
			if (genericXmlSecurityToken != null)
			{
				this._securityTokenXml = genericXmlSecurityToken.TokenXml;
			}
			this._securityToken = securityToken;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00036FBF File Offset: 0x000351BF
		public SecurityTokenElement(XmlElement securityTokenXml, SecurityTokenHandlerCollection securityTokenHandlers)
		{
			if (securityTokenXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenXml");
			}
			if (securityTokenHandlers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlers");
			}
			this._securityTokenXml = securityTokenXml;
			this._securityTokenHandlers = securityTokenHandlers;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00036FFB File Offset: 0x000351FB
		public XmlElement SecurityTokenXml
		{
			get
			{
				return this._securityTokenXml;
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00037003 File Offset: 0x00035203
		public SecurityToken GetSecurityToken()
		{
			if (this._securityToken == null)
			{
				this._securityToken = this.ReadSecurityToken(this._securityTokenXml, this._securityTokenHandlers);
			}
			return this._securityToken;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003702B File Offset: 0x0003522B
		public ReadOnlyCollection<ClaimsIdentity> GetIdentities()
		{
			if (this._subject == null)
			{
				this._subject = this.ValidateToken(this._securityTokenXml, this._securityTokenHandlers);
			}
			return this._subject;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00037054 File Offset: 0x00035254
		protected virtual ReadOnlyCollection<ClaimsIdentity> ValidateToken(XmlElement securityTokenXml, SecurityTokenHandlerCollection securityTokenHandlers)
		{
			if (securityTokenXml == null || securityTokenHandlers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4052")));
			}
			SecurityToken securityToken = this.GetSecurityToken();
			return securityTokenHandlers.ValidateToken(securityToken);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00037090 File Offset: 0x00035290
		protected virtual SecurityToken ReadSecurityToken(XmlElement securityTokenXml, SecurityTokenHandlerCollection securityTokenHandlers)
		{
			XmlReader xmlReader = new XmlNodeReader(securityTokenXml);
			xmlReader.MoveToContent();
			SecurityToken securityToken = securityTokenHandlers.ReadToken(xmlReader);
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4051", new object[]
				{
					securityTokenXml,
					xmlReader.LocalName,
					xmlReader.NamespaceURI
				})));
			}
			return securityToken;
		}

		// Token: 0x04000C3A RID: 3130
		private SecurityToken _securityToken;

		// Token: 0x04000C3B RID: 3131
		private XmlElement _securityTokenXml;

		// Token: 0x04000C3C RID: 3132
		private SecurityTokenHandlerCollection _securityTokenHandlers;

		// Token: 0x04000C3D RID: 3133
		private ReadOnlyCollection<ClaimsIdentity> _subject;
	}
}
