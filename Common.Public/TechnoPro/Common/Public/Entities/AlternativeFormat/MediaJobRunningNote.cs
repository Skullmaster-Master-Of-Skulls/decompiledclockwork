using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000582 RID: 1410
	public class MediaJobRunningNote : BusinessBase<int>
	{
		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000323BC File Offset: 0x000305BC
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x0000E258 File Offset: 0x0000C458
		public int NoteId
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

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000323D4 File Offset: 0x000305D4
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x000323DC File Offset: 0x000305DC
		public string Notes { get; set; }

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000323E5 File Offset: 0x000305E5
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000323ED File Offset: 0x000305ED
		public DateTime LastModifiedDatetime { get; set; }

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000323F6 File Offset: 0x000305F6
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x000323FE File Offset: 0x000305FE
		public PersonBase WhoModified { get; set; }
	}
}
