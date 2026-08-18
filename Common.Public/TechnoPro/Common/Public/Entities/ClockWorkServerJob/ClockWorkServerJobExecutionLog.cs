using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000456 RID: 1110
	public class ClockWorkServerJobExecutionLog : BusinessBase<int>
	{
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000259B4 File Offset: 0x00023BB4
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ExecutionLogId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000259CC File Offset: 0x00023BCC
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x000259D4 File Offset: 0x00023BD4
		public ClockWorkServerJobStep Step { get; set; }

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000259DD File Offset: 0x00023BDD
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x000259E5 File Offset: 0x00023BE5
		public eClockWorkServerJobResult Status { get; set; }

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000259EE File Offset: 0x00023BEE
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x000259F6 File Offset: 0x00023BF6
		public DateTime StartTime { get; set; }

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000259FF File Offset: 0x00023BFF
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x00025A07 File Offset: 0x00023C07
		public DateTime? EndTime { get; set; }

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x00025A10 File Offset: 0x00023C10
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x00025A18 File Offset: 0x00023C18
		public string Message { get; set; }

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00025A21 File Offset: 0x00023C21
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x00025A29 File Offset: 0x00023C29
		public string ServerIpAddress { get; set; }

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00025A32 File Offset: 0x00023C32
		// (set) Token: 0x060021D7 RID: 8663 RVA: 0x00025A3A File Offset: 0x00023C3A
		public Guid TransactionId { get; set; }
	}
}
