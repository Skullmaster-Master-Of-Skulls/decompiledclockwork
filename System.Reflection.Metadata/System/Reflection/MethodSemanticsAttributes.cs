using System;

namespace System.Reflection
{
	// Token: 0x0200000A RID: 10
	[Flags]
	public enum MethodSemanticsAttributes
	{
		// Token: 0x04000016 RID: 22
		Setter = 1,
		// Token: 0x04000017 RID: 23
		Getter = 2,
		// Token: 0x04000018 RID: 24
		Other = 4,
		// Token: 0x04000019 RID: 25
		Adder = 8,
		// Token: 0x0400001A RID: 26
		Remover = 16,
		// Token: 0x0400001B RID: 27
		Raiser = 32
	}
}
