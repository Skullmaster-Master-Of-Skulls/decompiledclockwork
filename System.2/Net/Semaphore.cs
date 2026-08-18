using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200020A RID: 522
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x00066920 File Offset: 0x00064B20
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				this.Handle = UnsafeNclNativeMethods.CreateSemaphore(IntPtr.Zero, initialCount, maxCount, IntPtr.Zero);
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00066974 File Offset: 0x00064B74
		internal bool ReleaseSemaphore()
		{
			return UnsafeNclNativeMethods.ReleaseSemaphore(this.Handle, 1, IntPtr.Zero);
		}
	}
}
