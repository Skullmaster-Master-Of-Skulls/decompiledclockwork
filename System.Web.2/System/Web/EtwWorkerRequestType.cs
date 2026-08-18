using System;

namespace System.Web
{
	// Token: 0x02000069 RID: 105
	internal enum EtwWorkerRequestType
	{
		// Token: 0x040001E9 RID: 489
		Undefined = -1,
		// Token: 0x040001EA RID: 490
		InProc,
		// Token: 0x040001EB RID: 491
		OutOfProc,
		// Token: 0x040001EC RID: 492
		IIS7Integrated = 3,
		// Token: 0x040001ED RID: 493
		Unknown = 999
	}
}
