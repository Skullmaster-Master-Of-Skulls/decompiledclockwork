using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000068 RID: 104
	internal class RadAjaxAsyncPostbackTrigger
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000B6E3 File Offset: 0x000098E3
		public RadAjaxAsyncPostbackTrigger(RadAjaxControl owner, string eventName)
		{
			this._owner = owner;
			this._eventName = eventName;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B6FC File Offset: 0x000098FC
		public void OnEvent(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				this._owner.PostbackTriggerInitiatorUniqueID = control.UniqueID;
				this._owner.PostbackTriggerEventName = this._eventName;
			}
		}

		// Token: 0x04000083 RID: 131
		private RadAjaxControl _owner;

		// Token: 0x04000084 RID: 132
		private string _eventName;
	}
}
