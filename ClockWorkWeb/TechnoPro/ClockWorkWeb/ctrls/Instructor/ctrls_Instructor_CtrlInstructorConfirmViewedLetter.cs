using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor
{
	// Token: 0x02000143 RID: 323
	public class ctrls_Instructor_CtrlInstructorConfirmViewedLetter : UserControl
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00044A58 File Offset: 0x00042C58
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_InstructorAccommodationLetterConfirmationMessage);
				bool flag2 = settingValue.Length > 0;
				if (flag2)
				{
					this.lbl_iagreemessage.Text = settingValue;
				}
				string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_InstructorAccommodationLetterIAgreeText);
				bool flag3 = settingValue2.Length > 0;
				if (flag3)
				{
					this.chk_iagree.Text = settingValue2;
				}
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00044AD0 File Offset: 0x00042CD0
		private string GetCourseDescription()
		{
			StringRequiredArgs stringRequiredArgs = new StringRequiredArgs();
			EventHandler<StringRequiredArgs> onCourseDescriptionRequired = this.OnCourseDescriptionRequired;
			bool flag = onCourseDescriptionRequired != null;
			if (flag)
			{
				onCourseDescriptionRequired(this, stringRequiredArgs);
			}
			return stringRequiredArgs.StringValue ?? "";
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00044B10 File Offset: 0x00042D10
		private void GetPersonIdAndLuCourseId(out int pid, out int lucid)
		{
			PrimaryAndSecondaryIdsRequiredArgs primaryAndSecondaryIdsRequiredArgs = new PrimaryAndSecondaryIdsRequiredArgs();
			EventHandler<PrimaryAndSecondaryIdsRequiredArgs> onStudentAndCourseIdsRequired = this.OnStudentAndCourseIdsRequired;
			bool flag = onStudentAndCourseIdsRequired != null;
			if (flag)
			{
				onStudentAndCourseIdsRequired(this, primaryAndSecondaryIdsRequiredArgs);
			}
			pid = primaryAndSecondaryIdsRequiredArgs.PrimaryId;
			lucid = primaryAndSecondaryIdsRequiredArgs.SecondaryId;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00044B4C File Offset: 0x00042D4C
		private void GetInstructorIdAndAltContactId(out int iid, out int altContactId)
		{
			PrimaryAndSecondaryIdsRequiredArgs primaryAndSecondaryIdsRequiredArgs = new PrimaryAndSecondaryIdsRequiredArgs();
			EventHandler<PrimaryAndSecondaryIdsRequiredArgs> onInstructorAndAltContactIdsRequired = this.OnInstructorAndAltContactIdsRequired;
			bool flag = onInstructorAndAltContactIdsRequired != null;
			if (flag)
			{
				onInstructorAndAltContactIdsRequired(this, primaryAndSecondaryIdsRequiredArgs);
			}
			iid = primaryAndSecondaryIdsRequiredArgs.PrimaryId;
			altContactId = primaryAndSecondaryIdsRequiredArgs.SecondaryId;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00044B88 File Offset: 0x00042D88
		public void MarkInstructorAlreadyConfirmed(DateTime dateAgreed)
		{
			this.p_iagreepending.Visible = false;
			this.p_iagreedone.Visible = true;
			this.lbl_iagreedate.Text = dateAgreed.ToString("MMMM d, yyyy . h:mm tt");
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00044BC0 File Offset: 0x00042DC0
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			bool @checked = this.chk_iagree.Checked;
			if (@checked)
			{
				int num;
				int num2;
				this.GetPersonIdAndLuCourseId(out num, out num2);
				int instructorId;
				int num3;
				this.GetInstructorIdAndAltContactId(out instructorId, out num3);
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				courseRegistrationClientManager.SetDateLetterReturnedByStudentAndCourse(num, num2, new DateTime?(DateTime.Now));
				IStudentCommonInfoClientManager studentCommonInfoClientManager = new StudentCommonInfoClientManager();
				StudentCommonInfoDTO studentCommonInfoDTO = studentCommonInfoClientManager.LoadStudentCommonInfo(num);
				CourseRegistrationDTO courseRegistrationDTO = courseRegistrationClientManager.LoadCourseRegistrationsByStudentAndCourse(num, num2);
				string value = (courseRegistrationDTO == null || courseRegistrationDTO.Course == null) ? "" : (courseRegistrationDTO.Course.Location ?? "");
				StringDictionary stringDictionary = new StringDictionary
				{
					{
						"email",
						(studentCommonInfoDTO == null) ? "" : (studentCommonInfoDTO.Email ?? "")
					},
					{
						"location",
						value
					},
					{
						"coursedescription",
						this.GetCourseDescription()
					},
					{
						"date",
						DateTime.Now.ToString("MMMM d, yyyy")
					}
				};
				IMailMergeCodes mailMergeCodes = new MailMergeCodes();
				stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.InstructorAccommodations));
				stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.InstructorAccommodations));
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
				{
					PersonId = num,
					LuCourseId = num2,
					InstructorId = instructorId
				};
				emailClientManager.SendEmail(Setting.INSTRUCTOR_AccommodationLetter_EmailToStudentOnInstructorAcknowledgeReceived, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "InstructorFinalExamUpload");
				string str = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num2);
				string str2 = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num);
				base.Response.Redirect("iletter.aspx?lucid=" + str + "&pid=" + str2, true);
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00044D73 File Offset: 0x00042F73
		protected void chk_iagree_OnCheckedChanged(object sender, EventArgs e)
		{
			this.btn_submit.Enabled = this.chk_iagree.Checked;
		}

		// Token: 0x040007A9 RID: 1961
		public EventHandler<PrimaryAndSecondaryIdsRequiredArgs> OnStudentAndCourseIdsRequired;

		// Token: 0x040007AA RID: 1962
		public EventHandler<PrimaryAndSecondaryIdsRequiredArgs> OnInstructorAndAltContactIdsRequired;

		// Token: 0x040007AB RID: 1963
		public EventHandler<StringRequiredArgs> OnCourseDescriptionRequired;

		// Token: 0x040007AC RID: 1964
		protected Panel p_iagree;

		// Token: 0x040007AD RID: 1965
		protected Panel p_iagreepending;

		// Token: 0x040007AE RID: 1966
		protected Label lbl_iagreemessage;

		// Token: 0x040007AF RID: 1967
		protected CheckBox chk_iagree;

		// Token: 0x040007B0 RID: 1968
		protected Button btn_submit;

		// Token: 0x040007B1 RID: 1969
		protected Panel p_iagreedone;

		// Token: 0x040007B2 RID: 1970
		protected Label lbl_iagreedone;

		// Token: 0x040007B3 RID: 1971
		protected Label lbl_iagreedate;
	}
}
