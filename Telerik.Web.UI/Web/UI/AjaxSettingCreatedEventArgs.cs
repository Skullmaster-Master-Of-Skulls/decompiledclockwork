using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200136B RID: 4971
	public class AjaxSettingCreatedEventArgs : EventArgs
	{
		// Token: 0x0600CF94 RID: 53140 RVA: 0x002E0CE3 File Offset: 0x002DEEE3
		public AjaxSettingCreatedEventArgs(Control initator, Control updated, UpdatePanel updatePanel)
		{
			this.initator = initator;
			this.updated = updated;
			this.updatePanel = updatePanel;
		}

		// Token: 0x170042B9 RID: 17081
		// (get) Token: 0x0600CF95 RID: 53141 RVA: 0x002E0D00 File Offset: 0x002DEF00
		public Control Initiator
		{
			get
			{
				return this.initator;
			}
		}

		// Token: 0x170042BA RID: 17082
		// (get) Token: 0x0600CF96 RID: 53142 RVA: 0x002E0D08 File Offset: 0x002DEF08
		public Control Updated
		{
			get
			{
				return this.updated;
			}
		}

		// Token: 0x170042BB RID: 17083
		// (get) Token: 0x0600CF97 RID: 53143 RVA: 0x002E0D10 File Offset: 0x002DEF10
		public UpdatePanel UpdatePanel
		{
			get
			{
				return this.updatePanel;
			}
		}

		// Token: 0x040037A6 RID: 14246
		private Control initator;

		// Token: 0x040037A7 RID: 14247
		private Control updated;

		// Token: 0x040037A8 RID: 14248
		private UpdatePanel updatePanel;
	}
}
