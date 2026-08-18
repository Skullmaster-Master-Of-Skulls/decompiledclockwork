using System;
using System.Data;

namespace ImportExportClassLibrary
{
	// Token: 0x0200001F RID: 31
	public class ImportItem
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004E89 File Offset: 0x00003E89
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004E91 File Offset: 0x00003E91
		public ImportProblem[] _ImportProblems
		{
			get
			{
				return this._importProblems;
			}
			set
			{
				this._importProblems = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004E9C File Offset: 0x00003E9C
		public string Problems
		{
			get
			{
				if (this._importProblems == null)
				{
					return "None.";
				}
				string text = "";
				foreach (ImportProblem importProblem in this._importProblems)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += importProblem._problemDescription;
				}
				return text;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004EF9 File Offset: 0x00003EF9
		public ImportItem(ImportProblem[] _ImportProblems, DataRow _DataRow)
		{
			this._importProblems = _ImportProblems;
			this._dataRow = _DataRow;
			this.extraNote = "";
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F21 File Offset: 0x00003F21
		public ImportItem(DataRow _DataRow)
		{
			this._importProblems = null;
			this._dataRow = _DataRow;
			this.extraNote = "";
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F4C File Offset: 0x00003F4C
		public override string ToString()
		{
			if (this._importProblems == null)
			{
				return "No problems; ready to import.";
			}
			string text = "";
			foreach (ImportProblem importProblem in this._importProblems)
			{
				if (text.Length > 0)
				{
					text += Environment.NewLine;
				}
				text += importProblem.ToString();
			}
			return text;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004FA9 File Offset: 0x00003FA9
		public void ClearProblems()
		{
			this._importProblems = null;
		}

		// Token: 0x0400002E RID: 46
		private ImportProblem[] _importProblems;

		// Token: 0x0400002F RID: 47
		public DataRow _dataRow;

		// Token: 0x04000030 RID: 48
		public DataRow[] internalRows;

		// Token: 0x04000031 RID: 49
		public DataTable[] internalTables;

		// Token: 0x04000032 RID: 50
		public bool imported;

		// Token: 0x04000033 RID: 51
		public bool discarded;

		// Token: 0x04000034 RID: 52
		public bool ignoreThisItem;

		// Token: 0x04000035 RID: 53
		public string extraNote;

		// Token: 0x04000036 RID: 54
		public int appID = -1;

		// Token: 0x04000037 RID: 55
		public bool bool1;

		// Token: 0x04000038 RID: 56
		public bool bool2;
	}
}
