using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200005F RID: 95
	internal enum TokenType
	{
		// Token: 0x040001F4 RID: 500
		None,
		// Token: 0x040001F5 RID: 501
		Space,
		// Token: 0x040001F6 RID: 502
		CommentOpen,
		// Token: 0x040001F7 RID: 503
		CommentClose,
		// Token: 0x040001F8 RID: 504
		Includes,
		// Token: 0x040001F9 RID: 505
		DashMatch,
		// Token: 0x040001FA RID: 506
		PrefixMatch,
		// Token: 0x040001FB RID: 507
		SuffixMatch,
		// Token: 0x040001FC RID: 508
		SubstringMatch,
		// Token: 0x040001FD RID: 509
		String,
		// Token: 0x040001FE RID: 510
		Identifier,
		// Token: 0x040001FF RID: 511
		Hash,
		// Token: 0x04000200 RID: 512
		ImportSymbol,
		// Token: 0x04000201 RID: 513
		PageSymbol,
		// Token: 0x04000202 RID: 514
		MediaSymbol,
		// Token: 0x04000203 RID: 515
		FontFaceSymbol,
		// Token: 0x04000204 RID: 516
		CharacterSetSymbol,
		// Token: 0x04000205 RID: 517
		AtKeyword,
		// Token: 0x04000206 RID: 518
		ImportantSymbol,
		// Token: 0x04000207 RID: 519
		NamespaceSymbol,
		// Token: 0x04000208 RID: 520
		KeyFramesSymbol,
		// Token: 0x04000209 RID: 521
		RelativeLength,
		// Token: 0x0400020A RID: 522
		AbsoluteLength,
		// Token: 0x0400020B RID: 523
		Resolution,
		// Token: 0x0400020C RID: 524
		Angle,
		// Token: 0x0400020D RID: 525
		Time,
		// Token: 0x0400020E RID: 526
		Frequency,
		// Token: 0x0400020F RID: 527
		Speech,
		// Token: 0x04000210 RID: 528
		Dimension,
		// Token: 0x04000211 RID: 529
		Percentage,
		// Token: 0x04000212 RID: 530
		Number,
		// Token: 0x04000213 RID: 531
		Uri,
		// Token: 0x04000214 RID: 532
		Function,
		// Token: 0x04000215 RID: 533
		Not,
		// Token: 0x04000216 RID: 534
		UnicodeRange,
		// Token: 0x04000217 RID: 535
		ProgId,
		// Token: 0x04000218 RID: 536
		Character,
		// Token: 0x04000219 RID: 537
		Comment,
		// Token: 0x0400021A RID: 538
		TopLeftCornerSymbol,
		// Token: 0x0400021B RID: 539
		TopLeftSymbol,
		// Token: 0x0400021C RID: 540
		TopCenterSymbol,
		// Token: 0x0400021D RID: 541
		TopRightSymbol,
		// Token: 0x0400021E RID: 542
		TopRightCornerSymbol,
		// Token: 0x0400021F RID: 543
		BottomLeftCornerSymbol,
		// Token: 0x04000220 RID: 544
		BottomLeftSymbol,
		// Token: 0x04000221 RID: 545
		BottomCenterSymbol,
		// Token: 0x04000222 RID: 546
		BottomRightSymbol,
		// Token: 0x04000223 RID: 547
		BottomRightCornerSymbol,
		// Token: 0x04000224 RID: 548
		LeftTopSymbol,
		// Token: 0x04000225 RID: 549
		LeftMiddleSymbol,
		// Token: 0x04000226 RID: 550
		LeftBottomSymbol,
		// Token: 0x04000227 RID: 551
		RightTopSymbol,
		// Token: 0x04000228 RID: 552
		RightMiddleSymbol,
		// Token: 0x04000229 RID: 553
		RightBottomSymbol,
		// Token: 0x0400022A RID: 554
		AspNetBlock,
		// Token: 0x0400022B RID: 555
		ReplacementToken,
		// Token: 0x0400022C RID: 556
		Error = -1
	}
}
