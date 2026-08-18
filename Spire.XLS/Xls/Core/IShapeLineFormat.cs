using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000223 RID: 547
	public interface IShapeLineFormat
	{
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060020F3 RID: 8435
		// (set) Token: 0x060020F4 RID: 8436
		double Weight { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060020F5 RID: 8437
		// (set) Token: 0x060020F6 RID: 8438
		Color ForeColor { get; set; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060020F7 RID: 8439
		// (set) Token: 0x060020F8 RID: 8440
		Color BackColor { get; set; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060020F9 RID: 8441
		// (set) Token: 0x060020FA RID: 8442
		ExcelColors ForeKnownColor { get; set; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060020FB RID: 8443
		// (set) Token: 0x060020FC RID: 8444
		ExcelColors BackKnownColor { get; set; }

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060020FD RID: 8445
		// (set) Token: 0x060020FE RID: 8446
		ShapeArrowStyleType BeginArrowHeadStyle { get; set; }

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060020FF RID: 8447
		// (set) Token: 0x06002100 RID: 8448
		ShapeArrowStyleType EndArrowHeadStyle { get; set; }

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002101 RID: 8449
		// (set) Token: 0x06002102 RID: 8450
		ShapeArrowLengthType BeginArrowheadLength { get; set; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002103 RID: 8451
		// (set) Token: 0x06002104 RID: 8452
		ShapeArrowLengthType EndArrowheadLength { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002105 RID: 8453
		// (set) Token: 0x06002106 RID: 8454
		ShapeArrowWidthType BeginArrowheadWidth { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002107 RID: 8455
		// (set) Token: 0x06002108 RID: 8456
		ShapeArrowWidthType EndArrowheadWidth { get; set; }

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002109 RID: 8457
		// (set) Token: 0x0600210A RID: 8458
		ShapeDashLineStyleType DashStyle { get; set; }

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600210B RID: 8459
		// (set) Token: 0x0600210C RID: 8460
		ShapeLineStyleType Style { get; set; }

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600210D RID: 8461
		// (set) Token: 0x0600210E RID: 8462
		double Transparency { get; set; }

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600210F RID: 8463
		// (set) Token: 0x06002110 RID: 8464
		bool Visible { get; set; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002111 RID: 8465
		// (set) Token: 0x06002112 RID: 8466
		GradientPatternType Pattern { get; set; }

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002113 RID: 8467
		// (set) Token: 0x06002114 RID: 8468
		bool HasPattern { get; set; }
	}
}
