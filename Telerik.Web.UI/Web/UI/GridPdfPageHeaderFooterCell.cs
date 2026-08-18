using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004C5 RID: 1221
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridPdfPageHeaderFooterCell : ObjectWithState
	{
		// Token: 0x06002C4E RID: 11342 RVA: 0x0009197E File Offset: 0x0008FB7E
		public GridPdfPageHeaderFooterCell(string prefix, StateBag ownerStateBag) : base(string.Format("gpphfc{0}_", prefix), ownerStateBag)
		{
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x00091992 File Offset: 0x0008FB92
		// (set) Token: 0x06002C50 RID: 11344 RVA: 0x000919C1 File Offset: 0x0008FBC1
		[DefaultValue("")]
		public string Text
		{
			get
			{
				if (base.ViewState["Text"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Text"];
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000919D4 File Offset: 0x0008FBD4
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x000919FF File Offset: 0x0008FBFF
		[DefaultValue(GridPdfPageHeaderFooterCell.CellTextAlign.Left)]
		public GridPdfPageHeaderFooterCell.CellTextAlign TextAlign
		{
			get
			{
				if (base.ViewState["TextAlign"] == null)
				{
					return GridPdfPageHeaderFooterCell.CellTextAlign.Left;
				}
				return (GridPdfPageHeaderFooterCell.CellTextAlign)base.ViewState["TextAlign"];
			}
			set
			{
				base.ViewState["TextAlign"] = value;
			}
		}

		// Token: 0x020004C6 RID: 1222
		public enum CellTextAlign
		{
			// Token: 0x04000B74 RID: 2932
			Left,
			// Token: 0x04000B75 RID: 2933
			Right,
			// Token: 0x04000B76 RID: 2934
			Center
		}
	}
}
