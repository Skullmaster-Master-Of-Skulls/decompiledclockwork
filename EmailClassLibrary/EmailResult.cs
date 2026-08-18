using System;

namespace EmailClassLibrary
{
	// Token: 0x02000004 RID: 4
	public class EmailResult
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002418 File Offset: 0x00001418
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002420 File Offset: 0x00001420
		public virtual EmailTemplate Email { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002429 File Offset: 0x00001429
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002431 File Offset: 0x00001431
		public virtual string Message { get; set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002410 File Offset: 0x00001410
		public EmailResult()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000243A File Offset: 0x0000143A
		public EmailResult(bool worked, Exception ex)
		{
			this.ex = ex;
			this.worked = worked;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002450 File Offset: 0x00001450
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002458 File Offset: 0x00001458
		public bool Worked
		{
			get
			{
				return this.worked;
			}
			set
			{
				this.worked = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002461 File Offset: 0x00001461
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002469 File Offset: 0x00001469
		public Exception Exception
		{
			get
			{
				return this.ex;
			}
			set
			{
				this.ex = value;
			}
		}

		// Token: 0x0400000B RID: 11
		private bool worked;

		// Token: 0x0400000C RID: 12
		private Exception ex;
	}
}
