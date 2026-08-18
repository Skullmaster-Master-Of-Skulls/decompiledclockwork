using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CA RID: 1994
	public abstract class RibbonBarClickableItem : RibbonBarItem, IRibbonBarSizableItem, IRibbonBarImageContainingItem, IRibbonBarImageLargeContainingItem, IRibbonBarTextContainingItem
	{
		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000DB2A5 File Offset: 0x000D94A5
		// (set) Token: 0x06004574 RID: 17780 RVA: 0x000DB2C5 File Offset: 0x000D94C5
		[UrlProperty]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("The URL of the small image displayed for the item.")]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x000DB2D8 File Offset: 0x000D94D8
		// (set) Token: 0x06004576 RID: 17782 RVA: 0x000DB2F8 File Offset: 0x000D94F8
		public override string AccessKey
		{
			get
			{
				return (string)(this.ViewState["AccessKey"] ?? string.Empty);
			}
			set
			{
				this.ViewState["AccessKey"] = value;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000DB30B File Offset: 0x000D950B
		// (set) Token: 0x06004578 RID: 17784 RVA: 0x000DB32B File Offset: 0x000D952B
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x000DB33E File Offset: 0x000D953E
		// (set) Token: 0x0600457A RID: 17786 RVA: 0x000DB35E File Offset: 0x000D955E
		[UrlProperty]
		[DefaultValue("")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x000DB371 File Offset: 0x000D9571
		// (set) Token: 0x0600457C RID: 17788 RVA: 0x000DB391 File Offset: 0x000D9591
		[Category("Appearance")]
		[Description("The URL of the large image displayed for the item.")]
		[UrlProperty]
		[DefaultValue("")]
		[ClientPersistedProperty]
		public string ImageUrlLarge
		{
			get
			{
				return (string)(this.ViewState["ImageUrlLarge"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrlLarge"] = value;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x000DB3A4 File Offset: 0x000D95A4
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x000DB3C4 File Offset: 0x000D95C4
		[DefaultValue("")]
		[UrlProperty]
		public string DisabledImageUrlLarge
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrlLarge"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrlLarge"] = value;
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x000DB3D7 File Offset: 0x000D95D7
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x000DB3F8 File Offset: 0x000D95F8
		[Category("Appearance")]
		[ClientPersistedProperty]
		[Description("The size of the item on initial load of the RibbonBar.")]
		[DefaultValue(RibbonBarItemSize.Small)]
		public virtual RibbonBarItemSize Size
		{
			get
			{
				return (RibbonBarItemSize)(this.ViewState["Size"] ?? RibbonBarItemSize.Small);
			}
			set
			{
				this.ViewState["Size"] = value;
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x000DB410 File Offset: 0x000D9610
		// (set) Token: 0x06004582 RID: 17794 RVA: 0x000DB4A9 File Offset: 0x000D96A9
		[ClientPersistedProperty]
		[Category("Appearance")]
		[Description("The size of the item on initial load of the RibbonBar.")]
		public virtual RibbonBarImageRenderingMode ImageRenderingMode
		{
			get
			{
				if (this.ViewState["ImageRenderingMode"] != null)
				{
					return (RibbonBarImageRenderingMode)this.ViewState["ImageRenderingMode"];
				}
				RibbonBarImageRenderingMode result = base.RibbonBar.ImageRenderingMode;
				if (base.RibbonBar.ImageRenderingMode == RibbonBarImageRenderingMode.Auto)
				{
					if (this.Enabled)
					{
						if (!string.IsNullOrEmpty(this.ImageUrl) && string.IsNullOrEmpty(this.ImageUrlLarge))
						{
							result = RibbonBarImageRenderingMode.Clip;
						}
						else
						{
							result = RibbonBarImageRenderingMode.Dual;
						}
					}
					else if (!string.IsNullOrEmpty(this.DisabledImageUrl) && string.IsNullOrEmpty(this.DisabledImageUrlLarge))
					{
						result = RibbonBarImageRenderingMode.Clip;
					}
					else
					{
						result = RibbonBarImageRenderingMode.Dual;
					}
				}
				return result;
			}
			set
			{
				this.ViewState["ImageRenderingMode"] = value;
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x000DB4C1 File Offset: 0x000D96C1
		// (set) Token: 0x06004584 RID: 17796 RVA: 0x000DB4E1 File Offset: 0x000D96E1
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x000DB4F4 File Offset: 0x000D96F4
		// (set) Token: 0x06004586 RID: 17798 RVA: 0x000DB514 File Offset: 0x000D9714
		[DefaultValue("")]
		public string ImageAltText
		{
			get
			{
				return (string)(this.ViewState["ImageAltText"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageAltText"] = value;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x000DB527 File Offset: 0x000D9727
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x000DB52F File Offset: 0x000D972F
		public RibbonBarItemQuickAccess QuickAccess { get; set; }

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x000DB538 File Offset: 0x000D9738
		internal virtual string RibbonBarItemTypeCssClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x000DB53F File Offset: 0x000D973F
		internal virtual bool ShouldRenderTextStructure
		{
			get
			{
				return this.ShouldRenderTextContent;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x000DB547 File Offset: 0x000D9747
		internal virtual bool ShouldRenderTextContent
		{
			get
			{
				return !string.IsNullOrEmpty(this.Text) && this.Size != RibbonBarItemSize.Small;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x000DB564 File Offset: 0x000D9764
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return base.Renderer.TagKey;
			}
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x000DB571 File Offset: 0x000D9771
		protected virtual string GetCssClass()
		{
			return base.Renderer.CssClassFormatString;
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x000DB57E File Offset: 0x000D977E
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000DB58C File Offset: 0x000D978C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.Renderer.RenderContents(writer);
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000DB59A File Offset: 0x000D979A
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			((RibbonBarItemRenderBase)base.Renderer).RenderBeginTagContext(writer);
			base.RenderBeginTag(writer);
		}
	}
}
