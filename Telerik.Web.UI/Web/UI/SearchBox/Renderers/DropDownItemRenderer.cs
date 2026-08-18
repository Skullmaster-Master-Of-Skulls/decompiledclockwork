using System;
using System.Web.UI;

namespace Telerik.Web.UI.SearchBox.Renderers
{
	// Token: 0x02000EF4 RID: 3828
	public class DropDownItemRenderer : IRenderer
	{
		// Token: 0x06009107 RID: 37127 RVA: 0x0020A42A File Offset: 0x0020862A
		public DropDownItemRenderer(DropDownItem owner)
		{
			this._control = owner;
		}

		// Token: 0x17002DF0 RID: 11760
		// (get) Token: 0x06009108 RID: 37128 RVA: 0x0020A439 File Offset: 0x00208639
		public HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x17002DF1 RID: 11761
		// (get) Token: 0x06009109 RID: 37129 RVA: 0x0020A43D File Offset: 0x0020863D
		public string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x0020A444 File Offset: 0x00208644
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x0020A44C File Offset: 0x0020864C
		public void RenderContents(HtmlTextWriter writer)
		{
			string text = "rsbListItem";
			if (this._control.Templated)
			{
				text += " rsbListItemTemplate";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(this.TagKey);
			if (this._control.Templated)
			{
				this.RenderTemplate(writer);
			}
			else
			{
				writer.Write(this._control.DisplayText);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600910C RID: 37132 RVA: 0x0020A4BC File Offset: 0x002086BC
		private void RenderTemplate(HtmlTextWriter writer)
		{
			foreach (object obj in this._control.Controls)
			{
				Control control = (Control)obj;
				control.RenderControl(writer);
			}
		}

		// Token: 0x0400293A RID: 10554
		private DropDownItem _control;
	}
}
