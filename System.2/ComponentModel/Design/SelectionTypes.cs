using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005FD RID: 1533
	[Flags]
	[ComVisible(true)]
	public enum SelectionTypes
	{
		// Token: 0x04002B1E RID: 11038
		Auto = 1,
		// Token: 0x04002B1F RID: 11039
		[Obsolete("This value has been deprecated. Use SelectionTypes.Auto instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Normal = 1,
		// Token: 0x04002B20 RID: 11040
		Replace = 2,
		// Token: 0x04002B21 RID: 11041
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseDown = 4,
		// Token: 0x04002B22 RID: 11042
		[Obsolete("This value has been deprecated.  It is no longer supported. http://go.microsoft.com/fwlink/?linkid=14202")]
		MouseUp = 8,
		// Token: 0x04002B23 RID: 11043
		[Obsolete("This value has been deprecated. Use SelectionTypes.Primary instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Click = 16,
		// Token: 0x04002B24 RID: 11044
		Primary = 16,
		// Token: 0x04002B25 RID: 11045
		Toggle = 32,
		// Token: 0x04002B26 RID: 11046
		Add = 64,
		// Token: 0x04002B27 RID: 11047
		Remove = 128,
		// Token: 0x04002B28 RID: 11048
		[Obsolete("This value has been deprecated. Use Enum class methods to determine valid values, or use a type converter. http://go.microsoft.com/fwlink/?linkid=14202")]
		Valid = 31
	}
}
