using System;

namespace TechnoPro.Common.Public.Entities.Communications
{
	// Token: 0x02000444 RID: 1092
	public class CommunicationBase : BusinessBase<int>
	{
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000253F0 File Offset: 0x000235F0
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CommunicationId
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

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00025408 File Offset: 0x00023608
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x00025410 File Offset: 0x00023610
		public int PersonId { get; set; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00025419 File Offset: 0x00023619
		// (set) Token: 0x0600211E RID: 8478 RVA: 0x00025421 File Offset: 0x00023621
		public DateTime DateSendAttempted { get; set; }

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0002542A File Offset: 0x0002362A
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x00025432 File Offset: 0x00023632
		public eCommunicationSendMethod SendAttemptedMethods { get; set; }

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x0002543B File Offset: 0x0002363B
		// (set) Token: 0x06002122 RID: 8482 RVA: 0x00025443 File Offset: 0x00023643
		public bool SentSuccessfully { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x0002544C File Offset: 0x0002364C
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x00025454 File Offset: 0x00023654
		public string ErrorMessage { get; set; }

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0002545D File Offset: 0x0002365D
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x00025465 File Offset: 0x00023665
		public virtual int WhoSentPersonId { get; set; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x0002546E File Offset: 0x0002366E
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x00025476 File Offset: 0x00023676
		public string Subject { get; set; }

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0002547F File Offset: 0x0002367F
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x00025487 File Offset: 0x00023687
		public string Body { get; set; }
	}
}
