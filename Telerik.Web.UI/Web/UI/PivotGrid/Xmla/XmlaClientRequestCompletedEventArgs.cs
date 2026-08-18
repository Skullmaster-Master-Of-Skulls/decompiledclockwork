using System;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D89 RID: 3465
	internal class XmlaClientRequestCompletedEventArgs : EventArgs
	{
		// Token: 0x06008101 RID: 33025 RVA: 0x001D7A18 File Offset: 0x001D5C18
		public XmlaClientRequestCompletedEventArgs(string result, XmlaClientRequestInfo requestInfo, OlapCommunicationException error)
		{
			this.Result = result;
			this.RequestInfo = requestInfo;
			this.Error = error;
		}

		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x06008102 RID: 33026 RVA: 0x001D7A35 File Offset: 0x001D5C35
		// (set) Token: 0x06008103 RID: 33027 RVA: 0x001D7A3D File Offset: 0x001D5C3D
		public OlapCommunicationException Error { get; private set; }

		// Token: 0x170028F0 RID: 10480
		// (get) Token: 0x06008104 RID: 33028 RVA: 0x001D7A46 File Offset: 0x001D5C46
		// (set) Token: 0x06008105 RID: 33029 RVA: 0x001D7A4E File Offset: 0x001D5C4E
		public string Result { get; private set; }

		// Token: 0x170028F1 RID: 10481
		// (get) Token: 0x06008106 RID: 33030 RVA: 0x001D7A57 File Offset: 0x001D5C57
		// (set) Token: 0x06008107 RID: 33031 RVA: 0x001D7A5F File Offset: 0x001D5C5F
		internal XmlaClientRequestInfo RequestInfo { get; private set; }
	}
}
