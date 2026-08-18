using System;
using System.Data;
using System.Text;

namespace ClockWorkLogger
{
	// Token: 0x02000002 RID: 2
	public static class Adapters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ToLoggerFormat(this DataTable dt)
		{
			return dt.DefaultView.ToLoggerFormat();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		public static string ToLoggerFormat(this DataView dv)
		{
			bool flag = dv.Table.Columns.Count < 1;
			string result;
			if (flag)
			{
				result = "{no columns, no rows}";
			}
			else
			{
				string newLine = Environment.NewLine;
				StringBuilder stringBuilder = new StringBuilder(newLine);
				DataTable table = dv.Table;
				int[] array = new int[table.Columns.Count];
				for (int i = 0; i < table.Columns.Count; i++)
				{
					string columnName = table.Columns[i].ColumnName;
					array[i] = columnName.Length + 2;
					int num = array[i] - columnName.Length;
					bool flag2 = num > array[i] || num < 0;
					if (flag2)
					{
						num = 0;
					}
					stringBuilder.Append(columnName);
					stringBuilder.Append(new string(' ', num));
					stringBuilder.Append(" | ");
				}
				string value = new string('=', stringBuilder.Length);
				stringBuilder.Append(newLine);
				stringBuilder.Append(value);
				stringBuilder.Append(newLine);
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					for (int j = 0; j < array.Length; j++)
					{
						string text = row[j].ToString();
						int num2 = array[j] - text.Length;
						bool flag3 = num2 > array[j] || num2 < 0;
						if (flag3)
						{
							num2 = 0;
						}
						bool flag4 = num2 == 0 && text.Length > array[j];
						if (flag4)
						{
							text = text.Substring(0, array[j]);
						}
						stringBuilder.Append(text);
						stringBuilder.Append(new string(' ', num2));
						stringBuilder.Append(" | ");
					}
					stringBuilder.Append(newLine);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
