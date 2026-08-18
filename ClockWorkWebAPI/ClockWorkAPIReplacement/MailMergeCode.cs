using System;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000061 RID: 97
	public class MailMergeCode
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00021E04 File Offset: 0x00020004
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00021E1C File Offset: 0x0002001C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value.ToLower();
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00021E2B File Offset: 0x0002002B
		public MailMergeCode(string name)
		{
			this.name = name;
		}

		// Token: 0x0400028F RID: 655
		private string name;
	}
}
