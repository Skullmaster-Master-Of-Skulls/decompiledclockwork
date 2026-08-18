using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Communications
{
	// Token: 0x02000446 RID: 1094
	public class StudentCommunicationHistory : BusinessBase<int>
	{
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x00025490 File Offset: 0x00023690
		// (set) Token: 0x0600212D RID: 8493 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentPersonId
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

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x000254A8 File Offset: 0x000236A8
		// (set) Token: 0x0600212F RID: 8495 RVA: 0x000254B0 File Offset: 0x000236B0
		public IList<Communication> Communications { get; set; }
	}
}
