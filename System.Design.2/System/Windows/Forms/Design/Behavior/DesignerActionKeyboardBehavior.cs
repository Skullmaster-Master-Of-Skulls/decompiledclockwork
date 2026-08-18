using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037C RID: 892
	internal sealed class DesignerActionKeyboardBehavior : Behavior
	{
		// Token: 0x060024B3 RID: 9395 RVA: 0x000E29DC File Offset: 0x000E0BDC
		public DesignerActionKeyboardBehavior(DesignerActionPanel panel, IServiceProvider serviceProvider, BehaviorService behaviorService) : base(true, behaviorService)
		{
			this.panel = panel;
			if (serviceProvider != null)
			{
				this.menuService = (serviceProvider.GetService(typeof(IMenuCommandService)) as IMenuCommandService);
				this.daUISvc = (serviceProvider.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService);
			}
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000E2A34 File Offset: 0x000E0C34
		public override MenuCommand FindCommand(CommandID commandId)
		{
			if (this.panel != null && this.menuService != null)
			{
				foreach (CommandID obj in this.panel.FilteredCommandIDs)
				{
					if (commandId.Equals(obj))
					{
						return new MenuCommand(delegate(object <p0>, EventArgs <p1>)
						{
						}, commandId)
						{
							Enabled = false
						};
					}
				}
				if (this.daUISvc != null && commandId.Guid == DesignerActionKeyboardBehavior.VSStandardCommandSet97 && commandId.ID == 1124)
				{
					this.daUISvc.HideUI(null);
				}
			}
			return base.FindCommand(commandId);
		}

		// Token: 0x04001A89 RID: 6793
		private DesignerActionPanel panel;

		// Token: 0x04001A8A RID: 6794
		private IMenuCommandService menuService;

		// Token: 0x04001A8B RID: 6795
		private DesignerActionUIService daUISvc;

		// Token: 0x04001A8C RID: 6796
		private static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");
	}
}
