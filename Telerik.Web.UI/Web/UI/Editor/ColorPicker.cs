using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200105F RID: 4191
	[ClientScriptResource("Telerik.Web.UI.Editor.ColorPicker", "Telerik.Web.UI.Common.Core.js")]
	public class ColorPicker : EditorToolsBase
	{
		// Token: 0x17003646 RID: 13894
		// (get) Token: 0x0600A91F RID: 43295 RVA: 0x0024BBC9 File Offset: 0x00249DC9
		public override string Name
		{
			get
			{
				return "BackColor";
			}
		}

		// Token: 0x17003647 RID: 13895
		// (get) Token: 0x0600A920 RID: 43296 RVA: 0x0024BBD0 File Offset: 0x00249DD0
		// (set) Token: 0x0600A921 RID: 43297 RVA: 0x0024BBD8 File Offset: 0x00249DD8
		public string Title { get; set; }

		// Token: 0x17003648 RID: 13896
		// (get) Token: 0x0600A922 RID: 43298 RVA: 0x0024BBE1 File Offset: 0x00249DE1
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A923 RID: 43299 RVA: 0x0024BBE4 File Offset: 0x00249DE4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			EditorSplitButton editorSplitButton = new EditorSplitButton(this.Name);
			editorSplitButton.RenderMode = this.ResolvedRenderMode;
			editorSplitButton.Text = (this.Title ?? this.Name);
			editorSplitButton.Visible = this.Visible;
			editorSplitButton.Enabled = this.Enabled;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			editorSplitButton.RenderControl(writer);
			base.RenderContents(writer);
		}
	}
}
