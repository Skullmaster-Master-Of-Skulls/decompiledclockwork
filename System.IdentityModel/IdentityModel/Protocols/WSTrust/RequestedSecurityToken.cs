using System;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FE RID: 510
	public class RequestedSecurityToken
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x000475C7 File Offset: 0x000457C7
		public RequestedSecurityToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this._requestedToken = token;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000475E9 File Offset: 0x000457E9
		public RequestedSecurityToken(XmlElement tokenAsXml)
		{
			if (tokenAsXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenAsXml");
			}
			this._tokenAsXml = tokenAsXml;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0004760B File Offset: 0x0004580B
		public virtual XmlElement SecurityTokenXml
		{
			get
			{
				return this._tokenAsXml;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00047613 File Offset: 0x00045813
		public SecurityToken SecurityToken
		{
			get
			{
				return this._requestedToken;
			}
		}

		// Token: 0x04000E80 RID: 3712
		private XmlElement _tokenAsXml;

		// Token: 0x04000E81 RID: 3713
		private SecurityToken _requestedToken;
	}
}
