using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000143 RID: 323
	internal interface ICustomAttributeTypeProvider<TType> : IPrimitiveTypeProvider<TType>, ISZArrayTypeProvider<TType>, ITypeProvider<TType>
	{
		// Token: 0x06000A62 RID: 2658
		TType GetSystemType();

		// Token: 0x06000A63 RID: 2659
		bool IsSystemType(TType type);

		// Token: 0x06000A64 RID: 2660
		TType GetTypeFromSerializedName(string name);

		// Token: 0x06000A65 RID: 2661
		PrimitiveTypeCode GetUnderlyingEnumType(TType type);
	}
}
