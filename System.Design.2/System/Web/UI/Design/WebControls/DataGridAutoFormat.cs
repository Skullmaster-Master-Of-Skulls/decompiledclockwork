using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000BA RID: 186
	internal sealed class DataGridAutoFormat : BaseAutoFormat<DataGrid>
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		public DataGridAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		protected override void Apply(DataGrid grid)
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
			grid.ItemStyle.ForeColor = ColorTranslator.FromHtml(this.itemForeColor);
			grid.ItemStyle.BackColor = ColorTranslator.FromHtml(this.itemBackColor);
			grid.ItemStyle.Font.Bold = ((this.itemFont & 1) != 0);
			grid.ItemStyle.Font.Italic = ((this.itemFont & 2) != 0);
			grid.ItemStyle.Font.ClearDefaults();
			grid.AlternatingItemStyle.ForeColor = ColorTranslator.FromHtml(this.alternatingItemForeColor);
			grid.AlternatingItemStyle.BackColor = ColorTranslator.FromHtml(this.alternatingItemBackColor);
			grid.AlternatingItemStyle.Font.Bold = ((this.alternatingItemFont & 1) != 0);
			grid.AlternatingItemStyle.Font.Italic = ((this.alternatingItemFont & 2) != 0);
			grid.AlternatingItemStyle.Font.ClearDefaults();
			grid.SelectedItemStyle.ForeColor = ColorTranslator.FromHtml(this.selectedItemForeColor);
			grid.SelectedItemStyle.BackColor = ColorTranslator.FromHtml(this.selectedItemBackColor);
			grid.SelectedItemStyle.Font.Bold = ((this.selectedItemFont & 1) != 0);
			grid.SelectedItemStyle.Font.Italic = ((this.selectedItemFont & 2) != 0);
			grid.SelectedItemStyle.Font.ClearDefaults();
			grid.PagerStyle.ForeColor = ColorTranslator.FromHtml(this.pagerForeColor);
			grid.PagerStyle.BackColor = ColorTranslator.FromHtml(this.pagerBackColor);
			grid.PagerStyle.Font.Bold = ((this.pagerFont & 1) != 0);
			grid.PagerStyle.Font.Italic = ((this.pagerFont & 2) != 0);
			grid.PagerStyle.HorizontalAlign = (HorizontalAlign)this.pagerAlign;
			grid.PagerStyle.Font.ClearDefaults();
			grid.PagerStyle.Mode = (PagerMode)this.pagerMode;
			grid.EditItemStyle.ForeColor = ColorTranslator.FromHtml(this.editItemForeColor);
			grid.EditItemStyle.BackColor = ColorTranslator.FromHtml(this.editItemBackColor);
			grid.EditItemStyle.Font.Bold = ((this.editItemFont & 1) != 0);
			grid.EditItemStyle.Font.Italic = ((this.editItemFont & 2) != 0);
			grid.EditItemStyle.Font.ClearDefaults();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		protected override void Initialize(DataRow schemeData)
		{
			this.foreColor = BaseAutoFormat<DataGrid>.GetStringProperty("ForeColor", schemeData);
			this.backColor = BaseAutoFormat<DataGrid>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<DataGrid>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<DataGrid>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<DataGrid>.GetIntProperty("BorderStyle", -1, schemeData);
			this.cellSpacing = BaseAutoFormat<DataGrid>.GetIntProperty("CellSpacing", schemeData);
			this.cellPadding = BaseAutoFormat<DataGrid>.GetIntProperty("CellPadding", -1, schemeData);
			this.gridLines = BaseAutoFormat<DataGrid>.GetIntProperty("GridLines", -1, schemeData);
			this.itemForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("ItemForeColor", schemeData);
			this.itemBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("ItemBackColor", schemeData);
			this.itemFont = BaseAutoFormat<DataGrid>.GetIntProperty("ItemFont", schemeData);
			this.alternatingItemForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("AltItemForeColor", schemeData);
			this.alternatingItemBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("AltItemBackColor", schemeData);
			this.alternatingItemFont = BaseAutoFormat<DataGrid>.GetIntProperty("AltItemFont", schemeData);
			this.selectedItemForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("SelItemForeColor", schemeData);
			this.selectedItemBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("SelItemBackColor", schemeData);
			this.selectedItemFont = BaseAutoFormat<DataGrid>.GetIntProperty("SelItemFont", schemeData);
			this.headerForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("HeaderForeColor", schemeData);
			this.headerBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("HeaderBackColor", schemeData);
			this.headerFont = BaseAutoFormat<DataGrid>.GetIntProperty("HeaderFont", schemeData);
			this.footerForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("FooterForeColor", schemeData);
			this.footerBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("FooterBackColor", schemeData);
			this.footerFont = BaseAutoFormat<DataGrid>.GetIntProperty("FooterFont", schemeData);
			this.pagerForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("PagerForeColor", schemeData);
			this.pagerBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("PagerBackColor", schemeData);
			this.pagerFont = BaseAutoFormat<DataGrid>.GetIntProperty("PagerFont", schemeData);
			this.pagerAlign = BaseAutoFormat<DataGrid>.GetIntProperty("PagerAlign", schemeData);
			this.pagerMode = BaseAutoFormat<DataGrid>.GetIntProperty("PagerMode", schemeData);
			this.editItemForeColor = BaseAutoFormat<DataGrid>.GetStringProperty("EditItemForeColor", schemeData);
			this.editItemBackColor = BaseAutoFormat<DataGrid>.GetStringProperty("EditItemBackColor", schemeData);
			this.editItemFont = BaseAutoFormat<DataGrid>.GetIntProperty("EditItemFont", schemeData);
		}

		// Token: 0x04000327 RID: 807
		private string headerForeColor;

		// Token: 0x04000328 RID: 808
		private string headerBackColor;

		// Token: 0x04000329 RID: 809
		private int headerFont;

		// Token: 0x0400032A RID: 810
		private string footerForeColor;

		// Token: 0x0400032B RID: 811
		private string footerBackColor;

		// Token: 0x0400032C RID: 812
		private int footerFont;

		// Token: 0x0400032D RID: 813
		private string borderColor;

		// Token: 0x0400032E RID: 814
		private string borderWidth;

		// Token: 0x0400032F RID: 815
		private int borderStyle = -1;

		// Token: 0x04000330 RID: 816
		private int gridLines = -1;

		// Token: 0x04000331 RID: 817
		private int cellSpacing;

		// Token: 0x04000332 RID: 818
		private int cellPadding = -1;

		// Token: 0x04000333 RID: 819
		private string foreColor;

		// Token: 0x04000334 RID: 820
		private string backColor;

		// Token: 0x04000335 RID: 821
		private string itemForeColor;

		// Token: 0x04000336 RID: 822
		private string itemBackColor;

		// Token: 0x04000337 RID: 823
		private int itemFont;

		// Token: 0x04000338 RID: 824
		private string alternatingItemForeColor;

		// Token: 0x04000339 RID: 825
		private string alternatingItemBackColor;

		// Token: 0x0400033A RID: 826
		private int alternatingItemFont;

		// Token: 0x0400033B RID: 827
		private string selectedItemForeColor;

		// Token: 0x0400033C RID: 828
		private string selectedItemBackColor;

		// Token: 0x0400033D RID: 829
		private int selectedItemFont;

		// Token: 0x0400033E RID: 830
		private string pagerForeColor;

		// Token: 0x0400033F RID: 831
		private string pagerBackColor;

		// Token: 0x04000340 RID: 832
		private int pagerFont;

		// Token: 0x04000341 RID: 833
		private int pagerAlign;

		// Token: 0x04000342 RID: 834
		private int pagerMode;

		// Token: 0x04000343 RID: 835
		private string editItemForeColor;

		// Token: 0x04000344 RID: 836
		private string editItemBackColor;

		// Token: 0x04000345 RID: 837
		private int editItemFont;

		// Token: 0x04000346 RID: 838
		private const int FONT_BOLD = 1;

		// Token: 0x04000347 RID: 839
		private const int FONT_ITALIC = 2;
	}
}
