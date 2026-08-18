using System;

namespace TechnoPro.Common.Public.Entities.Veteran
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public enum eVeteranRequestStatus
	{
		// Token: 0x040002C9 RID: 713
		[VeteranRequestStatus("Pending")]
		Unspecified,
		// Token: 0x040002CA RID: 714
		[VeteranRequestStatus("Approved")]
		Approved,
		// Token: 0x040002CB RID: 715
		[VeteranRequestStatus("Denied")]
		Denied
	}
}
