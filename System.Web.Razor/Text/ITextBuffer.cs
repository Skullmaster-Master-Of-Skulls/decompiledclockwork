using System;

namespace System.Web.Razor.Text
{
	// Token: 0x02000062 RID: 98
	public interface ITextBuffer
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600048D RID: 1165
		int Length { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600048E RID: 1166
		// (set) Token: 0x0600048F RID: 1167
		int Position { get; set; }

		// Token: 0x06000490 RID: 1168
		int Read();

		// Token: 0x06000491 RID: 1169
		int Peek();
	}
}
