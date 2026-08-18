using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C5 RID: 197
	internal sealed class DetailsViewAutoFormat : BaseAutoFormat<DetailsView>
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00021C27 File Offset: 0x0001FE27
		public DetailsViewAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00021C48 File Offset: 0x0001FE48
		protected override void Apply(DetailsView view)
		{
			view.HeaderStyle.ForeColor = ColorTranslator.FromHtml(this.headerForeColor);
			view.HeaderStyle.BackColor = ColorTranslator.FromHtml(this.headerBackColor);
			view.HeaderStyle.Font.Bold = ((this.headerFont & 1) != 0);
			view.HeaderStyle.Font.Italic = ((this.headerFont & 2) != 0);
			view.HeaderStyle.Font.ClearDefaults();
			view.FooterStyle.ForeColor = ColorTranslator.FromHtml(this.footerForeColor);
			view.FooterStyle.BackColor = ColorTranslator.FromHtml(this.footerBackColor);
			view.FooterStyle.Font.Bold = ((this.footerFont & 1) != 0);
			view.FooterStyle.Font.Italic = ((this.footerFont & 2) != 0);
			view.FooterStyle.Font.ClearDefaults();
			view.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			switch (this.gridLines)
			{
			case 0:
				view.GridLines = GridLines.None;
				break;
			case 1:
				view.GridLines = GridLines.Horizontal;
				break;
			case 2:
				view.GridLines = GridLines.Vertical;
				break;
			case 3:
				view.GridLines = GridLines.Both;
				break;
			default:
				view.GridLines = GridLines.Both;
				break;
			}
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				view.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				view.BorderStyle = BorderStyle.NotSet;
			}
			view.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			view.CellPadding = this.cellPadding;
			view.CellSpacing = this.cellSpacing;
			view.ForeColor = ColorTranslator.FromHtml(this.foreColor);
			view.BackColor = ColorTranslator.FromHtml(this.backColor);
			view.RowStyle.ForeColor = ColorTranslator.FromHtml(this.rowForeColor);
			view.RowStyle.BackColor = ColorTranslator.FromHtml(this.rowBackColor);
			view.RowStyle.Font.Bold = ((this.itemFont & 1) != 0);
			view.RowStyle.Font.Italic = ((this.itemFont & 2) != 0);
			view.RowStyle.Font.ClearDefaults();
			view.AlternatingRowStyle.ForeColor = ColorTranslator.FromHtml(this.alternatingRowForeColor);
			view.AlternatingRowStyle.BackColor = ColorTranslator.FromHtml(this.alternatingRowBackColor);
			view.AlternatingRowStyle.Font.Bold = ((this.alternatingRowFont & 1) != 0);
			view.AlternatingRowStyle.Font.Italic = ((this.alternatingRowFont & 2) != 0);
			view.AlternatingRowStyle.Font.ClearDefaults();
			view.CommandRowStyle.ForeColor = ColorTranslator.FromHtml(this.commandRowForeColor);
			view.CommandRowStyle.BackColor = ColorTranslator.FromHtml(this.commandRowBackColor);
			view.CommandRowStyle.Font.Bold = ((this.commandRowFont & 1) != 0);
			view.CommandRowStyle.Font.Italic = ((this.commandRowFont & 2) != 0);
			view.CommandRowStyle.Font.ClearDefaults();
			view.FieldHeaderStyle.ForeColor = ColorTranslator.FromHtml(this.fieldHeaderForeColor);
			view.FieldHeaderStyle.BackColor = ColorTranslator.FromHtml(this.fieldHeaderBackColor);
			view.FieldHeaderStyle.Font.Bold = ((this.fieldHeaderFont & 1) != 0);
			view.FieldHeaderStyle.Font.Italic = ((this.fieldHeaderFont & 2) != 0);
			view.FieldHeaderStyle.Font.ClearDefaults();
			view.EditRowStyle.ForeColor = ColorTranslator.FromHtml(this.editRowForeColor);
			view.EditRowStyle.BackColor = ColorTranslator.FromHtml(this.editRowBackColor);
			view.EditRowStyle.Font.Bold = ((this.editRowFont & 1) != 0);
			view.EditRowStyle.Font.Italic = ((this.editRowFont & 2) != 0);
			view.EditRowStyle.Font.ClearDefaults();
			view.PagerStyle.ForeColor = ColorTranslator.FromHtml(this.pagerForeColor);
			view.PagerStyle.BackColor = ColorTranslator.FromHtml(this.pagerBackColor);
			view.PagerStyle.Font.Bold = ((this.pagerFont & 1) != 0);
			view.PagerStyle.Font.Italic = ((this.pagerFont & 2) != 0);
			view.PagerStyle.HorizontalAlign = (HorizontalAlign)this.pagerAlign;
			view.PagerStyle.Font.ClearDefaults();
			view.PagerSettings.Mode = (PagerButtons)this.pagerButtons;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000220DC File Offset: 0x000202DC
		protected override void Initialize(DataRow schemeData)
		{
			this.foreColor = BaseAutoFormat<DetailsView>.GetStringProperty("ForeColor", schemeData);
			this.backColor = BaseAutoFormat<DetailsView>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<DetailsView>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<DetailsView>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<DetailsView>.GetIntProperty("BorderStyle", -1, schemeData);
			this.cellSpacing = BaseAutoFormat<DetailsView>.GetIntProperty("CellSpacing", schemeData);
			this.cellPadding = BaseAutoFormat<DetailsView>.GetIntProperty("CellPadding", -1, schemeData);
			this.gridLines = BaseAutoFormat<DetailsView>.GetIntProperty("GridLines", -1, schemeData);
			this.rowForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("RowForeColor", schemeData);
			this.rowBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("RowBackColor", schemeData);
			this.itemFont = BaseAutoFormat<DetailsView>.GetIntProperty("RowFont", schemeData);
			this.alternatingRowForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("AltRowForeColor", schemeData);
			this.alternatingRowBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("AltRowBackColor", schemeData);
			this.alternatingRowFont = BaseAutoFormat<DetailsView>.GetIntProperty("AltRowFont", schemeData);
			this.commandRowForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("CommandRowForeColor", schemeData);
			this.commandRowBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("CommandRowBackColor", schemeData);
			this.commandRowFont = BaseAutoFormat<DetailsView>.GetIntProperty("CommandRowFont", schemeData);
			this.fieldHeaderForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("FieldHeaderForeColor", schemeData);
			this.fieldHeaderBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("FieldHeaderBackColor", schemeData);
			this.fieldHeaderFont = BaseAutoFormat<DetailsView>.GetIntProperty("FieldHeaderFont", schemeData);
			this.editRowForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("EditRowForeColor", schemeData);
			this.editRowBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("EditRowBackColor", schemeData);
			this.editRowFont = BaseAutoFormat<DetailsView>.GetIntProperty("EditRowFont", schemeData);
			this.headerForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("HeaderForeColor", schemeData);
			this.headerBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("HeaderBackColor", schemeData);
			this.headerFont = BaseAutoFormat<DetailsView>.GetIntProperty("HeaderFont", schemeData);
			this.footerForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("FooterForeColor", schemeData);
			this.footerBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("FooterBackColor", schemeData);
			this.footerFont = BaseAutoFormat<DetailsView>.GetIntProperty("FooterFont", schemeData);
			this.pagerForeColor = BaseAutoFormat<DetailsView>.GetStringProperty("PagerForeColor", schemeData);
			this.pagerBackColor = BaseAutoFormat<DetailsView>.GetStringProperty("PagerBackColor", schemeData);
			this.pagerFont = BaseAutoFormat<DetailsView>.GetIntProperty("PagerFont", schemeData);
			this.pagerAlign = BaseAutoFormat<DetailsView>.GetIntProperty("PagerAlign", schemeData);
			this.pagerButtons = BaseAutoFormat<DetailsView>.GetIntProperty("PagerButtons", 1, schemeData);
		}

		// Token: 0x040003A6 RID: 934
		private string headerForeColor;

		// Token: 0x040003A7 RID: 935
		private string headerBackColor;

		// Token: 0x040003A8 RID: 936
		private int headerFont;

		// Token: 0x040003A9 RID: 937
		private string footerForeColor;

		// Token: 0x040003AA RID: 938
		private string footerBackColor;

		// Token: 0x040003AB RID: 939
		private int footerFont;

		// Token: 0x040003AC RID: 940
		private string borderColor;

		// Token: 0x040003AD RID: 941
		private string borderWidth;

		// Token: 0x040003AE RID: 942
		private int borderStyle = -1;

		// Token: 0x040003AF RID: 943
		private int gridLines = -1;

		// Token: 0x040003B0 RID: 944
		private int cellSpacing;

		// Token: 0x040003B1 RID: 945
		private int cellPadding = -1;

		// Token: 0x040003B2 RID: 946
		private string foreColor;

		// Token: 0x040003B3 RID: 947
		private string backColor;

		// Token: 0x040003B4 RID: 948
		private string rowForeColor;

		// Token: 0x040003B5 RID: 949
		private string rowBackColor;

		// Token: 0x040003B6 RID: 950
		private int itemFont;

		// Token: 0x040003B7 RID: 951
		private string alternatingRowForeColor;

		// Token: 0x040003B8 RID: 952
		private string alternatingRowBackColor;

		// Token: 0x040003B9 RID: 953
		private int alternatingRowFont;

		// Token: 0x040003BA RID: 954
		private string commandRowForeColor;

		// Token: 0x040003BB RID: 955
		private string commandRowBackColor;

		// Token: 0x040003BC RID: 956
		private int commandRowFont;

		// Token: 0x040003BD RID: 957
		private string fieldHeaderForeColor;

		// Token: 0x040003BE RID: 958
		private string fieldHeaderBackColor;

		// Token: 0x040003BF RID: 959
		private int fieldHeaderFont;

		// Token: 0x040003C0 RID: 960
		private string editRowForeColor;

		// Token: 0x040003C1 RID: 961
		private string editRowBackColor;

		// Token: 0x040003C2 RID: 962
		private int editRowFont;

		// Token: 0x040003C3 RID: 963
		private string pagerForeColor;

		// Token: 0x040003C4 RID: 964
		private string pagerBackColor;

		// Token: 0x040003C5 RID: 965
		private int pagerFont;

		// Token: 0x040003C6 RID: 966
		private int pagerAlign;

		// Token: 0x040003C7 RID: 967
		private int pagerButtons;

		// Token: 0x040003C8 RID: 968
		private const int FONT_BOLD = 1;

		// Token: 0x040003C9 RID: 969
		private const int FONT_ITALIC = 2;
	}
}
