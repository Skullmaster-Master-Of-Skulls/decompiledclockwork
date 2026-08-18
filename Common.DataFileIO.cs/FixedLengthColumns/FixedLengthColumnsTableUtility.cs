using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TechnoPro.Common.DataFileIO.cs.FixedLengthColumns
{
	// Token: 0x02000005 RID: 5
	public static class FixedLengthColumnsTableUtility
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000032C8 File Offset: 0x000014C8
		private static string GetFixedLengthString(string originalStr, int targetLength, char padChar)
		{
			string text = originalStr.PadRight(targetLength, padChar);
			return (text.Length <= targetLength) ? text : text.Substring(0, targetLength);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000032F8 File Offset: 0x000014F8
		public static string ConvertTableToFixedLengthColumnString(string colNameEqualsColLengthPairs, DataTable table, bool includeHeaderRow, string sortString)
		{
			string newLine = Environment.NewLine;
			return FixedLengthColumnsTableUtility.ConvertTableToFixedLengthColumnString(colNameEqualsColLengthPairs, table, includeHeaderRow, sortString, ' ', "", newLine);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003324 File Offset: 0x00001524
		public static string ConvertTableToFixedLengthColumnString(string colNameEqualsColLengthPairs, DataTable table, bool includeHeaderRow, string sortString, char padChar, string colDelimiter, string rowDelimiter)
		{
			FixedLengthColumnsTableUtility.ColumnLen[] array = FixedLengthColumnsTableUtility.ColumnLen.Parse(colNameEqualsColLengthPairs);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			if (includeHeaderRow)
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (FixedLengthColumnsTableUtility.ColumnLen columnLen in array)
				{
					bool flag = !string.IsNullOrEmpty(colDelimiter) && stringBuilder3.Length > 0;
					if (flag)
					{
						stringBuilder3.Append(colDelimiter);
					}
					stringBuilder3.Append(FixedLengthColumnsTableUtility.GetFixedLengthString(columnLen.ColumnName, columnLen.ColumnLength, padChar));
				}
				stringBuilder.Append(stringBuilder3);
				bool flag2 = !string.IsNullOrEmpty(rowDelimiter);
				if (flag2)
				{
					stringBuilder.Append(rowDelimiter);
				}
			}
			bool flag3 = string.IsNullOrEmpty(table.TableName);
			if (flag3)
			{
				table.TableName = "table";
			}
			DataView dataView = new DataView
			{
				Table = table
			};
			bool flag4 = !string.IsNullOrEmpty(sortString);
			if (flag4)
			{
				try
				{
					dataView.Sort = sortString;
				}
				catch
				{
				}
			}
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				bool flag5 = !string.IsNullOrEmpty(rowDelimiter) && stringBuilder2.Length > 0;
				if (flag5)
				{
					stringBuilder2.Append(rowDelimiter);
				}
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (FixedLengthColumnsTableUtility.ColumnLen columnLen2 in array)
				{
					bool flag6 = !string.IsNullOrEmpty(colDelimiter) && stringBuilder3.Length > 0;
					if (flag6)
					{
						stringBuilder3.Append(colDelimiter);
					}
					string originalStr = (row[columnLen2.ColumnName] is DBNull) ? "" : row[columnLen2.ColumnName].ToString();
					stringBuilder3.Append(FixedLengthColumnsTableUtility.GetFixedLengthString(originalStr, columnLen2.ColumnLength, padChar));
				}
				stringBuilder2.Append(stringBuilder3);
			}
			return stringBuilder.ToString() + stringBuilder2.ToString();
		}

		// Token: 0x02000011 RID: 17
		public class ColumnLen
		{
			// Token: 0x0600003D RID: 61 RVA: 0x0000492D File Offset: 0x00002B2D
			public ColumnLen()
			{
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00004937 File Offset: 0x00002B37
			public ColumnLen(string colName, int len)
			{
				this.ColumnName = colName;
				this.ColumnLength = len;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00004951 File Offset: 0x00002B51
			// (set) Token: 0x06000040 RID: 64 RVA: 0x00004959 File Offset: 0x00002B59
			public string ColumnName { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00004962 File Offset: 0x00002B62
			// (set) Token: 0x06000042 RID: 66 RVA: 0x0000496A File Offset: 0x00002B6A
			public int ColumnLength { get; set; }

			// Token: 0x06000043 RID: 67 RVA: 0x00004974 File Offset: 0x00002B74
			public static FixedLengthColumnsTableUtility.ColumnLen[] Parse(string colNameEqualsColLengthPairs)
			{
				string[] array = colNameEqualsColLengthPairs.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				List<FixedLengthColumnsTableUtility.ColumnLen> list = new List<FixedLengthColumnsTableUtility.ColumnLen>();
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					bool flag = num > 0;
					if (flag)
					{
						int num2;
						bool flag2 = int.TryParse(text.Substring(num + 1), out num2) && num2 > 0;
						if (flag2)
						{
							list.Add(new FixedLengthColumnsTableUtility.ColumnLen(text.Substring(0, num), num2));
						}
					}
				}
				return list.ToArray();
			}
		}
	}
}
