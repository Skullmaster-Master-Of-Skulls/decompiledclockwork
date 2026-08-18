using System;
using System.Data;
using System.IO;

namespace TechnoPro.Common.DataFileIO.cs.Base
{
	// Token: 0x0200000C RID: 12
	public class BaseParser
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00004409 File Offset: 0x00002609
		public BaseParser(char colDelimiter, bool ignoreQuotes)
		{
			this.ignoreQuotes = ignoreQuotes;
			this.colDelimiter = colDelimiter;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004424 File Offset: 0x00002624
		public DataTable ParseFromString(string data, bool headers = true)
		{
			return this.Parse(new StringReader(data), true);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004444 File Offset: 0x00002644
		public DataTable Parse(string path, bool headers = true)
		{
			DataTable result;
			using (TextReader textReader = new StreamReader(path))
			{
				result = this.Parse(textReader, headers);
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004484 File Offset: 0x00002684
		public DataTable Parse(TextReader stream, bool headers = true)
		{
			DataTable dataTable = new DataTable("t");
			BaseStream baseStream = new BaseStream(stream, this.colDelimiter, this.ignoreQuotes);
			string[] nextRow = baseStream.GetNextRow();
			bool flag = nextRow == null;
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (headers)
				{
					foreach (string text in nextRow)
					{
						bool flag2 = text != null && text.Length > 0 && !dataTable.Columns.Contains(text);
						if (flag2)
						{
							dataTable.Columns.Add(text, typeof(string));
						}
						else
						{
							dataTable.Columns.Add(this.GetNextColumnHeader(dataTable), typeof(string));
						}
					}
					nextRow = baseStream.GetNextRow();
				}
				while (nextRow != null)
				{
					while (nextRow.Length > dataTable.Columns.Count)
					{
						dataTable.Columns.Add(this.GetNextColumnHeader(dataTable), typeof(string));
					}
					DataRowCollection rows = dataTable.Rows;
					object[] values = nextRow;
					rows.Add(values);
					nextRow = baseStream.GetNextRow();
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000045BC File Offset: 0x000027BC
		private string GetNextColumnHeader(DataTable table)
		{
			int num = 1;
			string text;
			bool flag;
			do
			{
				text = "Column" + num++.ToString();
				flag = !table.Columns.Contains(text);
			}
			while (!flag);
			return text;
		}

		// Token: 0x04000005 RID: 5
		private char colDelimiter;

		// Token: 0x04000006 RID: 6
		private bool ignoreQuotes;
	}
}
