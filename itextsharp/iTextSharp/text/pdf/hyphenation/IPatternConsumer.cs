using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x020000E4 RID: 228
	public interface IPatternConsumer
	{
		// Token: 0x0600085D RID: 2141
		void AddClass(string chargroup);

		// Token: 0x0600085E RID: 2142
		void AddException(string word, List<object> hyphenatedword);

		// Token: 0x0600085F RID: 2143
		void AddPattern(string pattern, string values);
	}
}
