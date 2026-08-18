using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CE RID: 718
	internal class HeaderToolRenderer : IEditorToolRenderer, IEditorRenderer, IRenderer
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00052A15 File Offset: 0x00050C15
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x00052A1D File Offset: 0x00050C1D
		public EditorHeaderTool Owner { get; private set; }

		// Token: 0x060018FB RID: 6395 RVA: 0x00052A26 File Offset: 0x00050C26
		public HeaderToolRenderer(EditorHeaderTool owner)
		{
			this.Owner = owner;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00052A35 File Offset: 0x00050C35
		public void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00052A4C File Offset: 0x00050C4C
		public void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00052A61 File Offset: 0x00050C61
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetCssClassString());
			this.RenderToolText(writer);
			writer.AddAttribute("role", "button");
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00052A88 File Offset: 0x00050C88
		public void RenderToolText(HtmlTextWriter writer)
		{
			string value = (!string.IsNullOrEmpty(this.Owner.Text.Trim())) ? this.Owner.Text : this.Owner.Name;
			writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00052ACE File Offset: 0x00050CCE
		public void RenderContents(HtmlTextWriter writer)
		{
			this.AddIconAttributesToRender(writer);
			this.RenderToolIcon(writer);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00052ADE File Offset: 0x00050CDE
		public void AddIconAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reIcon reIcon" + this.Owner.Name);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00052AFD File Offset: 0x00050CFD
		public void RenderToolIcon(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00052B0D File Offset: 0x00050D0D
		public void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00052B15 File Offset: 0x00050D15
		public HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00052B19 File Offset: 0x00050D19
		public string CssClassString
		{
			get
			{
				return "reButton re" + this.Owner.Name;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00052B30 File Offset: 0x00050D30
		public string GetCssClassString()
		{
			return this.CssClassString;
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00052B38 File Offset: 0x00050D38
		public string CssClassFormatString
		{
			get
			{
				return "{0}";
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00052B3F File Offset: 0x00050D3F
		public void RenderChildren(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00052B41 File Offset: 0x00050D41
		public void AddTextAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00052B43 File Offset: 0x00050D43
		public void RenderSplitButtonArrow(HtmlTextWriter writer)
		{
		}
	}
}
