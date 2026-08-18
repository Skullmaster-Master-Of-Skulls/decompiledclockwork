using System;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D86 RID: 3462
	internal class SoapEnvelope
	{
		// Token: 0x060080F9 RID: 33017 RVA: 0x001D79BF File Offset: 0x001D5BBF
		public SoapEnvelope()
		{
			this.Header = new SoapHeader();
			this.Body = new SoapBody();
		}

		// Token: 0x170028EC RID: 10476
		// (get) Token: 0x060080FA RID: 33018 RVA: 0x001D79DD File Offset: 0x001D5BDD
		// (set) Token: 0x060080FB RID: 33019 RVA: 0x001D79E5 File Offset: 0x001D5BE5
		public SoapHeader Header { get; set; }

		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x060080FC RID: 33020 RVA: 0x001D79EE File Offset: 0x001D5BEE
		// (set) Token: 0x060080FD RID: 33021 RVA: 0x001D79F6 File Offset: 0x001D5BF6
		public SoapBody Body { get; set; }
	}
}
