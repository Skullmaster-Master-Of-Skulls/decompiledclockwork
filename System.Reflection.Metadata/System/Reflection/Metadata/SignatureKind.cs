using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AC RID: 172
	public enum SignatureKind : byte
	{
		// Token: 0x04000446 RID: 1094
		Method,
		// Token: 0x04000447 RID: 1095
		Field = 6,
		// Token: 0x04000448 RID: 1096
		LocalVariables,
		// Token: 0x04000449 RID: 1097
		Property,
		// Token: 0x0400044A RID: 1098
		MethodSpecification = 10
	}
}
