using System;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000065 RID: 101
	public enum eServiceProviderType
	{
		// Token: 0x04000224 RID: 548
		Unknown,
		// Token: 0x04000225 RID: 549
		Interpreter,
		// Token: 0x04000226 RID: 550
		Teamer,
		// Token: 0x04000227 RID: 551
		Professional_notetaker = 4,
		// Token: 0x04000228 RID: 552
		Coach = 8,
		// Token: 0x04000229 RID: 553
		Specialized_tutor = 16,
		// Token: 0x0400022A RID: 554
		Real_time_captioner = 32,
		// Token: 0x0400022B RID: 555
		Peer_assistant = 64,
		// Token: 0x0400022C RID: 556
		Peer_notetaker = 128,
		// Token: 0x0400022D RID: 557
		Peer_tutor = 256,
		// Token: 0x0400022E RID: 558
		Custom1 = 512,
		// Token: 0x0400022F RID: 559
		Custom2 = 1024,
		// Token: 0x04000230 RID: 560
		Custom3 = 2048,
		// Token: 0x04000231 RID: 561
		Custom4 = 4096,
		// Token: 0x04000232 RID: 562
		Custom5 = 8192
	}
}
