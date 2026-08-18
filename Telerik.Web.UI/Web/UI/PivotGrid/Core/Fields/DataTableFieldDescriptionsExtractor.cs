using System;
using System.Collections.Generic;
using System.Data;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAF RID: 3247
	internal class DataTableFieldDescriptionsExtractor : IFieldInfoExtractor
	{
		// Token: 0x0600799F RID: 31135 RVA: 0x001BEFAE File Offset: 0x001BD1AE
		public DataTableFieldDescriptionsExtractor(DataTable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			this.table = table;
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x001BEFCC File Offset: 0x001BD1CC
		public IEnumerable<IPivotFieldInfo> GetDescriptions()
		{
			List<IPivotFieldInfo> list = new List<IPivotFieldInfo>();
			foreach (object obj in this.table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				list.Add(new DataRowFieldInfo(dataColumn)
				{
					PreferredRole = FieldInfoHelper.GetRoleForType(dataColumn.DataType)
				});
			}
			return list;
		}

		// Token: 0x04002146 RID: 8518
		private DataTable table;
	}
}
