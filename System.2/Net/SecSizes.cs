using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000210 RID: 528
	[StructLayout(LayoutKind.Sequential)]
	internal class SecSizes
	{
		// Token: 0x060013C3 RID: 5059 RVA: 0x0006860C File Offset: 0x0006680C
		internal unsafe SecSizes(byte[] memory)
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
						this.MaxToken = (int)((uint)Marshal.ReadInt32(ptr));
						this.MaxSignature = (int)((uint)Marshal.ReadInt32(ptr, 4));
						this.BlockSize = (int)((uint)Marshal.ReadInt32(ptr, 8));
						this.SecurityTrailer = (int)((uint)Marshal.ReadInt32(ptr, 12));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04001579 RID: 5497
		public readonly int MaxToken;

		// Token: 0x0400157A RID: 5498
		public readonly int MaxSignature;

		// Token: 0x0400157B RID: 5499
		public readonly int BlockSize;

		// Token: 0x0400157C RID: 5500
		public readonly int SecurityTrailer;

		// Token: 0x0400157D RID: 5501
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SecSizes));
	}
}
