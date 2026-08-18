using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C9 RID: 713
	internal class MobileToolBarRenderer : EditorToolBarRendererBase
	{
		// Token: 0x060018CB RID: 6347 RVA: 0x0005251B File Offset: 0x0005071B
		public MobileToolBarRenderer(EditorToolBar editor) : base(editor)
		{
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00052524 File Offset: 0x00050724
		public override string CssClassFormatString
		{
			get
			{
				return "reToolList";
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0005253C File Offset: 0x0005073C
		public override void RenderChildren(HtmlTextWriter writer)
		{
			Collection<EditorToolBase> items = base.Owner.Items;
			Func<EditorToolBase, bool> func = (EditorToolBase item) => item is EditorTool;
			int num = 0;
			foreach (EditorToolBase editorToolBase in items)
			{
				num++;
				editorToolBase.RenderMode = base.Owner.RenderMode;
				if (editorToolBase.Type == EditorToolType.Button)
				{
					this.RenderButton((EditorTool)editorToolBase, writer);
					if (this._buttonGroupBeginTagIsRended && num == items.Count)
					{
						this.RenderButtonGroupEnd(writer);
					}
				}
				else
				{
					if (this._buttonGroupBeginTagIsRended)
					{
						this.RenderButtonGroupEnd(writer);
					}
					if (editorToolBase.Type == EditorToolType.DropDown || editorToolBase.Type == EditorToolType.ToolStrip || editorToolBase.Type == EditorToolType.SplitButton)
					{
						this.RenderDropDown((EditorTool)editorToolBase, writer);
					}
					else if (editorToolBase.Type != EditorToolType.Separator)
					{
						EditorTool editorTool = editorToolBase as EditorTool;
						writer.RenderBeginTag(HtmlTextWriterTag.Li);
						editorTool.RenderControl(writer);
						writer.RenderEndTag();
					}
				}
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00052658 File Offset: 0x00050858
		private void RenderButton(EditorTool item, HtmlTextWriter writer)
		{
			if (!this._buttonGroupBeginTagIsRended)
			{
				this.RenderButtonGroupStart(writer);
			}
			item.RenderControl(writer);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00052670 File Offset: 0x00050870
		private void RenderButtonGroupStart(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reButtonGroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this._buttonGroupBeginTagIsRended = true;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0005268E File Offset: 0x0005088E
		private void RenderButtonGroupEnd(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
			this._buttonGroupBeginTagIsRended = false;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0005269D File Offset: 0x0005089D
		private void RenderDropDown(EditorTool item, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reDropDownTool");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			item.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x04000686 RID: 1670
		private bool _buttonGroupBeginTagIsRended;
	}
}
