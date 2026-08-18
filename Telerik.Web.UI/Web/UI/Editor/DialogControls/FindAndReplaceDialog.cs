using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001056 RID: 4182
	[ClientScriptResource("Telerik.Web.UI.Widgets.FindAndReplace", "Telerik.Web.UI.Common.Core.js")]
	[RequiredScript(typeof(RadEditor))]
	[ToolboxItem(false)]
	public class FindAndReplaceDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17003639 RID: 13881
		// (get) Token: 0x0600A8F8 RID: 43256 RVA: 0x0024B632 File Offset: 0x00249832
		public override string DialogName
		{
			get
			{
				return "FindAndReplace";
			}
		}

		// Token: 0x0600A8F9 RID: 43257 RVA: 0x0024B63C File Offset: 0x0024983C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("dialogtabstrip");
			int num = (radTabStrip.Tabs.Count > 2) ? 2 : radTabStrip.Tabs.Count;
			for (int i = 0; i < num; i++)
			{
				radTabStrip.Tabs[i].Text = this.Localization.GetString(radTabStrip.Tabs[i].Value);
				radTabStrip.Tabs[i].ToolTip = this.Localization.GetString(radTabStrip.Tabs[i].Value);
			}
		}
	}
}
