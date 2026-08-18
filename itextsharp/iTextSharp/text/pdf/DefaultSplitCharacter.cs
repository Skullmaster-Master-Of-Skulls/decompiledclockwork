using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000ED RID: 237
	public class DefaultSplitCharacter : ISplitCharacter
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0002FEC8 File Offset: 0x0002EEC8
		public bool IsSplitCharacter(int start, int current, int end, char[] cc, PdfChunk[] ck)
		{
			char currentCharacter = this.GetCurrentCharacter(current, cc, ck);
			return currentCharacter <= ' ' || currentCharacter == '-' || currentCharacter == '‐' || (currentCharacter >= '\u2002' && ((currentCharacter >= '\u2002' && currentCharacter <= '​') || (currentCharacter >= '⺀' && currentCharacter < '힠') || (currentCharacter >= '豈' && currentCharacter < 'ﬀ') || (currentCharacter >= '︰' && currentCharacter < '﹐') || (currentCharacter >= '｡' && currentCharacter < 'ﾠ')));
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002FF53 File Offset: 0x0002EF53
		protected char GetCurrentCharacter(int current, char[] cc, PdfChunk[] ck)
		{
			if (ck == null)
			{
				return cc[current];
			}
			return (char)ck[Math.Min(current, ck.Length - 1)].GetUnicodeEquivalent((int)cc[current]);
		}

		// Token: 0x0400078C RID: 1932
		public static readonly ISplitCharacter DEFAULT = new DefaultSplitCharacter();
	}
}
