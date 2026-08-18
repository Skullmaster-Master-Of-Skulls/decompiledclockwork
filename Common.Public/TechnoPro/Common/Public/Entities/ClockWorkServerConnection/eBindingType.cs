using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerConnection
{
	// Token: 0x02000451 RID: 1105
	[Serializable]
	public enum eBindingType
	{
		// Token: 0x04001956 RID: 6486
		Unspecified,
		// Token: 0x04001957 RID: 6487
		NetTcpBinding,
		// Token: 0x04001958 RID: 6488
		HttpBinding,
		// Token: 0x04001959 RID: 6489
		MsmqBinding,
		// Token: 0x0400195A RID: 6490
		NetPipeBinding
	}
}
