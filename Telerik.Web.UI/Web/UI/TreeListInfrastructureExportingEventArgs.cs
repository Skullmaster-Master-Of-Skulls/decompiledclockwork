using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x02000970 RID: 2416
	public class TreeListInfrastructureExportingEventArgs : EventArgs
	{
		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x001183D8 File Offset: 0x001165D8
		// (set) Token: 0x06005BDC RID: 23516 RVA: 0x001183E0 File Offset: 0x001165E0
		public ExportStructure ExportStructure { get; internal set; }

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x001183E9 File Offset: 0x001165E9
		// (set) Token: 0x06005BDE RID: 23518 RVA: 0x001183F1 File Offset: 0x001165F1
		public ExportFormat ExportFormat { get; internal set; }

		// Token: 0x06005BDF RID: 23519 RVA: 0x001183FA File Offset: 0x001165FA
		public TreeListInfrastructureExportingEventArgs(ExportStructure exportStructure, ExportFormat exportFormat)
		{
			this.ExportStructure = exportStructure;
			this.ExportFormat = exportFormat;
		}
	}
}
