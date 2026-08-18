using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x0200016F RID: 367
	public interface IInterior
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001197 RID: 4503
		// (set) Token: 0x06001198 RID: 4504
		ExcelColors PatternKnownColor { get; set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001199 RID: 4505
		// (set) Token: 0x0600119A RID: 4506
		Color PatternColor { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600119B RID: 4507
		// (set) Token: 0x0600119C RID: 4508
		ExcelColors KnownColor { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600119D RID: 4509
		// (set) Token: 0x0600119E RID: 4510
		Color Color { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600119F RID: 4511
		ExcelGradient Gradient { get; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060011A0 RID: 4512
		// (set) Token: 0x060011A1 RID: 4513
		ExcelPatternType FillPattern { get; set; }
	}
}
