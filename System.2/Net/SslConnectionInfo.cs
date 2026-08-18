using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000213 RID: 531
	[StructLayout(LayoutKind.Sequential)]
	internal class SslConnectionInfo
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x000686A8 File Offset: 0x000668A8
		internal unsafe SslConnectionInfo(byte[] nativeBuffer)
		{
			fixed (byte[] array = nativeBuffer)
			{
				void* value;
				if (nativeBuffer == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = (void*)(&array[0]);
				}
				IntPtr ptr = new IntPtr(value);
				this.Protocol = Marshal.ReadInt32(ptr);
				this.DataCipherAlg = Marshal.ReadInt32(ptr, 4);
				this.DataKeySize = Marshal.ReadInt32(ptr, 8);
				this.DataHashAlg = Marshal.ReadInt32(ptr, 12);
				this.DataHashKeySize = Marshal.ReadInt32(ptr, 16);
				this.KeyExchangeAlg = Marshal.ReadInt32(ptr, 20);
				this.KeyExchKeySize = Marshal.ReadInt32(ptr, 24);
			}
		}

		// Token: 0x040015B3 RID: 5555
		public readonly int Protocol;

		// Token: 0x040015B4 RID: 5556
		public readonly int DataCipherAlg;

		// Token: 0x040015B5 RID: 5557
		public readonly int DataKeySize;

		// Token: 0x040015B6 RID: 5558
		public readonly int DataHashAlg;

		// Token: 0x040015B7 RID: 5559
		public readonly int DataHashKeySize;

		// Token: 0x040015B8 RID: 5560
		public readonly int KeyExchangeAlg;

		// Token: 0x040015B9 RID: 5561
		public readonly int KeyExchKeySize;
	}
}
