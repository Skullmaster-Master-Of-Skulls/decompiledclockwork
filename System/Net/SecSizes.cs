using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000543 RID: 1347
	[StructLayout(LayoutKind.Sequential)]
	internal class SecSizes
	{
		// Token: 0x06002921 RID: 10529 RVA: 0x000AB954 File Offset: 0x000AA954
		internal unsafe SecSizes(byte[] memory)
		{
			checked
			{
				fixed (void* ptr = memory)
				{
					IntPtr ptr2 = new IntPtr(ptr);
					try
					{
						this.MaxToken = (int)((uint)Marshal.ReadInt32(ptr2));
						this.MaxSignature = (int)((uint)Marshal.ReadInt32(ptr2, 4));
						this.BlockSize = (int)((uint)Marshal.ReadInt32(ptr2, 8));
						this.SecurityTrailer = (int)((uint)Marshal.ReadInt32(ptr2, 12));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x040027EA RID: 10218
		public readonly int MaxToken;

		// Token: 0x040027EB RID: 10219
		public readonly int MaxSignature;

		// Token: 0x040027EC RID: 10220
		public readonly int BlockSize;

		// Token: 0x040027ED RID: 10221
		public readonly int SecurityTrailer;

		// Token: 0x040027EE RID: 10222
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SecSizes));
	}
}
