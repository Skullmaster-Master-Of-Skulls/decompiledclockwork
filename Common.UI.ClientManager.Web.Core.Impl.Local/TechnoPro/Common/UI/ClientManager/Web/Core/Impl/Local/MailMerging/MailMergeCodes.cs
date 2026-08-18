using System;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging
{
	// Token: 0x02000019 RID: 25
	public class MailMergeCodes : IMailMergeCodes
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00005548 File Offset: 0x00003748
		public string GetDefaultFromAddress(eWebModule WebModule)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string text;
			switch (WebModule)
			{
			case eWebModule.TestsExams:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_TestExam);
				break;
			case eWebModule.AppointmentBooking:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_AppointmentBooking);
				break;
			case eWebModule.Workshops:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Workshops);
				break;
			case eWebModule.Notetaking:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Notetaking);
				break;
			case eWebModule.SelfRegistration:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_SelfRegistration);
				break;
			case eWebModule.Surveys:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Surveys);
				break;
			case eWebModule.InstructorAccommodations:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Instructor);
				break;
			case eWebModule.InstructorTestsExams:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Instructor);
				break;
			case eWebModule.StudentAccommodations:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_StudentAccommodations);
				break;
			case eWebModule.Intake:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Intake);
				break;
			case eWebModule.Veterans:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Veterans);
				break;
			default:
				text = null;
				break;
			}
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_FromEmailAddress);
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005664 File Offset: 0x00003864
		public string GetDefaultSignature(eWebModule WebModule)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string text;
			switch (WebModule)
			{
			case eWebModule.TestsExams:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_TestExam);
				break;
			case eWebModule.AppointmentBooking:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_AppointmentBooking);
				break;
			case eWebModule.Workshops:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Workshops);
				break;
			case eWebModule.Notetaking:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Notetaking);
				break;
			case eWebModule.SelfRegistration:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_SelfRegistration);
				break;
			case eWebModule.Surveys:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Surveys);
				break;
			case eWebModule.InstructorAccommodations:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Instructor);
				break;
			case eWebModule.InstructorTestsExams:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Instructor);
				break;
			case eWebModule.StudentAccommodations:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_StudentAccommodations);
				break;
			case eWebModule.Intake:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Intake);
				break;
			case eWebModule.Veterans:
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature_Veterans);
				break;
			default:
				text = null;
				break;
			}
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_DefaultSignature);
			}
			return result;
		}
	}
}
