using System;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000011 RID: 17
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class SecurityContextKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002FA4 File Offset: 0x000011A4
		public SecurityContextKeyIdentifierClause(System.Xml.UniqueId contextId) : this(contextId, null)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FAE File Offset: 0x000011AE
		public SecurityContextKeyIdentifierClause(System.Xml.UniqueId contextId, System.Xml.UniqueId generation) : this(contextId, generation, null, 0)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FBA File Offset: 0x000011BA
		public SecurityContextKeyIdentifierClause(System.Xml.UniqueId contextId, System.Xml.UniqueId generation, byte[] derivationNonce, int derivationLength) : base(null, derivationNonce, derivationLength)
		{
			if (contextId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextId");
			}
			this.contextId = contextId;
			this.generation = generation;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002FED File Offset: 0x000011ED
		public System.Xml.UniqueId ContextId
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FF5 File Offset: 0x000011F5
		public System.Xml.UniqueId Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003000 File Offset: 0x00001200
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = keyIdentifierClause as SecurityContextKeyIdentifierClause;
			return this == securityContextKeyIdentifierClause || (securityContextKeyIdentifierClause != null && securityContextKeyIdentifierClause.Matches(this.contextId, this.generation));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003031 File Offset: 0x00001231
		public bool Matches(System.Xml.UniqueId contextId, System.Xml.UniqueId generation)
		{
			return contextId == this.contextId && generation == this.generation;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000304F File Offset: 0x0000124F
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SecurityContextKeyIdentifierClause(ContextId = '{0}', Generation = '{1}')", new object[]
			{
				this.ContextId,
				this.Generation
			});
		}

		// Token: 0x04000075 RID: 117
		private readonly System.Xml.UniqueId contextId;

		// Token: 0x04000076 RID: 118
		private readonly System.Xml.UniqueId generation;
	}
}
