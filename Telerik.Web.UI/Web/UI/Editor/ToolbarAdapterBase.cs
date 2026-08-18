using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DA RID: 730
	internal abstract class ToolbarAdapterBase : ToolAdapter
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x000532F6 File Offset: 0x000514F6
		public ToolbarAdapterBase()
		{
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00053309 File Offset: 0x00051509
		public ToolbarAdapterBase(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00053320 File Offset: 0x00051520
		public override void PreRender()
		{
			this._toolbars.Clear();
			string runtimeSkin = base.Editor.RuntimeSkin;
			foreach (object obj in base.Editor.Tools)
			{
				EditorToolGroup editorToolGroup = (EditorToolGroup)obj;
				EditorToolBar editorToolBar = new EditorToolBar();
				editorToolBar.Skin = runtimeSkin;
				editorToolBar.RenderMode = base.Editor.ResolvedRenderMode;
				foreach (object obj2 in editorToolGroup.Tools)
				{
					EditorToolBase editorToolBase = (EditorToolBase)obj2;
					editorToolBase.RenderMode = editorToolBar.RenderMode;
					EditorToolBase toolUIObject = this.GetToolUIObject(editorToolBase);
					if (toolUIObject != null)
					{
						editorToolBar.Items.Add(toolUIObject);
					}
				}
				this._toolbars.Add(editorToolBar);
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00053434 File Offset: 0x00051634
		public override void Render(HtmlTextWriter writer)
		{
			foreach (EditorToolBar editorToolBar in this._toolbars)
			{
				editorToolBar.RenderControl(writer);
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00053488 File Offset: 0x00051688
		internal EditorToolBase GetToolUIObject(EditorToolBase tool)
		{
			EditorTool editorTool = null;
			EditorTool tool2 = tool as EditorTool;
			EditorToolType type = tool.Type;
			switch (type)
			{
			case EditorToolType.Button:
				editorTool = new EditorTool(tool2);
				break;
			case EditorToolType.DropDown:
			{
				EditorDropDown editorDropDown = new EditorDropDown(tool2);
				ToolbarAdapterBase.ConfigureDropDown(editorDropDown);
				this.SetText(editorDropDown);
				editorTool = editorDropDown;
				break;
			}
			case (EditorToolType)3:
				break;
			case EditorToolType.SplitButton:
			{
				EditorSplitButton editorSplitButton = new EditorSplitButton(tool2);
				ToolbarAdapterBase.ConfigureDropDown(editorSplitButton);
				editorTool = editorSplitButton;
				break;
			}
			default:
				if (type == EditorToolType.Separator)
				{
					return new EditorSeparator();
				}
				if (type == EditorToolType.ToolStrip)
				{
					EditorSplitButton editorSplitButton2 = new EditorSplitButton(tool as EditorToolStrip);
					ToolbarAdapterBase.ConfigureToolStrip(editorSplitButton2);
					editorTool = editorSplitButton2;
				}
				break;
			}
			if (tool.Type != EditorToolType.Separator)
			{
				this.SetText(editorTool);
			}
			return editorTool;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0005352D File Offset: 0x0005172D
		private void SetText(EditorTool button)
		{
			if (string.IsNullOrEmpty(button.Text))
			{
				button.Text = base.Editor.Localization.Tools.GetString(button.Name, false);
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00053560 File Offset: 0x00051760
		private static void ConfigureToolStrip(EditorSplitButton strip)
		{
			string text = strip.Name.ToLowerInvariant();
			AttributeCollection attributes = strip.Attributes;
			string a;
			if ((a = text) != null)
			{
				if (!(a == "inserttable"))
				{
					if (!(a == "formatstripper") && !(a == "insertformelement") && !(a == "pastestrip") && !(a == "formatpainter"))
					{
						return;
					}
					if (attributes["sizetofit"] == null)
					{
						attributes["sizetofit"] = "true";
					}
				}
				else
				{
					if (attributes["popupclassname"] == null)
					{
						attributes["popupclassname"] = "reInsertTable";
					}
					if (attributes["itemsperrow"] == null)
					{
						attributes["itemsperrow"] = "8";
					}
					if (attributes["sizetofit"] == null)
					{
						attributes["sizetofit"] = "true";
						return;
					}
				}
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00053644 File Offset: 0x00051844
		private static void ConfigureDropDown(EditorTool tool)
		{
			string text = tool.Name.ToLowerInvariant();
			AttributeCollection attributes = tool.Attributes;
			string key;
			switch (key = text)
			{
			case "insertcustomlink":
				if (attributes["popupclassname"] == null)
				{
					attributes["popupclassname"] = "reCustomLinks";
				}
				break;
			case "insertsymbol":
				if (attributes["itemsperrow"] == null)
				{
					attributes["itemsperrow"] = "8";
				}
				if (attributes["sizetofit"] == null)
				{
					attributes["sizetofit"] = "true";
				}
				if (attributes["popupclassname"] == null)
				{
					attributes["popupclassname"] = "reInsertSymbol";
				}
				break;
			case "undo":
			case "redo":
				if (attributes["popupclassname"] == null)
				{
					attributes["popupclassname"] = "reUndoRedo";
				}
				break;
			case "backcolor":
			case "forecolor":
				if (attributes["popupclassname"] == null)
				{
					attributes["popupclassname"] = "reColorPicker";
				}
				if (attributes["itemsperrow"] == null)
				{
					attributes["itemsperrow"] = "10";
				}
				if (attributes["sizetofit"] == null)
				{
					attributes["sizetofit"] = "true";
				}
				break;
			case "applyclass":
				if (attributes["popupclassname"] == null)
				{
					attributes["popupclassname"] = "reApplyClass";
				}
				break;
			}
			if (!(tool is EditorDropDown))
			{
				return;
			}
			string a;
			if ((a = text) != null)
			{
				if (!(a == "fontsize"))
				{
					if (!(a == "realfontsize"))
					{
						if (!(a == "zoom"))
						{
							return;
						}
						if (attributes["popupwidth"] == null)
						{
							attributes["popupwidth"] = "5em";
						}
					}
					else if (attributes["popupwidth"] == null)
					{
						attributes["popupwidth"] = "5em";
						return;
					}
				}
				else
				{
					if (attributes["popupwidth"] == null)
					{
						attributes["popupwidth"] = "5em";
					}
					if (attributes["sizetofit"] == null)
					{
						attributes["sizetofit"] = "true";
						return;
					}
				}
			}
		}

		// Token: 0x04000695 RID: 1685
		private readonly List<EditorToolBar> _toolbars = new List<EditorToolBar>();
	}
}
