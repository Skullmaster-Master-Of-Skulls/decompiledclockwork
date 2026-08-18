using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200136C RID: 4972
	public class AjaxSettingCreatingEventArgs : EventArgs
	{
		// Token: 0x0600CF98 RID: 53144 RVA: 0x002E0D18 File Offset: 0x002DEF18
		public AjaxSettingCreatingEventArgs(Control initator, Control updated, UpdatePanel updatePanel)
		{
			this.initator = initator;
			this.updated = updated;
			this.updatePanel = updatePanel;
		}

		// Token: 0x170042BC RID: 17084
		// (get) Token: 0x0600CF99 RID: 53145 RVA: 0x002E0D35 File Offset: 0x002DEF35
		// (set) Token: 0x0600CF9A RID: 53146 RVA: 0x002E0D3D File Offset: 0x002DEF3D
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
			set
			{
				this.canceled = value;
			}
		}

		// Token: 0x170042BD RID: 17085
		// (get) Token: 0x0600CF9B RID: 53147 RVA: 0x002E0D46 File Offset: 0x002DEF46
		// (set) Token: 0x0600CF9C RID: 53148 RVA: 0x002E0D4E File Offset: 0x002DEF4E
		public Control Initiator
		{
			get
			{
				return this.initator;
			}
			set
			{
				this.initator = value;
			}
		}

		// Token: 0x170042BE RID: 17086
		// (get) Token: 0x0600CF9D RID: 53149 RVA: 0x002E0D57 File Offset: 0x002DEF57
		// (set) Token: 0x0600CF9E RID: 53150 RVA: 0x002E0D5F File Offset: 0x002DEF5F
		public Control Updated
		{
			get
			{
				return this.updated;
			}
			set
			{
				this.updated = value;
			}
		}

		// Token: 0x170042BF RID: 17087
		// (get) Token: 0x0600CF9F RID: 53151 RVA: 0x002E0D68 File Offset: 0x002DEF68
		public UpdatePanel UpdatePanel
		{
			get
			{
				return this.updatePanel;
			}
		}

		// Token: 0x040037A9 RID: 14249
		private Control initator;

		// Token: 0x040037AA RID: 14250
		private Control updated;

		// Token: 0x040037AB RID: 14251
		private UpdatePanel updatePanel;

		// Token: 0x040037AC RID: 14252
		private bool canceled;
	}
}
