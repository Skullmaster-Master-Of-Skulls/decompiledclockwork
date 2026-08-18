using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel
{
	// Token: 0x0200004A RID: 74
	internal interface IWrappedTokenKeyResolver
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CF RID: 719
		// (set) Token: 0x060002D0 RID: 720
		SecurityToken ExpectedWrapper { get; set; }

		// Token: 0x060002D1 RID: 721
		bool CheckExternalWrapperMatch(SecurityKeyIdentifier keyIdentifier);
	}
}
