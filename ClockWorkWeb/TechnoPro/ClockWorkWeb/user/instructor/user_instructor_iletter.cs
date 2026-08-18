using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Instructor;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D8 RID: 216
	public class user_instructor_iletter : Page
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x000318A0 File Offset: 0x0002FAA0
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000318C4 File Offset: 0x0002FAC4
		protected void Page_Init(object sender, EventArgs e)
		{
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter.OnInstructorAndAltContactIdsRequired = (EventHandler<PrimaryAndSecondaryIdsRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter.OnInstructorAndAltContactIdsRequired, new EventHandler<PrimaryAndSecondaryIdsRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnInstructorAndAltContactIdsRequired));
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter2 = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter2.OnStudentAndCourseIdsRequired = (EventHandler<PrimaryAndSecondaryIdsRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter2.OnStudentAndCourseIdsRequired, new EventHandler<PrimaryAndSecondaryIdsRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnStudentAndCourseIdsRequired));
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter3 = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter3.OnCourseDescriptionRequired = (EventHandler<StringRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter3.OnCourseDescriptionRequired, new EventHandler<StringRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnCourseDescriptionRequired));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00031948 File Offset: 0x0002FB48
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupInstructorId();
			int altContactId = this.GetAltContactId();
			bool flag = num <= 0 && altContactId <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					bool flag3;
					int pid = this.GetPid(out flag3);
					bool flag4;
					int luCourseId = this.GetLuCourseId(out flag4);
					flag3 = (flag3 || flag4);
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_RedirectEmailLinksForIndividualLettersToLettersPage);
					bool flag5 = settingValue && flag3;
					bool flag6 = luCourseId < 1 || pid < 1 || flag5;
					if (flag6)
					{
						base.Response.Redirect("letters.aspx", true);
					}
					ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
					bool flag7 = courseRegistrationClientManager.IsInstructorOrAltContactTeachingStudentsCourse(pid, luCourseId, num, altContactId);
					bool flag8 = flag7;
					if (flag8)
					{
						StudentCourseLetterInfo studentsCoursesLettersAreAllowedForInstructorByStudentAndCourse = InstructorClientHelper.GetStudentsCoursesLettersAreAllowedForInstructorByStudentAndCourse(num, altContactId, pid, luCourseId);
						bool flag9 = studentsCoursesLettersAreAllowedForInstructorByStudentAndCourse != null;
						if (flag9)
						{
							int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterCourseEndDateAuthorizationExtensionInDays);
							LookupCourseBaseDTO courseBase = studentsCoursesLettersAreAllowedForInstructorByStudentAndCourse.CourseBase;
							DateTime date = ((courseBase != null) ? courseBase.EndDate : DateTime.MinValue).AddDays((double)settingValue2).Date;
							bool flag10 = date < DateTime.Today;
							if (flag10)
							{
								this.lbl_courseHasEnded.Visible = true;
								this.btn_viewLetter.Visible = false;
								this.lbl_btn_or.Visible = false;
								this.btn_viewLetterHtml.Visible = false;
							}
							DateTime? dateLetterReturned = studentsCoursesLettersAreAllowedForInstructorByStudentAndCourse.DateLetterReturned;
							bool flag11 = dateLetterReturned != null;
							if (flag11)
							{
								this.ctrlInstructorConfirmViewedLetter1.MarkInstructorAlreadyConfirmed(dateLetterReturned.Value);
							}
							IPeopleClientManager peopleClientManager = new PeopleClientManager();
							PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(pid);
							string arg = (personBaseDTO == null) ? "" : personBaseDTO.GetStudentName();
							string text = (personBaseDTO == null) ? "" : string.Concat(new string[]
							{
								personBaseDTO.FirstName ?? "",
								"_",
								personBaseDTO.LastName ?? "",
								"_",
								personBaseDTO.Student_no ?? ""
							});
							string courseDescription = studentsCoursesLettersAreAllowedForInstructorByStudentAndCourse.CourseBase.GetCourseDescription();
							this.lbl_title.Text = string.Format("Accommodations for {0}", arg);
							this.lbl_subTitle.Text = courseDescription;
							string text2 = string.Concat(new string[]
							{
								"Accommodations_",
								text.Replace(' ', '_'),
								"_",
								courseDescription.Trim().Replace(' ', '_'),
								".pdf"
							});
							char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
							foreach (char oldChar in invalidFileNameChars)
							{
								text2 = text2.Replace(oldChar, '_');
							}
							this.lbl_fn.Value = text2;
						}
					}
					int settingValue3 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterTemplateId);
					int settingValue4 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterHTMLTemplateId);
					bool flag12 = settingValue3 < 1;
					if (flag12)
					{
						this.lbl_btn_or.Visible = false;
						this.btn_viewLetter.Visible = false;
					}
					bool flag13 = settingValue4 < 1;
					if (flag13)
					{
						this.lbl_btn_or.Visible = false;
						this.btn_viewLetterHtml.Visible = false;
					}
				}
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00031CA3 File Offset: 0x0002FEA3
		private void ctrlInstructorConfirmViewedLetter1_OnStudentAndCourseIdsRequired(object sender, PrimaryAndSecondaryIdsRequiredArgs e)
		{
			e.PrimaryId = this.GetPid();
			e.SecondaryId = this.GetLuCourseId();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00031CC0 File Offset: 0x0002FEC0
		private void ctrlInstructorConfirmViewedLetter1_OnInstructorAndAltContactIdsRequired(object sender, PrimaryAndSecondaryIdsRequiredArgs e)
		{
			e.PrimaryId = this.LookupInstructorId();
			e.SecondaryId = this.GetAltContactId();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00031CDD File Offset: 0x0002FEDD
		private void ctrlInstructorConfirmViewedLetter1_OnCourseDescriptionRequired(object sender, StringRequiredArgs e)
		{
			e.StringValue = this.lbl_subTitle.Text;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00031CF4 File Offset: 0x0002FEF4
		private int LookupInstructorId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00031D18 File Offset: 0x0002FF18
		protected void btn_viewLetter_Click(object sender, EventArgs e)
		{
			int num = this.LookupInstructorId();
			int altContactId = this.GetAltContactId();
			bool flag = num <= 0 && altContactId <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
			}
			else
			{
				int pid = this.GetPid();
				int luCourseId = this.GetLuCourseId();
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				bool flag2 = courseRegistrationClientManager.IsInstructorOrAltContactTeachingStudentsCourse(pid, luCourseId, num, altContactId);
				bool flag3 = true;
				bool flag4 = flag2;
				if (flag4)
				{
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(luCourseId);
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_AccommodationLetterCourseEndDateAuthorizationExtensionInDays);
					DateTime date = ((lookupCourseDTO != null) ? lookupCourseDTO.EndDate : DateTime.MinValue).AddDays((double)settingValue).Date;
					bool flag5 = date >= DateTime.Today;
					if (flag5)
					{
						flag3 = false;
						IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
						BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.AutoMailMergeAccommodationLetter(new AccommodationLetterGenerateContextDTO
						{
							StudentPersonId = pid,
							LuCourseIds = new List<int>
							{
								luCourseId
							},
							InstructorId = num,
							AlternateContactId = altContactId,
							LetterType = eAccommodationLetterGenerationType.ProfLetter,
							WhoGeneratingFor = eAccommodationLetterGenerationForWhom.ForInstructor,
							OutputType = eAccommodationLetterGenerationOutputType.Pdf
						});
						IWebFileClientManager webFileClientManager = new WebFileClientManager();
						webFileClientManager.DownloadFile(binaryFileDTO.FileName, binaryFileDTO.ByteArray);
					}
				}
				bool flag6 = flag3;
				if (flag6)
				{
					this.lbl_title.Text = "Failed check.";
				}
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00031E8C File Offset: 0x0003008C
		protected void btn_back_Click(object sender, EventArgs e)
		{
			int luCourseId = this.GetLuCourseId();
			base.Response.Redirect("istudent.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(luCourseId), true);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00031EC4 File Offset: 0x000300C4
		private int GetLuCourseId(out bool wasLongTermUrl)
		{
			return NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["lucid"] ?? "", out wasLongTermUrl);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00031F00 File Offset: 0x00030100
		private int GetLuCourseId()
		{
			bool flag;
			return this.GetLuCourseId(out flag);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00031F1C File Offset: 0x0003011C
		private int GetPid()
		{
			bool flag;
			return this.GetPid(out flag);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00031F38 File Offset: 0x00030138
		private int GetPid(out bool wasLongTermUrl)
		{
			return NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["pid"] ?? "", out wasLongTermUrl);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00031F74 File Offset: 0x00030174
		protected void btn_viewLetterHtml_Click(object sender, EventArgs e)
		{
			int luCourseId = this.GetLuCourseId();
			int pid = this.GetPid();
			string url = "ilettercontent.aspx?lucid=" + NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(luCourseId) + "&pid=" + NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(pid);
			base.Response.Redirect(url, true);
		}

		// Token: 0x040004E8 RID: 1256
		protected Panel p_title;

		// Token: 0x040004E9 RID: 1257
		protected Label lbl_title;

		// Token: 0x040004EA RID: 1258
		protected Label lbl_subTitle;

		// Token: 0x040004EB RID: 1259
		protected Panel p_viewPdfLetter;

		// Token: 0x040004EC RID: 1260
		protected Button btn_viewLetter;

		// Token: 0x040004ED RID: 1261
		protected Label lbl_btn_or;

		// Token: 0x040004EE RID: 1262
		protected Button btn_viewLetterHtml;

		// Token: 0x040004EF RID: 1263
		protected Label lbl_courseHasEnded;

		// Token: 0x040004F0 RID: 1264
		protected ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrlInstructorConfirmViewedLetter1;

		// Token: 0x040004F1 RID: 1265
		protected Button btn_back;

		// Token: 0x040004F2 RID: 1266
		protected Panel p_accommodations;

		// Token: 0x040004F3 RID: 1267
		protected RadListBox lb_accommodations;

		// Token: 0x040004F4 RID: 1268
		protected HiddenField lbl_fn;
	}
}
