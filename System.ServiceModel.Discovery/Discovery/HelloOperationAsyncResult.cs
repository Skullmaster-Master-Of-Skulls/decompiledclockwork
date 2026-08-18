using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000034 RID: 52
	internal abstract class HelloOperationAsyncResult<TMessage> : AsyncResult where TMessage : class
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000872C File Offset: 0x0000692C
		internal HelloOperationAsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, TMessage message, AsyncCallback callback, object state) : base(callback, state)
		{
			this.announcementServiceImpl = announcementServiceImpl;
			if (this.IsInvalid(message))
			{
				base.Complete(true);
				return;
			}
			IAsyncResult asyncResult = this.announcementServiceImpl.OnBeginOnlineAnnouncement(this.GetMessageSequence(message), this.GetEndpointDiscoveryMetadata(message), base.PrepareAsyncCompletion(HelloOperationAsyncResult<TMessage>.onOnOnlineAnnoucementCompletedCallback), this);
			if (asyncResult.CompletedSynchronously && HelloOperationAsyncResult<TMessage>.OnOnOnlineAnnouncementCompleted(asyncResult))
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x060002AE RID: 686
		protected abstract bool ValidateContent(TMessage message);

		// Token: 0x060002AF RID: 687
		protected abstract DiscoveryMessageSequence GetMessageSequence(TMessage message);

		// Token: 0x060002B0 RID: 688
		protected abstract EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(TMessage message);

		// Token: 0x060002B1 RID: 689 RVA: 0x00008798 File Offset: 0x00006998
		private static bool OnOnOnlineAnnouncementCompleted(IAsyncResult result)
		{
			HelloOperationAsyncResult<TMessage> helloOperationAsyncResult = (HelloOperationAsyncResult<TMessage>)result.AsyncState;
			helloOperationAsyncResult.announcementServiceImpl.OnEndOnlineAnnouncement(result);
			return true;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000087C0 File Offset: 0x000069C0
		private bool IsInvalid(TMessage message)
		{
			EventTraceActivity eventTraceActivity = null;
			if (Fx.Trace.IsEtwProviderEnabled)
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(OperationContext.Current.IncomingMessage);
			}
			UniqueId messageId = OperationContext.Current.IncomingMessageHeaders.MessageId;
			if (messageId == null)
			{
				if (TD.DiscoveryMessageWithNullMessageIdIsEnabled())
				{
					TD.DiscoveryMessageWithNullMessageId(eventTraceActivity, "Hello");
				}
				return true;
			}
			if (this.announcementServiceImpl.IsDuplicate(messageId))
			{
				if (TD.DuplicateDiscoveryMessageIsEnabled())
				{
					TD.DuplicateDiscoveryMessage(eventTraceActivity, "Hello", messageId.ToString());
				}
				return true;
			}
			if (this.ValidateContent(message))
			{
				return false;
			}
			if (TD.DiscoveryMessageWithInvalidContentIsEnabled())
			{
				TD.DiscoveryMessageWithInvalidContent(eventTraceActivity, "Hello", messageId.ToString());
			}
			return true;
		}

		// Token: 0x040000A4 RID: 164
		private static AsyncResult.AsyncCompletion onOnOnlineAnnoucementCompletedCallback = new AsyncResult.AsyncCompletion(HelloOperationAsyncResult<TMessage>.OnOnOnlineAnnouncementCompleted);

		// Token: 0x040000A5 RID: 165
		private IAnnouncementServiceImplementation announcementServiceImpl;
	}
}
