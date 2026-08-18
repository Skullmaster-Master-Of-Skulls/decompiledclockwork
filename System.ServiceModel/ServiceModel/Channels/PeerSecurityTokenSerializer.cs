using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2C RID: 2604
	internal class PeerSecurityTokenSerializer : WSSecurityTokenSerializer
	{
		// Token: 0x0600675C RID: 26460 RVA: 0x00181F9A File Offset: 0x0018019A
		public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXml(XmlElement element, SecurityTokenReferenceStyle tokenReferenceStyle)
		{
			return null;
		}
	}
}
