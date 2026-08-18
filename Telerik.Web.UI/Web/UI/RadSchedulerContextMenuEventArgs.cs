using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A0F RID: 6671
	public sealed class RadSchedulerContextMenuEventArgs : EventArgs
	{
		// Token: 0x06010254 RID: 66132 RVA: 0x0039F9EE File Offset: 0x0039DBEE
		public RadSchedulerContextMenuEventArgs(Appointment appointment, RadMenuItem menuItem)
		{
			this._appointment = appointment;
			this._menuItem = menuItem;
		}

		// Token: 0x17004DF0 RID: 19952
		// (get) Token: 0x06010255 RID: 66133 RVA: 0x0039FA04 File Offset: 0x0039DC04
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x17004DF1 RID: 19953
		// (get) Token: 0x06010256 RID: 66134 RVA: 0x0039FA0C File Offset: 0x0039DC0C
		public Appointment Appointment
		{
			get
			{
				return this._appointment;
			}
		}

		// Token: 0x04004912 RID: 18706
		private readonly RadMenuItem _menuItem;

		// Token: 0x04004913 RID: 18707
		private readonly Appointment _appointment;
	}
}
