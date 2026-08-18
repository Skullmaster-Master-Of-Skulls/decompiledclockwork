using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	public class AppointmentNotFoundException : Exception
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0006E524 File Offset: 0x0006C724
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0006E52C File Offset: 0x0006C72C
		public int AppointmentId { get; set; }

		// Token: 0x06000A7A RID: 2682 RVA: 0x0006E535 File Offset: 0x0006C735
		public AppointmentNotFoundException()
		{
			this.AppointmentId = 0;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0006E547 File Offset: 0x0006C747
		public AppointmentNotFoundException(string msg) : base(msg)
		{
			this.AppointmentId = 0;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0006E55A File Offset: 0x0006C75A
		public AppointmentNotFoundException(string msg, Exception inner) : base(msg, inner)
		{
			this.AppointmentId = 0;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0006E56E File Offset: 0x0006C76E
		public AppointmentNotFoundException(int appId)
		{
			this.AppointmentId = appId;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0006E580 File Offset: 0x0006C780
		public override string Message
		{
			get
			{
				return base.Message + " (appid=" + this.AppointmentId.ToString() + ")";
			}
		}
	}
}
