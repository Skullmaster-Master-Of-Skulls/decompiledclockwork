using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C9 RID: 201
	internal sealed class FormViewAutoFormat : BaseAutoFormat<FormView>
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public FormViewAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00023C04 File Offset: 0x00021E04
		protected override void Apply(FormView view)
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
				view.GridLines = GridLines.None;
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
			view.RenderOuterTable = bool.Parse(this.renderOuterTable);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00023F54 File Offset: 0x00022154
		public override Control GetPreviewControl(Control runtimeControl)
		{
			Control previewControl = base.GetPreviewControl(runtimeControl);
			if (previewControl != null)
			{
				IDesignerHost designerHost = (IDesignerHost)runtimeControl.Site.GetService(typeof(IDesignerHost));
				FormView formView = previewControl as FormView;
				if (formView != null && designerHost != null)
				{
					TemplateBuilder templateBuilder = formView.ItemTemplate as TemplateBuilder;
					if ((templateBuilder != null && templateBuilder.Text.Length == 0) || formView.ItemTemplate == null)
					{
						string templateText = "####&nbsp;&nbsp;####<br/>####&nbsp;&nbsp;####<br/>####&nbsp;&nbsp;####<br/>####&nbsp;&nbsp;####";
						formView.ItemTemplate = ControlParser.ParseTemplate(designerHost, templateText);
						formView.RowStyle.HorizontalAlign = HorizontalAlign.Center;
					}
					formView.HorizontalAlign = HorizontalAlign.Center;
					formView.Width = new Unit(80.0, UnitType.Percentage);
				}
			}
			return previewControl;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00023FFC File Offset: 0x000221FC
		protected override void Initialize(DataRow schemeData)
		{
			this.foreColor = BaseAutoFormat<FormView>.GetStringProperty("ForeColor", schemeData);
			this.backColor = BaseAutoFormat<FormView>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<FormView>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<FormView>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<FormView>.GetIntProperty("BorderStyle", -1, schemeData);
			this.cellSpacing = BaseAutoFormat<FormView>.GetIntProperty("CellSpacing", schemeData);
			this.cellPadding = BaseAutoFormat<FormView>.GetIntProperty("CellPadding", -1, schemeData);
			this.gridLines = BaseAutoFormat<FormView>.GetIntProperty("GridLines", -1, schemeData);
			this.rowForeColor = BaseAutoFormat<FormView>.GetStringProperty("RowForeColor", schemeData);
			this.rowBackColor = BaseAutoFormat<FormView>.GetStringProperty("RowBackColor", schemeData);
			this.itemFont = BaseAutoFormat<FormView>.GetIntProperty("RowFont", schemeData);
			this.editRowForeColor = BaseAutoFormat<FormView>.GetStringProperty("EditRowForeColor", schemeData);
			this.editRowBackColor = BaseAutoFormat<FormView>.GetStringProperty("EditRowBackColor", schemeData);
			this.editRowFont = BaseAutoFormat<FormView>.GetIntProperty("EditRowFont", schemeData);
			this.headerForeColor = BaseAutoFormat<FormView>.GetStringProperty("HeaderForeColor", schemeData);
			this.headerBackColor = BaseAutoFormat<FormView>.GetStringProperty("HeaderBackColor", schemeData);
			this.headerFont = BaseAutoFormat<FormView>.GetIntProperty("HeaderFont", schemeData);
			this.footerForeColor = BaseAutoFormat<FormView>.GetStringProperty("FooterForeColor", schemeData);
			this.footerBackColor = BaseAutoFormat<FormView>.GetStringProperty("FooterBackColor", schemeData);
			this.footerFont = BaseAutoFormat<FormView>.GetIntProperty("FooterFont", schemeData);
			this.pagerForeColor = BaseAutoFormat<FormView>.GetStringProperty("PagerForeColor", schemeData);
			this.pagerBackColor = BaseAutoFormat<FormView>.GetStringProperty("PagerBackColor", schemeData);
			this.pagerFont = BaseAutoFormat<FormView>.GetIntProperty("PagerFont", schemeData);
			this.pagerAlign = BaseAutoFormat<FormView>.GetIntProperty("PagerAlign", schemeData);
			this.pagerButtons = BaseAutoFormat<FormView>.GetIntProperty("PagerButtons", 1, schemeData);
			this.renderOuterTable = BaseAutoFormat<FormView>.GetStringProperty("RenderOuterTable", schemeData);
		}

		// Token: 0x040003E1 RID: 993
		private string headerForeColor;

		// Token: 0x040003E2 RID: 994
		private string headerBackColor;

		// Token: 0x040003E3 RID: 995
		private int headerFont;

		// Token: 0x040003E4 RID: 996
		private string footerForeColor;

		// Token: 0x040003E5 RID: 997
		private string footerBackColor;

		// Token: 0x040003E6 RID: 998
		private int footerFont;

		// Token: 0x040003E7 RID: 999
		private string borderColor;

		// Token: 0x040003E8 RID: 1000
		private string borderWidth;

		// Token: 0x040003E9 RID: 1001
		private int borderStyle = -1;

		// Token: 0x040003EA RID: 1002
		private int gridLines = -1;

		// Token: 0x040003EB RID: 1003
		private int cellSpacing;

		// Token: 0x040003EC RID: 1004
		private int cellPadding = -1;

		// Token: 0x040003ED RID: 1005
		private string foreColor;

		// Token: 0x040003EE RID: 1006
		private string backColor;

		// Token: 0x040003EF RID: 1007
		private string rowForeColor;

		// Token: 0x040003F0 RID: 1008
		private string rowBackColor;

		// Token: 0x040003F1 RID: 1009
		private int itemFont;

		// Token: 0x040003F2 RID: 1010
		private string editRowForeColor;

		// Token: 0x040003F3 RID: 1011
		private string editRowBackColor;

		// Token: 0x040003F4 RID: 1012
		private int editRowFont;

		// Token: 0x040003F5 RID: 1013
		private string pagerForeColor;

		// Token: 0x040003F6 RID: 1014
		private string pagerBackColor;

		// Token: 0x040003F7 RID: 1015
		private int pagerFont;

		// Token: 0x040003F8 RID: 1016
		private int pagerAlign;

		// Token: 0x040003F9 RID: 1017
		private int pagerButtons;

		// Token: 0x040003FA RID: 1018
		private string renderOuterTable;

		// Token: 0x040003FB RID: 1019
		private const int FONT_BOLD = 1;

		// Token: 0x040003FC RID: 1020
		private const int FONT_ITALIC = 2;
	}
}
