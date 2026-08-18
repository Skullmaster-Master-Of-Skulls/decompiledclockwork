using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000910 RID: 2320
	internal interface IReliableChannelBinder
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06005878 RID: 22648
		bool CanSendAsynchronously { get; }

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06005879 RID: 22649
		IChannel Channel { get; }

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x0600587A RID: 22650
		bool Connected { get; }

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x0600587B RID: 22651
		TimeSpan DefaultSendTimeout { get; }

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x0600587C RID: 22652
		bool HasSession { get; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x0600587D RID: 22653
		EndpointAddress LocalAddress { get; }

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x0600587E RID: 22654
		EndpointAddress RemoteAddress { get; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x0600587F RID: 22655
		CommunicationState State { get; }

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06005880 RID: 22656
		// (remove) Token: 0x06005881 RID: 22657
		event BinderExceptionHandler Faulted;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06005882 RID: 22658
		// (remove) Token: 0x06005883 RID: 22659
		event BinderExceptionHandler OnException;

		// Token: 0x06005884 RID: 22660
		void Abort();

		// Token: 0x06005885 RID: 22661
		void Close(TimeSpan timeout);

		// Token: 0x06005886 RID: 22662
		void Close(TimeSpan timeout, MaskingMode maskingMode);

		// Token: 0x06005887 RID: 22663
		IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005888 RID: 22664
		IAsyncResult BeginClose(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state);

		// Token: 0x06005889 RID: 22665
		void EndClose(IAsyncResult result);

		// Token: 0x0600588A RID: 22666
		void Open(TimeSpan timeout);

		// Token: 0x0600588B RID: 22667
		IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600588C RID: 22668
		void EndOpen(IAsyncResult result);

		// Token: 0x0600588D RID: 22669
		IAsyncResult BeginSend(Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state);

		// Token: 0x0600588E RID: 22670
		IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600588F RID: 22671
		void EndSend(IAsyncResult result);

		// Token: 0x06005890 RID: 22672
		void Send(Message message, TimeSpan timeout);

		// Token: 0x06005891 RID: 22673
		void Send(Message message, TimeSpan timeout, MaskingMode maskingMode);

		// Token: 0x06005892 RID: 22674
		bool TryReceive(TimeSpan timeout, out RequestContext requestContext);

		// Token: 0x06005893 RID: 22675
		bool TryReceive(TimeSpan timeout, out RequestContext requestContext, MaskingMode maskingMode);

		// Token: 0x06005894 RID: 22676
		IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005895 RID: 22677
		IAsyncResult BeginTryReceive(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state);

		// Token: 0x06005896 RID: 22678
		bool EndTryReceive(IAsyncResult result, out RequestContext requestContext);

		// Token: 0x06005897 RID: 22679
		ISession GetInnerSession();

		// Token: 0x06005898 RID: 22680
		void HandleException(Exception e);

		// Token: 0x06005899 RID: 22681
		bool IsHandleable(Exception e);

		// Token: 0x0600589A RID: 22682
		void SetMaskingMode(RequestContext context, MaskingMode maskingMode);

		// Token: 0x0600589B RID: 22683
		RequestContext WrapRequestContext(RequestContext context);
	}
}
