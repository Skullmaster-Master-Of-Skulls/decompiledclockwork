using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BB RID: 187
	public enum OperatorPrecedence
	{
		// Token: 0x040004DD RID: 1245
		None,
		// Token: 0x040004DE RID: 1246
		Comma,
		// Token: 0x040004DF RID: 1247
		Assignment,
		// Token: 0x040004E0 RID: 1248
		Conditional,
		// Token: 0x040004E1 RID: 1249
		LogicalOr,
		// Token: 0x040004E2 RID: 1250
		LogicalAnd,
		// Token: 0x040004E3 RID: 1251
		BitwiseOr,
		// Token: 0x040004E4 RID: 1252
		BitwiseXor,
		// Token: 0x040004E5 RID: 1253
		BitwiseAnd,
		// Token: 0x040004E6 RID: 1254
		Equality,
		// Token: 0x040004E7 RID: 1255
		Relational,
		// Token: 0x040004E8 RID: 1256
		Shift,
		// Token: 0x040004E9 RID: 1257
		Additive,
		// Token: 0x040004EA RID: 1258
		Multiplicative,
		// Token: 0x040004EB RID: 1259
		Unary,
		// Token: 0x040004EC RID: 1260
		FieldAccess,
		// Token: 0x040004ED RID: 1261
		Primary,
		// Token: 0x040004EE RID: 1262
		Highest
	}
}
