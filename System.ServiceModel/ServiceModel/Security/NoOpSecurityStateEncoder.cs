using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000364 RID: 868
	internal class NoOpSecurityStateEncoder : SecurityStateEncoder
	{
		// Token: 0x06001FDD RID: 8157 RVA: 0x00077383 File Offset: 0x00075583
		protected internal override byte[] EncodeSecurityState(byte[] data)
		{
			return data;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00077386 File Offset: 0x00075586
		protected internal override byte[] DecodeSecurityState(byte[] data)
		{
			return data;
		}
	}
}
