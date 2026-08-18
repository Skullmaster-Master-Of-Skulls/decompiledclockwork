using System;
using System.Net;
using MailBee;

namespace a
{
	// Token: 0x020004B5 RID: 1205
	internal class ai
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x000C0216 File Offset: 0x000BF216
		public ai()
		{
			this.a = null;
			this.b = null;
			this.c = TopLevelProtocolType.Unknown;
			this.d = false;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000C023A File Offset: 0x000BF23A
		public ai(string A_0)
		{
			this.a = null;
			this.b = A_0;
			this.c = TopLevelProtocolType.Ews;
			this.d = false;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000C025E File Offset: 0x000BF25E
		public IPEndPoint d()
		{
			return this.a;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000C0266 File Offset: 0x000BF266
		public string b()
		{
			return this.b;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000C026E File Offset: 0x000BF26E
		public TopLevelProtocolType e()
		{
			return this.c;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000C0276 File Offset: 0x000BF276
		internal void a(IPEndPoint A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000C027F File Offset: 0x000BF27F
		internal void a(string A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000C0288 File Offset: 0x000BF288
		internal void a()
		{
			this.a = null;
			this.b = null;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000C0298 File Offset: 0x000BF298
		internal void a(TopLevelProtocolType A_0)
		{
			this.c = A_0;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000C02A1 File Offset: 0x000BF2A1
		public bool c()
		{
			return this.d;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000C02A9 File Offset: 0x000BF2A9
		public void a(bool A_0)
		{
			A_0 = this.d;
		}

		// Token: 0x04001C18 RID: 7192
		private IPEndPoint a;

		// Token: 0x04001C19 RID: 7193
		private string b;

		// Token: 0x04001C1A RID: 7194
		private TopLevelProtocolType c;

		// Token: 0x04001C1B RID: 7195
		private bool d;
	}
}
