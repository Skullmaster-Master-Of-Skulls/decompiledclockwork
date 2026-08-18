using System;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200016B RID: 363
	public abstract class SecurityKeyIdentifierClauseSerializer
	{
		// Token: 0x06000B77 RID: 2935
		public abstract bool CanReadKeyIdentifierClause(XmlReader reader);

		// Token: 0x06000B78 RID: 2936
		public abstract bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause securityKeyIdentifierClause);

		// Token: 0x06000B79 RID: 2937
		public abstract SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader);

		// Token: 0x06000B7A RID: 2938
		public abstract void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause securityKeyIdentifierClause);
	}
}
