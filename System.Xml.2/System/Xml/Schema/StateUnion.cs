using System;
using System.Runtime.InteropServices;

namespace System.Xml.Schema
{
	// Token: 0x02000265 RID: 613
	[StructLayout(LayoutKind.Explicit)]
	internal struct StateUnion
	{
		// Token: 0x04000FE4 RID: 4068
		[FieldOffset(0)]
		public int State;

		// Token: 0x04000FE5 RID: 4069
		[FieldOffset(0)]
		public int AllElementsRequired;

		// Token: 0x04000FE6 RID: 4070
		[FieldOffset(0)]
		public int CurPosIndex;

		// Token: 0x04000FE7 RID: 4071
		[FieldOffset(0)]
		public int NumberOfRunningPos;
	}
}
