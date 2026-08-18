using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.UI
{
	// Token: 0x0200115E RID: 4446
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridPdfSettings : ObjectWithState
	{
		// Token: 0x0600B532 RID: 46386 RVA: 0x0027EEB5 File Offset: 0x0027D0B5
		public GridPdfSettings(StateBag OwnerStateBag) : base("gpdfs_", OwnerStateBag)
		{
		}

		// Token: 0x17003A8D RID: 14989
		// (get) Token: 0x0600B533 RID: 46387 RVA: 0x0027EEC3 File Offset: 0x0027D0C3
		[Category("Pdf")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridPdfPageHeaderFooter PageHeader
		{
			get
			{
				if (this.pageHeader == null)
				{
					this.pageHeader = new GridPdfPageHeaderFooter("header", base.OwnerViewState);
				}
				return this.pageHeader;
			}
		}

		// Token: 0x17003A8E RID: 14990
		// (get) Token: 0x0600B534 RID: 46388 RVA: 0x0027EEE9 File Offset: 0x0027D0E9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Pdf")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridPdfPageHeaderFooter PageFooter
		{
			get
			{
				if (this.pageFooter == null)
				{
					this.pageFooter = new GridPdfPageHeaderFooter("footer", base.OwnerViewState);
				}
				return this.pageFooter;
			}
		}

		// Token: 0x17003A8F RID: 14991
		// (get) Token: 0x0600B535 RID: 46389 RVA: 0x0027EF0F File Offset: 0x0027D10F
		// (set) Token: 0x0600B536 RID: 46390 RVA: 0x0027EF3E File Offset: 0x0027D13E
		[DefaultValue("")]
		[Description("Document creator")]
		[NotifyParentProperty(true)]
		public string Creator
		{
			get
			{
				if (base.ViewState["_cr"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_cr"];
			}
			set
			{
				base.ViewState["_cr"] = value;
			}
		}

		// Token: 0x17003A90 RID: 14992
		// (get) Token: 0x0600B537 RID: 46391 RVA: 0x0027EF51 File Offset: 0x0027D151
		// (set) Token: 0x0600B538 RID: 46392 RVA: 0x0027EF80 File Offset: 0x0027D180
		[DefaultValue("")]
		[Description("Document producer")]
		[NotifyParentProperty(true)]
		public string Producer
		{
			get
			{
				if (base.ViewState["_pd"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_pd"];
			}
			set
			{
				base.ViewState["_pd"] = value;
			}
		}

		// Token: 0x17003A91 RID: 14993
		// (get) Token: 0x0600B539 RID: 46393 RVA: 0x0027EF93 File Offset: 0x0027D193
		// (set) Token: 0x0600B53A RID: 46394 RVA: 0x0027EFC2 File Offset: 0x0027D1C2
		[Description("Document author")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Author
		{
			get
			{
				if (base.ViewState["_at"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_at"];
			}
			set
			{
				base.ViewState["_at"] = value;
			}
		}

		// Token: 0x17003A92 RID: 14994
		// (get) Token: 0x0600B53B RID: 46395 RVA: 0x0027EFD5 File Offset: 0x0027D1D5
		// (set) Token: 0x0600B53C RID: 46396 RVA: 0x0027F004 File Offset: 0x0027D204
		[Description("Document title")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Title
		{
			get
			{
				if (base.ViewState["_ti"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_ti"];
			}
			set
			{
				base.ViewState["_ti"] = value;
			}
		}

		// Token: 0x17003A93 RID: 14995
		// (get) Token: 0x0600B53D RID: 46397 RVA: 0x0027F017 File Offset: 0x0027D217
		// (set) Token: 0x0600B53E RID: 46398 RVA: 0x0027F046 File Offset: 0x0027D246
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Document subject")]
		public string Subject
		{
			get
			{
				if (base.ViewState["_su"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_su"];
			}
			set
			{
				base.ViewState["_su"] = value;
			}
		}

		// Token: 0x17003A94 RID: 14996
		// (get) Token: 0x0600B53F RID: 46399 RVA: 0x0027F059 File Offset: 0x0027D259
		// (set) Token: 0x0600B540 RID: 46400 RVA: 0x0027F088 File Offset: 0x0027D288
		[DefaultValue("")]
		[Description("Page title")]
		[NotifyParentProperty(true)]
		public string PageTitle
		{
			get
			{
				if (base.ViewState["_pt"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_pt"];
			}
			set
			{
				base.ViewState["_pt"] = value;
			}
		}

		// Token: 0x17003A95 RID: 14997
		// (get) Token: 0x0600B541 RID: 46401 RVA: 0x0027F09C File Offset: 0x0027D29C
		// (set) Token: 0x0600B542 RID: 46402 RVA: 0x0027F0D4 File Offset: 0x0027D2D4
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[DefaultValue(null)]
		[Description("Comma delimited list of keywords")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string[] Keywords
		{
			get
			{
				object obj = base.ViewState["_kw"];
				if (obj != null)
				{
					return (string[])((string[])obj).Clone();
				}
				return new string[0];
			}
			set
			{
				base.ViewState["_kw"] = (string[])value.Clone();
			}
		}

		// Token: 0x17003A96 RID: 14998
		// (get) Token: 0x0600B543 RID: 46403 RVA: 0x0027F0F1 File Offset: 0x0027D2F1
		// (set) Token: 0x0600B544 RID: 46404 RVA: 0x0027F11C File Offset: 0x0027D31C
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Determines whether the content encryption will be disabled")]
		public bool DisableContentEncryption
		{
			get
			{
				return base.ViewState["ContentEncryption"] != null && (bool)base.ViewState["ContentEncryption"];
			}
			set
			{
				base.ViewState["ContentEncryption"] = value;
			}
		}

		// Token: 0x17003A97 RID: 14999
		// (get) Token: 0x0600B545 RID: 46405 RVA: 0x0027F134 File Offset: 0x0027D334
		// (set) Token: 0x0600B546 RID: 46406 RVA: 0x0027F15F File Offset: 0x0027D35F
		[DefaultValue(false)]
		[Description("Allow content to be added to the PDF file")]
		[NotifyParentProperty(true)]
		public bool AllowAdd
		{
			get
			{
				return base.ViewState["_aa"] != null && (bool)base.ViewState["_aa"];
			}
			set
			{
				base.ViewState["_aa"] = value;
			}
		}

		// Token: 0x17003A98 RID: 15000
		// (get) Token: 0x0600B547 RID: 46407 RVA: 0x0027F177 File Offset: 0x0027D377
		// (set) Token: 0x0600B548 RID: 46408 RVA: 0x0027F1A2 File Offset: 0x0027D3A2
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Allow content to be copied from the PDF file")]
		public bool AllowCopy
		{
			get
			{
				return base.ViewState["_ac"] != null && (bool)base.ViewState["_ac"];
			}
			set
			{
				base.ViewState["_ac"] = value;
			}
		}

		// Token: 0x17003A99 RID: 15001
		// (get) Token: 0x0600B549 RID: 46409 RVA: 0x0027F1BA File Offset: 0x0027D3BA
		// (set) Token: 0x0600B54A RID: 46410 RVA: 0x0027F1E5 File Offset: 0x0027D3E5
		[Description("Allow the content of the PDF file to be printed")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool AllowPrinting
		{
			get
			{
				return base.ViewState["_ap"] == null || (bool)base.ViewState["_ap"];
			}
			set
			{
				base.ViewState["_ap"] = value;
			}
		}

		// Token: 0x17003A9A RID: 15002
		// (get) Token: 0x0600B54B RID: 46411 RVA: 0x0027F1FD File Offset: 0x0027D3FD
		// (set) Token: 0x0600B54C RID: 46412 RVA: 0x0027F228 File Offset: 0x0027D428
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Allow the document to be modified")]
		public bool AllowModify
		{
			get
			{
				return base.ViewState["_am"] != null && (bool)base.ViewState["_am"];
			}
			set
			{
				base.ViewState["_am"] = value;
			}
		}

		// Token: 0x17003A9B RID: 15003
		// (get) Token: 0x0600B54D RID: 46413 RVA: 0x0027F240 File Offset: 0x0027D440
		// (set) Token: 0x0600B54E RID: 46414 RVA: 0x0027F26B File Offset: 0x0027D46B
		[DefaultValue(false)]
		[Description("Determines what will happen when a given text is larger than the cell width (and there are no whitespaces inside). ")]
		[NotifyParentProperty(true)]
		public bool ForceTextWrap
		{
			get
			{
				return base.ViewState["ForceTextWrap"] != null && (bool)base.ViewState["ForceTextWrap"];
			}
			set
			{
				base.ViewState["ForceTextWrap"] = value;
			}
		}

		// Token: 0x17003A9C RID: 15004
		// (get) Token: 0x0600B54F RID: 46415 RVA: 0x0027F283 File Offset: 0x0027D483
		// (set) Token: 0x0600B550 RID: 46416 RVA: 0x0027F2AE File Offset: 0x0027D4AE
		[DefaultValue(GridPaperSize.Letter)]
		[NotifyParentProperty(true)]
		[Description("The physical paper size that RadGrid will use when exporting to PDF. It will be overriden by setting PageWidth and PageHeight explicitly.")]
		public GridPaperSize PaperSize
		{
			get
			{
				if (base.ViewState["_ps"] == null)
				{
					return GridPaperSize.Letter;
				}
				return (GridPaperSize)base.ViewState["_ps"];
			}
			set
			{
				base.ViewState["_ps"] = value;
			}
		}

		// Token: 0x17003A9D RID: 15005
		// (get) Token: 0x0600B551 RID: 46417 RVA: 0x0027F2C6 File Offset: 0x0027D4C6
		// (set) Token: 0x0600B552 RID: 46418 RVA: 0x0027F2F1 File Offset: 0x0027D4F1
		[Description("Determines the default content filter used by the PDF engine")]
		[DefaultValue(GridPdfFilter.NoFilter)]
		[NotifyParentProperty(true)]
		public GridPdfFilter ContentFilter
		{
			get
			{
				if (base.ViewState["ContentFilter"] == null)
				{
					return GridPdfFilter.NoFilter;
				}
				return (GridPdfFilter)base.ViewState["ContentFilter"];
			}
			set
			{
				base.ViewState["ContentFilter"] = value;
			}
		}

		// Token: 0x17003A9E RID: 15006
		// (get) Token: 0x0600B553 RID: 46419 RVA: 0x0027F309 File Offset: 0x0027D509
		// (set) Token: 0x0600B554 RID: 46420 RVA: 0x0027F334 File Offset: 0x0027D534
		[Description("Determines the border type for the exported RadGrid")]
		[DefaultValue(GridPdfSettings.GridPdfBorderType.Separate)]
		[NotifyParentProperty(true)]
		public GridPdfSettings.GridPdfBorderType BorderType
		{
			get
			{
				if (base.ViewState["BorderType"] == null)
				{
					return GridPdfSettings.GridPdfBorderType.Separate;
				}
				return (GridPdfSettings.GridPdfBorderType)base.ViewState["BorderType"];
			}
			set
			{
				base.ViewState["BorderType"] = value;
			}
		}

		// Token: 0x17003A9F RID: 15007
		// (get) Token: 0x0600B555 RID: 46421 RVA: 0x0027F34C File Offset: 0x0027D54C
		// (set) Token: 0x0600B556 RID: 46422 RVA: 0x0027F377 File Offset: 0x0027D577
		[Description("Determines the thickness of the border")]
		[DefaultValue(GridPdfSettings.GridPdfBorderStyle.Medium)]
		[NotifyParentProperty(true)]
		public GridPdfSettings.GridPdfBorderStyle BorderStyle
		{
			get
			{
				if (base.ViewState["BorderStyle"] == null)
				{
					return GridPdfSettings.GridPdfBorderStyle.Medium;
				}
				return (GridPdfSettings.GridPdfBorderStyle)base.ViewState["BorderStyle"];
			}
			set
			{
				base.ViewState["BorderStyle"] = value;
			}
		}

		// Token: 0x17003AA0 RID: 15008
		// (get) Token: 0x0600B557 RID: 46423 RVA: 0x0027F38F File Offset: 0x0027D58F
		// (set) Token: 0x0600B558 RID: 46424 RVA: 0x0027F3BE File Offset: 0x0027D5BE
		[Description("Determines the color of the borders")]
		[DefaultValue(typeof(Color), "Black")]
		[NotifyParentProperty(true)]
		public Color BorderColor
		{
			get
			{
				if (base.ViewState["BorderColor"] == null)
				{
					return Color.Black;
				}
				return (Color)base.ViewState["BorderColor"];
			}
			set
			{
				base.ViewState["BorderColor"] = value;
			}
		}

		// Token: 0x17003AA1 RID: 15009
		// (get) Token: 0x0600B559 RID: 46425 RVA: 0x0027F3D8 File Offset: 0x0027D5D8
		// (set) Token: 0x0600B55A RID: 46426 RVA: 0x0027F416 File Offset: 0x0027D616
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "8.5in")]
		[Description("")]
		public Unit PageWidth
		{
			get
			{
				object obj = base.ViewState["_pw"];
				if (obj == null)
				{
					return (Unit)this.GetSizeForPaper(this.PaperSize).First;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["_pw"] = value;
			}
		}

		// Token: 0x17003AA2 RID: 15010
		// (get) Token: 0x0600B55B RID: 46427 RVA: 0x0027F430 File Offset: 0x0027D630
		// (set) Token: 0x0600B55C RID: 46428 RVA: 0x0027F46E File Offset: 0x0027D66E
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(typeof(Unit), "11in")]
		public Unit PageHeight
		{
			get
			{
				object obj = base.ViewState["_ph"];
				if (obj == null)
				{
					return (Unit)this.GetSizeForPaper(this.PaperSize).Second;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["_ph"] = value;
			}
		}

		// Token: 0x0600B55D RID: 46429 RVA: 0x0027F488 File Offset: 0x0027D688
		private Pair GetSizeForPaper(GridPaperSize paperSize)
		{
			switch (paperSize)
			{
			case GridPaperSize.Letter:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(11.0, UnitType.Inch));
			case GridPaperSize.Legal:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(14.0, UnitType.Inch));
			case GridPaperSize.Executive:
				return new Pair(new Unit(7.25, UnitType.Inch), new Unit(10.5, UnitType.Inch));
			case GridPaperSize.A4:
				return new Pair(new Unit(210.0, UnitType.Mm), new Unit(297.0, UnitType.Mm));
			case GridPaperSize.A5:
				return new Pair(new Unit(148.0, UnitType.Mm), new Unit(210.0, UnitType.Mm));
			case GridPaperSize.JIS_B5:
				return new Pair(new Unit(182.0, UnitType.Mm), new Unit(257.0, UnitType.Mm));
			case GridPaperSize.US_Folio:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(13.0, UnitType.Inch));
			case GridPaperSize.No_10_Envelope:
				return new Pair(new Unit(4.12, UnitType.Inch), new Unit(9.5, UnitType.Inch));
			case GridPaperSize.DL_Envelope:
				return new Pair(new Unit(110.0, UnitType.Mm), new Unit(220.0, UnitType.Mm));
			case GridPaperSize.C5_Envelope:
				return new Pair(new Unit(162.0, UnitType.Mm), new Unit(229.0, UnitType.Mm));
			case GridPaperSize.C6_Envelope:
				return new Pair(new Unit(114.0, UnitType.Mm), new Unit(162.0, UnitType.Mm));
			case GridPaperSize.ISO_B5:
				return new Pair(new Unit(176.0, UnitType.Mm), new Unit(250.0, UnitType.Mm));
			case GridPaperSize.Monarch_Envelope:
				return new Pair(new Unit(3.87, UnitType.Inch), new Unit(7.5, UnitType.Inch));
			case GridPaperSize.A6:
				return new Pair(new Unit(105.0, UnitType.Mm), new Unit(148.0, UnitType.Mm));
			case GridPaperSize.Oficio:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(13.5, UnitType.Inch));
			default:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(11.0, UnitType.Inch));
			}
		}

		// Token: 0x17003AA3 RID: 15011
		// (get) Token: 0x0600B55E RID: 46430 RVA: 0x0027F7BD File Offset: 0x0027D9BD
		// (set) Token: 0x0600B55F RID: 46431 RVA: 0x0027F7EC File Offset: 0x0027D9EC
		[DefaultValue(typeof(Unit), "")]
		[Description("The top margin of the page")]
		[NotifyParentProperty(true)]
		public Unit PageTopMargin
		{
			get
			{
				if (base.ViewState["_ptm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_ptm"];
			}
			set
			{
				base.ViewState["_ptm"] = value;
			}
		}

		// Token: 0x17003AA4 RID: 15012
		// (get) Token: 0x0600B560 RID: 46432 RVA: 0x0027F804 File Offset: 0x0027DA04
		// (set) Token: 0x0600B561 RID: 46433 RVA: 0x0027F833 File Offset: 0x0027DA33
		[Description("The bottom margin of the page")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit PageBottomMargin
		{
			get
			{
				if (base.ViewState["_pbm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_pbm"];
			}
			set
			{
				base.ViewState["_pbm"] = value;
			}
		}

		// Token: 0x17003AA5 RID: 15013
		// (get) Token: 0x0600B562 RID: 46434 RVA: 0x0027F84B File Offset: 0x0027DA4B
		// (set) Token: 0x0600B563 RID: 46435 RVA: 0x0027F87A File Offset: 0x0027DA7A
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("The left margin of the page")]
		public Unit PageLeftMargin
		{
			get
			{
				if (base.ViewState["_plm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_plm"];
			}
			set
			{
				base.ViewState["_plm"] = value;
			}
		}

		// Token: 0x17003AA6 RID: 15014
		// (get) Token: 0x0600B564 RID: 46436 RVA: 0x0027F892 File Offset: 0x0027DA92
		// (set) Token: 0x0600B565 RID: 46437 RVA: 0x0027F8C1 File Offset: 0x0027DAC1
		[NotifyParentProperty(true)]
		[Description("The right margin of the page")]
		[DefaultValue(typeof(Unit), "")]
		public Unit PageRightMargin
		{
			get
			{
				if (base.ViewState["_prm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_prm"];
			}
			set
			{
				base.ViewState["_prm"] = value;
			}
		}

		// Token: 0x17003AA7 RID: 15015
		// (get) Token: 0x0600B566 RID: 46438 RVA: 0x0027F8D9 File Offset: 0x0027DAD9
		// (set) Token: 0x0600B567 RID: 46439 RVA: 0x0027F908 File Offset: 0x0027DB08
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("The margin of the page header")]
		public Unit PageHeaderMargin
		{
			get
			{
				if (base.ViewState["_phm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_phm"];
			}
			set
			{
				base.ViewState["_phm"] = value;
			}
		}

		// Token: 0x17003AA8 RID: 15016
		// (get) Token: 0x0600B568 RID: 46440 RVA: 0x0027F920 File Offset: 0x0027DB20
		// (set) Token: 0x0600B569 RID: 46441 RVA: 0x0027F94F File Offset: 0x0027DB4F
		[DefaultValue(typeof(Unit), "")]
		[Description("The margin of the page footer")]
		[NotifyParentProperty(true)]
		public Unit PageFooterMargin
		{
			get
			{
				if (base.ViewState["_pfm"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["_pfm"];
			}
			set
			{
				base.ViewState["_pfm"] = value;
			}
		}

		// Token: 0x17003AA9 RID: 15017
		// (get) Token: 0x0600B56A RID: 46442 RVA: 0x0027F967 File Offset: 0x0027DB67
		// (set) Token: 0x0600B56B RID: 46443 RVA: 0x0027F992 File Offset: 0x0027DB92
		[DefaultValue(FontType.Subset)]
		[Description("Enumeration that dictates how Apoc should treat fonts when producing a Pdf document.")]
		[NotifyParentProperty(true)]
		public FontType FontType
		{
			get
			{
				if (base.ViewState["_ft"] == null)
				{
					return FontType.Subset;
				}
				return (FontType)base.ViewState["_ft"];
			}
			set
			{
				base.ViewState["_ft"] = value;
			}
		}

		// Token: 0x17003AAA RID: 15018
		// (get) Token: 0x0600B56C RID: 46444 RVA: 0x0027F9AA File Offset: 0x0027DBAA
		// (set) Token: 0x0600B56D RID: 46445 RVA: 0x0027F9D9 File Offset: 0x0027DBD9
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("If you set a password, the exported document will be password protected.")]
		public string UserPassword
		{
			get
			{
				if (base.ViewState["UserPassword"] == null)
				{
					return "";
				}
				return (string)base.ViewState["UserPassword"];
			}
			set
			{
				base.ViewState["UserPassword"] = value;
			}
		}

		// Token: 0x17003AAB RID: 15019
		// (get) Token: 0x0600B56E RID: 46446 RVA: 0x0027F9EC File Offset: 0x0027DBEC
		// (set) Token: 0x0600B56F RID: 46447 RVA: 0x0027FA1B File Offset: 0x0027DC1B
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Determines the default font.")]
		public string DefaultFontFamily
		{
			get
			{
				if (base.ViewState["DefaultFontFamily"] == null)
				{
					return "";
				}
				return (string)base.ViewState["DefaultFontFamily"];
			}
			set
			{
				base.ViewState["DefaultFontFamily"] = value;
			}
		}

		// Token: 0x04002FD8 RID: 12248
		private GridPdfPageHeaderFooter pageHeader;

		// Token: 0x04002FD9 RID: 12249
		private GridPdfPageHeaderFooter pageFooter;

		// Token: 0x0200115F RID: 4447
		public enum GridPdfBorderType
		{
			// Token: 0x04002FDB RID: 12251
			Separate,
			// Token: 0x04002FDC RID: 12252
			NoBorder,
			// Token: 0x04002FDD RID: 12253
			OuterBorders,
			// Token: 0x04002FDE RID: 12254
			TopAndBottom,
			// Token: 0x04002FDF RID: 12255
			AllBorders
		}

		// Token: 0x02001160 RID: 4448
		public enum GridPdfBorderStyle
		{
			// Token: 0x04002FE1 RID: 12257
			Medium,
			// Token: 0x04002FE2 RID: 12258
			Thick,
			// Token: 0x04002FE3 RID: 12259
			Thin
		}
	}
}
