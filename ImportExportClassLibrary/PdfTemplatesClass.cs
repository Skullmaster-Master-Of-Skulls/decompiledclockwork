using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using EmailClassLibrary;
using EncryptionClassLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000005 RID: 5
	public class PdfTemplatesClass
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002B0C File Offset: 0x00001B0C
		private static string[] SplitLines(string s)
		{
			string newLine = Environment.NewLine;
			int length = newLine.Length;
			ArrayList arrayList = new ArrayList();
			int num = 0;
			for (int i = s.IndexOf(newLine); i >= 0; i = s.IndexOf(newLine, num))
			{
				arrayList.Add(s.Substring(num, i - num));
				num = i + length;
			}
			if (arrayList.Count > 0)
			{
				string[] array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
				return array;
			}
			return new string[]
			{
				""
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BB4 File Offset: 0x00001BB4
		public static string CreateTemplate(DataSet ds, EmailTemplate emailTemplate, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string tempFilename, out string html, bool generateHtml, StringDictionary blankReplacements, string[] warningIfMissingCodes, string[] errorIfMissingCodes, out ArrayList warnings, out ArrayList errors)
		{
			html = "";
			Document document = new Document();
			warnings = new ArrayList();
			errors = new ArrayList();
			try
			{
				string[] templateLines = PdfTemplatesClass.SplitLines(emailTemplate.Body);
				PdfWriter.GetInstance(document, new FileStream(tempFilename, FileMode.Create));
				document.Open();
				PdfTemplatesClass.AddLinesToTemplate(ref document, templateLines, ref ds, da, tripleDES, out html, generateHtml, blankReplacements, warningIfMissingCodes, errorIfMissingCodes, ref warnings, ref errors);
				document.Close();
				document = null;
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
			try
			{
				if (document != null)
				{
					document.Close();
				}
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C60 File Offset: 0x00001C60
		private static void AddLinesToTemplate(ref Document doc, string[] templateLines, ref DataSet ds, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, out string html, bool generateHtml, StringDictionary blankReplacements, string[] warningIfMissingCodes, string[] errorIfMissingCodes, ref ArrayList warnings, ref ArrayList errors)
		{
			string str = "#<";
			string str2 = ">#";
			Regex regex = new Regex("(?<=\\#<)(\\w|\\.|\\[|\\])+(?=>\\#)");
			html = "";
			Stack stack = new Stack();
			int i = 0;
			bool flag = false;
			PdfPTable pdfPTable = null;
			string text = null;
			int num = 0;
			int num2 = 0;
			while (i < templateLines.Length)
			{
				string text2 = templateLines[i];
				MatchCollection matchCollection = regex.Matches(text2);
				string text3 = text2;
				int j = 0;
				bool flag2 = true;
				while (j < matchCollection.Count)
				{
					Match match = matchCollection[j];
					string text4 = match.Value.ToLower();
					string text5 = str + match.Value + str2;
					string[] array = text4.Split(new char[]
					{
						'.'
					});
					string text6 = array[array.Length - 1];
					string text7 = "";
					if (text6.CompareTo("loop") == 0)
					{
						TemplateLoop templateLoop = new TemplateLoop(array[0], i, ds);
						stack.Push(templateLoop);
						j = matchCollection.Count;
						text3 = text3.Replace(text5, text7);
						flag2 = false;
					}
					else if (text6.CompareTo("endloop") == 0)
					{
						TemplateLoop templateLoop = (TemplateLoop)stack.Peek();
						if (!templateLoop.IncrementCount())
						{
							stack.Pop();
						}
						else
						{
							i = templateLoop.StartIndex;
						}
						j = matchCollection.Count;
						text3 = text3.Replace(text5, text7);
						flag2 = false;
					}
					else if (text6.IndexOf("table") == 0)
					{
						string s = text6.Substring(5).Trim();
						num2 = int.Parse(s);
						num = 1;
						pdfPTable = new PdfPTable(num2);
						pdfPTable.WidthPercentage = 95f;
						text3 = "";
						flag2 = false;
						text = "<table border='1' cellspacing='4' cellpadding='2' width='95%'>";
						j++;
					}
					else if (text6.CompareTo("endtable") == 0)
					{
						doc.Add(pdfPTable);
						pdfPTable = null;
						text3 = "";
						flag2 = false;
						text = "</table>";
						j++;
					}
					else if (text6.CompareTo("date") == 0)
					{
						text3 = text3.Replace(text5, DateTime.Now.ToString("yyyy-MM-dd"));
						j++;
					}
					else
					{
						flag2 = true;
						string contextVal;
						if (stack.Count > 0)
						{
							contextVal = ((TemplateLoop)stack.Peek()).GetCurrentValue();
						}
						else
						{
							contextVal = "";
						}
						string text8;
						string contextValColNameMatch;
						if (array.Length > 1)
						{
							text8 = array[array.Length - 2];
							int num3 = text8.IndexOf('[');
							if (num3 > 0)
							{
								contextValColNameMatch = text8.Substring(num3 + 1, text8.Length - num3 - 2);
								text8 = text8.Substring(0, num3);
							}
							else
							{
								contextValColNameMatch = "";
							}
						}
						else
						{
							text8 = "";
							contextValColNameMatch = "";
						}
						text7 = PdfTemplatesClass.ExtractCellData(ds, text8, contextValColNameMatch, contextVal, text6);
						if (text7.Trim().Length < 1)
						{
							if (warningIfMissingCodes != null && Array.IndexOf<string>(warningIfMissingCodes, text5) >= 0)
							{
								warnings.Add("Missing " + text5);
							}
							if (errorIfMissingCodes != null && Array.IndexOf<string>(errorIfMissingCodes, text5) >= 0)
							{
								errors.Add("Missing " + text5);
							}
							if (blankReplacements != null && blankReplacements.ContainsKey(text5))
							{
								text7 = blankReplacements[text5];
							}
						}
						text3 = text3.Replace(text5, text7);
						j++;
					}
				}
				if (text3.Length < 1 && flag)
				{
					flag2 = false;
				}
				if (flag2)
				{
					if (pdfPTable == null)
					{
						PdfTemplatesClass.AddLine(ref doc, text3);
					}
					else
					{
						PdfTemplatesClass.AddCell(ref doc, ref pdfPTable, text3);
						num++;
					}
					if (generateHtml)
					{
						if (pdfPTable == null)
						{
							html = html + text3 + "<br />";
						}
						else
						{
							bool flag3 = (num - 2) % num2 == 0;
							if (flag3)
							{
								html += "<tr>";
							}
							html = html + "<td>" + ((text3.Trim().Length > 0) ? text3 : "&nbsp;") + "</td>";
							if ((num - 1) % num2 == 0)
							{
								html += "</tr>";
							}
						}
					}
					flag = (text3.Length < 1);
				}
				else if (generateHtml && text != null)
				{
					html += text;
					text = null;
				}
				i++;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003094 File Offset: 0x00002094
		private static int GetTableRowCount(DataSet ds, string tableName)
		{
			DataTable dataTable = ds.Tables[tableName];
			if (dataTable != null)
			{
				return dataTable.Rows.Count;
			}
			return 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000030C0 File Offset: 0x000020C0
		private static string ExtractCellData(DataSet ds, string tableName, string contextValColNameMatch, string contextVal, string colName)
		{
			string strB = contextVal.Trim().ToLower();
			DataTable dataTable = ds.Tables[tableName];
			if (dataTable != null && dataTable.Rows.Count > 0 && dataTable.Columns.Contains(colName))
			{
				string text = "";
				int num;
				int num2;
				if (contextValColNameMatch.Length > 0 && dataTable.Columns.Contains(contextValColNameMatch))
				{
					num = -1;
					num2 = 0;
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string text2 = dataTable.Rows[i][contextValColNameMatch].ToString().Trim().ToLower();
						if (text2.CompareTo(strB) == 0)
						{
							if (num < 0)
							{
								num = i;
								num2 = dataTable.Rows.Count - 1;
							}
						}
						else if (num > -1)
						{
							num2 = i - 1;
							break;
						}
					}
				}
				else
				{
					num = 0;
					num2 = dataTable.Rows.Count - 1;
				}
				if (num > -1)
				{
					for (int j = num; j <= num2; j++)
					{
						DataRow dataRow = dataTable.Rows[j];
						string text3 = dataRow[colName].ToString();
						if (text3.Trim().Length > 0)
						{
							if (text.Length > 0)
							{
								text += ",";
							}
							text += text3;
						}
					}
				}
				return text;
			}
			return "";
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000321B File Offset: 0x0000221B
		private static void AddLine(ref Document doc, string line)
		{
			if (line.Length > 0)
			{
				doc.Add(new Paragraph(line));
				return;
			}
			doc.Add(new Paragraph("\n"));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003248 File Offset: 0x00002248
		private static void AddCell(ref Document doc, ref PdfPTable table, string line)
		{
			PdfPCell pdfPCell = new PdfPCell(new Phrase(line.Replace("\\r\\n", "")));
			pdfPCell.Padding = 5f;
			table.AddCell(pdfPCell);
		}
	}
}
