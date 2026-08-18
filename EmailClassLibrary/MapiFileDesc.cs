using System;
using System.Runtime.InteropServices;

namespace EmailClassLibrary
{
	// Token: 0x0200000A RID: 10
	[StructLayout(LayoutKind.Sequential)]
	public class MapiFileDesc
	{
		// Token: 0x0400002B RID: 43
		public int reserved;

		// Token: 0x0400002C RID: 44
		public int flags;

		// Token: 0x0400002D RID: 45
		public int position;

		// Token: 0x0400002E RID: 46
		public string path;

		// Token: 0x0400002F RID: 47
		public string name;

		// Token: 0x04000030 RID: 48
		public IntPtr type;
	}
}
