using System;
using System.Web.Caching;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Templates;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkController
{
	// Token: 0x02000009 RID: 9
	public class Email
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003F40 File Offset: 0x00002140
		[Obsolete]
		public static void SendBannedEmailConfirmation(Cache cache, int pid, DateTime bannedUntil)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_email_banned);
			EmailTemplate emailTemplate = ClockWorkWebCore.CreateEmailTemplate(settingValue, cache, "EN");
			bool flag = emailTemplate != null;
			if (flag)
			{
				NameObjectPairCollection nameObjectPairCollection = new NameObjectPairCollection();
				DateTime now = DateTime.Now;
				bool flag2 = now.Month < 5;
				string val;
				if (flag2)
				{
					val = (now.Year - 1).ToString().Substring(2) + "." + now.Year.ToString();
				}
				else
				{
					val = now.Year.ToString().Substring(2) + "." + (now.Year + 1).ToString();
				}
				nameObjectPairCollection.Add("#<schoolyear>#", val);
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.GENERAL_EmailCid);
				bool settingValue3 = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
				string text = Student.LookupEmail(pid, settingValue2, settingValue3);
				nameObjectPairCollection.Add("#<email>#", text);
				nameObjectPairCollection.Add("#<banneduntil>#", bannedUntil.ToString("MMMM d, yyyy"));
				string text2;
				string text3;
				emailTemplate.MailMerge(nameObjectPairCollection, out text2, out text3);
				string text4 = (emailTemplate.To.ToLower().Trim().CompareTo("#<email>#") == 0) ? text : emailTemplate.To;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004094 File Offset: 0x00002294
		public static string GetAdminEmail(Setting primarySource)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(primarySource);
			bool flag = string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_AdminEmail);
			}
			return settingValue;
		}

		// Token: 0x0400000B RID: 11
		[Obsolete]
		public static readonly string EMAIL_TEMPLATES_TestBooking_InstructorEmailMissingOrInvalid = "<email>\r\n    <to>#~email~#</to>\r\n    <from></from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork: Unable to send email to instructor after test booking for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\nA student booked a test but the system was unable to email the instructor because the instructor email address is missing or invalid:\r\n\r\nStudent: #~name~#\r\nCourse: #~course~#\r\nDate of booking: #~startdatetime~#\r\n\r\nPlease fill in the missing instructor email address and notify the instructor of this test booking.\r\n    </body>\r\n </email>";

		// Token: 0x0400000C RID: 12
		public static readonly string EMAIL_TEMPLATES_TestBooking_StudentChangedInstructorNameAndOrEmail = "<email>\r\n    <to>#~adminemail~#</to>\r\n    <from></from>\r\n    <cc></cc>\r\n    <bcc></bcc>\r\n    <subject>ClockWork: Student entered a different instructor name and/or email for #~course~#</subject>\r\n    <attachments></attachments>\r\n    <isactive>1</isactive>\r\n    <body>Hello,\r\nA student booked a test and submitted a new instructor name and/or email:\r\n\r\nCurrent instructor name and email: #~instructorname~# #~instructoremail~#\r\nStudent entered instructor name and email: #~newinstructorname~# #~newinstructoremail~#\r\n\r\nPlease verify this information and enter the correct instructor name and email into ClockWork.  \r\n    </body>\r\n </email>\r\n";
	}
}
