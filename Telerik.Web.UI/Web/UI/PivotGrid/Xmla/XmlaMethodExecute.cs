using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D90 RID: 3472
	internal class XmlaMethodExecute : XmlaMethodBase
	{
		// Token: 0x06008119 RID: 33049 RVA: 0x001D7C53 File Offset: 0x001D5E53
		public XmlaMethodExecute(XmlaTextBodyCommand commandToExecute)
		{
			this.Command = commandToExecute;
		}

		// Token: 0x170028F5 RID: 10485
		// (get) Token: 0x0600811A RID: 33050 RVA: 0x001D7C62 File Offset: 0x001D5E62
		// (set) Token: 0x0600811B RID: 33051 RVA: 0x001D7C6A File Offset: 0x001D5E6A
		public IXmlaCommand Command { get; set; }
	}
}
