using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000B3B RID: 2875
	[ClientScriptResource("Telerik.Web.UI.Widgets.InsertSelectDialog", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class InsertSelectDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x06006C8B RID: 27787 RVA: 0x001935A5 File Offset: 0x001917A5
		public override string DialogName
		{
			get
			{
				return "InsertSelectDialog";
			}
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x001935AC File Offset: 0x001917AC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.LocalizeControls();
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x001935BC File Offset: 0x001917BC
		private void LocalizeControls()
		{
			StandardButton standardButton = (StandardButton)base.FindControlRecursive("StyleBuilder");
			if (standardButton != null)
			{
				standardButton.Text = base.ToolsLocalization.StyleBuilder;
			}
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x001935EE File Offset: 0x001917EE
		private T FindControlRecursive<T>(string id) where T : Control
		{
			return (T)((object)base.FindControlRecursive(id));
		}
	}
}
