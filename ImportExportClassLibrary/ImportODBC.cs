using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using AutoComboBox;
using EncryptionClassLibrary;
using Microsoft.Data.Odbc;
using SettingsPermissions;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x0200002A RID: 42
	public class ImportODBC
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600013D RID: 317 RVA: 0x00009690 File Offset: 0x00008690
		// (remove) Token: 0x0600013E RID: 318 RVA: 0x000096C8 File Offset: 0x000086C8
		public event EventHandler FinishedImporting;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600013F RID: 319 RVA: 0x00009700 File Offset: 0x00008700
		// (remove) Token: 0x06000140 RID: 320 RVA: 0x00009738 File Offset: 0x00008738
		public event EventHandler IncrementProgressBar;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000141 RID: 321 RVA: 0x00009770 File Offset: 0x00008770
		// (remove) Token: 0x06000142 RID: 322 RVA: 0x000097A8 File Offset: 0x000087A8
		public event ImportODBC.MessageFromPluginHandler MessageFromPlugin;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000097DD File Offset: 0x000087DD
		public DataTable ImportTable
		{
			get
			{
				return this._importTable;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000097E5 File Offset: 0x000087E5
		public string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000097ED File Offset: 0x000087ED
		public OdbcConnection Con
		{
			get
			{
				return this.con;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000097F5 File Offset: 0x000087F5
		public int DataRowCount
		{
			get
			{
				if (this._importTable == null)
				{
					return 0;
				}
				return this._importTable.Rows.Count;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009814 File Offset: 0x00008814
		public ImportODBC(Form ParentForm, Settings settings, DateTime _SessionStartDate, DateTime _SessionEndDate, UnivDataAdapter _ClockWorkDa, TripleDESEncryptionClass _TripleDES, string _ConnectionString, string _ImportSQL, string _UpdateSQL, NameValueCollection Parameters)
		{
			this._clockWorkDa = _ClockWorkDa;
			this._tripleDES = _TripleDES;
			this._connectionString = _ConnectionString;
			this._importSQL = _ImportSQL;
			this._updateSQL = _UpdateSQL;
			this._sessionStartDate = _SessionStartDate;
			this._sessionEndDate = _SessionEndDate;
			this._parentForm = ParentForm;
			this._settings = settings;
			this._parameters = Parameters;
			try
			{
				this.con = new OdbcConnection(this._connectionString);
				this.externalDa = new OdbcDataAdapter("", this.con);
			}
			catch (Exception ex)
			{
				this.con = null;
				this.externalDa = null;
				this._connectionString = "";
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000098E0 File Offset: 0x000088E0
		public ImportODBC(Form ParentForm, Settings settings, DateTime _SessionStartDate, DateTime _SessionEndDate, UnivDataAdapter _ClockWorkDa, TripleDESEncryptionClass _TripleDES, string _ConnectionString, string _ImportSQL, string _UpdateSQL)
		{
			this._clockWorkDa = _ClockWorkDa;
			this._tripleDES = _TripleDES;
			this._connectionString = _ConnectionString;
			this._importSQL = _ImportSQL;
			this._updateSQL = _UpdateSQL;
			this._sessionStartDate = _SessionStartDate;
			this._sessionEndDate = _SessionEndDate;
			this._parentForm = ParentForm;
			this._settings = settings;
			this._parameters = new NameValueCollection();
			try
			{
				this.con = new OdbcConnection(this._connectionString);
				this.externalDa = new OdbcDataAdapter("", this.con);
			}
			catch (Exception)
			{
				this.con = null;
				this.externalDa = null;
				this._connectionString = "";
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000099A4 File Offset: 0x000089A4
		public virtual DataTable ExecuteQuery(string query)
		{
			if (this.externalDa != null)
			{
				this.externalDa.SelectCommand.CommandText = query;
				this.externalDa.SelectCommand.Parameters.Clear();
				this.externalDa.SelectCommand.Parameters.Add("@currentsessionstartdate", this._sessionStartDate);
				this.externalDa.SelectCommand.Parameters.Add("@currentsessionenddate", this._sessionEndDate);
				this.externalDa.SelectCommand.Parameters.Add("@now", DateTime.Now);
				DataTable dataTable = new DataTable();
				try
				{
					this.externalDa.Fill(dataTable);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					dataTable = new DataTable();
				}
				return dataTable;
			}
			return null;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009A90 File Offset: 0x00008A90
		public virtual ProblemType ImportToMemory()
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
			ProblemType problemType = ProblemType.None;
			if (this._importTable == null || this._importTable.Rows.Count <= 0)
			{
				this._importItems = null;
			}
			else
			{
				this._importItems = new ImportItem[this._importTable.Rows.Count];
				int num = 0;
				foreach (object obj in this._importTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					this.FireIncrementProgressBar();
					ImportItem importItem = new ImportItem(dataRow);
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
				ImportItem[] array = new ImportItem[num];
				Array.Copy(this._importItems, array, num);
				this._importItems = null;
				this._importItems = array;
			}
			return problemType;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009C68 File Offset: 0x00008C68
		public void FireIncrementProgressBar()
		{
			if (this.IncrementProgressBar != null && this.im != null && this.im.IsHandleCreated)
			{
				this.im.BeginInvoke(this.IncrementProgressBar, new object[]
				{
					this,
					new EventArgs()
				});
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009CB8 File Offset: 0x00008CB8
		public virtual ImportProblem[] ImportItem(ImportItem ii)
		{
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009CBC File Offset: 0x00008CBC
		public virtual void FillListView(ListView lv)
		{
			lv.Parent.Text = string.Concat(new string[]
			{
				"Import Manager [",
				this._sessionStartDate.ToShortDateString(),
				" - ",
				this._sessionEndDate.ToShortDateString(),
				"]"
			});
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00009D18 File Offset: 0x00008D18
		public void IgnoreItem(ImportItem ii)
		{
			DataRow dataRow = ii._dataRow;
			this._importTable.Rows.Remove(dataRow);
			this.ignoredItems.Add(ii);
			if (this._importItems.Length <= 1)
			{
				this._importItems = null;
			}
			else
			{
				ImportItem[] array = new ImportItem[this._importItems.Length - 1];
				int num = 0;
				foreach (ImportItem importItem in this._importItems)
				{
					if (importItem != ii)
					{
						array[num++] = importItem;
					}
				}
				this._importItems = array;
			}
			ii = null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009DA7 File Offset: 0x00008DA7
		public virtual HowProblemWasFixed FixProblem(ImportItem importItem, ProblemSolution problemSolution)
		{
			return HowProblemWasFixed.NothingDoneBecauseOfError;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009DAC File Offset: 0x00008DAC
		public int GetNumberOfProblems()
		{
			int num = 0;
			foreach (ImportItem importItem in this._importItems)
			{
				if (importItem._ImportProblems != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009DE1 File Offset: 0x00008DE1
		public virtual ImportItemStatus ImportOneItem(ImportItem importItem)
		{
			return ImportItemStatus.Unknown;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009DE4 File Offset: 0x00008DE4
		public virtual bool Save()
		{
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009DE7 File Offset: 0x00008DE7
		public virtual int NumChanges()
		{
			return 0;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009DEC File Offset: 0x00008DEC
		public virtual void ShowImportManager(Form sender, string startDirectory)
		{
			this.pw = new PleaseWait();
			this.pw.Show();
			this.im = new ImportManager(this._parentForm, this._settings, this, startDirectory);
			this.IncrementProgressBar += this.im.IncrementProgressBar;
			this.im.FinishedInit += this.im_FinishedInit;
			this.thread = new Thread(new ThreadStart(this.im.Init));
			this.thread.Start();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009E7D File Offset: 0x00008E7D
		private void ShowImportManager2()
		{
			this.im.Init();
			this.pw.Close();
			this.pw.Dispose();
			this.im.ShowDialog();
			this.FireFinishedImporting();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009EB4 File Offset: 0x00008EB4
		private void FireFinishedImporting()
		{
			if (this.FinishedImporting != null)
			{
				if (this._parentForm != null && this._parentForm.IsHandleCreated)
				{
					this._parentForm.BeginInvoke(this.FinishedImporting, new object[]
					{
						this,
						new EventArgs()
					});
					return;
				}
				this.FinishedImporting(this, new EventArgs());
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009F16 File Offset: 0x00008F16
		public virtual void DiscardItem(ImportItem importItem)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009F18 File Offset: 0x00008F18
		public virtual string FixUpdateString(string updateString, DataRow externalDataRow)
		{
			string text = string.Copy(updateString);
			Regex regex = new Regex("@_([a-z]|[_])+");
			MatchCollection matchCollection = regex.Matches(updateString);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text2 = match.Value;
				text2 = text2.Substring(2);
				int num = externalDataRow.Table.Columns.IndexOf(text2);
				if (num >= 0)
				{
					"@" + text2;
					text = text.Replace(match.Value, "'" + externalDataRow[num].ToString().Trim() + "'");
				}
			}
			return text;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009FF0 File Offset: 0x00008FF0
		private void im_FinishedInit(object sender, EventArgs e)
		{
			this.pw.Close();
			this.pw.Dispose();
			this.im.ShowDialog();
			this.FireFinishedImporting();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A01A File Offset: 0x0000901A
		public virtual DataTable GetMainDataTable()
		{
			return this._importTable;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A022 File Offset: 0x00009022
		public virtual void SendMessageToPlugin(object o)
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A024 File Offset: 0x00009024
		public void FireMessageFromPlugin(object o)
		{
			if (this.MessageFromPlugin != null)
			{
				this.MessageFromPlugin(o);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A03C File Offset: 0x0000903C
		public virtual void Log(int logCode, int personid, string exam_form_id, int courseid, int appid, DateTime timeStamp, string memo)
		{
			int num;
			if (this._settings != null)
			{
				num = this._settings.whoAmIPersonID;
			}
			else
			{
				num = -999;
			}
			this._clockWorkDa.SelectCommand.CommandText = "INSERT INTO examimportlog (examimportlogcodeid,personid,exam_form_id_external,lucourseid,appid,whoami_personid,memo) VALUES (@examimportlogcodeid,@personid,@exam_form_id_external,@lucourseid,@appid,@whoami_personid,@memo)";
			this._clockWorkDa.SelectCommand.Parameters.Clear();
			this._clockWorkDa.SelectCommand.Parameters.Add("@examimportlogcodeid", logCode);
			this._clockWorkDa.SelectCommand.Parameters.Add("@personid", personid);
			this._clockWorkDa.SelectCommand.Parameters.Add("@exam_form_id_external", exam_form_id);
			this._clockWorkDa.SelectCommand.Parameters.Add("@lucourseid", courseid);
			this._clockWorkDa.SelectCommand.Parameters.Add("@appid", appid);
			this._clockWorkDa.SelectCommand.Parameters.Add("@whoami_personid", num);
			this._clockWorkDa.SelectCommand.Parameters.Add("@memo", memo);
			DataTable t = new DataTable();
			try
			{
				this._clockWorkDa.Fill(t);
			}
			catch
			{
			}
		}

		// Token: 0x0400008F RID: 143
		protected string _connectionString;

		// Token: 0x04000090 RID: 144
		protected OdbcConnection con;

		// Token: 0x04000091 RID: 145
		protected OdbcDataAdapter externalDa;

		// Token: 0x04000092 RID: 146
		protected string _importSQL;

		// Token: 0x04000093 RID: 147
		protected string _updateSQL;

		// Token: 0x04000094 RID: 148
		protected DataTable _importTable;

		// Token: 0x04000095 RID: 149
		protected ImportItem[] _importItems;

		// Token: 0x04000096 RID: 150
		protected TripleDESEncryptionClass _tripleDES;

		// Token: 0x04000097 RID: 151
		protected UnivDataAdapter _clockWorkDa;

		// Token: 0x04000098 RID: 152
		protected DateTime _sessionStartDate;

		// Token: 0x04000099 RID: 153
		protected DateTime _sessionEndDate;

		// Token: 0x0400009A RID: 154
		protected ArrayList ignoredItems = new ArrayList();

		// Token: 0x0400009B RID: 155
		protected ImportManager im;

		// Token: 0x0400009C RID: 156
		protected Form _parentForm;

		// Token: 0x0400009D RID: 157
		protected Settings _settings;

		// Token: 0x0400009E RID: 158
		protected NameValueCollection _parameters;

		// Token: 0x040000A2 RID: 162
		private PleaseWait pw;

		// Token: 0x040000A3 RID: 163
		private Thread thread;

		// Token: 0x0200002B RID: 43
		public enum LogCode
		{
			// Token: 0x040000A5 RID: 165
			Unknown,
			// Token: 0x040000A6 RID: 166
			ExecutedSqlAgainstExternalDb,
			// Token: 0x040000A7 RID: 167
			SavedImport
		}

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x0600015F RID: 351
		public delegate void MessageFromPluginHandler(object o);
	}
}
