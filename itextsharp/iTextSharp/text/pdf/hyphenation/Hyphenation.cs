using System;
using System.Text;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x02000059 RID: 89
	public class Hyphenation
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000CACA File Offset: 0x0000BACA
		internal Hyphenation(string word, int[] points)
		{
			this.word = word;
			this.hyphenPoints = points;
			this.len = points.Length;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000CAE9 File Offset: 0x0000BAE9
		public int Length
		{
			get
			{
				return this.len;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CAF1 File Offset: 0x0000BAF1
		public string GetPreHyphenText(int index)
		{
			return this.word.Substring(0, this.hyphenPoints[index]);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CB07 File Offset: 0x0000BB07
		public string GetPostHyphenText(int index)
		{
			return this.word.Substring(this.hyphenPoints[index]);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000CB1C File Offset: 0x0000BB1C
		public int[] HyphenationPoints
		{
			get
			{
				return this.hyphenPoints;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CB24 File Offset: 0x0000BB24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 0; i < this.len; i++)
			{
				stringBuilder.Append(this.word.Substring(num, this.hyphenPoints[i] - num) + "-");
				num = this.hyphenPoints[i];
			}
			stringBuilder.Append(this.word.Substring(num));
			return stringBuilder.ToString();
		}

		// Token: 0x0400013F RID: 319
		private int[] hyphenPoints;

		// Token: 0x04000140 RID: 320
		private string word;

		// Token: 0x04000141 RID: 321
		private int len;
	}
}
