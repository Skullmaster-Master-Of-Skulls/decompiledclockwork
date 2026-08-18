using System;

namespace System.IdentityModel
{
	// Token: 0x02000049 RID: 73
	internal interface ISignatureValueSecurityElement : ISecurityElement
	{
		// Token: 0x060002CE RID: 718
		byte[] GetSignatureValue();
	}
}
