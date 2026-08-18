using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000B4B RID: 2891
	[ClientScriptResource("Telerik.Web.UI.Editor.FormatSetsDropDown", "Telerik.Web.UI.Common.Core.js")]
	public class FormatSetsDropDown : EditorToolsBase
	{
		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x00194905 File Offset: 0x00192B05
		public override string Name
		{
			get
			{
				return "FormatSets";
			}
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06006CF7 RID: 27895 RVA: 0x0019490C File Offset: 0x00192B0C
		// (set) Token: 0x06006CF8 RID: 27896 RVA: 0x0019493B File Offset: 0x00192B3B
		public string DefaultText
		{
			get
			{
				if (this.ViewState["DefaultText"] == null)
				{
					return "FormatSetsDropDown";
				}
				return (string)this.ViewState["DefaultText"];
			}
			set
			{
				this.ViewState["DefaultText"] = value;
			}
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x0019494E File Offset: 0x00192B4E
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x00194954 File Offset: 0x00192B54
		protected override void RenderContents(HtmlTextWriter writer)
		{
			EditorDropDown editorDropDown = new EditorDropDown(this.Name);
			editorDropDown.RenderMode = this.ResolvedRenderMode;
			editorDropDown.Text = this.DefaultText;
			editorDropDown.Visible = this.Visible;
			editorDropDown.Enabled = this.Enabled;
			editorDropDown.Width = this.Width;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			editorDropDown.RenderControl(writer);
			base.RenderContents(writer);
		}
	}
}
