using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch
{
	// Token: 0x02000599 RID: 1433
	[Flags]
	[Serializable]
	public enum eBookSearchProviderType
	{
		// Token: 0x0400208D RID: 8333
		ExternalOnly = 1,
		// Token: 0x0400208E RID: 8334
		LocalOnly = 2,
		// Token: 0x0400208F RID: 8335
		All = 3
	}
}
