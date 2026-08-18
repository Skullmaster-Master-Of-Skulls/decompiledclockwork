using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SearchBox.Renderers
{
	// Token: 0x02000872 RID: 2162
	public class SearchContextRenderer : IRenderer
	{
		// Token: 0x06004FD6 RID: 20438 RVA: 0x000FA41A File Offset: 0x000F861A
		internal SearchContextRenderer(SearchContextControl owner)
		{
			this._control = owner;
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x000FA429 File Offset: 0x000F8629
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x000FA42D File Offset: 0x000F862D
		public virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x000FA434 File Offset: 0x000F8634
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this._control.CssClass;
			Unit width = this._control.Width;
			this._control.Width = Unit.Empty;
			string arg = this._control.Enabled ? string.Empty : "rsbSCDisabled";
			this._control.CssClass = string.Format("{0} {1} {2}", "rsbSearchContext", arg, this._control.CssClass).Trim();
			if (width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString());
			}
			this._control.CallBaseAddAttributesToRender(writer);
			this._control.CssClass = cssClass;
			this._control.Width = width;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x000FA4F4 File Offset: 0x000F86F4
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbSCInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderTextArea(writer);
			this.RenderArrow(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x000FA520 File Offset: 0x000F8720
		protected virtual void RenderTextArea(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbSCFakeInput");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (this._control.SelectedItem != null)
			{
				this._control.SelectedItem.Renderer.RenderContents(writer);
			}
			else if (this._control.IsUsingODataBinding || this._control.IsUsingWebServiceBinding)
			{
				writer.Write(this._control.LoadingItemsMessage);
			}
			else if (this._control.ShowDefaultItem)
			{
				writer.Write(this._control.DefaultItemText);
			}
			else if (this._control.Children.Count > 0)
			{
				this._control.Children[0].Renderer.RenderContents(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x000FA5E9 File Offset: 0x000F87E9
		protected virtual void RenderArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbSCIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
		}

		// Token: 0x040013D9 RID: 5081
		private SearchContextControl _control;
	}
}
