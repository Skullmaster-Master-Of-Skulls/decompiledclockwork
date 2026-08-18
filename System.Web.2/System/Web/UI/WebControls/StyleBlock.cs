using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000475 RID: 1141
	internal class StyleBlock : Control
	{
		// Token: 0x06003863 RID: 14435 RVA: 0x000B7C54 File Offset: 0x000B5E54
		public StyleBlockStyles AddStyleDefinition(string selector)
		{
			StyleBlockStyles styleBlockStyles = new StyleBlockStyles(selector, this);
			this._styles.Add(styleBlockStyles);
			return styleBlockStyles;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000B7C76 File Offset: 0x000B5E76
		public StyleBlockStyles AddStyleDefinition(string selectorFormat, params object[] args)
		{
			return this.AddStyleDefinition(string.Format(CultureInfo.InvariantCulture, selectorFormat, args));
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000B7C8C File Offset: 0x000B5E8C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this._styles.Any((StyleBlockStyles s) => !s.Empty))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
				writer.RenderBeginTag(HtmlTextWriterTag.Style);
				writer.WriteLine("/* <![CDATA[ */");
				foreach (StyleBlockStyles styleBlockStyles in from s in this._styles
				where !s.Empty
				select s)
				{
					styleBlockStyles.Render(writer);
				}
				writer.Write("/* ]]> */");
				writer.RenderEndTag();
			}
		}

		// Token: 0x04002280 RID: 8832
		private List<StyleBlockStyles> _styles = new List<StyleBlockStyles>();
	}
}
