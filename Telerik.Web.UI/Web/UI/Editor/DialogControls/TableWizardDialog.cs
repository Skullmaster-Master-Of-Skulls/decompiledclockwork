using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001058 RID: 4184
	[ClientScriptResource("Telerik.Web.UI.Widgets.TableWizard", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class TableWizardDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700363D RID: 13885
		// (get) Token: 0x0600A902 RID: 43266 RVA: 0x0024B81E File Offset: 0x00249A1E
		public override string DialogName
		{
			get
			{
				return "TableWizard";
			}
		}

		// Token: 0x0600A903 RID: 43267 RVA: 0x0024B828 File Offset: 0x00249A28
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("TableWizardTab");
			int num = (radTabStrip.Tabs.Count > 4) ? 4 : radTabStrip.Tabs.Count;
			for (int i = 0; i < num; i++)
			{
				radTabStrip.Tabs[i].Text = this.Localization.GetString(radTabStrip.Tabs[i].Value);
				radTabStrip.Tabs[i].ToolTip = this.Localization.GetString(radTabStrip.Tabs[i].Value);
			}
		}
	}
}
