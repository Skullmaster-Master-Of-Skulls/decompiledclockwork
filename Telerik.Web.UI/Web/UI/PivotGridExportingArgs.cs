using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200074B RID: 1867
	public class PivotGridExportingArgs : EventArgs
	{
		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x000D0052 File Offset: 0x000CE252
		// (set) Token: 0x06004230 RID: 16944 RVA: 0x000D005A File Offset: 0x000CE25A
		public string ExportOutput
		{
			get
			{
				return this._exportOutput;
			}
			set
			{
				this._exportOutput = ((!string.IsNullOrEmpty(value)) ? value : string.Empty);
			}
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000D0072 File Offset: 0x000CE272
		public PivotGridExportingArgs(string exportOutput)
		{
			this._exportOutput = exportOutput;
		}

		// Token: 0x0400118C RID: 4492
		private string _exportOutput;
	}
}
