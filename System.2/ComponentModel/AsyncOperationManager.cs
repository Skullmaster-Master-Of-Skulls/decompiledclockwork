using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000514 RID: 1300
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public static class AsyncOperationManager
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x000DF10A File Offset: 0x000DD30A
		[__DynamicallyInvokable]
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000DF117 File Offset: 0x000DD317
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x000DF12F File Offset: 0x000DD32F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[__DynamicallyInvokable]
		public static SynchronizationContext SynchronizationContext
		{
			[__DynamicallyInvokable]
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			[__DynamicallyInvokable]
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
}
