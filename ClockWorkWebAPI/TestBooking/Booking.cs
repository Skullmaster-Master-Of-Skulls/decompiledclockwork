using System;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class Booking
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000103B0 File Offset: 0x0000E5B0
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000103C8 File Offset: 0x0000E5C8
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000103D4 File Offset: 0x0000E5D4
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000103EC File Offset: 0x0000E5EC
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTime;
			}
			set
			{
				this.startDateTime = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000103F8 File Offset: 0x0000E5F8
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00010410 File Offset: 0x0000E610
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTime;
			}
			set
			{
				this.endDateTime = value;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001041A File Offset: 0x0000E61A
		public Booking()
		{
			this.pid = 0;
			this.startDateTime = DateTime.MinValue;
			this.endDateTime = DateTime.MinValue;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010441 File Offset: 0x0000E641
		public Booking(int pid, DateTime startDateTime, DateTime endDateTime)
		{
			this.pid = pid;
			this.startDateTime = startDateTime;
			this.endDateTime = endDateTime;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010460 File Offset: 0x0000E660
		public bool OverlapsWith(int pid, DateTime sdt, DateTime edt)
		{
			bool flag = this.pid == pid && this.startDateTime != DateTime.MinValue && this.endDateTime != DateTime.MinValue;
			bool result;
			if (flag)
			{
				DateTime t = this.startDateTime;
				DateTime t2 = this.endDateTime;
				result = (!(t2 < sdt) && !(t > edt));
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000157 RID: 343
		private int pid;

		// Token: 0x04000158 RID: 344
		private DateTime startDateTime;

		// Token: 0x04000159 RID: 345
		private DateTime endDateTime;
	}
}
