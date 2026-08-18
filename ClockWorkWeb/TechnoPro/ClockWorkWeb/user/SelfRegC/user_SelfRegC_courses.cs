using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.AccommodationsRequest.Controls;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x02000084 RID: 132
	public class user_SelfRegC_courses : Page
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00020C34 File Offset: 0x0001EE34
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00020C58 File Offset: 0x0001EE58
		protected void Page_Init(object sender, EventArgs e)
		{
			this.GetPid();
			string text = (string)SessionCaching.CurrentInstance["selfregc_msgcode"];
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				SessionCaching.CurrentInstance.Clear("selfregc_msgcode");
				bool flag2 = text.Equals("0");
				if (flag2)
				{
					this.ctrlMessage1.Message = "Your accommodation request(s) have been submitted.  Please review the list below.";
				}
				else
				{
					bool flag3 = text.Equals("1");
					if (flag3)
					{
						this.ctrlMessage1.Message = "You don't appear to have any accommodations available for the selected course.  Please contact us for assistance.";
					}
					else
					{
						bool flag4 = text.Equals("2");
						if (flag4)
						{
							this.ctrlMessage1.Message = "You don't appear to have any template accommodations available.  Please contact us for assistance.";
						}
						else
						{
							this.ctrlMessage1.Message = null;
						}
					}
				}
			}
			else
			{
				this.ctrlMessage1.Message = null;
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00020D28 File Offset: 0x0001EF28
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.PersonId = this.GetPid();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00020D40 File Offset: 0x0001EF40
		protected void Page_Load(object sender, EventArgs e)
		{
			this.p_topmsg.Visible = false;
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.SELFREGC_ControlIdToAuthorizeStudentForAccommodationsRequestSystem);
				bool flag2 = settingValue > 0;
				if (flag2)
				{
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					IList<DynamicDataDTO> list = dynamicDataClientManager.LoadDataByFields(new DynamicDataContextDTO
					{
						PrimaryId = pid
					}, new List<int>
					{
						settingValue
					}, eDynamicFormTypeDTO.Accommodation);
					bool flag3 = list == null || list.Count < 1;
					if (flag3)
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.SELFREGC_ControlIdToAuthorizeStudentForAccommodationsRequestSystemMessageOnFail, this.Page);
						return;
					}
				}
				bool flag4 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag4)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.SelfRegistration_Accommodations);
				}
				this.CtrlAccommodationRequestCoursesList1.Pid = pid;
				SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
				bool flag5 = selectedSession != null;
				if (flag5)
				{
					this.CtrlAccommodationRequestCoursesList1.SessionDTO = new SessionDTO
					{
						StartDate = selectedSession.StartDate,
						EndDate = selectedSession.EndDate
					};
				}
				bool flag6 = !base.IsPostBack;
				if (flag6)
				{
					this.CtrlAccommodationRequestCoursesList1.RefreshList();
				}
				else
				{
					this.CtrlAccommodationRequestCoursesList1.Pid = pid;
				}
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession != null;
			if (flag)
			{
				this.Session["tc_currentterm"] = selectedSession;
				this.CtrlAccommodationRequestCoursesList1.SessionDTO = selectedSession.ToDTO();
				this.CtrlAccommodationRequestCoursesList1.RefreshList();
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00020F14 File Offset: 0x0001F114
		protected void CtrlAccommodationRequestCoursesList_OnCourseRequestSubmitted(object sender, int lucid)
		{
			INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			string url = string.Format("request.aspx?lucid={0}&sd={1}", navigatorClientManager.ConvertIntParameterToUrlString(lucid), (selectedSession == null) ? "" : selectedSession.StartDate.ToString("yyyy-MM-dd"));
			base.Response.Redirect(url, true);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00020F74 File Offset: 0x0001F174
		protected void CtrlAccommodationRequestCoursesList1_OnLetterRequestSubmitted(object sender, int lucid)
		{
			bool flag = lucid <= 0;
			if (!flag)
			{
				int pid = this.GetPid();
				int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.SELFREGC_OverrideAccommodationLetterTemplateId);
				IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
				BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.AutoMailMergeAccommodationLetter(new AccommodationLetterGenerateContextDTO
				{
					StudentPersonId = pid,
					WhoGeneratingFor = eAccommodationLetterGenerationForWhom.ForStudent,
					OutputType = eAccommodationLetterGenerationOutputType.Pdf,
					LetterType = eAccommodationLetterGenerationType.StudentLetter,
					LuCourseIds = new List<int>
					{
						lucid
					},
					PreferredTemplateId = settingValue
				});
				string text = string.IsNullOrEmpty(binaryFileDTO.FileName) ? "_.pdf" : binaryFileDTO.FileName;
				byte[] byteArray = binaryFileDTO.ByteArray;
				ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
				LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(lucid);
				text = string.Format("AccommodationLetter_{0}{1}", this.FilterFilename((lookupCourseDTO == null) ? "" : lookupCourseDTO.GetCourseDescription()), Path.GetExtension(text));
				IWebFileClientManager webFileClientManager = new WebFileClientManager();
				webFileClientManager.DownloadFile(text, byteArray);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0002106C File Offset: 0x0001F26C
		private string FilterFilename(string s)
		{
			string result;
			if (!string.IsNullOrEmpty(s))
			{
				result = new string((from g in s.ToCharArray()
				select char.IsLetterOrDigit(g) ? g : '_').ToArray<char>());
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x04000264 RID: 612
		protected Panel p_topmsg;

		// Token: 0x04000265 RID: 613
		protected Image img_topmsg;

		// Token: 0x04000266 RID: 614
		protected Label lbl_topmsg;

		// Token: 0x04000267 RID: 615
		protected Label lblTitle;

		// Token: 0x04000268 RID: 616
		protected CtrlMessage ctrlMessage1;

		// Token: 0x04000269 RID: 617
		protected Panel p_info;

		// Token: 0x0400026A RID: 618
		protected Label lbl_info;

		// Token: 0x0400026B RID: 619
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x0400026C RID: 620
		protected CtrlAccommodationRequestCoursesList CtrlAccommodationRequestCoursesList1;

		// Token: 0x0400026D RID: 621
		protected Panel p_additionalInfo;

		// Token: 0x0400026E RID: 622
		protected Label lbl_additionalInfo;
	}
}
