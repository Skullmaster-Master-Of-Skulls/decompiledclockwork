using System;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099A RID: 2458
	internal sealed class BufferedReceiveMessageProperty
	{
		// Token: 0x06005FE9 RID: 24553 RVA: 0x001660B8 File Offset: 0x001642B8
		internal BufferedReceiveMessageProperty(ref MessageRpc rpc)
		{
			this.RequestContext = new BufferedRequestContext(rpc.RequestContext);
			rpc.RequestContext = this.RequestContext;
			this.Notification = rpc.InvokeNotification;
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06005FEA RID: 24554 RVA: 0x001660E9 File Offset: 0x001642E9
		public static string Name
		{
			get
			{
				return "BufferedReceiveMessageProperty";
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06005FEB RID: 24555 RVA: 0x001660F0 File Offset: 0x001642F0
		// (set) Token: 0x06005FEC RID: 24556 RVA: 0x001660F8 File Offset: 0x001642F8
		public object UserState { get; set; }

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x00166101 File Offset: 0x00164301
		// (set) Token: 0x06005FEE RID: 24558 RVA: 0x00166109 File Offset: 0x00164309
		public BufferedRequestContext RequestContext { get; private set; }

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x00166112 File Offset: 0x00164312
		// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x0016611A File Offset: 0x0016431A
		internal IInvokeReceivedNotification Notification { get; private set; }

		// Token: 0x06005FF1 RID: 24561 RVA: 0x00166123 File Offset: 0x00164323
		public void RegisterForReplay(OperationContext operationContext)
		{
			this.messageBuffer = (MessageBuffer)operationContext.IncomingMessageProperties["_RequestMessageBuffer_"];
			operationContext.IncomingMessageProperties["_RequestMessageBuffer_"] = BufferedReceiveMessageProperty.dummyMessageBuffer;
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x00166158 File Offset: 0x00164358
		public void ReplayRequest()
		{
			Message message = this.messageBuffer.CreateMessage();
			message.Properties["_RequestMessageBuffer_"] = this.messageBuffer;
			this.RequestContext.ReInitialize(message);
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x00166193 File Offset: 0x00164393
		public static bool TryGet(Message message, out BufferedReceiveMessageProperty property)
		{
			return BufferedReceiveMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x001661A4 File Offset: 0x001643A4
		public static bool TryGet(MessageProperties properties, out BufferedReceiveMessageProperty property)
		{
			object obj = null;
			if (properties.TryGetValue("BufferedReceiveMessageProperty", out obj))
			{
				property = (obj as BufferedReceiveMessageProperty);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x04003861 RID: 14433
		private const string PropertyName = "BufferedReceiveMessageProperty";

		// Token: 0x04003862 RID: 14434
		private MessageBuffer messageBuffer;

		// Token: 0x04003863 RID: 14435
		private static MessageBuffer dummyMessageBuffer = Message.CreateMessage(MessageVersion.Default, string.Empty).CreateBufferedCopy(1);
	}
}
