using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A7A RID: 2682
	internal static class EventTraceActivityHelper
	{
		// Token: 0x060069E2 RID: 27106 RVA: 0x0018A610 File Offset: 0x00188810
		public static bool TryAttachActivity(Message message, EventTraceActivity activity)
		{
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled && message != null && activity != null && !message.Properties.ContainsKey(EventTraceActivity.Name))
			{
				message.Properties.Add(EventTraceActivity.Name, activity);
				return true;
			}
			return false;
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x0018A64A File Offset: 0x0018884A
		public static EventTraceActivity TryExtractActivity(Message message)
		{
			return EventTraceActivityHelper.TryExtractActivity(message, false);
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x0018A654 File Offset: 0x00188854
		public static EventTraceActivity TryExtractActivity(Message message, bool createIfNotExist)
		{
			EventTraceActivity eventTraceActivity = null;
			if (message != null && message.State != MessageState.Closed)
			{
				object obj;
				if (message.Properties.TryGetValue(EventTraceActivity.Name, out obj))
				{
					eventTraceActivity = (obj as EventTraceActivity);
				}
				if (eventTraceActivity == null)
				{
					Guid guid;
					if (EventTraceActivityHelper.GetMessageId(message, out guid))
					{
						eventTraceActivity = new EventTraceActivity(guid, false);
					}
					else
					{
						UniqueId relatesTo = message.Headers.RelatesTo;
						if (relatesTo != null && relatesTo.TryGetGuid(out guid))
						{
							eventTraceActivity = new EventTraceActivity(guid, false);
						}
					}
					if (eventTraceActivity == null && createIfNotExist)
					{
						eventTraceActivity = new EventTraceActivity(false);
					}
					if (eventTraceActivity != null)
					{
						message.Properties[EventTraceActivity.Name] = eventTraceActivity;
					}
				}
			}
			return eventTraceActivity;
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x0018A6EF File Offset: 0x001888EF
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void SetOnThread(EventTraceActivity eventTraceActivity)
		{
			if (eventTraceActivity != null)
			{
				Trace.CorrelationManager.ActivityId = eventTraceActivity.ActivityId;
			}
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x0018A704 File Offset: 0x00188904
		private static bool GetMessageId(Message message, out Guid guid)
		{
			UniqueId messageId = message.Headers.MessageId;
			if (messageId == null)
			{
				guid = Guid.Empty;
				return false;
			}
			return messageId.TryGetGuid(out guid);
		}
	}
}
