using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000539 RID: 1337
	[Serializable]
	public class Booking
	{
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x0002CE1C File Offset: 0x0002B01C
		// (set) Token: 0x06002A8E RID: 10894 RVA: 0x0002CE34 File Offset: 0x0002B034
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

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x0002CE40 File Offset: 0x0002B040
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x0002CE58 File Offset: 0x0002B058
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

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x0002CE64 File Offset: 0x0002B064
		// (set) Token: 0x06002A92 RID: 10898 RVA: 0x0002CE7C File Offset: 0x0002B07C
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

		// Token: 0x06002A93 RID: 10899 RVA: 0x0002CE86 File Offset: 0x0002B086
		public Booking()
		{
			this.pid = 0;
			this.startDateTime = DateTime.MinValue;
			this.endDateTime = DateTime.MinValue;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0002CEAD File Offset: 0x0002B0AD
		public Booking(int pid, DateTime startDateTime, DateTime endDateTime)
		{
			this.pid = pid;
			this.startDateTime = startDateTime;
			this.endDateTime = endDateTime;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0002CECC File Offset: 0x0002B0CC
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

		// Token: 0x04001E5C RID: 7772
		private int pid;

		// Token: 0x04001E5D RID: 7773
		private DateTime startDateTime;

		// Token: 0x04001E5E RID: 7774
		private DateTime endDateTime;
	}
}
