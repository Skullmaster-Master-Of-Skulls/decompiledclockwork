using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD3 RID: 6867
	[ToolboxBitmap(typeof(RadPageView), "Telerik.Web.UI.PageView.png")]
	[TelerikToolboxCategory("Navigation")]
	[Designer("Telerik.Web.Design.RadPageViewDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[PersistChildren(true)]
	[ParseChildren(false)]
	[ToolboxData("<{0}:RadPageView Runat=\"server\" Width=\"100%\">PageView</{0}:RadPageView>")]
	public class RadPageView : WebControl
	{
		// Token: 0x170050D4 RID: 20692
		// (get) Token: 0x06010A00 RID: 68096 RVA: 0x003B58D3 File Offset: 0x003B3AD3
		public RadMultiPage MultiPage
		{
			get
			{
				return this.Parent as RadMultiPage;
			}
		}

		// Token: 0x170050D5 RID: 20693
		// (get) Token: 0x06010A01 RID: 68097 RVA: 0x003B58E0 File Offset: 0x003B3AE0
		// (set) Token: 0x06010A02 RID: 68098 RVA: 0x003B58FF File Offset: 0x003B3AFF
		[DefaultValue(false)]
		[Description("Specifies if current RadPageView is selected")]
		public bool Selected
		{
			get
			{
				return this.MultiPage != null && this.MultiPage.SelectedIndex == this.Index;
			}
			set
			{
				if (this.MultiPage == null)
				{
					this.cachedSelected = value;
					return;
				}
				this.MultiPage.SelectedIndex = (value ? this.Index : -1);
			}
		}

		// Token: 0x170050D6 RID: 20694
		// (get) Token: 0x06010A03 RID: 68099 RVA: 0x003B5928 File Offset: 0x003B3B28
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.MultiPage == null)
				{
					return -1;
				}
				return this.MultiPage.PageViews.IndexOf(this);
			}
		}

		// Token: 0x170050D7 RID: 20695
		// (get) Token: 0x06010A04 RID: 68100 RVA: 0x003B5945 File Offset: 0x003B3B45
		// (set) Token: 0x06010A05 RID: 68101 RVA: 0x003B5965 File Offset: 0x003B3B65
		[DefaultValue("")]
		[Description("Gets or sets the identifier for the default button that is contained in the RadPageView control.")]
		public string DefaultButton
		{
			get
			{
				return ((string)this.ViewState["DefaultButton"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DefaultButton"] = value;
			}
		}

		// Token: 0x170050D8 RID: 20696
		// (get) Token: 0x06010A06 RID: 68102 RVA: 0x003B5978 File Offset: 0x003B3B78
		// (set) Token: 0x06010A07 RID: 68103 RVA: 0x003B5998 File Offset: 0x003B3B98
		[Category("Content")]
		[DefaultValue("")]
		[Description("Specifies the URL that will be loaded in the RadPageView")]
		[UrlProperty]
		public string ContentUrl
		{
			get
			{
				return ((string)this.ViewState["ContentUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ContentUrl"] = value;
			}
		}

		// Token: 0x170050D9 RID: 20697
		// (get) Token: 0x06010A08 RID: 68104 RVA: 0x003B59AB File Offset: 0x003B3BAB
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06010A09 RID: 68105 RVA: 0x003B59B0 File Offset: 0x003B3BB0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.EnsureID();
			if (this.MultiPage == null)
			{
				throw new NotSupportedException("RadPageView must be added in a RadMultiPage control");
			}
			if (this.MultiPage.ScrollBars != MultiPageScrollBars.None)
			{
				this.RenderScrollbars(writer);
			}
			string cssClass = this.CssClass;
			this.CssClass = string.Format("{0}{1} {2}", "rmpView", this.Selected ? string.Empty : " rmpHidden", this.CssClass).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06010A0A RID: 68106 RVA: 0x003B5A34 File Offset: 0x003B3C34
		private void RenderScrollbars(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Style[HtmlTextWriterStyle.Overflow]) || !string.IsNullOrEmpty(base.Style["overflow"]))
			{
				return;
			}
			switch (this.MultiPage.ScrollBars)
			{
			case MultiPageScrollBars.Horizontal:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
				break;
			case MultiPageScrollBars.Vertical:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
				break;
			case MultiPageScrollBars.Both:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
				break;
			case MultiPageScrollBars.Auto:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
				break;
			case MultiPageScrollBars.Hidden:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
				break;
			}
			if (this.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			if (this.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			}
		}

		// Token: 0x06010A0B RID: 68107 RVA: 0x003B5B1C File Offset: 0x003B3D1C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.ContentUrl))
			{
				base.RenderContents(writer);
				return;
			}
			if (this.Selected)
			{
				writer.AddAttribute("src", base.ResolveUrl(this.ContentUrl));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Iframe);
			writer.RenderEndTag();
		}

		// Token: 0x04004A4F RID: 19023
		internal bool cachedSelected;
	}
}
