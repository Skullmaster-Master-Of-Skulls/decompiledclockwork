using System;
using System.Runtime.InteropServices;

namespace EmailClassLibrary
{
	// Token: 0x0200000B RID: 11
	[StructLayout(LayoutKind.Sequential)]
	public class MapiRecipDesc
	{
		// Token: 0x04000031 RID: 49
		public int reserved;

		// Token: 0x04000032 RID: 50
		public int recipClass;

		// Token: 0x04000033 RID: 51
		public string name;

		// Token: 0x04000034 RID: 52
		public string address;

		// Token: 0x04000035 RID: 53
		public int eIDSize;

		// Token: 0x04000036 RID: 54
		public IntPtr entryID;
	}
}
