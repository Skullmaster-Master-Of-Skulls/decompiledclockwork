using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D94 RID: 3476
	internal class XmlaTextBodyCommand : IXmlaCommand
	{
		// Token: 0x06008139 RID: 33081 RVA: 0x001D80DA File Offset: 0x001D62DA
		public XmlaTextBodyCommand(string name, string bodyText)
		{
			this.Name = name;
			this.Body = bodyText;
		}

		// Token: 0x170028F8 RID: 10488
		// (get) Token: 0x0600813A RID: 33082 RVA: 0x001D80F0 File Offset: 0x001D62F0
		// (set) Token: 0x0600813B RID: 33083 RVA: 0x001D80F8 File Offset: 0x001D62F8
		public string Name { get; private set; }

		// Token: 0x170028F9 RID: 10489
		// (get) Token: 0x0600813C RID: 33084 RVA: 0x001D8101 File Offset: 0x001D6301
		// (set) Token: 0x0600813D RID: 33085 RVA: 0x001D8109 File Offset: 0x001D6309
		public string Body { get; private set; }
	}
}
