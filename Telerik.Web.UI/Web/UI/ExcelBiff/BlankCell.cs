using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A6A RID: 2666
	internal class BlankCell : BiffCell
	{
		// Token: 0x060066EF RID: 26351 RVA: 0x0018168B File Offset: 0x0017F88B
		public BlankCell()
		{
			base.XFIndex = 15;
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x0018169C File Offset: 0x0017F89C
		public override IRecord GetRecord(int row, int col)
		{
			return new Blank((ushort)row, (ushort)col, (ushort)base.XFIndex);
		}
	}
}
