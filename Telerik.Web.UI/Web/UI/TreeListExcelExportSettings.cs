using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x02001224 RID: 4644
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListExcelExportSettings : StateManager, IDisposable
	{
		// Token: 0x17003DBF RID: 15807
		// (get) Token: 0x0600BF83 RID: 49027 RVA: 0x002A78EA File Offset: 0x002A5AEA
		// (set) Token: 0x0600BF84 RID: 49028 RVA: 0x002A7915 File Offset: 0x002A5B15
		[Description("Set Excel export format")]
		[Category("Data")]
		[DefaultValue(TreeListExcelFormat.Biff)]
		public TreeListExcelFormat Format
		{
			get
			{
				if (base.ViewState["Format"] == null)
				{
					return TreeListExcelFormat.Biff;
				}
				return (TreeListExcelFormat)base.ViewState["Format"];
			}
			set
			{
				base.ViewState["Format"] = value;
			}
		}

		// Token: 0x17003DC0 RID: 15808
		// (get) Token: 0x0600BF85 RID: 49029 RVA: 0x002A792D File Offset: 0x002A5B2D
		// (set) Token: 0x0600BF86 RID: 49030 RVA: 0x002A794D File Offset: 0x002A5B4D
		[Category("Data")]
		[Description("Page Footer contents")]
		[DefaultValue("")]
		public string PageFooter
		{
			get
			{
				return (base.ViewState["PageFooter"] as string) ?? "";
			}
			set
			{
				base.ViewState["PageFooter"] = value;
			}
		}

		// Token: 0x17003DC1 RID: 15809
		// (get) Token: 0x0600BF87 RID: 49031 RVA: 0x002A7960 File Offset: 0x002A5B60
		// (set) Token: 0x0600BF88 RID: 49032 RVA: 0x002A7980 File Offset: 0x002A5B80
		[DefaultValue("")]
		[Description("Page Footer contents")]
		[Category("Data")]
		public string PageHeader
		{
			get
			{
				return (base.ViewState["PageHeader"] as string) ?? "";
			}
			set
			{
				base.ViewState["PageHeader"] = value;
			}
		}

		// Token: 0x17003DC2 RID: 15810
		// (get) Token: 0x0600BF89 RID: 49033 RVA: 0x002A7993 File Offset: 0x002A5B93
		// (set) Token: 0x0600BF8A RID: 49034 RVA: 0x002A79B3 File Offset: 0x002A5BB3
		[DefaultValue("Sheet1")]
		[Description("Worksheet name")]
		[Category("Data")]
		public string WorksheetName
		{
			get
			{
				return (base.ViewState["WorksheetName"] as string) ?? "Sheet1";
			}
			set
			{
				base.ViewState["WorksheetName"] = value;
			}
		}

		// Token: 0x17003DC3 RID: 15811
		// (get) Token: 0x0600BF8B RID: 49035 RVA: 0x002A79C6 File Offset: 0x002A5BC6
		// (set) Token: 0x0600BF8C RID: 49036 RVA: 0x002A79F1 File Offset: 0x002A5BF1
		[DefaultValue(true)]
		[Description("Show gridlines")]
		[Category("Data")]
		public bool ShowGridlines
		{
			get
			{
				return base.ViewState["ShowGridlines"] == null || (bool)base.ViewState["ShowGridlines"];
			}
			set
			{
				base.ViewState["ShowGridlines"] = value;
			}
		}

		// Token: 0x17003DC4 RID: 15812
		// (get) Token: 0x0600BF8D RID: 49037 RVA: 0x002A7A09 File Offset: 0x002A5C09
		// (set) Token: 0x0600BF8E RID: 49038 RVA: 0x002A7A42 File Offset: 0x002A5C42
		[DefaultValue(typeof(Unit), "0.75in")]
		[Category("Appearance")]
		[Description("Top margin of the page")]
		public Unit PageTopMargin
		{
			get
			{
				if (base.ViewState["PageTopMargin"] != null)
				{
					return (Unit)base.ViewState["PageTopMargin"];
				}
				return new Unit(0.75, UnitType.Inch);
			}
			set
			{
				base.ViewState["PageTopMargin"] = value;
			}
		}

		// Token: 0x17003DC5 RID: 15813
		// (get) Token: 0x0600BF8F RID: 49039 RVA: 0x002A7A5A File Offset: 0x002A5C5A
		// (set) Token: 0x0600BF90 RID: 49040 RVA: 0x002A7A93 File Offset: 0x002A5C93
		[Description("Bottom margin of the page")]
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "0.75in")]
		public Unit PageBottomMargin
		{
			get
			{
				if (base.ViewState["PageBottomMargin"] != null)
				{
					return (Unit)base.ViewState["PageBottomMargin"];
				}
				return new Unit(0.75, UnitType.Inch);
			}
			set
			{
				base.ViewState["PageBottomMargin"] = value;
			}
		}

		// Token: 0x17003DC6 RID: 15814
		// (get) Token: 0x0600BF91 RID: 49041 RVA: 0x002A7AAB File Offset: 0x002A5CAB
		// (set) Token: 0x0600BF92 RID: 49042 RVA: 0x002A7AE4 File Offset: 0x002A5CE4
		[Category("Appearance")]
		[Description("Left margin of the page")]
		[DefaultValue(typeof(Unit), "0.7in")]
		public Unit PageLeftMargin
		{
			get
			{
				if (base.ViewState["PageLeftMargin"] != null)
				{
					return (Unit)base.ViewState["PageLeftMargin"];
				}
				return new Unit(0.7, UnitType.Inch);
			}
			set
			{
				base.ViewState["PageLeftMargin"] = value;
			}
		}

		// Token: 0x17003DC7 RID: 15815
		// (get) Token: 0x0600BF93 RID: 49043 RVA: 0x002A7AFC File Offset: 0x002A5CFC
		// (set) Token: 0x0600BF94 RID: 49044 RVA: 0x002A7B35 File Offset: 0x002A5D35
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "0.7in")]
		[Description("Right margin of the page")]
		public Unit PageRightMargin
		{
			get
			{
				if (base.ViewState["PageRightMargin"] != null)
				{
					return (Unit)base.ViewState["PageRightMargin"];
				}
				return new Unit(0.7, UnitType.Inch);
			}
			set
			{
				base.ViewState["PageRightMargin"] = value;
			}
		}

		// Token: 0x17003DC8 RID: 15816
		// (get) Token: 0x0600BF95 RID: 49045 RVA: 0x002A7B50 File Offset: 0x002A5D50
		// (set) Token: 0x0600BF96 RID: 49046 RVA: 0x002A7B79 File Offset: 0x002A5D79
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		[Description("This will swap the values of the PageWidth and PageHeight properties.")]
		public bool RotatePaper
		{
			get
			{
				object obj = base.ViewState["RotatePaper"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["RotatePaper"] = value;
			}
		}

		// Token: 0x17003DC9 RID: 15817
		// (get) Token: 0x0600BF97 RID: 49047 RVA: 0x002A7B91 File Offset: 0x002A5D91
		// (set) Token: 0x0600BF98 RID: 49048 RVA: 0x002A7BBC File Offset: 0x002A5DBC
		[NotifyParentProperty(true)]
		[DefaultValue(PaperKind.Letter)]
		[Description("Excel paper size. Custom Paper Size is not supported.")]
		[Category("Layout")]
		public PaperKind PaperSize
		{
			get
			{
				if (base.ViewState["PaperSize"] == null)
				{
					return PaperKind.Letter;
				}
				return (PaperKind)base.ViewState["PaperSize"];
			}
			set
			{
				base.ViewState["PaperSize"] = value;
			}
		}

		// Token: 0x0600BF99 RID: 49049 RVA: 0x002A7C2C File Offset: 0x002A5E2C
		internal static SizeF GetPaperKindDimensions(PaperKind paperKind)
		{
			XDocument xdocument = XDocument.Parse(TreeListExporter.GetEmbeddedResource("Telerik.Web.UI.Grid.Resources.PaperFormats.xml"));
			XElement xelement = xdocument.Element("papers").Elements("paper").Single((XElement x) => x.Attribute("id").Value == paperKind.ToString());
			if (xelement == null)
			{
				xelement = xdocument.Element("papers").Elements("paper").Single((XElement x) => x.Attribute("id").Value == "Letter");
			}
			Unit unit = Unit.Parse(xelement.Attribute("width").Value, CultureInfo.InvariantCulture);
			Unit unit2 = Unit.Parse(xelement.Attribute("height").Value, CultureInfo.InvariantCulture);
			if (unit.Type == UnitType.Mm)
			{
				return new SizeF((float)unit.Value, (float)unit2.Value);
			}
			return new SizeF((float)unit.Value * 25.4f, (float)unit2.Value * 25.4f);
		}

		// Token: 0x0600BF9A RID: 49050 RVA: 0x002A7D50 File Offset: 0x002A5F50
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.ItemStyle).TrackViewState();
			((IStateManager)this.AlternatingItemStyle).TrackViewState();
			((IStateManager)this.HeaderStyle).TrackViewState();
			((IStateManager)this.FooterItemStyle).TrackViewState();
			((IStateManager)this.ExpandCollapseCellStyle).TrackViewState();
		}

		// Token: 0x0600BF9B RID: 49051 RVA: 0x002A7DAC File Offset: 0x002A5FAC
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.ItemStyle).LoadViewState(array[num++]);
				((IStateManager)this.AlternatingItemStyle).LoadViewState(array[num++]);
				((IStateManager)this.HeaderStyle).LoadViewState(array[num++]);
				((IStateManager)this.FooterItemStyle).LoadViewState(array[num++]);
				((IStateManager)this.ExpandCollapseCellStyle).LoadViewState(array[num++]);
			}
		}

		// Token: 0x0600BF9C RID: 49052 RVA: 0x002A7E2C File Offset: 0x002A602C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ItemStyle).SaveViewState(),
				((IStateManager)this.AlternatingItemStyle).SaveViewState(),
				((IStateManager)this.HeaderStyle).SaveViewState(),
				((IStateManager)this.FooterItemStyle).SaveViewState(),
				((IStateManager)this.ExpandCollapseCellStyle).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x17003DCA RID: 15818
		// (get) Token: 0x0600BF9D RID: 49053 RVA: 0x002A7EB6 File Offset: 0x002A60B6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListExcelStyle ItemStyle
		{
			get
			{
				if (this._excelItemStyle == null)
				{
					this._excelItemStyle = new TreeListExcelStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._excelItemStyle).TrackViewState();
					}
				}
				return this._excelItemStyle;
			}
		}

		// Token: 0x17003DCB RID: 15819
		// (get) Token: 0x0600BF9E RID: 49054 RVA: 0x002A7EE4 File Offset: 0x002A60E4
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListExcelStyle AlternatingItemStyle
		{
			get
			{
				if (this._excelAlternatingItemStyle == null)
				{
					this._excelAlternatingItemStyle = new TreeListExcelStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._excelAlternatingItemStyle).TrackViewState();
					}
				}
				return this._excelAlternatingItemStyle;
			}
		}

		// Token: 0x17003DCC RID: 15820
		// (get) Token: 0x0600BF9F RID: 49055 RVA: 0x002A7F12 File Offset: 0x002A6112
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListExcelStyle HeaderStyle
		{
			get
			{
				if (this._excelHeaderStyle == null)
				{
					this._excelHeaderStyle = new TreeListExcelStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._excelHeaderStyle).TrackViewState();
					}
				}
				return this._excelHeaderStyle;
			}
		}

		// Token: 0x17003DCD RID: 15821
		// (get) Token: 0x0600BFA0 RID: 49056 RVA: 0x002A7F40 File Offset: 0x002A6140
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Style")]
		public TreeListExcelStyle FooterItemStyle
		{
			get
			{
				if (this._excelFooterStyle == null)
				{
					this._excelFooterStyle = new TreeListExcelStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._excelFooterStyle).TrackViewState();
					}
				}
				return this._excelFooterStyle;
			}
		}

		// Token: 0x17003DCE RID: 15822
		// (get) Token: 0x0600BFA1 RID: 49057 RVA: 0x002A7F6E File Offset: 0x002A616E
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListExcelExpandCollapseCellStyle ExpandCollapseCellStyle
		{
			get
			{
				if (this._excelExpandCollapseCellStyle == null)
				{
					this._excelExpandCollapseCellStyle = new TreeListExcelExpandCollapseCellStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._excelExpandCollapseCellStyle).TrackViewState();
					}
				}
				return this._excelExpandCollapseCellStyle;
			}
		}

		// Token: 0x0600BFA2 RID: 49058 RVA: 0x002A7F9C File Offset: 0x002A619C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600BFA3 RID: 49059 RVA: 0x002A7FA8 File Offset: 0x002A61A8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._excelAlternatingItemStyle != null)
				{
					this._excelAlternatingItemStyle.Dispose();
				}
				if (this._excelItemStyle != null)
				{
					this._excelItemStyle.Dispose();
				}
				if (this._excelHeaderStyle != null)
				{
					this._excelHeaderStyle.Dispose();
				}
				if (this._excelFooterStyle != null)
				{
					this._excelFooterStyle.Dispose();
				}
				if (this._excelExpandCollapseCellStyle != null)
				{
					this._excelExpandCollapseCellStyle.Dispose();
				}
			}
		}

		// Token: 0x04003247 RID: 12871
		private TreeListExcelStyle _excelItemStyle;

		// Token: 0x04003248 RID: 12872
		private TreeListExcelStyle _excelAlternatingItemStyle;

		// Token: 0x04003249 RID: 12873
		private TreeListExcelStyle _excelHeaderStyle;

		// Token: 0x0400324A RID: 12874
		private TreeListExcelStyle _excelFooterStyle;

		// Token: 0x0400324B RID: 12875
		private TreeListExcelExpandCollapseCellStyle _excelExpandCollapseCellStyle;
	}
}
