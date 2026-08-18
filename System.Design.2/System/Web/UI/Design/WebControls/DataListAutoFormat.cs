using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000BD RID: 189
	internal sealed class DataListAutoFormat : BaseAutoFormat<DataList>
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		public DataListAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		protected override void Apply(DataList list)
		{
			list.HeaderStyle.ForeColor = ColorTranslator.FromHtml(this.headerForeColor);
			list.HeaderStyle.BackColor = ColorTranslator.FromHtml(this.headerBackColor);
			list.HeaderStyle.Font.Bold = ((this.headerFont & 1) != 0);
			list.HeaderStyle.Font.Italic = ((this.headerFont & 2) != 0);
			list.HeaderStyle.Font.ClearDefaults();
			list.FooterStyle.ForeColor = ColorTranslator.FromHtml(this.footerForeColor);
			list.FooterStyle.BackColor = ColorTranslator.FromHtml(this.footerBackColor);
			list.FooterStyle.Font.Bold = ((this.footerFont & 1) != 0);
			list.FooterStyle.Font.Italic = ((this.footerFont & 2) != 0);
			list.FooterStyle.Font.ClearDefaults();
			list.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			switch (this.gridLines)
			{
			case 0:
				list.GridLines = GridLines.None;
				break;
			case 1:
				list.GridLines = GridLines.Horizontal;
				break;
			case 2:
				list.GridLines = GridLines.Vertical;
				break;
			case 3:
				list.GridLines = GridLines.Both;
				break;
			default:
				list.GridLines = GridLines.None;
				break;
			}
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				list.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				list.BorderStyle = BorderStyle.NotSet;
			}
			list.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			list.CellPadding = this.cellPadding;
			list.CellSpacing = this.cellSpacing;
			list.ForeColor = ColorTranslator.FromHtml(this.foreColor);
			list.BackColor = ColorTranslator.FromHtml(this.backColor);
			list.ItemStyle.ForeColor = ColorTranslator.FromHtml(this.itemForeColor);
			list.ItemStyle.BackColor = ColorTranslator.FromHtml(this.itemBackColor);
			list.ItemStyle.Font.Bold = ((this.itemFont & 1) != 0);
			list.ItemStyle.Font.Italic = ((this.itemFont & 2) != 0);
			list.ItemStyle.Font.ClearDefaults();
			list.AlternatingItemStyle.ForeColor = ColorTranslator.FromHtml(this.alternatingItemForeColor);
			list.AlternatingItemStyle.BackColor = ColorTranslator.FromHtml(this.alternatingItemBackColor);
			list.AlternatingItemStyle.Font.Bold = ((this.alternatingItemFont & 1) != 0);
			list.AlternatingItemStyle.Font.Italic = ((this.alternatingItemFont & 2) != 0);
			list.AlternatingItemStyle.Font.ClearDefaults();
			list.SelectedItemStyle.ForeColor = ColorTranslator.FromHtml(this.selectedItemForeColor);
			list.SelectedItemStyle.BackColor = ColorTranslator.FromHtml(this.selectedItemBackColor);
			list.SelectedItemStyle.Font.Bold = ((this.selectedItemFont & 1) != 0);
			list.SelectedItemStyle.Font.Italic = ((this.selectedItemFont & 2) != 0);
			list.SelectedItemStyle.Font.ClearDefaults();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00020218 File Offset: 0x0001E418
		public override Control GetPreviewControl(Control runtimeControl)
		{
			Control previewControl = base.GetPreviewControl(runtimeControl);
			if (previewControl != null)
			{
				IDesignerHost designerHost = (IDesignerHost)runtimeControl.Site.GetService(typeof(IDesignerHost));
				DataList dataList = previewControl as DataList;
				if (dataList != null && designerHost != null)
				{
					TemplateBuilder templateBuilder = dataList.ItemTemplate as TemplateBuilder;
					if ((templateBuilder != null && templateBuilder.Text.Length == 0) || dataList.ItemTemplate == null)
					{
						string templateText = "####";
						dataList.ItemTemplate = ControlParser.ParseTemplate(designerHost, templateText);
						dataList.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
					}
					dataList.HorizontalAlign = HorizontalAlign.Center;
					dataList.Width = new Unit(80.0, UnitType.Percentage);
				}
			}
			return previewControl;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000202C0 File Offset: 0x0001E4C0
		protected override void Initialize(DataRow schemeData)
		{
			this.foreColor = BaseAutoFormat<DataList>.GetStringProperty("ForeColor", schemeData);
			this.backColor = BaseAutoFormat<DataList>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<DataList>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<DataList>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<DataList>.GetIntProperty("BorderStyle", -1, schemeData);
			this.cellSpacing = BaseAutoFormat<DataList>.GetIntProperty("CellSpacing", schemeData);
			this.cellPadding = BaseAutoFormat<DataList>.GetIntProperty("CellPadding", -1, schemeData);
			this.gridLines = BaseAutoFormat<DataList>.GetIntProperty("GridLines", -1, schemeData);
			this.itemForeColor = BaseAutoFormat<DataList>.GetStringProperty("ItemForeColor", schemeData);
			this.itemBackColor = BaseAutoFormat<DataList>.GetStringProperty("ItemBackColor", schemeData);
			this.itemFont = BaseAutoFormat<DataList>.GetIntProperty("ItemFont", schemeData);
			this.alternatingItemForeColor = BaseAutoFormat<DataList>.GetStringProperty("AltItemForeColor", schemeData);
			this.alternatingItemBackColor = BaseAutoFormat<DataList>.GetStringProperty("AltItemBackColor", schemeData);
			this.alternatingItemFont = BaseAutoFormat<DataList>.GetIntProperty("AltItemFont", schemeData);
			this.selectedItemForeColor = BaseAutoFormat<DataList>.GetStringProperty("SelItemForeColor", schemeData);
			this.selectedItemBackColor = BaseAutoFormat<DataList>.GetStringProperty("SelItemBackColor", schemeData);
			this.selectedItemFont = BaseAutoFormat<DataList>.GetIntProperty("SelItemFont", schemeData);
			this.headerForeColor = BaseAutoFormat<DataList>.GetStringProperty("HeaderForeColor", schemeData);
			this.headerBackColor = BaseAutoFormat<DataList>.GetStringProperty("HeaderBackColor", schemeData);
			this.headerFont = BaseAutoFormat<DataList>.GetIntProperty("HeaderFont", schemeData);
			this.footerForeColor = BaseAutoFormat<DataList>.GetStringProperty("FooterForeColor", schemeData);
			this.footerBackColor = BaseAutoFormat<DataList>.GetStringProperty("FooterBackColor", schemeData);
			this.footerFont = BaseAutoFormat<DataList>.GetIntProperty("FooterFont", schemeData);
		}

		// Token: 0x04000357 RID: 855
		private string headerForeColor;

		// Token: 0x04000358 RID: 856
		private string headerBackColor;

		// Token: 0x04000359 RID: 857
		private int headerFont;

		// Token: 0x0400035A RID: 858
		private string footerForeColor;

		// Token: 0x0400035B RID: 859
		private string footerBackColor;

		// Token: 0x0400035C RID: 860
		private int footerFont;

		// Token: 0x0400035D RID: 861
		private string borderColor;

		// Token: 0x0400035E RID: 862
		private string borderWidth;

		// Token: 0x0400035F RID: 863
		private int borderStyle = -1;

		// Token: 0x04000360 RID: 864
		private int gridLines = -1;

		// Token: 0x04000361 RID: 865
		private int cellSpacing;

		// Token: 0x04000362 RID: 866
		private int cellPadding = -1;

		// Token: 0x04000363 RID: 867
		private string foreColor;

		// Token: 0x04000364 RID: 868
		private string backColor;

		// Token: 0x04000365 RID: 869
		private string itemForeColor;

		// Token: 0x04000366 RID: 870
		private string itemBackColor;

		// Token: 0x04000367 RID: 871
		private int itemFont;

		// Token: 0x04000368 RID: 872
		private string alternatingItemForeColor;

		// Token: 0x04000369 RID: 873
		private string alternatingItemBackColor;

		// Token: 0x0400036A RID: 874
		private int alternatingItemFont;

		// Token: 0x0400036B RID: 875
		private string selectedItemForeColor;

		// Token: 0x0400036C RID: 876
		private string selectedItemBackColor;

		// Token: 0x0400036D RID: 877
		private int selectedItemFont;

		// Token: 0x0400036E RID: 878
		private const int FONT_BOLD = 1;

		// Token: 0x0400036F RID: 879
		private const int FONT_ITALIC = 2;
	}
}
