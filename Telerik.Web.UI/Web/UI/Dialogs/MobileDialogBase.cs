using System;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x0200026E RID: 622
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileDialogBase", "Telerik.Web.UI.Dialogs.MobileDialogBase.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(DialogControlInitializer))]
	public abstract class MobileDialogBase : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x0004C834 File Offset: 0x0004AA34
		protected override void CreateChildControls()
		{
			this.titlebar = new MobileDialogTitleBar
			{
				ID = "MobileDialogTitleBar",
				Title = this.Title
			};
			this.Controls.Add(this.titlebar);
			base.CreateChildControls();
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0004C87C File Offset: 0x0004AA7C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddComponentProperty("titlebar", this.titlebar.ClientID);
			descriptor.AddProperty("_dialogName", this.DialogName);
		}

		// Token: 0x040005F3 RID: 1523
		protected MobileDialogTitleBar titlebar;
	}
}
