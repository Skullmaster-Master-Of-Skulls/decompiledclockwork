using System;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000181 RID: 385
	internal class NotificationInformation
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0009950C File Offset: 0x0009770C
		internal virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00099514 File Offset: 0x00097714
		internal virtual long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0009951C File Offset: 0x0009771C
		internal NotificationInformation(long stamp)
		{
			this.timestamp = stamp;
			this.count = 0;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00099534 File Offset: 0x00097734
		internal virtual void addCount()
		{
			this.count++;
		}

		// Token: 0x0400111F RID: 4383
		internal long timestamp;

		// Token: 0x04001120 RID: 4384
		internal int count;
	}
}
