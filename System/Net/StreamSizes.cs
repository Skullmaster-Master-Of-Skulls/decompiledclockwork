using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000542 RID: 1346
	[StructLayout(LayoutKind.Sequential)]
	internal class StreamSizes
	{
		// Token: 0x0600291F RID: 10527 RVA: 0x000AB8A8 File Offset: 0x000AA8A8
		internal unsafe StreamSizes(byte[] memory)
		{
			checked
			{
				fixed (void* ptr = memory)
				{
					IntPtr ptr2 = new IntPtr(ptr);
					try
					{
						this.header = (int)((uint)Marshal.ReadInt32(ptr2));
						this.trailer = (int)((uint)Marshal.ReadInt32(ptr2, 4));
						this.maximumMessage = (int)((uint)Marshal.ReadInt32(ptr2, 8));
						this.buffersCount = (int)((uint)Marshal.ReadInt32(ptr2, 12));
						this.blockSize = (int)((uint)Marshal.ReadInt32(ptr2, 16));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x040027E4 RID: 10212
		public int header;

		// Token: 0x040027E5 RID: 10213
		public int trailer;

		// Token: 0x040027E6 RID: 10214
		public int maximumMessage;

		// Token: 0x040027E7 RID: 10215
		public int buffersCount;

		// Token: 0x040027E8 RID: 10216
		public int blockSize;

		// Token: 0x040027E9 RID: 10217
		public static readonly int SizeOf = Marshal.SizeOf(typeof(StreamSizes));
	}
}
