using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x0200036D RID: 877
	public class GridInfrastructureExportingEventArgs : EventArgs
	{
		// Token: 0x06001E21 RID: 7713 RVA: 0x0005DC43 File Offset: 0x0005BE43
		public GridInfrastructureExportingEventArgs(ExportStructure exportStructure, ExportType exportFormat)
		{
			this.structure = exportStructure;
			this.format = exportFormat;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0005DC59 File Offset: 0x0005BE59
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x0005DC61 File Offset: 0x0005BE61
		public ExportType ExportFormat
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0005DC6A File Offset: 0x0005BE6A
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x0005DC72 File Offset: 0x0005BE72
		public ExportStructure ExportStructure
		{
			get
			{
				return this.structure;
			}
			set
			{
				this.structure = value;
			}
		}

		// Token: 0x04000778 RID: 1912
		private ExportStructure structure;

		// Token: 0x04000779 RID: 1913
		private ExportType format;
	}
}
