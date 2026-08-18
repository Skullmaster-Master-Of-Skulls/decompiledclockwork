using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000519 RID: 1305
	[ComVisible(false)]
	internal sealed class SafeOverlappedFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600283F RID: 10303 RVA: 0x000A5D69 File Offset: 0x000A4D69
		private SafeOverlappedFree() : base(true)
		{
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000A5D72 File Offset: 0x000A4D72
		private SafeOverlappedFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000A5D7C File Offset: 0x000A4D7C
		public static SafeOverlappedFree Alloc()
		{
			SafeOverlappedFree safeOverlappedFree = UnsafeNclNativeMethods.SafeNetHandlesSafeOverlappedFree.LocalAlloc(64, (UIntPtr)((ulong)((long)Win32.OverlappedSize)));
			if (safeOverlappedFree.IsInvalid)
			{
				safeOverlappedFree.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeOverlappedFree;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000A5DB4 File Offset: 0x000A4DB4
		public static SafeOverlappedFree Alloc(SafeCloseSocket socketHandle)
		{
			SafeOverlappedFree safeOverlappedFree = SafeOverlappedFree.Alloc();
			safeOverlappedFree._socketHandle = socketHandle;
			return safeOverlappedFree;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000A5DD0 File Offset: 0x000A4DD0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close(bool resetOwner)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (resetOwner)
				{
					this._socketHandle = null;
				}
				base.Close();
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000A5E08 File Offset: 0x000A4E08
		protected override bool ReleaseHandle()
		{
			SafeCloseSocket socketHandle = this._socketHandle;
			if (socketHandle != null && !socketHandle.IsInvalid)
			{
				socketHandle.Dispose();
			}
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04002779 RID: 10105
		private const int LPTR = 64;

		// Token: 0x0400277A RID: 10106
		internal static readonly SafeOverlappedFree Zero = new SafeOverlappedFree(false);

		// Token: 0x0400277B RID: 10107
		private SafeCloseSocket _socketHandle;
	}
}
