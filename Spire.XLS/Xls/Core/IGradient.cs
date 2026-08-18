using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000156 RID: 342
	public interface IGradient
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000EDE RID: 3806
		OColor BackColorObject { get; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000EDF RID: 3807
		OColor ForeColorObject { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000EE0 RID: 3808
		// (set) Token: 0x06000EE1 RID: 3809
		Color BackColor { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000EE2 RID: 3810
		// (set) Token: 0x06000EE3 RID: 3811
		ExcelColors BackKnownColor { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000EE4 RID: 3812
		// (set) Token: 0x06000EE5 RID: 3813
		Color ForeColor { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000EE6 RID: 3814
		// (set) Token: 0x06000EE7 RID: 3815
		ExcelColors ForeKnownColor { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000EE8 RID: 3816
		// (set) Token: 0x06000EE9 RID: 3817
		GradientStyleType GradientStyle { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000EEA RID: 3818
		// (set) Token: 0x06000EEB RID: 3819
		GradientVariantsType GradientVariant { get; set; }

		// Token: 0x06000EEC RID: 3820
		int CompareTo(IGradient gradient);

		// Token: 0x06000EED RID: 3821
		void TwoColorGradient();

		// Token: 0x06000EEE RID: 3822
		void TwoColorGradient(GradientStyleType style, GradientVariantsType variant);
	}
}
