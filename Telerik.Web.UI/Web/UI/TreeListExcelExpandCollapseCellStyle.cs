using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001225 RID: 4645
	public class TreeListExcelExpandCollapseCellStyle : TreeListExcelStyle
	{
		// Token: 0x17003DCF RID: 15823
		// (get) Token: 0x0600BFA6 RID: 49062 RVA: 0x002A801F File Offset: 0x002A621F
		// (set) Token: 0x0600BFA7 RID: 49063 RVA: 0x002A803F File Offset: 0x002A623F
		[Description("Represents the text that replaces the expand image.")]
		[NotifyParentProperty(true)]
		[DefaultValue("+")]
		[Category("Layout")]
		public virtual string ExpandText
		{
			get
			{
				return (base.ViewState["ExpandText"] as string) ?? "+";
			}
			set
			{
				base.ViewState["ExpandText"] = value;
			}
		}

		// Token: 0x17003DD0 RID: 15824
		// (get) Token: 0x0600BFA8 RID: 49064 RVA: 0x002A8052 File Offset: 0x002A6252
		// (set) Token: 0x0600BFA9 RID: 49065 RVA: 0x002A8072 File Offset: 0x002A6272
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue("-")]
		[Description("Represents the text that replaces the collapse image.")]
		public virtual string CollapseText
		{
			get
			{
				return (base.ViewState["CollapseText"] as string) ?? "-";
			}
			set
			{
				base.ViewState["CollapseText"] = value;
			}
		}

		// Token: 0x17003DD1 RID: 15825
		// (get) Token: 0x0600BFAA RID: 49066 RVA: 0x002A8085 File Offset: 0x002A6285
		// (set) Token: 0x0600BFAB RID: 49067 RVA: 0x002A80B0 File Offset: 0x002A62B0
		[DefaultValue(false)]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("Determines whether the expand/collapse image will be resized to fit in the cell boundaries.")]
		public virtual bool EnableImageBestFit
		{
			get
			{
				return base.ViewState["EnableImageBestFit"] != null && (bool)base.ViewState["EnableImageBestFit"];
			}
			set
			{
				base.ViewState["EnableImageBestFit"] = value;
			}
		}

		// Token: 0x17003DD2 RID: 15826
		// (get) Token: 0x0600BFAC RID: 49068 RVA: 0x002A80C8 File Offset: 0x002A62C8
		// (set) Token: 0x0600BFAD RID: 49069 RVA: 0x002A80E8 File Offset: 0x002A62E8
		[DefaultValue("")]
		[Description("Represents the path to the expand image.")]
		[Category("Layout")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		public virtual string ExpandImageUrl
		{
			get
			{
				return (base.ViewState["ExpandImageUrl"] as string) ?? "";
			}
			set
			{
				base.ViewState["ExpandImageUrl"] = value;
			}
		}

		// Token: 0x17003DD3 RID: 15827
		// (get) Token: 0x0600BFAE RID: 49070 RVA: 0x002A80FB File Offset: 0x002A62FB
		// (set) Token: 0x0600BFAF RID: 49071 RVA: 0x002A812A File Offset: 0x002A632A
		[Description("Width of the expand image.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit ExpandImageWidth
		{
			get
			{
				if (base.ViewState["ExpandImageWidth"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["ExpandImageWidth"];
			}
			set
			{
				base.ViewState["ExpandImageWidth"] = value;
			}
		}

		// Token: 0x17003DD4 RID: 15828
		// (get) Token: 0x0600BFB0 RID: 49072 RVA: 0x002A8142 File Offset: 0x002A6342
		// (set) Token: 0x0600BFB1 RID: 49073 RVA: 0x002A8171 File Offset: 0x002A6371
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("Height of the expand image.")]
		public virtual Unit ExpandImageHeight
		{
			get
			{
				if (base.ViewState["ExpandImageHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["ExpandImageHeight"];
			}
			set
			{
				base.ViewState["ExpandImageHeight"] = value;
			}
		}

		// Token: 0x17003DD5 RID: 15829
		// (get) Token: 0x0600BFB2 RID: 49074 RVA: 0x002A8189 File Offset: 0x002A6389
		// (set) Token: 0x0600BFB3 RID: 49075 RVA: 0x002A81A9 File Offset: 0x002A63A9
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[Description("Represents the path to the collapse image.")]
		[Category("Layout")]
		public virtual string CollapseImageUrl
		{
			get
			{
				return (base.ViewState["CollapseImageUrl"] as string) ?? "";
			}
			set
			{
				base.ViewState["CollapseImageUrl"] = value;
			}
		}

		// Token: 0x17003DD6 RID: 15830
		// (get) Token: 0x0600BFB4 RID: 49076 RVA: 0x002A81BC File Offset: 0x002A63BC
		// (set) Token: 0x0600BFB5 RID: 49077 RVA: 0x002A81EB File Offset: 0x002A63EB
		[Description("Width of the collapse image.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit CollapseImageWidth
		{
			get
			{
				if (base.ViewState["CollapseImageWidth"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["CollapseImageWidth"];
			}
			set
			{
				base.ViewState["CollapseImageWidth"] = value;
			}
		}

		// Token: 0x17003DD7 RID: 15831
		// (get) Token: 0x0600BFB6 RID: 49078 RVA: 0x002A8203 File Offset: 0x002A6403
		// (set) Token: 0x0600BFB7 RID: 49079 RVA: 0x002A8232 File Offset: 0x002A6432
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("Height of the collapse image.")]
		public virtual Unit CollapseImageHeight
		{
			get
			{
				if (base.ViewState["CollapseImageHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["CollapseImageHeight"];
			}
			set
			{
				base.ViewState["CollapseImageHeight"] = value;
			}
		}
	}
}
