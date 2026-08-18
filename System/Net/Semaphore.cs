using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000539 RID: 1337
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x060028E8 RID: 10472 RVA: 0x000A9EB4 File Offset: 0x000A8EB4
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				this.Handle = UnsafeNclNativeMethods.CreateSemaphore(IntPtr.Zero, initialCount, maxCount, IntPtr.Zero);
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000A9F00 File Offset: 0x000A8F00
		internal bool ReleaseSemaphore()
		{
			return UnsafeNclNativeMethods.ReleaseSemaphore(this.Handle, 1, IntPtr.Zero);
		}
	}
}
