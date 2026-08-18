using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Sequential)]
	internal class SslConnectionInfo
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0001402C File Offset: 0x0001222C
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

		// Token: 0x040004B5 RID: 1205
		public readonly int Protocol;

		// Token: 0x040004B6 RID: 1206
		public readonly int DataCipherAlg;

		// Token: 0x040004B7 RID: 1207
		public readonly int DataKeySize;

		// Token: 0x040004B8 RID: 1208
		public readonly int DataHashAlg;

		// Token: 0x040004B9 RID: 1209
		public readonly int DataHashKeySize;

		// Token: 0x040004BA RID: 1210
		public readonly int KeyExchangeAlg;

		// Token: 0x040004BB RID: 1211
		public readonly int KeyExchKeySize;
	}
}
