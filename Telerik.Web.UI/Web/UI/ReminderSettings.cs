using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9D RID: 3997
	public class ReminderSettings : ObjectWithState
	{
		// Token: 0x06009908 RID: 39176 RVA: 0x002222CE File Offset: 0x002204CE
		internal ReminderSettings(StateBag ownerViewState) : base("Reminders", ownerViewState)
		{
		}

		// Token: 0x17003071 RID: 12401
		// (get) Token: 0x06009909 RID: 39177 RVA: 0x002222DC File Offset: 0x002204DC
		// (set) Token: 0x0600990A RID: 39178 RVA: 0x002222FD File Offset: 0x002204FD
		[Category("Behavior")]
		[Description("A value indicating whether the user can view and edit reminders for appointments.")]
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17003072 RID: 12402
		// (get) Token: 0x0600990B RID: 39179 RVA: 0x00222315 File Offset: 0x00220515
		// (set) Token: 0x0600990C RID: 39180 RVA: 0x00222343 File Offset: 0x00220543
		[DefaultValue(typeof(TimeSpan), "14.00:00:00")]
		[Description("The period from the Appointment start after which the Reminder is expired.")]
		[Category("Behavior")]
		public TimeSpan MaxAge
		{
			get
			{
				return (TimeSpan)(base.ViewState["MaxAge"] ?? TimeSpan.FromDays(14.0));
			}
			set
			{
				base.ViewState["MaxAge"] = value;
			}
		}
	}
}
