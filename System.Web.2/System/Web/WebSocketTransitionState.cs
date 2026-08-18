using System;

namespace System.Web
{
	// Token: 0x02000020 RID: 32
	internal enum WebSocketTransitionState : byte
	{
		// Token: 0x04000103 RID: 259
		Inactive,
		// Token: 0x04000104 RID: 260
		AcceptWebSocketRequestCalled,
		// Token: 0x04000105 RID: 261
		TransitionStarted,
		// Token: 0x04000106 RID: 262
		TransitionCompleted
	}
}
