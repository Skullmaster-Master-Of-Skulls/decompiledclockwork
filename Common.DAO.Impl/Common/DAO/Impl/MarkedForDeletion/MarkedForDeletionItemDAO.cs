using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.MarkedForDeletion;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.DAO.Impl.MarkedForDeletion
{
	// Token: 0x02000091 RID: 145
	public class MarkedForDeletionItemDAO : IMarkedForDeletionItemDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public MarkedForDeletionItemDAO()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000216EE File Offset: 0x0001F8EE
		public MarkedForDeletionItemDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00021700 File Offset: 0x0001F900
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00021708 File Offset: 0x0001F908
		public OperationContext OpContext { get; set; }

		// Token: 0x060003C6 RID: 966 RVA: 0x00021714 File Offset: 0x0001F914
		public IList<MarkItemForDeletionResult> MarkItemsForDeletion(bool inProductionMode, eMarkedForDeletionType markedForDeletionType, IList<string> ids)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[3];
			array[0] = databaseLayer.GetParameter("@ids", DbType.String, string.Join(",", (from g in ids
			select g.ToString()).ToArray<string>()));
			array[1] = databaseLayer.GetParameter("@markedForDeletionType", DbType.Int32, (int)markedForDeletionType);
			array[2] = databaseLayer.GetParameter("@inProductionMode", DbType.String, inProductionMode);
			DbParameter[] parameters = array;
			IList<MarkItemForDeletionResult> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_MAINT_MARKEDFORDELETION_MarkForDeletion", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<MarkItemForDeletionResult> list = new List<MarkItemForDeletionResult>();
					while (dataReader.Read())
					{
						int num = (dataReader["actionType"] is DBNull) ? 0 : ((int)dataReader["actionType"]);
						list.Add(new MarkItemForDeletionResult
						{
							MarkedForDeletionItemId = dataReader["MakredForDeletionItemId"].ToString(),
							ActionType = (eMarkItemForDeletionActionType)(Enum.IsDefined(typeof(eMarkItemForDeletionActionType), num) ? num : 0)
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0002187C File Offset: 0x0001FA7C
		public void ExemptMarkedForDeletionItem(string markedForDeletionId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.String, markedForDeletionId)
			};
			databaseLayer.ExecuteNonQuery("", parameters);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000218C4 File Offset: 0x0001FAC4
		public void UnExemptMarkedForDeletionItem(string markedForDeletionId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.String, markedForDeletionId)
			};
			databaseLayer.ExecuteNonQuery("", parameters);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00003998 File Offset: 0x00001B98
		public MarkedForDeletionItem LoadMarkedForDeletionItemById(string markedForDeletionId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteMarkedForDeletionItemById(string markedForDeletionId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00003998 File Offset: 0x00001B98
		public IList<MarkedForDeletionItem> LoadMarkedForDeletionItemsByType(eMarkedForDeletionType type, bool includeExempt)
		{
			throw new NotImplementedException();
		}
	}
}
