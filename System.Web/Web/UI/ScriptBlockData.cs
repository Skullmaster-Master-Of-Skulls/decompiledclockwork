using System;

namespace System.Web.UI
{
	// Token: 0x02000476 RID: 1142
	internal class ScriptBlockData : SourceLineInfo
	{
		// Token: 0x060035BA RID: 13754 RVA: 0x000E7FA8 File Offset: 0x000E6FA8
		internal ScriptBlockData(int line, int column, string virtualPath)
		{
			base.Line = line;
			this.Column = column;
			base.VirtualPath = virtualPath;
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000E7FC5 File Offset: 0x000E6FC5
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000E7FCD File Offset: 0x000E6FCD
		internal int Column
		{
			get
			{
				return this._column;
			}
			set
			{
				this._column = value;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000E7FD6 File Offset: 0x000E6FD6
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000E7FDE File Offset: 0x000E6FDE
		internal string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				this._script = value;
			}
		}

		// Token: 0x04002552 RID: 9554
		protected string _script;

		// Token: 0x04002553 RID: 9555
		private int _column;
	}
}
