using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200008A RID: 138
	internal sealed class PinnedObject : CriticalDisposableObject
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00008B30 File Offset: 0x00006D30
		[SecuritySafeCritical]
		public PinnedObject(object obj)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
				this._isValid = 1;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00008B70 File Offset: 0x00006D70
		[SecuritySafeCritical]
		protected override void Release()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._handle.Free();
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				return (byte*)((void*)this._handle.AddrOfPinnedObject());
			}
		}

		// Token: 0x04000499 RID: 1177
		private GCHandle _handle;

		// Token: 0x0400049A RID: 1178
		private int _isValid;
	}
}
