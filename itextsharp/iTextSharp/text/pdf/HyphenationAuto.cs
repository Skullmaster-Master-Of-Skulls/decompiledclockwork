using System;
using iTextSharp.text.pdf.hyphenation;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000286 RID: 646
	public class HyphenationAuto : IHyphenationEvent
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x0008D37C File Offset: 0x0008C37C
		public HyphenationAuto(string lang, string country, int leftMin, int rightMin)
		{
			this.hyphenator = new Hyphenator(lang, country, leftMin, rightMin);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0008D394 File Offset: 0x0008C394
		public string HyphenSymbol
		{
			get
			{
				return "-";
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0008D39C File Offset: 0x0008C39C
		public string GetHyphenatedWordPre(string word, BaseFont font, float fontSize, float remainingWidth)
		{
			this.post = word;
			string hyphenSymbol = this.HyphenSymbol;
			float widthPoint = font.GetWidthPoint(hyphenSymbol, fontSize);
			if (widthPoint > remainingWidth)
			{
				return "";
			}
			Hyphenation hyphenation = this.hyphenator.Hyphenate(word);
			if (hyphenation == null)
			{
				return "";
			}
			int length = hyphenation.Length;
			int num = 0;
			while (num < length && font.GetWidthPoint(hyphenation.GetPreHyphenText(num), fontSize) + widthPoint <= remainingWidth)
			{
				num++;
			}
			num--;
			if (num < 0)
			{
				return "";
			}
			this.post = hyphenation.GetPostHyphenText(num);
			return hyphenation.GetPreHyphenText(num) + hyphenSymbol;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0008D43A File Offset: 0x0008C43A
		public string HyphenatedWordPost
		{
			get
			{
				return this.post;
			}
		}

		// Token: 0x04001062 RID: 4194
		protected Hyphenator hyphenator;

		// Token: 0x04001063 RID: 4195
		protected string post;
	}
}
