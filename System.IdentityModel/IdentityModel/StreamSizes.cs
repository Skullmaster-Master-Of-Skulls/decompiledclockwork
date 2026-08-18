using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A7 RID: 167
	[StructLayout(LayoutKind.Sequential)]
	internal class StreamSizes
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0001348C File Offset: 0x0001168C
		internal unsafe StreamSizes(byte[] memory)
		{
			fixed (byte[] array = memory)
			{
				void* value;
				if (memory == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = (void*)(&array[0]);
				}
				IntPtr ptr = new IntPtr(value);
				this.header = Marshal.ReadInt32(ptr);
				this.trailer = Marshal.ReadInt32(ptr, 4);
				this.maximumMessage = Marshal.ReadInt32(ptr, 8);
				this.buffersCount = Marshal.ReadInt32(ptr, 12);
				this.blockSize = Marshal.ReadInt32(ptr, 16);
			}
		}

		// Token: 0x040004AD RID: 1197
		public int header;

		// Token: 0x040004AE RID: 1198
		public int trailer;

		// Token: 0x040004AF RID: 1199
		public int maximumMessage;

		// Token: 0x040004B0 RID: 1200
		public int buffersCount;

		// Token: 0x040004B1 RID: 1201
		public int blockSize;

		// Token: 0x040004B2 RID: 1202
		public static readonly int SizeOf = Marshal.SizeOf(typeof(StreamSizes));
	}
}
