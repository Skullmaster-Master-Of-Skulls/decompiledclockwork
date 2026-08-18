using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000476 RID: 1142
	internal class StyleBlockStyles
	{
		// Token: 0x06003867 RID: 14439 RVA: 0x000B7D73 File Offset: 0x000B5F73
		public StyleBlockStyles(string selector, StyleBlock styleControl)
		{
			this._selector = selector;
			this._styleControl = styleControl;
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000B7D94 File Offset: 0x000B5F94
		public bool Empty
		{
			get
			{
				return this._styles.Count == 0;
			}
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000B7DA4 File Offset: 0x000B5FA4
		public StyleBlockStyles AddStyle(HtmlTextWriterStyle styleName, string value)
		{
			this._styles.Add(styleName, value);
			return this;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000B7DB4 File Offset: 0x000B5FB4
		public StyleBlockStyles AddStyle(string styleName, string value)
		{
			this._styles.Add(styleName, value);
			return this;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000B7DC4 File Offset: 0x000B5FC4
		public StyleBlockStyles AddStyles(Style style)
		{
			if (style != null)
			{
				this.AddStyles(style.GetStyleAttributes(this._styleControl));
			}
			return this;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000B7DE0 File Offset: 0x000B5FE0
		public StyleBlockStyles AddStyles(CssStyleCollection styles)
		{
			if (styles != null)
			{
				foreach (object obj in styles.Keys)
				{
					string key = (string)obj;
					this._styles.Add(key, styles[key]);
				}
			}
			return this;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000B7E4C File Offset: 0x000B604C
		public void Render(HtmlTextWriter writer)
		{
			writer.WriteLine("{0} {{ {1} }}", this._selector, this._styles.Value);
		}

		// Token: 0x04002281 RID: 8833
		private string _selector;

		// Token: 0x04002282 RID: 8834
		private StyleBlock _styleControl;

		// Token: 0x04002283 RID: 8835
		private CssStyleCollection _styles = new CssStyleCollection();
	}
}
