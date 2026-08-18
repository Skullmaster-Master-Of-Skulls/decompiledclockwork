using System;

namespace System.ServiceModel
{
	// Token: 0x0200002F RID: 47
	[__DynamicallyInvokable]
	public interface IDefaultCommunicationTimeouts
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019C RID: 412
		[__DynamicallyInvokable]
		TimeSpan CloseTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019D RID: 413
		[__DynamicallyInvokable]
		TimeSpan OpenTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600019E RID: 414
		[__DynamicallyInvokable]
		TimeSpan ReceiveTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600019F RID: 415
		[__DynamicallyInvokable]
		TimeSpan SendTimeout { [__DynamicallyInvokable] get; }
	}
}
