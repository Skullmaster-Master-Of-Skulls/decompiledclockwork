using System;
using System.Runtime.InteropServices;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000614 RID: 1556
	public sealed class Heap
	{
		// Token: 0x06005D47 RID: 23879
		[DllImport("kernel32")]
		public static extern IntPtr HeapAlloc(IntPtr hHeap, int dwFlags, int dwBytes);

		// Token: 0x06005D48 RID: 23880
		[DllImport("kernel32")]
		public static extern IntPtr HeapCreate(int flOptions, int dwInitialSize, int dwMaximumSize);

		// Token: 0x06005D49 RID: 23881
		[DllImport("kernel32")]
		public static extern int HeapDestroy(IntPtr hHeap);

		// Token: 0x06005D4A RID: 23882
		[DllImport("kernel32")]
		public static extern int HeapFree(IntPtr hHeap, int dwFlags, IntPtr lpMem);

		// Token: 0x06005D4B RID: 23883
		[DllImport("kernel32")]
		public static extern IntPtr HeapReAlloc(IntPtr hHeap, int dwFlags, IntPtr lpMem, int dwBytes);
	}
}
