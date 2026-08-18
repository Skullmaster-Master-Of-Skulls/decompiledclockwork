using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x02000032 RID: 50
	public class StudentCommonInfoClientManager : IStudentCommonInfoClientManager, IWebService
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00008D6C File Offset: 0x00006F6C
		public StudentCommonInfoDTO LoadStudentCommonInfo(int PersonId)
		{
			LoadStudentCommonInfoReq loadStudentCommonInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentCommonInfoReq>();
			loadStudentCommonInfoReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IStudentCommonInfo>().LoadStudentCommonInfo(loadStudentCommonInfoReq).Info;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public PersonBaseDTO LoadStudentByEmailAddress(string EmailAddress)
		{
			LoadStudentByEmailAddressReq loadStudentByEmailAddressReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentByEmailAddressReq>();
			loadStudentByEmailAddressReq.EmailAddress = EmailAddress;
			return ClientServiceFactory.GetClientInstance<IStudentCommonInfo>().LoadStudentByEmailAddress(loadStudentByEmailAddressReq).Student;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008DDC File Offset: 0x00006FDC
		public IList<StudentWithCommonInfoDTO> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0)
		{
			LoadMyStudentsReq loadMyStudentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMyStudentsReq>();
			loadMyStudentsReq.CounsellorPersonId = CounsellorPersonId;
			loadMyStudentsReq.StartDate = StartDate;
			loadMyStudentsReq.EndDate = EndDate;
			loadMyStudentsReq.ShowStudentsIHaveAppsWith = ShowStudentsIHaveAppsWith;
			loadMyStudentsReq.ShowStudentsIAmAdvisorFor = ShowStudentsIAmAdvisorFor;
			loadMyStudentsReq.IncludeCancelledAppointments = IncludeCancelledAppointments;
			loadMyStudentsReq.IncludeNoShowAppointments = IncludeNoShowAppointments;
			loadMyStudentsReq.OverrideAssignedCounsellorControlId = OverrideAssignedAdvisorControlId;
			return ClientServiceFactory.GetClientInstance<IStudentCommonInfo>().LoadMyStudents(loadMyStudentsReq).StudentsWithCommonInfo;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008E50 File Offset: 0x00007050
		public IList<StudentWithCommonInfoDTO> LoadStudentsWithCommonInfo(IList<int> PersonIds)
		{
			LoadStudentsWithCommonInfoReq loadStudentsWithCommonInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsWithCommonInfoReq>();
			loadStudentsWithCommonInfoReq.PersonIds = PersonIds;
			return ClientServiceFactory.GetClientInstance<IStudentCommonInfo>().LoadStudentsWithCommonInfo(loadStudentsWithCommonInfoReq).StudentsWithCommonInfo;
		}
	}
}
