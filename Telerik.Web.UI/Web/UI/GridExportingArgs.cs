using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001133 RID: 4403
	public class GridExportingArgs : EventArgs
	{
		// Token: 0x170039FF RID: 14847
		// (get) Token: 0x0600B382 RID: 45954 RVA: 0x00271775 File Offset: 0x0026F975
		public ExportType ExportType
		{
			get
			{
				return this._exportType;
			}
		}

		// Token: 0x17003A00 RID: 14848
		// (get) Token: 0x0600B383 RID: 45955 RVA: 0x0027177D File Offset: 0x0026F97D
		// (set) Token: 0x0600B384 RID: 45956 RVA: 0x00271785 File Offset: 0x0026F985
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

		// Token: 0x0600B385 RID: 45957 RVA: 0x0027179D File Offset: 0x0026F99D
		public GridExportingArgs(string exportOutput, ExportType exportType)
		{
			this._exportOutput = exportOutput;
			this._exportType = exportType;
		}

		// Token: 0x04002F43 RID: 12099
		private string _exportOutput;

		// Token: 0x04002F44 RID: 12100
		private ExportType _exportType;
	}
}
