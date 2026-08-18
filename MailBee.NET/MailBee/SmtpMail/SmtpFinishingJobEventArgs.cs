using System;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x02000156 RID: 342
	public class SmtpFinishingJobEventArgs : CommonEventArgs
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x00031615 File Offset: 0x00030615
		internal SmtpFinishingJobEventArgs(SendMailJob A_0, bc A_1) : base(A_1)
		{
			this.a = A_0;
			this.b = A_0.KeepProducedJobs;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00031631 File Offset: 0x00030631
		public SendMailJob Job
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00031639 File Offset: 0x00030639
		public bool IsSuccessful
		{
			get
			{
				return this.a.ErrorReason == null;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00031649 File Offset: 0x00030649
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00031651 File Offset: 0x00030651
		public bool KeepIt
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x04000878 RID: 2168
		private SendMailJob a;

		// Token: 0x04000879 RID: 2169
		private bool b;
	}
}
