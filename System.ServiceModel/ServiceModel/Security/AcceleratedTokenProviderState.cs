using System;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032C RID: 812
	internal class AcceleratedTokenProviderState : IssuanceTokenProviderState
	{
		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006BD98 File Offset: 0x00069F98
		public AcceleratedTokenProviderState(byte[] value)
		{
			this.entropy = value;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006BDA7 File Offset: 0x00069FA7
		public byte[] GetRequestorEntropy()
		{
			return this.entropy;
		}

		// Token: 0x04001DE8 RID: 7656
		private byte[] entropy;
	}
}
