using System;
using System.Runtime.InteropServices;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000615 RID: 1557
	public sealed class Memory
	{
		// Token: 0x06005D4D RID: 23885
		[DllImport("kernel32.dll")]
		public static extern void RtlMoveMemory(IntPtr ptrDest, IntPtr ptrSource, int iSize);

		// Token: 0x06005D4E RID: 23886
		[DllImport("kernel32.dll")]
		public static extern void RtlZeroMemory(IntPtr ptrDest, int iSize);

		// Token: 0x06005D4F RID: 23887
		[DllImport("kernel32.dll")]
		public static extern void CopyMemory(IntPtr ptrDest, IntPtr ptrSource, int iSize);

		// Token: 0x06005D50 RID: 23888
		[CLSCompliant(false)]
		[DllImport("kernel32.dll")]
		public unsafe static extern void CopyMemory(byte* ptrDest, byte* ptrSource, int iSize);
	}
}
