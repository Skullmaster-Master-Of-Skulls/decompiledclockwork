using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000683 RID: 1667
	internal sealed class SafeLsaMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C4B RID: 15435 RVA: 0x000CE404 File Offset: 0x000CD404
		private SafeLsaMemoryHandle() : base(true)
		{
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000CE40D File Offset: 0x000CD40D
		internal SafeLsaMemoryHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000CE41D File Offset: 0x000CD41D
		internal static SafeLsaMemoryHandle InvalidHandle
		{
			get
			{
				return new SafeLsaMemoryHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000CE429 File Offset: 0x000CD429
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaFreeMemory(this.handle) == 0;
		}
	}
}
