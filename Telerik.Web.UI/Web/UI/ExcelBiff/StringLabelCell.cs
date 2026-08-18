using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE4 RID: 2788
	internal sealed class StringLabelCell : BiffCell
	{
		// Token: 0x060068D9 RID: 26841 RVA: 0x00189441 File Offset: 0x00187641
		public StringLabelCell(string label)
		{
			base.XFIndex = 15;
			this.label = label;
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x00189458 File Offset: 0x00187658
		public override IRecord GetRecord(int row, int col)
		{
			return new Label((ushort)row, (ushort)col, (ushort)base.XFIndex, this.label);
		}

		// Token: 0x04001C12 RID: 7186
		private string label;
	}
}
