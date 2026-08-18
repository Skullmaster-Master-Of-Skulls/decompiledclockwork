using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000349 RID: 841
	[ControlBuilder(typeof(HtmlHeadBuilder))]
	public sealed class HtmlHead : HtmlGenericControl
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x0007EAC1 File Offset: 0x0007CCC1
		public HtmlHead() : base("head")
		{
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0007EACE File Offset: 0x0007CCCE
		public HtmlHead(string tag) : base(tag)
		{
			if (tag == null)
			{
				tag = string.Empty;
			}
			this._tagName = tag;
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x0007EAE8 File Offset: 0x0007CCE8
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

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x0007EB04 File Offset: 0x0007CD04
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x0007EB20 File Offset: 0x0007CD20
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

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0007EB3E File Offset: 0x0007CD3E
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x0007EB5A File Offset: 0x0007CD5A
		public string Description
		{
			get
			{
				if (this._description == null)
				{
					return this._cachedDescription;
				}
				return this._description.Content;
			}
			set
			{
				if (this._description == null)
				{
					this._cachedDescription = value;
					return;
				}
				this._description.Content = value;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0007EB78 File Offset: 0x0007CD78
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0007EB94 File Offset: 0x0007CD94
		public string Keywords
		{
			get
			{
				if (this._keywords == null)
				{
					return this._cachedKeywords;
				}
				return this._keywords.Content;
			}
			set
			{
				if (this._keywords == null)
				{
					this._cachedKeywords = value;
					return;
				}
				this._keywords.Content = value;
			}
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0007EBB4 File Offset: 0x0007CDB4
		protected internal override void AddedControl(Control control, int index)
		{
			base.AddedControl(control, index);
			if (!(control is HtmlTitle))
			{
				if (control is HtmlMeta)
				{
					HtmlMeta htmlMeta = (HtmlMeta)control;
					if (this._description == null && string.Equals(htmlMeta.Name, "description", StringComparison.OrdinalIgnoreCase))
					{
						this._description = htmlMeta;
						return;
					}
					if (this._keywords == null && string.Equals(htmlMeta.Name, "keywords", StringComparison.OrdinalIgnoreCase))
					{
						this._keywords = htmlMeta;
					}
				}
				return;
			}
			if (this._title != null)
			{
				throw new HttpException(SR.GetString("HtmlHead_OnlyOneTitleAllowed"));
			}
			this._title = (HtmlTitle)control;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0007EC4C File Offset: 0x0007CE4C
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

		// Token: 0x060026B8 RID: 9912 RVA: 0x0007EC99 File Offset: 0x0007CE99
		internal void RegisterCssStyleString(string outputString)
		{
			((HtmlHead.StyleSheetInternal)this.StyleSheet).CSSStyleString = outputString;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0007ECAC File Offset: 0x0007CEAC
		protected internal override void RemovedControl(Control control)
		{
			base.RemovedControl(control);
			if (control is HtmlTitle)
			{
				this._title = null;
				return;
			}
			if (control == this._description)
			{
				this._description = null;
				return;
			}
			if (control == this._keywords)
			{
				this._keywords = null;
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0007ECE8 File Offset: 0x0007CEE8
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
			if (this._description == null && !string.IsNullOrEmpty(this._cachedDescription))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, "description");
				writer.AddAttribute(HtmlTextWriterAttribute.Content, this._cachedDescription);
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this._keywords == null && !string.IsNullOrEmpty(this._cachedKeywords))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, "keywords");
				writer.AddAttribute(HtmlTextWriterAttribute.Content, this._cachedKeywords);
				writer.RenderBeginTag(HtmlTextWriterTag.Meta);
				writer.RenderEndTag();
			}
			if (this.Page.Request.Browser["requiresXhtmlCssSuppression"] != "true")
			{
				this.RenderStyleSheet(writer);
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0007EDCF File Offset: 0x0007CFCF
		internal void RenderStyleSheet(HtmlTextWriter writer)
		{
			if (this._styleSheet != null)
			{
				this._styleSheet.Render(writer);
			}
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0007EDE8 File Offset: 0x0007CFE8
		internal static void RenderCssRule(CssTextWriter cssWriter, string selector, Style style, IUrlResolutionService urlResolver)
		{
			cssWriter.WriteBeginCssRule(selector);
			CssStyleCollection styleAttributes = style.GetStyleAttributes(urlResolver);
			styleAttributes.Render(cssWriter);
			cssWriter.WriteEndCssRule();
		}

		// Token: 0x04001DBD RID: 7613
		private HtmlHead.StyleSheetInternal _styleSheet;

		// Token: 0x04001DBE RID: 7614
		private HtmlTitle _title;

		// Token: 0x04001DBF RID: 7615
		private string _cachedTitleText;

		// Token: 0x04001DC0 RID: 7616
		private HtmlMeta _description;

		// Token: 0x04001DC1 RID: 7617
		private string _cachedDescription;

		// Token: 0x04001DC2 RID: 7618
		private HtmlMeta _keywords;

		// Token: 0x04001DC3 RID: 7619
		private string _cachedKeywords;

		// Token: 0x0200098E RID: 2446
		private sealed class StyleSheetInternal : IStyleSheet, IUrlResolutionService
		{
			// Token: 0x06006A6D RID: 27245 RVA: 0x0017BDE9 File Offset: 0x00179FE9
			public StyleSheetInternal(HtmlHead owner)
			{
				this._owner = owner;
			}

			// Token: 0x17001D46 RID: 7494
			// (get) Token: 0x06006A6E RID: 27246 RVA: 0x0017BDF8 File Offset: 0x00179FF8
			// (set) Token: 0x06006A6F RID: 27247 RVA: 0x0017BE00 File Offset: 0x0017A000
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

			// Token: 0x06006A70 RID: 27248 RVA: 0x0017BE0C File Offset: 0x0017A00C
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

			// Token: 0x06006A71 RID: 27249 RVA: 0x0017BF1C File Offset: 0x0017A11C
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

			// Token: 0x06006A72 RID: 27250 RVA: 0x0017BFF0 File Offset: 0x0017A1F0
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
				int autoGenCount = this._autoGenCount;
				this._autoGenCount = autoGenCount + 1;
				int num = autoGenCount;
				string registeredCssClass = "aspnet_s" + num.ToString(NumberFormatInfo.InvariantInfo);
				style.SetRegisteredCssClass(registeredCssClass);
				this._styles.Add(styleInfo);
			}

			// Token: 0x06006A73 RID: 27251 RVA: 0x0017C094 File Offset: 0x0017A294
			string IUrlResolutionService.ResolveClientUrl(string relativeUrl)
			{
				return this._owner.ResolveClientUrl(relativeUrl);
			}

			// Token: 0x040038CF RID: 14543
			private HtmlHead _owner;

			// Token: 0x040038D0 RID: 14544
			private ArrayList _styles;

			// Token: 0x040038D1 RID: 14545
			private ArrayList _selectorStyles;

			// Token: 0x040038D2 RID: 14546
			private int _autoGenCount;

			// Token: 0x040038D3 RID: 14547
			private string _cssStyleString;

			// Token: 0x02000A8F RID: 2703
			private sealed class StyleInfo
			{
				// Token: 0x04003BE9 RID: 15337
				public Style style;

				// Token: 0x04003BEA RID: 15338
				public IUrlResolutionService urlResolver;
			}
		}
	}
}
