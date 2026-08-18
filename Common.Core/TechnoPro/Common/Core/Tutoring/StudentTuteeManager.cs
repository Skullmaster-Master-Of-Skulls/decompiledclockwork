using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Tutoring;
using TechnoPro.Common.DAO.Tutoring;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Tutoring
{
	// Token: 0x02000031 RID: 49
	public class StudentTuteeManager : IStudentTuteeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000A350 File Offset: 0x00008550
		public StudentTuteeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentTuteeDAO(this.OpContext);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A373 File Offset: 0x00008573
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000A37B File Offset: 0x0000857B
		public OperationContext OpContext { get; set; }

		// Token: 0x060001DC RID: 476 RVA: 0x0000A384 File Offset: 0x00008584
		public IList<MyTutor> GetStudentMyTutors(int StudentPersonId, DateTime? StartDate, DateTime? EndDate)
		{
			return this.dao.GetStudentMyTutors(StudentPersonId, StartDate, EndDate);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public void MarkStudentCantFindTutor(int PersonId, int searchLucid, string searchLuc, string searchString)
		{
			this.SendCantFindTutorEmail(PersonId, searchLucid, searchLuc, searchString);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public void MarkStudentCantFindAvailability(int PersonId, params int[] TutorPids)
		{
			List<TutorBase> list = new List<TutorBase>();
			bool flag = TutorPids != null && TutorPids.Length != 0;
			if (flag)
			{
				ITutorManager tutorManager = new TutorManager(this.OpContext);
				for (int i = 0; i < TutorPids.Length; i++)
				{
					int tutorPid = TutorPids[i];
					bool flag2 = tutorPid > 0 && list.FirstOrDefault((TutorBase g) => g.PersonId == tutorPid) == null;
					if (flag2)
					{
						Tutor tutor = tutorManager.LoadTutorByPersonId(tutorPid);
						bool flag3 = tutor != null;
						if (flag3)
						{
							list.Add(tutor);
						}
					}
				}
			}
			string tutorListHtml = "<ul>\r\n" + string.Join("\r\n", (from g in list
			select "<li>" + g.GetName() + "</li>").ToArray<string>()) + "</ul>";
			string tutorListPlainText = string.Join("\r\n", (from g in list
			select "* " + g.GetName()).ToArray<string>());
			this.SendCantFindAvailabilityEmail(PersonId, tutorListHtml, tutorListPlainText);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A4E8 File Offset: 0x000086E8
		private void SendCantFindAvailabilityEmail(int PersonId, string TutorListHtml, string TutorListPlainText)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			MailMergeContext context = new MailMergeContext
			{
				PersonId = PersonId
			};
			string value = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Tutoring);
			IEmailManager emailManager = new EmailManager(this.OpContext);
			bool flag = string.IsNullOrEmpty(value);
			if (flag)
			{
				value = emailManager.GetDefaultFromAddress();
			}
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = context,
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = new Dictionary<string, string>
					{
						{
							"from",
							value
						},
						{
							"tutors",
							TutorListHtml ?? ""
						},
						{
							"tutorsplain",
							TutorListPlainText ?? ""
						}
					}
				}
			};
			TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.TUTORING_TuteeEmail_CantFindAvailability);
			emailManager.SendEmail(message);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A5B8 File Offset: 0x000087B8
		private void SendCantFindTutorEmail(int PersonId, int searchLucid, string searchLuc, string searchString)
		{
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			MailMergeContext context = new MailMergeContext
			{
				PersonId = PersonId
			};
			string value = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Tutoring);
			IEmailManager emailManager = new EmailManager(this.OpContext);
			bool flag = string.IsNullOrEmpty(value);
			if (flag)
			{
				value = emailManager.GetDefaultFromAddress();
			}
			MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				Context = context,
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = new Dictionary<string, string>
					{
						{
							"from",
							value
						},
						{
							"searchcourse",
							searchLuc ?? ""
						},
						{
							"searchkeyword",
							searchString ?? ""
						}
					}
				}
			};
			TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.TUTORING_TuteeEmail_CantFindTutor);
			emailManager.SendEmail(message);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A688 File Offset: 0x00008888
		public eTuteeStatus GetTuteeStatus(int StudentPersonId)
		{
			bool flag = StudentPersonId < 1;
			eTuteeStatus result;
			if (flag)
			{
				result = eTuteeStatus.NotAllowedToUseTutoring;
			}
			else
			{
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				int settingValue = webSettingManager.GetSettingValue<int>(Setting.TUTORING_StudentIsAuthorizedCid);
				bool flag2 = settingValue > 0;
				if (flag2)
				{
					bool flag3 = !this.dao.GetIsStudentAuthorizedToUseTutoring(StudentPersonId, settingValue);
					if (flag3)
					{
						return eTuteeStatus.NotAllowedToUseTutoring;
					}
				}
				result = (this.IsConfidentialityAgreementSigningRequiredForStudent(StudentPersonId) ? eTuteeStatus.ActiveNeedsConfidentiality : eTuteeStatus.Active);
			}
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A6FC File Offset: 0x000088FC
		public void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId)
		{
			IConfidentialityFormSignedManager confidentialityFormSignedManager = new ConfidentialityFormSignedManager(this.OpContext);
			confidentialityFormSignedManager.RecordConfidentialityAgreementSignedByTutor(StudentPersonId, "StudentConfidentialityAgreementSigned", "Tutee signed confid. agreement");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A728 File Offset: 0x00008928
		public bool IsConfidentialityAgreementSigningRequiredForStudent(int StudentPersonId)
		{
			IConfidentialityFormSignedManager confidentialityFormSignedManager = new ConfidentialityFormSignedManager(this.OpContext);
			return confidentialityFormSignedManager.IsConfidentialityAgreementSigningRequired(StudentPersonId, Setting.TUTORING_StudentConfidentialityResignPolicy, "StudentConfidentialityAgreementSigned", "Tutee signed confid. agreement");
		}

		// Token: 0x0400005F RID: 95
		private const string StudentConfidentialityControlName = "StudentConfidentialityAgreementSigned";

		// Token: 0x04000060 RID: 96
		private const string StudentConfidentialityControlCaption = "Tutee signed confid. agreement";

		// Token: 0x04000061 RID: 97
		private IStudentTuteeDAO dao;
	}
}
