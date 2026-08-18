using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C5 RID: 709
	internal abstract class EditorToolBarRendererBase : IEditorRenderer, IRenderer
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x000521AA File Offset: 0x000503AA
		public EditorToolBarRendererBase(EditorToolBar owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x000521B9 File Offset: 0x000503B9
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x000521C1 File Offset: 0x000503C1
		protected EditorToolBar Owner { get; set; }

		// Token: 0x060018AE RID: 6318 RVA: 0x000521CA File Offset: 0x000503CA
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000521DF File Offset: 0x000503DF
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000521F8 File Offset: 0x000503F8
		public virtual void RenderChildren(HtmlTextWriter writer)
		{
			Collection<EditorToolBase> items = this.Owner.Items;
			Func<EditorToolBase, bool> predicate = (EditorToolBase item) => item is EditorTool;
			EditorToolBase editorToolBase = items.FirstOrDefault(predicate);
			EditorToolBase editorToolBase2 = items.LastOrDefault(predicate);
			foreach (EditorToolBase editorToolBase3 in items)
			{
				EditorSeparator editorSeparator = editorToolBase3 as EditorSeparator;
				if (editorSeparator != null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "reSeparator");
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					EditorSeparator.Render(writer);
				}
				else
				{
					EditorTool editorTool = editorToolBase3 as EditorTool;
					bool flag = editorToolBase3 == editorToolBase;
					if (flag || editorToolBase3 == editorToolBase2)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, flag ? "reToolFirstItem" : "reToolLastItem");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					editorTool.RenderMode = this.Owner.RenderMode;
					editorTool.RenderControl(writer);
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00052304 File Offset: 0x00050504
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Ul;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060018B2 RID: 6322
		public abstract string CssClassFormatString { get; }

		// Token: 0x060018B3 RID: 6323 RVA: 0x00052308 File Offset: 0x00050508
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.CssClassFormatString, this.Owner.RuntimeSkin));
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00052328 File Offset: 0x00050528
		public virtual void RenderContents(HtmlTextWriter writer)
		{
		}
	}
}
