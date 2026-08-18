using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.MarkedForDeletion;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;

namespace TechnoPro.Common.DAO.Impl.MarkedForDeletion
{
	// Token: 0x02000092 RID: 146
	public class MarkedForDeletionJobDAO : IMarkedForDeletionJobDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public MarkedForDeletionJobDAO()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00021909 File Offset: 0x0001FB09
		public MarkedForDeletionJobDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0002191B File Offset: 0x0001FB1B
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00021923 File Offset: 0x0001FB23
		public OperationContext OpContext { get; set; }

		// Token: 0x060003D0 RID: 976 RVA: 0x00003998 File Offset: 0x00001B98
		private MarkedForDeletionJob GetMarkedForDeletionJobFromRecord(IDataRecord record)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002192C File Offset: 0x0001FB2C
		public IList<MarkedForDeletionJob> LoadAllJobs()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IList<MarkedForDeletionJob> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(""))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<MarkedForDeletionJob> list = new List<MarkedForDeletionJob>();
					while (dataReader.Read())
					{
						MarkedForDeletionJob markedForDeletionJobFromRecord = this.GetMarkedForDeletionJobFromRecord(dataReader);
						bool flag2 = markedForDeletionJobFromRecord == null;
						if (!flag2)
						{
							list.Add(markedForDeletionJobFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000219C0 File Offset: 0x0001FBC0
		public IList<MarkedForDeletionJob> LoadJobsByType(params eMarkedForDeletionType[] types)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@types", DbType.String, string.Join(",", types.Select(delegate(eMarkedForDeletionType g)
			{
				int num = (int)g;
				return num.ToString();
			}).ToArray<string>()));
			DbParameter[] parameters = array;
			IList<MarkedForDeletionJob> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<MarkedForDeletionJob> list = new List<MarkedForDeletionJob>();
					while (dataReader.Read())
					{
						MarkedForDeletionJob markedForDeletionJobFromRecord = this.GetMarkedForDeletionJobFromRecord(dataReader);
						bool flag2 = markedForDeletionJobFromRecord == null;
						if (!flag2)
						{
							list.Add(markedForDeletionJobFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		public void UpdateJob(MarkedForDeletionJob job)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@whoamipid", DbType.Int32, this.OpContext.WhoAmI);
			array[1] = databaseLayer.GetParameter("@id", DbType.String, job.MarkedForDeletionJobId);
			array[2] = databaseLayer.GetParameter("@isactive", DbType.Boolean, job.IsActive);
			array[3] = databaseLayer.GetParameter("@memo", DbType.String, (job.Memo ?? "").Trim());
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@args";
			DbType pType = DbType.String;
			IDictionary<string, string> args = job.Args;
			array[num] = databaseLayer2.GetParameter(pName, pType, ((args != null) ? args.StringDictionaryToXml() : null) ?? "");
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("", parameters);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00021B78 File Offset: 0x0001FD78
		public void EnableJob(string markedForDeletionJobId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.String, markedForDeletionJobId)
			};
			databaseLayer.ExecuteNonQuery("", parameters);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		public void DisableJob(string markedForDeletionJobId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.String, markedForDeletionJobId)
			};
			databaseLayer.ExecuteNonQuery("", parameters);
		}
	}
}
