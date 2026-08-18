using System;
using System.Data;

namespace TechnoPro.Common.DataTables.Adapters
{
	// Token: 0x0200000B RID: 11
	public static class DataTableAdapter
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002EA0 File Offset: 0x000010A0
		public static string AddColumn(this DataTable t, string proposedColName, Type colType)
		{
			string text = t.Columns.Contains(proposedColName) ? t.GetUniqueColName(proposedColName) : proposedColName;
			t.Columns.Add(text, colType);
			return text;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ED8 File Offset: 0x000010D8
		public static string GetUniqueColName(this DataTable t, string proposedColName)
		{
			int i = 1;
			while (i < 1000000)
			{
				string text = proposedColName + "_" + i++.ToString();
				if (!t.Columns.Contains(text))
				{
					return text;
				}
			}
			return null;
		}
	}
}
