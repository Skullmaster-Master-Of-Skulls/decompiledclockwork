using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A11 RID: 6673
	public class AppointmentContextMenuItemClickingEventArgs : SchedulerCancelEventArgs
	{
		// Token: 0x06010259 RID: 66137 RVA: 0x0039FA2C File Offset: 0x0039DC2C
		public AppointmentContextMenuItemClickingEventArgs(Appointment appointment, RadMenuItem menuItem) : base(appointment)
		{
			this._menuItem = menuItem;
		}

		// Token: 0x17004DF3 RID: 19955
		// (get) Token: 0x0601025A RID: 66138 RVA: 0x0039FA3C File Offset: 0x0039DC3C
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x04004915 RID: 18709
		private readonly RadMenuItem _menuItem;
	}
}
