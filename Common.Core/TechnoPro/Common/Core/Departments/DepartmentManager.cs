using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Departments;
using TechnoPro.Common.DAO.Impl.Departments;
using TechnoPro.Common.ICore.Departments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Departments
{
	// Token: 0x02000106 RID: 262
	public class DepartmentManager : IDepartmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000450C5 File Offset: 0x000432C5
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000450CD File Offset: 0x000432CD
		internal IDepartmentDAO dao { get; set; }

		// Token: 0x06000AAF RID: 2735 RVA: 0x000450D6 File Offset: 0x000432D6
		public DepartmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DepartmentDAO(opContext);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000450F5 File Offset: 0x000432F5
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x000450FD File Offset: 0x000432FD
		public OperationContext OpContext { get; set; }

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00045108 File Offset: 0x00043308
		public IList<Department> GetDepartments()
		{
			return this.dao.GetDepartments();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00045128 File Offset: 0x00043328
		public Department GetDepartment(int departmentId)
		{
			return this.dao.GetDepartment(departmentId);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00045148 File Offset: 0x00043348
		public Department GetDepartment(string departmentName)
		{
			return this.dao.GetDepartment(departmentName);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00045168 File Offset: 0x00043368
		public int CreateDepartment(Department department)
		{
			return this.dao.CreateDepartment(department);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00045186 File Offset: 0x00043386
		public void UpdateDepartment(Department department)
		{
			this.dao.UpdateDepartment(department);
		}
	}
}
