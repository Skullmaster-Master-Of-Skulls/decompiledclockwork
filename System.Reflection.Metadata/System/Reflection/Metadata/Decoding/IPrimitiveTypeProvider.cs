using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000149 RID: 329
	internal interface IPrimitiveTypeProvider<TType>
	{
		// Token: 0x06000A73 RID: 2675
		TType GetPrimitiveType(PrimitiveTypeCode typeCode);
	}
}
