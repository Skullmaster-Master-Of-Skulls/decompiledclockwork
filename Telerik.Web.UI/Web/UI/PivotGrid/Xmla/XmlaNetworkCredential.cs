using System;
using System.Net;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D9C RID: 3484
	public class XmlaNetworkCredential : NetworkCredential
	{
		// Token: 0x060081D9 RID: 33241 RVA: 0x001DA168 File Offset: 0x001D8368
		public XmlaNetworkCredential()
		{
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x001DA170 File Offset: 0x001D8370
		public XmlaNetworkCredential(string username, string password) : base(username, password)
		{
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x001DA17A File Offset: 0x001D837A
		public XmlaNetworkCredential(string username, string password, string domain) : base(username, password, domain)
		{
		}
	}
}
