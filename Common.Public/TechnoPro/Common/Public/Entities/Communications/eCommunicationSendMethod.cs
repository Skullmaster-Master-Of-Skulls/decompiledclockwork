using System;

namespace TechnoPro.Common.Public.Entities.Communications
{
	// Token: 0x02000445 RID: 1093
	[Flags]
	[Serializable]
	public enum eCommunicationSendMethod
	{
		// Token: 0x04001917 RID: 6423
		Unknown = 0,
		// Token: 0x04001918 RID: 6424
		Email = 1,
		// Token: 0x04001919 RID: 6425
		TextMessage = 2,
		// Token: 0x0400191A RID: 6426
		EmailAndTextMessage = 3
	}
}
