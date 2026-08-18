using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F7 RID: 247
	public class user_TutorSchedule_Message : Page
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00036AB8 File Offset: 0x00034CB8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.AppointmentBooking_Help);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				object obj = base.Request.QueryString["msgcode"];
				bool flag3 = obj != null;
				if (flag3)
				{
					string text = obj.ToString();
					string a = text;
					if (!(a == "banned"))
					{
						if (!(a == "maxnumapptsreached"))
						{
							if (!(a == "notallowed"))
							{
								if (a == "invaliddatasync")
								{
									this.lbl_message.Text = "Unfortunately we are unable to locate your information and as a result we cannot create your new account at the current time.  Please contact us for assistance in getting your new account setup.";
								}
							}
							else
							{
								this.lbl_message.Text = "You are not authorized to use this online functionality.  Please contact us for more information.";
							}
						}
						else
						{
							int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_maxNumApptsInFuture);
							this.lbl_message.Text = "You are only allowed to book a maximum of " + settingValue.ToString() + " appointment(s) in the future at any given time using this online system. You will be allowed to book another appointment once your next appointment has passed, or when you cancel one of your existing appointments.";
						}
					}
					else
					{
						string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_BannedMessageToStudent);
						this.lbl_message.Text = settingValue2;
					}
				}
			}
		}

		// Token: 0x04000567 RID: 1383
		protected Panel p_message;

		// Token: 0x04000568 RID: 1384
		protected Label lbl_message;
	}
}
