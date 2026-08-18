using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001229 RID: 4649
	public class TreeListPdfExpandCollapseCellStyle : TreeListPdfStyle
	{
		// Token: 0x17003DDC RID: 15836
		// (get) Token: 0x0600BFCE RID: 49102 RVA: 0x002A920A File Offset: 0x002A740A
		// (set) Token: 0x0600BFCF RID: 49103 RVA: 0x002A922A File Offset: 0x002A742A
		[Category("Layout")]
		[Description("Represents the text that replaces the expand image.")]
		[DefaultValue("+")]
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

		// Token: 0x17003DDD RID: 15837
		// (get) Token: 0x0600BFD0 RID: 49104 RVA: 0x002A923D File Offset: 0x002A743D
		// (set) Token: 0x0600BFD1 RID: 49105 RVA: 0x002A925D File Offset: 0x002A745D
		[DefaultValue("-")]
		[NotifyParentProperty(true)]
		[Description("Represents the text that replaces the collapse image.")]
		[Category("Layout")]
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

		// Token: 0x17003DDE RID: 15838
		// (get) Token: 0x0600BFD2 RID: 49106 RVA: 0x002A9270 File Offset: 0x002A7470
		// (set) Token: 0x0600BFD3 RID: 49107 RVA: 0x002A9290 File Offset: 0x002A7490
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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

		// Token: 0x17003DDF RID: 15839
		// (get) Token: 0x0600BFD4 RID: 49108 RVA: 0x002A92A3 File Offset: 0x002A74A3
		// (set) Token: 0x0600BFD5 RID: 49109 RVA: 0x002A92D2 File Offset: 0x002A74D2
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("Width of the expand image.")]
		[Category("Layout")]
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

		// Token: 0x17003DE0 RID: 15840
		// (get) Token: 0x0600BFD6 RID: 49110 RVA: 0x002A92EA File Offset: 0x002A74EA
		// (set) Token: 0x0600BFD7 RID: 49111 RVA: 0x002A9319 File Offset: 0x002A7519
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("Height of the expand image.")]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x17003DE1 RID: 15841
		// (get) Token: 0x0600BFD8 RID: 49112 RVA: 0x002A9331 File Offset: 0x002A7531
		// (set) Token: 0x0600BFD9 RID: 49113 RVA: 0x002A9351 File Offset: 0x002A7551
		[DefaultValue("")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[Description("Represents the path to the collapse image.")]
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

		// Token: 0x17003DE2 RID: 15842
		// (get) Token: 0x0600BFDA RID: 49114 RVA: 0x002A9364 File Offset: 0x002A7564
		// (set) Token: 0x0600BFDB RID: 49115 RVA: 0x002A9393 File Offset: 0x002A7593
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Description("Width of the collapse image.")]
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

		// Token: 0x17003DE3 RID: 15843
		// (get) Token: 0x0600BFDC RID: 49116 RVA: 0x002A93AB File Offset: 0x002A75AB
		// (set) Token: 0x0600BFDD RID: 49117 RVA: 0x002A93DA File Offset: 0x002A75DA
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003DE4 RID: 15844
		// (get) Token: 0x0600BFDE RID: 49118 RVA: 0x002A93F2 File Offset: 0x002A75F2
		// (set) Token: 0x0600BFDF RID: 49119 RVA: 0x002A93FA File Offset: 0x002A75FA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Unit LineHeight { get; set; }
	}
}
