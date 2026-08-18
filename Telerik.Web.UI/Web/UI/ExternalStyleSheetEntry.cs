using System;
using System.IO;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000F4E RID: 3918
	internal class ExternalStyleSheetEntry : ScriptEntry
	{
		// Token: 0x17002F54 RID: 12116
		// (get) Token: 0x06009586 RID: 38278 RVA: 0x00216885 File Offset: 0x00214A85
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17002F55 RID: 12117
		// (get) Token: 0x06009587 RID: 38279 RVA: 0x0021688D File Offset: 0x00214A8D
		public long LastModified
		{
			get
			{
				if (this._lastModified == -1L && !this._disableLastModifiedLookup)
				{
					this._lastModified = ExternalStyleSheetUtils.GetLastModifiedInTicks(this._path);
				}
				return this._lastModified;
			}
		}

		// Token: 0x17002F56 RID: 12118
		// (set) Token: 0x06009588 RID: 38280 RVA: 0x002168B8 File Offset: 0x00214AB8
		internal bool DisableLastModifiedLookup
		{
			set
			{
				this._disableLastModifiedLookup = value;
			}
		}

		// Token: 0x17002F57 RID: 12119
		// (get) Token: 0x06009589 RID: 38281 RVA: 0x002168C1 File Offset: 0x00214AC1
		public override bool IsExternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600958A RID: 38282 RVA: 0x002168C4 File Offset: 0x00214AC4
		public ExternalStyleSheetEntry()
		{
		}

		// Token: 0x0600958B RID: 38283 RVA: 0x002168CC File Offset: 0x00214ACC
		public ExternalStyleSheetEntry(string path) : base(string.Empty, string.Empty, string.Empty)
		{
			this._path = path;
			this._lastModified = -1L;
		}

		// Token: 0x0600958C RID: 38284 RVA: 0x002168F4 File Offset: 0x00214AF4
		public override bool Equals(object obj)
		{
			ExternalStyleSheetEntry externalStyleSheetEntry = obj as ExternalStyleSheetEntry;
			if (externalStyleSheetEntry != null)
			{
				return this._path.Equals(externalStyleSheetEntry._path);
			}
			InvalidScriptEntry invalidScriptEntry = obj as InvalidScriptEntry;
			return invalidScriptEntry != null && invalidScriptEntry.Equals(this);
		}

		// Token: 0x0600958D RID: 38285 RVA: 0x00216930 File Offset: 0x00214B30
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x0600958E RID: 38286 RVA: 0x0021693D File Offset: 0x00214B3D
		internal override string GetSerializedAssemblyInfo()
		{
			return ";" + "|";
		}

		// Token: 0x0600958F RID: 38287 RVA: 0x0021694E File Offset: 0x00214B4E
		internal override string GetSerializedScriptEntryInfo()
		{
			return ":" + ScriptEntry.GetHashCode(this._path);
		}

		// Token: 0x06009590 RID: 38288 RVA: 0x00216965 File Offset: 0x00214B65
		public override string GetScript()
		{
			return ExternalStyleSheetUtils.LoadContent(this._path);
		}

		// Token: 0x06009591 RID: 38289 RVA: 0x00216974 File Offset: 0x00214B74
		public override Stream GetResourceStream()
		{
			string s = ExternalStyleSheetUtils.LoadContent(this._path);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return new MemoryStream(bytes);
		}

		// Token: 0x04002AC4 RID: 10948
		internal const string AssemblyInfoExternal = "|";

		// Token: 0x04002AC5 RID: 10949
		private readonly string _path;

		// Token: 0x04002AC6 RID: 10950
		private long _lastModified;

		// Token: 0x04002AC7 RID: 10951
		private bool _disableLastModifiedLookup;
	}
}
