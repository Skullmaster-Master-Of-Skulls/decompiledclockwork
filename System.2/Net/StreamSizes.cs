using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200020F RID: 527
	[StructLayout(LayoutKind.Sequential)]
	internal class StreamSizes
	{
		// Token: 0x060013C1 RID: 5057 RVA: 0x00068560 File Offset: 0x00066760
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
				checked
				{
					try
					{
						this.header = (int)((uint)Marshal.ReadInt32(ptr));
						this.trailer = (int)((uint)Marshal.ReadInt32(ptr, 4));
						this.maximumMessage = (int)((uint)Marshal.ReadInt32(ptr, 8));
						this.buffersCount = (int)((uint)Marshal.ReadInt32(ptr, 12));
						this.blockSize = (int)((uint)Marshal.ReadInt32(ptr, 16));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04001573 RID: 5491
		public int header;

		// Token: 0x04001574 RID: 5492
		public int trailer;

		// Token: 0x04001575 RID: 5493
		public int maximumMessage;

		// Token: 0x04001576 RID: 5494
		public int buffersCount;

		// Token: 0x04001577 RID: 5495
		public int blockSize;

		// Token: 0x04001578 RID: 5496
		public static readonly int SizeOf = Marshal.SizeOf(typeof(StreamSizes));
	}
}
