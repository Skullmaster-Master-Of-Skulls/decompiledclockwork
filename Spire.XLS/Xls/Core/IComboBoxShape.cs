using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000205 RID: 517
	public interface IComboBoxShape : IShape
	{
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06001DA5 RID: 7589
		// (set) Token: 0x06001DA6 RID: 7590
		IXLSRange ListFillRange { get; set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06001DA7 RID: 7591
		// (set) Token: 0x06001DA8 RID: 7592
		IXLSRange LinkedCell { get; set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06001DA9 RID: 7593
		// (set) Token: 0x06001DAA RID: 7594
		int SelectedIndex { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06001DAB RID: 7595
		// (set) Token: 0x06001DAC RID: 7596
		int DropDownLines { get; set; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06001DAD RID: 7597
		// (set) Token: 0x06001DAE RID: 7598
		bool Display3DShading { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06001DAF RID: 7599
		string SelectedValue { get; }
	}
}
