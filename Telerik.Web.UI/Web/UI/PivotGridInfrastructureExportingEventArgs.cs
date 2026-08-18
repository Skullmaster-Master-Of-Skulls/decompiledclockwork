using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x0200074E RID: 1870
	public class PivotGridInfrastructureExportingEventArgs : EventArgs
	{
		// Token: 0x0600423B RID: 16955 RVA: 0x000D00EB File Offset: 0x000CE2EB
		public PivotGridInfrastructureExportingEventArgs(ExportStructure exportStructure, PivotGridExportFormat exportFormat)
		{
			this._exportStructure = exportStructure;
			this._exportFormat = exportFormat;
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x000D0101 File Offset: 0x000CE301
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x000D0109 File Offset: 0x000CE309
		public PivotGridExportFormat ExportFormat
		{
			get
			{
				return this._exportFormat;
			}
			internal set
			{
				this._exportFormat = value;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x000D0112 File Offset: 0x000CE312
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x000D011A File Offset: 0x000CE31A
		public ExportStructure ExportStructure
		{
			get
			{
				return this._exportStructure;
			}
			internal set
			{
				this._exportStructure = value;
			}
		}

		// Token: 0x04001190 RID: 4496
		private ExportStructure _exportStructure;

		// Token: 0x04001191 RID: 4497
		private PivotGridExportFormat _exportFormat;
	}
}
