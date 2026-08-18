using System;
using System.Data;
using TechnoPro.Common.DAO.General;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.General;

namespace TechnoPro.Common.DAO.Impl.General
{
	// Token: 0x020000CA RID: 202
	public class ModificationHistoryItemDAO : IModificationHistoryItemDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x000343C3 File Offset: 0x000325C3
		public ModificationHistoryItemDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000343D5 File Offset: 0x000325D5
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000343DD File Offset: 0x000325DD
		public OperationContext OpContext { get; set; }

		// Token: 0x0600056E RID: 1390 RVA: 0x000343E8 File Offset: 0x000325E8
		public static ModificationHistoryItemBase GetModificationHistoryItemBaseFromRecord(IDataReader record, string prefix = "")
		{
			string name = prefix + "DateCreated";
			string name2 = prefix + "WhoCreatedPersonId";
			string name3 = prefix + "DateLastModified";
			string name4 = prefix + "WhoLastModifiedPersonId";
			return new ModificationHistoryItemBase
			{
				DateCreated = ((record[name] is DBNull) ? null : new DateTime?((DateTime)record[name])),
				DateLastModified = ((record[name3] is DBNull) ? null : new DateTime?((DateTime)record[name3])),
				WhoCreatedPersonId = ((record[name2] is DBNull) ? 0 : ((int)record[name2])),
				WhoLastModifiedPersonId = ((record[name4] is DBNull) ? 0 : ((int)record[name4]))
			};
		}
	}
}
