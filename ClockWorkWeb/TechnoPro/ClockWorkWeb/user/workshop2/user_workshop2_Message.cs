using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000022 RID: 34
	public class user_workshop2_Message : Page
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00005B50 File Offset: 0x00003D50
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = base.Request.QueryString["msgcode"];
				bool flag2 = obj != null;
				if (flag2)
				{
					string text = obj.ToString();
					string a = text;
					if (!(a == "banned"))
					{
						if (!(a == "maxnumapptsreached"))
						{
							if (a == "notallowed")
							{
								this.lbl_message.Text = "You are not authorized to use this online functionality.  Please contact us for more information.";
							}
						}
						else
						{
							db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
							int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.APPOINTMENTBOOKING_maxNumApptsInFuture, conn, base.Cache);
							this.lbl_message.Text = "You are only allowed to book a maximum of " + settingValueInt.ToString() + " appointment(s) in the future at any given time using this online system. You will be allowed to book another appointment once your next appointment has passed, or when you cancel one of your existing appointments.";
						}
					}
					else
					{
						string text2 = "<p>Our records show that you have missed two appointments in a row at the Writing & Learning Centre without cancelling on-line or calling us to cancel.  Unfortunately we have suspended your appointment privileges starting today and for the next four weeks.  However, you are still registered at the Writing & Learning Centre and can continue to make appointments with us after your suspension has lapsed.  Also, you are welcome to use our drop in service which is at various times Monday through Friday.  Contact wlc@ocad.ca or 416-977-6000 ext. 229, or drop by our offices for more information (5th Floor, 1501, 113 McCaul).</p>\r\n<p>About our 2 no-shows policy:  Please keep in mind that we regularly have waiting lists of students requesting appointments at the WLC and when we have a no-show, the tutor sits waiting when we could be matching that tutor with a student in need.  We are trying to do our best to make tutors available to students who need them.</p>";
						this.lbl_message.Text = text2;
					}
				}
			}
		}

		// Token: 0x04000085 RID: 133
		protected Panel p_message;

		// Token: 0x04000086 RID: 134
		protected Label lbl_message;
	}
}
