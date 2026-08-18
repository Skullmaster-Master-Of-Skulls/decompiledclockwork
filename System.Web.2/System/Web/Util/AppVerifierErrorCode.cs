using System;

namespace System.Web.Util
{
	// Token: 0x020001C2 RID: 450
	internal enum AppVerifierErrorCode
	{
		// Token: 0x040016CF RID: 5839
		Ok,
		// Token: 0x040016D0 RID: 5840
		HttpApplicationInstanceWasNull,
		// Token: 0x040016D1 RID: 5841
		BeginHandlerDelegateWasNull,
		// Token: 0x040016D2 RID: 5842
		AsyncCallbackInvokedMultipleTimes,
		// Token: 0x040016D3 RID: 5843
		AsyncCallbackInvokedWithNullParameter,
		// Token: 0x040016D4 RID: 5844
		AsyncCallbackGivenAsyncResultWhichWasNotCompleted,
		// Token: 0x040016D5 RID: 5845
		AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously,
		// Token: 0x040016D6 RID: 5846
		AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously,
		// Token: 0x040016D7 RID: 5847
		AsyncCallbackInvokedWithUnexpectedAsyncResultInstance,
		// Token: 0x040016D8 RID: 5848
		AsyncCallbackInvokedAsynchronouslyThenBeginHandlerThrew,
		// Token: 0x040016D9 RID: 5849
		BeginHandlerThrewThenAsyncCallbackInvokedAsynchronously,
		// Token: 0x040016DA RID: 5850
		AsyncCallbackInvokedSynchronouslyThenBeginHandlerThrew,
		// Token: 0x040016DB RID: 5851
		AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState,
		// Token: 0x040016DC RID: 5852
		AsyncCallbackCalledAfterHttpApplicationReassigned,
		// Token: 0x040016DD RID: 5853
		BeginHandlerReturnedNull,
		// Token: 0x040016DE RID: 5854
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted,
		// Token: 0x040016DF RID: 5855
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled,
		// Token: 0x040016E0 RID: 5856
		BeginHandlerReturnedUnexpectedAsyncResultInstance,
		// Token: 0x040016E1 RID: 5857
		BeginHandlerReturnedUnexpectedAsyncResultAsyncState,
		// Token: 0x040016E2 RID: 5858
		SyncContextSendOrPostCalledAfterRequestCompleted,
		// Token: 0x040016E3 RID: 5859
		SyncContextSendOrPostCalledBetweenNotifications,
		// Token: 0x040016E4 RID: 5860
		SyncContextPostCalledInNestedNotification,
		// Token: 0x040016E5 RID: 5861
		RequestNotificationCompletedSynchronouslyWithNotificationContextPending,
		// Token: 0x040016E6 RID: 5862
		NotificationContextHasChangedAfterSynchronouslyProcessingNotification,
		// Token: 0x040016E7 RID: 5863
		PendingProcessRequestNotificationStatusAfterCompletingNestedNotification
	}
}
