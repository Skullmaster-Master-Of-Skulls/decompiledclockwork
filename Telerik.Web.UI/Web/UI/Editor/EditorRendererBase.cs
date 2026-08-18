using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002BF RID: 703
	public abstract class EditorRendererBase : RendererBase, IEditorRenderer, IRenderer
	{
		// Token: 0x0600187B RID: 6267 RVA: 0x00050A33 File Offset: 0x0004EC33
		public EditorRendererBase(RadEditor editor)
		{
			this.Editor = editor;
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00050A42 File Offset: 0x0004EC42
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x00050A4A File Offset: 0x0004EC4A
		protected RadEditor Editor { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00050A53 File Offset: 0x0004EC53
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00050A57 File Offset: 0x0004EC57
		public override string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00050A5E File Offset: 0x0004EC5E
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00050A60 File Offset: 0x0004EC60
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00050A62 File Offset: 0x0004EC62
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00050A64 File Offset: 0x0004EC64
		public virtual void RenderChildren(HtmlTextWriter writer)
		{
		}
	}
}
