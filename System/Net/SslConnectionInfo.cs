using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000546 RID: 1350
	[StructLayout(LayoutKind.Sequential)]
	internal class SslConnectionInfo
	{
		// Token: 0x06002923 RID: 10531 RVA: 0x000AB9F0 File Offset: 0x000AA9F0
		internal unsafe SslConnectionInfo(byte[] nativeBuffer)
		{
			fixed (void* ptr = nativeBuffer)
			{
				IntPtr ptr2 = new IntPtr(ptr);
				this.Protocol = Marshal.ReadInt32(ptr2);
				this.DataCipherAlg = Marshal.ReadInt32(ptr2, 4);
				this.DataKeySize = Marshal.ReadInt32(ptr2, 8);
				this.DataHashAlg = Marshal.ReadInt32(ptr2, 12);
				this.DataHashKeySize = Marshal.ReadInt32(ptr2, 16);
				this.KeyExchangeAlg = Marshal.ReadInt32(ptr2, 20);
				this.KeyExchKeySize = Marshal.ReadInt32(ptr2, 24);
			}
		}

		// Token: 0x0400281E RID: 10270
		public readonly int Protocol;

		// Token: 0x0400281F RID: 10271
		public readonly int DataCipherAlg;

		// Token: 0x04002820 RID: 10272
		public readonly int DataKeySize;

		// Token: 0x04002821 RID: 10273
		public readonly int DataHashAlg;

		// Token: 0x04002822 RID: 10274
		public readonly int DataHashKeySize;

		// Token: 0x04002823 RID: 10275
		public readonly int KeyExchangeAlg;

		// Token: 0x04002824 RID: 10276
		public readonly int KeyExchKeySize;
	}
}
