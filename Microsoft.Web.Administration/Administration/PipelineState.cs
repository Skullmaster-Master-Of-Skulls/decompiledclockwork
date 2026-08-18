using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000060 RID: 96
	public enum PipelineState
	{
		// Token: 0x040000D9 RID: 217
		Unknown,
		// Token: 0x040000DA RID: 218
		BeginRequest,
		// Token: 0x040000DB RID: 219
		AuthenticateRequest,
		// Token: 0x040000DC RID: 220
		AuthorizeRequest = 4,
		// Token: 0x040000DD RID: 221
		ResolveRequestCache = 8,
		// Token: 0x040000DE RID: 222
		MapRequestHandler = 16,
		// Token: 0x040000DF RID: 223
		AcquireRequestState = 32,
		// Token: 0x040000E0 RID: 224
		PreExecuteRequestHandler = 64,
		// Token: 0x040000E1 RID: 225
		ExecuteRequestHandler = 128,
		// Token: 0x040000E2 RID: 226
		ReleaseRequestState = 256,
		// Token: 0x040000E3 RID: 227
		UpdateRequestCache = 512,
		// Token: 0x040000E4 RID: 228
		LogRequest = 1024,
		// Token: 0x040000E5 RID: 229
		EndRequest = 2048,
		// Token: 0x040000E6 RID: 230
		SendResponse = 536870912
	}
}
