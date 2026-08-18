using System;

namespace TechnoPro.Common.DAO.Impl.Departments
{
	// Token: 0x020000F3 RID: 243
	internal class QueryStorageDepartment
	{
		// Token: 0x0400040E RID: 1038
		internal const string SQ_DEPARTMENT_BY_ID = "select * from Departments where departmentid=@departmentid";

		// Token: 0x0400040F RID: 1039
		internal const string SQ_DEPARTMENT_BY_NAME = "select * from Departments where departmentname=@departmentname";

		// Token: 0x04000410 RID: 1040
		internal const string IQ_CREATE_DEPARTMENT = "insert into Departments (departmentname, [description], institution)\r\nvalues (@departmentname, @description, @institution)\r\nset @departmentid=SCOPE_IDENTITY()";

		// Token: 0x04000411 RID: 1041
		internal const string UQ_DEPARTMENT_BY_ID = "update Departments \r\nset departmentname=@departmentname,\r\n\t[description]=@description,\r\n\tinstitution=@institution\r\nwhere departmentid=@departmentid";
	}
}
