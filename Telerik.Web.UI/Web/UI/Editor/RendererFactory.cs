using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C4 RID: 708
	public class RendererFactory
	{
		// Token: 0x060018A4 RID: 6308 RVA: 0x0005204C File Offset: 0x0005024C
		public static IEditorRenderer GetRenderer(RadEditor editor)
		{
			switch (editor.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new LiteRenderer(editor);
			case RenderMode.Mobile:
				return new MobileRenderer(editor);
			}
			return new ClassicRenderer(editor);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005208C File Offset: 0x0005028C
		public static IEditorRenderer GetRenderer(EditorToolBar toolBar)
		{
			switch (toolBar.RenderMode)
			{
			case RenderMode.Lightweight:
				return new LiteToolBarRenderer(toolBar);
			case RenderMode.Mobile:
				return new MobileToolBarRenderer(toolBar);
			}
			return new ClassicToolBarRenderer(toolBar);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000520CA File Offset: 0x000502CA
		public static IEditorRenderer GetRenderer(HeaderToolsToolBar toolBar)
		{
			return new HeaderToolBarRenderer(toolBar);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000520D4 File Offset: 0x000502D4
		public static IEditorToolRenderer GetRenderer(EditorTool tool)
		{
			EditorHeaderTool editorHeaderTool = tool as EditorHeaderTool;
			if (editorHeaderTool != null)
			{
				return new HeaderToolRenderer(editorHeaderTool);
			}
			switch (tool.RenderMode)
			{
			case RenderMode.Lightweight:
				return new LiteToolRenderer(tool);
			case RenderMode.Mobile:
				return new MobileToolRenderer(tool);
			}
			return new ClassicToolRenderer(tool);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00052124 File Offset: 0x00050324
		public static IEditorToolRenderer GetRenderer(EditorDropDown dropDown)
		{
			switch (dropDown.RenderMode)
			{
			case RenderMode.Lightweight:
				return new LiteDropDownRenderer(dropDown);
			case RenderMode.Mobile:
				return new MobileDropDownRenderer(dropDown);
			}
			return new ClassicDropDownRenderer(dropDown);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00052164 File Offset: 0x00050364
		public static IEditorToolRenderer GetRenderer(EditorSplitButton splitButton)
		{
			switch (splitButton.RenderMode)
			{
			case RenderMode.Lightweight:
				return new LiteSplitButtonRenderer(splitButton);
			case RenderMode.Mobile:
				return new MobileSplitButtonRenderer(splitButton);
			}
			return new ClassicSplitButtonRenderer(splitButton);
		}
	}
}
