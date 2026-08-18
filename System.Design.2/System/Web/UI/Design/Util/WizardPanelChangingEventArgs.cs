using System;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200016E RID: 366
	internal class WizardPanelChangingEventArgs : EventArgs
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x000534CB File Offset: 0x000516CB
		public WizardPanelChangingEventArgs(WizardPanel currentPanel)
		{
			this._currentPanel = currentPanel;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000534DA File Offset: 0x000516DA
		public WizardPanel CurrentPanel
		{
			get
			{
				return this._currentPanel;
			}
		}

		// Token: 0x040007E1 RID: 2017
		private WizardPanel _currentPanel;
	}
}
