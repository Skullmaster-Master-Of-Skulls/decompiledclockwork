using System;

namespace Antlr.Runtime
{
	// Token: 0x02000003 RID: 3
	public interface ICharStream : IIntStream
	{
		// Token: 0x0600000B RID: 11
		string Substring(int start, int length);

		// Token: 0x0600000C RID: 12
		int LT(int i);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13
		// (set) Token: 0x0600000E RID: 14
		int Line { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x06000010 RID: 16
		int CharPositionInLine { get; set; }
	}
}
