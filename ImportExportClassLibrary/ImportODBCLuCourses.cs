using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using AutoComboBox;
using EncryptionClassLibrary;
using SettingsPermissions;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x0200002D RID: 45
	public class ImportODBCLuCourses : ImportODBC
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000A19C File Offset: 0x0000919C
		public ImportODBCLuCourses(Form ParentForm, Settings settings, DateTime _SessionStartDate, DateTime _SessionEndDate, UnivDataAdapter _ClockWorkDa, TripleDESEncryptionClass _TripleDES, string _ConnectionString, string ImportSQL, string UpdateSQL, NameValueCollection Parameters) : base(ParentForm, settings, _SessionStartDate, _SessionEndDate, _ClockWorkDa, _TripleDES, _ConnectionString, ImportSQL, UpdateSQL, Parameters)
		{
			Type type = Type.GetType("System.String");
			Type type2 = Type.GetType("System.Int32");
			Type.GetType("System.Boolean");
			Type type3 = Type.GetType("System.DateTime");
			byte[] array = new byte[0];
			array.GetType();
			this._internal_luCourses = new DataTable();
			this._internal_luCourses.Columns.Add("lucourseid", type2);
			this._internal_luCourses.Columns.Add("startdate", type3);
			this._internal_luCourses.Columns.Add("enddate", type3);
			this._internal_luCourses.Columns.Add("term", type);
			this._internal_luCourses.Columns.Add("duration", type);
			this._internal_luCourses.Columns.Add("subjectid", type2);
			this._internal_luCourses.Columns.Add("course", type);
			this._internal_luCourses.Columns.Add("timeofday", type);
			this._internal_luCourses.Columns.Add("section", type);
			this._internal_luCourses.Columns.Add("instructorid", type2);
			this._internal_luCourses.Columns.Add("crosslistcode", type2);
			this._internal_luCourses.Columns.Add("equivalentcode", type2);
			this._internal_luCourses.Columns.Add("coursenote", type);
			this._internal_luCourses.Columns.Add("subject1", type);
			this._internal_luCourses.Columns.Add("subject2", type);
			this._internal_luCourses.Columns.Add("prof1", type);
			this._internal_luCourses.Columns.Add("prof2", type);
			this._internal_luCourseData = new DataTable();
			this._internal_luCourseData.Columns.Add("lucoursedataid", type2);
			this._internal_luCourseData.Columns.Add("lookuplisttype", type2);
			this._internal_luCourseData.Columns.Add("lookupstring", type);
			this._internal_luCourseData.Columns.Add("altlookupstring", type);
			this._internal_luCourseData.Columns.Add("email", type);
			this._internal_luCourseData.Columns.Add("phone", type);
			this.showDuration = Settings.IntToBool(settings.GetSetting(253));
			this.showTimeOfDay = Settings.IntToBool(settings.GetSetting(254));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A474 File Offset: 0x00009474
		public override ImportProblem[] ImportItem(ImportItem ii)
		{
			if (ii.imported)
			{
				return ii._ImportProblems;
			}
			DataRow dataRow = ii._dataRow;
			ArrayList arrayList = new ArrayList();
			if (ii.internalRows == null)
			{
				DataRow[] internalRows = new DataRow[3];
				ii.internalRows = internalRows;
			}
			this.ImportItemLuCourse(ii, ref arrayList);
			if (arrayList.Count <= 0)
			{
				ii.imported = true;
				return null;
			}
			ImportProblem[] array = new ImportProblem[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A4E4 File Offset: 0x000094E4
		private void ImportItemLuCourse(ImportItem ii, ref ArrayList importProblems)
		{
			if (ii.internalRows[0] == null)
			{
				DataRow dataRow = ii._dataRow;
				string text = dataRow[0].ToString();
				string text2 = dataRow[1].ToString();
				bool flag = false;
				DateTime dateTime;
				DateTime dateTime2;
				if (text.Length <= 0 || text2.Length <= 0)
				{
					dateTime = this._sessionStartDate;
					dateTime2 = this._sessionEndDate;
					flag = true;
				}
				else
				{
					try
					{
						dateTime = DateTime.Parse(text);
						dateTime2 = DateTime.Parse(text2);
					}
					catch
					{
						dateTime = this._sessionStartDate;
						dateTime2 = this._sessionEndDate;
						flag = true;
					}
				}
				string text3 = dataRow[2].ToString().Trim().ToLower();
				string text4 = dataRow[3].ToString().Trim().ToLower();
				string parameterValue = dataRow[4].ToString().Trim().ToLower();
				string parameterValue2 = dataRow[5].ToString().Trim().ToLower();
				string parameterValue3 = dataRow[7].ToString().Trim().ToLower();
				string parameterValue4 = dataRow[6].ToString().Trim().ToLower();
				string prof = dataRow[8].ToString().Trim();
				if (flag)
				{
					DateTime now = DateTime.Now;
					DateTime now2 = DateTime.Now;
					if (this.GetPreviouslyConfirmedDates(text3, text4, ref now, ref now2))
					{
						dateTime = now;
						dateTime2 = now2;
					}
					else
					{
						DateRangeInput dateRangeInput = new DateRangeInput(string.Concat(new string[]
						{
							"Please confirm the start and end dates for the duration/term [",
							text4,
							"/",
							text3,
							"]:"
						}), "Please confirm term start/end dates.", dateTime, dateTime2);
						dateRangeInput.ShowEndDateTimePicker();
						dateRangeInput.ShowDialog();
						dateTime = dateRangeInput.StartDate;
						dateTime2 = dateRangeInput.EndDate;
						object[] value = new object[]
						{
							text3,
							text4,
							dateTime,
							dateTime2
						};
						this.userConfirmedDurationTermCombo_dates.Add(value);
					}
					dataRow[0] = dateTime;
					dataRow[1] = dateTime2;
				}
				if (ii.internalRows[0] == null)
				{
					this._clockWorkDa.SelectCommand.CommandText = "SELECT luc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.crosslistcode,luc.equivalentcode,luc.coursenote,lucd.lookupstring AS subject1,lucd.altlookupstring AS subject2, lucd2.lookupstring AS prof1, lucd2.altlookupstring AS prof2 FROM lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE ((@sdate <= luc.startdate AND luc.enddate <= @edate) OR (luc.enddate >= @sdate AND luc.enddate <= @edate) OR (luc.startdate >= @sdate AND luc.startdate <= @edate) OR (luc.startdate <= @sdate AND luc.enddate >= @edate))";
					if (text3.Length > 0)
					{
						UnivCommand selectCommand = this._clockWorkDa.SelectCommand;
						selectCommand.CommandText += " AND luc.term=@term";
					}
					if (this.showDuration)
					{
						UnivCommand selectCommand2 = this._clockWorkDa.SelectCommand;
						selectCommand2.CommandText += " AND luc.duration=@duration";
					}
					UnivCommand selectCommand3 = this._clockWorkDa.SelectCommand;
					selectCommand3.CommandText += " AND luc.subjectid IN (SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND (lookupstring=@subject OR altlookupstring=@subject)) AND luc.course=@course AND luc.section=@section";
					if (this.showTimeOfDay)
					{
						UnivCommand selectCommand4 = this._clockWorkDa.SelectCommand;
						selectCommand4.CommandText += " AND timeofday=@timeofday";
					}
					this._clockWorkDa.SelectCommand.Parameters.Clear();
					this._clockWorkDa.SelectCommand.Parameters.Add("@sdate", dateTime);
					this._clockWorkDa.SelectCommand.Parameters.Add("@edate", dateTime2);
					this._clockWorkDa.SelectCommand.Parameters.Add("@term", text3);
					this._clockWorkDa.SelectCommand.Parameters.Add("@duration", text4);
					this._clockWorkDa.SelectCommand.Parameters.Add("@subject", parameterValue);
					this._clockWorkDa.SelectCommand.Parameters.Add("@course", parameterValue2);
					this._clockWorkDa.SelectCommand.Parameters.Add("@section", parameterValue3);
					this._clockWorkDa.SelectCommand.Parameters.Add("@timeofday", parameterValue4);
					DataTable dataTable = new DataTable();
					this._clockWorkDa.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						ii.internalRows[0] = dataTable.Rows[0];
						this._internal_luCourses.ImportRow(ii.internalRows[0]);
						ii.internalRows[0].AcceptChanges();
					}
				}
				DataRow dataRow2 = ii.internalRows[0];
				if (dataRow2 == null)
				{
					this.AddLuCourse(ii);
					dataRow2 = ii.internalRows[0];
				}
				else
				{
					int instructorId = this.GetInstructorId(ii, prof);
					int num;
					if (dataRow2[9] == DBNull.Value)
					{
						num = 0;
					}
					else
					{
						num = (int)dataRow2[9];
					}
					if (num != instructorId)
					{
						dataRow2[9] = instructorId;
						ii.extraNote = string.Concat(new string[]
						{
							"Prof Changed (",
							num.ToString(),
							"->",
							instructorId.ToString(),
							")"
						});
						ii.bool1 = true;
					}
				}
				string text5 = dataRow[11].ToString().Trim().ToLower();
				if (text5.Length > 0)
				{
					string text6 = this.crossListCodes[text5];
					int num2;
					if (text6 != null && text6.Length > 0)
					{
						try
						{
							num2 = int.Parse(text6);
							goto IL_552;
						}
						catch
						{
							num2 = 0;
							goto IL_552;
						}
					}
					num2 = (int)dataRow2[0];
					this.crossListCodes.Add(text5, num2.ToString());
					IL_552:
					if (num2 != 0)
					{
						int num3;
						if (dataRow2[10] == DBNull.Value)
						{
							num3 = 0;
						}
						else
						{
							num3 = (int)dataRow2[10];
						}
						if (num3 != num2)
						{
							dataRow2[10] = num2;
							ii.bool2 = true;
						}
					}
				}
				if (dataRow2.RowState == DataRowState.Unchanged)
				{
					ii.ignoreThisItem = true;
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000AAB8 File Offset: 0x00009AB8
		private bool GetPreviouslyConfirmedDates(string term, string duration, ref DateTime previouslyConfirmed_sdate, ref DateTime previouslyConfirmed_edate)
		{
			string strB = term.ToLower().Trim();
			string strB2 = duration.ToLower().Trim();
			foreach (object obj in this.userConfirmedDurationTermCombo_dates)
			{
				object[] array = (object[])obj;
				string text = ((string)array[0]).ToLower().Trim();
				if (text.CompareTo(strB) == 0)
				{
					string text2 = ((string)array[1]).Trim().ToLower();
					if (text2.CompareTo(strB2) == 0)
					{
						previouslyConfirmed_sdate = (DateTime)array[2];
						previouslyConfirmed_edate = (DateTime)array[3];
						return true;
					}
				}
			}
			previouslyConfirmed_sdate = DateTime.Now;
			previouslyConfirmed_edate = DateTime.Now;
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000ABA0 File Offset: 0x00009BA0
		public override DataTable GetMainDataTable()
		{
			return this._internal_luCourses;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000ABA8 File Offset: 0x00009BA8
		public override void FillListView(ListView lv)
		{
			lv.BeginUpdate();
			if (lv.Columns.Count <= 0)
			{
				this.AddListViewColumns(lv);
			}
			lv.Items.Clear();
			if (this._importItems != null)
			{
				foreach (ImportItem importItem in this._importItems)
				{
					DataRow dataRow = importItem._dataRow;
					ListViewItem listViewItem = new ListViewItem(dataRow[0].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[1].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[2].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[3].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[4].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[5].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[6].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[7].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[8].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[9].ToString().Trim());
					listViewItem.SubItems.Add(dataRow[10].ToString().Trim());
					listViewItem.SubItems.Add(importItem.extraNote);
					string text = "";
					string text2 = "";
					string text3 = "";
					if (importItem.discarded)
					{
						listViewItem.ImageIndex = 2;
					}
					else if (importItem.imported)
					{
						DataRow dataRow2 = importItem.internalRows[0];
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						if (dataRow2[5] != DBNull.Value)
						{
							int num = (int)dataRow2[5];
							if (num >= 0)
							{
								text = "+" + num.ToString();
							}
							else
							{
								text = num.ToString();
							}
							if (num < 0)
							{
								flag2 = true;
							}
						}
						if (dataRow2[9] != DBNull.Value)
						{
							int num2 = (int)dataRow2[9];
							if (num2 >= 0)
							{
								text2 = "+" + num2.ToString();
							}
							else
							{
								text2 = num2.ToString();
							}
							if (num2 < -1)
							{
								flag = true;
							}
						}
						int num3 = (int)dataRow2[0];
						text3 = num3.ToString();
						if (num3 < 0)
						{
							flag3 = true;
						}
						if (flag3)
						{
							if (flag2 && flag)
							{
								listViewItem.ImageIndex = 10;
							}
							else if (flag2)
							{
								listViewItem.ImageIndex = 11;
							}
							else if (flag)
							{
								listViewItem.ImageIndex = 12;
							}
							else
							{
								listViewItem.ImageIndex = 9;
							}
						}
						else if (flag2 && flag)
						{
							listViewItem.ImageIndex = 13;
						}
						else if (flag2)
						{
							listViewItem.ImageIndex = 14;
						}
						else if (flag)
						{
							listViewItem.ImageIndex = 15;
						}
						else
						{
							listViewItem.ImageIndex = 8;
						}
					}
					else if (importItem._ImportProblems == null)
					{
						listViewItem.ImageIndex = 6;
					}
					else
					{
						listViewItem.ImageIndex = 4;
					}
					listViewItem.SubItems.Add(text);
					listViewItem.SubItems.Add(text2);
					listViewItem.SubItems.Add(text3);
					listViewItem.Tag = importItem;
					lv.Items.Add(listViewItem);
				}
			}
			lv.EndUpdate();
			base.FillListView(lv);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000AF30 File Offset: 0x00009F30
		private void AddListViewColumns(ListView lv)
		{
			int width = 100;
			lv.Columns.Add("StartDate", width, HorizontalAlignment.Left);
			lv.Columns.Add("EndDate", width, HorizontalAlignment.Left);
			lv.Columns.Add("Term", width, HorizontalAlignment.Left);
			lv.Columns.Add("Duration", width, HorizontalAlignment.Left);
			lv.Columns.Add("Subject", width, HorizontalAlignment.Left);
			lv.Columns.Add("Course", width, HorizontalAlignment.Left);
			lv.Columns.Add("TimeOfDay", width, HorizontalAlignment.Left);
			lv.Columns.Add("Section", width, HorizontalAlignment.Left);
			lv.Columns.Add("Instructor", width, HorizontalAlignment.Left);
			lv.Columns.Add("InstructorEmail", width, HorizontalAlignment.Left);
			lv.Columns.Add("InstructorPhone", width, HorizontalAlignment.Left);
			lv.Columns.Add("IMPORTNOTE:", width, HorizontalAlignment.Left);
			lv.Columns.Add("SubjectId", width, HorizontalAlignment.Left);
			lv.Columns.Add("ProfId", width, HorizontalAlignment.Left);
			lv.Columns.Add("LuCourseId", width, HorizontalAlignment.Left);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B060 File Offset: 0x0000A060
		public override HowProblemWasFixed FixProblem(ImportItem importItem, ProblemSolution problemSolution)
		{
			if (problemSolution != ProblemSolution.AddCourse)
			{
				switch (problemSolution)
				{
				case ProblemSolution.Discard:
				case ProblemSolution.Ignore:
					this.DiscardItem(importItem);
					return HowProblemWasFixed.ItemDiscarded;
				default:
					return HowProblemWasFixed.NothingDoneBecauseOfError;
				}
			}
			else
			{
				if (this.AddLuCourse(importItem))
				{
					this.ImportItem(importItem);
					return HowProblemWasFixed.ProblemSolved;
				}
				return HowProblemWasFixed.NothingDoneBecauseOfError;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B0A4 File Offset: 0x0000A0A4
		private int GetSubjectId(ImportItem ii, string subject)
		{
			int num = -1;
			string text = subject.Trim().ToLower();
			foreach (object obj in this._internal_luCourseData.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((int)dataRow[1] == 0)
				{
					string text2 = dataRow[2].ToString().Trim().ToLower();
					string text3 = dataRow[3].ToString().Trim().ToLower();
					if (text2.CompareTo(text) == 0 || text3.CompareTo(text) == 0)
					{
						num = (int)dataRow[0];
					}
				}
			}
			if (num == -1)
			{
				UnivDataAdapter clockWorkDa = this._clockWorkDa;
				clockWorkDa.SelectCommand.CommandText = "SELECT lucoursedataid FROM lucoursedata WHERE lookupstring=@subject OR altlookupstring=@subject";
				clockWorkDa.SelectCommand.Parameters.Clear();
				clockWorkDa.SelectCommand.Parameters.Add("@subject", text);
				DataTable dataTable = new DataTable();
				clockWorkDa.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					num = (int)dataTable.Rows[0][0];
				}
			}
			if (num == -1)
			{
				object[] array = new object[6];
				num = this._newLuCourseDataID--;
				array[0] = num;
				array[1] = 0;
				array[2] = subject;
				array[3] = subject;
				array[4] = "";
				array[5] = "";
				DataRow dataRow2 = this._internal_luCourseData.Rows.Add(array);
				ii.internalRows[1] = dataRow2;
			}
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B260 File Offset: 0x0000A260
		private int GetInstructorId(ImportItem ii, string prof)
		{
			int num = -1;
			string text = prof.Trim().ToLower();
			text = text.Replace(" ", "");
			text = text.Replace(".", " ");
			text = text.Replace("-", " ");
			if (text.Length < 1)
			{
				return -1;
			}
			foreach (object obj in this._internal_luCourseData.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = (int)dataRow[1];
				if (num2 == 1)
				{
					string text2 = dataRow[2].ToString().Trim().ToLower().Replace(" ", "").Replace(".", " ").Replace("-", " ");
					if (text2.CompareTo(text) == 0)
					{
						num = (int)dataRow[0];
					}
					else
					{
						string text3 = dataRow[3].ToString().Trim().ToLower().Replace(" ", "").Replace(".", " ").Replace("-", " ");
						if (text3.CompareTo(text) == 0)
						{
							num = (int)dataRow[0];
						}
					}
				}
			}
			if (num == -1)
			{
				UnivDataAdapter clockWorkDa = this._clockWorkDa;
				clockWorkDa.SelectCommand.CommandText = "SELECT lucoursedataid FROM lucoursedata WHERE REPLACE(REPLACE(REPLACE(lookupstring,' ',''),'.',' '),'-',' ')=@prof OR REPLACE(REPLACE(REPLACE(altlookupstring,' ',''),'.',' '),'-',' ')=@prof";
				clockWorkDa.SelectCommand.Parameters.Clear();
				clockWorkDa.SelectCommand.Parameters.Add("@prof", text);
				DataTable dataTable = new DataTable();
				clockWorkDa.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					num = (int)dataTable.Rows[0][0];
				}
			}
			if (num == -1)
			{
				object[] array = new object[6];
				num = this._newLuCourseDataID--;
				array[0] = num;
				array[1] = 1;
				array[2] = prof;
				array[3] = prof;
				array[4] = "";
				array[5] = "";
				DataRow dataRow2 = this._internal_luCourseData.Rows.Add(array);
				ii.internalRows[2] = dataRow2;
			}
			return num;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B4CC File Offset: 0x0000A4CC
		private bool AddLuCourse(ImportItem ii)
		{
			DataRow dataRow = ii._dataRow;
			string text = dataRow[0].ToString();
			string text2 = dataRow[1].ToString();
			DateTime dateTime;
			DateTime dateTime2;
			if (text.Length <= 0 || text2.Length <= 0)
			{
				dateTime = this._sessionStartDate;
				dateTime2 = this._sessionEndDate;
			}
			else
			{
				try
				{
					dateTime = DateTime.Parse(text);
					dateTime2 = DateTime.Parse(text2);
				}
				catch
				{
					dateTime = this._sessionStartDate;
					dateTime2 = this._sessionEndDate;
				}
			}
			string text3 = dataRow[2].ToString().Trim();
			string text4 = dataRow[3].ToString().Trim();
			string subject = dataRow[4].ToString().Trim();
			string text5 = dataRow[5].ToString().Trim();
			string text6 = dataRow[7].ToString().Trim();
			string text7 = dataRow[6].ToString().Trim();
			string prof = dataRow[8].ToString().Trim();
			int subjectId = this.GetSubjectId(ii, subject);
			int instructorId = this.GetInstructorId(ii, prof);
			object[] array = new object[this._internal_luCourses.Columns.Count];
			array[0] = this._newLuCourseId--;
			array[1] = dateTime;
			array[2] = dateTime2;
			array[3] = text3;
			array[4] = text4;
			array[5] = subjectId;
			array[6] = text5;
			array[7] = text7;
			array[8] = text6;
			array[9] = instructorId;
			array[10] = -1;
			array[11] = -1;
			array[12] = "";
			DataRow dataRow2 = this._internal_luCourses.Rows.Add(array);
			ii.internalRows[0] = dataRow2;
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B6A0 File Offset: 0x0000A6A0
		public override void DiscardItem(ImportItem importItem)
		{
			importItem.discarded = true;
			importItem.imported = true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B6B0 File Offset: 0x0000A6B0
		public override ImportItemStatus ImportOneItem(ImportItem importItem)
		{
			if (importItem._ImportProblems == null)
			{
				DataRow[] internalRows = importItem.internalRows;
				DataRow dataRow = internalRows[0];
				if (dataRow != null)
				{
					int num = (int)dataRow[0];
					importItem.imported = true;
					return ImportItemStatus.ImportedSuccessfully;
				}
			}
			return ImportItemStatus.NotImportedProblemsExist;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B6EC File Offset: 0x0000A6EC
		public override bool Save()
		{
			if (this._importItems != null)
			{
				foreach (ImportItem importItem in this._importItems)
				{
					try
					{
						if (importItem.imported)
						{
							int num = 0;
							if (!importItem.discarded)
							{
								DataRow[] internalRows = importItem.internalRows;
								DataRow dataRow = internalRows[0];
								DataRow dataRow2 = internalRows[1];
								DataRow dataRow3 = internalRows[2];
								if (dataRow != null)
								{
									DataRow dataRow4 = dataRow;
									int num2 = (int)dataRow4[5];
									int num3 = (int)dataRow4[9];
									int num4 = (int)dataRow4[0];
									if (num2 < 0 || num3 < 0 || num4 < 0 || importItem.bool1 || importItem.bool2)
									{
										dataRow4 = dataRow2;
										if (dataRow4 != null && num2 < 0)
										{
											this._clockWorkDa.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (@lookuplisttype,@lookupstring,@altlookupstring,@email,@phone)";
											this._clockWorkDa.SelectCommand.Parameters.Clear();
											this._clockWorkDa.SelectCommand.Parameters.Add("@lookuplisttype", dataRow4.ItemArray[1]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@lookupstring", dataRow4.ItemArray[2]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@altlookupstring", dataRow4.ItemArray[3]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@email", dataRow4.ItemArray[4]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@phone", dataRow4.ItemArray[5]);
											DataTable dataTable = new DataTable();
											this._clockWorkDa.FillReturnIdentity(dataTable, "lucoursedataid", "lucoursedata");
											int num5 = (int)dataTable.Rows[0].ItemArray[0];
											foreach (object obj in this._internal_luCourses.Rows)
											{
												DataRow dataRow5 = (DataRow)obj;
												if (dataRow5.RowState != DataRowState.Deleted)
												{
													int num6 = (int)dataRow5.ItemArray[5];
													if (num6 == num2)
													{
														dataRow5[5] = num5;
													}
												}
											}
											foreach (ImportItem importItem2 in this._importItems)
											{
												DataRow dataRow6 = importItem2.internalRows[0];
												if (dataRow6 != null)
												{
													int num7 = (int)dataRow6[5];
													if (num7 == num2)
													{
														dataRow6[5] = num5;
													}
												}
											}
											dataRow4.AcceptChanges();
										}
										dataRow4 = dataRow3;
										if (dataRow4 != null && num3 < -1)
										{
											this._clockWorkDa.SelectCommand.CommandText = "INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring,email,phone) VALUES (@lookuplisttype,@lookupstring,@altlookupstring,@email,@phone)";
											this._clockWorkDa.SelectCommand.Parameters.Clear();
											this._clockWorkDa.SelectCommand.Parameters.Add("@lookuplisttype", dataRow4.ItemArray[1]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@lookupstring", dataRow4.ItemArray[2]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@altlookupstring", dataRow4.ItemArray[3]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@email", dataRow4.ItemArray[4]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@phone", dataRow4.ItemArray[5]);
											DataTable dataTable = new DataTable();
											this._clockWorkDa.FillReturnIdentity(dataTable, "lucoursedataid", "lucoursedata");
											int num8 = (int)dataTable.Rows[0].ItemArray[0];
											dataRow[9] = num8;
											dataRow4[0] = num8;
											foreach (object obj2 in this._internal_luCourses.Rows)
											{
												DataRow dataRow7 = (DataRow)obj2;
												if (dataRow7.RowState != DataRowState.Deleted)
												{
													int num9 = (int)dataRow7[9];
													if (num9 == num3)
													{
														dataRow7[9] = num8;
													}
												}
											}
											foreach (ImportItem importItem3 in this._importItems)
											{
												DataRow dataRow8 = importItem3.internalRows[0];
												if (dataRow8 != null)
												{
													int num10 = (int)dataRow8[9];
													if (num10 == num3)
													{
														dataRow8[9] = num8;
													}
												}
											}
											dataRow4.AcceptChanges();
										}
										dataRow4 = dataRow;
										if (num4 < 0)
										{
											this._clockWorkDa.SelectCommand.CommandText = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,section,instructorid,crosslistcode,equivalentcode,coursenote) VALUES (@startdate,@enddate,@term,@duration,@subjectid,@course,@timeofday,@section,@instructorid,@crosslistcode,@equivalentcode,@coursenote)";
											this._clockWorkDa.SelectCommand.Parameters.Clear();
											this._clockWorkDa.SelectCommand.Parameters.Add("@startdate", dataRow4.ItemArray[1]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@enddate", dataRow4.ItemArray[2]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@term", dataRow4.ItemArray[3]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@duration", dataRow4.ItemArray[4]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@subjectid", dataRow4.ItemArray[5]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@course", dataRow4.ItemArray[6]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@timeofday", dataRow4.ItemArray[7]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@section", dataRow4.ItemArray[8]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@instructorid", dataRow4.ItemArray[9]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@crosslistcode", dataRow4.ItemArray[10]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@equivalentcode", dataRow4.ItemArray[11]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@coursenote", dataRow4.ItemArray[12]);
											DataTable dataTable = new DataTable();
											this._clockWorkDa.FillReturnIdentity(dataTable, "lucourseid", "lucourses");
											int num11 = (int)dataTable.Rows[0].ItemArray[0];
											int num12 = (int)dataRow4.ItemArray[10];
											dataRow4.AcceptChanges();
											if (dataRow4.ItemArray[10] != DBNull.Value && (int)dataRow4.ItemArray[10] < -1)
											{
												dataRow4[10] = num11;
												dataRow4[0] = num11;
												foreach (object obj3 in this._internal_luCourses.Rows)
												{
													DataRow dataRow9 = (DataRow)obj3;
													if ((dataRow9.RowState == DataRowState.Modified || dataRow9.RowState == DataRowState.Added) && dataRow9.ItemArray[10] != DBNull.Value)
													{
														int num13 = (int)dataRow9.ItemArray[10];
														if (num13 == num12)
														{
															dataRow9[10] = num11;
														}
													}
												}
											}
										}
										if (importItem.bool1 || importItem.bool2)
										{
											this._clockWorkDa.SelectCommand.CommandText = "UPDATE lucourses SET instructorid=@instructorid,crosslistcode=@crosslistcode WHERE lucourseid=@lucourseid";
											this._clockWorkDa.SelectCommand.Parameters.Clear();
											this._clockWorkDa.SelectCommand.Parameters.Add("@instructorid", dataRow4.ItemArray[9]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@crosslistcode", dataRow4.ItemArray[10]);
											this._clockWorkDa.SelectCommand.Parameters.Add("@lucourseid", dataRow4.ItemArray[0]);
											this._clockWorkDa.Fill(new DataTable());
											dataRow4.AcceptChanges();
										}
									}
								}
							}
							if (this._updateSQL.Length > 0)
							{
								this.externalDa.SelectCommand.CommandText = this._updateSQL;
								this.externalDa.SelectCommand.Parameters.Clear();
								this.externalDa.SelectCommand.CommandText = this.FixUpdateString(this.externalDa.SelectCommand.CommandText, importItem._dataRow);
								this.externalDa.SelectCommand.CommandText = this.externalDa.SelectCommand.CommandText.Replace("@clockworkid", num.ToString());
								this.externalDa.SelectCommand.Parameters.Add("@clockworkid", num);
								try
								{
									this.externalDa.Fill(new DataTable());
								}
								catch (Exception ex)
								{
									string message = ex.Message;
								}
							}
						}
					}
					catch (Exception ex2)
					{
						DialogResult dialogResult = MessageBox.Show(ex2.ToString(), "Click 'cancel' to abort!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
						if (dialogResult == DialogResult.Cancel)
						{
							break;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C10C File Offset: 0x0000B10C
		private void AddUpdateProf(DataRow dr)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C110 File Offset: 0x0000B110
		public override int NumChanges()
		{
			int num = 0;
			if (this._importItems != null)
			{
				foreach (ImportItem importItem in this._importItems)
				{
					if (importItem.imported)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C150 File Offset: 0x0000B150
		public static string FixInstructorName(string instructorName)
		{
			string text;
			string text2;
			string text3;
			return ImportODBCLuCourses.FixInstructorName(instructorName, out text, out text2, out text3);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C16C File Offset: 0x0000B16C
		public static string FixInstructorName(string instructorName, out string problemString, out string firstName, out string lastName)
		{
			string text = "";
			instructorName = instructorName.Replace("/", ",");
			instructorName = instructorName.Replace("\\", ",");
			instructorName = instructorName.Replace("&", ",");
			instructorName = instructorName.Replace("+", ",");
			foreach (char c in instructorName)
			{
				if (char.IsLetter(c) || c == ',' || c == '.' || c == ' ' || c == '-')
				{
					text += c;
				}
			}
			problemString = null;
			string[] array = text.Split(new char[]
			{
				','
			});
			if (array.Length > 2)
			{
				text = array[0] + ", " + array[1];
				problemString = "Too many commas.";
			}
			array = text.Split(new char[]
			{
				','
			});
			if (array.Length == 2)
			{
				firstName = array[1];
				lastName = array[0];
			}
			else
			{
				text.IndexOf(' ');
				array = text.Split(new char[]
				{
					' '
				});
				if (array.Length == 2)
				{
					problemString = "Missing comma, suspicious space";
					firstName = array[0];
					lastName = array[1];
				}
				else if (array.Length > 2)
				{
					problemString = "Too many spaces, missing comma";
					firstName = "";
					lastName = text;
				}
				else
				{
					firstName = "";
					lastName = text;
					problemString = "Missing comma";
				}
			}
			string text3 = "";
			foreach (char c2 in firstName)
			{
				if (c2 == '.')
				{
					break;
				}
				text3 += c2;
			}
			firstName = text3.Trim();
			lastName = lastName.Trim();
			if (firstName.Length > 0)
			{
				return lastName + ", " + firstName;
			}
			return lastName;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C344 File Offset: 0x0000B344
		private void SortDataView(ref DataView dv, params int[] colIndices)
		{
			string text = "";
			foreach (int index in colIndices)
			{
				if (text.Length > 0)
				{
					text += ",";
				}
				text += dv.Table.Columns[index].ColumnName;
			}
			dv.Sort = text;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C3A8 File Offset: 0x0000B3A8
		private string ExtractRowString(DataRow dr, params int[] colIndices)
		{
			string text = "";
			foreach (int columnIndex in colIndices)
			{
				if (text.Length > 0)
				{
					text += "~";
				}
				text += dr[columnIndex].ToString().Trim().ToLower();
			}
			return text;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C404 File Offset: 0x0000B404
		public override ProblemType ImportToMemory()
		{
			this.externalDa.SelectCommand.CommandText = this._importSQL;
			this.externalDa.SelectCommand.Parameters.Clear();
			this.externalDa.SelectCommand.Parameters.Add("@now", DateTime.Now);
			this._importTable = new DataTable("importtable");
			try
			{
				this.externalDa.Fill(this._importTable);
			}
			catch (Exception ex)
			{
				this._importTable = new DataTable();
				MessageBox.Show(ex.Message);
			}
			DataView dataView = new DataView(this._importTable);
			this.SortDataView(ref dataView, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			});
			string text = null;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in dataView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				string text2 = this.ExtractRowString(row, new int[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7
				});
				if (text != null && text.CompareTo(text2) == 0)
				{
					arrayList.Add(row);
				}
				text = text2;
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row2 = (DataRow)obj2;
				this._importTable.Rows.Remove(row2);
			}
			dataView = null;
			arrayList.Clear();
			arrayList = null;
			string text3 = this._settings.GetSettingString(601).Trim().ToLower();
			if (text3.Length > 0)
			{
				string[] array = text3.Split(new char[]
				{
					','
				});
				foreach (object obj3 in this._importTable.Rows)
				{
					DataRow dataRow = (DataRow)obj3;
					string strB = dataRow[8].ToString().Trim().ToLower();
					bool flag = false;
					foreach (string text4 in array)
					{
						if (text4.CompareTo(strB) == 0)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						dataRow[8] = "";
					}
				}
			}
			ProblemType problemType = ProblemType.None;
			if (this._importTable == null || this._importTable.Rows.Count <= 0)
			{
				this._importItems = null;
			}
			else
			{
				this._importItems = new ImportItem[this._importTable.Rows.Count];
				int num = 0;
				foreach (object obj4 in this._importTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj4;
					base.FireIncrementProgressBar();
					dataRow2[8] = ImportODBCLuCourses.FixInstructorName(dataRow2[8].ToString().Trim());
					ImportItem importItem = new ImportItem(dataRow2);
					ImportProblem[] importProblems = this.ImportItem(importItem);
					if (!importItem.ignoreThisItem)
					{
						importItem._ImportProblems = importProblems;
						this._importItems[num++] = importItem;
						if (importItem._ImportProblems != null)
						{
							foreach (ImportProblem importProblem in importItem._ImportProblems)
							{
								problemType |= importProblem._problemType;
							}
						}
					}
				}
				ImportItem[] array3 = new ImportItem[num];
				Array.Copy(this._importItems, array3, num);
				this._importItems = null;
				this._importItems = array3;
			}
			return problemType;
		}

		// Token: 0x040000A8 RID: 168
		private const int Col_StartDate = 0;

		// Token: 0x040000A9 RID: 169
		private const int Col_EndDate = 1;

		// Token: 0x040000AA RID: 170
		private const int Col_Term = 2;

		// Token: 0x040000AB RID: 171
		private const int Col_Duration = 3;

		// Token: 0x040000AC RID: 172
		private const int Col_Subject = 4;

		// Token: 0x040000AD RID: 173
		private const int Col_Course = 5;

		// Token: 0x040000AE RID: 174
		private const int Col_TimeOfDay = 6;

		// Token: 0x040000AF RID: 175
		private const int Col_Section = 7;

		// Token: 0x040000B0 RID: 176
		private const int Col_Instructor = 8;

		// Token: 0x040000B1 RID: 177
		private const int Col_InstructorEmail = 9;

		// Token: 0x040000B2 RID: 178
		private const int Col_InstructorPhone = 10;

		// Token: 0x040000B3 RID: 179
		private const int Col_CrossListCode = 11;

		// Token: 0x040000B4 RID: 180
		private const int Col_CourseNote = 12;

		// Token: 0x040000B5 RID: 181
		private const int Col_SubjectId = 13;

		// Token: 0x040000B6 RID: 182
		private const int Col_ProfId = 14;

		// Token: 0x040000B7 RID: 183
		private const int Col_LuCourseID = 15;

		// Token: 0x040000B8 RID: 184
		private int _newLuCourseId = -2;

		// Token: 0x040000B9 RID: 185
		private int _newLuCourseDataID = -1;

		// Token: 0x040000BA RID: 186
		private DataTable _internal_luCourses;

		// Token: 0x040000BB RID: 187
		private DataTable _internal_luCourseData;

		// Token: 0x040000BC RID: 188
		private ArrayList userConfirmedDurationTermCombo_dates = new ArrayList();

		// Token: 0x040000BD RID: 189
		private bool showDuration;

		// Token: 0x040000BE RID: 190
		private bool showTimeOfDay;

		// Token: 0x040000BF RID: 191
		private NameValueCollection crossListCodes = new NameValueCollection();

		// Token: 0x0200002E RID: 46
		private enum InternalRow
		{
			// Token: 0x040000C1 RID: 193
			LuCourses,
			// Token: 0x040000C2 RID: 194
			LuCourseDataSubjects,
			// Token: 0x040000C3 RID: 195
			LuCourseDataProfs
		}
	}
}
