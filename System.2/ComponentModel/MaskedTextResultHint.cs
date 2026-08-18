using System;

namespace System.ComponentModel
{
	// Token: 0x0200058F RID: 1423
	public enum MaskedTextResultHint
	{
		// Token: 0x04002A19 RID: 10777
		Unknown,
		// Token: 0x04002A1A RID: 10778
		CharacterEscaped,
		// Token: 0x04002A1B RID: 10779
		NoEffect,
		// Token: 0x04002A1C RID: 10780
		SideEffect,
		// Token: 0x04002A1D RID: 10781
		Success,
		// Token: 0x04002A1E RID: 10782
		AsciiCharacterExpected = -1,
		// Token: 0x04002A1F RID: 10783
		AlphanumericCharacterExpected = -2,
		// Token: 0x04002A20 RID: 10784
		DigitExpected = -3,
		// Token: 0x04002A21 RID: 10785
		LetterExpected = -4,
		// Token: 0x04002A22 RID: 10786
		SignedDigitExpected = -5,
		// Token: 0x04002A23 RID: 10787
		InvalidInput = -51,
		// Token: 0x04002A24 RID: 10788
		PromptCharNotAllowed = -52,
		// Token: 0x04002A25 RID: 10789
		UnavailableEditPosition = -53,
		// Token: 0x04002A26 RID: 10790
		NonEditPosition = -54,
		// Token: 0x04002A27 RID: 10791
		PositionOutOfRange = -55
	}
}
