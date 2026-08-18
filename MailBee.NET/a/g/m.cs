using System;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x02000406 RID: 1030
	internal abstract class m
	{
		// Token: 0x0600243E RID: 9278 RVA: 0x0009A0B2 File Offset: 0x000990B2
		public m()
		{
			this.a = 0;
			this.b = DateTime.MinValue;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0009A0CC File Offset: 0x000990CC
		public void d()
		{
			this.a++;
			this.b = DateTime.Now;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0009A0E7 File Offset: 0x000990E7
		public void b()
		{
			this.a = 0;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0009A0F0 File Offset: 0x000990F0
		public bool e()
		{
			return this.a < DnsCache.SmtpMXMaxFailureCount || DateTime.Now > this.b.AddMilliseconds((double)DnsCache.SmtpMXNextAttemptInterval);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x0009A11C File Offset: 0x0009911C
		public bool c()
		{
			return this.a == 0;
		}

		// Token: 0x06002443 RID: 9283
		public abstract h a5();

		// Token: 0x04001813 RID: 6163
		private int a;

		// Token: 0x04001814 RID: 6164
		private DateTime b;
	}
}
