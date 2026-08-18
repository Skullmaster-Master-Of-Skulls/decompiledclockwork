using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	// Token: 0x0200004F RID: 79
	internal struct DATA_BLOB : IDisposable
	{
		// Token: 0x0600033A RID: 826 RVA: 0x000129F7 File Offset: 0x00010BF7
		void IDisposable.Dispose()
		{
			if (this.pbData != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.pbData);
				this.pbData = IntPtr.Zero;
			}
		}

		// Token: 0x0400024A RID: 586
		public int cbData;

		// Token: 0x0400024B RID: 587
		public IntPtr pbData;
	}
}
