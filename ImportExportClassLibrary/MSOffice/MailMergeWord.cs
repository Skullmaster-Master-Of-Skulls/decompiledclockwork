using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MailMerging;
using Spire.Doc;
using Spire.Doc.Documents;

namespace ImportExportClassLibrary.MSOffice
{
	// Token: 0x02000038 RID: 56
	public class MailMergeWord
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00015D01 File Offset: 0x00014D01
		public static List<MailMergeCode> ExtractMailMergeCodes(string filename)
		{
			return MailMergeWord.ExtractMailMergeCodes(filename, null);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00015D0C File Offset: 0x00014D0C
		public static List<MailMergeCode> ExtractMailMergeCodes(string filename, string base64)
		{
			LicenseProvider.Register("TechnoPro Computer Solutions, Inc.", "LnTchk/c5BSbDo0OBQsod+tvC4obtOiMQzzMP76h0VxH3bPwamkzFzetBo4sMHdF");
			Document document;
			if (base64 == null)
			{
				document = new Document(filename);
			}
			else
			{
				byte[] buffer = Convert.FromBase64String(base64);
				MemoryStream stream = new MemoryStream(buffer);
				document = new Document(stream);
			}
			List<MailMergeCode> list = new List<MailMergeCode>();
			TextSelection[] array = document.FindAllPattern(new Regex("#\\<[^\\>#]+\\>#"));
			if (array != null)
			{
				foreach (TextSelection textSelection in array)
				{
					string selectedText = textSelection.SelectedText;
					if (selectedText != null && selectedText.Length > 4)
					{
						MailMergeCode item = new MailMergeCode(selectedText.Substring(2, selectedText.Length - 4));
						list.Add(item);
					}
				}
			}
			document.Close();
			return list;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00015DC3 File Offset: 0x00014DC3
		public static string MailMergeInfo(string templateFilename, List<List<MailMergeCodeValue>> pages)
		{
			return MailMergeWord.MailMergeInfo(templateFilename, pages, null);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00015DD0 File Offset: 0x00014DD0
		public static string MailMergeInfo(string templateFilename, List<List<MailMergeCodeValue>> pages, MailMergeWord.ProgressChangedHandler progressChanged)
		{
			MailMergeWord.CancelMailMerge = false;
			LicenseProvider.Register("TechnoPro Computer Solutions, Inc.", "LnTchk/c5BSbDo0OBQsod+tvC4obtOiMQzzMP76h0VxH3bPwamkzFzetBo4sMHdF");
			Document document = new Document();
			Document document2 = new Document();
			string text = Path.GetExtension(templateFilename).ToLower();
			FileFormat fileFormat;
			if (text.Equals(".docx"))
			{
				fileFormat = FileFormat.Docx;
			}
			else if (text.Equals(".rtf"))
			{
				fileFormat = FileFormat.PDF;
			}
			else
			{
				fileFormat = FileFormat.Doc;
			}
			document2.LoadFromFileInReadMode(templateFilename, fileFormat);
			int num;
			if (pages.Count > 0)
			{
				num = Convert.ToInt32(Convert.ToDouble(pages.Count) / 100.0);
				if (num < 1)
				{
					num = 1;
				}
			}
			else
			{
				num = 1;
			}
			for (int i = 0; i < pages.Count; i++)
			{
				if (MailMergeWord.CancelMailMerge)
				{
					MailMergeWord.CancelMailMerge = false;
					break;
				}
				List<MailMergeCodeValue> list = pages[i];
				Document document3 = document2.Clone();
				foreach (MailMergeCodeValue mailMergeCodeValue in list)
				{
					string matchString = string.Format("#<{0}>#", mailMergeCodeValue.CodeName);
					document3.Replace(matchString, mailMergeCodeValue.Value.ValueToString, false, true);
				}
				document.ImportContent(document3);
				if (progressChanged != null && i % num == 0)
				{
					try
					{
						progressChanged(Convert.ToInt32(Convert.ToDouble(i) / (double)pages.Count * 100.0));
					}
					catch
					{
					}
				}
			}
			string tempFilename = TemplatesClass.GetTempFilename(text);
			document.SaveToFile(tempFilename);
			return tempFilename;
		}

		// Token: 0x04000107 RID: 263
		public static bool CancelMailMerge;

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x06000208 RID: 520
		public delegate void ProgressChangedHandler(int currentPercent);
	}
}
