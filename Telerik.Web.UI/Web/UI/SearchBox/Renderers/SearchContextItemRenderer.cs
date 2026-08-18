using System;
using System.Web.UI;

namespace Telerik.Web.UI.SearchBox.Renderers
{
	// Token: 0x02000871 RID: 2161
	public class SearchContextItemRenderer : IRenderer
	{
		// Token: 0x06004FCF RID: 20431 RVA: 0x000FA2B2 File Offset: 0x000F84B2
		public SearchContextItemRenderer(SearchContextItem item)
		{
			this._item = item;
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x000FA2C1 File Offset: 0x000F84C1
		public virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x000FA2C8 File Offset: 0x000F84C8
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x000FA2CC File Offset: 0x000F84CC
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1} {2} {3}", new object[]
			{
				"rsbListItem",
				this._item.Selected ? "rsbListItemSelected" : "",
				this._item.Enabled ? "" : "rsbListItemDisabled",
				this._item.CssClass
			}).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x000FA348 File Offset: 0x000F8548
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this._item.ImageUrl))
			{
				this.RenderImage(writer);
				if (!string.IsNullOrEmpty(this._item.Text))
				{
					this.RenderTextElement(writer);
					return;
				}
			}
			else
			{
				writer.Write(this._item.Text);
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x000FA399 File Offset: 0x000F8599
		protected virtual void RenderTextElement(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbListItemText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this._item.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x000FA3C8 File Offset: 0x000F85C8
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbListItemImg");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, "image");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this._item.ResolveClientUrl(this._item.ImageUrl));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x040013D7 RID: 5079
		private const string ImageAlt = "image";

		// Token: 0x040013D8 RID: 5080
		private SearchContextItem _item;
	}
}
