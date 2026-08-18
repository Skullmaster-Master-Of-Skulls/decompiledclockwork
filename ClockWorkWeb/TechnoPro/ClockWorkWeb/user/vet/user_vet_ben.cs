using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x0200002E RID: 46
	public class user_vet_ben : Page
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000086BC File Offset: 0x000068BC
		private int screenNum
		{
			get
			{
				bool flag = this._screenNum != null;
				int result;
				if (flag)
				{
					result = this._screenNum.Value;
				}
				else
				{
					DynamicDataDTO studentsChapter = this.StudentsChapter;
					bool flag2 = studentsChapter == null;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						string substringToMatch = studentsChapter.Value.ToString();
						IList<DynamicFormDTO> list = this.dynamicFormWebClientManager.FindFormByTitleSubstringMatch(substringToMatch, true, true);
						bool flag3 = list == null || list.Count < 1;
						if (flag3)
						{
							result = 0;
						}
						else
						{
							result = list[0].ScreenNum;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00008748 File Offset: 0x00006948
		private DynamicDataDTO StudentsChapter
		{
			get
			{
				bool flag = this._studentsChapter != null;
				DynamicDataDTO result;
				if (flag)
				{
					result = this._studentsChapter;
				}
				else
				{
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					DynamicDataContextDTO context = new DynamicDataContextDTO
					{
						PrimaryId = this.Pid,
						SecondaryId = this.PerDateEntryId
					};
					List<int> controlIds = new List<int>
					{
						this.ChapterCid
					};
					IList<DynamicDataDTO> list = dynamicDataClientManager.LoadDataByFields(context, controlIds, eDynamicFormTypeDTO.PerDate);
					bool flag2 = list == null || list.Count < 1;
					if (flag2)
					{
						list = dynamicDataClientManager.LoadDataByFields(context, controlIds, eDynamicFormTypeDTO.PerStudent);
					}
					bool flag3 = list == null || list.Count < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						this._studentsChapter = list[0];
						result = this._studentsChapter;
					}
				}
				return result;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00008808 File Offset: 0x00006A08
		private int ChapterCid
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_ChapterCid);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000882C File Offset: 0x00006A2C
		private IDynamicFormsClientManager dynamicFormWebClientManager
		{
			get
			{
				IDynamicFormsClientManager result;
				if ((result = this._dynamicFormWebClientManager) == null)
				{
					result = (this._dynamicFormWebClientManager = new DynamicFormClientManager());
				}
				return result;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00008854 File Offset: 0x00006A54
		private IDynamicDataClientManager dynamicDataClientManager
		{
			get
			{
				IDynamicDataClientManager result;
				if ((result = this._dynamicDataClientManager) == null)
				{
					result = (this._dynamicDataClientManager = new DynamicDataClientManager());
				}
				return result;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000887C File Offset: 0x00006A7C
		private SessionDTO CurrentSession
		{
			get
			{
				object obj = this.Session["VetSelectedSession"];
				bool flag = obj != null;
				SessionDTO result;
				if (flag)
				{
					SessionView view = (SessionView)obj;
					result = view.ToDTO();
				}
				else
				{
					SessionClientManager sessionClientManager = new SessionClientManager();
					SessionView currentSession = sessionClientManager.GetCurrentSession();
					this.Session["VetSelectedSession"] = currentSession;
					result = currentSession.ToDTO();
				}
				return result;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000088E4 File Offset: 0x00006AE4
		protected void Page_Load(object sender, EventArgs e)
		{
			SessionDTO currentSession = this.CurrentSession;
			bool flag = currentSession.EndDate < DateTime.Now.Date;
			if (flag)
			{
				base.Response.Redirect("default.aspx", true);
			}
			int pid = this.Pid;
			int perDateEntryId = this.PerDateEntryId;
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				string psCidsCommaSeparated = this.PsCidsCommaSeparated;
				string pmCidsCommaSeparated = this.PmCidsCommaSeparated;
				DynamicScreenLayout.FillScreenWithPerDateData(this.p_data, this.screenNum, this.Pid, this.PerDateEntryId, base.Cache, psCidsCommaSeparated);
				DynamicScreenLayout.FillScreenWithPerStudentData(this.p_data, this.screenNum, this.Pid, base.Cache, pmCidsCommaSeparated);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000089A4 File Offset: 0x00006BA4
		private int Pid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000089C8 File Offset: 0x00006BC8
		private int StaticPerStudentScreenNum
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_StaticPerStudentForm);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000089EC File Offset: 0x00006BEC
		private int PackageScreenNum
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_PackageFormNum);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00008A10 File Offset: 0x00006C10
		private int PerDateEntryId
		{
			get
			{
				bool flag = this.pdEntryId < 0;
				if (flag)
				{
					SessionClientManager sessionClientManager = new SessionClientManager();
					PerDateEntryDTO existingPerDateEntry = this.dynamicDataClientManager.GetExistingPerDateEntry(this.Pid, this.PackageScreenNum, this.CurrentSession);
					this.pdEntryId = ((existingPerDateEntry == null) ? 0 : existingPerDateEntry.AppointmentId);
				}
				return this.pdEntryId;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A70 File Offset: 0x00006C70
		private void Page_Init(object sender, EventArgs e)
		{
			DynamicDataDTO studentsChapter = this.StudentsChapter;
			bool flag = studentsChapter == null || studentsChapter.ValueId < 1;
			if (flag)
			{
				base.Response.Redirect("default.aspx?code=missingchapter", true);
			}
			int screenNum = this.screenNum;
			bool flag2 = screenNum < 1;
			if (flag2)
			{
				base.Response.Redirect("default.aspx?code=nochapterform&valueid=" + ((studentsChapter == null) ? "0" : studentsChapter.ValueId.ToString()), true);
			}
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			string exemptCids = "";
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, conn, screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008694 File Offset: 0x00006894
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008B24 File Offset: 0x00006D24
		protected void btn_save_Click(object sender, EventArgs e)
		{
			SessionDTO currentSession = this.CurrentSession;
			int pid = this.Pid;
			int num = this.PerDateEntryId;
			bool flag = num < 1;
			if (flag)
			{
				int num2 = this.dynamicDataClientManager.CreatePerDateEntry(new PerDateEntryDTO
				{
					ScreenNum = this.PackageScreenNum,
					DateEntered = currentSession.StartDate.Date.AddDays(1.0),
					Student = new PersonBaseDTO
					{
						PersonId = pid
					},
					WhoEntered = new PersonBaseDTO
					{
						PersonId = pid
					}
				});
				num = num2;
			}
			string psCidsCommaSeparated = this.PsCidsCommaSeparated;
			string pmCidsCommaSeparated = this.PmCidsCommaSeparated;
			Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerDate, pid, num, this.PackageScreenNum, base.Cache, this.p_data, psCidsCommaSeparated);
			bool flag2 = ex != null;
			if (flag2)
			{
				CWLogger.Logger.Error("Veterans benefit request form Error: {0}", ex.ToString());
			}
			else
			{
				bool flag3 = pmCidsCommaSeparated.Length > 0;
				if (flag3)
				{
					ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, pid, this.StaticPerStudentScreenNum, base.Cache, this.p_data, pmCidsCommaSeparated);
					bool flag4 = ex != null;
					if (flag4)
					{
						CWLogger.Logger.Error("Veterans benefit request form Error 2: {0}", ex.ToString());
						return;
					}
				}
				base.Response.Redirect("default.aspx", true);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00008C80 File Offset: 0x00006E80
		private string PsCidsCommaSeparated
		{
			get
			{
				List<int> psCids = this.GetPsCids();
				return string.Join(",", psCids.ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00008CD0 File Offset: 0x00006ED0
		private string PmCidsCommaSeparated
		{
			get
			{
				List<int> pmCids = this.GetPmCids();
				return string.Join(",", pmCids.ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008D20 File Offset: 0x00006F20
		private List<int> GetPsCids()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			IList<DynamicFieldDTO> source = this.dynamicFieldClientManager.LoadFieldsByForm(new DynamicFormDTO
			{
				ScreenNum = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_StaticPerStudentForm)
			});
			IEnumerable<int> enumerable = from f in source
			select f.ControlId;
			return (enumerable != null) ? enumerable.ToList<int>() : null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008D8C File Offset: 0x00006F8C
		private List<int> GetPmCids()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			IList<DynamicFieldDTO> source = this.dynamicFieldClientManager.LoadFieldsByForm(new DynamicFormDTO
			{
				ScreenNum = webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_PackageFormNum)
			});
			IEnumerable<int> enumerable = from f in source
			select f.ControlId;
			return (enumerable != null) ? enumerable.ToList<int>() : null;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00008DF8 File Offset: 0x00006FF8
		private IDynamicFieldClientManager dynamicFieldClientManager
		{
			get
			{
				IDynamicFieldClientManager result;
				if ((result = this._dynamicFieldClientManager) == null)
				{
					result = (this._dynamicFieldClientManager = new DynamicFieldClientManager());
				}
				return result;
			}
		}

		// Token: 0x040000D6 RID: 214
		private int? _screenNum;

		// Token: 0x040000D7 RID: 215
		private DynamicDataDTO _studentsChapter;

		// Token: 0x040000D8 RID: 216
		private IDynamicFormsClientManager _dynamicFormWebClientManager;

		// Token: 0x040000D9 RID: 217
		private IDynamicDataClientManager _dynamicDataClientManager;

		// Token: 0x040000DA RID: 218
		private int pdEntryId = -1;

		// Token: 0x040000DB RID: 219
		private IDynamicFieldClientManager _dynamicFieldClientManager;

		// Token: 0x040000DC RID: 220
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x040000DD RID: 221
		protected ValidationSummary vsumAll;

		// Token: 0x040000DE RID: 222
		protected Panel p_data;

		// Token: 0x040000DF RID: 223
		protected Panel p_options;

		// Token: 0x040000E0 RID: 224
		protected Button btn_save;

		// Token: 0x040000E1 RID: 225
		protected Button btn_cancel;
	}
}
