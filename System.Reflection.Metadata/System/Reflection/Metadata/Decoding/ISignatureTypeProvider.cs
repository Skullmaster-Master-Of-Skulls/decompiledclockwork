using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000146 RID: 326
	internal interface ISignatureTypeProvider<TType> : IPrimitiveTypeProvider<TType>, ITypeProvider<TType>, IConstructedTypeProvider<TType>, ISZArrayTypeProvider<TType>
	{
		// Token: 0x06000A6B RID: 2667
		TType GetFunctionPointerType(MethodSignature<TType> signature);

		// Token: 0x06000A6C RID: 2668
		TType GetGenericMethodParameter(int index);

		// Token: 0x06000A6D RID: 2669
		TType GetGenericTypeParameter(int index);

		// Token: 0x06000A6E RID: 2670
		TType GetModifiedType(MetadataReader reader, bool isRequired, TType modifier, TType unmodifiedType);

		// Token: 0x06000A6F RID: 2671
		TType GetPinnedType(TType elementType);
	}
}
