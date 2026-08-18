using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Instructor;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D9 RID: 217
	public class user_instructor_ilettercontent : Page
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00031FC4 File Offset: 0x000301C4
		protected void Page_Init(object sender, EventArgs e)
		{
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter.OnInstructorAndAltContactIdsRequired = (EventHandler<PrimaryAndSecondaryIdsRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter.OnInstructorAndAltContactIdsRequired, new EventHandler<PrimaryAndSecondaryIdsRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnInstructorAndAltContactIdsRequired));
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter2 = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter2.OnStudentAndCourseIdsRequired = (EventHandler<PrimaryAndSecondaryIdsRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter2.OnStudentAndCourseIdsRequired, new EventHandler<PrimaryAndSecondaryIdsRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnStudentAndCourseIdsRequired));
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrls_Instructor_CtrlInstructorConfirmViewedLetter3 = this.ctrlInstructorConfirmViewedLetter1;
			ctrls_Instructor_CtrlInstructorConfirmViewedLetter3.OnCourseDescriptionRequired = (EventHandler<StringRequiredArgs>)Delegate.Combine(ctrls_Instructor_CtrlInstructorConfirmViewedLetter3.OnCourseDescriptionRequired, new EventHandler<StringRequiredArgs>(this.ctrlInstructorConfirmViewedLetter1_OnCourseDescriptionRequired));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00032048 File Offset: 0x00030248
		protected void Page_Load(object sender, EventArgs e)
		{
			int iid = this.LookupInstructorId();
			int altContactId = this.GetAltContactId();
			bool flag = iid <= 0 && altContactId <= 0;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					int luCourseId = this.GetLuCourseId();
					int pid = this.GetPid();
					ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
					CourseRegistrationDTO courseRegistrationDTO = courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(pid, luCourseId);
					DateTime? dateTime = (courseRegistrationDTO == null) ? null : courseRegistrationDTO.DateLetterReturned;
					bool flag3 = dateTime != null;
					if (flag3)
					{
						this.ctrlInstructorConfirmViewedLetter1.MarkInstructorAlreadyConfirmed(dateTime.Value);
					}
					IPeopleClientManager peopleClientManager = new PeopleClientManager();
					PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(pid);
					string arg = (personBaseDTO == null) ? "" : personBaseDTO.GetStudentName();
					string text = (courseRegistrationDTO != null && courseRegistrationDTO.Course != null) ? courseRegistrationDTO.Course.GetCourseDescription() : "";
					this.lbl_title.Text = string.Format("Accommodations for {0}", arg);
					this.lbl_subTitle.Text = text;
					bool flag4 = courseRegistrationDTO != null && courseRegistrationDTO.Course != null && ((courseRegistrationDTO.Course.Instructors != null && courseRegistrationDTO.Course.Instructors.FirstOrDefault((LookupInstructorDTO g) => g.InstructorId == iid) != null) || (courseRegistrationDTO.Course.AlternateContacts != null && courseRegistrationDTO.Course.AlternateContacts.FirstOrDefault((AlternateContactDTO g) => g.AlternateContactId == altContactId) != null));
					bool flag5 = !flag4;
					if (flag5)
					{
						base.Response.Redirect("Default.aspx", true);
					}
				}
				this.ViewLetter();
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0003221E File Offset: 0x0003041E
		private void ctrlInstructorConfirmViewedLetter1_OnStudentAndCourseIdsRequired(object sender, PrimaryAndSecondaryIdsRequiredArgs e)
		{
			e.PrimaryId = this.GetPid();
			e.SecondaryId = this.GetLuCourseId();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003223B File Offset: 0x0003043B
		private void ctrlInstructorConfirmViewedLetter1_OnInstructorAndAltContactIdsRequired(object sender, PrimaryAndSecondaryIdsRequiredArgs e)
		{
			e.PrimaryId = this.LookupInstructorId();
			e.SecondaryId = this.GetAltContactId();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00032258 File Offset: 0x00030458
		private void ctrlInstructorConfirmViewedLetter1_OnCourseDescriptionRequired(object sender, StringRequiredArgs e)
		{
			e.StringValue = this.lbl_subTitle.Text;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002FC01 File Offset: 0x0002DE01
		protected void btn_back_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00032270 File Offset: 0x00030470
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			int luCourseId = this.GetLuCourseId();
			int pid = this.GetPid();
			base.Response.Redirect("iletter.aspx?lucid=" + NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(luCourseId) + "&pid=" + NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(pid), true);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000322C0 File Offset: 0x000304C0
		private eExportFileType GetExportAs()
		{
			return eExportFileType.Html;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000322D4 File Offset: 0x000304D4
		private int LookupInstructorId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000322F8 File Offset: 0x000304F8
		private int GetLuCourseId()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"]);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0003232C File Offset: 0x0003052C
		private int GetPid()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["pid"]);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00032360 File Offset: 0x00030560
		private bool GetInFrench()
		{
			bool result = false;
			object obj = base.Request.QueryString["l"];
			bool flag = obj != null;
			if (flag)
			{
				string text = obj.ToString().ToLower();
				bool flag2 = text.Equals("fr");
				if (flag2)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000323B8 File Offset: 0x000305B8
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000323DC File Offset: 0x000305DC
		private bool AuthorizeInstructorToViewThisLetter(out int iid, out int altContactId, out int pid, out int lucid, out bool isNotAllowed)
		{
			iid = this.LookupInstructorId();
			altContactId = this.GetAltContactId();
			bool flag = iid > 0 || altContactId > 0;
			bool result;
			if (flag)
			{
				isNotAllowed = false;
				pid = this.GetPid();
				lucid = this.GetLuCourseId();
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				CourseRegistrationDTO courseRegistrationDTO = courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(pid, lucid);
				bool flag2 = courseRegistrationDTO == null || courseRegistrationDTO.Course == null;
				if (flag2)
				{
					this.lbl.Text = "Failed check.";
					CWLogger.Logger.Warn("user/instructor/ilettercontent.aspx.cs:ViewLetter:Student is not registered in course, coursereg is null:pid={0}:lucid={1}", pid.ToString(), lucid.ToString());
					result = false;
				}
				else
				{
					int instructorId = iid;
					int alternateContactId = altContactId;
					bool flag3 = (courseRegistrationDTO.Course.Instructors == null || courseRegistrationDTO.Course.Instructors.FirstOrDefault((LookupInstructorDTO g) => g.InstructorId == instructorId) == null) && (courseRegistrationDTO.Course.AlternateContacts == null || courseRegistrationDTO.Course.AlternateContacts.FirstOrDefault((AlternateContactDTO g) => g.AlternateContactId == alternateContactId) == null);
					if (flag3)
					{
						this.lbl.Text = "Failed check.";
						CWLogger.Logger.Warn("user/instructor/ilettercontent.aspx.cs:ViewLetter:Instructor or alt contact is not teaching this course:pid={0}:lucid={1}:iid={2}:altcontactid={3}", new object[]
						{
							pid.ToString(),
							lucid.ToString(),
							iid.ToString(),
							altContactId.ToString()
						});
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			else
			{
				pid = 0;
				lucid = 0;
				isNotAllowed = true;
				result = false;
			}
			return result;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00032564 File Offset: 0x00030764
		protected void ViewLetter()
		{
			int instructorId;
			int alternateContactId;
			int studentPersonId;
			int item;
			bool flag2;
			bool flag = this.AuthorizeInstructorToViewThisLetter(out instructorId, out alternateContactId, out studentPersonId, out item, out flag2);
			bool flag3 = flag2;
			if (flag3)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			else
			{
				bool flag4 = flag;
				if (flag4)
				{
					IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
					BinaryFileDTO binaryFileDTO = mailMergingDocClientManager.AutoMailMergeAccommodationLetter(new AccommodationLetterGenerateContextDTO
					{
						StudentPersonId = studentPersonId,
						LuCourseIds = new List<int>
						{
							item
						},
						InstructorId = instructorId,
						AlternateContactId = alternateContactId,
						WhoGeneratingFor = eAccommodationLetterGenerationForWhom.ForInstructor,
						OutputType = eAccommodationLetterGenerationOutputType.Html,
						LetterType = eAccommodationLetterGenerationType.ProfLetter
					});
					string text = this.LoadDocument(binaryFileDTO.ByteArray);
					this.lbl.Text = text;
				}
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00032624 File Offset: 0x00030824
		private string LoadDocument(byte[] bytes)
		{
			string source = null;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (TextReader textReader = new StreamReader(memoryStream))
				{
					source = textReader.ReadToEnd();
				}
			}
			return source.DecodeHtml();
		}

		// Token: 0x040004F5 RID: 1269
		protected Panel p_title;

		// Token: 0x040004F6 RID: 1270
		protected Label lbl_title;

		// Token: 0x040004F7 RID: 1271
		protected Label lbl_subTitle;

		// Token: 0x040004F8 RID: 1272
		protected Button btn_back;

		// Token: 0x040004F9 RID: 1273
		protected Panel p_letter;

		// Token: 0x040004FA RID: 1274
		protected Label lbl;

		// Token: 0x040004FB RID: 1275
		protected ctrls_Instructor_CtrlInstructorConfirmViewedLetter ctrlInstructorConfirmViewedLetter1;

		// Token: 0x040004FC RID: 1276
		protected HiddenField lbl_fn;
	}
}
