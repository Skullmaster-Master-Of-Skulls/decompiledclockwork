using System;
using Spire.Doc.Formatting;

namespace Spire.Doc.Interface
{
	// Token: 0x0200049E RID: 1182
	public interface IParagraphStyle : IStyle
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06004091 RID: 16529
		ParagraphFormat ParagraphFormat { get; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06004092 RID: 16530
		CharacterFormat CharacterFormat { get; }
	}
}
