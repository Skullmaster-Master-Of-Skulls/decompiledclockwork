using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200105D RID: 4189
	[ClientScriptResource("Telerik.Web.UI.Editor.AlignmentSelector", "Telerik.Web.UI.Common.Core.js")]
	public class AlignmentSelector : EditorToolsBase
	{
		// Token: 0x17003640 RID: 13888
		// (get) Token: 0x0600A913 RID: 43283 RVA: 0x0024BA70 File Offset: 0x00249C70
		public override string Name
		{
			get
			{
				return "AlignmentSelector";
			}
		}

		// Token: 0x17003641 RID: 13889
		// (get) Token: 0x0600A914 RID: 43284 RVA: 0x0024BA77 File Offset: 0x00249C77
		// (set) Token: 0x0600A915 RID: 43285 RVA: 0x0024BA7F File Offset: 0x00249C7F
		public string Title { get; set; }

		// Token: 0x17003642 RID: 13890
		// (get) Token: 0x0600A916 RID: 43286 RVA: 0x0024BA88 File Offset: 0x00249C88
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A917 RID: 43287 RVA: 0x0024BA8C File Offset: 0x00249C8C
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
