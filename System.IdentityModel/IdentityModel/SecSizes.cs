using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Sequential)]
	internal class SecSizes
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x000140C0 File Offset: 0x000122C0
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
				this.MaxToken = Marshal.ReadInt32(ptr);
				this.MaxSignature = Marshal.ReadInt32(ptr, 4);
				this.BlockSize = Marshal.ReadInt32(ptr, 8);
				this.SecurityTrailer = Marshal.ReadInt32(ptr, 12);
			}
		}

		// Token: 0x040004BC RID: 1212
		public int MaxToken;

		// Token: 0x040004BD RID: 1213
		public int MaxSignature;

		// Token: 0x040004BE RID: 1214
		public int BlockSize;

		// Token: 0x040004BF RID: 1215
		public int SecurityTrailer;

		// Token: 0x040004C0 RID: 1216
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SecSizes));
	}
}
