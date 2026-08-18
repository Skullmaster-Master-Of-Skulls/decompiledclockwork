using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Settings;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000045 RID: 69
	public class ReferenceTableSettingManager : IReferenceTableSettingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010566 File Offset: 0x0000E766
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0001056E File Offset: 0x0000E76E
		protected IReferenceTableSettingDAO ReferenceTableSettingDAO { get; set; }

		// Token: 0x060002C6 RID: 710 RVA: 0x00010577 File Offset: 0x0000E777
		public ReferenceTableSettingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.ReferenceTableSettingDAO = new ReferenceTableSettingDAO(opContext);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00010596 File Offset: 0x0000E796
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0001059E File Offset: 0x0000E79E
		public OperationContext OpContext { get; set; }

		// Token: 0x060002C9 RID: 713 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted, string overrideSql)
		{
			return this.ReferenceTableSettingDAO.GetValues(tableName, idColumnName, columnNames, isValueEncrypted, overrideSql);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000105CC File Offset: 0x0000E7CC
		public IList<KeyValuePair<int, string[]>> GetValues(string tableName, string idColumnName, string[] columnNames, bool[] isValueEncrypted)
		{
			return this.GetValues(tableName, idColumnName, columnNames, isValueEncrypted, null);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000105EC File Offset: 0x0000E7EC
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql, bool overrideSortByDisplayName)
		{
			return this.ReferenceTableSettingDAO.GetValues(tableName, idColumnName, columnName, isValueEncrypted, overrideSql, overrideSortByDisplayName);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00010614 File Offset: 0x0000E814
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted)
		{
			return this.GetValues(tableName, idColumnName, columnName, isValueEncrypted, null);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010634 File Offset: 0x0000E834
		public IList<KeyValuePair<int, string>> GetValues(string tableName, string idColumnName, string columnName, bool isValueEncrypted, string overrideSql)
		{
			return this.GetValues(tableName, idColumnName, columnName, isValueEncrypted, overrideSql, false);
		}
	}
}
