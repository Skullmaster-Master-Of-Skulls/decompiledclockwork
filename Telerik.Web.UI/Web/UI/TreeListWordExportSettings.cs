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
	// Token: 0x02000960 RID: 2400
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListWordExportSettings : StateManager, IDisposable
	{
		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06005B4C RID: 23372 RVA: 0x00115C30 File Offset: 0x00113E30
		// (set) Token: 0x06005B4D RID: 23373 RVA: 0x00115C50 File Offset: 0x00113E50
		[Description("Page Footer contents")]
		[Category("Data")]
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

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06005B4E RID: 23374 RVA: 0x00115C63 File Offset: 0x00113E63
		// (set) Token: 0x06005B4F RID: 23375 RVA: 0x00115C83 File Offset: 0x00113E83
		[Category("Data")]
		[Description("Page Footer contents")]
		[DefaultValue("")]
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

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x00115C96 File Offset: 0x00113E96
		// (set) Token: 0x06005B51 RID: 23377 RVA: 0x00115CC1 File Offset: 0x00113EC1
		[Category("Data")]
		[DefaultValue(true)]
		[Description("Show gridlines")]
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

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x06005B52 RID: 23378 RVA: 0x00115CD9 File Offset: 0x00113ED9
		// (set) Token: 0x06005B53 RID: 23379 RVA: 0x00115D12 File Offset: 0x00113F12
		[Description("Top margin of the page")]
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "0.75in")]
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

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06005B54 RID: 23380 RVA: 0x00115D2A File Offset: 0x00113F2A
		// (set) Token: 0x06005B55 RID: 23381 RVA: 0x00115D63 File Offset: 0x00113F63
		[Category("Appearance")]
		[Description("Bottom margin of the page")]
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

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x00115D7B File Offset: 0x00113F7B
		// (set) Token: 0x06005B57 RID: 23383 RVA: 0x00115DB4 File Offset: 0x00113FB4
		[Description("Left margin of the page")]
		[Category("Appearance")]
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

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x00115DCC File Offset: 0x00113FCC
		// (set) Token: 0x06005B59 RID: 23385 RVA: 0x00115E05 File Offset: 0x00114005
		[DefaultValue(typeof(Unit), "0.7in")]
		[Category("Appearance")]
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

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x00115E20 File Offset: 0x00114020
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x00115E49 File Offset: 0x00114049
		[DefaultValue(false)]
		[Description("This will swap the values of the PageWidth and PageHeight properties.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x00115E61 File Offset: 0x00114061
		// (set) Token: 0x06005B5D RID: 23389 RVA: 0x00115E8C File Offset: 0x0011408C
		[DefaultValue(PaperKind.Letter)]
		[NotifyParentProperty(true)]
		[Description("Word paper size. Custom Paper Size is not supported.")]
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

		// Token: 0x06005B5E RID: 23390 RVA: 0x00115EFC File Offset: 0x001140FC
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

		// Token: 0x06005B5F RID: 23391 RVA: 0x00116020 File Offset: 0x00114220
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

		// Token: 0x06005B60 RID: 23392 RVA: 0x0011607C File Offset: 0x0011427C
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

		// Token: 0x06005B61 RID: 23393 RVA: 0x001160FC File Offset: 0x001142FC
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

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x00116186 File Offset: 0x00114386
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListWordStyle ItemStyle
		{
			get
			{
				if (this._wordItemStyle == null)
				{
					this._wordItemStyle = new TreeListWordStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._wordItemStyle).TrackViewState();
					}
				}
				return this._wordItemStyle;
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x001161B4 File Offset: 0x001143B4
		[Category("Style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListWordStyle AlternatingItemStyle
		{
			get
			{
				if (this._wordAlternatingItemStyle == null)
				{
					this._wordAlternatingItemStyle = new TreeListWordStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._wordAlternatingItemStyle).TrackViewState();
					}
				}
				return this._wordAlternatingItemStyle;
			}
		}

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x06005B64 RID: 23396 RVA: 0x001161E2 File Offset: 0x001143E2
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public TreeListWordStyle HeaderStyle
		{
			get
			{
				if (this._wordHeaderStyle == null)
				{
					this._wordHeaderStyle = new TreeListWordStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._wordHeaderStyle).TrackViewState();
					}
				}
				return this._wordHeaderStyle;
			}
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x00116210 File Offset: 0x00114410
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		public TreeListWordStyle FooterItemStyle
		{
			get
			{
				if (this._wordFooterStyle == null)
				{
					this._wordFooterStyle = new TreeListWordStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._wordFooterStyle).TrackViewState();
					}
				}
				return this._wordFooterStyle;
			}
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0011623E File Offset: 0x0011443E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListWordExpandCollapseCellStyle ExpandCollapseCellStyle
		{
			get
			{
				if (this._wordExpandCollapseCellStyle == null)
				{
					this._wordExpandCollapseCellStyle = new TreeListWordExpandCollapseCellStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._wordExpandCollapseCellStyle).TrackViewState();
					}
				}
				return this._wordExpandCollapseCellStyle;
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0011626C File Offset: 0x0011446C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x00116278 File Offset: 0x00114478
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._wordAlternatingItemStyle != null)
				{
					this._wordAlternatingItemStyle.Dispose();
				}
				if (this._wordItemStyle != null)
				{
					this._wordItemStyle.Dispose();
				}
				if (this._wordHeaderStyle != null)
				{
					this._wordHeaderStyle.Dispose();
				}
				if (this._wordFooterStyle != null)
				{
					this._wordFooterStyle.Dispose();
				}
				if (this._wordExpandCollapseCellStyle != null)
				{
					this._wordExpandCollapseCellStyle.Dispose();
				}
			}
		}

		// Token: 0x040015F7 RID: 5623
		private TreeListWordStyle _wordItemStyle;

		// Token: 0x040015F8 RID: 5624
		private TreeListWordStyle _wordAlternatingItemStyle;

		// Token: 0x040015F9 RID: 5625
		private TreeListWordStyle _wordHeaderStyle;

		// Token: 0x040015FA RID: 5626
		private TreeListWordStyle _wordFooterStyle;

		// Token: 0x040015FB RID: 5627
		private TreeListWordExpandCollapseCellStyle _wordExpandCollapseCellStyle;
	}
}
