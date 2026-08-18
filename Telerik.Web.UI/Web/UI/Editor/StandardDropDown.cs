using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001095 RID: 4245
	[ClientScriptResource("Telerik.Web.UI.EditorSelfUpdateableDropDown", "Telerik.Web.UI.Common.Core.js")]
	public class StandardDropDown : EditorToolsBase
	{
		// Token: 0x170037CD RID: 14285
		// (get) Token: 0x0600AC92 RID: 44178 RVA: 0x00250973 File Offset: 0x0024EB73
		public override string Name
		{
			get
			{
				return "DropDown";
			}
		}

		// Token: 0x170037CE RID: 14286
		// (get) Token: 0x0600AC93 RID: 44179 RVA: 0x0025097A File Offset: 0x0024EB7A
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600AC94 RID: 44180 RVA: 0x00250980 File Offset: 0x0024EB80
		protected override void RenderContents(HtmlTextWriter writer)
		{
			EditorDropDown editorDropDown = new EditorDropDown(this.Name);
			editorDropDown.RenderMode = this.ResolvedRenderMode;
			editorDropDown.Visible = this.Visible;
			editorDropDown.Enabled = this.Enabled;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			editorDropDown.RenderControl(writer);
			base.RenderContents(writer);
		}
	}
}
