using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200105E RID: 4190
	[ClientScriptResource("Telerik.Web.UI.Editor.ApplyClassDropDown", "Telerik.Web.UI.Common.Core.js")]
	public class ApplyClassDropDown : EditorToolsBase
	{
		// Token: 0x17003643 RID: 13891
		// (get) Token: 0x0600A919 RID: 43289 RVA: 0x0024BB03 File Offset: 0x00249D03
		public override string Name
		{
			get
			{
				return "ApplyClass";
			}
		}

		// Token: 0x17003644 RID: 13892
		// (get) Token: 0x0600A91A RID: 43290 RVA: 0x0024BB0A File Offset: 0x00249D0A
		// (set) Token: 0x0600A91B RID: 43291 RVA: 0x0024BB39 File Offset: 0x00249D39
		public string DefaultText
		{
			get
			{
				if (this.ViewState["DefaultText"] == null)
				{
					return "ApplyClass";
				}
				return (string)this.ViewState["DefaultText"];
			}
			set
			{
				this.ViewState["DefaultText"] = value;
			}
		}

		// Token: 0x17003645 RID: 13893
		// (get) Token: 0x0600A91C RID: 43292 RVA: 0x0024BB4C File Offset: 0x00249D4C
		protected override bool AddClientIDToRootTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A91D RID: 43293 RVA: 0x0024BB50 File Offset: 0x00249D50
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
