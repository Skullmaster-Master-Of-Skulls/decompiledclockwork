using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x0200074D RID: 1869
	public class PivotGridBiffExportingEventArgs : EventArgs
	{
		// Token: 0x06004238 RID: 16952 RVA: 0x000D00CB File Offset: 0x000CE2CB
		public PivotGridBiffExportingEventArgs(ExportStructure structure)
		{
			this._exportStructure = structure;
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000D00DA File Offset: 0x000CE2DA
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x000D00E2 File Offset: 0x000CE2E2
		public ExportStructure ExportStructure
		{
			get
			{
				return this._exportStructure;
			}
			set
			{
				this._exportStructure = value;
			}
		}

		// Token: 0x0400118F RID: 4495
		private ExportStructure _exportStructure;
	}
}
