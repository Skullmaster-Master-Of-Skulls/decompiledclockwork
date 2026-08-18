using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200079F RID: 1951
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WorkItem
	{
		// Token: 0x06005D79 RID: 23929 RVA: 0x00176617 File Offset: 0x00175617
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Post(WorkItemCallback callback)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
			}
			WorkItem.PostInternal(callback);
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x0017663C File Offset: 0x0017563C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static void CallCallbackWithAssert(WorkItemCallback callback)
		{
			callback();
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x00176644 File Offset: 0x00175644
		private static void OnQueueUserWorkItemCompletion(object state)
		{
			WorkItemCallback workItemCallback = state as WorkItemCallback;
			if (workItemCallback != null)
			{
				WorkItem.CallCallbackWithAssert(workItemCallback);
			}
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x00176664 File Offset: 0x00175664
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

		// Token: 0x040031DC RID: 12764
		private static bool _useQueueUserWorkItem = true;

		// Token: 0x040031DD RID: 12765
		private static WaitCallback _onQueueUserWorkItemCompletion = new WaitCallback(WorkItem.OnQueueUserWorkItemCompletion);
	}
}
