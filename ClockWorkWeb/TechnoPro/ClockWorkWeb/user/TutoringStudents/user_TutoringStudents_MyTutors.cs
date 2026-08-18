using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005A RID: 90
	public class user_TutoringStudents_MyTutors : Page
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000D33C File Offset: 0x0000B53C
		protected void Page_Load(object sender, EventArgs e)
		{
			int studentPersonId = user_TutoringStudents_MyTutors.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceStudentTuteeRedirects(studentPersonId, this.Page, eClockWorkWebPage.TutoringStudents_MyTutors);
				bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.TutoringStudents_MyTutors);
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		[WebMethod]
		public static IList<MyTutorWrapper> GetTutors()
		{
			int num = user_TutoringStudents_MyTutors.LookupStudentPid();
			bool flag = num < 1;
			IList<MyTutorWrapper> result;
			if (flag)
			{
				result = new List<MyTutorWrapper>();
			}
			else
			{
				IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
				eTuteeStatus tuteeStatus = studentTuteeClientManager.GetTuteeStatus(num);
				bool flag2 = tuteeStatus != eTuteeStatus.Active;
				if (flag2)
				{
					result = new List<MyTutorWrapper>();
				}
				else
				{
					IList<MyTutorDTO> studentMyTutors = studentTuteeClientManager.GetStudentMyTutors(num, new DateTime?(DateTime.Now.AddYears(-4)), null);
					result = (from g in studentMyTutors
					select new MyTutorWrapper(g)).ToList<MyTutorWrapper>();
				}
			}
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D460 File Offset: 0x0000B660
		private static int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D47C File Offset: 0x0000B67C
		[WebMethod]
		public static TutorInfoWrapper LoadTutorInfo2(int tutorId)
		{
			int num = user_TutoringStudents_MyTutors.LookupStudentPid();
			bool flag = num < 1;
			TutorInfoWrapper result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IStudentTuteeClientManager studentTuteeClientManager = new StudentTuteeWebClientManager();
				eTuteeStatus tuteeStatus = studentTuteeClientManager.GetTuteeStatus(num);
				bool flag2 = tuteeStatus != eTuteeStatus.Active;
				if (flag2)
				{
					result = null;
				}
				else
				{
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					TutorDTO tutorDTO = tutorClientManager.LoadTutorById(tutorId);
					bool flag3 = tutorDTO == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
						int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
						IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
						IList<DynamicDataDTO> list = dynamicDataClientManager.LoadData(new DynamicDataContextDTO
						{
							PrimaryId = tutorId
						}, new DynamicFormDTO
						{
							ScreenNum = settingValue
						});
						List<DynamicDataDTO> data = (list != null) ? list.ToList<DynamicDataDTO>() : null;
						result = new TutorInfoWrapper(tutorDTO, data);
					}
				}
			}
			return result;
		}
	}
}
