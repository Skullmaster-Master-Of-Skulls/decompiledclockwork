using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000061 RID: 97
	public class user_test_AccommodationsLetter : Page
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000DE1C File Offset: 0x0000C01C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DE40 File Offset: 0x0000C040
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool flag2 = !webSettingsClientManager.GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentsAllowedToAccessAccommodationLettersOnline);
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.ACCOMMODATIONS_ErrorMessage_ModuleDisabled, this.Page);
				}
				else
				{
					bool flag3 = !this.Page.IsPostBack;
					if (flag3)
					{
						bool flag4 = base.Master != null && base.Master is IClockWorkMasterPage;
						if (flag4)
						{
							((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_Accommodations);
						}
						int luCourseId = this.GetLuCourseId();
						bool flag5 = !webSettingsClientManager.GetSettingValue<bool>(Setting.ACCOMMODATIONS_TemplateAccommodationLetterOnly) && luCourseId < 1;
						if (flag5)
						{
							this.ShowMessage("Missing course information.");
						}
						else
						{
							string text;
							bool flag6 = this.CheckLetterIsAvailableForThisCourse(pid, luCourseId, out text);
							bool flag7 = !flag6;
							if (flag7)
							{
								this.ShowMessage(text ?? "Unknown error.");
							}
							else
							{
								string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_IndividualCourseInstructions);
								bool flag8 = !string.IsNullOrEmpty(settingValue);
								if (flag8)
								{
									this.p_pageInstructions.Visible = true;
									this.lbl_pageInstructions.Text = settingValue;
								}
								bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentAllowedToGenerateStudentLetter);
								bool flag9 = !settingValue2;
								if (flag9)
								{
									this.btn_viewLetter.Visible = false;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		private bool CheckLetterIsAvailableForThisCourse(int pid, int lucid, out string errorMessage)
		{
			IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
			bool flag = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(pid, true);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.ACCOMMODATIONS_TemplateAccommodationLetterOnly);
			IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
			bool flag2;
			IList<AccommodationDataDTO> list = accommodationsClientManager.LoadAccommodationsByStudentAndCourseOrTemplate(pid, settingValue ? 0 : lucid, out flag2);
			string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_HiddenControlIds) ?? "";
			List<int> hiddenCids = (from h in text.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
			{
				int result2;
				int.TryParse(g.Trim(), out result2);
				return result2;
			})
			where h > 0
			select h).Distinct<int>().ToList<int>();
			List<int> collection = (from h in list.Where(delegate(AccommodationDataDTO g)
			{
				bool result2;
				if (g.Detail != null)
				{
					eAccommodationGroupDTO @group = g.Detail.Group;
					result2 = (g.Detail.Group == eAccommodationGroupDTO.None);
				}
				else
				{
					result2 = false;
				}
				return result2;
			})
			select h.Data.Field.ControlId).Distinct<int>().ToList<int>();
			hiddenCids.AddRange(collection);
			bool flag3 = hiddenCids.Count > 0;
			if (flag3)
			{
				list = (from g in list
				where !hiddenCids.Contains(g.Data.Field.ControlId)
				select g).ToList<AccommodationDataDTO>();
			}
			bool flag4 = list.Count < 1;
			bool result;
			if (flag4)
			{
				errorMessage = "You do not have any accommodations.  Please contact your disability advisor if you have any questions.";
				result = false;
			}
			else
			{
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				CourseRegistrationDTO courseRegistrationDTO = settingValue ? null : courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(pid, lucid);
				bool flag5 = !settingValue;
				if (flag5)
				{
					bool flag6 = courseRegistrationDTO == null;
					if (flag6)
					{
						errorMessage = "Course information could not be found.  Please contact your disability advisor if you have any questions.";
						return false;
					}
					bool flag7 = courseRegistrationDTO.DateLetterIssued == null;
					if (flag7)
					{
						errorMessage = "Your accommodations letter has not been approved for release yet.  Please contact your disability advisor if you have any questions.";
						return false;
					}
				}
				List<string> list2 = (from g in list
				select g.Data.GetValueDisplay()).ToList<string>();
				list2.Sort((string g1, string g2) => g1.CompareTo(g2));
				bool flag8 = settingValue;
				if (flag8)
				{
					bool flag9 = flag;
					if (flag9)
					{
						errorMessage = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_ErrorMessage_AccommodationsExpired);
						bool flag10 = string.IsNullOrWhiteSpace(errorMessage);
						if (flag10)
						{
							errorMessage = "Your accommodations are expired.  Please contact your advisor if you have any questions.";
						}
						return false;
					}
					foreach (string text2 in list2)
					{
						this.lb_accommodations.Items.Add(new RadListBoxItem(text2));
					}
					this.lbl_title.Text = "Your approved accommodations";
					this.btn_back.Visible = false;
					this.lbl_accommodationsListTitle.Visible = false;
				}
				else
				{
					bool flag11 = courseRegistrationDTO.Course == null || flag;
					bool flag12 = flag11;
					if (flag12)
					{
						this.p_expired.Visible = true;
						this.btn_viewLetter.Enabled = false;
						this.btn_viewLetter.ToolTip = "This option is not available because your accommodations have expired.";
						this.lbl_accommodationsListTitle.Text = "Your accommodations (currently expired and therefore in-active)";
					}
					else
					{
						bool flag13 = courseRegistrationDTO.Course != null && courseRegistrationDTO.Course.EndDate != DateTime.MinValue && this.HasCutoffForViewingLettersAfterCourseEndDateEnded(courseRegistrationDTO.Course.EndDate);
						if (flag13)
						{
							this.p_courseEnded.Visible = true;
							this.btn_viewLetter.Enabled = false;
							this.btn_viewLetter.ToolTip = "This option is not available because your course has ended.";
							this.lbl_accommodationsListTitle.Text = "Your accommodations (course has ended and therefore in-active)";
						}
					}
					bool flag14 = courseRegistrationDTO.Course != null;
					if (flag14)
					{
						string courseDescription = courseRegistrationDTO.Course.GetCourseDescription();
						this.lbl_title.Text = "Accommodations for " + courseDescription;
						string text3 = "Accommodations_" + courseDescription.Trim().Replace(' ', '_') + ".pdf";
						char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
						text3 = invalidFileNameChars.Aggregate(text3, (string current, char c) => current.Replace(c, '_'));
						this.lbl_fn.Value = text3;
						foreach (string text4 in list2)
						{
							this.lb_accommodations.Items.Add(new RadListBoxItem(text4));
						}
					}
				}
				errorMessage = null;
				result = true;
			}
			return result;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		private bool HasCutoffForViewingLettersAfterCourseEndDateEnded(DateTime courseEndDate)
		{
			string xml = new WebSettingsClientManager().GetSettingValue<string>(Setting.ACCOMMODATIONS_AllowStudentToViewLettersForCoursesThatHaveEnded) ?? "";
			CutoffTime cutoffTime = xml.CutoffTimeFromXml() ?? CutoffTime.None;
			bool flag = !cutoffTime.Enabled;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DateTime? maximumDateForAfterTypeCutoff = cutoffTime.GetMaximumDateForAfterTypeCutoff();
				result = (maximumDateForAfterTypeCutoff != null && courseEndDate < maximumDateForAfterTypeCutoff);
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E528 File Offset: 0x0000C728
		protected void btn_viewLetter_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
				navigatorClientManager.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int luCourseId = this.GetLuCourseId();
				string text;
				bool flag2 = this.CheckLetterIsAvailableForThisCourse(pid, luCourseId, out text);
				bool flag3 = !flag2;
				if (flag3)
				{
					this.ShowMessage(text ?? "Unknown error.");
				}
				else
				{
					IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
					BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.AutoMailMergeAccommodationLetter(new AccommodationLetterGenerateContextDTO
					{
						StudentPersonId = pid,
						LuCourseIds = new List<int>
						{
							luCourseId
						},
						LetterType = eAccommodationLetterGenerationType.StudentLetter,
						WhoGeneratingFor = eAccommodationLetterGenerationForWhom.ForStudent,
						OutputType = eAccommodationLetterGenerationOutputType.Pdf
					});
					byte[] array = (binaryFileDTO == null) ? null : binaryFileDTO.ByteArray;
					string filename = (binaryFileDTO == null) ? "accommodations.pdf" : binaryFileDTO.FileName;
					bool flag4 = binaryFileDTO == null || array == null;
					if (flag4)
					{
						this.ShowMessage("You do not have any accommodations.  Please contact your disability advisor if you have any questions.");
					}
					else
					{
						IWebFileClientManager webFileClientManager = new WebFileClientManager();
						webFileClientManager.DownloadFile(filename, array);
					}
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000E641 File Offset: 0x0000C841
		private void ShowMessage(string msg)
		{
			this.p_msg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E65E File Offset: 0x0000C85E
		protected void btn_back_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("AccommodationsLetters.aspx", true);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000E674 File Offset: 0x0000C874
		private int GetLuCourseId()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x040001BB RID: 443
		protected Label lbl_title;

		// Token: 0x040001BC RID: 444
		protected Panel p_msg;

		// Token: 0x040001BD RID: 445
		protected Label lbl_msg;

		// Token: 0x040001BE RID: 446
		protected Panel p_expired;

		// Token: 0x040001BF RID: 447
		protected Label lbl_expired;

		// Token: 0x040001C0 RID: 448
		protected Panel p_courseEnded;

		// Token: 0x040001C1 RID: 449
		protected Label Label1;

		// Token: 0x040001C2 RID: 450
		protected Panel p_pageInstructions;

		// Token: 0x040001C3 RID: 451
		protected Label lbl_pageInstructions;

		// Token: 0x040001C4 RID: 452
		protected Button btn_viewLetter;

		// Token: 0x040001C5 RID: 453
		protected Button btn_back;

		// Token: 0x040001C6 RID: 454
		protected Label lbl_accommodationsListTitle;

		// Token: 0x040001C7 RID: 455
		protected RadListBox lb_accommodations;

		// Token: 0x040001C8 RID: 456
		protected HiddenField lbl_fn;
	}
}
