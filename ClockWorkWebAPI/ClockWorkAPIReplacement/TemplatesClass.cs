using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000071 RID: 113
	public class TemplatesClass
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00024C18 File Offset: 0x00022E18
		private static long IndexOf(Stream s, int byte1, int byte2, ref Queue bytes, bool recordBytes)
		{
			int i = s.ReadByte();
			int num = 0;
			while (i >= 0)
			{
				byte b = (byte)i;
				bool flag = (int)b == byte1;
				if (flag)
				{
					int num2 = s.ReadByte();
					bool flag2 = num2 >= 0;
					long result;
					if (flag2)
					{
						byte b2 = (byte)num2;
						bool flag3 = (int)b2 == byte2;
						if (!flag3)
						{
							if (recordBytes)
							{
								bytes.Enqueue(b);
							}
							if (recordBytes)
							{
								bytes.Enqueue(b2);
							}
							num++;
							goto IL_C0;
						}
						result = s.Position;
					}
					else
					{
						if (recordBytes)
						{
							bytes.Enqueue(b);
						}
						result = -1L;
					}
					return result;
				}
				if (recordBytes)
				{
					bytes.Enqueue(b);
				}
				IL_C0:
				i = s.ReadByte();
				num++;
			}
			return -1L;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00024D0C File Offset: 0x00022F0C
		public static string GetExcelConnectionString(string filename)
		{
			bool flag = IntPtr.Size == 4;
			string result;
			if (flag)
			{
				result = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			else
			{
				result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", filename);
			}
			return result;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00024D48 File Offset: 0x00022F48
		private static string AllowUserToChooseColumns(ref DataView dv, bool askUserToFilterColumns, bool showTableName)
		{
			string text;
			return TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, showTableName, null, out text);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00024D68 File Offset: 0x00022F68
		public static string ChangeFileExtension(string filename, string newExtensionWithDot)
		{
			string text = filename;
			string extension = Path.GetExtension(filename);
			bool flag = extension.Length > 0;
			if (flag)
			{
				text = text.Substring(0, text.Length - extension.Length);
			}
			return text + newExtensionWithDot;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00024DB0 File Offset: 0x00022FB0
		private static string AllowUserToChooseColumns(ref DataView dv, bool askUserToFilterColumns, bool showTableName, string filterColumnsChecked, out string newFilterColumnsChecked)
		{
			newFilterColumnsChecked = "";
			return "";
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00024DD0 File Offset: 0x00022FD0
		public static string GetConnectionStringOleDb_Excel(string filename)
		{
			return TemplatesClass.GetExcelConnectionString(filename);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00024DE8 File Offset: 0x00022FE8
		public static string FixColumnName(ref ArrayList usedColNames, string colName)
		{
			string text = "";
			foreach (char c in colName)
			{
				bool flag = char.IsLetter(c);
				if (flag)
				{
					text += c.ToString();
				}
				else
				{
					bool flag2 = text.Length > 0 && (char.IsDigit(c) || c == '_');
					if (flag2)
					{
						text += c.ToString();
					}
				}
			}
			bool flag3 = text.Length < 1;
			if (flag3)
			{
				text = "column";
			}
			bool flag4 = usedColNames.Contains(text);
			if (flag4)
			{
				int num = 1;
				string str = text;
				while (usedColNames.Contains(text) && num < 10000)
				{
					text = str + num.ToString();
					num++;
				}
			}
			return text;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00024ED0 File Offset: 0x000230D0
		public static string FixColumnNameOld(ref ArrayList usedColNames, string cName)
		{
			string text = cName.Replace("/", "");
			text = text.Replace(".", "");
			return text.Replace(",", "");
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00024F18 File Offset: 0x00023118
		public static void ShowDelimiteredTextFile(string fn)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			TemplatesClass.ShowDelimiteredTextFile(fn, !flag);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00024F44 File Offset: 0x00023144
		public static void ShowDelimiteredTextFile(string fn, bool showInNotepad)
		{
			string text = Path.Combine(Environment.SystemDirectory, "notepad.exe");
			bool flag = !File.Exists(text);
			if (flag)
			{
				text = Path.Combine(Environment.SystemDirectory, "system32");
				bool flag2 = !File.Exists(text);
				if (flag2)
				{
					text = null;
				}
			}
			bool flag3 = !showInNotepad;
			if (flag3)
			{
				text = null;
			}
			bool flag4 = text != null;
			if (flag4)
			{
				Process.Start(text, fn);
			}
			else
			{
				Process.Start(fn);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00024FBB File Offset: 0x000231BB
		public static void ExportToDelimeteredText(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			TemplatesClass.ExportToDelimeteredText(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns, colDelimiter, rowDelimiter);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00024FD1 File Offset: 0x000231D1
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToDelimeteredText(dv, tempFilename, startDirectory, askUserToFilterColumns, ",", Environment.NewLine);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00024FE8 File Offset: 0x000231E8
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			string text = TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false);
			bool flag = text == null;
			if (!flag)
			{
				DataTable table = dv.Table;
				Type type = Type.GetType("System.Int32");
				Type type2 = Type.GetType("System.DateTime");
				Type type3 = Type.GetType("System.Double");
				StreamWriter streamWriter = new StreamWriter(tempFilename);
				bool flag2 = true;
				for (int i = 0; i < table.Columns.Count; i++)
				{
					DataColumn dataColumn = table.Columns[i];
					bool flag3 = dataColumn.ColumnMapping != MappingType.Hidden;
					if (flag3)
					{
						string text2 = dataColumn.ColumnName;
						text2 = text2.Replace(colDelimiter, " ");
						text2 = text2.Replace(rowDelimiter, "");
						bool flag4 = !flag2;
						if (flag4)
						{
							streamWriter.Write(colDelimiter);
						}
						else
						{
							flag2 = false;
						}
						streamWriter.Write(text2);
					}
				}
				streamWriter.Write(rowDelimiter);
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					flag2 = true;
					for (int j = 0; j < table.Columns.Count; j++)
					{
						DataColumn dataColumn2 = table.Columns[j];
						bool flag5 = dataColumn2.ColumnMapping != MappingType.Hidden;
						if (flag5)
						{
							bool flag6 = !flag2;
							if (flag6)
							{
								streamWriter.Write(colDelimiter);
							}
							else
							{
								flag2 = false;
							}
							bool flag7 = table.Columns[j].DataType == type;
							string text3;
							if (flag7)
							{
								text3 = row[j].ToString();
							}
							else
							{
								bool flag8 = table.Columns[j].DataType == type2;
								if (flag8)
								{
									bool flag9 = row[j] == DBNull.Value;
									if (flag9)
									{
										text3 = "";
									}
									else
									{
										DateTime dateTime = (DateTime)row[j];
										text3 = dateTime.ToShortDateString();
										bool flag10 = dateTime.Hour != 0 || dateTime.Minute != 0;
										if (flag10)
										{
											text3 = text3 + " " + dateTime.ToLongTimeString();
										}
									}
								}
								else
								{
									bool flag11 = table.Columns[j].DataType == type3;
									if (flag11)
									{
										text3 = row[j].ToString();
									}
									else
									{
										text3 = row[j].ToString();
									}
								}
							}
							text3 = text3.Replace(colDelimiter, " ").Replace(rowDelimiter, "");
							streamWriter.Write(text3);
						}
					}
					streamWriter.Write(rowDelimiter);
				}
				streamWriter.Close();
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000252F0 File Offset: 0x000234F0
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToFormattedText(t, tempFilename, askUserToFilterColumns, true);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00025300 File Offset: 0x00023500
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			DataView dv = new DataView(t);
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, showNotepad, true);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00025320 File Offset: 0x00023520
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, true, true);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002532E File Offset: 0x0002352E
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, showNotepad, true);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002533C File Offset: 0x0002353C
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns, bool showNotepad, bool showColumnNames)
		{
			DataTable table = dv.Table;
			string text = TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false);
			bool flag = text == null;
			if (!flag)
			{
				StreamWriter streamWriter = new StreamWriter(tempFilename);
				int[] array = new int[table.Columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = table.Columns[i].ColumnName.Length + 1;
				}
				foreach (object obj in dv)
				{
					DataRowView dataRowView = (DataRowView)obj;
					DataRow row = dataRowView.Row;
					for (int j = 0; j < table.Columns.Count; j++)
					{
						DataColumn dataColumn = table.Columns[j];
						bool flag2 = dataColumn.ColumnMapping != MappingType.Hidden;
						if (flag2)
						{
							int num = row[j].ToString().Trim().Length + 1;
							bool flag3 = num > array[j];
							if (flag3)
							{
								array[j] = num;
							}
						}
					}
				}
				if (showColumnNames)
				{
					for (int k = 0; k < table.Columns.Count; k++)
					{
						DataColumn dataColumn2 = table.Columns[k];
						bool flag4 = dataColumn2.ColumnMapping != MappingType.Hidden;
						if (flag4)
						{
							string text2 = dataColumn2.ColumnName;
							int num2 = array[k] - text2.Length;
							bool flag5 = num2 > 0;
							if (flag5)
							{
								text2 += new string(' ', num2);
							}
							streamWriter.Write(text2);
						}
					}
					streamWriter.WriteLine();
					for (int l = 0; l < table.Columns.Count; l++)
					{
						DataColumn dataColumn3 = table.Columns[l];
						bool flag6 = dataColumn3.ColumnMapping != MappingType.Hidden;
						if (flag6)
						{
							string text3 = "";
							int num3 = array[l] - text3.Length - 1;
							bool flag7 = num3 > 0;
							if (flag7)
							{
								text3 += new string('=', num3);
							}
							text3 += "+";
							streamWriter.Write(text3);
						}
					}
					streamWriter.WriteLine();
				}
				foreach (object obj2 in dv)
				{
					DataRowView dataRowView2 = (DataRowView)obj2;
					DataRow row2 = dataRowView2.Row;
					for (int m = 0; m < table.Columns.Count; m++)
					{
						DataColumn dataColumn4 = table.Columns[m];
						bool flag8 = dataColumn4.ColumnMapping != MappingType.Hidden;
						if (flag8)
						{
							string text4 = row2[m].ToString();
							int num4 = array[m] - text4.Length;
							bool flag9 = num4 > 0;
							if (flag9)
							{
								text4 += new string(' ', num4);
							}
							streamWriter.Write(text4);
						}
					}
					streamWriter.WriteLine();
				}
				streamWriter.Close();
				bool flag10 = showNotepad && File.Exists(tempFilename);
				if (flag10)
				{
					Process.Start(tempFilename, "");
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000256D0 File Offset: 0x000238D0
		public static string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.GetFileNameWithoutExtension(tempFileName) + fnExtension;
			return Path.Combine(text, path);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00025730 File Offset: 0x00023930
		public static string GetTempFilenameGuid(string fnExtension)
		{
			string path = string.Format("{0}_{1}{2}", Guid.NewGuid().ToString(), DateTime.Now.Millisecond.ToString(), fnExtension);
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			return Path.Combine(text, path);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000257B4 File Offset: 0x000239B4
		public static DataRow CopyRow(DataRow drSource, DataTable tDest)
		{
			DataRow dataRow = tDest.NewRow();
			for (int i = 0; i < tDest.Columns.Count; i++)
			{
				dataRow[i] = drSource[i];
			}
			return dataRow;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000257FC File Offset: 0x000239FC
		public static ArrayList SetCodeValues(DataRow dr, ref ArrayList codes)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in codes)
			{
				Code code = (Code)obj;
				string text = code.codeText.ToUpper().Trim();
				string[] array = code.codeValue.Split(new char[]
				{
					','
				});
				ArrayList arrayList2 = new ArrayList();
				foreach (string text2 in array)
				{
					arrayList2.Add(text2.Trim());
				}
				string text3 = ", ";
				bool flag = text.Length > 2 && text[0] == '[';
				if (flag)
				{
					int num = text.IndexOf(']', 1);
					bool flag2 = num > 1;
					if (flag2)
					{
						text3 = text.Substring(1, num - 1);
						bool flag3 = text3.CompareTo("\\N") == 0;
						if (flag3)
						{
							text3 = Environment.NewLine;
						}
						text = text.Substring(num + 1);
					}
				}
				string text4 = " ";
				string[] array3 = text.Split(new char[]
				{
					' '
				});
				bool flag4 = array3.Length == 1;
				if (flag4)
				{
					array3 = text.Split(new char[]
					{
						','
					});
					text4 = ", ";
				}
				bool flag5 = array3.Length == 1;
				if (flag5)
				{
					array3 = text.Split(new char[]
					{
						'.'
					});
					text4 = Environment.NewLine;
				}
				string text5 = "";
				foreach (string text6 in array3)
				{
					bool flag6 = text6.Length > 0 && text6[text6.Length - 1] == '1';
					string text7;
					bool flag8;
					if (flag6)
					{
						bool flag7 = text6.Length == 1;
						if (flag7)
						{
							text7 = "";
						}
						else
						{
							text7 = text6.Substring(0, text6.Length - 1);
						}
						flag8 = true;
					}
					else
					{
						text7 = text6;
						flag8 = false;
					}
					bool flag9 = false;
					for (int k = 0; k < dr.Table.Columns.Count; k++)
					{
						string text8 = dr.Table.Columns[k].ColumnName.ToUpper().Trim();
						string text9 = dr[k].ToString().Trim();
						bool flag10 = text8.CompareTo(text7) == 0;
						if (flag10)
						{
							bool flag11 = text9.Length > 0;
							if (flag11)
							{
								bool flag12 = flag8;
								bool flag13;
								if (flag12)
								{
									string[] array5 = text5.Split(text4.ToCharArray());
									flag13 = (Array.IndexOf<string>(array5, text9) < 0);
								}
								else
								{
									flag13 = true;
								}
								bool flag14 = flag13;
								if (flag14)
								{
									bool flag15 = text5.Length > 0;
									if (flag15)
									{
										text5 += text4;
									}
									text5 += text9;
								}
							}
							flag9 = true;
							break;
						}
					}
					bool flag16 = !flag9;
					if (flag16)
					{
						bool flag17 = text7.CompareTo("DATE") == 0;
						if (flag17)
						{
							bool flag18 = text5.Length > 0;
							if (flag18)
							{
								text5 += text4;
							}
							text5 += DateTime.Now.ToShortDateString();
						}
					}
				}
				bool flag19 = !arrayList2.Contains(text5);
				if (flag19)
				{
					for (int l = 0; l < arrayList2.Count; l++)
					{
						string text10 = arrayList2[l].ToString();
						text10.Replace(" ", " ");
					}
					arrayList2.Add(text5);
					bool flag20 = code.codeValue.Length > 0;
					if (flag20)
					{
						Code code2 = code;
						code2.codeValue += text3;
					}
					Code code3 = code;
					code3.codeValue += text5;
				}
				bool flag21 = code.codeValue.Trim().Length < 1;
				if (flag21)
				{
					arrayList.Add(code);
				}
			}
			return arrayList;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00025C4C File Offset: 0x00023E4C
		public static string GetTemplateFromUser(Form parentForm, string templatesDirectory)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Please choose the template file to use:";
			openFileDialog.InitialDirectory = templatesDirectory;
			DialogResult dialogResult = openFileDialog.ShowDialog(parentForm);
			bool flag = dialogResult == DialogResult.OK;
			string result;
			if (flag)
			{
				result = openFileDialog.FileName;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00025C94 File Offset: 0x00023E94
		public static void LaunchFilledInTemplate(string tempFilename)
		{
			bool flag = File.Exists(tempFilename);
			if (flag)
			{
				Process.Start(tempFilename);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00025CB4 File Offset: 0x00023EB4
		public static ArrayList SetCodeValues(ref ArrayList codes, DataTable t, string namecol, string valcol)
		{
			ArrayList arrayList = new ArrayList();
			int num = t.Columns.IndexOf(namecol);
			int num2 = t.Columns.IndexOf(valcol);
			bool flag = num >= 0 && num2 >= 0;
			if (flag)
			{
				foreach (object obj in codes)
				{
					Code code = (Code)obj;
					string codeValue = TemplatesClass.GetCodeValue(t, num, num2, code);
					bool flag2 = codeValue.Length > 0;
					if (flag2)
					{
						bool flag3 = code.codeValue.Length > 0;
						if (flag3)
						{
							Code code2 = code;
							code2.codeValue = code2.codeValue + ", " + codeValue;
						}
						else
						{
							code.codeValue = codeValue;
						}
					}
					else
					{
						arrayList.Add(code);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00025DB0 File Offset: 0x00023FB0
		public static string GetCodeValue(DataTable t, int namec, int valc, Code code)
		{
			string text = code.codeText.Trim().ToLower();
			bool flag = t.Columns.Contains("cid");
			string strB = flag ? TemplatesClass.ExtractDigitsOnly(text) : "";
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag2 = dataRow.RowState != DataRowState.Deleted;
				if (flag2)
				{
					string text2 = dataRow[namec].ToString().Trim().ToLower();
					bool flag3 = text2.CompareTo(text) == 0;
					if (flag3)
					{
						return dataRow[valc].ToString().Trim();
					}
					bool flag4 = flag;
					if (flag4)
					{
						string text3 = dataRow["cid"].ToString();
						bool flag5 = text3.CompareTo(strB) == 0;
						if (flag5)
						{
							return dataRow[valc].ToString().Trim();
						}
					}
				}
			}
			return "";
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00025EF0 File Offset: 0x000240F0
		private static string ExtractDigitsOnly(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				bool flag = char.IsDigit(c);
				if (flag)
				{
					text += c.ToString();
				}
			}
			return text;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00025F44 File Offset: 0x00024144
		public static void ClearClockWorkTempFiles()
		{
			try
			{
				string text = Path.GetTempPath();
				text = Path.Combine(text, "TechnoPro\\ClockWork");
				bool flag = !Directory.Exists(text);
				if (flag)
				{
					Directory.CreateDirectory(text);
				}
				TemplatesClass.DeleteAllFilesIncludingFilesInSubFolders_IgnoreExceptions(text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00025FA4 File Offset: 0x000241A4
		private static void DeleteAllFilesIncludingFilesInSubFolders_IgnoreExceptions(string folder)
		{
			try
			{
				string[] directories = Directory.GetDirectories(folder);
				foreach (string folder2 in directories)
				{
					TemplatesClass.DeleteAllFilesIncludingFilesInSubFolders_IgnoreExceptions(folder2);
				}
				string[] files = Directory.GetFiles(folder);
				foreach (string path in files)
				{
					try
					{
						File.Delete(path);
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00026038 File Offset: 0x00024238
		public static string LookupValue(TemplateCodeCollection codes, string codeText)
		{
			string strB = codeText.ToLower().Trim();
			foreach (object obj in codes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				bool flag = templateCode.CodeName_lcase.CompareTo(strB) == 0;
				if (flag)
				{
					return templateCode.CodeValue.ToString();
				}
			}
			return "";
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000260C8 File Offset: 0x000242C8
		private static string WordWrap(string text, int maxLineLength)
		{
			List<string> list = new List<string>();
			int num = 0;
			char[] trimChars = new char[]
			{
				' ',
				'\r',
				'\n',
				'\t'
			};
			int num2;
			do
			{
				num2 = ((num + maxLineLength > text.Length) ? text.Length : (text.LastIndexOfAny(new char[]
				{
					' ',
					',',
					'.',
					'?',
					'!',
					':',
					';',
					'-',
					'\n',
					'\r',
					'\t'
				}, Math.Min(text.Length - 1, num + maxLineLength)) + 1));
				bool flag = num2 <= num;
				if (flag)
				{
					num2 = Math.Min(num + maxLineLength, text.Length);
				}
				list.Add(text.Substring(num, num2 - num).Trim(trimChars));
				num = num2;
			}
			while (num2 < text.Length);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				bool flag2 = i > 0;
				if (flag2)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000261D0 File Offset: 0x000243D0
		private static string FixTextForPdf(string s)
		{
			bool flag = s.Contains("\n");
			string result;
			if (flag)
			{
				string[] array = s.Split("\n".ToCharArray());
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append("\n");
					}
					bool flag3 = text.Length > 80;
					if (flag3)
					{
						stringBuilder.Append(TemplatesClass.WordWrap(text, 80));
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				result = stringBuilder.ToString();
			}
			else
			{
				result = s;
			}
			return result;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002627C File Offset: 0x0002447C
		private static string FormatCaption(string caption)
		{
			string text = caption.Trim();
			bool flag = text.LastIndexOf(":") == text.Length - 1;
			string result;
			if (flag)
			{
				bool flag2 = caption.LastIndexOf(" ") == caption.Length - 1;
				if (flag2)
				{
					result = caption;
				}
				else
				{
					caption = (result = caption + " ");
				}
			}
			else
			{
				result = caption + ": ";
			}
			return result;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000262E8 File Offset: 0x000244E8
		public static string GetDefaultDataString(Control c, object dataValue, Type dataType)
		{
			bool flag = c.Tag is DataRow;
			string text;
			if (flag)
			{
				DataRow dataRow = (DataRow)c.Tag;
				text = dataRow[3].ToString();
			}
			else
			{
				text = "";
			}
			string a = (dataValue == null) ? "" : dataValue.ToString();
			bool flag2 = dataType == typeof(bool);
			string result;
			if (flag2)
			{
				result = (((bool)dataValue) ? text : "");
			}
			else
			{
				bool flag3 = dataType == typeof(DateTime);
				if (flag3)
				{
					result = ((dataValue == null) ? "" : (TemplatesClass.FormatCaption(text) + ((DateTime)dataValue).ToString("yyyy-MM-dd")));
				}
				else
				{
					bool flag4 = dataType == typeof(string);
					if (flag4)
					{
						result = ((a == "") ? "" : (TemplatesClass.FormatCaption(text) + (string)dataValue));
					}
					else
					{
						result = ((a == "") ? "" : (TemplatesClass.FormatCaption(text) + dataValue.ToString()));
					}
				}
			}
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00026418 File Offset: 0x00024618
		public static string ExtractCids(ArrayList templateCodes, int studentPid, UnivDataAdapter da)
		{
			string text = "";
			foreach (object obj in templateCodes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				bool flag = templateCode.CodeName.Length > 0;
				if (flag)
				{
					char c = templateCode.CodeName[0];
					bool flag2 = c == '.';
					string text2;
					if (flag2)
					{
						text2 = templateCode.CodeName.Substring(1);
					}
					else
					{
						text2 = templateCode.CodeName;
					}
					bool flag3 = text2[text2.Length - 1] == '.';
					if (flag3)
					{
						text2 = text2.Substring(text2.Length - 1);
					}
					bool flag4 = text2.Length > 0;
					if (flag4)
					{
						bool flag5 = char.IsDigit(text2[0]);
						if (flag5)
						{
							templateCode.AddAlias(text2);
							bool flag6 = text.Length > 0;
							if (flag6)
							{
								text += ",";
							}
							text += text2;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00026550 File Offset: 0x00024750
		private static Control GetControl2(Control parent, string ControlName, bool ignoreLabel, DataTable dynamicScreenNonDataControlsTable)
		{
			string text = ControlName.ToUpper().Trim().Replace(":", "");
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				bool flag = control.Controls.Count > 0;
				if (flag)
				{
					Control control2 = TemplatesClass.GetControl2(control, text, ignoreLabel, dynamicScreenNonDataControlsTable);
					bool flag2 = control2 != null;
					if (flag2)
					{
						return control2;
					}
				}
				bool flag3 = control.Tag is DataRow;
				if (flag3)
				{
					DataRow dataRow = (DataRow)control.Tag;
					bool flag4 = dataRow.Table.Columns.Contains("controlcode");
					if (flag4)
					{
						int controlCode = (int)dataRow[2];
						int num = (dataRow["controlid"] == DBNull.Value) ? -1 : ((int)dataRow["controlid"]);
						bool flag5 = TemplatesClass.IsControlCodeDataHolding(dynamicScreenNonDataControlsTable, controlCode) || (!ignoreLabel && control is Label);
						if (flag5)
						{
							string text2 = dataRow[3].ToString().Trim().ToUpper();
							text2 = text2.Replace(":", "");
							bool flag6 = text2.CompareTo(text) == 0 || num.ToString().CompareTo(text) == 0;
							if (flag6)
							{
								return control;
							}
						}
					}
				}
				else
				{
					bool flag7 = control is Label && !ignoreLabel;
					if (flag7)
					{
						string text3 = control.Text.ToUpper().Trim().Replace(":", "");
						bool flag8 = text3.CompareTo(text) == 0;
						if (flag8)
						{
							return control;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00026758 File Offset: 0x00024958
		private static bool IsControlCodeDataHolding(DataTable dynamicScreenNonDataControlsTable, int ControlCode)
		{
			foreach (object obj in dynamicScreenNonDataControlsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[1];
				bool flag = num == ControlCode;
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040002F9 RID: 761
		private static string NewLine = "\r";

		// Token: 0x020000A9 RID: 169
		private struct ControlCodes
		{
			// Token: 0x040003CD RID: 973
			public const int _textBox = 1;

			// Token: 0x040003CE RID: 974
			public const int _checkBox = 2;

			// Token: 0x040003CF RID: 975
			public const int _comboBox = 3;

			// Token: 0x040003D0 RID: 976
			public const int _radioButton = 4;

			// Token: 0x040003D1 RID: 977
			public const int _label = 5;

			// Token: 0x040003D2 RID: 978
			public const int _date = 6;

			// Token: 0x040003D3 RID: 979
			public const int _time = 7;

			// Token: 0x040003D4 RID: 980
			public const int _horizontalRule = 8;

			// Token: 0x040003D5 RID: 981
			public const int _blankSpace = 9;

			// Token: 0x040003D6 RID: 982
			public const int _listView = 10;

			// Token: 0x040003D7 RID: 983
			public const int _myCheckBox = 12;

			// Token: 0x040003D8 RID: 984
			public const int _myTextBox = 11;

			// Token: 0x040003D9 RID: 985
			public const int _indent = 13;

			// Token: 0x040003DA RID: 986
			public const int _radioGroup = 14;

			// Token: 0x040003DB RID: 987
			public const int _panelStart = 30;

			// Token: 0x040003DC RID: 988
			public const int _panelClose = 31;

			// Token: 0x040003DD RID: 989
			public const int _tabControlStart = 32;

			// Token: 0x040003DE RID: 990
			public const int _tabPageStart = 33;

			// Token: 0x040003DF RID: 991
			public const int _tabPageClose = 34;

			// Token: 0x040003E0 RID: 992
			public const int _tabControlClose = 35;

			// Token: 0x040003E1 RID: 993
			public const int _columnBreak = 50;

			// Token: 0x040003E2 RID: 994
			public const int _staffComboBox = 100;

			// Token: 0x040003E3 RID: 995
			public const int _schoolYearChooser = 200;

			// Token: 0x040003E4 RID: 996
			public const int VerticalPadTextBox = 6;

			// Token: 0x040003E5 RID: 997
			public const int VerticalPadComboBox = 6;

			// Token: 0x040003E6 RID: 998
			public const int VerticalPadRadioButton = 2;

			// Token: 0x040003E7 RID: 999
			public const int VerticalPadCheckBox = 2;

			// Token: 0x040003E8 RID: 1000
			public const int VerticalPadLabel = 2;

			// Token: 0x040003E9 RID: 1001
			public const int VerticalPadDateTimePicker = 4;
		}
	}
}
