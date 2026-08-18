using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000EDE RID: 3806
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SchedulerPdfSettings : ObjectWithState
	{
		// Token: 0x06009074 RID: 36980 RVA: 0x00208F52 File Offset: 0x00207152
		public SchedulerPdfSettings(StateBag OwnerStateBag) : base("spdfs_", OwnerStateBag)
		{
		}

		// Token: 0x17002DB6 RID: 11702
		// (get) Token: 0x06009075 RID: 36981 RVA: 0x00208F60 File Offset: 0x00207160
		// (set) Token: 0x06009076 RID: 36982 RVA: 0x00208F8F File Offset: 0x0020718F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("")]
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

		// Token: 0x17002DB7 RID: 11703
		// (get) Token: 0x06009077 RID: 36983 RVA: 0x00208FA2 File Offset: 0x002071A2
		// (set) Token: 0x06009078 RID: 36984 RVA: 0x00208FD1 File Offset: 0x002071D1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("")]
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

		// Token: 0x17002DB8 RID: 11704
		// (get) Token: 0x06009079 RID: 36985 RVA: 0x00208FE4 File Offset: 0x002071E4
		// (set) Token: 0x0600907A RID: 36986 RVA: 0x00209013 File Offset: 0x00207213
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("")]
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

		// Token: 0x17002DB9 RID: 11705
		// (get) Token: 0x0600907B RID: 36987 RVA: 0x00209026 File Offset: 0x00207226
		// (set) Token: 0x0600907C RID: 36988 RVA: 0x00209055 File Offset: 0x00207255
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("")]
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

		// Token: 0x17002DBA RID: 11706
		// (get) Token: 0x0600907D RID: 36989 RVA: 0x00209068 File Offset: 0x00207268
		// (set) Token: 0x0600907E RID: 36990 RVA: 0x00209097 File Offset: 0x00207297
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("")]
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

		// Token: 0x17002DBB RID: 11707
		// (get) Token: 0x0600907F RID: 36991 RVA: 0x002090AA File Offset: 0x002072AA
		// (set) Token: 0x06009080 RID: 36992 RVA: 0x002090D9 File Offset: 0x002072D9
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("")]
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

		// Token: 0x17002DBC RID: 11708
		// (get) Token: 0x06009081 RID: 36993 RVA: 0x002090EC File Offset: 0x002072EC
		// (set) Token: 0x06009082 RID: 36994 RVA: 0x00209124 File Offset: 0x00207324
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(SchedulerStringArrayConverter))]
		[DefaultValue(null)]
		[Description("Comma delimited list of keywords")]
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

		// Token: 0x17002DBD RID: 11709
		// (get) Token: 0x06009083 RID: 36995 RVA: 0x00209144 File Offset: 0x00207344
		// (set) Token: 0x06009084 RID: 36996 RVA: 0x0020917C File Offset: 0x0020737C
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(SchedulerStringArrayConverter))]
		[DefaultValue(null)]
		[Description("Comma delimited list of stylesheets that RadScheduler will use when exporting to PDF.")]
		public virtual string[] StyleSheets
		{
			get
			{
				object obj = base.ViewState["_sh"];
				if (obj != null)
				{
					return (string[])((string[])obj).Clone();
				}
				return new string[0];
			}
			set
			{
				base.ViewState["_sh"] = (string[])value.Clone();
			}
		}

		// Token: 0x17002DBE RID: 11710
		// (get) Token: 0x06009085 RID: 36997 RVA: 0x00209199 File Offset: 0x00207399
		// (set) Token: 0x06009086 RID: 36998 RVA: 0x002091C4 File Offset: 0x002073C4
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("")]
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

		// Token: 0x17002DBF RID: 11711
		// (get) Token: 0x06009087 RID: 36999 RVA: 0x002091DC File Offset: 0x002073DC
		// (set) Token: 0x06009088 RID: 37000 RVA: 0x00209207 File Offset: 0x00207407
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
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

		// Token: 0x17002DC0 RID: 11712
		// (get) Token: 0x06009089 RID: 37001 RVA: 0x0020921F File Offset: 0x0020741F
		// (set) Token: 0x0600908A RID: 37002 RVA: 0x0020924A File Offset: 0x0020744A
		[DefaultValue(true)]
		[Description("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002DC1 RID: 11713
		// (get) Token: 0x0600908B RID: 37003 RVA: 0x00209262 File Offset: 0x00207462
		// (set) Token: 0x0600908C RID: 37004 RVA: 0x0020928D File Offset: 0x0020748D
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("")]
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

		// Token: 0x17002DC2 RID: 11714
		// (get) Token: 0x0600908D RID: 37005 RVA: 0x002092A5 File Offset: 0x002074A5
		// (set) Token: 0x0600908E RID: 37006 RVA: 0x002092D0 File Offset: 0x002074D0
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
		public bool AllowPaging
		{
			get
			{
				return base.ViewState["_pg"] != null && (bool)base.ViewState["_pg"];
			}
			set
			{
				base.ViewState["_pg"] = value;
			}
		}

		// Token: 0x17002DC3 RID: 11715
		// (get) Token: 0x0600908F RID: 37007 RVA: 0x002092E8 File Offset: 0x002074E8
		// (set) Token: 0x06009090 RID: 37008 RVA: 0x00209313 File Offset: 0x00207513
		[DefaultValue(SchedulerPaperOrientation.Portrait)]
		[NotifyParentProperty(true)]
		[Description("The physical paper orientation that RadScheduler will use when exporting to PDF. It will be overridden by setting PageWidth and PageHeight explicitly.")]
		public SchedulerPaperOrientation PaperOrientation
		{
			get
			{
				if (base.ViewState["_po"] == null)
				{
					return SchedulerPaperOrientation.Portrait;
				}
				return (SchedulerPaperOrientation)base.ViewState["_po"];
			}
			set
			{
				base.ViewState["_po"] = value;
			}
		}

		// Token: 0x17002DC4 RID: 11716
		// (get) Token: 0x06009091 RID: 37009 RVA: 0x0020932B File Offset: 0x0020752B
		// (set) Token: 0x06009092 RID: 37010 RVA: 0x00209356 File Offset: 0x00207556
		[DefaultValue(SchedulerPaperSize.Letter)]
		[NotifyParentProperty(true)]
		[Description("The physical paper size that RadScheduler will use when exporting to PDF. It will be overridden by setting PageWidth and PageHeight explicitly.")]
		public SchedulerPaperSize PaperSize
		{
			get
			{
				if (base.ViewState["_ps"] == null)
				{
					return SchedulerPaperSize.Letter;
				}
				return (SchedulerPaperSize)base.ViewState["_ps"];
			}
			set
			{
				base.ViewState["_ps"] = value;
			}
		}

		// Token: 0x17002DC5 RID: 11717
		// (get) Token: 0x06009093 RID: 37011 RVA: 0x00209370 File Offset: 0x00207570
		// (set) Token: 0x06009094 RID: 37012 RVA: 0x002093C0 File Offset: 0x002075C0
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
					Pair sizeForPaper = this.GetSizeForPaper(this.PaperSize);
					return (Unit)((this.PaperOrientation == SchedulerPaperOrientation.Portrait) ? sizeForPaper.First : sizeForPaper.Second);
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["_pw"] = value;
			}
		}

		// Token: 0x17002DC6 RID: 11718
		// (get) Token: 0x06009095 RID: 37013 RVA: 0x002093D8 File Offset: 0x002075D8
		// (set) Token: 0x06009096 RID: 37014 RVA: 0x00209428 File Offset: 0x00207628
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "11in")]
		[Description("")]
		public Unit PageHeight
		{
			get
			{
				object obj = base.ViewState["_ph"];
				if (obj == null)
				{
					Pair sizeForPaper = this.GetSizeForPaper(this.PaperSize);
					return (Unit)((this.PaperOrientation == SchedulerPaperOrientation.Portrait) ? sizeForPaper.Second : sizeForPaper.First);
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["_ph"] = value;
			}
		}

		// Token: 0x06009097 RID: 37015 RVA: 0x00209440 File Offset: 0x00207640
		private Pair GetSizeForPaper(SchedulerPaperSize paperSize)
		{
			switch (paperSize)
			{
			case SchedulerPaperSize.Letter:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(11.0, UnitType.Inch));
			case SchedulerPaperSize.Legal:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(14.0, UnitType.Inch));
			case SchedulerPaperSize.Executive:
				return new Pair(new Unit(7.25, UnitType.Inch), new Unit(10.5, UnitType.Inch));
			case SchedulerPaperSize.A1:
				return new Pair(new Unit(594.0, UnitType.Mm), new Unit(841.0, UnitType.Mm));
			case SchedulerPaperSize.A2:
				return new Pair(new Unit(420.0, UnitType.Mm), new Unit(594.0, UnitType.Mm));
			case SchedulerPaperSize.A3:
				return new Pair(new Unit(297.0, UnitType.Mm), new Unit(420.0, UnitType.Mm));
			case SchedulerPaperSize.A4:
				return new Pair(new Unit(210.0, UnitType.Mm), new Unit(297.0, UnitType.Mm));
			case SchedulerPaperSize.A5:
				return new Pair(new Unit(148.0, UnitType.Mm), new Unit(210.0, UnitType.Mm));
			case SchedulerPaperSize.JIS_B5:
				return new Pair(new Unit(182.0, UnitType.Mm), new Unit(257.0, UnitType.Mm));
			case SchedulerPaperSize.US_Folio:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(13.0, UnitType.Inch));
			case SchedulerPaperSize.No_10_Envelope:
				return new Pair(new Unit(4.12, UnitType.Inch), new Unit(9.5, UnitType.Inch));
			case SchedulerPaperSize.DL_Envelope:
				return new Pair(new Unit(110.0, UnitType.Mm), new Unit(220.0, UnitType.Mm));
			case SchedulerPaperSize.C5_Envelope:
				return new Pair(new Unit(162.0, UnitType.Mm), new Unit(229.0, UnitType.Mm));
			case SchedulerPaperSize.C6_Envelope:
				return new Pair(new Unit(114.0, UnitType.Mm), new Unit(162.0, UnitType.Mm));
			case SchedulerPaperSize.ISO_B5:
				return new Pair(new Unit(176.0, UnitType.Mm), new Unit(250.0, UnitType.Mm));
			case SchedulerPaperSize.Monarch_Envelope:
				return new Pair(new Unit(3.87, UnitType.Inch), new Unit(7.5, UnitType.Inch));
			case SchedulerPaperSize.A6:
				return new Pair(new Unit(105.0, UnitType.Mm), new Unit(148.0, UnitType.Mm));
			case SchedulerPaperSize.Oficio:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(13.5, UnitType.Inch));
			default:
				return new Pair(new Unit(8.5, UnitType.Inch), new Unit(11.0, UnitType.Inch));
			}
		}

		// Token: 0x17002DC7 RID: 11719
		// (get) Token: 0x06009098 RID: 37016 RVA: 0x0020980C File Offset: 0x00207A0C
		// (set) Token: 0x06009099 RID: 37017 RVA: 0x00209874 File Offset: 0x00207A74
		[DefaultValue(typeof(Unit), "")]
		[Description("")]
		[NotifyParentProperty(true)]
		public Unit PageTopMargin
		{
			get
			{
				Unit result = (this.PageHeight.Type == UnitType.Mm) ? new Unit(25.4, UnitType.Mm) : new Unit(1.0, UnitType.Inch);
				if (base.ViewState["_ptm"] == null)
				{
					return result;
				}
				return (Unit)base.ViewState["_ptm"];
			}
			set
			{
				base.ViewState["_ptm"] = value;
			}
		}

		// Token: 0x17002DC8 RID: 11720
		// (get) Token: 0x0600909A RID: 37018 RVA: 0x0020988C File Offset: 0x00207A8C
		// (set) Token: 0x0600909B RID: 37019 RVA: 0x002098F4 File Offset: 0x00207AF4
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("")]
		public Unit PageBottomMargin
		{
			get
			{
				Unit result = (this.PageHeight.Type == UnitType.Mm) ? new Unit(25.4, UnitType.Mm) : new Unit(1.0, UnitType.Inch);
				if (base.ViewState["_pbm"] == null)
				{
					return result;
				}
				return (Unit)base.ViewState["_pbm"];
			}
			set
			{
				base.ViewState["_pbm"] = value;
			}
		}

		// Token: 0x17002DC9 RID: 11721
		// (get) Token: 0x0600909C RID: 37020 RVA: 0x0020990C File Offset: 0x00207B0C
		// (set) Token: 0x0600909D RID: 37021 RVA: 0x00209974 File Offset: 0x00207B74
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("")]
		public Unit PageLeftMargin
		{
			get
			{
				Unit result = (this.PageWidth.Type == UnitType.Mm) ? new Unit(25.4, UnitType.Mm) : new Unit(1.0, UnitType.Inch);
				if (base.ViewState["_plm"] == null)
				{
					return result;
				}
				return (Unit)base.ViewState["_plm"];
			}
			set
			{
				base.ViewState["_plm"] = value;
			}
		}

		// Token: 0x17002DCA RID: 11722
		// (get) Token: 0x0600909E RID: 37022 RVA: 0x0020998C File Offset: 0x00207B8C
		// (set) Token: 0x0600909F RID: 37023 RVA: 0x002099F4 File Offset: 0x00207BF4
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("")]
		public Unit PageRightMargin
		{
			get
			{
				Unit result = (this.PageWidth.Type == UnitType.Mm) ? new Unit(25.4, UnitType.Mm) : new Unit(1.0, UnitType.Inch);
				if (base.ViewState["_prm"] == null)
				{
					return result;
				}
				return (Unit)base.ViewState["_prm"];
			}
			set
			{
				base.ViewState["_prm"] = value;
			}
		}

		// Token: 0x17002DCB RID: 11723
		// (get) Token: 0x060090A0 RID: 37024 RVA: 0x00209A0C File Offset: 0x00207C0C
		// (set) Token: 0x060090A1 RID: 37025 RVA: 0x00209A3B File Offset: 0x00207C3B
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x17002DCC RID: 11724
		// (get) Token: 0x060090A2 RID: 37026 RVA: 0x00209A53 File Offset: 0x00207C53
		// (set) Token: 0x060090A3 RID: 37027 RVA: 0x00209A82 File Offset: 0x00207C82
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("")]
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

		// Token: 0x17002DCD RID: 11725
		// (get) Token: 0x060090A4 RID: 37028 RVA: 0x00209A9A File Offset: 0x00207C9A
		// (set) Token: 0x060090A5 RID: 37029 RVA: 0x00209AC5 File Offset: 0x00207CC5
		[DefaultValue(FontType.Subset)]
		[NotifyParentProperty(true)]
		[Description("Enumeration that dictates how Apoc should treat fonts when producing a Pdf document.")]
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

		// Token: 0x17002DCE RID: 11726
		// (get) Token: 0x060090A6 RID: 37030 RVA: 0x00209ADD File Offset: 0x00207CDD
		// (set) Token: 0x060090A7 RID: 37031 RVA: 0x00209B0C File Offset: 0x00207D0C
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

		// Token: 0x17002DCF RID: 11727
		// (get) Token: 0x060090A8 RID: 37032 RVA: 0x00209B1F File Offset: 0x00207D1F
		// (set) Token: 0x060090A9 RID: 37033 RVA: 0x00209B4E File Offset: 0x00207D4E
		[Description("Determines the default font.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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
	}
}
