using System;

namespace Spire.Xls.Core.Parser.Biff_Records.Formula
{
	// Token: 0x02000641 RID: 1601
	public enum Priority
	{
		// Token: 0x04002F1F RID: 12063
		None,
		// Token: 0x04002F20 RID: 12064
		Equality,
		// Token: 0x04002F21 RID: 12065
		Concat,
		// Token: 0x04002F22 RID: 12066
		PlusMinus,
		// Token: 0x04002F23 RID: 12067
		MulDiv,
		// Token: 0x04002F24 RID: 12068
		Power,
		// Token: 0x04002F25 RID: 12069
		UnaryMinus,
		// Token: 0x04002F26 RID: 12070
		CellRange
	}
}
