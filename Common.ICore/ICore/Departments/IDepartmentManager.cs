using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.Departments
{
	// Token: 0x020000A3 RID: 163
	public interface IDepartmentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C9 RID: 1225
		IList<Department> GetDepartments();

		// Token: 0x060004CA RID: 1226
		Department GetDepartment(int departmentId);

		// Token: 0x060004CB RID: 1227
		Department GetDepartment(string departmentName);

		// Token: 0x060004CC RID: 1228
		int CreateDepartment(Department department);

		// Token: 0x060004CD RID: 1229
		void UpdateDepartment(Department department);
	}
}
