using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003E5 RID: 997
	public class DataSyncMapperItemBase
	{
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000221AD File Offset: 0x000203AD
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x000221B5 File Offset: 0x000203B5
		public int ControlId { get; set; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x000221BE File Offset: 0x000203BE
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x000221C6 File Offset: 0x000203C6
		public string ControlCaption { get; set; }

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000221CF File Offset: 0x000203CF
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x000221D7 File Offset: 0x000203D7
		public eControlCode ControlCode { get; set; }
	}
}
