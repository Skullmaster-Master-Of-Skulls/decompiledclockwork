using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000079 RID: 121
	public class StudentCommonInfoServiceManager : IStudentCommonInfo, IService
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0001543C File Offset: 0x0001363C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015450 File Offset: 0x00013650
		public LoadStudentCommonInfoResp LoadStudentCommonInfo(LoadStudentCommonInfoReq Request)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(Request.GetOperationContext());
			StudentCommonInfo studentCommonInfo = studentCommonInfoManager.LoadStudentCommonInfo(Request.PersonId);
			return new LoadStudentCommonInfoResp
			{
				Info = ((studentCommonInfo == null) ? null : studentCommonInfo.ToDTO())
			};
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015494 File Offset: 0x00013694
		public LoadStudentByEmailAddressResp LoadStudentByEmailAddress(LoadStudentByEmailAddressReq Request)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(Request.GetOperationContext());
			PersonBase personBase = studentCommonInfoManager.LoadStudentByEmailAddress(Request.EmailAddress);
			return new LoadStudentByEmailAddressResp
			{
				Student = ((personBase == null) ? null : personBase.ToDTO())
			};
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000154D8 File Offset: 0x000136D8
		public LoadMyStudentsResp LoadMyStudents(LoadMyStudentsReq Request)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(Request.GetOperationContext());
			IList<StudentWithCommonInfo> list = studentCommonInfoManager.LoadMyStudents(Request.CounsellorPersonId, Request.StartDate, Request.EndDate, Request.ShowStudentsIHaveAppsWith, Request.ShowStudentsIAmAdvisorFor, Request.IncludeCancelledAppointments, Request.IncludeNoShowAppointments, Request.OverrideAssignedCounsellorControlId);
			LoadMyStudentsResp loadMyStudentsResp = new LoadMyStudentsResp();
			IList<StudentWithCommonInfoDTO> studentsWithCommonInfo;
			if (list != null)
			{
				studentsWithCommonInfo = list.ToList<StudentWithCommonInfo>().ConvertAll<StudentWithCommonInfoDTO>((StudentWithCommonInfo g) => g.ToDTO());
			}
			else
			{
				studentsWithCommonInfo = null;
			}
			loadMyStudentsResp.StudentsWithCommonInfo = studentsWithCommonInfo;
			return loadMyStudentsResp;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001556C File Offset: 0x0001376C
		public LoadStudentsWithCommonInfoResp LoadStudentsWithCommonInfo(LoadStudentsWithCommonInfoReq Request)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(Request.GetOperationContext());
			IList<StudentWithCommonInfo> list = studentCommonInfoManager.LoadStudentsWithCommonInfo(Request.PersonIds);
			LoadStudentsWithCommonInfoResp loadStudentsWithCommonInfoResp = new LoadStudentsWithCommonInfoResp();
			IList<StudentWithCommonInfoDTO> studentsWithCommonInfo;
			if (list != null)
			{
				studentsWithCommonInfo = list.ToList<StudentWithCommonInfo>().ConvertAll<StudentWithCommonInfoDTO>((StudentWithCommonInfo g) => g.ToDTO());
			}
			else
			{
				studentsWithCommonInfo = null;
			}
			loadStudentsWithCommonInfoResp.StudentsWithCommonInfo = studentsWithCommonInfo;
			return loadStudentsWithCommonInfoResp;
		}
	}
}
