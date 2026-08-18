using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001060 RID: 4192
	[ClientScriptResource("Telerik.Web.UI.ImageDialogCaller", "Telerik.Web.UI.Common.Core.js")]
	public class ImageDialogCaller : EditorToolsBase, INamingContainer
	{
		// Token: 0x17003649 RID: 13897
		// (get) Token: 0x0600A925 RID: 43301 RVA: 0x0024BC5B File Offset: 0x00249E5B
		public override string Name
		{
			get
			{
				return "ImageDialogCaller";
			}
		}

		// Token: 0x1700364A RID: 13898
		// (get) Token: 0x0600A926 RID: 43302 RVA: 0x0024BC62 File Offset: 0x00249E62
		// (set) Token: 0x0600A927 RID: 43303 RVA: 0x0024BC6A File Offset: 0x00249E6A
		public string Text { get; set; }

		// Token: 0x0600A928 RID: 43304 RVA: 0x0024BC73 File Offset: 0x00249E73
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		// Token: 0x0600A929 RID: 43305 RVA: 0x0024BC82 File Offset: 0x00249E82
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.button.ID = "CallerButton";
			this.Controls.Add(this.button);
		}

		// Token: 0x0600A92A RID: 43306 RVA: 0x0024BCAC File Offset: 0x00249EAC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reImageDialogCaller");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, "redInputToolTextInput");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "redInputTool");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (!string.IsNullOrEmpty(this.Text))
			{
				this.button.Text = this.Text;
			}
			base.RenderContents(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x04002DB1 RID: 11697
		private StandardButton button = new StandardButton("ImageManager");
	}
}
