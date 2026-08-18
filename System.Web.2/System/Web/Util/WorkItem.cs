using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000231 RID: 561
	public class WorkItem
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x00053867 File Offset: 0x00051A67
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Post(WorkItemCallback callback)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
			}
			WorkItem.PostInternal(callback);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0005388C File Offset: 0x00051A8C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static void CallCallbackWithAssert(WorkItemCallback callback)
		{
			callback();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00053894 File Offset: 0x00051A94
		private static void OnQueueUserWorkItemCompletion(object state)
		{
			WorkItemCallback workItemCallback = state as WorkItemCallback;
			if (workItemCallback != null)
			{
				WorkItem.CallCallbackWithAssert(workItemCallback);
			}
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000538B4 File Offset: 0x00051AB4
		internal static void PostInternal(WorkItemCallback callback)
		{
			if (WorkItem._useQueueUserWorkItem)
			{
				ThreadPool.QueueUserWorkItem(WorkItem._onQueueUserWorkItemCompletion, callback);
				return;
			}
			WrappedWorkItemCallback wrappedWorkItemCallback = new WrappedWorkItemCallback(callback);
			wrappedWorkItemCallback.Post();
		}

		// Token: 0x04001846 RID: 6214
		private static bool _useQueueUserWorkItem = true;

		// Token: 0x04001847 RID: 6215
		private static WaitCallback _onQueueUserWorkItemCompletion = new WaitCallback(WorkItem.OnQueueUserWorkItemCompletion);
	}
}
