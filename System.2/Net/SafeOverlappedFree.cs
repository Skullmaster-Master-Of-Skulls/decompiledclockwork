using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F3 RID: 499
	[ComVisible(false)]
	internal sealed class SafeOverlappedFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x00064471 File Offset: 0x00062671
		private SafeOverlappedFree() : base(true)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0006447A File Offset: 0x0006267A
		private SafeOverlappedFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00064484 File Offset: 0x00062684
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

		// Token: 0x06001310 RID: 4880 RVA: 0x000644BC File Offset: 0x000626BC
		public static SafeOverlappedFree Alloc(SafeCloseSocket socketHandle)
		{
			SafeOverlappedFree safeOverlappedFree = SafeOverlappedFree.Alloc();
			safeOverlappedFree._socketHandle = socketHandle;
			return safeOverlappedFree;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000644D8 File Offset: 0x000626D8
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

		// Token: 0x06001312 RID: 4882 RVA: 0x00064510 File Offset: 0x00062710
		protected override bool ReleaseHandle()
		{
			SafeCloseSocket socketHandle = this._socketHandle;
			if (socketHandle != null && !socketHandle.IsInvalid)
			{
				socketHandle.Dispose();
			}
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04001541 RID: 5441
		private const int LPTR = 64;

		// Token: 0x04001542 RID: 5442
		internal static readonly SafeOverlappedFree Zero = new SafeOverlappedFree(false);

		// Token: 0x04001543 RID: 5443
		private SafeCloseSocket _socketHandle;
	}
}
