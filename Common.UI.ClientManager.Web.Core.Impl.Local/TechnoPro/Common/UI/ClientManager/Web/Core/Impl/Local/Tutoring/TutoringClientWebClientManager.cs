using System;
using System.Web;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring
{
	// Token: 0x02000014 RID: 20
	public class TutoringClientWebClientManager : ITutoringClientWebClientManager
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000043CC File Offset: 0x000025CC
		public int TutorAvailabilityScheduleGroupId
		{
			get
			{
				string key = "Tutor_AvailabilityGroupId";
				SessionCaching currentInstance = SessionCaching.CurrentInstance;
				object obj = currentInstance[key];
				int num = (obj != null) ? ((int)obj) : 0;
				bool flag = num < 1;
				if (flag)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					num = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_Availability_Schedule_Id);
					currentInstance.Insert(key, num);
				}
				return num;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004430 File Offset: 0x00002630
		public eClockWorkWebPage EnforceTutoringRedirects(int TutorPersonId, object currentPageObj, eClockWorkWebPage currentPage)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			ITutorClientManager tutorClientManager = new TutorWebClientManager();
			eTutorStatus tutorStatus = tutorClientManager.GetTutorStatus(TutorPersonId);
			eClockWorkWebPage eClockWorkWebPage = eClockWorkWebPage.Unknown;
			CWLogger.Logger.Trace("TutoringClientWebClientManager:EnforceTutoringRedirects:tutorpid={0}:status={1}", TutorPersonId.ToString(), tutorStatus.ToString());
			switch (tutorStatus)
			{
			case eTutorStatus.NotATutor:
				eClockWorkWebPage = eClockWorkWebPage.TutoringTutors_Registration;
				break;
			case eTutorStatus.TutorNotActive:
				eClockWorkWebPage = eClockWorkWebPage.TutoringTutors_WaitForApproval;
				break;
			case eTutorStatus.TutorActiveNeedsConfidentiality:
				eClockWorkWebPage = eClockWorkWebPage.TutoringTutors_ConfidentialityAgreement;
				break;
			case eTutorStatus.TutorActive:
			{
				bool flag = currentPage == eClockWorkWebPage.TutoringTutors_ConfidentialityAgreement;
				if (flag)
				{
					eClockWorkWebPage = eClockWorkWebPage.TutoringTutors_Profile;
				}
				break;
			}
			default:
			{
				INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
				navigatorClientManager.NotAllowed(Setting.TUTORING_TutorIsAuthorizedCid, currentPage);
				break;
			}
			}
			bool flag2 = eClockWorkWebPage != eClockWorkWebPage.Unknown && eClockWorkWebPage != currentPage;
			if (flag2)
			{
				switch (eClockWorkWebPage)
				{
				case eClockWorkWebPage.TutoringTutors_Registration:
					response.Redirect("registration.aspx", true);
					break;
				case eClockWorkWebPage.TutoringTutors_WaitForApproval:
					response.Redirect("WaitForApproval.aspx", true);
					break;
				case eClockWorkWebPage.TutoringTutors_ConfidentialityAgreement:
					response.Redirect("agreement.aspx", true);
					break;
				}
			}
			return eClockWorkWebPage;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004548 File Offset: 0x00002748
		public eClockWorkWebPage EnforceStudentTuteeRedirects(int StudentPersonId, object currentPageObj, eClockWorkWebPage currentPage)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
			eTuteeStatus tuteeStatus = studentTuteeClientManager.GetTuteeStatus(StudentPersonId);
			eClockWorkWebPage eClockWorkWebPage = eClockWorkWebPage.Unknown;
			CWLogger.Logger.Trace("TutoringClientWebClientManager:EnforceStudentTuteeRedirects:pid={0}:status={1}", StudentPersonId.ToString(), tuteeStatus.ToString());
			eTuteeStatus eTuteeStatus = tuteeStatus;
			eTuteeStatus eTuteeStatus2 = eTuteeStatus;
			if (eTuteeStatus2 != eTuteeStatus.ActiveNeedsConfidentiality)
			{
				if (eTuteeStatus2 != eTuteeStatus.Active)
				{
					INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
					navigatorClientManager.NotAllowed(Setting.TUTORING_StudentIsAuthorizedCid, currentPage);
				}
			}
			else
			{
				eClockWorkWebPage = eClockWorkWebPage.TutoringStudents_ConfidentialityAgreement;
			}
			bool flag = eClockWorkWebPage != eClockWorkWebPage.Unknown && eClockWorkWebPage != currentPage;
			if (flag)
			{
				eClockWorkWebPage eClockWorkWebPage2 = eClockWorkWebPage;
				eClockWorkWebPage eClockWorkWebPage3 = eClockWorkWebPage2;
				if (eClockWorkWebPage3 == eClockWorkWebPage.TutoringStudents_ConfidentialityAgreement)
				{
					CWLogger.Logger.Trace("TutoringClientWebClientManager:EnforceStudentTuteeRedirects:TutoringStudents_ConfidentialityAgreement:pid={0}:status={1}", StudentPersonId.ToString(), tuteeStatus.ToString());
					response.Redirect("agreement.aspx", true);
				}
			}
			return eClockWorkWebPage;
		}
	}
}
