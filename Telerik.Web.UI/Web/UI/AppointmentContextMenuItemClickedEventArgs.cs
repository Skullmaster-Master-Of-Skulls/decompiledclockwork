using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A10 RID: 6672
	public class AppointmentContextMenuItemClickedEventArgs : SchedulerEventArgs
	{
		// Token: 0x06010257 RID: 66135 RVA: 0x0039FA14 File Offset: 0x0039DC14
		public AppointmentContextMenuItemClickedEventArgs(Appointment appointment, RadMenuItem menuItem) : base(appointment)
		{
			this._menuItem = menuItem;
		}

		// Token: 0x17004DF2 RID: 19954
		// (get) Token: 0x06010258 RID: 66136 RVA: 0x0039FA24 File Offset: 0x0039DC24
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x04004914 RID: 18708
		private readonly RadMenuItem _menuItem;
	}
}
