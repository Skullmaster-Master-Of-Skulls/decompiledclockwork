using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000496 RID: 1174
	[ControlBuilder(typeof(HtmlHeadBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlHead : HtmlGenericControl
	{
		// Token: 0x060036E6 RID: 14054 RVA: 0x000ECBB8 File Offset: 0x000EBBB8
		public HtmlHead() : base("head")
		{
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000ECBC5 File Offset: 0x000EBBC5
		public HtmlHead(string tag) : base(tag)
		{
			if (tag == null)
			{
				tag = string.Empty;
			}
			this._tagName = tag;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000ECBDF File Offset: 0x000EBBDF
		public IStyleSheet StyleSheet
		{
			get
			{
				if (this._styleSheet == null)
				{
					this._styleSheet = new HtmlHead.StyleSheetInternal(this);
				}
				return this._styleSheet;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000ECBFB File Offset: 0x000EBBFB
		// (set) Token: 0x060036EA RID: 14058 RVA: 0x000ECC17 File Offset: 0x000EBC17
		public string Title
		{
			get
			{
				if (this._title == null)
				{
					return this._cachedTitleText;
				}
				return this._title.Text;
			}
			set
			{
				if (this._title == null)
				{
					this._cachedTitleText = value;
					return;
				}
				this._title.Text = value;
			}
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000ECC35 File Offset: 0x000EBC35
		protected internal override void AddedControl(Control control, int index)
		{
			base.AddedControl(control, index);
			if (control is HtmlTitle)
			{
				if (this._title != null)
				{
					throw new HttpException(SR.GetString("HtmlHead_OnlyOneTitleAllowed"));
				}
				this._title = (HtmlTitle)control;
			}
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000ECC6C File Offset: 0x000EBC6C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page == null)
			{
				throw new HttpException(SR.GetString("Head_Needs_Page"));
			}
			if (page.Header != null)
			{
				throw new HttpException(SR.GetString("HtmlHead_OnlyOneHeadAllowed"));
			}
			page.SetHeader(this);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000ECCB9 File Offset: 0x000EBCB9
		internal void RegisterCssStyleString(string outputString)
		{
			((HtmlHead.StyleSheetInternal)this.StyleSheet).CSSStyleString = outputString;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000ECCCC File Offset: 0x000EBCCC
		protected internal override void RemovedControl(Control control)
		{
			base.RemovedControl(control);
			if (control is HtmlTitle)
			{
				this._title = null;
			}
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000ECCE4 File Offset: 0x000EBCE4
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
			if (this._title == null)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Title);
				if (this._cachedTitleText != null)
				{
					writer.Write(this._cachedTitleText);
				}
				writer.RenderEndTag();
			}
			if (this.Page.Request.Browser["requiresXhtmlCssSuppression"] != "true")
			{
				this.RenderStyleSheet(writer);
			}
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000ECD4F File Offset: 0x000EBD4F
		internal void RenderStyleSheet(HtmlTextWriter writer)
		{
			if (this._styleSheet != null)
			{
				this._styleSheet.Render(writer);
			}
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000ECD68 File Offset: 0x000EBD68
		internal static void RenderCssRule(CssTextWriter cssWriter, string selector, Style style, IUrlResolutionService urlResolver)
		{
			cssWriter.WriteBeginCssRule(selector);
			CssStyleCollection styleAttributes = style.GetStyleAttributes(urlResolver);
			styleAttributes.Render(cssWriter);
			cssWriter.WriteEndCssRule();
		}

		// Token: 0x040025B5 RID: 9653
		private HtmlHead.StyleSheetInternal _styleSheet;

		// Token: 0x040025B6 RID: 9654
		private HtmlTitle _title;

		// Token: 0x040025B7 RID: 9655
		private string _cachedTitleText;

		// Token: 0x02000497 RID: 1175
		private sealed class StyleSheetInternal : IStyleSheet, IUrlResolutionService
		{
			// Token: 0x060036F2 RID: 14066 RVA: 0x000ECD91 File Offset: 0x000EBD91
			public StyleSheetInternal(HtmlHead owner)
			{
				this._owner = owner;
			}

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000ECDA0 File Offset: 0x000EBDA0
			// (set) Token: 0x060036F4 RID: 14068 RVA: 0x000ECDA8 File Offset: 0x000EBDA8
			internal string CSSStyleString
			{
				get
				{
					return this._cssStyleString;
				}
				set
				{
					this._cssStyleString = value;
				}
			}

			// Token: 0x060036F5 RID: 14069 RVA: 0x000ECDB4 File Offset: 0x000EBDB4
			public void Render(HtmlTextWriter writer)
			{
				if (this._styles == null && this._selectorStyles == null && this.CSSStyleString == null)
				{
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
				writer.RenderBeginTag(HtmlTextWriterTag.Style);
				CssTextWriter cssWriter = new CssTextWriter(writer);
				if (this._styles != null)
				{
					for (int i = 0; i < this._styles.Count; i++)
					{
						HtmlHead.StyleSheetInternal.StyleInfo styleInfo = (HtmlHead.StyleSheetInternal.StyleInfo)this._styles[i];
						string registeredCssClass = styleInfo.style.RegisteredCssClass;
						if (registeredCssClass.Length != 0)
						{
							HtmlHead.RenderCssRule(cssWriter, "." + registeredCssClass, styleInfo.style, styleInfo.urlResolver);
						}
					}
				}
				if (this._selectorStyles != null)
				{
					for (int j = 0; j < this._selectorStyles.Count; j++)
					{
						SelectorStyleInfo selectorStyleInfo = (SelectorStyleInfo)this._selectorStyles[j];
						HtmlHead.RenderCssRule(cssWriter, selectorStyleInfo.selector, selectorStyleInfo.style, selectorStyleInfo.urlResolver);
					}
				}
				if (this.CSSStyleString != null)
				{
					writer.Write(this.CSSStyleString);
				}
				writer.RenderEndTag();
			}

			// Token: 0x060036F6 RID: 14070 RVA: 0x000ECEC4 File Offset: 0x000EBEC4
			void IStyleSheet.CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selector)
			{
				if (style == null)
				{
					throw new ArgumentNullException("style");
				}
				if (selector.Length == 0)
				{
					throw new ArgumentNullException("selector");
				}
				if (this._selectorStyles == null)
				{
					this._selectorStyles = new ArrayList();
				}
				if (urlResolver == null)
				{
					urlResolver = this;
				}
				SelectorStyleInfo selectorStyleInfo = new SelectorStyleInfo();
				selectorStyleInfo.selector = selector;
				selectorStyleInfo.style = style;
				selectorStyleInfo.urlResolver = urlResolver;
				this._selectorStyles.Add(selectorStyleInfo);
				Page page = this._owner.Page;
				if (page.PartialCachingControlStack != null)
				{
					foreach (object obj in page.PartialCachingControlStack)
					{
						BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
						basePartialCachingControl.RegisterStyleInfo(selectorStyleInfo);
					}
				}
			}

			// Token: 0x060036F7 RID: 14071 RVA: 0x000ECF98 File Offset: 0x000EBF98
			void IStyleSheet.RegisterStyle(Style style, IUrlResolutionService urlResolver)
			{
				if (style == null)
				{
					throw new ArgumentNullException("style");
				}
				if (this._styles == null)
				{
					this._styles = new ArrayList();
				}
				else if (style.RegisteredCssClass.Length != 0)
				{
					throw new InvalidOperationException(SR.GetString("HtmlHead_StyleAlreadyRegistered"));
				}
				if (urlResolver == null)
				{
					urlResolver = this;
				}
				HtmlHead.StyleSheetInternal.StyleInfo styleInfo = new HtmlHead.StyleSheetInternal.StyleInfo();
				styleInfo.style = style;
				styleInfo.urlResolver = urlResolver;
				string registeredCssClass = "aspnet_s" + this._autoGenCount++.ToString(NumberFormatInfo.InvariantInfo);
				style.SetRegisteredCssClass(registeredCssClass);
				this._styles.Add(styleInfo);
			}

			// Token: 0x060036F8 RID: 14072 RVA: 0x000ED03C File Offset: 0x000EC03C
			string IUrlResolutionService.ResolveClientUrl(string relativeUrl)
			{
				return this._owner.ResolveClientUrl(relativeUrl);
			}

			// Token: 0x040025B8 RID: 9656
			private HtmlHead _owner;

			// Token: 0x040025B9 RID: 9657
			private ArrayList _styles;

			// Token: 0x040025BA RID: 9658
			private ArrayList _selectorStyles;

			// Token: 0x040025BB RID: 9659
			private int _autoGenCount;

			// Token: 0x040025BC RID: 9660
			private string _cssStyleString;

			// Token: 0x02000498 RID: 1176
			private sealed class StyleInfo
			{
				// Token: 0x040025BD RID: 9661
				public Style style;

				// Token: 0x040025BE RID: 9662
				public IUrlResolutionService urlResolver;
			}
		}
	}
}
