using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000002 RID: 2
	public class TemplateInDatabase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public string Name
		{
			get
			{
				string text;
				if (this.fileName != null)
				{
					text = Path.GetFileNameWithoutExtension(this.fileName);
				}
				else if (this.dr != null)
				{
					string text2 = (string)this.dr[TemplateInDatabase.TEMPLATE_NAME_TITLE_COL_NAME];
					int num = text2.IndexOf('_');
					if (num > 0 && num < text2.Length - 1)
					{
						text2 = text2.Substring(num + 1);
					}
					text = text2;
				}
				else
				{
					text = "Blank";
				}
				if (!string.IsNullOrEmpty(text))
				{
					int num2 = 0;
					for (int i = 0; i < text.Length; i++)
					{
						num2 = i;
						if (text[num2] != '_')
						{
							break;
						}
					}
					return text.Substring(num2);
				}
				return "";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F7 File Offset: 0x000010F7
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FF File Offset: 0x000010FF
		public TemplateInDatabaseListDialog.DateType ListSelectionType
		{
			get
			{
				return this.listSelectionType;
			}
			set
			{
				this.listSelectionType = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00001108
		public string TemplateNameWithPrefix
		{
			get
			{
				if (this.dr != null)
				{
					return this.dr["efrom"].ToString();
				}
				return "";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212D File Offset: 0x0000112D
		public int TemplateId
		{
			get
			{
				if (this.dr != null)
				{
					return (int)this.dr["templateid"];
				}
				return 0;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214E File Offset: 0x0000114E
		public TemplateInDatabase(DataRow dr)
		{
			this.dr = dr;
			this.fileName = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00001164
		public TemplateInDatabase(string fileName)
		{
			this.fileName = fileName;
			this.dr = null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217A File Offset: 0x0000117A
		public TemplateInDatabase()
		{
			this.fileName = null;
			this.dr = null;
			this.isEmpty = true;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002197 File Offset: 0x00001197
		public bool IsEmpty
		{
			get
			{
				return this.isEmpty;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000011A0
		public static TemplateInDatabaseCollection GetAvailableTemplates(UnivDataAdapter da, string prefixGroup)
		{
			da.SelectCommand.CommandText = "SELECT * FROM emailtemplates WHERE efrom LIKE @prefixGroup ORDER BY efrom";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@prefixGroup", prefixGroup + "_%");
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				return new TemplateInDatabaseCollection(new Exception(text));
			}
			TemplateInDatabaseCollection templateInDatabaseCollection = new TemplateInDatabaseCollection();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				templateInDatabaseCollection.Add(new TemplateInDatabase(dataRow));
			}
			return templateInDatabaseCollection;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002278 File Offset: 0x00001278
		public static TemplateInDatabaseCollection GetAvailableTemplatesNoFiles(UnivDataAdapter da, string prefixGroup)
		{
			da.SelectCommand.CommandText = "SELECT templateid,templategroupid,templatename,efrom,eto,ecc,ebcc,eattachments,ebody,ebodypdf,emode,blankreplacements,warningifmissingcodes,errorifmissingcodes,whocreated,datecreated,wholastmodified,datelastmodified,isactive FROM emailtemplates WHERE isactive=1 AND efrom LIKE @prefixGroup ORDER BY efrom";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@prefixGroup", prefixGroup + "_%");
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				return new TemplateInDatabaseCollection(new Exception(text));
			}
			TemplateInDatabaseCollection templateInDatabaseCollection = new TemplateInDatabaseCollection();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				templateInDatabaseCollection.Add(new TemplateInDatabase(dataRow));
			}
			templateInDatabaseCollection.Sort();
			return templateInDatabaseCollection;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002354 File Offset: 0x00001354
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000235C File Offset: 0x0000135C
		public DateTime SpecificDate
		{
			get
			{
				return this.specificDate;
			}
			set
			{
				this.specificDate = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002365 File Offset: 0x00001365
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000236D File Offset: 0x0000136D
		public string Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				this.sort = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002376 File Offset: 0x00001376
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000237E File Offset: 0x0000137E
		public bool IncludeCancelledAndNoshow
		{
			get
			{
				return this.includeCancelledAndNoshow;
			}
			set
			{
				this.includeCancelledAndNoshow = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002388 File Offset: 0x00001388
		public static TemplateInDatabase AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType templateDialogType, UnivDataAdapter da, string prefixGroup, string defaultSelected, string title, string captionMessage, bool canModifyTemplates, DataTable t_forSorting)
		{
			return TemplateInDatabase.AskUserWhichTemplate(templateDialogType, da, prefixGroup, defaultSelected, title, captionMessage, canModifyTemplates, t_forSorting, false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023A8 File Offset: 0x000013A8
		public static TemplateInDatabase AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType templateDialogType, UnivDataAdapter da, string prefixGroup, string defaultSelected, string title, string captionMessage, bool canModifyTemplates, DataTable t_forSorting, bool allowUserToChooseJustExportToExcel)
		{
			return TemplateInDatabase.AskUserWhichTemplate(templateDialogType, da, prefixGroup, defaultSelected, title, captionMessage, canModifyTemplates, t_forSorting, allowUserToChooseJustExportToExcel, false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023CC File Offset: 0x000013CC
		public static TemplateInDatabase AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType templateDialogType, UnivDataAdapter da, string prefixGroup, string defaultSelected, string title, string captionMessage, bool canModifyTemplates, DataTable t_forSorting, bool allowUserToChooseJustExportToExcel, bool allowUserToChooseUseWhatISelectedOnTheTestBookingsList)
		{
			return TemplateInDatabase.AskUserWhichTemplate(templateDialogType, da, prefixGroup, defaultSelected, title, captionMessage, canModifyTemplates, t_forSorting, allowUserToChooseJustExportToExcel, allowUserToChooseUseWhatISelectedOnTheTestBookingsList, false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F0 File Offset: 0x000013F0
		public static TemplateInDatabase AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType templateDialogType, UnivDataAdapter da, string prefixGroup, string defaultSelected, string title, string captionMessage, bool canModifyTemplates, DataTable t_forSorting, bool allowUserToChooseJustExportToExcel, bool allowUserToChooseUseWhatISelectedOnTheTestBookingsList, bool forEditingTemplatesOnly)
		{
			return TemplateInDatabase.AskUserWhichTemplate(templateDialogType, da, prefixGroup, defaultSelected, title, captionMessage, canModifyTemplates, t_forSorting, allowUserToChooseJustExportToExcel, allowUserToChooseUseWhatISelectedOnTheTestBookingsList, forEditingTemplatesOnly, null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002418 File Offset: 0x00001418
		public static TemplateInDatabase AskUserWhichTemplate(TemplateInDatabase.TemplateDialogType templateDialogType, UnivDataAdapter da, string prefixGroup, string defaultSelected, string title, string captionMessage, bool canModifyTemplates, DataTable t_forSorting, bool allowUserToChooseJustExportToExcel, bool allowUserToChooseUseWhatISelectedOnTheTestBookingsList, bool forEditingTemplatesOnly, GetCodesHandler getCodesHandler)
		{
			TemplateInDatabaseListDialog templateInDatabaseListDialog = new TemplateInDatabaseListDialog(da, templateDialogType, prefixGroup, null, title, captionMessage, defaultSelected, canModifyTemplates, t_forSorting);
			if (forEditingTemplatesOnly)
			{
				templateInDatabaseListDialog.Button_ChooseFile_Visible = false;
				templateInDatabaseListDialog.Button_SelectTemplate_Visible = false;
				templateInDatabaseListDialog.CancelButtonText = "&Close";
			}
			if (getCodesHandler != null)
			{
				templateInDatabaseListDialog.OnCodesRequested += getCodesHandler;
			}
			if (allowUserToChooseJustExportToExcel)
			{
				templateInDatabaseListDialog.ShowExportToExcelButton();
			}
			if (allowUserToChooseUseWhatISelectedOnTheTestBookingsList)
			{
				templateInDatabaseListDialog.TurnOnOptionToUseWhatISelectedOnTheTestBookingsList();
			}
			DialogResult dialogResult = templateInDatabaseListDialog.ShowDialog();
			if (dialogResult != DialogResult.OK)
			{
				return null;
			}
			if (allowUserToChooseJustExportToExcel && templateInDatabaseListDialog.UserChoseExportToExcel)
			{
				return new TemplateInDatabase
				{
					ListSelectionType = templateInDatabaseListDialog.TypeOfDate,
					IncludeCancelledAndNoshow = templateInDatabaseListDialog.IncludeCancelledAndNoshow,
					SpecificDate = templateInDatabaseListDialog.GetSpecificDate()
				};
			}
			TemplateInDatabase selectedTemplate = templateInDatabaseListDialog.SelectedTemplate;
			if (templateDialogType == TemplateInDatabase.TemplateDialogType.Tests)
			{
				selectedTemplate.Sort = templateInDatabaseListDialog.GetSort();
				selectedTemplate.SpecificDate = templateInDatabaseListDialog.GetSpecificDate();
				selectedTemplate.ListSelectionType = templateInDatabaseListDialog.TypeOfDate;
				selectedTemplate.IncludeCancelledAndNoshow = templateInDatabaseListDialog.IncludeCancelledAndNoshow;
			}
			return selectedTemplate;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000014F8
		public static TemplateInDatabase LoadTemplate(UnivDataAdapter da, string templateNameWithPrefix)
		{
			da.SelectCommand.CommandText = "SELECT templateid FROM emailtemplates WHERE efrom=@tname";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@tname", templateNameWithPrefix);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
			{
				return TemplateInDatabase.LoadTemplate(da, (int)dataTable.Rows[0][0]);
			}
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002578 File Offset: 0x00001578
		public static TemplateInDatabase LoadTemplate(UnivDataAdapter da, int templateId)
		{
			da.SelectCommand.CommandText = "SELECT * FROM emailtemplates WHERE templateid=@id";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@id", templateId);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			if (text != null && text.Length > 0)
			{
				return null;
			}
			return new TemplateInDatabase(dataTable.Rows[0]);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025F0 File Offset: 0x000015F0
		public void ResetTemplateFile()
		{
			this.dr = null;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025FC File Offset: 0x000015FC
		public string FilenameDontAskUsingOpenFileDialog
		{
			get
			{
				if (this.dr != null)
				{
					return TemplateInDatabase.RetrieveTemplateFile(this.dr, true);
				}
				if (this.fileName != null)
				{
					string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(this.fileName));
					File.Copy(this.fileName, tempFilename, true);
					return tempFilename;
				}
				string tempFilename2 = TemplatesClass.GetTempFilename(".txt");
				File.WriteAllText(tempFilename2, "");
				return tempFilename2;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002660 File Offset: 0x00001660
		public string Filename
		{
			get
			{
				if (this.dr != null)
				{
					return TemplateInDatabase.RetrieveTemplateFile(this.dr);
				}
				if (this.fileName != null)
				{
					string tempFilename = TemplatesClass.GetTempFilename(Path.GetExtension(this.fileName));
					File.Copy(this.fileName, tempFilename, true);
					return tempFilename;
				}
				string tempFilename2 = TemplatesClass.GetTempFilename(".txt");
				File.WriteAllText(tempFilename2, "");
				return tempFilename2;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026C0 File Offset: 0x000016C0
		public static string RetrieveTemplateFile(TemplateInDatabase template)
		{
			return TemplateInDatabase.RetrieveTemplateFile(template.dr);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026CD File Offset: 0x000016CD
		public static string RetrieveTemplateFile(DataRow dr)
		{
			return TemplateInDatabase.RetrieveTemplateFile(dr, false);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D8 File Offset: 0x000016D8
		public static string RetrieveTemplateFile(DataRow dr, bool dontAskUserForReplaceFileIfMissing)
		{
			int num = (int)dr["templateid"];
			string base64Text = (string)dr["emisc"];
			string text = (dr.Table.Columns.Contains("ebodypdf") && dr["ebodypdf"] != DBNull.Value) ? ((string)dr["ebodypdf"]) : "";
			return BinaryFile.CreateTemporaryFile(text, base64Text, dontAskUserForReplaceFileIfMissing);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002754 File Offset: 0x00001754
		public string RetrieveEmailTemplateTextRaw()
		{
			string result;
			try
			{
				if (this.dr != null)
				{
					int num = (int)this.dr["templateid"];
					string text = (string)this.dr["emisc"];
					if (this.dr.Table.Columns.Contains("ebodypdf") && this.dr["ebodypdf"] != DBNull.Value)
					{
						string text2 = (string)this.dr["ebodypdf"];
					}
					if (text.Length > 0)
					{
						byte[] bytes = Convert.FromBase64String(text);
						string @string = Encoding.ASCII.GetString(bytes);
						result = @string;
					}
					else
					{
						result = "";
					}
				}
				else
				{
					result = "";
				}
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000282C File Offset: 0x0000182C
		public string RetrieveEmailTemplateText()
		{
			string result;
			try
			{
				if (this.dr != null)
				{
					int num = (int)this.dr["templateid"];
					string text = (string)this.dr["emisc"];
					if (this.dr.Table.Columns.Contains("ebodypdf") && this.dr["ebodypdf"] != DBNull.Value)
					{
						string text2 = (string)this.dr["ebodypdf"];
					}
					if (text.Length > 0)
					{
						byte[] bytes = Convert.FromBase64String(text);
						string text3 = Encoding.ASCII.GetString(bytes);
						text3 = text3.Replace("#~", "#<").Replace("~#", ">#");
						result = text3;
					}
					else
					{
						result = "";
					}
				}
				else
				{
					result = "";
				}
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002924 File Offset: 0x00001924
		public static int CreateNewTemplate(UnivDataAdapter da, string titleWithPrefix)
		{
			if (titleWithPrefix == null || titleWithPrefix.Trim().Length <= 0)
			{
				return 0;
			}
			string commandText = "IF NOT EXISTS(SELECT templateid FROM emailtemplates WHERE efrom=@title)\r\nBEGIN\r\n    INSERT INTO emailtemplates (efrom,eto,ecc,ebcc,eattachments,ebody,emisc) VALUES (@title,'','','','','','')\r\n    SELECT CAST(SCOPE_IDENTITY() as int)\r\nEND\r\nELSE\r\nBEGIN\r\n    SELECT templateid FROM emailtemplates WHERE efrom=@title\r\nEND";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@title", titleWithPrefix);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
			{
				return (int)dataTable.Rows[0][0];
			}
			return 0;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029D4 File Offset: 0x000019D4
		public static Exception ReplaceTemplateFileWithText(UnivDataAdapter da, int templateId, string text, string extension)
		{
			string tempFilename = TemplatesClass.GetTempFilename(extension);
			File.WriteAllText(tempFilename, text);
			return TemplateInDatabase.ReplaceTemplateFile(da, templateId, tempFilename);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029F8 File Offset: 0x000019F8
		public static Exception ReplaceTemplateFile(UnivDataAdapter da, int templateId, string fileName)
		{
			string parameterValue = BinaryFile.ConvertFileToBase64Text(fileName);
			da.SelectCommand.CommandText = "UPDATE emailtemplates SET emisc=@emisc,ebodypdf=@fn WHERE templateid=@templateid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@emisc", parameterValue);
			da.SelectCommand.Parameters.Add("@templateid", templateId);
			da.SelectCommand.Parameters.Add("@fn", Path.GetFileName(fileName));
			string text;
			da.Fill(new DataTable(), out text);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Exception(text);
		}

		// Token: 0x04000001 RID: 1
		public static string TEMPLATE_NAME_TITLE_COL_NAME = "efrom";

		// Token: 0x04000002 RID: 2
		private DataRow dr;

		// Token: 0x04000003 RID: 3
		private string fileName;

		// Token: 0x04000004 RID: 4
		private TemplateInDatabaseListDialog.DateType listSelectionType;

		// Token: 0x04000005 RID: 5
		private bool isEmpty;

		// Token: 0x04000006 RID: 6
		private DateTime specificDate;

		// Token: 0x04000007 RID: 7
		private string sort;

		// Token: 0x04000008 RID: 8
		private bool includeCancelledAndNoshow;

		// Token: 0x02000003 RID: 3
		public enum TemplateDialogType
		{
			// Token: 0x0400000A RID: 10
			Generic,
			// Token: 0x0400000B RID: 11
			Tests,
			// Token: 0x0400000C RID: 12
			Accommodations,
			// Token: 0x0400000D RID: 13
			Email
		}
	}
}
