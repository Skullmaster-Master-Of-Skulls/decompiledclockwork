using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.Common.EventArgs;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor
{
	// Token: 0x02000146 RID: 326
	public class CtrlInstructorTestExamDynamicFormData : UserControl
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00045CB4 File Offset: 0x00043EB4
		private void Page_Init(object sender, EventArgs e)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int screenNum = this.GetScreenNum();
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_uploadScreenExemptControlIds);
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, settingValue);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00045CF4 File Offset: 0x00043EF4
		public int GetScreenNum()
		{
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_OverrideExamInfoFormNum);
			bool flag = settingValue < 1;
			if (flag)
			{
				settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
			}
			return settingValue;
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060009F8 RID: 2552 RVA: 0x00045D30 File Offset: 0x00043F30
		// (remove) Token: 0x060009F9 RID: 2553 RVA: 0x00045D68 File Offset: 0x00043F68
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<NumberEventArgs> OnExamIdRequired;

		// Token: 0x060009FA RID: 2554 RVA: 0x00045DA0 File Offset: 0x00043FA0
		private int GetExamId()
		{
			EventHandler<NumberEventArgs> onExamIdRequired = this.OnExamIdRequired;
			bool flag = onExamIdRequired == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				NumberEventArgs numberEventArgs = new NumberEventArgs();
				onExamIdRequired(this, numberEventArgs);
				result = numberEventArgs.Number;
			}
			return result;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00045DDC File Offset: 0x00043FDC
		public string GetDataSummaryForDisplay()
		{
			return DynamicScreenLayout.GetSummary(this.p_data, this.GetScreenNum(), this.GetExamId(), base.Cache, "", true);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00045E14 File Offset: 0x00044014
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int examId = this.GetExamId();
				bool flag2 = examId > 0;
				if (flag2)
				{
					DynamicScreenLayout.FillScreenWithPerAppointmentData("InstructorPM", this.p_data, this.GetScreenNum(), 0, examId, base.Cache, "");
				}
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00045E6C File Offset: 0x0004406C
		public void SaveDynamicData(int iid, int examId)
		{
			int screenNum = this.GetScreenNum();
			bool flag = screenNum > 0;
			if (flag)
			{
				Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_InstructorPerExam, 0, examId, screenNum, base.Cache, this.p_data, "");
				bool flag2 = ex == null;
				if (flag2)
				{
					CWLogger.Logger.Debug("Instructor:ExamUpload:SaveDynamicDataSuccess:iid={0}:examid={1}", iid.ToString(), examId.ToString());
				}
				else
				{
					string text;
					try
					{
						text = DynamicScreenLayout.GetSummaryPlainText(this.p_data, screenNum, 0, base.Cache, null, "", false);
					}
					catch (Exception ex2)
					{
						text = ex2.ToString();
					}
					CWLogger.Logger.Error("Instructor:ExamUpload:SaveDynamicDataFail:iid={0}:examid={1}:formdata={2}:error={3}", new object[]
					{
						iid.ToString(),
						examId.ToString(),
						text.ToString(),
						ex.ToString()
					});
				}
			}
		}

		// Token: 0x040007C2 RID: 1986
		protected Panel p_data;
	}
}
