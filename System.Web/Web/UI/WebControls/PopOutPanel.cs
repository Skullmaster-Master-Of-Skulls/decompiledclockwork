using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200061C RID: 1564
	internal sealed class PopOutPanel : Panel
	{
		// Token: 0x06004D9D RID: 19869 RVA: 0x0013AD8B File Offset: 0x00139D8B
		public PopOutPanel(Menu owner, Style style)
		{
			this._owner = owner;
			this._style = style;
			this._emptyPopOutPanelStyle = new PopOutPanel.PopOutPanelStyle(null);
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06004D9E RID: 19870 RVA: 0x0013ADAD File Offset: 0x00139DAD
		public override ScrollBars ScrollBars
		{
			get
			{
				return ScrollBars.None;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06004D9F RID: 19871 RVA: 0x0013ADB0 File Offset: 0x00139DB0
		// (set) Token: 0x06004DA0 RID: 19872 RVA: 0x0013ADB8 File Offset: 0x00139DB8
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

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x0013ADC1 File Offset: 0x00139DC1
		// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x0013ADC9 File Offset: 0x00139DC9
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

		// Token: 0x06004DA3 RID: 19875 RVA: 0x0013ADD4 File Offset: 0x00139DD4
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

		// Token: 0x06004DA4 RID: 19876 RVA: 0x0013AE28 File Offset: 0x00139E28
		internal PopOutPanel.PopOutPanelStyle GetEmptyPopOutPanelStyle()
		{
			return this._emptyPopOutPanelStyle;
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x0013AE30 File Offset: 0x00139E30
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

		// Token: 0x06004DA6 RID: 19878 RVA: 0x0013AFAC File Offset: 0x00139FAC
		private void RenderScrollerAttributes(HtmlTextWriter writer)
		{
			if (this.Page != null && this.Page.SupportsStyleSheets)
			{
				if (!string.IsNullOrEmpty(this.ScrollerClass))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ScrollerClass + ' ' + this.GetEmptyPopOutPanelStyle().RegisteredCssClass);
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

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0013B0B0 File Offset: 0x0013A0B0
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
						writer.AddAttribute(HtmlTextWriterAttribute.Class, (!string.IsNullOrEmpty(this.CssClass)) ? (registeredCssClass + ' ' + this.CssClass) : registeredCssClass);
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

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0013B1E9 File Offset: 0x0013A1E9
		internal void SetInternalStyle(Style style)
		{
			this._style = style;
		}

		// Token: 0x04002C69 RID: 11369
		private Menu _owner;

		// Token: 0x04002C6A RID: 11370
		private string _scrollerClass;

		// Token: 0x04002C6B RID: 11371
		private Style _scrollerStyle;

		// Token: 0x04002C6C RID: 11372
		private Style _style;

		// Token: 0x04002C6D RID: 11373
		private PopOutPanel.PopOutPanelStyle _emptyPopOutPanelStyle;

		// Token: 0x0200061E RID: 1566
		internal sealed class PopOutPanelStyle : SubMenuStyle
		{
			// Token: 0x06004DBF RID: 19903 RVA: 0x0013B7B6 File Offset: 0x0013A7B6
			public PopOutPanelStyle(PopOutPanel owner)
			{
				this._owner = owner;
			}

			// Token: 0x06004DC0 RID: 19904 RVA: 0x0013B7C8 File Offset: 0x0013A7C8
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

			// Token: 0x04002C70 RID: 11376
			private PopOutPanel _owner;
		}
	}
}
