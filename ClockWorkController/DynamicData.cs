using System;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using Databases;

namespace ClockWorkController
{
	// Token: 0x02000008 RID: 8
	public class DynamicData
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003D8C File Offset: 0x00001F8C
		public static void UpdateInsertDynamicDataOtherInfoPS(int pid, int cid, byte[] val)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] values = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, cid),
				clockWork.GetParameter("@val", DbType.Binary, val)
			};
			clockWork.Command.CommandText = QueryStorage.QS_UPDATE_UpdateDynamicDataPSOtherInfo1;
			clockWork.Command.Parameters.Clear();
			clockWork.Command.Parameters.AddRange(values);
			int num = clockWork.Command.ExecuteNonQuery();
			bool flag = num < 1;
			if (flag)
			{
				clockWork.Command.CommandText = QueryStorage.QS_INSERT_UpdateDynamicDataPSOtherInfo2;
				clockWork.Command.Parameters.Clear();
				clockWork.Command.Parameters.AddRange(values);
				clockWork.Command.ExecuteNonQuery();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003E70 File Offset: 0x00002070
		public static int UpdateInsertDynamicDataOtherInfoPS2(int pid, int cid, byte[] val)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, cid),
				clockWork.GetParameter("@cv", DbType.Binary, val)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_INSERTUPDATE_InsertOrUpdateDynamicDataPSOther, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value && (int)dataTable.Rows[0][0] > 0;
			int result;
			if (flag)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
