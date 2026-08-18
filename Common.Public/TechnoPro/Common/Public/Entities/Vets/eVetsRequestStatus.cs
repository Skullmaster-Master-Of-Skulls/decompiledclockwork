using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public enum eVetsRequestStatus
	{
		// Token: 0x0400028B RID: 651
		[VeteranRequestStatus("Pending")]
		Unspecified,
		// Token: 0x0400028C RID: 652
		[VeteranRequestStatus("In-progress")]
		InProgress,
		// Token: 0x0400028D RID: 653
		[VeteranRequestStatus("Approved")]
		Approved,
		// Token: 0x0400028E RID: 654
		[VeteranRequestStatus("Denied")]
		Denied
	}
}
