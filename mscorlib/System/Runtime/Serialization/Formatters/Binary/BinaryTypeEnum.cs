using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007BF RID: 1983
	[Serializable]
	internal enum BinaryTypeEnum
	{
		// Token: 0x04002336 RID: 9014
		Primitive,
		// Token: 0x04002337 RID: 9015
		String,
		// Token: 0x04002338 RID: 9016
		Object,
		// Token: 0x04002339 RID: 9017
		ObjectUrt,
		// Token: 0x0400233A RID: 9018
		ObjectUser,
		// Token: 0x0400233B RID: 9019
		ObjectArray,
		// Token: 0x0400233C RID: 9020
		StringArray,
		// Token: 0x0400233D RID: 9021
		PrimitiveArray
	}
}
