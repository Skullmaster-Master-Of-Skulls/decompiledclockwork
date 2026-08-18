using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200005A RID: 90
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct UNICODE_INTPTR_STRING
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000BD4A File Offset: 0x00009F4A
		internal UNICODE_INTPTR_STRING(int length, int maximumLength, IntPtr buffer)
		{
			this.Length = (ushort)length;
			this.MaxLength = (ushort)maximumLength;
			this.Buffer = buffer;
		}

		// Token: 0x040002F4 RID: 756
		internal ushort Length;

		// Token: 0x040002F5 RID: 757
		internal ushort MaxLength;

		// Token: 0x040002F6 RID: 758
		internal IntPtr Buffer;
	}
}
