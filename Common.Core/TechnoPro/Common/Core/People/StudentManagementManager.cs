using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A9 RID: 169
	public class StudentManagementManager : IStudentManagementManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00022F43 File Offset: 0x00021143
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00022F4B File Offset: 0x0002114B
		public OperationContext OpContext { get; set; }

		// Token: 0x060005F9 RID: 1529 RVA: 0x00022F54 File Offset: 0x00021154
		public StudentManagementManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentManagementDAO(this.OpContext);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00022F78 File Offset: 0x00021178
		public StudentSummary LoadStudentSummary(int PersonId)
		{
			IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(this.OpContext);
			StudentCommonInfo studentCommonInfo = studentCommonInfoManager.LoadStudentCommonInfo(PersonId);
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			IList<BaseExtendedAppointment> appointments = baseAppointmentManager.LoadBaseExtendedAppointmentsByPersonId<BaseExtendedAppointment>(PersonId);
			return new StudentSummary
			{
				PersonId = PersonId,
				StudentCommonInfo = studentCommonInfo,
				Appointments = appointments
			};
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00022FD4 File Offset: 0x000211D4
		public IList<PersonBase> LoadActiveStudents(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadActiveStudents(StartDate, EndDate);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00022FF4 File Offset: 0x000211F4
		public string LoadStudentNumber(int PersonId)
		{
			return this.dao.LoadStudentNumber(PersonId);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00023014 File Offset: 0x00021214
		public IList<PersonBase> PermanentlyDeleteStudents(IList<int> StudentPersonIds)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			IList<PersonBase> studentsToDelete = peopleManager.LoadPersonsByIds(StudentPersonIds);
			IStudentManagementDAO studentManagementDAO = new StudentManagementDAO(this.OpContext);
			IList<PersonBase> list = studentManagementDAO.PermanentlyDeleteStudents(studentsToDelete);
			bool flag = list == null;
			if (flag)
			{
				throw new DatabaseDeleteFailedException("Failed to permanently delete students.  Operation was rolled back; no changes were made.");
			}
			return list;
		}

		// Token: 0x04000132 RID: 306
		private IStudentManagementDAO dao;
	}
}
