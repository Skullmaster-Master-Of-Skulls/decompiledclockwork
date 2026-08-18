using System;
using System.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000937 RID: 2359
	internal abstract class FaultHelper
	{
		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x0014D237 File Offset: 0x0014B437
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06005AAF RID: 23215
		public abstract void Abort();

		// Token: 0x06005AB0 RID: 23216 RVA: 0x0014D240 File Offset: 0x0014B440
		public static bool AddressReply(Message message, Message faultMessage)
		{
			try
			{
				RequestReplyCorrelator.PrepareReply(faultMessage, message);
			}
			catch (MessageHeaderException exception)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}
			bool result = true;
			try
			{
				result = RequestReplyCorrelator.AddressReply(faultMessage, message);
			}
			catch (MessageHeaderException exception2)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
			}
			return result;
		}

		// Token: 0x06005AB1 RID: 23217
		public abstract IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005AB2 RID: 23218
		public abstract void Close(TimeSpan timeout);

		// Token: 0x06005AB3 RID: 23219
		public abstract void EndClose(IAsyncResult result);

		// Token: 0x06005AB4 RID: 23220
		public abstract void SendFaultAsync(IReliableChannelBinder binder, RequestContext requestContext, Message faultMessage);

		// Token: 0x040036B2 RID: 14002
		private object thisLock = new object();
	}
}
