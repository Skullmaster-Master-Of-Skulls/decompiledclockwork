using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000014 RID: 20
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal class BufferedGenericXmlSecurityToken : GenericXmlSecurityToken
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000323B File Offset: 0x0000143B
		public BufferedGenericXmlSecurityToken(XmlElement tokenXml, SecurityToken proofToken, DateTime effectiveTime, DateTime expirationTime, SecurityKeyIdentifierClause internalTokenReference, SecurityKeyIdentifierClause externalTokenReference, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, XmlBuffer tokenXmlBuffer) : base(tokenXml, proofToken, effectiveTime, expirationTime, internalTokenReference, externalTokenReference, authorizationPolicies)
		{
			this.tokenXmlBuffer = tokenXmlBuffer;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003256 File Offset: 0x00001456
		public XmlBuffer TokenXmlBuffer
		{
			get
			{
				return this.tokenXmlBuffer;
			}
		}

		// Token: 0x04000081 RID: 129
		private XmlBuffer tokenXmlBuffer;
	}
}
