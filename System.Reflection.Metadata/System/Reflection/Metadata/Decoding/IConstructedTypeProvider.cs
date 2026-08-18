using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x0200014A RID: 330
	internal interface IConstructedTypeProvider<TType> : ISZArrayTypeProvider<TType>
	{
		// Token: 0x06000A74 RID: 2676
		TType GetGenericInstance(TType genericType, ImmutableArray<TType> typeArguments);

		// Token: 0x06000A75 RID: 2677
		TType GetArrayType(TType elementType, ArrayShape shape);

		// Token: 0x06000A76 RID: 2678
		TType GetByReferenceType(TType elementType);

		// Token: 0x06000A77 RID: 2679
		TType GetPointerType(TType elementType);
	}
}
