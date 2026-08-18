using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Departments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Departments
{
	// Token: 0x020000F2 RID: 242
	public class DepartmentDAO : IDepartmentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000483A9 File Offset: 0x000465A9
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x000483B1 File Offset: 0x000465B1
		internal DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x060006EA RID: 1770 RVA: 0x000483BA File Offset: 0x000465BA
		public DepartmentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000483EB File Offset: 0x000465EB
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x000483F3 File Offset: 0x000465F3
		public OperationContext OpContext { get; set; }

		// Token: 0x060006ED RID: 1773 RVA: 0x000483FC File Offset: 0x000465FC
		public Department GetDepartment(int departmentId)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@departmentid", DbType.Int32, departmentId);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from Departments where departmentid=@departmentid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DepartmentDAO.GetDepartment(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0004847C File Offset: 0x0004667C
		public int CreateDepartment(Department department)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@departmentid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@departmentname", DbType.String, department.Name),
				this.DatabaseManager.GetParameter("@description", DbType.String, string.IsNullOrEmpty(department.Description) ? string.Empty : department.Description),
				this.DatabaseManager.GetParameter("@institution", DbType.String, string.IsNullOrEmpty(department.Institution) ? string.Empty : department.Institution)
			};
			this.DatabaseManager.ExecuteNonQuery("insert into Departments (departmentname, [description], institution)\r\nvalues (@departmentname, @description, @institution)\r\nset @departmentid=SCOPE_IDENTITY()", array);
			bool flag = !(array[0].Value is DBNull);
			if (flag)
			{
				department.Id = (int)array[0].Value;
			}
			return department.Id;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00048568 File Offset: 0x00046768
		public void UpdateDepartment(Department department)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@deparmentid", DbType.Int32, department.Id),
				this.DatabaseManager.GetParameter("@departmentname", DbType.String, department.Name),
				this.DatabaseManager.GetParameter("@description", DbType.String, string.IsNullOrEmpty(department.Description) ? string.Empty : department.Description),
				this.DatabaseManager.GetParameter("@institution", DbType.String, string.IsNullOrEmpty(department.Institution) ? string.Empty : department.Institution)
			};
			this.DatabaseManager.ExecuteNonQuery("update Departments \r\nset departmentname=@departmentname,\r\n\t[description]=@description,\r\n\tinstitution=@institution\r\nwhere departmentid=@departmentid", parameters);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00048628 File Offset: 0x00046828
		public Department GetDepartment(string departmentName)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@departmentname", DbType.String, departmentName);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from Departments where departmentname=@departmentname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DepartmentDAO.GetDepartment(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<Department> GetDepartments()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000486A4 File Offset: 0x000468A4
		private static Department GetDepartment(IDataRecord record)
		{
			return new Department
			{
				Id = (int)record["departmentid"],
				Name = (string)record["departmentname"],
				Description = ((record["description"] is DBNull) ? string.Empty : ((string)record["description"])),
				Institution = (string)record["institution"]
			};
		}
	}
}
