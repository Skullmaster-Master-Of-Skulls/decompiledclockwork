using System;

namespace System.Web
{
	// Token: 0x020000ED RID: 237
	[Flags]
	public enum RequestNotification
	{
		// Token: 0x0400057C RID: 1404
		BeginRequest = 1,
		// Token: 0x0400057D RID: 1405
		AuthenticateRequest = 2,
		// Token: 0x0400057E RID: 1406
		AuthorizeRequest = 4,
		// Token: 0x0400057F RID: 1407
		ResolveRequestCache = 8,
		// Token: 0x04000580 RID: 1408
		MapRequestHandler = 16,
		// Token: 0x04000581 RID: 1409
		AcquireRequestState = 32,
		// Token: 0x04000582 RID: 1410
		PreExecuteRequestHandler = 64,
		// Token: 0x04000583 RID: 1411
		ExecuteRequestHandler = 128,
		// Token: 0x04000584 RID: 1412
		ReleaseRequestState = 256,
		// Token: 0x04000585 RID: 1413
		UpdateRequestCache = 512,
		// Token: 0x04000586 RID: 1414
		LogRequest = 1024,
		// Token: 0x04000587 RID: 1415
		EndRequest = 2048,
		// Token: 0x04000588 RID: 1416
		SendResponse = 536870912
	}
}
