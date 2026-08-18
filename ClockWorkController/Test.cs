using System;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkController
{
	// Token: 0x0200000F RID: 15
	public class Test
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00005EE4 File Offset: 0x000040E4
		public static void MarkInstructorAcknowledgedTestRequest(int appId, int acknowledgeValue)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "\r\n";
			clockWork.ExecuteNonQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appId),
				clockWork.GetParameter("@val", DbType.Int32, acknowledgeValue)
			});
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005F3C File Offset: 0x0000413C
		public static DataTable LoadStudentsWritingTest(int examId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@examid", DbType.Int32, examId)
			};
			DataTable tSource = clockWork.ExecuteQuery(QueryStorage.QS_Select_LoadAllStudentsWritingTest2a, parameters);
			return encryption.EncryptOrDecryptNameDataTableBatch(false, tSource, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005FB0 File Offset: 0x000041B0
		private static bool DoesColumnExist(string tableName, string colName)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = string.Concat(new string[]
			{
				"SELECT * from syscolumns WHERE id=object_id('",
				tableName,
				"') AND name='",
				colName,
				"'"
			});
			DataTable dataTable = clockWork.ExecuteQuery(query);
			return dataTable.Rows.Count > 0;
		}
	}
}
