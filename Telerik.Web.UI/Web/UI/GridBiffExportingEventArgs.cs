using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x02000B72 RID: 2930
	public class GridBiffExportingEventArgs : EventArgs
	{
		// Token: 0x06006E80 RID: 28288 RVA: 0x00199AA5 File Offset: 0x00197CA5
		public GridBiffExportingEventArgs(ExportStructure structure)
		{
			this.structure = structure;
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x06006E81 RID: 28289 RVA: 0x00199AB4 File Offset: 0x00197CB4
		// (set) Token: 0x06006E82 RID: 28290 RVA: 0x00199ABC File Offset: 0x00197CBC
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

		// Token: 0x04001DD4 RID: 7636
		private ExportStructure structure;
	}
}
