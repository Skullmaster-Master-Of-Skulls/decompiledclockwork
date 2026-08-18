using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C7 RID: 711
	internal class HeaderToolBarRenderer : IEditorRenderer, IRenderer
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x00052398 File Offset: 0x00050598
		public HeaderToolBarRenderer(HeaderToolsToolBar owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x000523A7 File Offset: 0x000505A7
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x000523AF File Offset: 0x000505AF
		public HeaderToolsToolBar Owner { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x000523B8 File Offset: 0x000505B8
		public virtual string CssClassFormatString
		{
			get
			{
				return "reToolBar t-hbox";
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000523E0 File Offset: 0x000505E0
		public virtual void RenderChildren(HtmlTextWriter writer)
		{
			IEnumerable<EditorHeaderTool> source = from t in this.Owner.Items
			select t as EditorHeaderTool;
			this.RenderTools(writer, from t in source
			where t.Position == EditorHeaderToolPosition.Left
			select t);
			this.RenderSpacer(writer);
			this.RenderTools(writer, from t in source
			where t.Position == EditorHeaderToolPosition.Right
			select t);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00052478 File Offset: 0x00050678
		private void RenderTools(HtmlTextWriter writer, IEnumerable<EditorHeaderTool> tools)
		{
			foreach (EditorHeaderTool editorHeaderTool in tools)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				editorHeaderTool.RenderControl(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000524D0 File Offset: 0x000506D0
		private void RenderSpacer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-spacer");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.RenderEndTag();
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000524ED File Offset: 0x000506ED
		public void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000524FB File Offset: 0x000506FB
		public void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00052503 File Offset: 0x00050703
		public HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Ul;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00052507 File Offset: 0x00050707
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00052509 File Offset: 0x00050709
		public void RenderContents(HtmlTextWriter writer)
		{
		}
	}
}
