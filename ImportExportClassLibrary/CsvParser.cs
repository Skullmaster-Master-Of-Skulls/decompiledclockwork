using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000047 RID: 71
	public class CsvParser
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0001D81D File Offset: 0x0001C81D
		public static DataTable Parse(string data, bool headers)
		{
			return CsvParser.Parse(new StringReader(data), headers);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001D82B File Offset: 0x0001C82B
		public static DataTable Parse(string data)
		{
			return CsvParser.Parse(new StringReader(data));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001D838 File Offset: 0x0001C838
		public static DataTable Parse(TextReader stream)
		{
			return CsvParser.Parse(stream, false);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001D844 File Offset: 0x0001C844
		public static DataTable Parse(TextReader stream, bool headers)
		{
			DataTable dataTable = new DataTable();
			CsvParser.CsvStream csvStream = new CsvParser.CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			if (nextRow == null)
			{
				return null;
			}
			if (headers)
			{
				foreach (string text in nextRow)
				{
					if (text != null && text.Length > 0 && !dataTable.Columns.Contains(text))
					{
						dataTable.Columns.Add(text, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				nextRow = csvStream.GetNextRow();
			}
			while (nextRow != null)
			{
				while (nextRow.Length > dataTable.Columns.Count)
				{
					dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
				}
				dataTable.Rows.Add(nextRow);
				nextRow = csvStream.GetNextRow();
			}
			return dataTable;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001D928 File Offset: 0x0001C928
		public static DataTable ParseTabDelimiteredToClockWorkTable(TextReader stream, bool headers, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableName, int[] colIndicesToDecrypt)
		{
			char delimiter = '\t';
			return CsvParser.ParseTabDelimiteredToClockWorkTable(stream, headers, da, tripleDES, tableName, colIndicesToDecrypt, delimiter);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001D948 File Offset: 0x0001C948
		public static DataTable ParseTabDelimiteredToClockWorkTable(TextReader stream, bool headers, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableName, int[] colIndicesToDecrypt, char delimiter)
		{
			DataTable dataTable = new DataTable();
			string text = stream.ReadLine();
			string[] array = text.Split(new char[]
			{
				delimiter
			});
			if (headers)
			{
				foreach (string text2 in array)
				{
					if (text2 != null && text2.Length > 0 && !dataTable.Columns.Contains(text2))
					{
						dataTable.Columns.Add(text2, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				text = stream.ReadLine();
				if (text != null)
				{
					array = text.Split(new char[]
					{
						delimiter
					});
				}
			}
			while (array.Length > dataTable.Columns.Count)
			{
				dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
			}
			da.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
			DataTable dataTable2 = new DataTable();
			da.Fill(dataTable2);
			da.SelectCommand.CommandText = "TRUNCATE " + tableName;
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			DataTable dataTable3 = new DataTable();
			da.Fill(dataTable3);
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableName;
				da.Fill(new DataTable());
			}
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			dataTable3 = new DataTable();
			da.Fill(dataTable3);
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				return dataTable;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT INTO ");
			stringBuilder.Append(tableName);
			stringBuilder.Append(" (");
			for (int j = 0; j < dataTable2.Columns.Count; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append(",[");
				}
				else
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(dataTable2.Columns[j].ColumnName);
				stringBuilder.Append("]");
			}
			stringBuilder.Append(") VALUES (");
			int count = dataTable2.Columns.Count;
			for (int k = 0; k < count; k++)
			{
				if (k > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("@p");
				stringBuilder.Append(k.ToString());
			}
			stringBuilder.Append(")");
			string sql = stringBuilder.ToString();
			UnivTransaction univTransaction = null;
			try
			{
				da.Connection.Open();
				univTransaction = da.Connection.BeginTransaction();
				object[] oo = null;
				while (text != null)
				{
					array = text.Split(new char[]
					{
						delimiter
					});
					using (UnivCommand univCommand = da.CreateCommand(sql))
					{
						univCommand.Transaction = univTransaction;
						for (int l = 0; l < count; l++)
						{
							string parameterName = "@p" + l.ToString();
							if (l < array.Length)
							{
								if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0)
								{
									string text3 = array[l].Trim();
									if (text3.Length > 0)
									{
										byte[] parameterValue;
										oo = tripleDES.EncryptBatch(out parameterValue, array[l], oo);
										univCommand.Parameters.Add(parameterName, parameterValue);
									}
									else
									{
										univCommand.Parameters.Add(parameterName, new byte[0]);
									}
								}
								else
								{
									univCommand.Parameters.Add(parameterName, array[l]);
								}
							}
							else if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0)
							{
								univCommand.Parameters.Add(parameterName, new byte[0]);
							}
							else
							{
								univCommand.Parameters.Add(parameterName, "");
							}
						}
						univCommand.ExecuteNonQuery2();
					}
					text = stream.ReadLine();
				}
				univTransaction.Commit();
			}
			catch (Exception)
			{
				if (univTransaction != null)
				{
					univTransaction.Rollback();
				}
			}
			finally
			{
				da.Connection.Close();
			}
			return dataTable;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001DE10 File Offset: 0x0001CE10
		public static DataTable ParseTabDelimitered(TextReader stream, bool headers)
		{
			char delimiter = '\t';
			return CsvParser.ParseTabDelimitered(stream, headers, delimiter);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001DE28 File Offset: 0x0001CE28
		public static DataTable ParseTabDelimitered(TextReader stream, bool headers, char delimiter)
		{
			DataTable dataTable = new DataTable();
			string text = stream.ReadLine();
			string[] array = text.Split(new char[]
			{
				delimiter
			});
			if (headers)
			{
				foreach (string text2 in array)
				{
					if (text2 != null && text2.Length > 0 && !dataTable.Columns.Contains(text2))
					{
						dataTable.Columns.Add(text2, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				text = stream.ReadLine();
				if (text != null)
				{
					array = text.Split(new char[]
					{
						delimiter
					});
				}
			}
			else
			{
				for (int j = 0; j < array.Length; j++)
				{
					dataTable.Columns.Add("col" + j.ToString());
				}
			}
			while (text != null)
			{
				array = text.Split(new char[]
				{
					delimiter
				});
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < array.Length; k++)
				{
					if (k < dataTable.Columns.Count)
					{
						dataRow[k] = array[k];
					}
				}
				dataTable.Rows.Add(dataRow);
				text = stream.ReadLine();
			}
			return dataTable;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001DF88 File Offset: 0x0001CF88
		public static DataTable ParseToClockWorkTable(TextReader stream, bool headers, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tableName, int[] colIndicesToDecrypt)
		{
			DataTable dataTable = new DataTable();
			CsvParser.CsvStream csvStream = new CsvParser.CsvStream(stream);
			string[] nextRow = csvStream.GetNextRow();
			if (nextRow == null)
			{
				return null;
			}
			if (headers)
			{
				foreach (string text in nextRow)
				{
					if (text != null && text.Length > 0 && !dataTable.Columns.Contains(text))
					{
						dataTable.Columns.Add(text, typeof(string));
					}
					else
					{
						dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
					}
				}
				nextRow = csvStream.GetNextRow();
			}
			else
			{
				while (nextRow.Length > dataTable.Columns.Count)
				{
					dataTable.Columns.Add(CsvParser.GetNextColumnHeader(dataTable), typeof(string));
				}
			}
			da.SelectCommand.CommandText = "SELECT * FROM " + tableName + " WHERE 1=0";
			DataTable dataTable2 = new DataTable();
			da.Fill(dataTable2);
			da.SelectCommand.CommandText = "TRUNCATE TABLE " + tableName;
			da.Fill(new DataTable());
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			DataTable dataTable3 = new DataTable();
			da.Fill(dataTable3);
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				da.SelectCommand.CommandText = "DELETE FROM " + tableName;
				da.Fill(new DataTable());
			}
			da.SelectCommand.CommandText = "SELECT COUNT(*) FROM " + tableName;
			dataTable3 = new DataTable();
			da.Fill(dataTable3);
			if (dataTable3.Rows.Count > 0 && (int)dataTable3.Rows[0][0] > 0)
			{
				return dataTable;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT INTO ");
			stringBuilder.Append(tableName);
			stringBuilder.Append(" (");
			for (int j = 0; j < dataTable2.Columns.Count; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append(",[");
				}
				else
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(dataTable2.Columns[j].ColumnName);
				stringBuilder.Append("]");
			}
			stringBuilder.Append(") VALUES (");
			int count = dataTable2.Columns.Count;
			for (int k = 0; k < count; k++)
			{
				if (k > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("@p");
				stringBuilder.Append(k.ToString());
			}
			stringBuilder.Append(")");
			string commandText = stringBuilder.ToString();
			try
			{
				da.Connection.Open();
				da.SelectCommand.CommandText = commandText;
				object[] oo = null;
				int num = 0;
				while (nextRow != null && num++ < 350000000)
				{
					UnivCommand selectCommand = da.SelectCommand;
					selectCommand.Parameters.Clear();
					for (int l = 0; l < count; l++)
					{
						string parameterName = "@p" + l.ToString();
						if (l < nextRow.Length)
						{
							if (colIndicesToDecrypt == null || Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0)
							{
								string text2 = nextRow[l].Trim();
								if (text2.Length > 0)
								{
									byte[] parameterValue;
									oo = tripleDES.EncryptBatch(out parameterValue, nextRow[l], oo);
									selectCommand.Parameters.Add(parameterName, parameterValue);
								}
								else
								{
									selectCommand.Parameters.Add(parameterName, new byte[0]);
								}
							}
							else
							{
								selectCommand.Parameters.Add(parameterName, nextRow[l]);
							}
						}
						else if (colIndicesToDecrypt != null && Array.IndexOf<int>(colIndicesToDecrypt, l) >= 0)
						{
							selectCommand.Parameters.Add(parameterName, new byte[0]);
						}
						else
						{
							selectCommand.Parameters.Add(parameterName, "");
						}
					}
					selectCommand.ExecuteNonQuery2();
					nextRow = csvStream.GetNextRow();
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					da.Connection.Close();
				}
				catch
				{
				}
			}
			return dataTable;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001E408 File Offset: 0x0001D408
		private static string GetNextColumnHeader(DataTable table)
		{
			int num = 1;
			string text;
			do
			{
				text = "Column" + num++;
			}
			while (table.Columns.Contains(text));
			return text;
		}

		// Token: 0x02000048 RID: 72
		private class CsvStream
		{
			// Token: 0x060002E1 RID: 737 RVA: 0x0001E443 File Offset: 0x0001D443
			public CsvStream(TextReader s)
			{
				this.stream = s;
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0001E464 File Offset: 0x0001D464
			public string[] GetNextRow()
			{
				ArrayList arrayList = new ArrayList();
				for (;;)
				{
					string nextItem = this.GetNextItem();
					if (nextItem == null)
					{
						break;
					}
					arrayList.Add(nextItem);
				}
				if (arrayList.Count != 0)
				{
					return (string[])arrayList.ToArray(typeof(string));
				}
				return null;
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0001E4AC File Offset: 0x0001D4AC
			private string GetNextItem()
			{
				if (this.EOL)
				{
					this.EOL = false;
					return null;
				}
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				StringBuilder stringBuilder = new StringBuilder();
				char nextChar;
				for (;;)
				{
					nextChar = this.GetNextChar(true);
					if (this.EOS)
					{
						break;
					}
					if ((flag3 || !flag) && nextChar == ',')
					{
						goto Block_5;
					}
					if ((flag2 || flag3 || !flag) && (nextChar == '\n' || nextChar == '\r'))
					{
						goto IL_68;
					}
					if (!flag2 || nextChar != ' ')
					{
						if (flag2 && nextChar == '"')
						{
							flag = true;
							flag2 = false;
						}
						else if (flag2)
						{
							flag2 = false;
							stringBuilder.Append(nextChar);
						}
						else if (nextChar == '"' && flag)
						{
							if (this.GetNextChar(false) == '"')
							{
								stringBuilder.Append(this.GetNextChar(true));
							}
							else
							{
								flag3 = true;
							}
						}
						else
						{
							stringBuilder.Append(nextChar);
						}
					}
				}
				if (stringBuilder.Length <= 0)
				{
					return null;
				}
				return stringBuilder.ToString();
				Block_5:
				return stringBuilder.ToString();
				IL_68:
				this.EOL = true;
				if (nextChar == '\r' && this.GetNextChar(false) == '\n')
				{
					this.GetNextChar(true);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0001E5B4 File Offset: 0x0001D5B4
			private char GetNextChar(bool eat)
			{
				if (this.pos >= this.length)
				{
					this.length = this.stream.ReadBlock(this.buffer, 0, this.buffer.Length);
					if (this.length == 0)
					{
						this.EOS = true;
						return '\0';
					}
					this.pos = 0;
				}
				if (eat)
				{
					return this.buffer[this.pos++];
				}
				return this.buffer[this.pos];
			}

			// Token: 0x0400019D RID: 413
			private TextReader stream;

			// Token: 0x0400019E RID: 414
			private bool EOS;

			// Token: 0x0400019F RID: 415
			private bool EOL;

			// Token: 0x040001A0 RID: 416
			private char[] buffer = new char[4096];

			// Token: 0x040001A1 RID: 417
			private int pos;

			// Token: 0x040001A2 RID: 418
			private int length;
		}
	}
}
