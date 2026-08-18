using System;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkWebAPI
{
	// Token: 0x02000013 RID: 19
	public class DataSync
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00009D1C File Offset: 0x00007F1C
		public static void GetDaTripleDES(db conn, out UnivDataAdapter da, out IEncryption tripleDES)
		{
			DataSync.GetDaTripleDES(out da, out tripleDES);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009D28 File Offset: 0x00007F28
		public static void GetDaTripleDES(out UnivDataAdapter da, out IEncryption tripleDES)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string connectionString = "Provider=SQLOLEDB.1;" + clockWork.ConnectionString;
			UnivConnection univConnection = UnivOleDbFactory.CreateConnection(connectionString);
			da = univConnection.CreateDataAdapter();
			tripleDES = clockWork.Encryption;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009D64 File Offset: 0x00007F64
		public static int LookupLuCourseId(db conn, DataRow dr, int nid)
		{
			return DataSync.LookupLuCourseId(dr, nid);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009D80 File Offset: 0x00007F80
		public static int LookupLuCourseId(DataRow dr, int nid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string value = dr["subject"].ToString();
			string value2 = dr["course"].ToString();
			string value3 = dr["section"].ToString();
			DateTime dateTime = DateTime.Parse(dr["startdate"].ToString());
			DateTime dateTime2 = DateTime.Parse(dr["enddate"].ToString());
			DbParameter[] array = new DbParameter[5];
			for (int i = 0; i < 5; i++)
			{
				array[i] = clockWork.Parameter;
			}
			array[0].ParameterName = "@subject";
			array[0].DbType = DbType.String;
			array[0].Value = value;
			array[1].ParameterName = "@course";
			array[1].DbType = DbType.String;
			array[1].Value = value2;
			array[2].ParameterName = "@section";
			array[2].DbType = DbType.String;
			array[2].Value = value3;
			array[3].ParameterName = "@sdate";
			array[3].DbType = DbType.DateTime;
			array[3].Value = dateTime;
			array[4].ParameterName = "@edate";
			array[4].DbType = DbType.DateTime;
			array[4].Value = dateTime2;
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_FindCourseBySubjectCourseSection, array);
			bool flag = dataTable.Rows.Count > 0;
			int result;
			if (flag)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				array = new DbParameter[]
				{
					clockWork.Parameter
				};
				array[0].ParameterName = "@subject";
				array[0].DbType = DbType.String;
				array[0].Value = value;
				dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_FindSubjectId, array);
				bool flag2 = dataTable.Rows.Count > 0;
				int num;
				if (flag2)
				{
					num = (int)dataTable.Rows[0][0];
				}
				else
				{
					array = new DbParameter[]
					{
						clockWork.Parameter
					};
					array[0].ParameterName = "@subject";
					array[0].DbType = DbType.String;
					array[0].Value = value;
					object obj = clockWork.ExecuteScalar(QueryStorage.QS_INSERT_AddSubject, array);
					bool flag3 = obj != null;
					if (flag3)
					{
						num = (int)obj;
					}
					else
					{
						num = 0;
					}
				}
				bool flag4 = num > 0;
				if (flag4)
				{
					int num2 = 0;
					array = new DbParameter[10];
					for (int j = 0; j < 10; j++)
					{
						array[j] = clockWork.Parameter;
					}
					array[0].ParameterName = "@startdate";
					array[0].DbType = DbType.DateTime;
					array[0].Value = dateTime;
					array[1].ParameterName = "@enddate";
					array[1].DbType = DbType.DateTime;
					array[1].Value = dateTime2;
					array[2].ParameterName = "@term";
					array[2].DbType = DbType.String;
					array[2].Value = dr["term"].ToString();
					array[3].ParameterName = "@duration";
					array[3].DbType = DbType.String;
					array[3].Value = (dr.Table.Columns.Contains("duration") ? dr["duration"].ToString() : "");
					array[4].ParameterName = "@subjectid";
					array[4].DbType = DbType.Int32;
					array[4].Value = num;
					array[5].ParameterName = "@course";
					array[5].DbType = DbType.String;
					array[5].Value = value2;
					array[6].ParameterName = "@timeofday";
					array[6].DbType = DbType.String;
					array[6].Value = (dr.Table.Columns.Contains("timeofday") ? dr["timeofday"].ToString() : "");
					array[7].ParameterName = "@section";
					array[7].DbType = DbType.String;
					array[7].Value = value3;
					array[8].ParameterName = "@iid";
					array[8].DbType = DbType.Int32;
					array[8].Value = num2;
					array[9].ParameterName = "@nid";
					array[9].DbType = DbType.Int32;
					array[9].Value = nid;
					object obj2 = clockWork.ExecuteScalar(QueryStorage.QS_INSERT_AddCourse, array);
					bool flag5 = obj2 != null;
					int num3;
					if (flag5)
					{
						num3 = (int)obj2;
					}
					else
					{
						num3 = 0;
					}
					result = num3;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}
	}
}
