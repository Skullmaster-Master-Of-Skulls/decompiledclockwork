using System;

namespace MailBee.AntiSpam
{
	// Token: 0x0200012D RID: 301
	public class RblStatus
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x0002D1E5 File Offset: 0x0002C1E5
		internal RblStatus(string A_0, bool A_1, string A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = false;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002D209 File Offset: 0x0002C209
		internal RblStatus(string A_0)
		{
			this.a = A_0;
			this.b = false;
			this.c = null;
			this.d = true;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002D22D File Offset: 0x0002C22D
		public string RblHost
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002D235 File Offset: 0x0002C235
		public string RblReplyText
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002D23D File Offset: 0x0002C23D
		public bool IsIPAddressInRbl
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002D245 File Offset: 0x0002C245
		public bool IsError
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x040007A0 RID: 1952
		private string a;

		// Token: 0x040007A1 RID: 1953
		private bool b;

		// Token: 0x040007A2 RID: 1954
		private string c;

		// Token: 0x040007A3 RID: 1955
		private bool d;
	}
}
