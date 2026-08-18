using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A8 RID: 168
	public enum SerializationTypeCode : byte
	{
		// Token: 0x04000424 RID: 1060
		Invalid,
		// Token: 0x04000425 RID: 1061
		Boolean = 2,
		// Token: 0x04000426 RID: 1062
		Char,
		// Token: 0x04000427 RID: 1063
		SByte,
		// Token: 0x04000428 RID: 1064
		Byte,
		// Token: 0x04000429 RID: 1065
		Int16,
		// Token: 0x0400042A RID: 1066
		UInt16,
		// Token: 0x0400042B RID: 1067
		Int32,
		// Token: 0x0400042C RID: 1068
		UInt32,
		// Token: 0x0400042D RID: 1069
		Int64,
		// Token: 0x0400042E RID: 1070
		UInt64,
		// Token: 0x0400042F RID: 1071
		Single,
		// Token: 0x04000430 RID: 1072
		Double,
		// Token: 0x04000431 RID: 1073
		String,
		// Token: 0x04000432 RID: 1074
		SZArray = 29,
		// Token: 0x04000433 RID: 1075
		Type = 80,
		// Token: 0x04000434 RID: 1076
		TaggedObject,
		// Token: 0x04000435 RID: 1077
		Enum = 85
	}
}
