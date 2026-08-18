using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.AlertTrigger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.AlertTrigger
{
	// Token: 0x02000177 RID: 375
	public class AlertTriggerDAO : IAlertTriggerDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x0007882A File Offset: 0x00076A2A
		public AlertTriggerDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0007883C File Offset: 0x00076A3C
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00078844 File Offset: 0x00076A44
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B56 RID: 2902 RVA: 0x00078850 File Offset: 0x00076A50
		public int[] FindFieldsFilledInForUserPerStudent(int pid, int[] cidsToCheck)
		{
			return AlertTriggerDAO.FindFieldsFilledInForUser(pid, cidsToCheck, this.OpContext, "SELECT DISTINCT controlid FROM ");
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00078874 File Offset: 0x00076A74
		public int[] FindFieldsFilledInForUserPerAppointment(int pid, int[] cidsToCheck)
		{
			return AlertTriggerDAO.FindFieldsFilledInForUser(pid, cidsToCheck, this.OpContext, "");
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00078898 File Offset: 0x00076A98
		private static int[] FindFieldsFilledInForUser(int pid, int[] cidsToCheck, OperationContext opContext, string query)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, opContext.TenantId);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@pid", DbType.Int32, pid);
			array[1] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", (from g in cidsToCheck
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			List<int> list = new List<int>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					list.Add((dataReader[0] is DBNull) ? 0 : ((int)dataReader[0]));
				}
			}
			return (from g in list
			where g > 0
			select g).Distinct<int>().ToArray<int>();
		}
	}
}
