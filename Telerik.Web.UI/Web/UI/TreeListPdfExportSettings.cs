using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.UI
{
	// Token: 0x02001276 RID: 4726
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListPdfExportSettings : StateManager, IDisposable
	{
		// Token: 0x0600C4F6 RID: 50422 RVA: 0x002C0400 File Offset: 0x002BE600
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
			((IStateManager)this.ExpandCollapseCellStyle).TrackViewState();
		}

		// Token: 0x0600C4F7 RID: 50423 RVA: 0x002C0450 File Offset: 0x002BE650
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
				((IStateManager)this.ExpandCollapseCellStyle).LoadViewState(array[num++]);
			}
		}

		// Token: 0x0600C4F8 RID: 50424 RVA: 0x002C04C0 File Offset: 0x002BE6C0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ItemStyle).SaveViewState(),
				((IStateManager)this.AlternatingItemStyle).SaveViewState(),
				((IStateManager)this.HeaderStyle).SaveViewState(),
				((IStateManager)this.ExpandCollapseCellStyle).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x17003F89 RID: 16265
		// (get) Token: 0x0600C4F9 RID: 50425 RVA: 0x002C0538 File Offset: 0x002BE738
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListPdfStyle ItemStyle
		{
			get
			{
				if (this._pdfItemStyle == null)
				{
					this._pdfItemStyle = new TreeListPdfStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pdfItemStyle).TrackViewState();
					}
				}
				return this._pdfItemStyle;
			}
		}

		// Token: 0x17003F8A RID: 16266
		// (get) Token: 0x0600C4FA RID: 50426 RVA: 0x002C0566 File Offset: 0x002BE766
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		public TreeListPdfStyle AlternatingItemStyle
		{
			get
			{
				if (this._pdfAlternatingItemStyle == null)
				{
					this._pdfAlternatingItemStyle = new TreeListPdfStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pdfAlternatingItemStyle).TrackViewState();
					}
				}
				return this._pdfAlternatingItemStyle;
			}
		}

		// Token: 0x17003F8B RID: 16267
		// (get) Token: 0x0600C4FB RID: 50427 RVA: 0x002C0594 File Offset: 0x002BE794
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Style")]
		public TreeListPdfStyle HeaderStyle
		{
			get
			{
				if (this._pdfHeaderStyle == null)
				{
					this._pdfHeaderStyle = new TreeListPdfStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pdfHeaderStyle).TrackViewState();
					}
				}
				return this._pdfHeaderStyle;
			}
		}

		// Token: 0x17003F8C RID: 16268
		// (get) Token: 0x0600C4FC RID: 50428 RVA: 0x002C05C2 File Offset: 0x002BE7C2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListPdfExpandCollapseCellStyle ExpandCollapseCellStyle
		{
			get
			{
				if (this._pdfExpandCollapseCellStyle == null)
				{
					this._pdfExpandCollapseCellStyle = new TreeListPdfExpandCollapseCellStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pdfExpandCollapseCellStyle).TrackViewState();
					}
				}
				return this._pdfExpandCollapseCellStyle;
			}
		}

		// Token: 0x17003F8D RID: 16269
		// (get) Token: 0x0600C4FD RID: 50429 RVA: 0x002C05F0 File Offset: 0x002BE7F0
		// (set) Token: 0x0600C4FE RID: 50430 RVA: 0x002C0619 File Offset: 0x002BE819
		[DefaultValue(false)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003F8E RID: 16270
		// (get) Token: 0x0600C4FF RID: 50431 RVA: 0x002C0631 File Offset: 0x002BE831
		// (set) Token: 0x0600C500 RID: 50432 RVA: 0x002C065C File Offset: 0x002BE85C
		[Description("PDF paper size. Can be overriden by setting PageWidth and PageHeight explicitly.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(PaperKind.Letter)]
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
				this._defaultPageHeight = Unit.Empty;
				this._defaultPageWidth = Unit.Empty;
			}
		}

		// Token: 0x17003F8F RID: 16271
		// (get) Token: 0x0600C501 RID: 50433 RVA: 0x002C068A File Offset: 0x002BE88A
		// (set) Token: 0x0600C502 RID: 50434 RVA: 0x002C06AA File Offset: 0x002BE8AA
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("Determines the default font")]
		[DefaultValue("")]
		public string DefaultFontFamily
		{
			get
			{
				return (base.ViewState["DefaultFontFamily"] as string) ?? "";
			}
			set
			{
				base.ViewState["DefaultFontFamily"] = value;
			}
		}

		// Token: 0x17003F90 RID: 16272
		// (get) Token: 0x0600C503 RID: 50435 RVA: 0x002C06BD File Offset: 0x002BE8BD
		// (set) Token: 0x0600C504 RID: 50436 RVA: 0x002C06EC File Offset: 0x002BE8EC
		[NotifyParentProperty(true)]
		[Description("Top page margin size")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit PageTopMargin
		{
			get
			{
				if (base.ViewState["TopMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["TopMargin"];
			}
			set
			{
				base.ViewState["TopMargin"] = value;
			}
		}

		// Token: 0x17003F91 RID: 16273
		// (get) Token: 0x0600C505 RID: 50437 RVA: 0x002C0704 File Offset: 0x002BE904
		// (set) Token: 0x0600C506 RID: 50438 RVA: 0x002C0733 File Offset: 0x002BE933
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("Bottom page margin size")]
		public Unit PageBottomMargin
		{
			get
			{
				if (base.ViewState["BottomMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["BottomMargin"];
			}
			set
			{
				base.ViewState["BottomMargin"] = value;
			}
		}

		// Token: 0x17003F92 RID: 16274
		// (get) Token: 0x0600C507 RID: 50439 RVA: 0x002C074B File Offset: 0x002BE94B
		// (set) Token: 0x0600C508 RID: 50440 RVA: 0x002C077A File Offset: 0x002BE97A
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		[Description("Left page margin size")]
		public Unit PageLeftMargin
		{
			get
			{
				if (base.ViewState["LeftMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["LeftMargin"];
			}
			set
			{
				base.ViewState["LeftMargin"] = value;
			}
		}

		// Token: 0x17003F93 RID: 16275
		// (get) Token: 0x0600C509 RID: 50441 RVA: 0x002C0792 File Offset: 0x002BE992
		// (set) Token: 0x0600C50A RID: 50442 RVA: 0x002C07C1 File Offset: 0x002BE9C1
		[NotifyParentProperty(true)]
		[Description("Right page margin size")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit PageRightMargin
		{
			get
			{
				if (base.ViewState["RightMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["RightMargin"];
			}
			set
			{
				base.ViewState["RightMargin"] = value;
			}
		}

		// Token: 0x17003F94 RID: 16276
		// (get) Token: 0x0600C50B RID: 50443 RVA: 0x002C07D9 File Offset: 0x002BE9D9
		// (set) Token: 0x0600C50C RID: 50444 RVA: 0x002C0808 File Offset: 0x002BEA08
		[Description("Page header margin size")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit PageHeaderMargin
		{
			get
			{
				if (base.ViewState["HeaderMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["HeaderMargin"];
			}
			set
			{
				base.ViewState["HeaderMargin"] = value;
			}
		}

		// Token: 0x17003F95 RID: 16277
		// (get) Token: 0x0600C50D RID: 50445 RVA: 0x002C0820 File Offset: 0x002BEA20
		// (set) Token: 0x0600C50E RID: 50446 RVA: 0x002C084F File Offset: 0x002BEA4F
		[Description("Page footer margin size")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		public Unit PageFooterMargin
		{
			get
			{
				if (base.ViewState["FooterMargin"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["FooterMargin"];
			}
			set
			{
				base.ViewState["FooterMargin"] = value;
			}
		}

		// Token: 0x17003F96 RID: 16278
		// (get) Token: 0x0600C50F RID: 50447 RVA: 0x002C0867 File Offset: 0x002BEA67
		// (set) Token: 0x0600C510 RID: 50448 RVA: 0x002C0887 File Offset: 0x002BEA87
		[DefaultValue("")]
		[Description("Page title contents will be displayed in the page header")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		public string PageTitle
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

		// Token: 0x17003F97 RID: 16279
		// (get) Token: 0x0600C511 RID: 50449 RVA: 0x002C089A File Offset: 0x002BEA9A
		// (set) Token: 0x0600C512 RID: 50450 RVA: 0x002C08BA File Offset: 0x002BEABA
		[NotifyParentProperty(true)]
		[Description("Setting a value for this property will enable password protection.")]
		[Category("Misc")]
		[DefaultValue("")]
		public string UserPassword
		{
			get
			{
				return (base.ViewState["UserPassword"] as string) ?? "";
			}
			set
			{
				base.ViewState["UserPassword"] = value;
			}
		}

		// Token: 0x17003F98 RID: 16280
		// (get) Token: 0x0600C513 RID: 50451 RVA: 0x002C08CD File Offset: 0x002BEACD
		// (set) Token: 0x0600C514 RID: 50452 RVA: 0x002C08F8 File Offset: 0x002BEAF8
		[Description("Determines whether to embed, link or subset the fonts, used in the PDF document")]
		[NotifyParentProperty(true)]
		[DefaultValue(FontType.Subset)]
		public FontType FontType
		{
			get
			{
				if (base.ViewState["FontType"] == null)
				{
					return FontType.Subset;
				}
				return (FontType)base.ViewState["FontType"];
			}
			set
			{
				base.ViewState["FontType"] = value;
			}
		}

		// Token: 0x17003F99 RID: 16281
		// (get) Token: 0x0600C515 RID: 50453 RVA: 0x002C0910 File Offset: 0x002BEB10
		// (set) Token: 0x0600C516 RID: 50454 RVA: 0x002C0961 File Offset: 0x002BEB61
		[NotifyParentProperty(true)]
		[Description("Determines the page width of the exported PDF file. Will override the PaperSize property, if used")]
		public Unit PageWidth
		{
			get
			{
				object obj = base.ViewState["PageWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				if (this._defaultPageWidth == Unit.Empty)
				{
					this._defaultPageWidth = TreeListPdfExportSettings.GetPaperWidth(this.PaperSize);
				}
				return this._defaultPageWidth;
			}
			set
			{
				base.ViewState["PageWidth"] = value;
			}
		}

		// Token: 0x17003F9A RID: 16282
		// (get) Token: 0x0600C517 RID: 50455 RVA: 0x002C097C File Offset: 0x002BEB7C
		// (set) Token: 0x0600C518 RID: 50456 RVA: 0x002C09CD File Offset: 0x002BEBCD
		[Description("Determines the page height of the exported PDF file. Will override the PaperSize property, if used")]
		[NotifyParentProperty(true)]
		public Unit PageHeight
		{
			get
			{
				object obj = base.ViewState["PageHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				if (this._defaultPageHeight == Unit.Empty)
				{
					this._defaultPageHeight = TreeListPdfExportSettings.GetPaperHeight(this.PaperSize);
				}
				return this._defaultPageHeight;
			}
			set
			{
				base.ViewState["PageHeight"] = value;
			}
		}

		// Token: 0x17003F9B RID: 16283
		// (get) Token: 0x0600C519 RID: 50457 RVA: 0x002C09E5 File Offset: 0x002BEBE5
		// (set) Token: 0x0600C51A RID: 50458 RVA: 0x002C0A10 File Offset: 0x002BEC10
		[NotifyParentProperty(true)]
		[Category("Misc")]
		[DefaultValue(false)]
		[Description("Allow adding new content to the PDF file")]
		public bool AllowAdd
		{
			get
			{
				return base.ViewState["AllowAdd"] != null && (bool)base.ViewState["AllowAdd"];
			}
			set
			{
				base.ViewState["AllowAdd"] = value;
			}
		}

		// Token: 0x17003F9C RID: 16284
		// (get) Token: 0x0600C51B RID: 50459 RVA: 0x002C0A28 File Offset: 0x002BEC28
		// (set) Token: 0x0600C51C RID: 50460 RVA: 0x002C0A53 File Offset: 0x002BEC53
		[Description("Allow copying PDF content to the clipboard")]
		[Category("Misc")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowCopy
		{
			get
			{
				return base.ViewState["AllowCopy"] != null && (bool)base.ViewState["AllowCopy"];
			}
			set
			{
				base.ViewState["AllowCopy"] = value;
			}
		}

		// Token: 0x17003F9D RID: 16285
		// (get) Token: 0x0600C51D RID: 50461 RVA: 0x002C0A6B File Offset: 0x002BEC6B
		// (set) Token: 0x0600C51E RID: 50462 RVA: 0x002C0A96 File Offset: 0x002BEC96
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Allow printing the contents of the PDF document")]
		[Category("Misc")]
		public bool AllowPrinting
		{
			get
			{
				return base.ViewState["AllowPrinting"] == null || (bool)base.ViewState["AllowPrinting"];
			}
			set
			{
				base.ViewState["AllowPrinting"] = value;
			}
		}

		// Token: 0x17003F9E RID: 16286
		// (get) Token: 0x0600C51F RID: 50463 RVA: 0x002C0AAE File Offset: 0x002BECAE
		// (set) Token: 0x0600C520 RID: 50464 RVA: 0x002C0AD9 File Offset: 0x002BECD9
		[Description("Allow modifying the PDF contents")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Misc")]
		public bool AllowModify
		{
			get
			{
				return base.ViewState["AllowModify"] != null && (bool)base.ViewState["AllowModify"];
			}
			set
			{
				base.ViewState["AllowModify"] = value;
			}
		}

		// Token: 0x17003F9F RID: 16287
		// (get) Token: 0x0600C521 RID: 50465 RVA: 0x002C0AF1 File Offset: 0x002BECF1
		// (set) Token: 0x0600C522 RID: 50466 RVA: 0x002C0B11 File Offset: 0x002BED11
		[Description("Document creator")]
		[Category("Misc")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Creator
		{
			get
			{
				return (base.ViewState["Creator"] as string) ?? "";
			}
			set
			{
				base.ViewState["Creator"] = value;
			}
		}

		// Token: 0x17003FA0 RID: 16288
		// (get) Token: 0x0600C523 RID: 50467 RVA: 0x002C0B24 File Offset: 0x002BED24
		// (set) Token: 0x0600C524 RID: 50468 RVA: 0x002C0B44 File Offset: 0x002BED44
		[NotifyParentProperty(true)]
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Document producer")]
		public string Producer
		{
			get
			{
				return (base.ViewState["Producer"] as string) ?? "";
			}
			set
			{
				base.ViewState["Producer"] = value;
			}
		}

		// Token: 0x17003FA1 RID: 16289
		// (get) Token: 0x0600C525 RID: 50469 RVA: 0x002C0B57 File Offset: 0x002BED57
		// (set) Token: 0x0600C526 RID: 50470 RVA: 0x002C0B77 File Offset: 0x002BED77
		[Description("Document author")]
		[Category("Misc")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Author
		{
			get
			{
				return (base.ViewState["Author"] as string) ?? "";
			}
			set
			{
				base.ViewState["Author"] = value;
			}
		}

		// Token: 0x17003FA2 RID: 16290
		// (get) Token: 0x0600C527 RID: 50471 RVA: 0x002C0B8A File Offset: 0x002BED8A
		// (set) Token: 0x0600C528 RID: 50472 RVA: 0x002C0BAA File Offset: 0x002BEDAA
		[NotifyParentProperty(true)]
		[Description("Document title")]
		[Category("Misc")]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return (base.ViewState["Title"] as string) ?? "";
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x17003FA3 RID: 16291
		// (get) Token: 0x0600C529 RID: 50473 RVA: 0x002C0BBD File Offset: 0x002BEDBD
		// (set) Token: 0x0600C52A RID: 50474 RVA: 0x002C0BDD File Offset: 0x002BEDDD
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Document subject")]
		[Category("Misc")]
		public string Subject
		{
			get
			{
				return (base.ViewState["Subject"] as string) ?? "";
			}
			set
			{
				base.ViewState["Subject"] = value;
			}
		}

		// Token: 0x17003FA4 RID: 16292
		// (get) Token: 0x0600C52B RID: 50475 RVA: 0x002C0BF0 File Offset: 0x002BEDF0
		// (set) Token: 0x0600C52C RID: 50476 RVA: 0x002C0C20 File Offset: 0x002BEE20
		[Description("PDF document keywords")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[TypeConverter(typeof(StringArrayConverter))]
		[Category("Misc")]
		public string[] Keywords
		{
			get
			{
				if (base.ViewState["Keywords"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["Keywords"];
			}
			set
			{
				base.ViewState["Keywords"] = value;
			}
		}

		// Token: 0x0600C52D RID: 50477 RVA: 0x002C0C88 File Offset: 0x002BEE88
		private static Pair GetPaperKindDimensions(PaperKind paperKind)
		{
			XDocument xdocument = XDocument.Parse(TreeListExporter.GetEmbeddedResource("Telerik.Web.UI.Grid.Resources.PaperFormats.xml"));
			XElement xelement = xdocument.Element("papers").Elements("paper").Single((XElement x) => x.Attribute("id").Value == paperKind.ToString());
			if (xelement == null)
			{
				xelement = xdocument.Element("papers").Elements("paper").Single((XElement x) => x.Attribute("id").Value == "Letter");
			}
			Unit unit = Unit.Parse(xelement.Attribute("width").Value, CultureInfo.InvariantCulture);
			Unit unit2 = Unit.Parse(xelement.Attribute("height").Value, CultureInfo.InvariantCulture);
			return new Pair(unit, unit2);
		}

		// Token: 0x0600C52E RID: 50478 RVA: 0x002C0D7C File Offset: 0x002BEF7C
		private static Unit GetPaperWidth(PaperKind paperKind)
		{
			Pair paperKindDimensions = TreeListPdfExportSettings.GetPaperKindDimensions(paperKind);
			return (Unit)paperKindDimensions.First;
		}

		// Token: 0x0600C52F RID: 50479 RVA: 0x002C0D9C File Offset: 0x002BEF9C
		private static Unit GetPaperHeight(PaperKind paperKind)
		{
			Pair paperKindDimensions = TreeListPdfExportSettings.GetPaperKindDimensions(paperKind);
			return (Unit)paperKindDimensions.Second;
		}

		// Token: 0x0600C530 RID: 50480 RVA: 0x002C0DBB File Offset: 0x002BEFBB
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600C531 RID: 50481 RVA: 0x002C0DC4 File Offset: 0x002BEFC4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._pdfAlternatingItemStyle != null)
				{
					this._pdfAlternatingItemStyle.Dispose();
				}
				if (this._pdfExpandCollapseCellStyle != null)
				{
					this._pdfExpandCollapseCellStyle.Dispose();
				}
				if (this._pdfHeaderStyle != null)
				{
					this._pdfHeaderStyle.Dispose();
				}
				if (this._pdfItemStyle != null)
				{
					this._pdfItemStyle.Dispose();
				}
			}
		}

		// Token: 0x0400341A RID: 13338
		private TreeListPdfStyle _pdfItemStyle;

		// Token: 0x0400341B RID: 13339
		private TreeListPdfStyle _pdfAlternatingItemStyle;

		// Token: 0x0400341C RID: 13340
		private TreeListPdfStyle _pdfHeaderStyle;

		// Token: 0x0400341D RID: 13341
		private TreeListPdfExpandCollapseCellStyle _pdfExpandCollapseCellStyle;

		// Token: 0x0400341E RID: 13342
		private Unit _defaultPageWidth = Unit.Empty;

		// Token: 0x0400341F RID: 13343
		private Unit _defaultPageHeight = Unit.Empty;
	}
}
