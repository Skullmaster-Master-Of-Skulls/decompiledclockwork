using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000145 RID: 325
	internal interface ISZArrayTypeProvider<TType>
	{
		// Token: 0x06000A6A RID: 2666
		TType GetSZArrayType(TType elementType);
	}
}
