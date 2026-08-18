using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B3D RID: 2877
	public class EditorExportingArgs : CancelEventArgs
	{
		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x06006C99 RID: 27801 RVA: 0x0019371B File Offset: 0x0019191B
		public ExportType ExportType
		{
			get
			{
				return this._exportType;
			}
		}

		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x06006C9A RID: 27802 RVA: 0x00193723 File Offset: 0x00191923
		// (set) Token: 0x06006C9B RID: 27803 RVA: 0x0019372B File Offset: 0x0019192B
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

		// Token: 0x06006C9C RID: 27804 RVA: 0x00193743 File Offset: 0x00191943
		public EditorExportingArgs(string exportOutput, ExportType exportType)
		{
			this._exportOutput = exportOutput;
			this._exportType = exportType;
		}

		// Token: 0x04001D36 RID: 7478
		private string _exportOutput;

		// Token: 0x04001D37 RID: 7479
		private readonly ExportType _exportType;
	}
}
