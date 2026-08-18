using System;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C0 RID: 704
	public class ClassicRenderer : EditorRendererBase
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x00050A66 File Offset: 0x0004EC66
		public ClassicRenderer(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00050A70 File Offset: 0x0004EC70
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Editor.InDesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(base.Editor));
				writer.Write("<style type='text/css'>.reToolbar {float: none !important;}</style>");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}Wrapper", base.Editor.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reLayoutWrapper");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Editor.Height.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (!base.Editor.InDesignMode)
			{
				this.RenderCaption(writer, "RadEditor - HTML WYSIWYG Editor. MS Word-like content editing experience thanks to a rich set of formatting tools, dropdowns, dialogs, system modules and built-in spell-check.");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Thead);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				this.RenderTh(writer, "RadEditor's components - toolbar, content area, modes and modules", "col");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!base.Editor.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				this.RenderTh(writer, string.Empty, "row");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_corner reCorner_top_left");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_center reCenter_top");
			writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_corner reCorner_top_right");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!base.Editor.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				this.RenderTh(writer, "Toolbar's wrapper", "row");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reLeftVerticalSide");
			writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, "4");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, "4");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Left"));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reTlbVertical");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Top"));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolCell");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (!base.Editor.InDesignMode)
			{
				HttpBrowserCapabilities browser = base.Editor.ControlContext.Request.Browser;
				if (browser.Browser == "IE")
				{
					decimal d = 0m;
					if (decimal.TryParse(browser.Version, out d) && d < 9.0m)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
						writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "0px");
					}
				}
			}
			if (base.Editor.ToolbarMode != EditorToolbarMode.Default || base.Editor.EditType == EditorEditType.Inline)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "0px");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, base.Editor.RuntimeSkin + " reToolbarWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			bool flag = false;
			bool flag2 = false;
			if (base.Editor.InDesignMode && base.Editor.Tools.Count == 0 && string.IsNullOrEmpty(base.Editor.ToolsFile))
			{
				base.Editor.LoadToolsFile();
				base.Editor.ForceEditorToolType();
				flag = true;
				if (base.Editor.Modules.Count == 4 || ((base.Editor.EnableComments || base.Editor.EnableTrackChanges) && base.Editor.Modules.Count == 5))
				{
					flag2 = true;
				}
			}
			base.Editor.ToolAdapter.Render(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, "4");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Right"));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reTlbVertical");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, "4");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reRightVerticalSide");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!base.Editor.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				this.RenderTh(writer, "Content area wrapper", "row");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Center"));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("reContentCell{0}", (base.Editor.ContentAreaMode == EditorContentAreaMode.Div) ? " reNoBorder" : ""));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (!base.Editor.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.AddAttribute(HtmlTextWriterAttribute.For, string.Format("{0}{1}", base.Editor.ClientID, "ContentHiddenTextarea"));
				writer.RenderBeginTag(HtmlTextWriterTag.Label);
				writer.Write("RadEditor hidden textarea");
				writer.RenderEndTag();
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "ContentHiddenTextarea"));
				writer.AddAttribute(HtmlTextWriterAttribute.Name, base.Editor.UniqueID);
				writer.AddAttribute(HtmlTextWriterAttribute.Rows, "4");
				writer.AddAttribute(HtmlTextWriterAttribute.Cols, "20");
				writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
				writer.Write(ContentEncoder.Encode(base.Editor.Content));
				writer.RenderEndTag();
			}
			else if (base.Editor.InDesignMode && base.Editor.Tools.Count == 0 && !string.IsNullOrEmpty(base.Editor.ToolsFile))
			{
				writer.Write("<p>The tools of the control will be loaded form the ToolsFile property.</p>");
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (base.Editor.EditType != EditorEditType.Inline)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (!base.Editor.InDesignMode)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					this.RenderTh(writer, "RadEditor's bottom area: Design, Html and Preview modes, Statistics module and resize handle.", "row");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolZone");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderBottomZone(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (!base.Editor.InDesignMode)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
					this.RenderTh(writer, "RadEditor's Modules - special tools used to provide extra information such as Tag Inspector, Real Time HTML Viewer, Tag Properties and other.", "row");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Module"));
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolZone");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!base.Editor.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				this.RenderTh(writer, string.Empty, "row");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_corner reCorner_bottom_left");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_center reCenter_bottom");
			writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "3");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reWrapper_corner reCorner_bottom_right");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (flag)
			{
				base.Editor.Tools.Clear();
			}
			if (flag2)
			{
				base.Editor.Modules.Clear();
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000512A8 File Offset: 0x0004F4A8
		public override string CssClassFormatString
		{
			get
			{
				if (base.Editor.EditType == EditorEditType.Inline)
				{
					return "RadEditor {0} reWrapper reInlineEditor";
				}
				return "RadEditor {0} reWrapper";
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000512C4 File Offset: 0x0004F4C4
		protected virtual void RenderBottomZone(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomTable");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			if (!base.Editor.InDesignMode)
			{
				HttpBrowserCapabilities browser = base.Editor.ControlContext.Request.Browser;
				if (browser.Browser == "IE")
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_BottomTable", base.Editor.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (!base.Editor.InDesignMode)
			{
				this.RenderCaption(writer, "It contains RadEditor's Modes/views (HTML, Design and Preview), Statistics and Resizer");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Thead);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				this.RenderTh(writer, "Editor Mode buttons", "col");
				this.RenderTh(writer, "Statistics module", "col");
				this.RenderTh(writer, "Editor resizer", "col");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			if (base.Editor.EditModes == EditModes.Design || base.Editor.EditModes == EditModes.Html || base.Editor.EditModes == EditModes.Preview)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			if (base.Editor.EditType != EditorEditType.Inline)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reEditorModesCell");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderEditModes(writer);
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomZone");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Bottom"));
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
			if (base.Editor.EnableResize && !base.Editor.AutoResizeHeight)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reResizeCell");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "15px");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "BottomResizer"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
			if (base.Editor.EditType != EditorEditType.Inline)
			{
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Noscript);
			writer.RenderBeginTag(HtmlTextWriterTag.P);
			writer.Write("RadEditor - please enable JavaScript to use the rich text editor.");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00051568 File Offset: 0x0004F768
		protected virtual void RenderEditModes(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reEditorModes");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "_ModesWrapper"));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Editor.EditModes != EditModes.Design)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				if ((base.Editor.EditModes & EditModes.Design) > (EditModes)0)
				{
					ClassicRenderer.Render_LI_A_SPAN(writer, base.Editor.Localization.Main.RadEditorDesignMode, base.Editor.Localization.Main.RadEditorDesignMode, "reMode_design reMode_selected");
				}
				if ((base.Editor.EditModes & EditModes.Html) > (EditModes)0)
				{
					ClassicRenderer.Render_LI_A_SPAN(writer, base.Editor.Localization.Main.RadEditorHtmlMode, base.Editor.Localization.Main.RadEditorHtmlMode, "reMode_html");
				}
				if ((base.Editor.EditModes & EditModes.Preview) > (EditModes)0)
				{
					ClassicRenderer.Render_LI_A_SPAN(writer, base.Editor.Localization.Main.RadEditorPreviewMode, base.Editor.Localization.Main.RadEditorPreviewMode, "reMode_preview");
				}
				writer.RenderEndTag();
			}
			else
			{
				writer.Write(' ');
			}
			writer.RenderEndTag();
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000516AA File Offset: 0x0004F8AA
		private void RenderCaption(HtmlTextWriter writer, string text)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000516CE File Offset: 0x0004F8CE
		private void RenderTh(HtmlTextWriter writer, string text, string scope)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, scope);
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(text);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000516FC File Offset: 0x0004F8FC
		private static void Render_LI_A_SPAN(HtmlTextWriter writer, string anchorTitle, string spanText, string className)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0);");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, anchorTitle);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, className);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(spanText);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
