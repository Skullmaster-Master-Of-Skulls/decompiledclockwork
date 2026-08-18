using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B6 RID: 182
	public class InventoryLoanStatusDAO : IInventoryLoanStatusDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0002E94F File Offset: 0x0002CB4F
		public InventoryLoanStatusDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0002E961 File Offset: 0x0002CB61
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0002E969 File Offset: 0x0002CB69
		public OperationContext OpContext { get; set; }

		// Token: 0x060004FF RID: 1279 RVA: 0x0002E974 File Offset: 0x0002CB74
		public int CreateLoanStatus(InventoryLoanStatus lStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@loanstatusid", DbType.Int32, 0),
				databaseLayer.GetParameter("@loanstatusname", DbType.String, lStatus.Name),
				databaseLayer.GetParameter("@loanstatusdescription", DbType.String, lStatus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO [InventoryV2_LoanStatus]\r\n                       ([LoanStatusName]\r\n                       ,[LoanStatusDescription])\r\n              VALUES\r\n                       (@loanstatusname\r\n                       ,@loanstatusdescription)\r\n\r\n            SET @loanstatusid = scope_identity()", array);
			return lStatus.LoanStatusId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002EA24 File Offset: 0x0002CC24
		public void UpdateLoanStatus(InventoryLoanStatus lStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loanstatusid", DbType.Int32, lStatus.LoanStatusId),
				databaseLayer.GetParameter("@loanstatusname", DbType.String, lStatus.Name),
				databaseLayer.GetParameter("@loanstatusdescription", DbType.String, lStatus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [InventoryV2_LoanStatus]\r\n                SET [LoanStatusName] = @loanstatusname\r\n                    ,[LoanStatusDescription] = @loanstatusdescription\r\n                WHERE LoanStatusID=@loanstatusid", parameters);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public InventoryLoanStatus GetLoanStatusById(int lStatusId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loanstatusid", DbType.Int32, lStatusId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from InventoryV2_LoanStatus where LoanStatusID=@loanstatusid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryLoanStatusDAO.GetLoanStatusFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002EB44 File Offset: 0x0002CD44
		public IList<InventoryLoanStatus> GetLoanStatusList()
		{
			List<InventoryLoanStatus> list = new List<InventoryLoanStatus>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from InventoryV2_LoanStatus"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryLoanStatus loanStatusFromReader = InventoryLoanStatusDAO.GetLoanStatusFromReader(dataReader);
						bool flag2 = loanStatusFromReader != null;
						if (flag2)
						{
							list.Add(loanStatusFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		internal static InventoryLoanStatus GetLoanStatusFromReader(IDataRecord record)
		{
			return new InventoryLoanStatus
			{
				LoanStatusId = Convert.ToInt32(record["LoanStatusID"]),
				Name = Convert.ToString(record["LoanStatusName"]),
				Description = Convert.ToString(record["LoanStatusDescription"])
			};
		}
	}
}
