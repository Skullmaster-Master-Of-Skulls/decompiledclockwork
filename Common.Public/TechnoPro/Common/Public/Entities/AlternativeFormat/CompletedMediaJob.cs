using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000592 RID: 1426
	public class CompletedMediaJob : MediaJob
	{
		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x00033035 File Offset: 0x00031235
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x0003303D File Offset: 0x0003123D
		public DateTime CompletedOn { get; set; }

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x00033048 File Offset: 0x00031248
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x00032FDF File Offset: 0x000311DF
		public override bool IsCancelled
		{
			get
			{
				return false;
			}
			set
			{
				base.IsCancelled = value;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x0003305C File Offset: 0x0003125C
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x00032FFF File Offset: 0x000311FF
		public override bool IsCompleted
		{
			get
			{
				return true;
			}
			set
			{
				base.IsCompleted = value;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x0003306F File Offset: 0x0003126F
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x00033077 File Offset: 0x00031277
		public PersonBase CompletedBy { get; set; }

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x00033080 File Offset: 0x00031280
		// (set) Token: 0x06002E5D RID: 11869 RVA: 0x00033088 File Offset: 0x00031288
		public string CompletedNotes { get; set; }
	}
}
