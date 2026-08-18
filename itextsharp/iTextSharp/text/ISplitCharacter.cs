using System;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020000EC RID: 236
	public interface ISplitCharacter
	{
		// Token: 0x060008DC RID: 2268
		bool IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck);
	}
}
