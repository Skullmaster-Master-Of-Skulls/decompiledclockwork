using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CC RID: 204
	internal sealed class GridViewAutoFormat : BaseAutoFormat<GridView>
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x00025544 File Offset: 0x00023744
		public GridViewAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 260;
			base.Style.Height = 240;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00025598 File Offset: 0x00023798
		protected override void Apply(GridView grid)
		{
			grid.HeaderStyle.ForeColor = ColorTranslator.FromHtml(this.headerForeColor);
			grid.HeaderStyle.BackColor = ColorTranslator.FromHtml(this.headerBackColor);
			grid.HeaderStyle.Font.Bold = ((this.headerFont & 1) != 0);
			grid.HeaderStyle.Font.Italic = ((this.headerFont & 2) != 0);
			grid.HeaderStyle.Font.ClearDefaults();
			grid.FooterStyle.ForeColor = ColorTranslator.FromHtml(this.footerForeColor);
			grid.FooterStyle.BackColor = ColorTranslator.FromHtml(this.footerBackColor);
			grid.FooterStyle.Font.Bold = ((this.footerFont & 1) != 0);
			grid.FooterStyle.Font.Italic = ((this.footerFont & 2) != 0);
			grid.FooterStyle.Font.ClearDefaults();
			grid.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			switch (this.gridLines)
			{
			case 0:
				grid.GridLines = GridLines.None;
				goto IL_13B;
			case 1:
				grid.GridLines = GridLines.Horizontal;
				goto IL_13B;
			case 2:
				grid.GridLines = GridLines.Vertical;
				goto IL_13B;
			}
			grid.GridLines = GridLines.Both;
			IL_13B:
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				grid.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				grid.BorderStyle = BorderStyle.NotSet;
			}
			grid.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			grid.CellPadding = this.cellPadding;
			grid.CellSpacing = this.cellSpacing;
			grid.ForeColor = ColorTranslator.FromHtml(this.foreColor);
			grid.BackColor = ColorTranslator.FromHtml(this.backColor);
			grid.RowStyle.ForeColor = ColorTranslator.FromHtml(this.itemForeColor);
			grid.RowStyle.BackColor = ColorTranslator.FromHtml(this.itemBackColor);
			grid.RowStyle.Font.Bold = ((this.itemFont & 1) != 0);
			grid.RowStyle.Font.Italic = ((this.itemFont & 2) != 0);
			grid.RowStyle.Font.ClearDefaults();
			grid.AlternatingRowStyle.ForeColor = ColorTranslator.FromHtml(this.alternatingItemForeColor);
			grid.AlternatingRowStyle.BackColor = ColorTranslator.FromHtml(this.alternatingItemBackColor);
			grid.AlternatingRowStyle.Font.Bold = ((this.alternatingItemFont & 1) != 0);
			grid.AlternatingRowStyle.Font.Italic = ((this.alternatingItemFont & 2) != 0);
			grid.AlternatingRowStyle.Font.ClearDefaults();
			grid.SelectedRowStyle.ForeColor = ColorTranslator.FromHtml(this.selectedItemForeColor);
			grid.SelectedRowStyle.BackColor = ColorTranslator.FromHtml(this.selectedItemBackColor);
			grid.SelectedRowStyle.Font.Bold = ((this.selectedItemFont & 1) != 0);
			grid.SelectedRowStyle.Font.Italic = ((this.selectedItemFont & 2) != 0);
			grid.SelectedRowStyle.Font.ClearDefaults();
			grid.PagerStyle.ForeColor = ColorTranslator.FromHtml(this.pagerForeColor);
			grid.PagerStyle.BackColor = ColorTranslator.FromHtml(this.pagerBackColor);
			grid.PagerStyle.Font.Bold = ((this.pagerFont & 1) != 0);
			grid.PagerStyle.Font.Italic = ((this.pagerFont & 2) != 0);
			grid.PagerStyle.HorizontalAlign = (HorizontalAlign)this.pagerAlign;
			grid.PagerStyle.Font.ClearDefaults();
			grid.PagerSettings.Mode = (PagerButtons)this.pagerButtons;
			grid.EditRowStyle.ForeColor = ColorTranslator.FromHtml(this.editItemForeColor);
			grid.EditRowStyle.BackColor = ColorTranslator.FromHtml(this.editItemBackColor);
			grid.EditRowStyle.Font.Bold = ((this.editItemFont & 1) != 0);
			grid.EditRowStyle.Font.Italic = ((this.editItemFont & 2) != 0);
			grid.EditRowStyle.Font.ClearDefaults();
			grid.SortedAscendingCellStyle.BackColor = ColorTranslator.FromHtml(this.sortedAscendingCellBackColor);
			grid.SortedDescendingCellStyle.BackColor = ColorTranslator.FromHtml(this.sortedDescendingCellBackColor);
			grid.SortedAscendingHeaderStyle.BackColor = ColorTranslator.FromHtml(this.sortedAscendingHeaderBackColor);
			grid.SortedDescendingHeaderStyle.BackColor = ColorTranslator.FromHtml(this.sortedDescendingHeaderBackColor);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00025A08 File Offset: 0x00023C08
		protected override void Initialize(DataRow schemeData)
		{
			this.foreColor = BaseAutoFormat<GridView>.GetStringProperty("ForeColor", schemeData);
			this.backColor = BaseAutoFormat<GridView>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<GridView>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<GridView>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<GridView>.GetIntProperty("BorderStyle", -1, schemeData);
			this.cellSpacing = BaseAutoFormat<GridView>.GetIntProperty("CellSpacing", schemeData);
			this.cellPadding = BaseAutoFormat<GridView>.GetIntProperty("CellPadding", -1, schemeData);
			this.gridLines = BaseAutoFormat<GridView>.GetIntProperty("GridLines", -1, schemeData);
			this.itemForeColor = BaseAutoFormat<GridView>.GetStringProperty("ItemForeColor", schemeData);
			this.itemBackColor = BaseAutoFormat<GridView>.GetStringProperty("ItemBackColor", schemeData);
			this.itemFont = BaseAutoFormat<GridView>.GetIntProperty("ItemFont", schemeData);
			this.alternatingItemForeColor = BaseAutoFormat<GridView>.GetStringProperty("AltItemForeColor", schemeData);
			this.alternatingItemBackColor = BaseAutoFormat<GridView>.GetStringProperty("AltItemBackColor", schemeData);
			this.alternatingItemFont = BaseAutoFormat<GridView>.GetIntProperty("AltItemFont", schemeData);
			this.selectedItemForeColor = BaseAutoFormat<GridView>.GetStringProperty("SelItemForeColor", schemeData);
			this.selectedItemBackColor = BaseAutoFormat<GridView>.GetStringProperty("SelItemBackColor", schemeData);
			this.selectedItemFont = BaseAutoFormat<GridView>.GetIntProperty("SelItemFont", schemeData);
			this.headerForeColor = BaseAutoFormat<GridView>.GetStringProperty("HeaderForeColor", schemeData);
			this.headerBackColor = BaseAutoFormat<GridView>.GetStringProperty("HeaderBackColor", schemeData);
			this.headerFont = BaseAutoFormat<GridView>.GetIntProperty("HeaderFont", schemeData);
			this.footerForeColor = BaseAutoFormat<GridView>.GetStringProperty("FooterForeColor", schemeData);
			this.footerBackColor = BaseAutoFormat<GridView>.GetStringProperty("FooterBackColor", schemeData);
			this.footerFont = BaseAutoFormat<GridView>.GetIntProperty("FooterFont", schemeData);
			this.pagerForeColor = BaseAutoFormat<GridView>.GetStringProperty("PagerForeColor", schemeData);
			this.pagerBackColor = BaseAutoFormat<GridView>.GetStringProperty("PagerBackColor", schemeData);
			this.pagerFont = BaseAutoFormat<GridView>.GetIntProperty("PagerFont", schemeData);
			this.pagerAlign = BaseAutoFormat<GridView>.GetIntProperty("PagerAlign", schemeData);
			this.pagerButtons = BaseAutoFormat<GridView>.GetIntProperty("PagerButtons", 1, schemeData);
			this.editItemForeColor = BaseAutoFormat<GridView>.GetStringProperty("EditItemForeColor", schemeData);
			this.editItemBackColor = BaseAutoFormat<GridView>.GetStringProperty("EditItemBackColor", schemeData);
			this.editItemFont = BaseAutoFormat<GridView>.GetIntProperty("EditItemFont", schemeData);
			this.sortedAscendingCellBackColor = BaseAutoFormat<GridView>.GetStringProperty("SortedAscendingCellBackColor", schemeData);
			this.sortedDescendingCellBackColor = BaseAutoFormat<GridView>.GetStringProperty("SortedDescendingCellBackColor", schemeData);
			this.sortedAscendingHeaderBackColor = BaseAutoFormat<GridView>.GetStringProperty("SortedAscendingHeaderBackColor", schemeData);
			this.sortedDescendingHeaderBackColor = BaseAutoFormat<GridView>.GetStringProperty("SortedDescendingHeaderBackColor", schemeData);
		}

		// Token: 0x0400041F RID: 1055
		private string headerForeColor;

		// Token: 0x04000420 RID: 1056
		private string headerBackColor;

		// Token: 0x04000421 RID: 1057
		private int headerFont;

		// Token: 0x04000422 RID: 1058
		private string footerForeColor;

		// Token: 0x04000423 RID: 1059
		private string footerBackColor;

		// Token: 0x04000424 RID: 1060
		private int footerFont;

		// Token: 0x04000425 RID: 1061
		private string borderColor;

		// Token: 0x04000426 RID: 1062
		private string borderWidth;

		// Token: 0x04000427 RID: 1063
		private int borderStyle = -1;

		// Token: 0x04000428 RID: 1064
		private int gridLines = -1;

		// Token: 0x04000429 RID: 1065
		private int cellSpacing;

		// Token: 0x0400042A RID: 1066
		private int cellPadding = -1;

		// Token: 0x0400042B RID: 1067
		private string foreColor;

		// Token: 0x0400042C RID: 1068
		private string backColor;

		// Token: 0x0400042D RID: 1069
		private string itemForeColor;

		// Token: 0x0400042E RID: 1070
		private string itemBackColor;

		// Token: 0x0400042F RID: 1071
		private int itemFont;

		// Token: 0x04000430 RID: 1072
		private string alternatingItemForeColor;

		// Token: 0x04000431 RID: 1073
		private string alternatingItemBackColor;

		// Token: 0x04000432 RID: 1074
		private int alternatingItemFont;

		// Token: 0x04000433 RID: 1075
		private string selectedItemForeColor;

		// Token: 0x04000434 RID: 1076
		private string selectedItemBackColor;

		// Token: 0x04000435 RID: 1077
		private int selectedItemFont;

		// Token: 0x04000436 RID: 1078
		private string pagerForeColor;

		// Token: 0x04000437 RID: 1079
		private string pagerBackColor;

		// Token: 0x04000438 RID: 1080
		private int pagerFont;

		// Token: 0x04000439 RID: 1081
		private int pagerAlign;

		// Token: 0x0400043A RID: 1082
		private int pagerButtons;

		// Token: 0x0400043B RID: 1083
		private string editItemForeColor;

		// Token: 0x0400043C RID: 1084
		private string editItemBackColor;

		// Token: 0x0400043D RID: 1085
		private int editItemFont;

		// Token: 0x0400043E RID: 1086
		private string sortedAscendingCellBackColor;

		// Token: 0x0400043F RID: 1087
		private string sortedDescendingCellBackColor;

		// Token: 0x04000440 RID: 1088
		private string sortedAscendingHeaderBackColor;

		// Token: 0x04000441 RID: 1089
		private string sortedDescendingHeaderBackColor;

		// Token: 0x04000442 RID: 1090
		private const int FONT_BOLD = 1;

		// Token: 0x04000443 RID: 1091
		private const int FONT_ITALIC = 2;
	}
}
