using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002FB RID: 763
	public abstract class SecurityStateEncoder
	{
		// Token: 0x060019E5 RID: 6629
		protected internal abstract byte[] DecodeSecurityState(byte[] data);

		// Token: 0x060019E6 RID: 6630
		protected internal abstract byte[] EncodeSecurityState(byte[] data);
	}
}
