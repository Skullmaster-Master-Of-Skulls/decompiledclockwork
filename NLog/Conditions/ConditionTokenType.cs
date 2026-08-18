using System;

namespace NLog.Conditions
{
	// Token: 0x0200003E RID: 62
	internal enum ConditionTokenType
	{
		// Token: 0x04000052 RID: 82
		EndOfInput,
		// Token: 0x04000053 RID: 83
		BeginningOfInput,
		// Token: 0x04000054 RID: 84
		Number,
		// Token: 0x04000055 RID: 85
		String,
		// Token: 0x04000056 RID: 86
		Keyword,
		// Token: 0x04000057 RID: 87
		Whitespace,
		// Token: 0x04000058 RID: 88
		FirstPunct,
		// Token: 0x04000059 RID: 89
		LessThan,
		// Token: 0x0400005A RID: 90
		GreaterThan,
		// Token: 0x0400005B RID: 91
		LessThanOrEqualTo,
		// Token: 0x0400005C RID: 92
		GreaterThanOrEqualTo,
		// Token: 0x0400005D RID: 93
		EqualTo,
		// Token: 0x0400005E RID: 94
		NotEqual,
		// Token: 0x0400005F RID: 95
		LeftParen,
		// Token: 0x04000060 RID: 96
		RightParen,
		// Token: 0x04000061 RID: 97
		Dot,
		// Token: 0x04000062 RID: 98
		Comma,
		// Token: 0x04000063 RID: 99
		Not,
		// Token: 0x04000064 RID: 100
		And,
		// Token: 0x04000065 RID: 101
		Or,
		// Token: 0x04000066 RID: 102
		Minus,
		// Token: 0x04000067 RID: 103
		LastPunct,
		// Token: 0x04000068 RID: 104
		Invalid,
		// Token: 0x04000069 RID: 105
		ClosingCurlyBrace,
		// Token: 0x0400006A RID: 106
		Colon,
		// Token: 0x0400006B RID: 107
		Exclamation,
		// Token: 0x0400006C RID: 108
		Ampersand,
		// Token: 0x0400006D RID: 109
		Pipe
	}
}
