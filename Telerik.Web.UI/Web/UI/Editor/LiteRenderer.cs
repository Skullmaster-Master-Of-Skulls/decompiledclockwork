using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C2 RID: 706
	public class LiteRenderer : EditorRendererBase
	{
		// Token: 0x06001894 RID: 6292 RVA: 0x00051758 File Offset: 0x0004F958
		public LiteRenderer(RadEditor owner) : base(owner)
		{
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00051761 File Offset: 0x0004F961
		public override string CssClassFormatString
		{
			get
			{
				if (base.Editor.EditType == EditorEditType.Inline)
				{
					return "RadEditor RadEditor_{0} reWrapper reInlineEditor";
				}
				return "RadEditor RadEditor_{0} reWrapper";
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0005177C File Offset: 0x0004F97C
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}Top", base.Editor.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Editor.ToolbarMode != EditorToolbarMode.Default || base.Editor.EditType == EditorEditType.Inline)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "0px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "0");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("RadEditor_{0} reToolBarWrapper", base.Editor.RuntimeSkin));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Editor.InDesignMode && base.Editor.Tools.Count == 0 && string.IsNullOrEmpty(base.Editor.ToolsFile))
			{
				base.Editor.LoadToolsFile();
				base.Editor.ForceEditorToolType();
				if (base.Editor.Modules.Count != 4 && (base.Editor.EnableComments || base.Editor.EnableTrackChanges))
				{
					int count = base.Editor.Modules.Count;
				}
			}
			base.Editor.ToolAdapter.Render(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}Left", base.Editor.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}Right", base.Editor.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}Center", base.Editor.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
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
			if (base.Editor.EditType != EditorEditType.Inline)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomProperties");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reRow");
				if ((base.Editor.EditModes == EditModes.Design || base.Editor.EditModes == EditModes.Html || base.Editor.EditModes == EditModes.Preview) && !base.Editor.ContainsModule("RadEditorStatistics"))
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reModes");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "_ModesWrapper"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				if (base.Editor.EditModes != EditModes.Design)
				{
					if ((base.Editor.EditModes & EditModes.Design) > (EditModes)0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Href, "#Design");
						writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Editor.Localization.Main.RadEditorDesignMode);
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "reSelectedMode");
						writer.RenderBeginTag(HtmlTextWriterTag.A);
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "reIcon reDesignIcon");
						writer.RenderBeginTag(HtmlTextWriterTag.Span);
						writer.RenderEndTag();
						writer.Write(base.Editor.Localization.Main.RadEditorDesignMode);
						writer.RenderEndTag();
					}
					if ((base.Editor.EditModes & EditModes.Html) > (EditModes)0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Href, "#Html");
						writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Editor.Localization.Main.RadEditorHtmlMode);
						writer.RenderBeginTag(HtmlTextWriterTag.A);
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "reIcon reHTMLIcon");
						writer.RenderBeginTag(HtmlTextWriterTag.Span);
						writer.RenderEndTag();
						writer.Write(base.Editor.Localization.Main.RadEditorHtmlMode);
						writer.RenderEndTag();
					}
					if ((base.Editor.EditModes & EditModes.Preview) > (EditModes)0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Href, "#Preview");
						writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Editor.Localization.Main.RadEditorPreviewMode);
						writer.RenderBeginTag(HtmlTextWriterTag.A);
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "reIcon rePreviewIcon");
						writer.RenderBeginTag(HtmlTextWriterTag.Span);
						writer.RenderEndTag();
						writer.Write(base.Editor.Localization.Main.RadEditorPreviewMode);
						writer.RenderEndTag();
					}
				}
				else
				{
					writer.Write(' ');
				}
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reBottomZone");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Bottom"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				if (!base.Editor.EnableResize || base.Editor.AutoResizeHeight || base.Editor.EditType == EditorEditType.Inline)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reResize rdwDoNotTransfrom");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "BottomResizer"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "Module"));
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reTableDiv");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}
	}
}
