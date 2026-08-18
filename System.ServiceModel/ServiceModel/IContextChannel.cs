using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000FC RID: 252
	[__DynamicallyInvokable]
	public interface IContextChannel : IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000540 RID: 1344
		// (set) Token: 0x06000541 RID: 1345
		[__DynamicallyInvokable]
		bool AllowOutputBatching { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000542 RID: 1346
		[__DynamicallyInvokable]
		IInputSession InputSession { [__DynamicallyInvokable] get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000543 RID: 1347
		[__DynamicallyInvokable]
		EndpointAddress LocalAddress { [__DynamicallyInvokable] get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000544 RID: 1348
		// (set) Token: 0x06000545 RID: 1349
		[__DynamicallyInvokable]
		TimeSpan OperationTimeout { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000546 RID: 1350
		[__DynamicallyInvokable]
		IOutputSession OutputSession { [__DynamicallyInvokable] get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000547 RID: 1351
		[__DynamicallyInvokable]
		EndpointAddress RemoteAddress { [__DynamicallyInvokable] get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000548 RID: 1352
		[__DynamicallyInvokable]
		string SessionId { [__DynamicallyInvokable] get; }
	}
}
