using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001069 RID: 4201
	[ClientScriptResource("Telerik.Web.UI.Widgets.LinkManager", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class LinkManagerDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17003673 RID: 13939
		// (get) Token: 0x0600A988 RID: 43400 RVA: 0x0024D18E File Offset: 0x0024B38E
		public override string DialogName
		{
			get
			{
				return "LinkManager";
			}
		}

		// Token: 0x17003674 RID: 13940
		// (get) Token: 0x0600A989 RID: 43401 RVA: 0x0024D198 File Offset: 0x0024B398
		// (set) Token: 0x0600A98A RID: 43402 RVA: 0x0024D1C1 File Offset: 0x0024B3C1
		[DefaultValue(true)]
		public bool StandAlone
		{
			get
			{
				object obj = this.ViewState["StandAlone"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["StandAlone"] = value;
			}
		}

		// Token: 0x0600A98B RID: 43403 RVA: 0x0024D1DC File Offset: 0x0024B3DC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("LinkManagerTab");
			int num = (radTabStrip.Tabs.Count > 3) ? 3 : radTabStrip.Tabs.Count;
			for (int i = 0; i < num; i++)
			{
				radTabStrip.Tabs[i].Text = this.Localization.GetString(radTabStrip.Tabs[i].Value);
				radTabStrip.Tabs[i].ToolTip = this.Localization.GetString(radTabStrip.Tabs[i].Value);
			}
			StandardButton standardButton = (StandardButton)base.FindControlRecursive("DocumentManagerCaller");
			if (standardButton != null)
			{
				standardButton.Text = base.ToolsLocalization.DocumentManager;
			}
			if (!this.StandAlone)
			{
				PlaceHolder placeHolder = this.FindControl("controlButtonsRow") as PlaceHolder;
				if (placeHolder != null)
				{
					placeHolder.Visible = false;
				}
				PlaceHolder placeHolder2 = this.FindControl("documentCallerRow") as PlaceHolder;
				if (placeHolder2 != null)
				{
					placeHolder2.Visible = false;
				}
				PlaceHolder placeHolder3 = this.FindControl("existingAnchorRow") as PlaceHolder;
				if (placeHolder3 != null)
				{
					placeHolder3.Visible = false;
				}
				if (radTabStrip.Tabs.Count > 2)
				{
					radTabStrip.Tabs[1].Visible = false;
					radTabStrip.Tabs[2].Visible = false;
				}
			}
		}
	}
}
