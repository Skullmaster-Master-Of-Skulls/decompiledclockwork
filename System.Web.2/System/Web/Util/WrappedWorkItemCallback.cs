using System;
using System.Runtime.InteropServices;

namespace System.Web.Util
{
	// Token: 0x02000232 RID: 562
	internal class WrappedWorkItemCallback
	{
		// Token: 0x06001A98 RID: 6808 RVA: 0x000538FB File Offset: 0x00051AFB
		internal WrappedWorkItemCallback(WorkItemCallback callback)
		{
			this._originalCallback = callback;
			this._wrapperCallback = new WorkItemCallback(this.OnCallback);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0005391C File Offset: 0x00051B1C
		internal void Post()
		{
			this._rootedThis = GCHandle.Alloc(this);
			if (UnsafeNativeMethods.PostThreadPoolWorkItem(this._wrapperCallback) != 1)
			{
				this._rootedThis.Free();
				throw new HttpException(SR.GetString("Cannot_post_workitem"));
			}
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00053953 File Offset: 0x00051B53
		private void OnCallback()
		{
			this._rootedThis.Free();
			this._originalCallback();
		}

		// Token: 0x04001848 RID: 6216
		private GCHandle _rootedThis;

		// Token: 0x04001849 RID: 6217
		private WorkItemCallback _originalCallback;

		// Token: 0x0400184A RID: 6218
		private WorkItemCallback _wrapperCallback;
	}
}
