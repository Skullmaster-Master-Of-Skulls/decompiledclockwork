using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004C4 RID: 1220
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridPdfPageHeaderFooter : ObjectWithState
	{
		// Token: 0x06002C49 RID: 11337 RVA: 0x0009187C File Offset: 0x0008FA7C
		public GridPdfPageHeaderFooter(string prefix, StateBag ownerStateBag) : base(string.Format("gpp{0}_", prefix), ownerStateBag)
		{
			this.prefix = prefix;
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x00091897 File Offset: 0x0008FA97
		[Category("Pdf")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public GridPdfPageHeaderFooterCell LeftCell
		{
			get
			{
				if (this.leftCell == null)
				{
					this.leftCell = new GridPdfPageHeaderFooterCell(this.prefix + "l", base.OwnerViewState);
				}
				return this.leftCell;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000918C8 File Offset: 0x0008FAC8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Category("Pdf")]
		public GridPdfPageHeaderFooterCell MiddleCell
		{
			get
			{
				if (this.middleCell == null)
				{
					this.middleCell = new GridPdfPageHeaderFooterCell(this.prefix + "m", base.OwnerViewState);
				}
				return this.middleCell;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000918F9 File Offset: 0x0008FAF9
		[Category("Pdf")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridPdfPageHeaderFooterCell RightCell
		{
			get
			{
				if (this.rightCell == null)
				{
					this.rightCell = new GridPdfPageHeaderFooterCell(this.prefix + "r", base.OwnerViewState);
				}
				return this.rightCell;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x0009192C File Offset: 0x0008FB2C
		public bool IsEmpty
		{
			get
			{
				return this.RightCell.Text == string.Empty && this.LeftCell.Text == string.Empty && this.MiddleCell.Text == string.Empty;
			}
		}

		// Token: 0x04000B6F RID: 2927
		private GridPdfPageHeaderFooterCell rightCell;

		// Token: 0x04000B70 RID: 2928
		private GridPdfPageHeaderFooterCell middleCell;

		// Token: 0x04000B71 RID: 2929
		private GridPdfPageHeaderFooterCell leftCell;

		// Token: 0x04000B72 RID: 2930
		private string prefix;
	}
}
