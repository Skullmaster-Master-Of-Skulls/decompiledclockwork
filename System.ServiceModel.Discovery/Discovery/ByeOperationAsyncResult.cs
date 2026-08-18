using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200000B RID: 11
	internal abstract class ByeOperationAsyncResult<TMessage> : AsyncResult where TMessage : class
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003704 File Offset: 0x00001904
		internal ByeOperationAsyncResult(IAnnouncementServiceImplementation announcementServiceImpl, TMessage message, AsyncCallback callback, object state) : base(callback, state)
		{
			this.announcementServiceImpl = announcementServiceImpl;
			if (this.IsInvalid(message))
			{
				base.Complete(true);
				return;
			}
			IAsyncResult asyncResult = this.announcementServiceImpl.OnBeginOfflineAnnouncement(this.GetMessageSequence(message), this.GetEndpointDiscoveryMetadata(message), base.PrepareAsyncCompletion(ByeOperationAsyncResult<TMessage>.onOnOfflineAnnoucementCompletedCallback), this);
			if (asyncResult.CompletedSynchronously && ByeOperationAsyncResult<TMessage>.OnOnOfflineAnnouncementCompleted(asyncResult))
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x060000A2 RID: 162
		protected abstract bool ValidateContent(TMessage message);

		// Token: 0x060000A3 RID: 163
		protected abstract DiscoveryMessageSequence GetMessageSequence(TMessage message);

		// Token: 0x060000A4 RID: 164
		protected abstract EndpointDiscoveryMetadata GetEndpointDiscoveryMetadata(TMessage message);

		// Token: 0x060000A5 RID: 165 RVA: 0x00003770 File Offset: 0x00001970
		private static bool OnOnOfflineAnnouncementCompleted(IAsyncResult result)
		{
			ByeOperationAsyncResult<TMessage> byeOperationAsyncResult = (ByeOperationAsyncResult<TMessage>)result.AsyncState;
			byeOperationAsyncResult.announcementServiceImpl.OnEndOfflineAnnouncement(result);
			return true;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003798 File Offset: 0x00001998
		private bool IsInvalid(TMessage message)
		{
			UniqueId messageId = OperationContext.Current.IncomingMessageHeaders.MessageId;
			if (messageId == null)
			{
				if (TD.DiscoveryMessageWithNullMessageIdIsEnabled())
				{
					TD.DiscoveryMessageWithNullMessageId(null, "Bye");
				}
				return true;
			}
			EventTraceActivity eventTraceActivity = null;
			if (Fx.Trace.IsEtwProviderEnabled)
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(OperationContext.Current.IncomingMessage);
			}
			if (this.announcementServiceImpl.IsDuplicate(messageId))
			{
				if (TD.DuplicateDiscoveryMessageIsEnabled())
				{
					TD.DuplicateDiscoveryMessage(eventTraceActivity, "Bye", messageId.ToString());
				}
				return true;
			}
			if (this.ValidateContent(message))
			{
				return false;
			}
			if (TD.DiscoveryMessageWithInvalidContentIsEnabled())
			{
				TD.DiscoveryMessageWithInvalidContent(eventTraceActivity, "Bye", messageId.ToString());
			}
			return true;
		}

		// Token: 0x0400002D RID: 45
		private static AsyncResult.AsyncCompletion onOnOfflineAnnoucementCompletedCallback = new AsyncResult.AsyncCompletion(ByeOperationAsyncResult<TMessage>.OnOnOfflineAnnouncementCompleted);

		// Token: 0x0400002E RID: 46
		private IAnnouncementServiceImplementation announcementServiceImpl;
	}
}
