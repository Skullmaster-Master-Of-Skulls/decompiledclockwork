using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007A RID: 122
	public class StudentManagementServiceManager : IStudentManagement, IService
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x000155D4 File Offset: 0x000137D4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000155E8 File Offset: 0x000137E8
		public LoadStudentSummaryResp LoadStudentSummary(LoadStudentSummaryReq Request)
		{
			IStudentManagementManager studentManagementManager = new StudentManagementManager(Request.GetOperationContext());
			StudentSummary studentSummary = studentManagementManager.LoadStudentSummary(Request.PersonId);
			return new LoadStudentSummaryResp
			{
				StudentSummary = ((studentSummary == null) ? null : studentSummary.ToDTO())
			};
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001562C File Offset: 0x0001382C
		public PermanentlyDeleteStudentsResp PermanentlyDeleteStudents(PermanentlyDeleteStudentsReq Request)
		{
			IStudentManagementManager studentManagementManager = new StudentManagementManager(Request.GetOperationContext());
			IList<PersonBase> list = studentManagementManager.PermanentlyDeleteStudents(Request.StudentPersonIdsToDelete);
			PermanentlyDeleteStudentsResp permanentlyDeleteStudentsResp = new PermanentlyDeleteStudentsResp();
			IList<PersonBaseDTO> studentsDeleted;
			if (list != null)
			{
				studentsDeleted = (from g in list
				select g.ToDTO()).ToList<PersonBaseDTO>();
			}
			else
			{
				studentsDeleted = null;
			}
			permanentlyDeleteStudentsResp.StudentsDeleted = studentsDeleted;
			return permanentlyDeleteStudentsResp;
		}
	}
}
