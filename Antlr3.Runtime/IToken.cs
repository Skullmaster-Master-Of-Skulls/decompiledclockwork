using System;

namespace Antlr.Runtime
{
	// Token: 0x02000015 RID: 21
	public interface IToken
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CD RID: 205
		string Text { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CE RID: 206
		// (set) Token: 0x060000CF RID: 207
		int Type { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D0 RID: 208
		// (set) Token: 0x060000D1 RID: 209
		int Line { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D2 RID: 210
		// (set) Token: 0x060000D3 RID: 211
		int CharPositionInLine { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		int Channel { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D7 RID: 215
		int StartIndex { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D9 RID: 217
		int StopIndex { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DA RID: 218
		// (set) Token: 0x060000DB RID: 219
		int TokenIndex { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DC RID: 220
		// (set) Token: 0x060000DD RID: 221
		ICharStream InputStream { get; set; }
	}
}
