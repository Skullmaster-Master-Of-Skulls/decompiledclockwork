using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000DE RID: 222
	[DataContract(Namespace = "http://tpro.ca")]
	public enum CWDbType
	{
		// Token: 0x04000068 RID: 104
		[EnumMember]
		AnsiString,
		// Token: 0x04000069 RID: 105
		[EnumMember]
		AnsiStringFixedLength = 22,
		// Token: 0x0400006A RID: 106
		[EnumMember]
		Binary = 1,
		// Token: 0x0400006B RID: 107
		[EnumMember]
		Boolean = 3,
		// Token: 0x0400006C RID: 108
		[EnumMember]
		Byte = 2,
		// Token: 0x0400006D RID: 109
		[EnumMember]
		Currency = 4,
		// Token: 0x0400006E RID: 110
		[EnumMember]
		Date,
		// Token: 0x0400006F RID: 111
		[EnumMember]
		DateTime,
		// Token: 0x04000070 RID: 112
		[EnumMember]
		DateTime2 = 26,
		// Token: 0x04000071 RID: 113
		[EnumMember]
		DateTimeOffset,
		// Token: 0x04000072 RID: 114
		[EnumMember]
		Decimal = 7,
		// Token: 0x04000073 RID: 115
		[EnumMember]
		Double,
		// Token: 0x04000074 RID: 116
		[EnumMember]
		Guid,
		// Token: 0x04000075 RID: 117
		[EnumMember]
		Int16,
		// Token: 0x04000076 RID: 118
		[EnumMember]
		Int32,
		// Token: 0x04000077 RID: 119
		[EnumMember]
		Int64,
		// Token: 0x04000078 RID: 120
		[EnumMember]
		Object,
		// Token: 0x04000079 RID: 121
		[EnumMember]
		SByte,
		// Token: 0x0400007A RID: 122
		[EnumMember]
		Single,
		// Token: 0x0400007B RID: 123
		[EnumMember]
		String,
		// Token: 0x0400007C RID: 124
		[EnumMember]
		StringFixedLength = 23,
		// Token: 0x0400007D RID: 125
		[EnumMember]
		Time = 17,
		// Token: 0x0400007E RID: 126
		[EnumMember]
		UInt16,
		// Token: 0x0400007F RID: 127
		[EnumMember]
		UInt32,
		// Token: 0x04000080 RID: 128
		[EnumMember]
		UInt64,
		// Token: 0x04000081 RID: 129
		[EnumMember]
		VarNumeric,
		// Token: 0x04000082 RID: 130
		[EnumMember]
		Xml = 25
	}
}
