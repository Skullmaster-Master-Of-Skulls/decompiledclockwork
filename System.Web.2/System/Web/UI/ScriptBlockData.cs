using System;

namespace System.Web.UI
{
	// Token: 0x02000317 RID: 791
	internal class ScriptBlockData : SourceLineInfo
	{
		// Token: 0x060024FB RID: 9467 RVA: 0x0007A518 File Offset: 0x00078718
		internal ScriptBlockData(int line, int column, string virtualPath)
		{
			base.Line = line;
			this.Column = column;
			base.VirtualPath = virtualPath;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x0007A535 File Offset: 0x00078735
		// (set) Token: 0x060024FD RID: 9469 RVA: 0x0007A53D File Offset: 0x0007873D
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

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x0007A546 File Offset: 0x00078746
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x0007A54E File Offset: 0x0007874E
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

		// Token: 0x04001D5E RID: 7518
		protected string _script;

		// Token: 0x04001D5F RID: 7519
		private int _column;
	}
}
