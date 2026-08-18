using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200095F RID: 2399
	public class TreeListWordExpandCollapseCellStyle : TreeListWordStyle
	{
		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x00115A40 File Offset: 0x00113C40
		// (set) Token: 0x06005B3C RID: 23356 RVA: 0x00115A60 File Offset: 0x00113C60
		[DefaultValue("+")]
		[Description("Represents the text that replaces the expand image.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06005B3D RID: 23357 RVA: 0x00115A73 File Offset: 0x00113C73
		// (set) Token: 0x06005B3E RID: 23358 RVA: 0x00115A93 File Offset: 0x00113C93
		[Description("Represents the text that replaces the collapse image.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue("-")]
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

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x00115AA6 File Offset: 0x00113CA6
		// (set) Token: 0x06005B40 RID: 23360 RVA: 0x00115AC6 File Offset: 0x00113CC6
		[Category("Layout")]
		[DefaultValue("")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		[Description("Represents the path to the expand image.")]
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

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x00115AD9 File Offset: 0x00113CD9
		// (set) Token: 0x06005B42 RID: 23362 RVA: 0x00115B08 File Offset: 0x00113D08
		[Category("Layout")]
		[Description("Width of the expand image.")]
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

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x00115B20 File Offset: 0x00113D20
		// (set) Token: 0x06005B44 RID: 23364 RVA: 0x00115B4F File Offset: 0x00113D4F
		[Description("Height of the expand image.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
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

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06005B45 RID: 23365 RVA: 0x00115B67 File Offset: 0x00113D67
		// (set) Token: 0x06005B46 RID: 23366 RVA: 0x00115B87 File Offset: 0x00113D87
		[UrlProperty]
		[Description("Represents the path to the collapse image.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x00115B9A File Offset: 0x00113D9A
		// (set) Token: 0x06005B48 RID: 23368 RVA: 0x00115BC9 File Offset: 0x00113DC9
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

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x00115BE1 File Offset: 0x00113DE1
		// (set) Token: 0x06005B4A RID: 23370 RVA: 0x00115C10 File Offset: 0x00113E10
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Height of the collapse image.")]
		[NotifyParentProperty(true)]
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
