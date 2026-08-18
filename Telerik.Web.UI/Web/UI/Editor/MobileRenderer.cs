using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C3 RID: 707
	public class MobileRenderer : EditorRendererBase
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x00051DCA File Offset: 0x0004FFCA
		public MobileRenderer(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00051DD3 File Offset: 0x0004FFD3
		public override string CssClassFormatString
		{
			get
			{
				return "RadEditor RadEditor_{0} reWrapper t-vbox";
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00051DDA File Offset: 0x0004FFDA
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderHeader(writer);
			this.RenderHeaderTools(writer);
			this.RenderContentArea(writer);
			this.RenderToolZone(writer);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00051DF8 File Offset: 0x0004FFF8
		private void RenderHeader(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reHeader t-hbox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			string text = "EditContent";
			HeaderToolRenderer headerToolRenderer = new HeaderToolRenderer(new EditorHeaderTool(text)
			{
				Text = base.Editor.Localization.Tools.GetString(text, false)
			});
			headerToolRenderer.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00051E58 File Offset: 0x00050058
		private void RenderHeaderTools(HtmlTextWriter writer)
		{
			if (base.Editor.HeaderTools.Count > 0)
			{
				base.Editor.HeaderToolsToolAdapter.Render(writer);
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00051E7E File Offset: 0x0005007E
		private void RenderContentArea(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reContent t-flex t-vbox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderHiddenTextArea(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00051EA4 File Offset: 0x000500A4
		private void RenderHiddenTextArea(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", base.Editor.ClientID, "ContentHiddenTextarea"));
			writer.AddAttribute(HtmlTextWriterAttribute.Name, base.Editor.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Rows, "4");
			writer.AddAttribute(HtmlTextWriterAttribute.Cols, "20");
			writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
			writer.Write(ContentEncoder.Encode(base.Editor.Content));
			writer.RenderEndTag();
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00051F34 File Offset: 0x00050134
		private void RenderToolZone(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolZone t-vbox reHidden");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderModulesContainer(writer);
			this.RenderTabChooserContainer(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolView t-vbox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderToolContent(writer);
			this.RenderDropDownsContainer(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00051FA0 File Offset: 0x000501A0
		private void RenderModulesContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reModules t-vbox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00051FBD File Offset: 0x000501BD
		private void RenderTabChooserContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolHeader t-hbox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reTabChooser");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00051FF5 File Offset: 0x000501F5
		private void RenderToolContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderTools(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00052019 File Offset: 0x00050219
		private void RenderDropDownsContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reDropDownsContainer t-flex");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00052036 File Offset: 0x00050236
		private void RenderTools(HtmlTextWriter writer)
		{
			base.Editor.ToolAdapter.Render(writer);
		}
	}
}
