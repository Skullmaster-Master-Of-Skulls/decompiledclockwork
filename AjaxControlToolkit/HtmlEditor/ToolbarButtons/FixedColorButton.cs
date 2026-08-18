using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000104 RID: 260
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.FixedColorButton", "HtmlEditor.ToolbarButtons.FixedColorButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class FixedColorButton : DesignModeBoxButton
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001393D File Offset: 0x00011B3D
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00013945 File Offset: 0x00011B45
		protected MethodButton MethodButton
		{
			get
			{
				return this._methodButton;
			}
			set
			{
				this._methodButton = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001394E File Offset: 0x00011B4E
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00013956 File Offset: 0x00011B56
		protected DesignModeBoxButton ColorDiv
		{
			get
			{
				return this._colorDiv;
			}
			set
			{
				this._colorDiv = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001395F File Offset: 0x00011B5F
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00013967 File Offset: 0x00011B67
		[ClientPropertyName("defaultColor")]
		[DefaultValue("#000000")]
		[ExtenderControlProperty]
		[Category("Behavior")]
		public string DefaultColor
		{
			get
			{
				return this._defaultColor;
			}
			set
			{
				this._defaultColor = value;
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00013970 File Offset: 0x00011B70
		protected override void CreateChildControls()
		{
			Table table = new Table();
			table.Attributes.Add("border", "0");
			table.Attributes.Add("cellspacing", "0");
			table.Attributes.Add("cellpadding", "0");
			table.Style[HtmlTextWriterStyle.Margin] = "1px";
			table.Style[HtmlTextWriterStyle.Padding] = "0";
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			table.Rows.Add(tableRow);
			tableRow.Cells.Add(tableCell);
			if (this.MethodButton != null)
			{
				tableCell.Controls.Add(this.MethodButton);
			}
			tableRow = new TableRow();
			tableCell = new TableCell();
			table.Rows.Add(tableRow);
			tableRow.Cells.Add(tableCell);
			this.ColorDiv = new DesignModeBoxButton();
			this.ColorDiv.CssClass = string.Empty;
			this.ColorDiv.Style[HtmlTextWriterStyle.Margin] = "0";
			this.ColorDiv.Style[HtmlTextWriterStyle.Padding] = "0";
			this.ColorDiv.Width = new Unit(21.0, UnitType.Pixel);
			this.ColorDiv.Height = new Unit(5.0, UnitType.Pixel);
			this.ColorDiv.Style["background-color"] = this.DefaultColor;
			this.ColorDiv.Style["font-size"] = "1px";
			tableCell.Controls.Add(this.ColorDiv);
			base.Content.Add(table);
			base.CreateChildControls();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00013B20 File Offset: 0x00011D20
		protected override void OnPreRender(EventArgs e)
		{
			this.ColorDiv.ToolTip = this.ToolTip;
			if (this.MethodButton != null)
			{
				this.MethodButton.ToolTip = this.ToolTip;
			}
			base.OnPreRender(e);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00013B53 File Offset: 0x00011D53
		internal override void CreateChilds(DesignerWithMapPath designer)
		{
			if (this.MethodButton != null)
			{
				this.MethodButton._designer = designer;
			}
			base.Content.Clear();
			base.CreateChilds(designer);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00013B7B File Offset: 0x00011D7B
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddComponentProperty("colorDiv", this.ColorDiv.ClientID);
			if (this.MethodButton != null)
			{
				descriptor.AddComponentProperty("methodButton", this.MethodButton.ClientID);
			}
		}

		// Token: 0x0400030B RID: 779
		private MethodButton _methodButton;

		// Token: 0x0400030C RID: 780
		private DesignModeBoxButton _colorDiv;

		// Token: 0x0400030D RID: 781
		private string _defaultColor = "#000000";
	}
}
