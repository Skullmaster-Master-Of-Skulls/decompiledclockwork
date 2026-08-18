using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000245 RID: 581
	[Flags]
	internal enum ComRights
	{
		// Token: 0x040018B8 RID: 6328
		EXECUTE = 1,
		// Token: 0x040018B9 RID: 6329
		EXECUTE_LOCAL = 2,
		// Token: 0x040018BA RID: 6330
		EXECUTE_REMOTE = 4,
		// Token: 0x040018BB RID: 6331
		ACTIVATE_LOCAL = 8,
		// Token: 0x040018BC RID: 6332
		ACTIVATE_REMOTE = 16
	}
}
