using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Departments
{
	// Token: 0x02000077 RID: 119
	public interface IDepartmentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002ED RID: 749
		IList<Department> GetDepartments();

		// Token: 0x060002EE RID: 750
		Department GetDepartment(int departmentId);

		// Token: 0x060002EF RID: 751
		Department GetDepartment(string departmentName);

		// Token: 0x060002F0 RID: 752
		int CreateDepartment(Department department);

		// Token: 0x060002F1 RID: 753
		void UpdateDepartment(Department department);
	}
}
