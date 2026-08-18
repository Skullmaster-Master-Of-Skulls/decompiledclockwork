using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AutoComboBox;
using AutoComboBox.MyControls;
using Excel;
using ImportExportClassLibrary.MSOffice;
using MailMerging;
using SettingsPermissions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using UnivOleDb;
using Word;

namespace ImportExportClassLibrary
{
	// Token: 0x02000035 RID: 53
	public class TemplatesClass
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000CB7C File Offset: 0x0000BB7C
		public static void PreviewWord(string fn, RichTextBox _RichTextBox)
		{
			object obj = fn;
			object value = Missing.Value;
			object obj2 = true;
			object value2 = Missing.Value;
			object value3 = Missing.Value;
			object value4 = Missing.Value;
			object value5 = Missing.Value;
			object value6 = Missing.Value;
			object value7 = Missing.Value;
			object value8 = Missing.Value;
			object value9 = Missing.Value;
			object obj3 = false;
			object obj4 = false;
			object obj5 = null;
			object obj6 = null;
			_Application application = new ApplicationClass();
			application.Visible = false;
			_Document document = application.Documents.Open(ref obj, ref value, ref obj2, ref value2, ref value3, ref value4, ref value5, ref value6, ref value7, ref value8, ref value9, ref obj3);
			if (document.ProtectionType == -1)
			{
				document.ActiveWindow.Selection.WholeStory();
				document.ActiveWindow.Selection.Copy();
				IDataObject dataObject = Clipboard.GetDataObject();
				_RichTextBox.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
			}
			else
			{
				_RichTextBox.Rtf = "";
			}
			document.ActiveWindow.Close(ref obj4, ref obj6);
			application.Quit(ref obj4, ref obj5, ref obj6);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000CC8C File Offset: 0x0000BC8C
		private static long IndexOf(Stream s, int byte1, int byte2, ref Queue bytes, bool recordBytes)
		{
			int i = s.ReadByte();
			int num = 0;
			while (i >= 0)
			{
				byte b = (byte)i;
				if ((int)b == byte1)
				{
					int num2 = s.ReadByte();
					if (num2 < 0)
					{
						if (recordBytes)
						{
							bytes.Enqueue(b);
						}
						return -1L;
					}
					byte b2 = (byte)num2;
					if ((int)b2 == byte2)
					{
						return s.Position;
					}
					if (recordBytes)
					{
						bytes.Enqueue(b);
					}
					if (recordBytes)
					{
						bytes.Enqueue(b2);
					}
					num++;
				}
				else if (recordBytes)
				{
					bytes.Enqueue(b);
				}
				i = s.ReadByte();
				num++;
			}
			return -1L;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000CD28 File Offset: 0x0000BD28
		public static string ExportToFile(string fileName, DataSet dataSet)
		{
			ArrayList arrayList = new ArrayList();
			if (File.Exists(fileName))
			{
				string text = Path.GetTempFileName();
				text = text.Replace(Path.GetExtension(text), Path.GetExtension(fileName));
				File.Copy(fileName, text, true);
				fileName = text;
				StreamReader streamReader = new StreamReader(fileName);
				Stream baseStream = streamReader.BaseStream;
				Queue queue = new Queue();
				for (long num = TemplatesClass.IndexOf(baseStream, 60, 35, ref queue, false); num >= 0L; num = TemplatesClass.IndexOf(baseStream, 60, 35, ref queue, false))
				{
					long num2 = TemplatesClass.IndexOf(baseStream, 35, 62, ref queue, true);
					if (num2 >= 0L)
					{
						string text2 = "";
						while (queue.Count > 0)
						{
							byte value = (byte)queue.Dequeue();
							text2 += Convert.ToChar(value).ToString();
						}
						text2 = text2.Trim().ToUpper();
						if (text2.Length > 0)
						{
							Code value2 = new Code(text2, num - 2L, num2 - 1L);
							arrayList.Add(value2);
						}
					}
					queue.Clear();
				}
				string text3 = Path.GetExtension(fileName).ToUpper();
				if (text3.CompareTo(".DOC") != 0)
				{
					string text4 = Path.GetTempFileName();
					text4 = text4.Replace(Path.GetExtension(text4), Path.GetExtension(fileName));
					StreamWriter streamWriter = new StreamWriter(text4, false);
					Stream baseStream2 = streamWriter.BaseStream;
					baseStream.Position = 0L;
					int num3 = 0;
					for (int i = baseStream.ReadByte(); i >= 0; i = baseStream.ReadByte())
					{
						bool flag = true;
						long num4 = baseStream.Position - 1L;
						if (num3 < arrayList.Count)
						{
							Code code = (Code)arrayList[num3];
							if (num4 == code.startIndex)
							{
								while (i >= 0 && baseStream.Position <= code.endIndex)
								{
									i = baseStream.ReadByte();
								}
								TemplatesClass.WriteString(baseStream2, "abcdefghijklmnopqrstuvwxyz1234567890");
								num3++;
								flag = false;
							}
						}
						if (flag)
						{
							byte value3 = (byte)i;
							baseStream2.WriteByte(value3);
						}
					}
					streamReader.Close();
					streamWriter.Close();
					return text4;
				}
				streamReader.Close();
				TemplatesClass.ToWordFile(fileName, text, arrayList);
			}
			return "";
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000CF44 File Offset: 0x0000BF44
		private static void WriteString(Stream s, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			if (bytes != null && bytes.Length > 0)
			{
				s.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CF74 File Offset: 0x0000BF74
		public static string GetUniqueFilename(string path, string filename)
		{
			string text = Path.Combine(path, filename);
			if (!File.Exists(text))
			{
				return text;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			string extension = Path.GetExtension(filename);
			for (int i = 2; i < 100000; i++)
			{
				string text2 = Path.Combine(path, fileNameWithoutExtension + "_" + i.ToString() + extension);
				if (!File.Exists(text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CFDC File Offset: 0x0000BFDC
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple, string password)
		{
			string text;
			TemplatesClass.ToWordFile(fileName, tempFileName, codesMultiple, password, out text);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CFF4 File Offset: 0x0000BFF4
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple, string password, out string errmsg)
		{
			TemplatesClass.ToWordFile(fileName, tempFileName, codesMultiple, false, password, out errmsg);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000D002 File Offset: 0x0000C002
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple)
		{
			TemplatesClass.ToWordFile(fileName, tempFileName, codesMultiple, false, null);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000D00E File Offset: 0x0000C00E
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile)
		{
			TemplatesClass.ToWordFile(fileName, tempFileName, codesMultiple, printWordFile, null);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000D01C File Offset: 0x0000C01C
		public static void OpenWordFile(string fileName, string password)
		{
			try
			{
				object value = Missing.Value;
				_Application application = new ApplicationClass();
				object obj;
				if (password == null)
				{
					obj = value;
				}
				else
				{
					obj = password;
				}
				object obj2 = fileName;
				_Document document = application.Documents.Open(ref obj2, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				application.Visible = true;
				document.UserControl = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000D09C File Offset: 0x0000C09C
		public static void OpenWordMailingLabels(string startDirectory, DataTable t, string labelName, bool firstLineCapitalized, bool lines2AndOnCapitalized)
		{
			TemplatesClass.OpenWordMailingLabels(startDirectory, t, labelName, "", -1, false, firstLineCapitalized, lines2AndOnCapitalized);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000D0B0 File Offset: 0x0000C0B0
		public static void OpenWordMailingLabels(string startDirectory, DataTable t, string labelName, string fontName, int fontSize, bool firstLineBolded, bool firstLineCapitalized, bool lines2AndOnCapitalized)
		{
			TemplatesClass.OpenWordMailingLabels(startDirectory, t, labelName, fontName, fontSize, firstLineBolded, firstLineCapitalized, lines2AndOnCapitalized, null);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000D0D0 File Offset: 0x0000C0D0
		public static void OpenWordMailingLabels(string startDirectory, DataTable t, string labelName, string fontName, int fontSize, bool firstLineBolded, bool firstLineCapitalized, bool lines2AndOnCapitalized, params string[] lineFormattings)
		{
			if (t.Rows.Count < 1)
			{
				return;
			}
			_Application application;
			_Document document2;
			try
			{
				object value = Missing.Value;
				object obj = false;
				Path.GetTempPath();
				string path = "MailingLabels.mdb";
				string text = Path.Combine(startDirectory, path);
				string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(text));
				File.Copy(text, tempFilename, true);
				OleDbConnection oleDbConnection = new OleDbConnection(Core.GetExcelConnectionString(tempFilename));
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
				oleDbDataAdapter.SelectCommand.CommandText = "DELETE FROM lines";
				oleDbConnection.Open();
				oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				foreach (object obj2 in t.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					oleDbDataAdapter.SelectCommand.CommandText = "INSERT INTO lines (line1,line2,line3,line4,line5,line6) VALUES (@line1,@line2,@line3,@line4,@line5,@line6)";
					oleDbDataAdapter.SelectCommand.Parameters.Clear();
					string text2 = (dataRow[0] == DBNull.Value) ? "" : ((string)dataRow[0]);
					string text3 = (dataRow[1] == DBNull.Value) ? "" : ((string)dataRow[1]);
					string text4 = (dataRow[2] == DBNull.Value) ? "" : ((string)dataRow[2]);
					string text5 = (dataRow[3] == DBNull.Value) ? "" : ((string)dataRow[3]);
					string text6 = (dataRow[4] == DBNull.Value) ? "" : ((string)dataRow[4]);
					string text7 = (dataRow[5] == DBNull.Value) ? "" : ((string)dataRow[5]);
					if (firstLineCapitalized)
					{
						text2 = text2.ToUpper();
					}
					if (lines2AndOnCapitalized)
					{
						text3 = text3.ToUpper();
						text4 = text4.ToUpper();
						text5 = text5.ToUpper();
						text6 = text6.ToUpper();
						text7 = text7.ToUpper();
					}
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line1", text2);
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line2", text3);
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line3", text4);
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line4", text5);
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line5", text6);
					oleDbDataAdapter.SelectCommand.Parameters.Add("@line6", text7);
					oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				}
				oleDbConnection.Close();
				application = null;
				application = new ApplicationClass();
				_Document document = application.Documents.Add(ref value, ref value, ref value, ref value);
				object obj3 = labelName;
				try
				{
					document2 = application.MailingLabel.CreateNewDocument(ref obj3, ref value, ref value, ref value, ref value);
				}
				catch (Exception ex)
				{
					throw new Exception("Mailing Label Type not valid! (" + labelName + ")", ex.InnerException);
				}
				document2.Select();
				document.Close(ref obj, ref value, ref value);
				MailMerge mailMerge = document2.MailMerge;
				mailMerge.MainDocumentType = 1;
				Selection selection = application.Selection;
				MailMergeFields fields = mailMerge.Fields;
				if (fontName.Length > 0)
				{
					selection.Font.Name = fontName;
				}
				if (fontSize > 0)
				{
					selection.Font.Size = (float)fontSize;
				}
				else
				{
					fontSize = Convert.ToInt32(selection.Font.Size);
				}
				if (firstLineBolded)
				{
					selection.Font.Bold = 1;
					TemplatesClass.AddMailMergeLine(0, fields, selection, lineFormattings);
					selection.Font.Bold = 0;
				}
				else
				{
					TemplatesClass.AddMailMergeLine(0, fields, selection, lineFormattings);
				}
				TemplatesClass.AddMailMergeLine(1, fields, selection, lineFormattings);
				TemplatesClass.AddMailMergeLine(2, fields, selection, lineFormattings);
				TemplatesClass.AddMailMergeLine(3, fields, selection, lineFormattings);
				if (lineFormattings == null || lineFormattings.Length < 1)
				{
					selection.Font.Size = (float)(fontSize - 2);
					selection.ParagraphFormat.Alignment = 2;
					fields.Add(selection.Range, "line5");
					selection.TypeText("\n");
					selection.Font.Size = (float)fontSize;
					selection.ParagraphFormat.Alignment = 0;
					fields.Add(selection.Range, "line6");
					selection.TypeText("\n");
				}
				else
				{
					TemplatesClass.AddMailMergeLine(4, fields, selection, lineFormattings);
					TemplatesClass.AddMailMergeLine(5, fields, selection, lineFormattings);
				}
				object wordBasic = application.WordBasic;
				wordBasic.GetType().InvokeMember("MailMergePropagateLabel", BindingFlags.InvokeMethod, null, wordBasic, null);
				object obj4 = "SELECT * FROM `lines`";
				mailMerge.OpenDataSource(tempFilename, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj4, ref value);
				mailMerge.Execute(ref value);
				application.Visible = true;
				document2.UserControl = true;
				document2.Close(ref obj, ref value, ref value);
				File.Delete(tempFilename);
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.ToString());
			}
			document2 = null;
			application = null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000D634 File Offset: 0x0000C634
		private static void AddMailMergeLine(int lineIndex, MailMergeFields fields, Selection sel, string[] lineFormattings)
		{
			if (lineFormattings != null && lineIndex < lineFormattings.Length)
			{
				string text = (lineFormattings[lineIndex] == null) ? string.Empty : lineFormattings[lineIndex];
				string text2 = Regex.Replace(text, "\\D", string.Empty).Replace(".", "");
				int num;
				if (!string.IsNullOrEmpty(text2))
				{
					int.TryParse(text2, out num);
				}
				else
				{
					num = 0;
				}
				bool flag = text.Contains("b");
				bool flag2 = text.Contains("u");
				bool flag3 = text.Contains("i");
				bool flag4 = text.Contains(".");
				WdParagraphAlignment alignment = 0;
				if (text.Contains("l"))
				{
					alignment = 0;
				}
				else if (text.Contains("r"))
				{
					alignment = 2;
				}
				else if (text.Contains("c"))
				{
					alignment = 1;
				}
				if (num > 0)
				{
					sel.Font.Size = (float)num;
				}
				else
				{
					num = Convert.ToInt32(sel.Font.Size);
				}
				sel.ParagraphFormat.Alignment = alignment;
				if (flag4)
				{
					sel.ParagraphFormat.LineSpacing = (float)((double)sel.ParagraphFormat.LineSpacing * 1.5);
				}
				if (flag)
				{
					sel.Font.Bold = 1;
				}
				if (flag3)
				{
					sel.Font.Italic = 1;
				}
				if (flag2)
				{
					sel.Font.Underline = 1;
				}
				fields.Add(sel.Range, string.Format("line{0}", (lineIndex + 1).ToString()));
				sel.TypeText("\n");
				if (flag)
				{
					sel.Font.Bold = 0;
				}
				if (flag3)
				{
					sel.Font.Italic = 0;
				}
				if (flag2)
				{
					sel.Font.Underline = 0;
				}
				if (flag4)
				{
					sel.ParagraphFormat.LineSpacing = (float)((double)sel.ParagraphFormat.LineSpacing / 1.5);
					return;
				}
			}
			else
			{
				fields.Add(sel.Range, string.Format("line{0}", (lineIndex + 1).ToString()));
				sel.TypeText("\n");
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D838 File Offset: 0x0000C838
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile, string password)
		{
			string text;
			TemplatesClass.ToWordFile(fileName, tempFileName, codesMultiple, printWordFile, password, out text);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000D854 File Offset: 0x0000C854
		public static void ToWordFile(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile, string password, out string errmsg)
		{
			_Application application;
			_Document document2;
			try
			{
				object value = Missing.Value;
				object obj = "\\endofdoc";
				object obj2 = 2;
				object obj3 = 7;
				object obj4 = false;
				object obj5 = true;
				application = null;
				application = new ApplicationClass();
				application.Visible = false;
				object obj6 = fileName;
				_Document document = application.Documents.Open(ref obj6, ref value, ref obj5, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				float leftMargin = document.PageSetup.LeftMargin;
				float topMargin = document.PageSetup.TopMargin;
				float bottomMargin = document.PageSetup.BottomMargin;
				float rightMargin = document.PageSetup.RightMargin;
				document.Close(ref value, ref value, ref value);
				document2 = application.Documents.Add(ref value, ref value, ref value, ref value);
				try
				{
					document2.PageSetup.TopMargin = topMargin;
					document2.PageSetup.BottomMargin = bottomMargin;
					document2.PageSetup.LeftMargin = leftMargin;
					document2.PageSetup.RightMargin = rightMargin;
				}
				catch
				{
				}
				bool flag = true;
				int count = codesMultiple.Count;
				for (int i = 0; i < codesMultiple.Count; i++)
				{
					if (flag)
					{
						flag = false;
						document2.Bookmarks.Item(ref obj).Range.InsertFile(fileName, ref value, ref obj4, ref value, ref value);
					}
					else
					{
						document2.Bookmarks.Item(ref obj).Range.InsertBreak(ref obj3);
						object range = document2.Bookmarks.Item(ref obj).Range;
						document2.Bookmarks.Add("bookmark" + i.ToString(), ref range);
						document2.Bookmarks.Item(ref obj).Range.InsertFile(fileName, ref value, ref obj4, ref value, ref value);
						object obj7 = "bookmark" + i.ToString();
						document2.Bookmarks.Item(ref obj7).Range.Select();
					}
					ArrayList arrayList = (ArrayList)codesMultiple[i];
					foreach (object obj8 in arrayList)
					{
						Code code = (Code)obj8;
						string text = "#<" + code.codeText + ">#";
						string text2 = code.codeValue;
						int num = 250 - text.Length;
						int num2 = 0;
						if (text2.Length < 1)
						{
							object[] args = new object[]
							{
								text,
								false,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								"",
								obj2,
								value,
								value,
								value,
								value
							};
							object find = application.Selection.Find;
							find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
						}
						else
						{
							while (text2.Length > 0 && num2++ <= 250)
							{
								int num3 = text2.Length;
								if (num3 > num)
								{
									num3 = num;
								}
								string text3 = text2.Substring(0, num3);
								if (text3.Length < text2.Length)
								{
									text3 += text;
									text2 = text2.Substring(num3);
								}
								else
								{
									text2 = "";
								}
								object[] args = new object[]
								{
									text,
									false,
									value,
									value,
									value,
									value,
									value,
									value,
									value,
									text3,
									obj2,
									value,
									value,
									value,
									value
								};
								object find = application.Selection.Find;
								find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
							}
						}
					}
				}
				object obj9 = tempFileName;
				document2.SaveAs(ref obj9, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				if (password != null)
				{
					document2.Password = password;
					document2.Save();
				}
				if (printWordFile)
				{
					object obj10 = 0;
					object obj11 = 0;
					object obj12 = "1";
					object obj13 = "1";
					object obj14 = 0;
					document2.PrintOut(ref obj5, ref obj4, ref obj10, ref value, ref value, ref value, ref obj11, ref obj12, ref obj13, ref obj14, ref obj4, ref obj5, ref value, ref obj4, ref value, ref value, ref value, ref value);
				}
				Type.GetTypeFromProgID("Word.Application");
				object[] array = new object[]
				{
					true
				};
				application.Quit(ref obj5, ref value, ref value);
				errmsg = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				errmsg = ex.ToString();
			}
			document2 = null;
			application = null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000DD80 File Offset: 0x0000CD80
		public static void ToWordFileKeepItOpen(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile, string password)
		{
			string text;
			TemplatesClass.ToWordFileKeepItOpen(fileName, tempFileName, codesMultiple, printWordFile, password, out text);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000DD9A File Offset: 0x0000CD9A
		public static void ToWordFileKeepItOpen(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile, string password, out string errmsg)
		{
			TemplatesClass.ToWordFileKeepItOpen(fileName, tempFileName, codesMultiple, printWordFile, password, out errmsg, false);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000DDAC File Offset: 0x0000CDAC
		public static void CloseWordDoc(object wordDoc)
		{
			if (wordDoc != null)
			{
				_Document document = (_Document)wordDoc;
				object value = Missing.Value;
				object obj = false;
				document.Close(ref obj, ref value, ref value);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000DDE0 File Offset: 0x0000CDE0
		public static object[] ToWordFileKeepItOpen(string fileName, string tempFileName, ArrayList codesMultiple, bool printWordFile, string password, out string errmsg, bool hideTheWordFile)
		{
			_Application application;
			_Document document2;
			try
			{
				object value = Missing.Value;
				object obj = "\\endofdoc";
				object obj2 = 2;
				object obj3 = 7;
				object obj4 = false;
				object obj5 = true;
				application = null;
				application = new ApplicationClass();
				application.Visible = true;
				if (hideTheWordFile)
				{
					application.WindowState = 2;
				}
				object obj6 = fileName;
				object obj7 = value;
				_Document document = application.Documents.Open(ref obj6, ref value, ref obj5, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj7);
				float leftMargin = document.PageSetup.LeftMargin;
				float topMargin = document.PageSetup.TopMargin;
				float bottomMargin = document.PageSetup.BottomMargin;
				float rightMargin = document.PageSetup.RightMargin;
				document.Close(ref obj4, ref value, ref value);
				document2 = application.Documents.Add(ref value, ref value, ref value, ref obj7);
				try
				{
					document2.PageSetup.TopMargin = topMargin;
					document2.PageSetup.BottomMargin = bottomMargin;
					document2.PageSetup.LeftMargin = leftMargin;
					document2.PageSetup.RightMargin = rightMargin;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				bool flag = true;
				int count = codesMultiple.Count;
				for (int i = 0; i < codesMultiple.Count; i++)
				{
					if (flag)
					{
						flag = false;
						document2.Bookmarks.Item(ref obj).Range.InsertFile(fileName, ref value, ref obj4, ref value, ref value);
					}
					else
					{
						document2.Bookmarks.Item(ref obj).Range.InsertBreak(ref obj3);
						object range = document2.Bookmarks.Item(ref obj).Range;
						document2.Bookmarks.Add("bookmark" + i.ToString(), ref range);
						document2.Bookmarks.Item(ref obj).Range.InsertFile(fileName, ref value, ref obj4, ref value, ref value);
						object obj8 = "bookmark" + i.ToString();
						document2.Bookmarks.Item(ref obj8).Range.Select();
					}
					ArrayList arrayList = (ArrayList)codesMultiple[i];
					foreach (object obj9 in arrayList)
					{
						Code code = (Code)obj9;
						string text = "#<" + code.codeText + ">#";
						string text2 = code.codeValue;
						int num = 250 - text.Length;
						int num2 = 0;
						if (text2.Length < 1)
						{
							object[] args = new object[]
							{
								text,
								false,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								"",
								obj2,
								value,
								value,
								value,
								value
							};
							object find = application.Selection.Find;
							find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
						}
						else
						{
							while (text2.Length > 0 && num2++ <= 250)
							{
								int num3 = text2.Length;
								if (num3 > num)
								{
									num3 = num;
								}
								string text3 = text2.Substring(0, num3);
								if (text3.Length < text2.Length)
								{
									text3 += text;
									text2 = text2.Substring(num3);
								}
								else
								{
									text2 = "";
								}
								object[] args = new object[]
								{
									text,
									false,
									value,
									value,
									value,
									value,
									value,
									value,
									value,
									text3,
									obj2,
									value,
									value,
									value,
									value
								};
								object find = application.Selection.Find;
								find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
							}
						}
					}
				}
				TemplatesClass.FixWordCheckboxes(application, document2, "~x~", "R", "WingDings 2");
				TemplatesClass.FixWordCheckboxes(application, document2, "~p~", "£", "WingDings 2");
				TemplatesClass.CheckForBookmarkDefinitions(document2, codesMultiple, value);
				application.ScreenUpdating = true;
				object obj10 = tempFileName;
				application.Selection.SetRange(0, 0);
				document2.SaveAs(ref obj10, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				if (password != null)
				{
					document2.Password = password;
					document2.Save();
				}
				if (printWordFile)
				{
					object obj11 = 0;
					object obj12 = 0;
					object obj13 = "1";
					object obj14 = "1";
					object obj15 = 0;
					document2.PrintOut(ref obj5, ref obj4, ref obj11, ref value, ref value, ref value, ref obj12, ref obj13, ref obj14, ref obj15, ref obj4, ref obj5, ref value, ref obj4, ref value, ref value, ref value, ref value);
				}
				errmsg = null;
				document2.Activate();
				return new object[]
				{
					application,
					document2
				};
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
				errmsg = ex2.ToString();
				application = null;
				document2 = null;
			}
			return new object[]
			{
				application,
				document2
			};
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000E378 File Offset: 0x0000D378
		private static void CheckForBookmarkDefinitions2(_Document wordDoc, TemplateCodeGroupCollection codesMultiple, object oMissing)
		{
			foreach (object obj in wordDoc.Bookmarks)
			{
				Bookmark bookmark = (Bookmark)obj;
				string text = bookmark.Name.ToLower().Trim();
				if (text.IndexOf("remove_") == 0)
				{
					string strB = text.Substring(7);
					bool flag = false;
					foreach (object obj2 in codesMultiple)
					{
						TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj2;
						if (flag)
						{
							break;
						}
						foreach (object obj3 in templateCodeGroup.SubCodes)
						{
							TemplateCode templateCode = (TemplateCode)obj3;
							if (templateCode.CodeName_lcase.CompareTo(strB) == 0 && ((templateCode.CodeValue != null && templateCode.CodeValue is bool && !(bool)templateCode.CodeValue) || templateCode.CodeValue.ToString().Trim().Length < 1))
							{
								Range range = bookmark.Range;
								range.Delete(ref oMissing, ref oMissing);
								flag = true;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000E528 File Offset: 0x0000D528
		private static void CheckForBookmarkDefinitions(_Document wordDoc, ArrayList codesMultiple, object oMissing)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000E52C File Offset: 0x0000D52C
		private static void FixWordCheckboxes(_Application wordApp, _Document wordDoc, string lookFor, string replaceWith, string fontName)
		{
			object value = Missing.Value;
			object obj = 0;
			for (;;)
			{
				wordApp.Selection.SetRange(0, 0);
				object[] args = new object[]
				{
					lookFor,
					false,
					value,
					value,
					value,
					value,
					value,
					value,
					value,
					value,
					obj,
					value,
					value,
					value,
					value
				};
				object find = wordApp.Selection.Find;
				find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
				if (wordApp.Selection.Text.ToLower().CompareTo(lookFor) != 0)
				{
					break;
				}
				wordApp.Selection.Font.Name = fontName;
				wordApp.Selection.Text = replaceWith;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000E615 File Offset: 0x0000D615
		public static ArrayList GetCodes(string fileName)
		{
			return TemplatesClass.GetCodes(fileName, '#', '<', '>', '#');
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000E628 File Offset: 0x0000D628
		public static ArrayList GetCodesFromWord(string fileName, char char1a, char char1b, char char2a, char char2b)
		{
			string text;
			return TemplatesClass.GetCodesFromWord(fileName, char1a, char1b, char2a, char2b, out text);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000E644 File Offset: 0x0000D644
		public static ArrayList GetCodesFromWord4(string fileName, char char1a, char char1b, char char2a, char char2b, out string errmsg)
		{
			ArrayList result;
			try
			{
				List<MailMergeCode> list = MailMergeWord.ExtractMailMergeCodes(fileName);
				ArrayList arrayList = new ArrayList();
				foreach (MailMergeCode mailMergeCode in list)
				{
					Code value = new Code(mailMergeCode.Name.ToUpper(), 0L, 0L, mailMergeCode.Name);
					arrayList.Add(value);
				}
				errmsg = null;
				result = arrayList;
			}
			catch (Exception ex)
			{
				errmsg = ex.ToString();
				result = null;
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000E6E4 File Offset: 0x0000D6E4
		public static ArrayList GetCodesFromWord(string fileName, char char1a, char char1b, char char2a, char char2b, out string errmsg)
		{
			string arg = "A";
			int num = 0;
			ArrayList result;
			try
			{
				string text = char1a + char1b;
				string text2 = char2a + char2b;
				ArrayList arrayList = new ArrayList();
				object value = Missing.Value;
				object obj = "\\endofdoc";
				object obj2 = 2;
				object obj3 = 0;
				object obj4 = false;
				_Application application = new ApplicationClass();
				arg = "B:wordApp=" + ((application == null) ? "NULL" : "notnull");
				application.Visible = false;
				_Document document = application.Documents.Add(ref value, ref value, ref value, ref value);
				arg = "C:wordDoc=" + ((document == null) ? "NULL" : "notnull");
				document.Bookmarks.Item(ref obj).Range.InsertFile(fileName, ref value, ref obj4, ref value, ref value);
				arg = "D";
				for (;;)
				{
					object[] args = new object[]
					{
						text,
						false,
						value,
						value,
						value,
						value,
						value,
						value,
						value,
						value,
						obj3,
						value,
						value,
						value,
						value
					};
					object find = application.Selection.Find;
					arg = "E:myFind=" + ((find == null) ? "NULL" : "notnull");
					find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
					arg = "F";
					string text3 = application.Selection.Text.Trim();
					arg = "G:s=" + ((text3 == null) ? "NULL" : text3);
					if (text3.CompareTo(text) != 0)
					{
						break;
					}
					application.Selection.ExtendMode = true;
					arg = "H";
					args = new object[]
					{
						text2,
						false,
						value,
						value,
						value,
						value,
						value,
						value,
						value,
						value,
						obj3,
						value,
						value,
						value,
						value
					};
					arg = "I";
					find = application.Selection.Find;
					arg = "J";
					find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
					arg = "K";
					text3 = application.Selection.Text.Trim();
					arg = "L";
					application.Selection.ExtendMode = false;
					arg = "M";
					args = new object[]
					{
						text3,
						false,
						value,
						value,
						value,
						value,
						value,
						value,
						value,
						"",
						obj2,
						value,
						value,
						value,
						value
					};
					arg = "N";
					find = application.Selection.Find;
					arg = "O";
					find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
					arg = "P";
					string text4 = text3.Replace(text, "").Replace(text2, "").Trim();
					arg = "Q";
					Code value2 = new Code(text4.ToUpper(), 0L, 0L, text4);
					arg = "R";
					arrayList.Add(value2);
					arg = "S";
					num++;
				}
				arg = "T:wordDoc=" + ((document == null) ? "NULL" : "notnull");
				document.Close(ref obj4, ref value, ref value);
				arg = "U:wordApp=" + ((application == null) ? "NULL" : "notnull");
				application.Quit(ref obj4, ref value, ref value);
				errmsg = null;
				result = arrayList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("TemplatesClass:GetCodesFromWord:i={0}:loc={1}:err={2}", num.ToString(), arg, ex.ToString()));
				errmsg = null;
				result = new ArrayList();
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000EB64 File Offset: 0x0000DB64
		public static ArrayList GetCodes(string fileName, char char1a, char char1b, char char2a, char char2b)
		{
			string text;
			return TemplatesClass.GetCodes(fileName, char1a, char1b, char2a, char2b, out text);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000EB80 File Offset: 0x0000DB80
		public static ArrayList GetCodes4(string fileName, char char1a, char char1b, char char2a, char char2b, out string errmsg)
		{
			string text = Path.GetExtension(fileName).ToLower();
			if (text.Equals(".doc") || text.Equals(".docx"))
			{
				return TemplatesClass.GetCodesFromWord4(fileName, char1a, char1b, char2a, char2b, out errmsg);
			}
			return TemplatesClass.GetCodes(fileName, char1a, char1b, char2a, char2b, out errmsg);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000EBD0 File Offset: 0x0000DBD0
		public static ArrayList GetCodes(string fileName, char char1a, char char1b, char char2a, char char2b, out string errmsg)
		{
			ArrayList result;
			try
			{
				ArrayList arrayList = new ArrayList();
				string text = Path.GetExtension(fileName).ToLower();
				if (text.Equals(".doc") || text.Equals(".docx"))
				{
					result = TemplatesClass.GetCodesFromWord(fileName, char1a, char1b, char2a, char2b, out errmsg);
				}
				else if (text.Equals(".rtf"))
				{
					string input = File.ReadAllText(fileName);
					Regex regex = new Regex("#<[^#>]*>#");
					MatchCollection matchCollection = regex.Matches(input);
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						if (match.Value.Length > 4)
						{
							string text2 = match.Value.Substring(2, match.Value.Length - 4);
							Code value = new Code(text2.ToUpper(), 0L, 1L, text2);
							arrayList.Add(value);
						}
					}
					errmsg = null;
					result = arrayList;
				}
				else if (text.Equals(".xml"))
				{
					string input = File.ReadAllText(fileName).Replace("#~", "#<").Replace("~#", ">#");
					Regex regex = new Regex("#<[^#>]*>#");
					MatchCollection matchCollection = regex.Matches(input);
					foreach (object obj2 in matchCollection)
					{
						Match match2 = (Match)obj2;
						if (match2.Value.Length > 4)
						{
							string text3 = match2.Value.Substring(2, match2.Value.Length - 4);
							Code value2 = new Code(text3.ToUpper(), 0L, 1L, text3);
							arrayList.Add(value2);
						}
					}
					errmsg = null;
					result = arrayList;
				}
				else
				{
					int @byte = Convert.ToInt32(char1a);
					int byte2 = Convert.ToInt32(char1b);
					int byte3 = Convert.ToInt32(char2a);
					int byte4 = Convert.ToInt32(char2b);
					StreamReader streamReader = new StreamReader(fileName);
					Stream baseStream = streamReader.BaseStream;
					Queue queue = new Queue();
					for (long num = TemplatesClass.IndexOf(baseStream, @byte, byte2, ref queue, false); num >= 0L; num = TemplatesClass.IndexOf(baseStream, @byte, byte2, ref queue, false))
					{
						long num2 = TemplatesClass.IndexOf(baseStream, byte3, byte4, ref queue, true);
						if (num2 >= 0L)
						{
							string text4 = "";
							while (queue.Count > 0)
							{
								byte value3 = (byte)queue.Dequeue();
								text4 += Convert.ToChar(value3).ToString();
							}
							text4 = text4.Trim();
							if (text4.Length > 0)
							{
								Code value4 = new Code(text4.ToUpper(), num - 2L, num2 - 1L, text4);
								arrayList.Add(value4);
							}
						}
						queue.Clear();
					}
					baseStream.Close();
					errmsg = null;
					result = arrayList;
				}
			}
			catch (Exception ex)
			{
				errmsg = ex.ToString();
				result = new ArrayList();
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000EF18 File Offset: 0x0000DF18
		public static string WriteCodes(string fileName, string newFileName, ArrayList codesMultiple)
		{
			return TemplatesClass.WriteCodes(fileName, newFileName, codesMultiple, null);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000EF24 File Offset: 0x0000DF24
		public static string WriteCodes(string fileName, string newFileName, ArrayList codesMultiple, string password)
		{
			string text = Path.GetExtension(fileName).ToUpper();
			if (text.CompareTo(".DOC") == 0)
			{
				string result;
				TemplatesClass.ToWordFile(fileName, newFileName, codesMultiple, password, out result);
				return result;
			}
			if (text.Equals(".XML"))
			{
				string str = File.ReadAllText(fileName);
				string text2 = "";
				foreach (object obj in codesMultiple)
				{
					ArrayList arrayList = (ArrayList)obj;
					text2 += str;
					for (int i = 0; i < arrayList.Count; i++)
					{
						Code code = (Code)arrayList[i];
						text2 = text2.Replace(string.Format("#~{0}~#", code.OriginalCodeText), code.codeValue);
					}
				}
				File.WriteAllText(newFileName, text2);
				return null;
			}
			string result2;
			try
			{
				StreamWriter streamWriter = new StreamWriter(newFileName, false);
				Stream baseStream = streamWriter.BaseStream;
				StreamReader streamReader = new StreamReader(fileName);
				Stream baseStream2 = streamReader.BaseStream;
				bool flag = true;
				foreach (object obj2 in codesMultiple)
				{
					ArrayList arrayList2 = (ArrayList)obj2;
					if (flag)
					{
						flag = false;
					}
					else
					{
						TemplatesClass.WriteString(baseStream, Environment.NewLine + Environment.NewLine);
					}
					baseStream2.Position = 0L;
					int num = 0;
					for (int j = baseStream2.ReadByte(); j >= 0; j = baseStream2.ReadByte())
					{
						bool flag2 = true;
						long num2 = baseStream2.Position - 1L;
						if (num < arrayList2.Count)
						{
							Code code2 = (Code)arrayList2[num];
							if (num2 == code2.startIndex)
							{
								while (j >= 0 && baseStream2.Position <= code2.endIndex)
								{
									j = baseStream2.ReadByte();
								}
								TemplatesClass.WriteString(baseStream, code2.codeValue);
								num++;
								flag2 = false;
							}
						}
						if (flag2)
						{
							byte value = (byte)j;
							baseStream.WriteByte(value);
						}
					}
					baseStream2.Close();
				}
				streamWriter.Close();
				result2 = null;
			}
			catch (Exception ex)
			{
				string text3 = ex.ToString();
				result2 = text3;
			}
			return result2;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F1A0 File Offset: 0x0000E1A0
		private static string MergeRtf(RichTextBox rtb, string rtf1, string rtf2)
		{
			rtb.Rtf = rtf1;
			rtb.SelectionStart = rtb.Text.Length;
			rtb.SelectedRtf = "{\\rtf1 \\par \\page}";
			rtb.SelectionStart = rtb.Text.Length;
			rtb.SelectedRtf = rtf2;
			return rtb.Rtf;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F1F0 File Offset: 0x0000E1F0
		private static string MergeRtf(string rtf1, string rtf2)
		{
			RichTextBox richTextBox = new RichTextBox();
			string result = TemplatesClass.MergeRtf(richTextBox, rtf1, rtf2);
			richTextBox.Dispose();
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F218 File Offset: 0x0000E218
		public static string WriteCodesWithNonWordSearchAndReplace(string fileName, string newFileName, ArrayList codesMultiple, string password)
		{
			if (Path.GetExtension(fileName).ToUpper().CompareTo(".DOC") == 0)
			{
				string result;
				TemplatesClass.ToWordFile(fileName, newFileName, codesMultiple, password, out result);
				return result;
			}
			string result2;
			try
			{
				string text = File.ReadAllText(fileName);
				string text2 = Path.GetExtension(fileName).ToLower();
				int num = 0;
				string text3 = "";
				RichTextBox richTextBox = new RichTextBox();
				foreach (object obj in codesMultiple)
				{
					ArrayList arrayList = (ArrayList)obj;
					string text4 = text;
					foreach (object obj2 in arrayList)
					{
						Code code = (Code)obj2;
						text4 = text4.Replace("#<" + code.OriginalCodeText + ">#", code.codeValue);
					}
					if (num == 0)
					{
						text3 = text4;
					}
					else if (text2.Equals(".rtf"))
					{
						text3 = TemplatesClass.MergeRtf(richTextBox, text3, text4);
					}
					else
					{
						text3 += text4;
					}
					num++;
				}
				richTextBox.Dispose();
				richTextBox = null;
				File.WriteAllText(newFileName, text3);
				result2 = null;
			}
			catch (Exception ex)
			{
				string text5 = ex.ToString();
				result2 = text5;
			}
			return result2;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F3B8 File Offset: 0x0000E3B8
		public static string OpenCsvInExcelAndSaveAsCsv(string csvFilename, string outFilename)
		{
			object value = Missing.Value;
			try
			{
				Workbooks workbooks = new ApplicationClass
				{
					Visible = false,
					UserControl = false
				}.Workbooks;
				_Workbook workbook = workbooks.Open(csvFilename, value, value, value, value, value, value, value, value, value, value, value, value);
				_Worksheet worksheet = (_Worksheet)workbook.ActiveSheet;
				object obj = 6;
				object value2 = Missing.Value;
				object obj2 = false;
				worksheet.SaveAs(outFilename, obj, value2, value2, value2, value2, value2, value2, value2);
				workbook.Close(obj2, value2, value2);
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
			return null;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F474 File Offset: 0x0000E474
		public static string OpenExcel(string tempFilename)
		{
			object value = Missing.Value;
			try
			{
				Application application = new ApplicationClass();
				Workbooks workbooks = application.Workbooks;
				_Workbook workbook = workbooks.Open(tempFilename, value, value, value, value, value, value, value, value, value, value, value, value);
				_Worksheet worksheet = (_Worksheet)workbook.ActiveSheet;
				Range range = worksheet.get_Range("A1", "B1");
				range = range.EntireRow;
				range.EntireColumn.AutoFit();
				range.Font.Bold = true;
				application.Visible = true;
				application.UserControl = true;
			}
			catch (Exception ex)
			{
				string text = "Error: ";
				text += ex.Message;
				text += " Line: ";
				text += ex.Source;
				try
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.FileName = Path.GetFileName(tempFilename);
					DialogResult dialogResult = saveFileDialog.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						File.Copy(tempFilename, saveFileDialog.FileName);
					}
				}
				catch
				{
				}
				return text;
			}
			return null;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F5A0 File Offset: 0x0000E5A0
		private static string AllowUserToChooseColumns(ref DataView dv, bool askUserToFilterColumns, bool showTableName)
		{
			string text;
			return TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, showTableName, null, out text);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F5B8 File Offset: 0x0000E5B8
		public static string ChangeFileExtension(string filename, string newExtensionWithDot)
		{
			string text = filename;
			string extension = Path.GetExtension(filename);
			if (extension.Length > 0)
			{
				text = text.Substring(0, text.Length - extension.Length);
			}
			return text + newExtensionWithDot;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F5F8 File Offset: 0x0000E5F8
		private static string AllowUserToChooseColumns(ref DataView dv, bool askUserToFilterColumns, bool showTableName, string filterColumnsChecked, out string newFilterColumnsChecked)
		{
			if (!askUserToFilterColumns)
			{
				newFilterColumnsChecked = filterColumnsChecked;
				return "";
			}
			FilterColumns filterColumns = new FilterColumns(dv, showTableName);
			string[] filterColumnsChecked2;
			if (filterColumnsChecked == null)
			{
				filterColumnsChecked2 = null;
			}
			else
			{
				filterColumnsChecked2 = filterColumnsChecked.Split(new char[]
				{
					','
				});
			}
			filterColumns.filterColumnsChecked = filterColumnsChecked2;
			filterColumns.ShowDialog();
			if (filterColumns.DialogResult != DialogResult.OK)
			{
				newFilterColumnsChecked = filterColumnsChecked;
				return null;
			}
			string text = "";
			foreach (object obj in filterColumns.listView1.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (!listViewItem.Checked)
				{
					DataColumn dataColumn = (DataColumn)listViewItem.Tag;
					dataColumn.ColumnMapping = MappingType.Hidden;
					if (text.Length > 0)
					{
						text += ",";
					}
					text += dataColumn.ColumnName.ToLower().Trim();
				}
			}
			newFilterColumnsChecked = text;
			return filterColumns.txt_tableName.Text.Trim();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F710 File Offset: 0x0000E710
		public static void ExportToExcel(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			string text;
			TemplatesClass.ExportToExcel(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns, null, out text);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F730 File Offset: 0x0000E730
		public static void ExportToExcel(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns, string filterColumnsChecked, out string newFilterColumnsChecked)
		{
			string text;
			TemplatesClass.ExportToExcel(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns, filterColumnsChecked, out text);
			newFilterColumnsChecked = text;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F754 File Offset: 0x0000E754
		public static void ExportToExcel(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			string text;
			TemplatesClass.ExportToExcel(dv, tempFilename, startDirectory, askUserToFilterColumns, null, out text);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000F76D File Offset: 0x0000E76D
		public static string GetConnectionStringOleDb_Excel(string filename)
		{
			return Core.GetExcelConnectionString(filename);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F778 File Offset: 0x0000E778
		public static string FixColumnName(ref ArrayList usedColNames, string colName)
		{
			string text = "";
			foreach (char c in colName)
			{
				if (char.IsLetter(c))
				{
					text += c;
				}
				else if (text.Length > 0 && (char.IsDigit(c) || c == '_'))
				{
					text += c;
				}
			}
			if (text.Length < 1)
			{
				text = "column";
			}
			if (usedColNames.Contains(text))
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

		// Token: 0x060001AF RID: 431 RVA: 0x0000F824 File Offset: 0x0000E824
		public static string FixColumnNameOld(ref ArrayList usedColNames, string cName)
		{
			string text = cName.Replace("/", "");
			text = text.Replace(".", "");
			return text.Replace(",", "");
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F868 File Offset: 0x0000E868
		public static void ExportToExcel(DataView dv, string tempFilename0, string startDirectory, bool askUserToFilterColumns, string filterColumnsChecked, out string newFilterColumnsChecked)
		{
			bool flag = Path.GetExtension(tempFilename0).ToLower().CompareTo(".xlsold") == 0;
			string text;
			if (flag)
			{
				text = tempFilename0.Substring(0, tempFilename0.Length - 3);
			}
			else
			{
				text = tempFilename0;
			}
			string text2;
			if (TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false, filterColumnsChecked, out text2) == null)
			{
				newFilterColumnsChecked = filterColumnsChecked;
				return;
			}
			newFilterColumnsChecked = text2;
			DataTable table = dv.Table;
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.Boolean");
			Type type3 = Type.GetType("System.DateTime");
			File.Copy(Path.Combine(startDirectory, "BlankExcel.xls"), text, true);
			TemplatesClass.GetConnectionStringOleDb_Excel(text);
			OleDbConnection oleDbConnection = new OleDbConnection(Core.GetExcelConnectionString(text));
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			string text3 = "[sheet1$]";
			int num = 0;
			string text4 = "";
			string text5 = "";
			string[] array = new string[table.Columns.Count];
			oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + text3 + " (";
			oleDbDataAdapter.SelectCommand.Parameters.Clear();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text6 = flag ? TemplatesClass.FixColumnNameOld(ref arrayList, dataColumn.ColumnName) : TemplatesClass.FixColumnName(ref arrayList, dataColumn.ColumnName);
					int num2 = 60 - i.ToString().Length;
					if (text6.Length >= num2)
					{
						text6 = text6.Substring(0, num2) + i.ToString();
					}
					string text7 = "@col" + i.ToString();
					if (num++ > 0)
					{
						OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
						selectCommand.CommandText += ",";
						text4 += ",";
						text5 += ",";
					}
					text6 = "[" + text6 + "]";
					OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
					selectCommand2.CommandText += text6;
					text4 += text7;
					text5 += text6;
					array[i] = text7;
					Type dataType = table.Columns[i].DataType;
					if (dataType == type)
					{
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += " NUMBER";
					}
					else if (dataType == type2)
					{
						OleDbCommand selectCommand4 = oleDbDataAdapter.SelectCommand;
						selectCommand4.CommandText += " BIT";
					}
					else if (dataType == type3)
					{
						OleDbCommand selectCommand5 = oleDbDataAdapter.SelectCommand;
						selectCommand5.CommandText += " DATETIME";
					}
					else
					{
						OleDbCommand selectCommand6 = oleDbDataAdapter.SelectCommand;
						selectCommand6.CommandText += " TEXT";
					}
				}
			}
			OleDbCommand selectCommand7 = oleDbDataAdapter.SelectCommand;
			selectCommand7.CommandText += ")";
			try
			{
				oleDbDataAdapter.Fill(new DataTable());
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(message);
			}
			int num3 = 0;
			oleDbConnection.Open();
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				num3++;
				DataRow row = dataRowView.Row;
				oleDbDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
				{
					"INSERT INTO ",
					text3,
					" (",
					text5,
					") VALUES (",
					text4,
					")"
				});
				oleDbDataAdapter.SelectCommand.Parameters.Clear();
				for (int j = 0; j < table.Columns.Count; j++)
				{
					DataColumn dataColumn2 = table.Columns[j];
					if (dataColumn2.ColumnMapping != MappingType.Hidden)
					{
						object obj2 = row[j];
						if (obj2 is DateTime)
						{
							DateTime dateTime = (DateTime)obj2;
							obj2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
						}
						else if (obj2 is string)
						{
							string text8 = (string)obj2;
							text8 = text8.Replace("\r\n", "\r");
							obj2 = text8;
						}
						oleDbDataAdapter.SelectCommand.Parameters.Add(array[j], obj2);
					}
				}
				try
				{
					oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				}
				catch (Exception ex2)
				{
					string message2 = ex2.Message;
					if (!arrayList2.Contains(message2))
					{
						arrayList2.Add(message2);
					}
				}
			}
			if (arrayList2.Count > 0)
			{
				string text9 = "";
				foreach (object obj3 in arrayList2)
				{
					string str = (string)obj3;
					if (text9.Length > 0)
					{
						text9 += Environment.NewLine;
					}
					text9 += str;
				}
				MessageBox.Show("Some rows were not added due to errors: " + Environment.NewLine + text9, "There were errors.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			oleDbConnection.Close();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000FE50 File Offset: 0x0000EE50
		public static void ExportToAccess(string tableName, DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			bool askUserToFilterColumns2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			TemplatesClass.ExportToAccess(tableName, new DataView(t), tempFilename, startDirectory, askUserToFilterColumns2);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000FE80 File Offset: 0x0000EE80
		public static void ExportToAccess(string tableName, DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			string text = TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, true);
			if (text == null)
			{
				return;
			}
			if (text != null && text.Length > 0)
			{
				tableName = text;
			}
			else
			{
				tableName = tableName.Trim();
			}
			if (tableName.Length <= 0)
			{
				tableName = "table1";
			}
			DataTable table = dv.Table;
			Type type = Type.GetType("System.DateTime");
			Type type2 = Type.GetType("System.Double");
			Type type3 = Type.GetType("System.Int32");
			Type type4 = Type.GetType("System.Boolean");
			if (!File.Exists(tempFilename))
			{
				File.Copy(Path.Combine(startDirectory, "BlankAccess.mdb"), tempFilename, true);
			}
			OleDbConnection oleDbConnection = new OleDbConnection(Core.GetAccessConnectionString(tempFilename));
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("", oleDbConnection);
			int num = 0;
			string text2 = "";
			string text3 = "";
			string[] array = new string[table.Columns.Count];
			oleDbDataAdapter.SelectCommand.CommandText = "CREATE TABLE " + tableName + " (";
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text4 = dataColumn.ColumnName.Replace("/", "");
					text4 = text4.Replace("_", "");
					text4 = text4.Replace(".", "");
					if (text4.Length < 1)
					{
						text4 = "Unknown" + i.ToString();
					}
					text4 = "[" + text4 + "]";
					string text5 = "@col" + i.ToString();
					if (num++ > 0)
					{
						OleDbCommand selectCommand = oleDbDataAdapter.SelectCommand;
						selectCommand.CommandText += ",";
						text2 += ",";
						text3 += ",";
					}
					OleDbCommand selectCommand2 = oleDbDataAdapter.SelectCommand;
					selectCommand2.CommandText += text4;
					text2 += text5;
					text3 += text4;
					array[i] = text5;
					if (table.Columns[i].DataType == type3)
					{
						OleDbCommand selectCommand3 = oleDbDataAdapter.SelectCommand;
						selectCommand3.CommandText += " NUMBER";
					}
					else if (table.Columns[i].DataType == type)
					{
						OleDbCommand selectCommand4 = oleDbDataAdapter.SelectCommand;
						selectCommand4.CommandText += " DATETIME";
					}
					else if (table.Columns[i].DataType == type2)
					{
						OleDbCommand selectCommand5 = oleDbDataAdapter.SelectCommand;
						selectCommand5.CommandText += " DOUBLE";
					}
					else if (table.Columns[i].DataType == type4)
					{
						OleDbCommand selectCommand6 = oleDbDataAdapter.SelectCommand;
						selectCommand6.CommandText += " BIT";
					}
					else
					{
						OleDbCommand selectCommand7 = oleDbDataAdapter.SelectCommand;
						selectCommand7.CommandText += " TEXT";
					}
				}
			}
			OleDbCommand selectCommand8 = oleDbDataAdapter.SelectCommand;
			selectCommand8.CommandText += ")";
			try
			{
				oleDbDataAdapter.Fill(new DataTable());
			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}
			int num2 = 0;
			oleDbConnection.Open();
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				num2++;
				DataRow row = dataRowView.Row;
				oleDbDataAdapter.SelectCommand.CommandText = string.Concat(new string[]
				{
					"INSERT INTO ",
					tableName,
					" (",
					text3,
					") VALUES (",
					text2,
					")"
				});
				oleDbDataAdapter.SelectCommand.Parameters.Clear();
				for (int j = 0; j < table.Columns.Count; j++)
				{
					DataColumn dataColumn2 = table.Columns[j];
					if (dataColumn2.ColumnMapping != MappingType.Hidden)
					{
						oleDbDataAdapter.SelectCommand.Parameters.Add(array[j], row[j]);
					}
				}
				try
				{
					oleDbDataAdapter.SelectCommand.ExecuteNonQuery();
				}
				catch (Exception)
				{
				}
			}
			oleDbConnection.Close();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001031C File Offset: 0x0000F31C
		public static void ShowDelimiteredTextFile(string fn)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			TemplatesClass.ShowDelimiteredTextFile(fn, !flag);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00010348 File Offset: 0x0000F348
		public static void ShowDelimiteredTextFile(string fn, bool showInNotepad)
		{
			string text = Path.Combine(Environment.SystemDirectory, "notepad.exe");
			if (!File.Exists(text))
			{
				text = Path.Combine(Environment.SystemDirectory, "system32");
				if (!File.Exists(text))
				{
					text = null;
				}
			}
			if (!showInNotepad)
			{
				text = null;
			}
			if (text != null)
			{
				Process.Start(text, fn);
				return;
			}
			Process.Start(fn);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001039F File Offset: 0x0000F39F
		public static void ExportToDelimeteredText(DataTable t, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			TemplatesClass.ExportToDelimeteredText(new DataView(t), tempFilename, startDirectory, askUserToFilterColumns, colDelimiter, rowDelimiter);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000103B3 File Offset: 0x0000F3B3
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToDelimeteredText(dv, tempFilename, startDirectory, askUserToFilterColumns, ",", Environment.NewLine);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000103C8 File Offset: 0x0000F3C8
		public static void ExportToDelimeteredText(DataView dv, string tempFilename, string startDirectory, bool askUserToFilterColumns, string colDelimiter, string rowDelimiter)
		{
			if (TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false) == null)
			{
				return;
			}
			DataTable table = dv.Table;
			Type type = Type.GetType("System.Int32");
			Type type2 = Type.GetType("System.DateTime");
			Type type3 = Type.GetType("System.Double");
			StreamWriter streamWriter = new StreamWriter(tempFilename);
			bool flag = true;
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					string text = dataColumn.ColumnName;
					text = text.Replace(colDelimiter, " ");
					text = text.Replace(rowDelimiter, "");
					if (!flag)
					{
						streamWriter.Write(colDelimiter);
					}
					else
					{
						flag = false;
					}
					streamWriter.Write(text);
				}
			}
			streamWriter.Write(rowDelimiter);
			foreach (object obj in dv)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				flag = true;
				for (int j = 0; j < table.Columns.Count; j++)
				{
					DataColumn dataColumn2 = table.Columns[j];
					if (dataColumn2.ColumnMapping != MappingType.Hidden)
					{
						if (!flag)
						{
							streamWriter.Write(colDelimiter);
						}
						else
						{
							flag = false;
						}
						string text2;
						if (table.Columns[j].DataType == type)
						{
							text2 = row[j].ToString();
						}
						else if (table.Columns[j].DataType == type2)
						{
							if (row[j] == DBNull.Value)
							{
								text2 = "";
							}
							else
							{
								DateTime dateTime = (DateTime)row[j];
								text2 = dateTime.ToShortDateString();
								if (dateTime.Hour != 0 || dateTime.Minute != 0)
								{
									text2 = text2 + " " + dateTime.ToLongTimeString();
								}
							}
						}
						else if (table.Columns[j].DataType == type3)
						{
							text2 = row[j].ToString();
						}
						else
						{
							text2 = row[j].ToString();
						}
						text2 = text2.Replace(colDelimiter, " ").Replace(rowDelimiter, "");
						streamWriter.Write(text2);
					}
				}
				streamWriter.Write(rowDelimiter);
			}
			streamWriter.Close();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00010654 File Offset: 0x0000F654
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToFormattedText(t, tempFilename, askUserToFilterColumns, true);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00010660 File Offset: 0x0000F660
		public static void ExportToFormattedText(DataTable t, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			DataView dv = new DataView(t);
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, showNotepad, true);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001067E File Offset: 0x0000F67E
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns)
		{
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, true, true);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001068A File Offset: 0x0000F68A
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns, bool showNotepad)
		{
			TemplatesClass.ExportToFormattedText(dv, tempFilename, askUserToFilterColumns, showNotepad, true);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00010698 File Offset: 0x0000F698
		public static void ExportToFormattedText(DataView dv, string tempFilename, bool askUserToFilterColumns, bool showNotepad, bool showColumnNames)
		{
			DataTable table = dv.Table;
			if (TemplatesClass.AllowUserToChooseColumns(ref dv, askUserToFilterColumns, false) == null)
			{
				return;
			}
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
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						int num = row[j].ToString().Trim().Length + 1;
						if (num > array[j])
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
					if (dataColumn2.ColumnMapping != MappingType.Hidden)
					{
						string text = dataColumn2.ColumnName;
						int num2 = array[k] - text.Length;
						if (num2 > 0)
						{
							text += new string(' ', num2);
						}
						streamWriter.Write(text);
					}
				}
				streamWriter.WriteLine();
				for (int l = 0; l < table.Columns.Count; l++)
				{
					DataColumn dataColumn3 = table.Columns[l];
					if (dataColumn3.ColumnMapping != MappingType.Hidden)
					{
						string text2 = "";
						int num3 = array[l] - text2.Length - 1;
						if (num3 > 0)
						{
							text2 += new string('=', num3);
						}
						text2 += "+";
						streamWriter.Write(text2);
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
					if (dataColumn4.ColumnMapping != MappingType.Hidden)
					{
						string text3 = row2[m].ToString();
						int num4 = array[m] - text3.Length;
						if (num4 > 0)
						{
							text3 += new string(' ', num4);
						}
						streamWriter.Write(text3);
					}
				}
				streamWriter.WriteLine();
			}
			streamWriter.Close();
			if (showNotepad && File.Exists(tempFilename))
			{
				Process.Start(tempFilename, "");
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00010988 File Offset: 0x0000F988
		public static string GetTempFilename(string fnExtension)
		{
			string tempFileName = Path.GetTempFileName();
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.GetFileNameWithoutExtension(tempFileName) + fnExtension;
			return Path.Combine(text, path);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000109DC File Offset: 0x0000F9DC
		public static string GetTempFilenameGuid(string fnExtension)
		{
			string path = string.Format("{0}_{1}{2}", Guid.NewGuid().ToString(), DateTime.Now.Millisecond.ToString(), fnExtension);
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return Path.Combine(text, path);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00010A54 File Offset: 0x0000FA54
		public static string FillTemplate(ArrayList items, string templateFilename)
		{
			ArrayList codes = TemplatesClass.GetCodes(templateFilename, '#', '<', '>', '#');
			string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(templateFilename));
			foreach (object obj in items)
			{
				DataRow dataRow = null;
				if (obj is ListViewItem)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (listViewItem.Tag is DataRow)
					{
						dataRow = (DataRow)listViewItem.Tag;
					}
					else if (listViewItem.Tag is ImportItem)
					{
						ImportItem importItem = (ImportItem)listViewItem.Tag;
						dataRow = importItem._dataRow;
					}
				}
				else if (obj is DataRow)
				{
					dataRow = (DataRow)obj;
				}
				if (dataRow != null)
				{
					TemplatesClass.SetCodeValues(dataRow, ref codes);
				}
			}
			TemplatesClass.WriteCodes(templateFilename, tempFilename, new ArrayList(1)
			{
				codes
			});
			return tempFilename;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010B50 File Offset: 0x0000FB50
		public static string FillTemplate(DataTable t, string nameColName, string valCol, string templateFilename)
		{
			ArrayList codes = TemplatesClass.GetCodes(templateFilename, '#', '<', '>', '#');
			string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(templateFilename));
			TemplatesClass.SetCodeValues(ref codes, t, nameColName, valCol);
			TemplatesClass.WriteCodes(templateFilename, tempFilename, new ArrayList(1)
			{
				codes
			});
			return tempFilename;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00010B9C File Offset: 0x0000FB9C
		public static string FillTemplateMultiple(DataTable t, int[] uniqueItemCols, string nameColName, string valCol, string templateFilename)
		{
			if (t.Rows.Count < 1)
			{
				return "";
			}
			DataSet dataSet = new DataSet();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = "t_";
				for (int i = 0; i < uniqueItemCols.Length; i++)
				{
					text += dataRow[uniqueItemCols[i]].ToString().Trim();
				}
				if (!arrayList.Contains(text))
				{
					arrayList.Add(text);
					DataTable dataTable = t.Clone();
					dataTable.TableName = text;
					dataSet.Tables.Add(dataTable);
				}
				DataTable dataTable2 = dataSet.Tables[text];
				DataRow row = TemplatesClass.CopyRow(dataRow, dataTable2);
				dataTable2.Rows.Add(row);
			}
			return TemplatesClass.FillTemplateMultiple(dataSet, uniqueItemCols, nameColName, valCol, templateFilename);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00010CB0 File Offset: 0x0000FCB0
		public static DataRow CopyRow(DataRow drSource, DataTable tDest)
		{
			DataRow dataRow = tDest.NewRow();
			for (int i = 0; i < tDest.Columns.Count; i++)
			{
				dataRow[i] = drSource[i];
			}
			return dataRow;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010CEC File Offset: 0x0000FCEC
		public static string FillTemplateMultiple(DataSet tables, int[] uniqueItemCols, string nameColName, string valCol, string templateFilename)
		{
			ArrayList arrayList = new ArrayList(tables.Tables.Count);
			string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(templateFilename));
			foreach (object obj in tables.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (uniqueItemCols != null && dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					for (int i = 0; i < uniqueItemCols.Length; i++)
					{
						DataRow dataRow2 = dataTable.NewRow();
						string columnName = dataTable.Columns[uniqueItemCols[i]].ColumnName;
						dataRow2[nameColName] = columnName;
						string value = dataRow[uniqueItemCols[i]].ToString();
						dataRow2[valCol] = value;
						dataTable.Rows.Add(dataRow2);
					}
				}
				ArrayList codes = TemplatesClass.GetCodes(templateFilename, '#', '<', '>', '#');
				TemplatesClass.SetCodeValues(ref codes, dataTable, nameColName, valCol);
				arrayList.Add(codes);
			}
			TemplatesClass.WriteCodes(templateFilename, tempFilename, arrayList);
			return tempFilename;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00010E1C File Offset: 0x0000FE1C
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
				if (text.Length > 2 && text[0] == '[')
				{
					int num = text.IndexOf(']', 1);
					if (num > 1)
					{
						text3 = text.Substring(1, num - 1);
						if (text3.CompareTo("\\N") == 0)
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
				if (array3.Length == 1)
				{
					array3 = text.Split(new char[]
					{
						','
					});
					text4 = ", ";
				}
				if (array3.Length == 1)
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
					string text7;
					bool flag;
					if (text6.Length > 0 && text6[text6.Length - 1] == '1')
					{
						if (text6.Length == 1)
						{
							text7 = "";
						}
						else
						{
							text7 = text6.Substring(0, text6.Length - 1);
						}
						flag = true;
					}
					else
					{
						text7 = text6;
						flag = false;
					}
					bool flag2 = false;
					for (int k = 0; k < dr.Table.Columns.Count; k++)
					{
						string text8 = dr.Table.Columns[k].ColumnName.ToUpper().Trim();
						string text9 = dr[k].ToString().Trim();
						if (text8.CompareTo(text7) == 0)
						{
							if (text9.Length > 0)
							{
								bool flag3;
								if (flag)
								{
									string[] array5 = text5.Split(text4.ToCharArray());
									flag3 = (Array.IndexOf<string>(array5, text9) < 0);
								}
								else
								{
									flag3 = true;
								}
								if (flag3)
								{
									if (text5.Length > 0)
									{
										text5 += text4;
									}
									text5 += text9;
								}
							}
							flag2 = true;
							break;
						}
					}
					if (!flag2 && text7.CompareTo("DATE") == 0)
					{
						if (text5.Length > 0)
						{
							text5 += text4;
						}
						text5 += DateTime.Now.ToShortDateString();
					}
				}
				if (!arrayList2.Contains(text5))
				{
					for (int l = 0; l < arrayList2.Count; l++)
					{
						string text10 = arrayList2[l].ToString();
						text10.Replace(" ", " ");
					}
					arrayList2.Add(text5);
					if (code.codeValue.Length > 0)
					{
						Code code2 = code;
						code2.codeValue += text3;
					}
					Code code3 = code;
					code3.codeValue += text5;
				}
				if (code.codeValue.Trim().Length < 1)
				{
					arrayList.Add(code);
				}
			}
			return arrayList;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000111CC File Offset: 0x000101CC
		public static string GetTemplateFromUser(Form parentForm, string templatesDirectory)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Please choose the template file to use:";
			openFileDialog.InitialDirectory = templatesDirectory;
			DialogResult dialogResult;
			if (parentForm == null)
			{
				dialogResult = openFileDialog.ShowDialog();
			}
			else
			{
				dialogResult = openFileDialog.ShowDialog(parentForm);
			}
			if (dialogResult == DialogResult.OK)
			{
				return openFileDialog.FileName;
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00011214 File Offset: 0x00010214
		public static void ExportItemsOneTemplate(Form parentForm, ArrayList items, string StartDirectory, Settings settings)
		{
			string settingString = settings.GetSettingString(19, Path.Combine(StartDirectory, "templates"));
			string templateFromUser = TemplatesClass.GetTemplateFromUser(parentForm, settingString);
			if (templateFromUser != null)
			{
				string tempFilename = TemplatesClass.FillTemplate(items, templateFromUser);
				TemplatesClass.LaunchFilledInTemplate(tempFilename);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001124E File Offset: 0x0001024E
		public static void LaunchFilledInTemplate(string tempFilename)
		{
			if (File.Exists(tempFilename))
			{
				Process.Start(tempFilename);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00011260 File Offset: 0x00010260
		public static ArrayList SetCodeValues(ref ArrayList codes, DataTable t, string namecol, string valcol)
		{
			ArrayList arrayList = new ArrayList();
			int num = t.Columns.IndexOf(namecol);
			int num2 = t.Columns.IndexOf(valcol);
			if (num >= 0 && num2 >= 0)
			{
				foreach (object obj in codes)
				{
					Code code = (Code)obj;
					string codeValue = TemplatesClass.GetCodeValue(t, num, num2, code);
					if (codeValue.Length > 0)
					{
						if (code.codeValue.Length > 0)
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

		// Token: 0x060001C9 RID: 457 RVA: 0x00011334 File Offset: 0x00010334
		public static string GetCodeValue(DataTable t, int namec, int valc, Code code)
		{
			string text = code.codeText.Trim().ToLower();
			bool flag = t.Columns.Contains("cid");
			string strB = flag ? TemplatesClass.ExtractDigitsOnly(text) : "";
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					string text2 = dataRow[namec].ToString().Trim().ToLower();
					if (text2.CompareTo(text) == 0)
					{
						return dataRow[valc].ToString().Trim();
					}
					if (flag)
					{
						string text3 = dataRow["cid"].ToString();
						if (text3.CompareTo(strB) == 0)
						{
							return dataRow[valc].ToString().Trim();
						}
					}
				}
			}
			return "";
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00011448 File Offset: 0x00010448
		private static string ExtractDigitsOnly(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsDigit(c))
				{
					text += c;
				}
			}
			return text;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001148C File Offset: 0x0001048C
		public static void ClearClockWorkTempFiles()
		{
			try
			{
				string text = Path.GetTempPath();
				text = Path.Combine(text, "TechnoPro\\ClockWork");
				if (!Directory.Exists(text))
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

		// Token: 0x060001CC RID: 460 RVA: 0x000114E4 File Offset: 0x000104E4
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
			catch (Exception)
			{
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001156C File Offset: 0x0001056C
		public static void CreateWordReport(string text, Image img)
		{
			_Application application;
			try
			{
				object value = Missing.Value;
				application = null;
				application = new ApplicationClass();
				application.Visible = true;
				application.Documents.Add(ref value, ref value, ref value, ref value);
				application.Selection.ExtendMode = false;
				if (img != null)
				{
					string tempFilename = TemplatesClass.GetTempFilename(".jpg");
					img.Save(tempFilename, ImageFormat.Jpeg);
					try
					{
						application.ActiveWindow.Selection.Range.InlineShapes.AddPicture(tempFilename, ref value, ref value, ref value);
					}
					finally
					{
						File.Delete(tempFilename);
					}
				}
				Selection selection = application.Selection;
				selection.InsertAfter("ClockWork Problem Report" + Environment.NewLine + Environment.NewLine);
				selection.InsertAfter(text);
				selection.MoveStart(ref value, ref value);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			application = null;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00011658 File Offset: 0x00010658
		private static string GetExcelColEquivalent(int colInd, int rowInd)
		{
			if (colInd <= 26)
			{
				return (char)(65 + colInd + 1) + rowInd.ToString();
			}
			return "a";
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00011680 File Offset: 0x00010680
		public static string ExportToExcelTemplate(string fn, DataView dv)
		{
			Dictionary<string, string> args = new Dictionary<string, string>();
			return TemplatesClass.ExportToExcelTemplate(fn, dv, args);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0001169B File Offset: 0x0001069B
		public static void FormatExcelFile(string fn, bool firstRowIsColumnHeaders, bool shadeAlternateRows)
		{
			TemplatesClass.FormatExcelFile(fn, firstRowIsColumnHeaders, shadeAlternateRows, 15);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000116A8 File Offset: 0x000106A8
		public static void FormatExcelFile(string fn, bool firstRowIsColumnHeaders, bool shadeAlternateRows, int shadeColourIndex)
		{
			object value = Missing.Value;
			try
			{
				_Workbook workbook = new ApplicationClass
				{
					DisplayAlerts = false
				}.Workbooks.Open(fn, 0, false, 5, Missing.Value, Missing.Value, true, 2, "\t", true, false, Missing.Value, true);
				Sheets worksheets = workbook.Worksheets;
				_Worksheet worksheet = (Worksheet)worksheets[1];
				Range usedRange = worksheet.UsedRange;
				int count = usedRange.Columns.Count;
				int count2 = usedRange.Rows.Count;
				if (firstRowIsColumnHeaders)
				{
					worksheet.get_Range(worksheet.Cells.get__Default(1, 1), worksheet.Cells.get__Default(1, count)).Font.Bold = true;
				}
				if (shadeAlternateRows)
				{
					for (int i = 1; i <= count2; i += 2)
					{
						object obj = worksheet.Cells.get__Default(i, 1);
						object obj2 = worksheet.Cells.get__Default(i, count);
						worksheet.get_Range(obj, obj2).Interior.ColorIndex = shadeColourIndex;
					}
				}
				workbook.Save();
				workbook.Close(false, value, value);
				if (workbook != null)
				{
					Marshal.ReleaseComObject(workbook);
				}
			}
			catch
			{
			}
			finally
			{
				GC.Collect();
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00011874 File Offset: 0x00010874
		public static List<string> ExtractCodesFromExcelTemplate(string fn)
		{
			List<string> list = new List<string>();
			object value = Missing.Value;
			try
			{
				string tempFilename = TemplatesClass.GetTempFilename(".xls");
				File.Copy(fn, tempFilename);
				Application application = new ApplicationClass();
				if (application == null)
				{
					return list;
				}
				_Workbook workbook = application.Workbooks.Open(tempFilename, 0, false, 5, Missing.Value, Missing.Value, true, 2, "\t", true, false, Missing.Value, true);
				Sheets worksheets = workbook.Worksheets;
				_Worksheet worksheet = (Worksheet)worksheets[1];
				Range range = worksheet.Cells.Find("#<", worksheet.Cells.get__Default(1, 1), -4163, 2, value, 1, false, value);
				if (range != null)
				{
					int num = -1;
					int num2 = -1;
					while (range != null)
					{
						if (range.Text != null)
						{
							string text = (string)range.Text;
							if (text.StartsWith("#<") && text.EndsWith(">#"))
							{
								list.Add(text.Substring(2, text.Length - 4));
							}
						}
						if (num < 0 && num2 < 0)
						{
							num = range.Row;
							num2 = range.Column;
						}
						range = worksheet.Cells.FindNext(range);
						if (range != null && range.Row == num && range.Column == num2)
						{
							break;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00011A3C File Offset: 0x00010A3C
		public static string ExportToExcelTemplate(string fn, DataView dv, Dictionary<string, string> args)
		{
			Application application = null;
			DataTable table = dv.Table;
			string result;
			try
			{
				string tempFilename = TemplatesClass.GetTempFilename(".xls");
				File.Copy(fn, tempFilename);
				application = new ApplicationClass();
				if (application == null)
				{
					result = "";
				}
				else
				{
					_Workbook workbook = application.Workbooks.Open(tempFilename, 0, false, 5, Missing.Value, Missing.Value, true, 2, "\t", true, false, Missing.Value, true);
					Sheets worksheets = workbook.Worksheets;
					_Worksheet worksheet = (Worksheet)worksheets[1];
					foreach (KeyValuePair<string, string> keyValuePair in args)
					{
						string text = string.Format("#<{0}>#", keyValuePair.Key);
						Range range = worksheet.Cells.Find(text, Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
						if (range != null && range.Cells.Count > 0)
						{
							range.Value = keyValuePair.Value;
						}
					}
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					int num = -1;
					int num2 = -1;
					for (int i = 0; i < 200; i++)
					{
						string excelCellValue = TemplatesClass.GetExcelCellValue(worksheet, 1, i + 1);
						if (excelCellValue.IndexOf("#<") == 0 && excelCellValue.IndexOf(">#") > 0)
						{
							num = 0;
							num2 = i;
							for (int j = 0; j < 255; j++)
							{
								string excelCellValue2 = TemplatesClass.GetExcelCellValue(worksheet, j + 1, i + 1);
								if (excelCellValue2.Length <= 0 || excelCellValue2.IndexOf("#<") != 0 || excelCellValue2.IndexOf(">#") <= 0)
								{
									break;
								}
								int num3 = excelCellValue2.IndexOf(">#");
								string text2 = excelCellValue2.Substring(2, num3 - 2);
								string text3 = "";
								if (text2.IndexOf("*") >= 0)
								{
									text2 = text2.Replace("*", "");
									text3 += "*";
								}
								else if (text2.IndexOf("!") >= 0)
								{
									text2 = text2.Replace("!", "");
									text3 += "!";
								}
								arrayList2.Add(text3);
								string[] array;
								if (text2.Length > 0 && text2[0] == '.')
								{
									array = new string[]
									{
										text2
									};
								}
								else
								{
									array = text2.Split(new char[]
									{
										'.'
									});
								}
								if (array.Length > 1)
								{
									int[] array2 = new int[array.Length];
									for (int k = 0; k < array.Length; k++)
									{
										int num4 = table.Columns.IndexOf(array[k]);
										array2[k] = num4;
									}
									arrayList.Add(array2);
								}
								else
								{
									int num5 = table.Columns.IndexOf(text2);
									arrayList.Add(new int[]
									{
										num5
									});
								}
							}
							break;
						}
					}
					if (arrayList.Count > 0 && num >= 0 && num2 >= 0)
					{
						int num6 = num;
						int num7 = num2;
						Range range = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 2, num + arrayList.Count, num2 + 2);
						range.EntireRow.Insert(Missing.Value);
						range = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1, num + arrayList.Count, num2 + 1);
						range.Copy(Missing.Value);
						range = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 2, num + arrayList.Count, num2 + 2);
						range.PasteSpecial(-4104, -4142, false, false);
						num2++;
						for (int l = 0; l < table.Rows.Count - 1; l++)
						{
							Range excelRange = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + l, num + arrayList.Count, num2 + 1 + l);
							excelRange.EntireRow.Insert(-4121);
						}
						num2--;
						object[] array3 = new object[arrayList.Count];
						for (int m = 0; m < array3.Length; m++)
						{
							array3[m] = null;
						}
						for (int n = 0; n < table.Rows.Count; n++)
						{
							Range excelRange2 = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + n, num + arrayList.Count, num2 + 1 + n);
							object[] itemArray = table.Rows[n].ItemArray;
							object[] array4 = new object[arrayList.Count];
							for (int num8 = 0; num8 < array4.Length; num8++)
							{
								int[] array5 = (int[])arrayList[num8];
								object obj;
								if (array5.Length == 1)
								{
									int num9 = array5[0];
									if (num9 >= 0)
									{
										obj = itemArray[num9];
									}
									else
									{
										obj = "";
									}
								}
								else
								{
									string text4 = "";
									bool flag = false;
									foreach (int num11 in array5)
									{
										if (!flag && text4.Length > 0)
										{
											text4 += " ";
										}
										if (num11 >= 0)
										{
											text4 += itemArray[num11];
											flag = false;
										}
										else
										{
											text4 = (text4 ?? "");
											flag = true;
										}
									}
									obj = text4;
								}
								if (obj != null && obj is string)
								{
									string text5 = (string)obj;
									obj = text5.Replace("\r\n", "\n");
								}
								string text6 = (string)arrayList2[num8];
								if (text6.IndexOf("*") >= 0)
								{
									if (array3[num8] == null)
									{
										array3[num8] = new NameValueCollection();
									}
									NameValueCollection nameValueCollection = (NameValueCollection)array3[num8];
									string text7 = obj.ToString().Trim();
									if (text7.Length > 0)
									{
										string[] array6 = text7.Split(new char[]
										{
											','
										});
										string text8 = "";
										foreach (string text9 in array6)
										{
											string text10 = text9.Trim();
											if (text10.Length > 0)
											{
												if ((text10.ToLower().IndexOf("other") == 0 && text10.IndexOf(":") > 0) || (text10.ToLower().IndexOf("extra time") == 0 && text10.IndexOf(":") > 0))
												{
													if (text8.Length > 0)
													{
														text8 += ", ";
													}
													text8 += text10;
												}
												else
												{
													string text11 = nameValueCollection[text10];
													if (text11 == null)
													{
														text11 = TemplatesClass.FigureOutLegendCode(nameValueCollection, text10);
														args.Add(text10, text11);
													}
													if (text8.Length > 0)
													{
														text8 += ", ";
													}
													text8 += text11;
												}
											}
										}
										obj = text8;
									}
								}
								else
								{
									text6.IndexOf("!");
								}
								array4[num8] = obj;
							}
							excelRange2.Value = array4;
							excelRange2.EntireRow.AutoFit();
						}
						Range excelRange3 = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + table.Rows.Count, num + arrayList.Count, num2 + 1 + table.Rows.Count);
						excelRange3.EntireRow.Delete(Missing.Value);
						foreach (object obj2 in array3)
						{
							if (obj2 != null && obj2 is NameValueCollection)
							{
								NameValueCollection nameValueCollection2 = (NameValueCollection)obj2;
								if (nameValueCollection2.Count > 0)
								{
									range = worksheet.Cells.Find("*legend1*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
									if (range == null || range.Cells.Count < 1)
									{
										range = worksheet.Cells.Find("*legend*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
										if (range != null && range.Cells.Count > 0)
										{
											string text12 = "";
											for (int num13 = 0; num13 < nameValueCollection2.Count; num13++)
											{
												text12 = text12 + nameValueCollection2.Keys[num13] + " - ";
												string text13 = nameValueCollection2[num13];
												if (text13 != null)
												{
													text12 += text13;
												}
												text12 += '\n';
											}
											range.Value = text12;
											range.EntireRow.AutoFit();
											break;
										}
										break;
									}
									else
									{
										Range range2 = worksheet.Cells.Find("*legend1*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
										object rowHeight = range2.EntireRow.RowHeight;
										double num14 = Convert.ToDouble(rowHeight);
										Range excelRange4 = TemplatesClass.GetExcelRange(worksheet, 1, 1, 1, 1);
										excelRange4.EntireRow.Insert(-4121);
										Range excelRange5 = TemplatesClass.GetExcelRange(worksheet, 1, 1, 1, 1);
										excelRange5.Font.Size = range2.Font.Size;
										excelRange5.Value = "hi";
										excelRange5.EntireRow.AutoFit();
										object rowHeight2 = excelRange5.EntireRow.RowHeight;
										double num15 = Convert.ToDouble(rowHeight2);
										excelRange5.EntireRow.Delete(Missing.Value);
										int num16 = 1;
										for (int num17 = 2; num17 < 25; num17++)
										{
											range = worksheet.Cells.Find("*legend" + num17.ToString() + "*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
											if (range == null || range.Cells.Count < 1)
											{
												break;
											}
											num16++;
										}
										int num18 = Convert.ToInt32(nameValueCollection2.Count / num16);
										if (nameValueCollection2.Count % num16 != 0)
										{
											num18++;
										}
										int num19 = 1;
										range = worksheet.Cells.Find("*legend1*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
										string text14 = "";
										for (int num20 = 0; num20 < nameValueCollection2.Count; num20++)
										{
											if (num20 > 0 && num20 % num18 == 0)
											{
												if (range != null && range.Cells.Count > 0)
												{
													range.Value = text14;
												}
												num19++;
												text14 = "";
												range = worksheet.Cells.Find("*legend" + num19.ToString() + "*", Missing.Value, Missing.Value, Missing.Value, Missing.Value, 1, false, false);
											}
											if (range != null && range.Cells.Count > 0)
											{
												if (text14.Length > 0)
												{
													text14 += '\n';
												}
												text14 = text14 + nameValueCollection2.Keys[num20] + " - ";
												string text15 = nameValueCollection2[num20];
												if (text15 != null)
												{
													text14 += text15;
												}
											}
										}
										if (range != null && range.Cells.Count > 0)
										{
											range.Value = text14;
										}
										if (range2 != null && range2.Cells.Count > 0)
										{
											double num21 = num15 * (double)num18;
											if (num21 < num14)
											{
												num21 = num14;
											}
											if (num21 > 400.0)
											{
												num21 = 400.0;
											}
											range2.EntireRow.RowHeight = num21;
											break;
										}
										break;
									}
								}
							}
						}
						range = TemplatesClass.GetExcelRange(worksheet, num6 + 1, num7 + 1, num6 + 1, num7 + 1);
						range.Select();
						application.Visible = true;
					}
					application.UserControl = true;
					application = null;
					worksheet = null;
					GC.Collect();
					result = tempFilename;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
				if (application != null)
				{
					try
					{
						application.Interactive = false;
						application.UserControl = false;
						application.Quit();
					}
					catch
					{
					}
				}
				application = null;
				_Worksheet worksheet = null;
				GC.Collect();
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00012720 File Offset: 0x00011720
		public static string LookupValue(TemplateCodeCollection codes, string codeText)
		{
			string strB = codeText.ToLower().Trim();
			foreach (object obj in codes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				if (templateCode.CodeName_lcase.CompareTo(strB) == 0)
				{
					return templateCode.CodeValue.ToString();
				}
			}
			return "";
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000127A0 File Offset: 0x000117A0
		public static string ExportToExcelTemplate2(string fn, DataView dv, TemplateCodeCollection staticCodes)
		{
			Application application = null;
			DataTable table = dv.Table;
			object color = ColorTranslator.ToWin32(Color.White);
			string result;
			try
			{
				string tempFilename = TemplatesClass.GetTempFilename(".xls");
				File.Copy(fn, tempFilename);
				application = new ApplicationClass();
				if (application == null)
				{
					result = "";
				}
				else
				{
					_Workbook workbook = application.Workbooks.Open(tempFilename, 0, false, 5, Missing.Value, Missing.Value, true, 2, "\t", true, false, Missing.Value, true);
					Sheets worksheets = workbook.Worksheets;
					_Worksheet worksheet = (Worksheet)worksheets[1];
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					int num = -1;
					int num2 = -1;
					for (int i = 0; i < 200; i++)
					{
						Range range;
						string excelCellValue = TemplatesClass.GetExcelCellValue2(worksheet, 1, i + 1, out range);
						if ((excelCellValue.IndexOf("#<.") != 0 || excelCellValue.IndexOf(">#") <= 0) && excelCellValue.IndexOf("#<") == 0 && excelCellValue.IndexOf(">#") > 0)
						{
							num = 0;
							num2 = i;
							for (int j = 0; j < 255; j++)
							{
								string excelCellValue2 = TemplatesClass.GetExcelCellValue(worksheet, j + 1, i + 1);
								if (excelCellValue2.Length <= 0 || excelCellValue2.IndexOf("#<") != 0 || excelCellValue2.IndexOf(">#") <= 0)
								{
									break;
								}
								int num3 = excelCellValue2.IndexOf(">#");
								string text = excelCellValue2.Substring(2, num3 - 2);
								string text2 = "";
								if (text.IndexOf("*") >= 0)
								{
									text = text.Replace("*", "");
									text2 += "*";
								}
								else if (text.IndexOf("!") >= 0)
								{
									text = text.Replace("!", "");
									text2 += "!";
								}
								arrayList2.Add(text2);
								string[] array = text.Split(new char[]
								{
									'.'
								});
								if (array.Length > 1)
								{
									int[] array2 = new int[array.Length];
									for (int k = 0; k < array.Length; k++)
									{
										int num4 = table.Columns.IndexOf(array[k]);
										array2[k] = num4;
									}
									arrayList.Add(array2);
								}
								else
								{
									int num5 = table.Columns.IndexOf(text);
									arrayList.Add(new int[]
									{
										num5
									});
								}
							}
							break;
						}
					}
					if (arrayList.Count > 0 && num >= 0 && num2 >= 0)
					{
						int num6 = num;
						int num7 = num2;
						Range excelRange = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 2, num + arrayList.Count, num2 + 2);
						excelRange.EntireRow.Insert(Missing.Value);
						color = excelRange.Interior.Color;
						excelRange = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1, num + arrayList.Count, num2 + 1);
						excelRange.Copy(Missing.Value);
						excelRange = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 2, num + arrayList.Count, num2 + 2);
						excelRange.PasteSpecial(-4104, -4142, false, false);
						num2++;
						for (int l = 0; l < table.Rows.Count - 1; l++)
						{
							Range excelRange2 = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + l, num + arrayList.Count, num2 + 1 + l);
							excelRange2.EntireRow.Insert(-4121);
						}
						num2--;
						object[] array3 = new object[arrayList.Count];
						for (int m = 0; m < array3.Length; m++)
						{
							array3[m] = null;
						}
						for (int n = 0; n < table.Rows.Count; n++)
						{
							Range excelRange3 = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + n, num + arrayList.Count, num2 + 1 + n);
							if (n % 2 == 1)
							{
								excelRange3.EntireRow.Interior.Color = color;
							}
							object[] itemArray = table.Rows[n].ItemArray;
							object[] array4 = new object[arrayList.Count];
							for (int num8 = 0; num8 < array4.Length; num8++)
							{
								int[] array5 = (int[])arrayList[num8];
								object obj;
								if (array5.Length == 1)
								{
									int num9 = array5[0];
									if (num9 >= 0)
									{
										obj = itemArray[num9];
									}
									else
									{
										obj = "";
									}
								}
								else
								{
									string text3 = "";
									bool flag = false;
									foreach (int num11 in array5)
									{
										if (!flag && text3.Length > 0)
										{
											text3 += " ";
										}
										if (num11 >= 0)
										{
											text3 += itemArray[num11];
											flag = false;
										}
										else
										{
											text3 = (text3 ?? "");
											flag = true;
										}
									}
									obj = text3;
								}
								string text4 = (string)arrayList2[num8];
								array4[num8] = obj;
							}
							excelRange3.Value = array4;
							excelRange3.EntireRow.AutoFit();
						}
						Range excelRange4 = TemplatesClass.GetExcelRange(worksheet, num + 1, num2 + 1 + table.Rows.Count, num + arrayList.Count, num2 + 1 + table.Rows.Count);
						excelRange4.EntireRow.Delete(Missing.Value);
						decimal subTotal = 0m;
						foreach (object obj2 in staticCodes)
						{
							TemplateCode templateCode = (TemplateCode)obj2;
							if (templateCode.CodeName_lcase.IndexOf("subtotalx") < 0)
							{
								TemplatesClass.ReplaceValuesExcel(worksheet, "#<." + templateCode.CodeName_lcase + ">#", templateCode.GetCodeValueString());
								if (templateCode.CodeName_lcase.CompareTo("subtotal") == 0)
								{
									subTotal = (decimal)templateCode.CodeValue;
								}
							}
						}
						TemplatesClass.ReplaceValuesExcelMultiplier(worksheet, "subtotalx1.", subTotal);
						TemplatesClass.ReplaceValuesExcelMultiplier(worksheet, "subtotalx.", subTotal);
						excelRange = TemplatesClass.GetExcelRange(worksheet, num6 + 1, num7 + 1, num6 + 1, num7 + 1);
						excelRange.Select();
						application.Visible = true;
					}
					application.UserControl = true;
					application = null;
					worksheet = null;
					GC.Collect();
					result = tempFilename;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
				if (application != null)
				{
					try
					{
						application.Interactive = false;
						application.UserControl = false;
						application.Quit();
					}
					catch
					{
					}
				}
				application = null;
				_Worksheet worksheet = null;
				GC.Collect();
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00012EE8 File Offset: 0x00011EE8
		private static void ReplaceValuesExcelMultiplier(_Worksheet ws, string codePrefix, decimal subTotal)
		{
			object value = Missing.Value;
			object obj = false;
			Range range = ws.Cells.Find(codePrefix, value, value, 2, value, 1, obj, value);
			if (range != null)
			{
				foreach (object obj2 in range.Cells)
				{
					Range range2 = (Range)obj2;
					string text = (range2.Value == null) ? "" : range2.Value.ToString();
					int num = text.IndexOf('x');
					if (num > 0)
					{
						string s = text.Substring(num + 1).Replace(">#", "");
						decimal d;
						try
						{
							d = decimal.Parse(s);
						}
						catch
						{
							d = 0m;
						}
						range2.Value = Math.Round(subTotal * d, 2).ToString();
					}
					else
					{
						range2.Value = Math.Round(subTotal, 2).ToString();
					}
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00013014 File Offset: 0x00012014
		private static void ReplaceValuesExcel(_Worksheet sheet, string oldValue, string newValue)
		{
			object value = Missing.Value;
			object obj = false;
			Range range = sheet.Cells.Find(oldValue, value, value, 2, value, 1, obj, value);
			if (range != null)
			{
				foreach (object obj2 in range.Cells)
				{
					Range range2 = (Range)obj2;
					object value2 = range2.Value;
					string text = (value2 != null) ? value2.ToString() : "";
					range2.Value = text.Replace(oldValue, newValue);
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000130C4 File Offset: 0x000120C4
		private static string FigureOutLegendCode(NameValueCollection args, string name0)
		{
			if (name0.Length < 1)
			{
				return "";
			}
			string text = TemplatesClass.ExtractFirstNumber(name0);
			if (text.Length > 0 && name0.ToLower().IndexOf("double") > 0)
			{
				text = "D" + text;
			}
			string text2 = "";
			foreach (char c in name0)
			{
				if (!char.IsDigit(c))
				{
					text2 += c;
				}
			}
			string[] array = text2.Split(new char[]
			{
				' '
			});
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Trim().Length > 0)
				{
					num++;
				}
			}
			string[] array2;
			if (num > 1)
			{
				array2 = new string[num];
				int num2 = 0;
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k].Trim().Length > 0)
					{
						array2[num2++] = array[k].Trim();
					}
				}
			}
			else
			{
				array2 = null;
			}
			for (int l = 0; l < text2.Length; l++)
			{
				int num3 = l + 1;
				string text3;
				if (num3 > 1 && array2 != null && array2.Length > 1 && num3 <= array2.Length)
				{
					text3 = "";
					for (int m = 0; m < num3; m++)
					{
						text3 += array2[m].Substring(0, 1);
					}
				}
				else
				{
					text3 = text2.Substring(0, num3);
				}
				if (text3.Length < 1 || text3.IndexOf(" ") >= 0)
				{
					text3 = text2.Substring(0, num3);
				}
				string text4 = text3 + text;
				if (TemplatesClass.IndexOfValueCollectionValue(args, text4) < 0)
				{
					return text4.ToUpper();
				}
			}
			return text2;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00013294 File Offset: 0x00012294
		private static string ExtractFirstNumber(string s)
		{
			bool flag = false;
			string text = "";
			foreach (char c in s)
			{
				if (flag && !char.IsDigit(c))
				{
					break;
				}
				if (char.IsDigit(c))
				{
					flag = true;
					text += c;
				}
			}
			return text;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000132EC File Offset: 0x000122EC
		private static int IndexOfValueCollectionValue(NameValueCollection args, string val)
		{
			for (int i = 0; i < args.Count; i++)
			{
				string text = args[i];
				if (text != null && text.CompareTo(val) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00013324 File Offset: 0x00012324
		private static Range GetExcelRange(_Worksheet ws, int x1, int y1, int x2, int y2)
		{
			Range excelCell = TemplatesClass.GetExcelCell(ws, x1, y1);
			Range excelCell2 = TemplatesClass.GetExcelCell(ws, x2, y2);
			return ws.get_Range(excelCell, excelCell2);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00013350 File Offset: 0x00012350
		private static Range GetExcelCell(_Worksheet ws, int x, int y)
		{
			object obj = ws.Cells.get__Default(y, x);
			if (obj != null)
			{
				return (Range)obj;
			}
			return null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00013384 File Offset: 0x00012384
		private static string GetExcelCellValue(_Worksheet ws, int x, int y)
		{
			Range excelCell = TemplatesClass.GetExcelCell(ws, x, y);
			if (excelCell != null)
			{
				object value = excelCell.Value;
				if (value != null)
				{
					return value.ToString().Trim();
				}
			}
			return "";
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000133BC File Offset: 0x000123BC
		private static string GetExcelCellValue2(_Worksheet ws, int x, int y, out Range range)
		{
			range = TemplatesClass.GetExcelCell(ws, x, y);
			if (range != null)
			{
				object value = range.Value;
				if (value != null)
				{
					return value.ToString().Trim();
				}
			}
			return "";
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000133F8 File Offset: 0x000123F8
		private static string[] ConvertToStringArray(Array values)
		{
			string[] array = new string[values.Length];
			for (int i = 1; i <= values.Length; i++)
			{
				if (values.GetValue(1, i) == null)
				{
					array[i - 1] = "";
				}
				else
				{
					array[i - 1] = values.GetValue(1, i).ToString();
				}
			}
			return array;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0001344C File Offset: 0x0001244C
		public static TemplateCodeGroupCollection ExtractCodes(string fileName, bool printAutomaticallyIfSupported, bool returnWordApp, ref object WordApp, out string errmsg)
		{
			return TemplatesClass.ExtractCodes(fileName, printAutomaticallyIfSupported, '#', '<', '>', '#', returnWordApp, ref WordApp, out errmsg);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001346C File Offset: 0x0001246C
		public static TemplateCodeGroupCollection ExtractCodes(string fileName, bool printAutomaticallyIfSupported, char char1a, char char1b, char char2a, char char2b, bool returnWordApp, ref object WordApp, out string errmsg)
		{
			if (fileName == null || fileName.Length < 1)
			{
				errmsg = "";
				return null;
			}
			if (Path.GetExtension(fileName).ToLower().CompareTo(".doc") == 0)
			{
				return TemplatesClass.ExtractCodesFromWord(fileName, printAutomaticallyIfSupported, char1a, char1b, char2a, char2b, returnWordApp, ref WordApp, out errmsg);
			}
			int @byte = Convert.ToInt32(char1a);
			int byte2 = Convert.ToInt32(char1b);
			int byte3 = Convert.ToInt32(char2a);
			int byte4 = Convert.ToInt32(char2b);
			StreamReader streamReader = new StreamReader(fileName);
			Stream baseStream = streamReader.BaseStream;
			Queue queue = new Queue();
			TemplateCodeGroupCollection templateCodeGroupCollection = new TemplateCodeGroupCollection();
			for (long num = TemplatesClass.IndexOf(baseStream, @byte, byte2, ref queue, false); num >= 0L; num = TemplatesClass.IndexOf(baseStream, @byte, byte2, ref queue, false))
			{
				long num2 = TemplatesClass.IndexOf(baseStream, byte3, byte4, ref queue, true);
				if (num2 >= 0L)
				{
					string text = "";
					while (queue.Count > 0)
					{
						byte value = (byte)queue.Dequeue();
						text += Convert.ToChar(value).ToString();
					}
					text = text.Trim().ToUpper();
					if (text.Length > 0)
					{
						TemplateCodeGroup templateCodeGroup = new TemplateCodeGroup(text, num - 2L, num2 - 1L);
						templateCodeGroupCollection.Add(templateCodeGroup);
					}
				}
				queue.Clear();
			}
			baseStream.Close();
			errmsg = null;
			WordApp = null;
			return templateCodeGroupCollection;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000135BC File Offset: 0x000125BC
		public static object GetNewWordAppInstance()
		{
			_Application application = new ApplicationClass();
			application.Visible = false;
			object value = Missing.Value;
			object obj = false;
			_Document document = application.Documents.Add(ref value, ref value, ref value, ref obj);
			document.Close(ref obj, ref value, ref value);
			return application;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00013608 File Offset: 0x00012608
		public static bool CloseWordAppIfItsNotVisible(object wordAppObj)
		{
			if (wordAppObj != null)
			{
				_Application application = (_Application)wordAppObj;
				if (!application.Visible)
				{
					TemplatesClass.CloseWordApp(wordAppObj);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00013630 File Offset: 0x00012630
		public static void CloseWordApp(object wordAppObj)
		{
			if (wordAppObj != null)
			{
				object value = Missing.Value;
				object obj = false;
				_Application application = (_Application)wordAppObj;
				application.Quit(ref obj, ref value, ref value);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00013664 File Offset: 0x00012664
		public static TemplateCodeGroupCollection ExtractCodesFromWord(string fileName, bool printAutomaticallyIfSupported, char char1a, char char1b, char char2a, char char2b, bool returnWordApp, ref object WordApp, out string errmsg)
		{
			char1a + char1b;
			char2a + char2b;
			TemplateCodeGroupCollection templateCodeGroupCollection = new TemplateCodeGroupCollection();
			object value = Missing.Value;
			object obj = 0;
			object obj2 = false;
			_Application application;
			if (WordApp == null)
			{
				application = null;
				application = new ApplicationClass();
				application.Visible = false;
			}
			else
			{
				application = (_Application)WordApp;
			}
			object obj3 = fileName;
			object obj4 = true;
			object obj5 = false;
			_Document document;
			try
			{
				document = application.Documents.Open(ref obj3, ref value, ref obj4, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj5);
			}
			catch
			{
				application = new ApplicationClass();
				application.Visible = false;
				document = null;
			}
			if (document == null)
			{
				document = application.Documents.Open(ref obj3, ref value, ref obj4, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj5);
			}
			int num = 0;
			object obj6 = document.Content.Start;
			object obj7 = document.Content.End;
			Range range = document.Range(ref obj6, ref obj7);
			Range duplicate = range.Duplicate;
			duplicate.Select();
			duplicate.Find.ClearFormatting();
			duplicate.Find.Forward = true;
			for (;;)
			{
				duplicate.Find.Text = "#<";
				if (!duplicate.Find.Execute(ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value))
				{
					break;
				}
				int num2 = duplicate.Start + 2;
				Range duplicate2 = range.Duplicate;
				duplicate2.SetRange(num2, range.End);
				string text = duplicate2.Text;
				string text2 = "";
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					if (c == '>')
					{
						int num3 = i + 1;
						if (num3 < text.Length)
						{
							char c2 = text[num3];
							if (c2 == '#')
							{
								break;
							}
							text2 += c;
						}
						else
						{
							text2 += c;
						}
					}
					else
					{
						text2 += c;
					}
				}
				if (text2.Length > 0)
				{
					TemplateCodeGroup templateCodeGroup = new TemplateCodeGroup(text2, 0L, 0L);
					templateCodeGroupCollection.Add(templateCodeGroup);
				}
				num++;
			}
			foreach (object obj8 in document.Bookmarks)
			{
				Bookmark bookmark = (Bookmark)obj8;
				string text3 = bookmark.Name.ToLower().Trim();
				if (text3.IndexOf("remove_") == 0)
				{
					string rawCode = text3.Substring(7);
					TemplateCodeGroup templateCodeGroup2 = new TemplateCodeGroup(rawCode, 0L, 0L);
					templateCodeGroupCollection.Add(templateCodeGroup2);
				}
			}
			for (int j = 0; j < 1; j++)
			{
				Section first = document.Sections.First;
				for (int k = 0; k < 6; k++)
				{
					HeaderFooter headerFooter;
					switch (k)
					{
					case 0:
						headerFooter = first.Footers.Item(2);
						break;
					case 1:
						headerFooter = first.Footers.Item(1);
						break;
					case 2:
						headerFooter = first.Footers.Item(3);
						break;
					case 3:
						headerFooter = first.Headers.Item(1);
						break;
					case 4:
						headerFooter = first.Headers.Item(2);
						break;
					case 5:
						headerFooter = first.Headers.Item(3);
						break;
					default:
						headerFooter = first.Footers.Item(1);
						break;
					}
					duplicate = headerFooter.Range.Duplicate;
					if (duplicate != null)
					{
						range = duplicate.Duplicate;
						duplicate.Select();
						duplicate.Find.ClearFormatting();
						duplicate.Find.Forward = true;
						for (;;)
						{
							duplicate.Find.Text = "#<";
							if (!duplicate.Find.Execute(ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value))
							{
								break;
							}
							int num4 = duplicate.Start + 2;
							Range duplicate3 = range.Duplicate;
							duplicate3.SetRange(num4, range.End);
							string text4 = duplicate3.Text;
							string text5 = "";
							for (int l = 0; l < text4.Length; l++)
							{
								char c3 = text4[l];
								if (c3 == '>')
								{
									int num5 = l + 1;
									if (num5 < text4.Length)
									{
										char c4 = text4[num5];
										if (c4 == '#')
										{
											break;
										}
										text5 += c3;
									}
									else
									{
										text5 += c3;
									}
								}
								else
								{
									text5 += c3;
								}
							}
							if (text5.Length > 0)
							{
								TemplateCodeGroup templateCodeGroup3 = new TemplateCodeGroup(text5, 0L, 0L);
								templateCodeGroupCollection.Add(templateCodeGroup3);
							}
						}
					}
				}
			}
			TemplateCode templateCode = templateCodeGroupCollection.FindTemplateCode("!");
			if (templateCode != null)
			{
				templateCodeGroupCollection.NewPageSeparator = templateCode.YesRule;
				templateCode.IgnoreForFillCodes = true;
			}
			bool flag = !returnWordApp;
			document.Close(ref obj2, ref value, ref value);
			if (flag)
			{
				application.Quit(ref obj2, ref value, ref value);
				application = null;
				WordApp = null;
			}
			else
			{
				WordApp = application;
			}
			document = null;
			errmsg = null;
			return templateCodeGroupCollection;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00013BF4 File Offset: 0x00012BF4
		public static void WriteCodes2(string fileName, string tempFileName, ArrayList codesMultiple, bool printDocument, TemplatesClassPrinterSettings printerSettings, bool openDocument, string password, out string errmsg)
		{
			TemplatesClass.WriteCodes2(null, fileName, tempFileName, codesMultiple, printDocument, printerSettings, openDocument, password, out errmsg);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00013C14 File Offset: 0x00012C14
		public static void WriteCodes2(object WordApp, string fileName, string tempFileName, ArrayList codesMultiple, bool printDocument, TemplatesClassPrinterSettings printerSettings, bool openDocument, string password, out string errmsg)
		{
			if (Path.GetExtension(fileName).ToUpper().CompareTo(".DOC") == 0)
			{
				TemplatesClass.ToWordFile2(WordApp, fileName, tempFileName, codesMultiple, printDocument, printerSettings, openDocument, password, out errmsg);
				return;
			}
			try
			{
				StreamWriter streamWriter = new StreamWriter(tempFileName, false);
				Stream baseStream = streamWriter.BaseStream;
				StreamReader streamReader = new StreamReader(fileName);
				Stream baseStream2 = streamReader.BaseStream;
				bool flag = true;
				foreach (object obj in codesMultiple)
				{
					TemplateCodeGroupCollection templateCodeGroupCollection = (TemplateCodeGroupCollection)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						TemplatesClass.WriteString(baseStream, Environment.NewLine + Environment.NewLine);
					}
					baseStream2.Position = 0L;
					int num = 0;
					for (int i = baseStream2.ReadByte(); i >= 0; i = baseStream2.ReadByte())
					{
						bool flag2 = true;
						long num2 = baseStream2.Position - 1L;
						if (num < templateCodeGroupCollection.Count)
						{
							TemplateCodeGroup templateCodeGroup = templateCodeGroupCollection[num];
							foreach (object obj2 in templateCodeGroup.SubCodes)
							{
								TemplateCode templateCode = (TemplateCode)obj2;
								if (num2 == templateCodeGroup.StartIndex)
								{
									while (i >= 0 && baseStream2.Position <= templateCodeGroup.EndIndex)
									{
										i = baseStream2.ReadByte();
									}
									string codeValue = templateCodeGroup.GetCodeValue(templateCodeGroupCollection, num);
									TemplatesClass.WriteString(baseStream, codeValue);
									num++;
									flag2 = false;
								}
							}
						}
						if (flag2)
						{
							byte value = (byte)i;
							baseStream.WriteByte(value);
						}
					}
					baseStream2.Close();
				}
				streamWriter.Close();
				if (openDocument)
				{
					Process.Start(tempFileName);
				}
				errmsg = null;
			}
			catch (Exception ex)
			{
				errmsg = ex.ToString();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00013E24 File Offset: 0x00012E24
		private static Range WordFindLongText(Range Range, string findTextLong, int max)
		{
			object value = Missing.Value;
			object obj = 0;
			Range duplicate = Range.Duplicate;
			int num = findTextLong.Length / max;
			if (findTextLong.Length % max > 0)
			{
				num++;
			}
			int num2 = 0;
			while (num2++ < 10000)
			{
				string text = findTextLong.Substring(0, max);
				duplicate.Find.Text = text;
				duplicate.Find.MatchCase = false;
				bool flag = duplicate.Find.Execute(ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value);
				if (!flag || duplicate == null || duplicate.Text == null)
				{
					return null;
				}
				int start = duplicate.Start;
				for (int i = 1; i < num; i++)
				{
					int num3 = i * max;
					int num4 = findTextLong.Length - num3;
					duplicate.SetRange(duplicate.Start + max, duplicate.Start + max + num4);
					string text2 = (duplicate.Text == null) ? "" : duplicate.Text.ToLower();
					string strB = findTextLong.Substring(num3, num4).ToLower();
					if (text2.CompareTo(strB) != 0)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					duplicate.SetRange(start, start + findTextLong.Length);
					return duplicate;
				}
			}
			return null;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00013F78 File Offset: 0x00012F78
		private static void WordSearchAndReplace(Range range, string findText, string replaceText, bool matchCase)
		{
			object value = Missing.Value;
			object obj = 2;
			int num = 9213;
			replaceText = replaceText.Replace("\r", "^l");
			int num2 = 200;
			string text;
			if (findText.Length > num2)
			{
				text = "#<tp...." + num.ToString() + ">#";
				num++;
				for (Range range2 = TemplatesClass.WordFindLongText(range, findText, num2); range2 != null; range2 = TemplatesClass.WordFindLongText(range, findText, num2))
				{
					if (range2.Text == null)
					{
						break;
					}
					range2.Text = text;
				}
			}
			else
			{
				text = findText;
			}
			if (replaceText.Length > num2)
			{
				int num3 = replaceText.Length / num2;
				if (replaceText.Length % num2 > 0)
				{
					num3++;
				}
				for (int i = 0; i < num3; i++)
				{
					int startIndex = i * num2;
					string text2;
					if (i < num3 - 1)
					{
						text2 = replaceText.Substring(startIndex, num2);
					}
					else
					{
						text2 = replaceText.Substring(startIndex);
					}
					if (i < num3 - 1)
					{
						text2 += text;
					}
					range.Find.Text = text;
					range.Find.Replacement.Text = text2;
					range.Find.MatchCase = matchCase;
					range.Find.Execute(ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value);
				}
				return;
			}
			range.Find.Text = text;
			range.Find.Replacement.Text = replaceText;
			range.Find.MatchCase = matchCase;
			range.Find.Execute(ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref obj, ref value, ref value, ref value, ref value);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00014128 File Offset: 0x00013128
		private static void WordSearchAndReplaceAllHeadersFooters(_Application wordApp, _Document wordDoc, string findText, string replaceText, bool matchCase)
		{
			for (int i = 0; i < 1; i++)
			{
				Section first = wordDoc.Sections.First;
				for (int j = 0; j < 6; j++)
				{
					Range range;
					switch (j)
					{
					case 0:
						range = first.Footers.Item(2).Range;
						break;
					case 1:
						range = first.Footers.Item(1).Range;
						break;
					case 2:
						range = first.Footers.Item(3).Range;
						break;
					case 3:
						range = first.Headers.Item(1).Range;
						break;
					case 4:
						range = first.Headers.Item(2).Range;
						break;
					case 5:
						range = first.Headers.Item(3).Range;
						break;
					default:
						range = first.Footers.Item(1).Range;
						break;
					}
					if (range != null)
					{
						TemplatesClass.WordSearchAndReplace(range, findText, replaceText, matchCase);
					}
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00014220 File Offset: 0x00013220
		public static void ToWordFile2(string fileName, string tempFileName, ArrayList codesMultiple, bool printDocument, TemplatesClassPrinterSettings printerSettings, bool openDocument, string password, out string errmsg)
		{
			TemplatesClass.ToWordFile2(null, fileName, tempFileName, codesMultiple, printDocument, printerSettings, openDocument, password, out errmsg);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001423F File Offset: 0x0001323F
		public static string ToWordFile4(string fileName, string tempFilename, ArrayList codesMultiple, bool openDocument, bool exportAsPdf, out string errmsg)
		{
			return TemplatesClass.ToWordFile4(false, fileName, tempFilename, codesMultiple, openDocument, exportAsPdf, out errmsg);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00014270 File Offset: 0x00013270
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
				if (num2 <= num)
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
				if (i > 0)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00014350 File Offset: 0x00013350
		private static string FixTextForPdf(string s)
		{
			if (s.Contains("\n"))
			{
				string[] array = s.Split("\n".ToCharArray());
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (i > 0)
					{
						stringBuilder.Append("\n");
					}
					if (text.Length > 80)
					{
						stringBuilder.Append(TemplatesClass.WordWrap(text, 80));
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				return stringBuilder.ToString();
			}
			return s;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000143D0 File Offset: 0x000133D0
		public static string ToWordFile4(bool filenameContainsTemplateBase64_tempFilenameContainsNothing, string fileName, string tempFilename, ArrayList codesMultiple, bool openDocument, bool exportAsPdf, out string errmsg)
		{
			LicenseProvider.Register("TechnoPro Computer Solutions, Inc.", "LnTchk/c5BSbDo0OBQsod+tvC4obtOiMQzzMP76h0VxH3bPwamkzFzetBo4sMHdF");
			Document document = new Document();
			Document document2;
			if (filenameContainsTemplateBase64_tempFilenameContainsNothing)
			{
				byte[] buffer = Convert.FromBase64String(fileName);
				MemoryStream stream = new MemoryStream(buffer);
				document2 = new Document(stream);
			}
			else
			{
				document2 = new Document(fileName);
			}
			try
			{
				int count = codesMultiple.Count;
				for (int i = 0; i < codesMultiple.Count; i++)
				{
					Document document3 = document2.Clone();
					TemplateCodeGroupCollection templateCodeGroupCollection = null;
					ArrayList arrayList = null;
					bool flag = true;
					if (codesMultiple[i] is TemplateCodeGroupCollection)
					{
						templateCodeGroupCollection = (TemplateCodeGroupCollection)codesMultiple[i];
					}
					else
					{
						arrayList = (ArrayList)codesMultiple[i];
						flag = false;
					}
					int num = flag ? templateCodeGroupCollection.Count : arrayList.Count;
					for (int j = 0; j < num; j++)
					{
						string text;
						string text2;
						if (flag)
						{
							TemplateCodeGroup templateCodeGroup = templateCodeGroupCollection[j];
							text = templateCodeGroup.Char1ab + templateCodeGroup.RawCode + templateCodeGroup.Char2ab;
							text2 = templateCodeGroup.GetCodeValue(templateCodeGroupCollection, j).Replace(Environment.NewLine, '\r'.ToString());
						}
						else
						{
							Code code = (Code)arrayList[j];
							text = "#<" + code.codeText + ">#";
							text2 = code.codeValue;
						}
						bool flag2 = text2 != null && text2.StartsWith("*IMAGE*:");
						if (!flag2)
						{
							text2 = text2.Replace(Environment.NewLine, "\n");
							text2 = TemplatesClass.FixTextForPdf(text2);
						}
						if (text2.Length < 1)
						{
							document3.Replace(text, "", false, true);
						}
						else
						{
							if (flag2)
							{
								try
								{
									string s = text2.Substring(8);
									byte[] array = Convert.FromBase64String(s);
									using (MemoryStream memoryStream = new MemoryStream(array, 0, array.Length))
									{
										memoryStream.Write(array, 0, array.Length);
										Image image = Image.FromStream(memoryStream, true);
										if (image != null)
										{
											TextSelection textSelection = document3.FindString(text, false, true);
											if (textSelection != null)
											{
												TextRange asOneRange = textSelection.GetAsOneRange();
												if (asOneRange != null)
												{
													asOneRange.OwnerParagraph.AppendPicture(image);
												}
												asOneRange.Text = "";
											}
										}
									}
									goto IL_227;
								}
								catch
								{
									document3.Replace(text, text2, false, false);
									goto IL_227;
								}
							}
							document3.Replace(text, text2, false, false);
						}
						IL_227:;
					}
					document.ImportContent(document3);
				}
				if (filenameContainsTemplateBase64_tempFilenameContainsNothing)
				{
					MemoryStream memoryStream2 = new MemoryStream();
					FileFormat fileFormat;
					if (exportAsPdf)
					{
						fileFormat = FileFormat.Html;
					}
					else
					{
						fileFormat = FileFormat.Auto;
					}
					document.SaveToStream(memoryStream2, fileFormat);
					memoryStream2.Seek(0L, SeekOrigin.Begin);
					byte[] array2 = new byte[memoryStream2.Length];
					memoryStream2.Read(array2, 0, array2.Length);
					tempFilename = Convert.ToBase64String(array2);
				}
				else if (exportAsPdf)
				{
					string directoryName = Path.GetDirectoryName(tempFilename);
					tempFilename = Path.GetFileNameWithoutExtension(tempFilename) + ".pdf";
					tempFilename = Path.Combine(directoryName, tempFilename);
					document.SaveToFile(tempFilename, FileFormat.Html);
				}
				else
				{
					document.SaveToFile(tempFilename);
				}
				document.Close();
				if (openDocument && !filenameContainsTemplateBase64_tempFilenameContainsNothing)
				{
					Process.Start(tempFilename);
				}
				errmsg = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				errmsg = ex.ToString();
			}
			return tempFilename;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00014740 File Offset: 0x00013740
		public static void ToWordFile2(object WordApp, string fileName, string tempFileName, ArrayList codesMultiple, bool printDocument, TemplatesClassPrinterSettings printerSettings, bool openDocument, string password, out string errmsg)
		{
			_Application application = null;
			try
			{
				object value = Missing.Value;
				object obj = "\\endofdoc";
				object obj2 = 7;
				object obj3 = false;
				object obj4 = true;
				if (WordApp != null && WordApp is _Application)
				{
					application = (_Application)WordApp;
				}
				else
				{
					application = new ApplicationClass();
					application.Visible = false;
				}
				object obj5 = fileName;
				_Document document;
				try
				{
					document = application.Documents.Open(ref obj5, ref value, ref obj4, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				}
				catch
				{
					document = null;
					application = new ApplicationClass();
					application.Visible = false;
				}
				if (document == null)
				{
					document = application.Documents.Open(ref obj5, ref value, ref obj4, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				}
				float leftMargin = document.PageSetup.LeftMargin;
				float topMargin = document.PageSetup.TopMargin;
				float bottomMargin = document.PageSetup.BottomMargin;
				float rightMargin = document.PageSetup.RightMargin;
				document.Close(ref value, ref value, ref value);
				document = null;
				_Document document2 = application.Documents.Add(ref value, ref value, ref value, ref value);
				if (topMargin < 99999f)
				{
					document2.PageSetup.TopMargin = topMargin;
					document2.PageSetup.BottomMargin = bottomMargin;
					document2.PageSetup.LeftMargin = leftMargin;
					document2.PageSetup.RightMargin = rightMargin;
				}
				bool flag = true;
				int count = codesMultiple.Count;
				TemplateCodeGroupCollection templateCodeGroupCollection = null;
				object obj6 = document2.Content.Start;
				object obj7 = document2.Content.End;
				Range range = document2.Range(ref obj6, ref obj7);
				for (int i = 0; i < codesMultiple.Count; i++)
				{
					if (flag)
					{
						flag = false;
						range.SetRange(range.Start, range.Start);
						int end = range.End;
						range.InsertFile(fileName, ref value, ref obj3, ref value, ref value);
						range.SetRange(end, range.End);
					}
					else
					{
						range = document2.Bookmarks.Item(ref obj).Range;
						if (templateCodeGroupCollection.NewPageSeparator == "\\p")
						{
							range.InsertBreak(ref obj2);
						}
						else
						{
							string str = templateCodeGroupCollection.NewPageSeparator.Replace("\\n", TemplatesClass.NewLine);
							Range range2 = range;
							range2.Text += str;
						}
						int start = range.Start;
						range.InsertFile(fileName, ref value, ref obj3, ref value, ref value);
						range.SetRange(start, document2.Content.End);
					}
					templateCodeGroupCollection = (TemplateCodeGroupCollection)codesMultiple[i];
					for (int j = 0; j < templateCodeGroupCollection.Count; j++)
					{
						TemplateCodeGroup templateCodeGroup = templateCodeGroupCollection[j];
						string text = templateCodeGroup.Char1ab + templateCodeGroup.RawCode + templateCodeGroup.Char2ab;
						string text2 = templateCodeGroup.GetCodeValue(templateCodeGroupCollection, j).Replace(Environment.NewLine, '\r'.ToString());
						if (text2.Length < 1)
						{
							TemplatesClass.WordSearchAndReplace(range, text, "", false);
						}
						else if (text.IndexOf("#<TABLE") == 0)
						{
							string[] array = text2.Split(new char[]
							{
								'`'
							});
							string[] array2 = array[0].Split(new char[]
							{
								'|'
							});
							object[] args = new object[]
							{
								text,
								false,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value,
								value
							};
							object find = application.Selection.Find;
							find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, args);
							Table table = document2.Tables.Add(application.Selection.Range, array.Length, array2.Length, ref value, ref value);
							for (int k = 0; k < array.Length; k++)
							{
								string[] array3 = array[k].Split(new char[]
								{
									'|'
								});
								for (int l = 0; l < array2.Length; l++)
								{
									if (k == 0)
									{
										table.Cell(1, l + 1).Shading.BackgroundPatternColorIndex = 16;
									}
									table.Cell(k + 1, l + 1).Range.InsertAfter(array3[l]);
								}
							}
						}
						else
						{
							TemplatesClass.WordSearchAndReplace(range, text, text2, false);
							TemplatesClass.WordSearchAndReplaceAllHeadersFooters(application, document2, text, text2, false);
						}
					}
				}
				TemplatesClass.FixWordCheckboxes(application, document2, "~x~", "R", "WingDings 2");
				TemplatesClass.FixWordCheckboxes(application, document2, "~p~", "£", "WingDings 2");
				TemplatesClass.CheckForBookmarkDefinitions2(document2, (TemplateCodeGroupCollection)codesMultiple[0], value);
				object obj8 = tempFileName;
				document2.SaveAs(ref obj8, ref value, ref value, ref value, ref value, ref value, ref obj4, ref value, ref value, ref value, ref value);
				if (password != null)
				{
					document2.Password = password;
					document2.Save();
				}
				if (printDocument)
				{
					if (printerSettings != null && printerSettings.PrintPreview)
					{
						if (printerSettings.PrinterSettings != null)
						{
							application.ActivePrinter = printerSettings.PrinterSettings.PrinterName;
						}
						application.PrintPreview = true;
						openDocument = true;
					}
					else
					{
						object obj9 = obj3;
						object obj10 = obj3;
						object obj11 = 0;
						object obj12 = value;
						object obj13 = value;
						object obj14 = value;
						object obj15 = 0;
						object obj16 = "1";
						object obj17 = "1";
						object obj18 = 0;
						object obj19 = obj3;
						object obj20 = obj4;
						object obj21 = value;
						object obj22 = obj3;
						object obj23 = value;
						object obj24 = value;
						object obj25 = value;
						object obj26 = value;
						if (printerSettings != null && printerSettings.PrinterSettings != null)
						{
							PrinterSettings printerSettings2 = printerSettings.PrinterSettings;
							application.ActivePrinter = printerSettings.PrinterSettings.PrinterName;
							obj16 = printerSettings2.Copies;
							obj20 = printerSettings2.Collate;
						}
						document2.PrintOut(ref obj9, ref obj10, ref obj11, ref obj12, ref obj13, ref obj14, ref obj15, ref obj16, ref obj17, ref obj18, ref obj19, ref obj20, ref obj21, ref obj22, ref obj23, ref obj24, ref obj25, ref obj26);
					}
				}
				if (openDocument)
				{
					application.Visible = true;
					application.Activate();
				}
				else
				{
					document2.Close(ref value, ref value, ref value);
					application.Quit(ref value, ref value, ref value);
					application = null;
				}
				errmsg = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				errmsg = ex.ToString();
				if (application != null)
				{
					application.Visible = true;
				}
			}
			application = null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00014DE4 File Offset: 0x00013DE4
		public static void CloseWord(ref object wordDocObj, ref object wordAppObj)
		{
			object value = Missing.Value;
			if (wordDocObj != null)
			{
				_Document document = (_Document)wordDocObj;
				document.Close(ref value, ref value, ref value);
			}
			if (wordAppObj != null)
			{
				_Application application = (_Application)wordAppObj;
				application.Quit(ref value, ref value, ref value);
			}
			wordDocObj = null;
			wordAppObj = null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00014E38 File Offset: 0x00013E38
		public static object GetControlValue(Control c, bool ignoreLabels, out Type controlDataType, TemplateCode tc, TemplateCodeGroup tcg)
		{
			if (c is CheckBox || c is MyCheckBox)
			{
				CheckBox checkBox = (CheckBox)c;
				controlDataType = typeof(bool);
				return checkBox.Checked;
			}
			if (c is RadioButton)
			{
				RadioButton radioButton = (RadioButton)c;
				controlDataType = typeof(bool);
				return radioButton.Checked;
			}
			if (c is MyDateTimePicker)
			{
				MyDateTimePicker myDateTimePicker = (MyDateTimePicker)c;
				DataRow dataRow = (c.Tag != null && c.Tag is DataRow) ? ((DataRow)c.Tag) : null;
				if (dataRow != null)
				{
					int num = (int)dataRow[2];
					if (num == 6)
					{
						controlDataType = typeof(string);
						if (myDateTimePicker.Value == DateTime.MinValue)
						{
							return null;
						}
						return myDateTimePicker.Value.ToString("yyyy-MM-dd");
					}
				}
				controlDataType = typeof(DateTime);
				if (myDateTimePicker.Value == DateTime.MinValue)
				{
					return null;
				}
				return myDateTimePicker.Value;
			}
			else
			{
				if (c is System.Windows.Forms.TextBox || c is MyTextBox || c is MaskedTextBox)
				{
					controlDataType = typeof(string);
					return c.Text;
				}
				if (c is AutoComboBox)
				{
					controlDataType = typeof(string);
					return c.Text;
				}
				if (c is MyRadioGroup)
				{
					MyRadioGroup myRadioGroup = (MyRadioGroup)c;
					controlDataType = typeof(string);
					return myRadioGroup.SelectedText;
				}
				if (c is MyRadioGroupPrimaryCheckboxMultiple)
				{
					MyRadioGroupPrimaryCheckboxMultiple myRadioGroupPrimaryCheckboxMultiple = (MyRadioGroupPrimaryCheckboxMultiple)c;
					controlDataType = typeof(bool);
					return myRadioGroupPrimaryCheckboxMultiple.Checked;
				}
				if (c is MyRadioGroupPrimary)
				{
					MyRadioGroupPrimary myRadioGroupPrimary = (MyRadioGroupPrimary)c;
					controlDataType = typeof(string);
					return myRadioGroupPrimary.SelectedText;
				}
				if (c is ListViewEx)
				{
					controlDataType = typeof(string);
					ListViewEx listViewEx = (ListViewEx)c;
					string text = "";
					foreach (object obj in listViewEx.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						text += Environment.NewLine;
						text += listViewItem.Text.Trim();
						for (int i = 1; i < listViewEx.Columns.Count; i++)
						{
							text = text + ", " + listViewItem.SubItems[i].Text;
						}
					}
					return text;
				}
				if (!ignoreLabels && c is Label)
				{
					controlDataType = typeof(string);
					string text2 = "";
					int num2 = c.Parent.Controls.IndexOf(c);
					string str = (tcg.Prefix == null) ? "" : tcg.Prefix;
					string text3 = (tcg.Suffix == null) ? "" : tcg.Suffix;
					for (int j = num2 + 1; j < c.Parent.Controls.Count; j++)
					{
						Control c2 = c.Parent.Controls[j];
						Type dataType;
						object controlValue = TemplatesClass.GetControlValue(c2, true, out dataType, tc, tcg);
						string defaultDataString = TemplatesClass.GetDefaultDataString(c2, controlValue, dataType);
						if (defaultDataString.Length > 0)
						{
							text2 = text2 + str + defaultDataString + text3;
						}
					}
					if (text2.Length > 0)
					{
						string text4 = (tcg.GroupPrefix == null) ? "" : tcg.GroupPrefix;
						string text5 = (tcg.GroupSuffix == null) ? "" : tcg.GroupSuffix;
						tcg.Prefix = "";
						tcg.Suffix = "";
						tcg.GroupPrefix = "";
						tcg.GroupSuffix = "";
						return string.Concat(new string[]
						{
							text4,
							c.Text,
							text3,
							text2,
							text5
						});
					}
					return "";
				}
				else if (c is MyMultiCheckbox)
				{
					MyMultiCheckbox myMultiCheckbox = (MyMultiCheckbox)c;
					controlDataType = typeof(string);
					string text6 = myMultiCheckbox.GetText().Trim();
					if (text6.Length > 0)
					{
						return text6;
					}
					CheckBox lastCheckbox = myMultiCheckbox.GetLastCheckbox();
					if (lastCheckbox != null && lastCheckbox.Checked)
					{
						return "-";
					}
					return "";
				}
				else
				{
					if (c is MyRichText)
					{
						MyRichText myRichText = (MyRichText)c;
						controlDataType = typeof(string);
						return myRichText.PlainText;
					}
					controlDataType = typeof(string);
					return "";
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000152E0 File Offset: 0x000142E0
		private static string FormatCaption(string caption)
		{
			string text = caption.Trim();
			if (text.LastIndexOf(":") != text.Length - 1)
			{
				return caption + ": ";
			}
			if (caption.LastIndexOf(" ") == caption.Length - 1)
			{
				return caption;
			}
			return caption += " ";
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0001533C File Offset: 0x0001433C
		public static string GetDefaultDataString(Control c, object dataValue, Type dataType)
		{
			string text;
			if (c.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)c.Tag;
				text = dataRow[3].ToString();
			}
			else
			{
				text = "";
			}
			string a = (dataValue == null) ? "" : dataValue.ToString();
			if (dataType == typeof(bool))
			{
				if (!(bool)dataValue)
				{
					return "";
				}
				return text;
			}
			else if (dataType == typeof(DateTime))
			{
				if (dataValue != null)
				{
					return TemplatesClass.FormatCaption(text) + ((DateTime)dataValue).ToString("yyyy-MM-dd");
				}
				return "";
			}
			else if (dataType == typeof(string))
			{
				if (!(a == ""))
				{
					return TemplatesClass.FormatCaption(text) + (string)dataValue;
				}
				return "";
			}
			else
			{
				if (!(a == ""))
				{
					return TemplatesClass.FormatCaption(text) + dataValue.ToString();
				}
				return "";
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00015430 File Offset: 0x00014430
		public static ArrayList FillInCodes2(TemplateCodeGroupCollection codes, TemplateCodeGroupCollection explicitCodeLookupValues, Control p_data, DataTable dynamicScreenNonDataControlsTable)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in codes)
			{
				TemplateCodeGroup templateCodeGroup = (TemplateCodeGroup)obj;
				foreach (object obj2 in templateCodeGroup.SubCodes)
				{
					TemplateCode templateCode = (TemplateCode)obj2;
					if (!templateCode.IgnoreForFillCodes)
					{
						TemplateCode templateCode2 = explicitCodeLookupValues.FindTemplateCode(templateCode.CodeName_lcase);
						if (templateCode2 == null)
						{
							string text = "";
							object obj3 = null;
							string controlName;
							if (templateCode.CodeName_lcase.Length > 0 && templateCode.CodeName_lcase[0] == '.')
							{
								string[] array = templateCode.CodeName_lcase.Substring(1).Split(new char[]
								{
									'.'
								});
								controlName = array[0];
								if (array.Length == 3)
								{
									obj3 = array[1];
									text = array[2];
								}
								else
								{
									obj3 = "~x~";
									text = "~p~";
								}
							}
							else
							{
								controlName = templateCode.CodeName_lcase;
							}
							Control control = TemplatesClass.GetControl2(p_data, controlName, true, dynamicScreenNonDataControlsTable);
							if (control == null)
							{
								control = TemplatesClass.GetControl2(p_data, controlName, false, dynamicScreenNonDataControlsTable);
							}
							if (control != null)
							{
								Type typeFromHandle;
								object controlValue = TemplatesClass.GetControlValue(control, false, out typeFromHandle, templateCode, templateCodeGroup);
								if (obj3 == null)
								{
									templateCode.CodeValue = controlValue;
									templateCode.CodeDataType = typeFromHandle;
								}
								else
								{
									typeFromHandle = typeof(string);
									templateCode.CodeDataType = typeFromHandle;
									if (controlValue == null)
									{
										templateCode.CodeValue = text;
									}
									else if (controlValue is bool)
									{
										templateCode.CodeValue = (((bool)controlValue) ? obj3 : text);
									}
									else if (controlValue is string)
									{
										string text2 = (string)controlValue;
										if (text2.Trim().Length < 1)
										{
											templateCode.CodeValue = text;
										}
										else
										{
											templateCode.CodeValue = obj3;
										}
									}
									else
									{
										templateCode.CodeValue = obj3;
									}
								}
							}
							else
							{
								templateCode.CodeValue = text;
								templateCode.CodeDataType = typeof(string);
								arrayList.Add(templateCode);
							}
						}
						else
						{
							templateCode.CodeValue = templateCode2.CodeValue;
							templateCode.CodeDataType = templateCode2.CodeDataType;
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001569C File Offset: 0x0001469C
		public static string ExtractCids(ArrayList templateCodes, int studentPid, UnivDataAdapter da)
		{
			string text = "";
			foreach (object obj in templateCodes)
			{
				TemplateCode templateCode = (TemplateCode)obj;
				if (templateCode.CodeName.Length > 0)
				{
					char c = templateCode.CodeName[0];
					string text2;
					if (c == '.')
					{
						text2 = templateCode.CodeName.Substring(1);
					}
					else
					{
						text2 = templateCode.CodeName;
					}
					if (text2[text2.Length - 1] == '.')
					{
						text2 = text2.Substring(text2.Length - 1);
					}
					if (text2.Length > 0 && char.IsDigit(text2[0]))
					{
						templateCode.AddAlias(text2);
						if (text.Length > 0)
						{
							text += ",";
						}
						text += text2;
					}
				}
			}
			return text;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00015794 File Offset: 0x00014794
		private static Control GetControl2(Control parent, string ControlName, bool ignoreLabel, DataTable dynamicScreenNonDataControlsTable)
		{
			string text = ControlName.ToUpper().Trim().Replace(":", "");
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.Controls.Count > 0)
				{
					Control control2 = TemplatesClass.GetControl2(control, text, ignoreLabel, dynamicScreenNonDataControlsTable);
					if (control2 != null)
					{
						return control2;
					}
				}
				if (control.Tag is DataRow)
				{
					DataRow dataRow = (DataRow)control.Tag;
					if (dataRow.Table.Columns.Contains("controlcode"))
					{
						int controlCode = (int)dataRow[2];
						int num = (dataRow["controlid"] == DBNull.Value) ? -1 : ((int)dataRow["controlid"]);
						if (TemplatesClass.IsControlCodeDataHolding(dynamicScreenNonDataControlsTable, controlCode) || (!ignoreLabel && control is Label))
						{
							string text2 = dataRow[3].ToString().Trim().ToUpper();
							text2 = text2.Replace(":", "");
							if (text2.CompareTo(text) == 0 || num.ToString().CompareTo(text) == 0)
							{
								return control;
							}
						}
					}
				}
				else if (control is Label && !ignoreLabel)
				{
					string text3 = control.Text.ToUpper().Trim().Replace(":", "");
					if (text3.CompareTo(text) == 0)
					{
						return control;
					}
				}
			}
			return null;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0001594C File Offset: 0x0001494C
		private static bool IsControlCodeDataHolding(DataTable dynamicScreenNonDataControlsTable, int ControlCode)
		{
			foreach (object obj in dynamicScreenNonDataControlsTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[1];
				if (num == ControlCode)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000159BC File Offset: 0x000149BC
		public static void CreateWordDefaultTemplate(string defaultTemplateFilename, string templateFilename, TemplateCodeGroupCollection codes)
		{
			string text = "#<";
			string text2 = ">#";
			string text3 = "[" + '\t' + "• ]";
			string text4 = "[";
			string text5 = "]";
			string text6 = "[\\n{\\n}]";
			string.Concat('\r');
			object value = Missing.Value;
			object obj = "\\endofdoc";
			object obj2 = false;
			object obj3 = true;
			try
			{
				_Application application = new ApplicationClass();
				application.Visible = false;
				object obj4 = (defaultTemplateFilename != null && defaultTemplateFilename.Length > 0 && File.Exists(defaultTemplateFilename)) ? defaultTemplateFilename : null;
				if (obj4 != null)
				{
					File.Copy(defaultTemplateFilename, templateFilename, false);
				}
				else
				{
					File.Create(templateFilename);
				}
				obj4 = templateFilename;
				_Document document = application.Documents.Open(ref obj4, ref value, ref obj2, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value, ref value);
				Range range = document.Bookmarks.Item(ref obj).Range;
				bool flag = false;
				for (int i = 0; i < codes.Count; i++)
				{
					TemplateCodeGroup templateCodeGroup = codes[i];
					for (int j = 0; j < templateCodeGroup.SubCodes.Count; j++)
					{
						TemplateCode templateCode = templateCodeGroup.SubCodes[j];
						if (templateCode.ControlCode == 30)
						{
							int num = i + 1;
							TemplateCodeGroup templateCodeGroup2 = (num < codes.Count) ? codes[num] : null;
							if (templateCodeGroup2 != null && templateCodeGroup2.SubCodes.Count > 0 && templateCodeGroup2.SubCodes[0].ControlCode == 5)
							{
								TemplateCode templateCode2 = templateCodeGroup2.SubCodes[0];
								range.InsertAfter(string.Concat(new string[]
								{
									text,
									text3,
									templateCode2.CodeName,
									text6,
									text2
								}));
								range = document.Bookmarks.Item(ref obj).Range;
								flag = true;
							}
						}
						else if (templateCode.ControlCode == 31)
						{
							flag = false;
						}
						else if (!flag && templateCode.IsDataHolding)
						{
							range.InsertAfter(string.Concat(new string[]
							{
								text,
								text4,
								templateCode.CodeName,
								": ",
								text5,
								templateCode.CodeName,
								text6,
								text2
							}));
							range = document.Bookmarks.Item(ref obj).Range;
						}
					}
				}
				document.Save();
				document.Close(ref obj3, ref value, ref value);
				application.Quit(ref obj3, ref value, ref value);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x040000E7 RID: 231
		private static string NewLine = string.Concat('\r');

		// Token: 0x02000036 RID: 54
		private struct ControlCodes
		{
			// Token: 0x040000E8 RID: 232
			public const int _textBox = 1;

			// Token: 0x040000E9 RID: 233
			public const int _checkBox = 2;

			// Token: 0x040000EA RID: 234
			public const int _comboBox = 3;

			// Token: 0x040000EB RID: 235
			public const int _radioButton = 4;

			// Token: 0x040000EC RID: 236
			public const int _label = 5;

			// Token: 0x040000ED RID: 237
			public const int _date = 6;

			// Token: 0x040000EE RID: 238
			public const int _time = 7;

			// Token: 0x040000EF RID: 239
			public const int _horizontalRule = 8;

			// Token: 0x040000F0 RID: 240
			public const int _blankSpace = 9;

			// Token: 0x040000F1 RID: 241
			public const int _listView = 10;

			// Token: 0x040000F2 RID: 242
			public const int _myCheckBox = 12;

			// Token: 0x040000F3 RID: 243
			public const int _myTextBox = 11;

			// Token: 0x040000F4 RID: 244
			public const int _indent = 13;

			// Token: 0x040000F5 RID: 245
			public const int _radioGroup = 14;

			// Token: 0x040000F6 RID: 246
			public const int _panelStart = 30;

			// Token: 0x040000F7 RID: 247
			public const int _panelClose = 31;

			// Token: 0x040000F8 RID: 248
			public const int _tabControlStart = 32;

			// Token: 0x040000F9 RID: 249
			public const int _tabPageStart = 33;

			// Token: 0x040000FA RID: 250
			public const int _tabPageClose = 34;

			// Token: 0x040000FB RID: 251
			public const int _tabControlClose = 35;

			// Token: 0x040000FC RID: 252
			public const int _columnBreak = 50;

			// Token: 0x040000FD RID: 253
			public const int _staffComboBox = 100;

			// Token: 0x040000FE RID: 254
			public const int _schoolYearChooser = 200;

			// Token: 0x040000FF RID: 255
			public const int VerticalPadTextBox = 6;

			// Token: 0x04000100 RID: 256
			public const int VerticalPadComboBox = 6;

			// Token: 0x04000101 RID: 257
			public const int VerticalPadRadioButton = 2;

			// Token: 0x04000102 RID: 258
			public const int VerticalPadCheckBox = 2;

			// Token: 0x04000103 RID: 259
			public const int VerticalPadLabel = 2;

			// Token: 0x04000104 RID: 260
			public const int VerticalPadDateTimePicker = 4;
		}
	}
}
