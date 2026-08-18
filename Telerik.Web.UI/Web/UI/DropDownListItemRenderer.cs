using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B27 RID: 2855
	public class DropDownListItemRenderer : IRenderer
	{
		// Token: 0x06006B07 RID: 27399 RVA: 0x001902CA File Offset: 0x0018E4CA
		public DropDownListItemRenderer(DropDownListItem item)
		{
			this._item = item;
		}

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06006B08 RID: 27400 RVA: 0x001902D9 File Offset: 0x0018E4D9
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x06006B09 RID: 27401 RVA: 0x001902DD File Offset: 0x0018E4DD
		public string CssClassFormatString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x001902E4 File Offset: 0x0018E4E4
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1} {2} {3} {4}", new object[]
			{
				"rddlItem",
				(this._item.Templated || this._item.Controls.IsReadOnly) ? "rddlItemTemplate" : "",
				this._item.Selected ? "rddlItemSelected" : "",
				this._item.Enabled ? "" : "rddlItemDisabled",
				this._item.CssClass
			}).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			if (this._item.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this._item.ToolTip);
			}
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x001903B4 File Offset: 0x0018E5B4
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this._item.ImageUrl))
			{
				this.RenderImage(writer);
				this.RenderTextElement(writer);
				return;
			}
			writer.Write(this._item.Text);
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x001903E8 File Offset: 0x0018E5E8
		protected virtual void RenderTextElement(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlItemText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this._item.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x00190418 File Offset: 0x0018E618
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlItemImg");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, "image");
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this._item.ResolveClientUrl(this._item.ImageUrl));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x04001CF1 RID: 7409
		private const string ImageAlt = "image";

		// Token: 0x04001CF2 RID: 7410
		private DropDownListItem _item;
	}
}
