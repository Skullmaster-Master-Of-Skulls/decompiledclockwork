using System;
using System.IdentityModel.Protocols.WSTrust;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012C RID: 300
	public abstract class ProofDescriptor
	{
		// Token: 0x06000866 RID: 2150
		public abstract void ApplyTo(RequestSecurityTokenResponse response);

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000867 RID: 2151
		public abstract SecurityKeyIdentifier KeyIdentifier { get; }
	}
}
