using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000285 RID: 645
	public interface IHyphenationEvent
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600185C RID: 6236
		string HyphenSymbol { get; }

		// Token: 0x0600185D RID: 6237
		string GetHyphenatedWordPre(string word, BaseFont font, float fontSize, float remainingWidth);

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600185E RID: 6238
		string HyphenatedWordPost { get; }
	}
}
