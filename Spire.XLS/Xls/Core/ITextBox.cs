using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005D5 RID: 1493
	public interface ITextBox : IExcelApplication
	{
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060058B5 RID: 22709
		// (set) Token: 0x060058B6 RID: 22710
		CommentHAlignType HAlignment { get; set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060058B7 RID: 22711
		// (set) Token: 0x060058B8 RID: 22712
		CommentVAlignType VAlignment { get; set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060058B9 RID: 22713
		// (set) Token: 0x060058BA RID: 22714
		TextRotationType TextRotation { get; set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060058BB RID: 22715
		// (set) Token: 0x060058BC RID: 22716
		bool IsTextLocked { get; set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060058BD RID: 22717
		IRichTextString RichText { get; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060058BE RID: 22718
		// (set) Token: 0x060058BF RID: 22719
		string Text { get; set; }
	}
}
