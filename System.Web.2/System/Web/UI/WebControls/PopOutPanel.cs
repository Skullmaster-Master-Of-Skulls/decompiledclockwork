using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A3 RID: 1187
	internal sealed class PopOutPanel : Panel
	{
		// Token: 0x06003B80 RID: 15232 RVA: 0x000C1423 File Offset: 0x000BF623
		public PopOutPanel(Menu owner, Style style)
		{
			this._owner = owner;
			this._style = style;
			this._emptyPopOutPanelStyle = new PopOutPanel.PopOutPanelStyle(null);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x00007722 File Offset: 0x00005922
		public override ScrollBars ScrollBars
		{
			get
			{
				return ScrollBars.None;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000C1445 File Offset: 0x000BF645
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x000C144D File Offset: 0x000BF64D
		internal string ScrollerClass
		{
			get
			{
				return this._scrollerClass;
			}
			set
			{
				this._scrollerClass = value;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000C1456 File Offset: 0x000BF656
		// (set) Token: 0x06003B85 RID: 15237 RVA: 0x000C145E File Offset: 0x000BF65E
		internal Style ScrollerStyle
		{
			get
			{
				return this._scrollerStyle;
			}
			set
			{
				this._scrollerStyle = value;
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000C1468 File Offset: 0x000BF668
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			Style style = this._style;
			this.CssClass = string.Empty;
			this._style = null;
			base.ControlStyle.Reset();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
			this._style = style;
			this.RenderStyleAttributes(writer);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000C14BC File Offset: 0x000BF6BC
		internal PopOutPanel.PopOutPanelStyle GetEmptyPopOutPanelStyle()
		{
			return this._emptyPopOutPanelStyle;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000C14C4 File Offset: 0x000BF6C4
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!this._owner.DesignMode)
			{
				this.RenderScrollerAttributes(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "Up");
				writer.AddAttribute("onmouseover", "PopOut_Up(this)");
				writer.AddAttribute("onmouseout", "PopOut_Stop(this)");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				string scrollUpImageUrl = this._owner.ScrollUpImageUrl;
				if (scrollUpImageUrl.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(scrollUpImageUrl));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.GetImageUrl(0));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this._owner.ScrollUpText);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				this.RenderScrollerAttributes(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "Dn");
				writer.AddAttribute("onmouseover", "PopOut_Down(this)");
				writer.AddAttribute("onmouseout", "PopOut_Stop(this)");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				string scrollDownImageUrl = this._owner.ScrollDownImageUrl;
				if (scrollDownImageUrl.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(scrollDownImageUrl));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.GetImageUrl(1));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this._owner.ScrollDownText);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000C1640 File Offset: 0x000BF840
		private void RenderScrollerAttributes(HtmlTextWriter writer)
		{
			if (this.Page != null && this.Page.SupportsStyleSheets)
			{
				if (!string.IsNullOrEmpty(this.ScrollerClass))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ScrollerClass + " " + this.GetEmptyPopOutPanelStyle().RegisteredCssClass);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetEmptyPopOutPanelStyle().RegisteredCssClass);
				}
			}
			else
			{
				if (this.ScrollerStyle != null && !this.ScrollerStyle.IsEmpty)
				{
					this.ScrollerStyle.AddAttributesToRender(writer);
				}
				if (this.ScrollerStyle.BackColor.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white");
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000C1740 File Offset: 0x000BF940
		private void RenderStyleAttributes(HtmlTextWriter writer)
		{
			if (this._style == null)
			{
				if (!string.IsNullOrEmpty(this.CssClass))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
					return;
				}
				if (this.BackColor.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white");
				}
				else
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(this.BackColor));
				}
				if (!this._owner.DesignMode)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0px");
					writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
					return;
				}
			}
			else
			{
				if (this.Page != null && this.Page.SupportsStyleSheets)
				{
					string registeredCssClass = this._style.RegisteredCssClass;
					if (registeredCssClass.Trim().Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, (!string.IsNullOrEmpty(this.CssClass)) ? (registeredCssClass + " " + this.CssClass) : registeredCssClass);
						return;
					}
				}
				if (!string.IsNullOrEmpty(this.CssClass))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
					return;
				}
				this._style.AddAttributesToRender(writer);
			}
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000C1877 File Offset: 0x000BFA77
		internal void SetInternalStyle(Style style)
		{
			this._style = style;
		}

		// Token: 0x04002344 RID: 9028
		private Menu _owner;

		// Token: 0x04002345 RID: 9029
		private string _scrollerClass;

		// Token: 0x04002346 RID: 9030
		private Style _scrollerStyle;

		// Token: 0x04002347 RID: 9031
		private Style _style;

		// Token: 0x04002348 RID: 9032
		private PopOutPanel.PopOutPanelStyle _emptyPopOutPanelStyle;

		// Token: 0x020009C6 RID: 2502
		internal sealed class PopOutPanelStyle : SubMenuStyle
		{
			// Token: 0x06006C5E RID: 27742 RVA: 0x00183AAD File Offset: 0x00181CAD
			public PopOutPanelStyle(PopOutPanel owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006C5F RID: 27743 RVA: 0x00183ABC File Offset: 0x00181CBC
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				if (base.BackColor.IsEmpty && (this._owner == null || this._owner.BackColor.IsEmpty))
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, "white");
				}
				attributes.Add(HtmlTextWriterStyle.Visibility, "hidden");
				attributes.Add(HtmlTextWriterStyle.Display, "none");
				attributes.Add(HtmlTextWriterStyle.Position, "absolute");
				attributes.Add(HtmlTextWriterStyle.Left, "0px");
				attributes.Add(HtmlTextWriterStyle.Top, "0px");
				base.FillStyleAttributes(attributes, urlResolver);
			}

			// Token: 0x040039B1 RID: 14769
			private PopOutPanel _owner;
		}
	}
}
