using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2B RID: 6955
	public class StylesElement : ElementBase
	{
		// Token: 0x17005200 RID: 20992
		// (get) Token: 0x06010D45 RID: 68933 RVA: 0x003BBEEC File Offset: 0x003BA0EC
		protected override string StartTag
		{
			get
			{
				return "<Styles>";
			}
		}

		// Token: 0x17005201 RID: 20993
		// (get) Token: 0x06010D46 RID: 68934 RVA: 0x003BBEF3 File Offset: 0x003BA0F3
		protected override string EndTag
		{
			get
			{
				return "</Styles>";
			}
		}

		// Token: 0x17005202 RID: 20994
		// (get) Token: 0x06010D47 RID: 68935 RVA: 0x003BBEFA File Offset: 0x003BA0FA
		public virtual IStylesCollection Styles
		{
			get
			{
				if (this._styles == null)
				{
					this._styles = new StylesCollection();
				}
				return this._styles;
			}
		}

		// Token: 0x17005203 RID: 20995
		// (get) Token: 0x06010D48 RID: 68936 RVA: 0x003BBF15 File Offset: 0x003BA115
		public override IAttributesCollection Attributes
		{
			get
			{
				return new AttributesCollection();
			}
		}

		// Token: 0x06010D49 RID: 68937 RVA: 0x003BBF1C File Offset: 0x003BA11C
		protected override void AppendAttributes(StringBuilder sb)
		{
			sb.Append(this.StartTag);
		}

		// Token: 0x06010D4A RID: 68938 RVA: 0x003BBF2C File Offset: 0x003BA12C
		protected override void RenderChildElements(StringBuilder sb)
		{
			foreach (object obj in this.Styles)
			{
				StyleElement styleElement = (StyleElement)obj;
				if (styleElement != null)
				{
					((IElement)styleElement).Render(sb);
				}
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04004B42 RID: 19266
		private IStylesCollection _styles;
	}
}
