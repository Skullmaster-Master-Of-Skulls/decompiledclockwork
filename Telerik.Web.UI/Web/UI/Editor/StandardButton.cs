using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001094 RID: 4244
	[ClientScriptResource("Telerik.Web.UI.EditorButton", "Telerik.Web.UI.Common.Core.js")]
	public class StandardButton : EditorToolsBase
	{
		// Token: 0x0600AC89 RID: 44169 RVA: 0x0025088C File Offset: 0x0024EA8C
		public StandardButton()
		{
			this._name = "StandardButton";
		}

		// Token: 0x0600AC8A RID: 44170 RVA: 0x0025089F File Offset: 0x0024EA9F
		public StandardButton(string name)
		{
			this._name = name;
		}

		// Token: 0x170037C9 RID: 14281
		// (get) Token: 0x0600AC8B RID: 44171 RVA: 0x002508AE File Offset: 0x0024EAAE
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170037CA RID: 14282
		// (get) Token: 0x0600AC8C RID: 44172 RVA: 0x002508B6 File Offset: 0x0024EAB6
		// (set) Token: 0x0600AC8D RID: 44173 RVA: 0x002508BE File Offset: 0x0024EABE
		public string ToolName
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170037CB RID: 14283
		// (get) Token: 0x0600AC8E RID: 44174 RVA: 0x002508C7 File Offset: 0x0024EAC7
		// (set) Token: 0x0600AC8F RID: 44175 RVA: 0x002508CF File Offset: 0x0024EACF
		public string Text { get; set; }

		// Token: 0x170037CC RID: 14284
		// (get) Token: 0x0600AC90 RID: 44176 RVA: 0x002508D8 File Offset: 0x0024EAD8
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600AC91 RID: 44177 RVA: 0x002508DC File Offset: 0x0024EADC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this._name == "StandardButton")
			{
				throw new ArgumentException("The ToolName property must be set!");
			}
			EditorTool editorTool = new EditorTool(this.Name);
			editorTool.RenderMode = this.ResolvedRenderMode;
			editorTool.Text = ((!string.IsNullOrEmpty(this.Text)) ? this.Text : this.Name);
			editorTool.Visible = this.Visible;
			editorTool.Enabled = this.Enabled;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			editorTool.RenderControl(writer);
			base.RenderContents(writer);
		}

		// Token: 0x04002DC0 RID: 11712
		private string _name;
	}
}
