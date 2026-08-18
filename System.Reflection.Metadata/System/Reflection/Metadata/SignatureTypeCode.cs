using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AD RID: 173
	public enum SignatureTypeCode : byte
	{
		// Token: 0x0400044C RID: 1100
		Invalid,
		// Token: 0x0400044D RID: 1101
		Void,
		// Token: 0x0400044E RID: 1102
		Boolean,
		// Token: 0x0400044F RID: 1103
		Char,
		// Token: 0x04000450 RID: 1104
		SByte,
		// Token: 0x04000451 RID: 1105
		Byte,
		// Token: 0x04000452 RID: 1106
		Int16,
		// Token: 0x04000453 RID: 1107
		UInt16,
		// Token: 0x04000454 RID: 1108
		Int32,
		// Token: 0x04000455 RID: 1109
		UInt32,
		// Token: 0x04000456 RID: 1110
		Int64,
		// Token: 0x04000457 RID: 1111
		UInt64,
		// Token: 0x04000458 RID: 1112
		Single,
		// Token: 0x04000459 RID: 1113
		Double,
		// Token: 0x0400045A RID: 1114
		String,
		// Token: 0x0400045B RID: 1115
		Pointer,
		// Token: 0x0400045C RID: 1116
		ByReference,
		// Token: 0x0400045D RID: 1117
		GenericTypeParameter = 19,
		// Token: 0x0400045E RID: 1118
		Array,
		// Token: 0x0400045F RID: 1119
		GenericTypeInstance,
		// Token: 0x04000460 RID: 1120
		TypedReference,
		// Token: 0x04000461 RID: 1121
		IntPtr = 24,
		// Token: 0x04000462 RID: 1122
		UIntPtr,
		// Token: 0x04000463 RID: 1123
		FunctionPointer = 27,
		// Token: 0x04000464 RID: 1124
		Object,
		// Token: 0x04000465 RID: 1125
		SZArray,
		// Token: 0x04000466 RID: 1126
		GenericMethodParameter,
		// Token: 0x04000467 RID: 1127
		RequiredModifier,
		// Token: 0x04000468 RID: 1128
		OptionalModifier,
		// Token: 0x04000469 RID: 1129
		TypeHandle = 64,
		// Token: 0x0400046A RID: 1130
		Sentinel,
		// Token: 0x0400046B RID: 1131
		Pinned = 69
	}
}
