using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authorization
{
	// Token: 0x02000497 RID: 1175
	public enum eLookupMethod
	{
		// Token: 0x04001A7D RID: 6781
		Default,
		// Token: 0x04001A7E RID: 6782
		ByUsername,
		// Token: 0x04001A7F RID: 6783
		ByStudentNumberOrEmployeeId,
		// Token: 0x04001A80 RID: 6784
		ByEmail = 4,
		// Token: 0x04001A81 RID: 6785
		ByCustomField = 8
	}
}
