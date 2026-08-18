using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;

namespace TechnoPro.Common.DAO.Helper
{
	// Token: 0x02000002 RID: 2
	public static class QueryHelperDAO
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IList<T> ExecuteQueryReturnList<T>(this DatabaseLayer db, string sql, Func<IDataReader, T> GetItemFromRecord, params DbParameter[] parameters) where T : class
		{
			IList<T> result;
			using (IDataReader dataReader = db.ExecuteQueryReader(sql, parameters))
			{
				if (dataReader == null)
				{
					result = null;
				}
				else
				{
					List<T> list = new List<T>();
					while (dataReader.Read())
					{
						T t = GetItemFromRecord(dataReader);
						if (t != null)
						{
							list.Add(t);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B4 File Offset: 0x000002B4
		public static IList<T> ExecuteQueryReturnList<T>(this DatabaseLayer db, string sql, Func<IDataReader, IList<T>> GetItemsFromRecord, params DbParameter[] parameters) where T : class
		{
			IList<T> result;
			using (IDataReader dataReader = db.ExecuteQueryReader(sql, parameters))
			{
				if (dataReader == null)
				{
					result = null;
				}
				else
				{
					List<T> list = new List<T>();
					while (dataReader.Read())
					{
						IList<T> list2 = GetItemsFromRecord(dataReader);
						if (list2 != null && list2.Count >= 1)
						{
							list.AddRange(list2);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		public static Task<IList<T>> ExecuteQueryReturnListAsync<T>(this DatabaseLayer db, string sql, Func<IDataReader, T> GetItemFromRecord, params DbParameter[] parameters) where T : class
		{
			QueryHelperDAO.<ExecuteQueryReturnListAsync>d__2<T> <ExecuteQueryReturnListAsync>d__;
			<ExecuteQueryReturnListAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<T>>.Create();
			<ExecuteQueryReturnListAsync>d__.db = db;
			<ExecuteQueryReturnListAsync>d__.sql = sql;
			<ExecuteQueryReturnListAsync>d__.GetItemFromRecord = GetItemFromRecord;
			<ExecuteQueryReturnListAsync>d__.parameters = parameters;
			<ExecuteQueryReturnListAsync>d__.<>1__state = -1;
			<ExecuteQueryReturnListAsync>d__.<>t__builder.Start<QueryHelperDAO.<ExecuteQueryReturnListAsync>d__2<T>>(ref <ExecuteQueryReturnListAsync>d__);
			return <ExecuteQueryReturnListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		public static Task<IList<T>> ExecuteQueryReturnListAsync<T>(this DatabaseLayer db, string sql, Func<IDataReader, IList<T>> GetItemsFromRecord, params DbParameter[] parameters) where T : class
		{
			QueryHelperDAO.<ExecuteQueryReturnListAsync>d__3<T> <ExecuteQueryReturnListAsync>d__;
			<ExecuteQueryReturnListAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<T>>.Create();
			<ExecuteQueryReturnListAsync>d__.db = db;
			<ExecuteQueryReturnListAsync>d__.sql = sql;
			<ExecuteQueryReturnListAsync>d__.GetItemsFromRecord = GetItemsFromRecord;
			<ExecuteQueryReturnListAsync>d__.parameters = parameters;
			<ExecuteQueryReturnListAsync>d__.<>1__state = -1;
			<ExecuteQueryReturnListAsync>d__.<>t__builder.Start<QueryHelperDAO.<ExecuteQueryReturnListAsync>d__3<T>>(ref <ExecuteQueryReturnListAsync>d__);
			return <ExecuteQueryReturnListAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D4 File Offset: 0x000003D4
		public static T ExecuteQueryReturnItem<T>(this DatabaseLayer db, string sql, Func<IDataReader, T> GetItemFromRecord, params DbParameter[] parameters) where T : class
		{
			T t;
			using (IDataReader dataReader = db.ExecuteQueryReader(sql, parameters))
			{
				if (dataReader == null || !dataReader.Read())
				{
					t = default(T);
					t = t;
				}
				else
				{
					t = GetItemFromRecord(dataReader);
				}
			}
			return t;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002228 File Offset: 0x00000428
		public static Task<T> ExecuteQueryReturnItemAsync<T>(this DatabaseLayer db, string sql, Func<IDataReader, T> GetItemFromRecord, params DbParameter[] parameters) where T : class
		{
			QueryHelperDAO.<ExecuteQueryReturnItemAsync>d__5<T> <ExecuteQueryReturnItemAsync>d__;
			<ExecuteQueryReturnItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<ExecuteQueryReturnItemAsync>d__.db = db;
			<ExecuteQueryReturnItemAsync>d__.sql = sql;
			<ExecuteQueryReturnItemAsync>d__.GetItemFromRecord = GetItemFromRecord;
			<ExecuteQueryReturnItemAsync>d__.parameters = parameters;
			<ExecuteQueryReturnItemAsync>d__.<>1__state = -1;
			<ExecuteQueryReturnItemAsync>d__.<>t__builder.Start<QueryHelperDAO.<ExecuteQueryReturnItemAsync>d__5<T>>(ref <ExecuteQueryReturnItemAsync>d__);
			return <ExecuteQueryReturnItemAsync>d__.<>t__builder.Task;
		}
	}
}
